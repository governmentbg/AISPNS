using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.Data.DataModel
{
    public partial class AistnContext : DbContext
    {


        /// <summary>
        /// Here we set specifics when some properties are identity and dont need to be edited, insterted etc.
        /// </summary>
        /// <param name="builder">This is scaffoled model builder which is visible in the scaffold class AistnContext</param>
        partial void OnModelCreatingPartial(ModelBuilder builder)
        {

            #region Messages
            builder.Entity<Message>().Property(x => x.Number).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            #endregion

            
        }
    }
}
