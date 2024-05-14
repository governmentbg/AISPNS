using AISTN.Data.DataModel;
using AISTN.Data.LogDataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace AISTN.Repository
{
    public class AistnContextLoggable : AistnContext
    {
        private readonly IDbContextFactory<LogAistnContext> _logDbFactory;

        public AistnContextLoggable(DbContextOptions<AistnContext> options, IDbContextFactory<LogAistnContext> logDbFactory) : base(options)
        {
            Database.SetCommandTimeout(600);
            _logDbFactory = logDbFactory;
        }

        ///Prop used for linking user action to DBcontextLog revords
        private LogUserAction? _currentUserAction { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("SaveChangesAsync not implemented yet");
        }

        /// <summary>
        /// Method that makes a record in UserActivityLog table linked to the DBContextLog records
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public int SaveChanges(UserActivity? activity)
        {
            //Prepare LogUserAction row if there is a user activity present
            if (activity != null)
            {
                _currentUserAction = new LogUserAction()
                {
                    IpAddress = activity.IpAddress,
                    Message = activity.Message,
                    Timestamp = DateTime.Now,
                    UserActionType = activity.ActionType.ToString(),
                    UserId = activity.UserID.Value,
                };
            }

            return SaveChanges();
        }

        /// <summary>
        /// Method saving details of the inserted/updated/removed rows and data in the DB, apart from saving the changes
        /// </summary>
        /// <returns></returns>
        [Obsolete("Are you sure you have to use this override? Consider using _db.SaveChanges(UserActivity.NewActivity(_currentUser, eUserActionType._______))", error: true)]
        public override int SaveChanges()
        {
            try
            {
                var _logDb = _logDbFactory.CreateDbContext();

                ChangeTracker.DetectChanges();

                var entries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Deleted).ToList();
                var logs = entries.Select(x => GetLogDbcontext(x, x.State)).ToList();

                var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();

                int count = base.SaveChanges();

                var addedLogs = addedEntries.Select(x => GetLogDbcontext(x, EntityState.Added)).ToList();

                logs.AddRange(addedLogs);

                _logDb.LogDbcontexts.AddRange(logs);

                _logDb.SaveChanges();

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Reset the member
                _currentUserAction = null;
            }
        }

        private LogDbcontext GetLogDbcontext(EntityEntry entry, EntityState state)
        {
            var modifiedProperties = GetModifiedProperties(entry, state);

            return new LogDbcontext
            {
                ActionTable = entry.Entity.GetType().ToString(),
                Timestamp = DateTime.UtcNow,
                OldValue = JsonConvert.SerializeObject(modifiedProperties.Previous),
                NewValue = JsonConvert.SerializeObject(modifiedProperties.Current),
                ActionType = state.ToString(),
                TableRow = GetPrimaryKeyValue(entry),
                //Attach a UserActivityLog row to the DB Changes rows
                UserActionLog = _currentUserAction
            };
        }

        private ModifiedProperties GetModifiedProperties(EntityEntry entry, EntityState state)
        {
            var differences = new ModifiedProperties();

            foreach (var property in entry.Properties)
            {
                switch (state)
                {
                    case EntityState.Added:
                        SetDifferences(differences, property.CurrentValue?.GetType() == typeof(byte[]), property.Metadata.Name, null, property.CurrentValue);
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                            SetDifferences(differences, property.CurrentValue?.GetType() == typeof(byte[]), property.Metadata.Name, property.OriginalValue, property.CurrentValue);
                        break;
                    case EntityState.Deleted:
                        SetDifferences(differences, property.OriginalValue?.GetType() == typeof(byte[]), property.Metadata.Name, property.OriginalValue, null);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return differences;
        }

        private void SetDifferences(ModifiedProperties differences, bool isByteArray, string name, object? previousValue, object? currentValue)
        {
            if (previousValue != null)
                differences.Previous.Add(name, isByteArray ? "ByteArray1" : previousValue);

            if (currentValue != null)
                differences.Current.Add(name, isByteArray ? "ByteArray2" : currentValue);
        }

        private string? GetPrimaryKeyValue(EntityEntry entry)
        {
            var keyName = entry.Metadata.FindPrimaryKey()?.Properties.Select(x => x.Name).First();
            var value = entry.Properties.Single(x => x.Metadata.Name == keyName).OriginalValue;

            return value?.ToString();
        }
    }

    public class ModifiedProperties
    {
        public Dictionary<string, object?> Previous { get; private set; }
        public Dictionary<string, object?> Current { get; private set; }

        public ModifiedProperties()
        {
            Previous = new Dictionary<string, object?>();
            Current = new Dictionary<string, object?>();
        }
    }
}
