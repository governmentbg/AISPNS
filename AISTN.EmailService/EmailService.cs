using AISTN.Common.Helper;
using AISTN.Data.DataModel;
using AISTN.Repository;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace AISTN.EmailService
{
    public class EmailService
    {
        private readonly AistnContextLoggable _context;

        public EmailService(AistnContextLoggable context)
        {
            _context = context;
        }

        public async Task SendNotificationEmail()
        {
            var syndics = _context.Syndics.Include(x => x.Installments)
                                             .ThenInclude(x => x.InstallmentKind)
                                          .Include(x => x.Installments)
                                             .ThenInclude(x => x.InstallmentYear);

            foreach (var syndic in syndics)
            {
                // check if there is an installment close to expiration (7 days)
                var installment = syndic.Installments.Where(x => x.TerminationDeadline.Value.AddDays(-7) <= DateTime.Today).FirstOrDefault();

                if (installment != null)
                {
                    await SendEmailAsync(syndic, installment);
                }
            }
        }

        private async Task<bool> SendEmailAsync(Syndic syndic, Installment installment)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse("aistn@bobo.bg"));
                message.To.Add(MailboxAddress.Parse($"{syndic.Email}"));
                message.Subject = "Изтичаш срок за вноска";
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = $"Срока за заплащане на годишната вноска на синдика за {installment.InstallmentYear} изтича на дата {installment.TerminationDeadline}."
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(SmtpProfile.SmtpServer, SmtpProfile.Port);
                    await client.AuthenticateAsync(SmtpProfile.Username, SmtpProfile.Password);

                    await client.SendAsync(message);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }

            return false;
        }
    }
}
