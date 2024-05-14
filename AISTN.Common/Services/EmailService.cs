using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Data.DataModel;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System.Text.RegularExpressions;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace AISTN.Common.Services
{
    [Injectable]
    public class EmailService
    {
        private readonly AistnContextLoggable _context;
        private readonly ExceptionLogger _logger;
        private readonly IConfiguration _configuration;
        private int _deadline;
        private string emailRegex = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        private string emailSuffix = string.Empty;
        private string emailSender = string.Empty;
        private string smtpServer = string.Empty;
        private int smtpPort = default;
        private string smtpUsername = string.Empty;
        private string smtpPassword = string.Empty;
        private bool useSSL = false;

        public EmailService(AistnContextLoggable context, ExceptionLogger logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _deadline = _context.Settings.FirstOrDefault().NotificationDeadline.Value;

            emailSuffix = _configuration.GetValue<string>("EmailProperties:EmailSuffix")!;
            emailSender = _configuration.GetValue<string>("EmailProperties:EmailSender")!;
            smtpServer = _configuration.GetValue<string>("EmailProperties:SmtpServer")!;
            smtpPort = _configuration.GetValue<int>("EmailProperties:Port");
            smtpUsername = _configuration.GetValue<string>("EmailProperties:Username")!;
            smtpPassword = _configuration.GetValue<string>("EmailProperties:Password")!;
            useSSL = _configuration.GetValue<bool>("EmailProperties:UseSSL")!;
        }

        public bool SendEmail()
        {
            try
            {
                var syndics = _context.Syndics.Include(x => x.Installments)
                                                  .ThenInclude(x => x.InstallmentYear)
                                              .Where(x => x.Installments
                                              .Any(x => x.TerminationDeadline.Value.Day <= _deadline));

                foreach (var syndic in syndics)
                {
                    var installment = syndic.Installments.FirstOrDefault(x => x.TerminationDeadline != null);

                    if (installment != null && syndic.Email != null)
                    {
                        SendEmail(syndic, installment);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return false;
            };
        }

        public bool SendEmail(User user)
        {
            try
            {
                if (IsValidEmail(user.Email))
                {
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse(emailSender));
                    message.To.Add(MailboxAddress.Parse($"{user.Email}"));
                    message.Subject = EmailReason.NewProfileMessage;
                    message.Body = new TextPart(TextFormat.Html) { Text = "Получихте ново съобщение, влезте в профила си за да го видите." + emailSuffix };

                    using (var client = new SmtpClient())
                    {
                        if (useSSL == true)
                        {
                            client.Connect(smtpServer, smtpPort, true);
                        }
                        else
                        {
                            client.Connect(smtpServer, smtpPort, SecureSocketOptions.None);
                        }
                        client.Authenticate(smtpUsername, smtpPassword);
                        client.Send(message);

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return false;
            }
        }

        public bool SendEmail(Syndic syndic, Installment installment)
        {
            try
            {
                if (IsValidEmail(syndic.Email) && installment.TerminationDeadline != null && installment.TerminationDeadline.Value.Day <= _deadline)
                {
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse(emailSender));
                    message.To.Add(MailboxAddress.Parse($"{syndic.Email}"));
                    message.Subject = EmailReason.InstallmentExpiring;
                    message.Body = new TextPart(TextFormat.Html)
                    {
                        Text = $"Срока за заплащане на годишната ви вноска за {installment.InstallmentYear.Year} година" +
                               $" изтича на {installment.TerminationDeadline.Value.ToString("dd/MM/yyyy")}." + emailSuffix
                    };

                    using (var client = new SmtpClient())
                    {
                        if (useSSL == true)
                        {
                            client.Connect(smtpServer, smtpPort, true);
                        }
                        else
                        {
                            client.Connect(smtpServer, smtpPort, SecureSocketOptions.None);
                        }
                        client.Authenticate(smtpUsername, smtpPassword);

                        client.Send(message);

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }

            return true;
        }

        public bool SendEmail(Syndic syndic, string emailReason, string bodyMessage)
        {
            try
            {
                if (IsValidEmail(syndic.Email))
                {
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse(emailSender));
                    message.To.Add(MailboxAddress.Parse($"{syndic.Email}"));
                    message.Subject = emailReason;
                    message.Body = new TextPart(TextFormat.Html) { Text = bodyMessage.Replace("\n", "<br />") + emailSuffix };

                    using (var client = new SmtpClient())
                    {
                        if (useSSL == true)
                        {
                            client.Connect(smtpServer, smtpPort, true);
                        }
                        else
                        {
                            client.Connect(smtpServer, smtpPort, SecureSocketOptions.None);
                        }
                        client.Authenticate(smtpUsername, smtpPassword);

                        client.Send(message);

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return false;
            }
        }

        public bool SendCustomEmail(SendEmailDTO emailDTO)
        {
            try
            {
                var syndic = _context.Syndics.Include(x => x.Installments)
                                                .ThenInclude(x => x.InstallmentYear)
                                             .FirstOrDefault(x => x.Id == emailDTO.SyndicId);

                if (IsValidEmail(syndic.Email))
                {
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse(emailSender));
                    message.To.Add(MailboxAddress.Parse($"{syndic.Email}"));
                    message.Subject = emailDTO.Subject;
                    message.Body = new TextPart(TextFormat.Html) { Text = emailDTO.Body.Replace("\n", "<br />") + emailSuffix };
                    

                    using (SmtpClient client = new SmtpClient())
                    {
                        if (useSSL == true)
                        {
                            client.Connect(smtpServer, smtpPort, true);
                        }
                        else
                        {
                            client.Connect(smtpServer, smtpPort, SecureSocketOptions.None);
                        }
                        client.Authenticate(smtpUsername, smtpPassword);

                        client.Send(message);

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return false;
            }
        }

        private bool IsValidEmail(string email)
        {
            if (email == null)
            {
                return false;
            }

            return Regex.IsMatch(email, emailRegex);
        }
    }
}
