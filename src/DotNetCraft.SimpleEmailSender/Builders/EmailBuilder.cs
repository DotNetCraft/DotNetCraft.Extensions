using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using DotNetCraft.SimpleEmailSender.Entities;
using DotNetCraft.SimpleEmailSender.Entities.Enums;
using DotNetCraft.SimpleEmailSender.Interfaces;

namespace DotNetCraft.SimpleEmailSender.Builders
{
    public class EmailBuilder : IEmailBuilder
    {
        private readonly IContentBuilder _contentBuilder;
        private readonly EmailMessage emailMessage;
        public EmailBuilder(IContentBuilder contentBuilder)
        {
            if (contentBuilder == null)
                throw new ArgumentNullException("contentBuilder");
            _contentBuilder = contentBuilder;
            emailMessage = new EmailMessage();
        }

        #region Implementation of IEmailBuilder

        public IEmailBuilder To(string emailAddress, string name = "")
        {
            Recipient recipient = string.IsNullOrWhiteSpace(name) ? new Recipient(emailAddress) : new Recipient(emailAddress, name);
            emailMessage.ToAddresses.Add(recipient);
            return this;
        }

        public IEmailBuilder To(IList<Recipient> recipientAddresses)
        {
            emailMessage.ToAddresses.AddRange(recipientAddresses);
            return this;
        }

        public IEmailBuilder CC(string emailAddress, string name = "")
        {
            Recipient recipient = string.IsNullOrWhiteSpace(name) ? new Recipient(emailAddress) : new Recipient(emailAddress, name);
            emailMessage.CcAddresses.Add(recipient);
            return this;
        }

        public IEmailBuilder CC(IList<Recipient> recipientAddresses)
        {
            emailMessage.CcAddresses.AddRange(recipientAddresses);
            return this;
        }

        public IEmailBuilder BCC(string emailAddress, string name = "")
        {
            Recipient recipient = string.IsNullOrWhiteSpace(name) ? new Recipient(emailAddress) : new Recipient(emailAddress, name);
            emailMessage.BccAddresses.Add(recipient);
            return this;
        }

        public IEmailBuilder BCC(IList<Recipient> recipientAddresses)
        {
            emailMessage.BccAddresses.AddRange(recipientAddresses);
            return this;
        }

        public IEmailBuilder ReplyTo(string emailAddress, string name = "")
        {
            Recipient recipient = string.IsNullOrWhiteSpace(name) ? new Recipient(emailAddress) : new Recipient(emailAddress, name);
            emailMessage.ReplyToAddresses.Add(recipient);
            return this;
        }

        public IEmailBuilder ReplyTo(IList<Recipient> recipientAddresses)
        {
            emailMessage.ReplyToAddresses.AddRange(recipientAddresses);
            return this;
        }

        public IEmailBuilder From(string emailAddress, string name = "")
        {
            Recipient recipient = string.IsNullOrWhiteSpace(name) ? new Recipient(emailAddress) : new Recipient(emailAddress, name);
            emailMessage.FromAddress = recipient;
            return this;
        }

        public IEmailBuilder Subject(string subject)
        {
            emailMessage.Subject = subject;
            return this;
        }

        public IEmailBuilder SetPriority(EmailPriotiry emailPriotiry)
        {
            emailMessage.Priority = emailPriotiry;
            return this;
        }

        public IEmailBuilder Body(string body, BodyType bodyType = BodyType.Html)
        {
            emailMessage.Body = body;
            return this;
        }

        public IEmailBuilder Body<TModel>(string path, TModel model, Assembly assembly, BodyType bodyType = BodyType.Html)
        {
            string template = GetResourceAsString(assembly, path);
            return Body(template, model, bodyType);
        }

        public IEmailBuilder Body<TModel>(string filename, TModel model, CultureInfo culture, BodyType bodyType = BodyType.Html)
        {
            filename = GetCultureFileName(filename, culture);

            string template;

            using (var reader = new StreamReader(File.OpenRead(filename)))
            {
                template = reader.ReadToEnd();
            }

            return Body(template, model, bodyType);
        }

        public IEmailBuilder Body<TModel>(string template, TModel model, BodyType bodyType = BodyType.Html)
        {
            switch (bodyType)
            {
                case BodyType.Html:
                    emailMessage.Body = _contentBuilder.BuildHtml(template, model);
                    break;
                case BodyType.PlainText:
                    emailMessage.Body = _contentBuilder.Build(template, model);
                    break;
                default:
                    throw new NotSupportedException();
            }
            emailMessage.BodyType = bodyType;
            return this;
        }

        public IEmailBuilder Attach(FileAttachment fileAttachment)
        {
            if (!emailMessage.Attachments.Contains(fileAttachment))
            {
                emailMessage.Attachments.Add(fileAttachment);
            }
            return this;
        }

        public IEmailBuilder Attach(IList<FileAttachment> fileAttachments)
        {
            foreach (FileAttachment fileAttachment in fileAttachments)
            {
                Attach(fileAttachment);
            }
            return this;
        }

        public EmailMessage Build()
        {
            return emailMessage;
        }

        #endregion

        private static string GetResourceAsString(Assembly assembly, string path)
        {
            string result;

            using (var stream = assembly.GetManifestResourceStream(path))
            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        private static string GetCultureFileName(string fileName, CultureInfo culture)
        {
            var extension = Path.GetExtension(fileName);
            var cultureExtension = string.Format("{0}{1}", culture.Name, extension);

            var cultureFile = Path.ChangeExtension(fileName, cultureExtension);
            if (File.Exists(cultureFile))
                return cultureFile;
            return fileName;
        }
    }

}
