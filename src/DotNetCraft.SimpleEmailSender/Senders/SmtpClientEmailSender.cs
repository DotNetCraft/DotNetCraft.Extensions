using System;
using System.Net.Mail;
using System.Threading;
#if (!NET40)
using System.Threading.Tasks;
#endif
using DotNetCraft.SimpleEmailSender.Entities;
using DotNetCraft.SimpleEmailSender.Entities.Enums;
using DotNetCraft.SimpleEmailSender.Interfaces;

namespace DotNetCraft.SimpleEmailSender.Senders
{
    public class SmtpClientEmailSender : IEmailSender
    {
        public bool UseSsl { get; set; }

        private readonly SmtpClient _smtpClient;

        public SmtpClientEmailSender(SmtpClient smtpClient)
        {
            if (smtpClient == null)
                throw new ArgumentNullException("smtpClient");
            _smtpClient = smtpClient;
            UseSsl = true;
        }

#if (NET40)
        public SendResponse Send(EmailMessage emailMessage, CancellationToken? token = null)
        {
            var response = new SendResponse();
            _smtpClient.EnableSsl = UseSsl;

            var message = CreateMailMessage(emailMessage);

            if (token.HasValue && token.Value.IsCancellationRequested)
            {
                response.ErrorMessages.Add("Message was cancelled by cancellation token.");
                return response;
            }

            _smtpClient.Send(message);

            return response;
        }
#else

        public SendResponse Send(EmailMessage emailMessage, CancellationToken? token = null)
        {
            return SendAsync(emailMessage, token).GetAwaiter().GetResult();
        }

        public async Task<SendResponse> SendAsync(EmailMessage emailMessage, CancellationToken? token = null)
        {
            var response = new SendResponse();
            _smtpClient.EnableSsl = UseSsl;

            var message = CreateMailMessage(emailMessage);

            if (token?.IsCancellationRequested ?? false)
            {
                response.ErrorMessages.Add("Message was cancelled by cancellation token.");
                return response;
            }

            await _smtpClient.SendMailAsync(message);

            return response;
        }
#endif

        private MailMessage CreateMailMessage(EmailMessage emailMessage)
        {
            var message = new MailMessage
            {
                Subject = emailMessage.Subject,
                Body = emailMessage.Body,
                IsBodyHtml = emailMessage.BodyType == BodyType.Html,
                From = new MailAddress(emailMessage.FromAddress.EmailAddress, emailMessage.FromAddress.Name)
            };

            emailMessage.ToAddresses.ForEach(x =>
            {
                message.To.Add(new MailAddress(x.EmailAddress, x.Name));
            });

            emailMessage.CcAddresses.ForEach(x =>
            {
                message.CC.Add(new MailAddress(x.EmailAddress, x.Name));
            });

            emailMessage.BccAddresses.ForEach(x =>
            {
                message.Bcc.Add(new MailAddress(x.EmailAddress, x.Name));
            });

            emailMessage.ReplyToAddresses.ForEach(x =>
            {
                message.ReplyToList.Add(new MailAddress(x.EmailAddress, x.Name));
            });

            switch (emailMessage.Priority)
            {
                case EmailPriotiry.LowPriority:
                    message.Priority = MailPriority.Low;
                    break;
                case EmailPriotiry.NormalPriority:
                    message.Priority = MailPriority.Normal;
                    break;
                case EmailPriotiry.HighPriority:
                    message.Priority = MailPriority.High;
                    break;
            }

            emailMessage.Attachments.ForEach(x =>
            {
                message.Attachments.Add(new System.Net.Mail.Attachment(x.Data, x.Filename, x.ContentType));
            });

            return message;
        }
    }
}
