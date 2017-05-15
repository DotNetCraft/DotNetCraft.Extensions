using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using DotNetCraft.Common.Core.Utils.Logging;
using DotNetCraft.Common.Utils.Extensions;
using DotNetCraft.Common.Utils.Logging;
using DotNetCraft.SimpleEmailSender.Entities;
using DotNetCraft.SimpleEmailSender.Entities.Enums;
using DotNetCraft.SimpleEmailSender.Interfaces;

namespace DotNetCraft.SimpleEmailSender.Builders
{
    public class EmailBuilder : IEmailBuilder
    {
        #region Fields...
        /// <summary>
        /// The ICommonLogger instance.
        /// </summary>
        private static readonly ICommonLogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Content builder.
        /// </summary>
        private readonly IContentBuilder _contentBuilder;

        /// <summary>
        /// Email's message.
        /// </summary>
        private readonly EmailMessage emailMessage;
        #endregion

        #region Constructors...

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="contentBuilder">Content builder.</param>
        /// <exception cref="ArgumentNullException"><paramref name="contentBuilder"/> is <see langword="null"/></exception>
        public EmailBuilder(IContentBuilder contentBuilder)
        {
            if (contentBuilder == null)
                throw new ArgumentNullException("contentBuilder");
            _contentBuilder = contentBuilder;
            emailMessage = new EmailMessage();
        }

        #endregion

        #region Implementation of IEmailBuilder

        public IEmailBuilder To(string emailAddress, string name = "")
        {
            logger.Debug("Adding a new recipient: {0] ({1})...", emailAddress, name);
            Recipient recipient = string.IsNullOrWhiteSpace(name) ? new Recipient(emailAddress) : new Recipient(emailAddress, name);
            emailMessage.ToAddresses.Add(recipient);
            logger.Debug("The recipient has been added.");
            return this;
        }

        public IEmailBuilder To(IList<Recipient> recipientAddresses)
        {
            logger.Debug("Adding recipients (Count= {0])...", recipientAddresses.Count);
            emailMessage.ToAddresses.AddRange(recipientAddresses);
            logger.Debug("The recipients have been added.");
            return this;
        }

        public IEmailBuilder CC(string emailAddress, string name = "")
        {
            logger.Debug("Adding a new recipient into the CC list: {0] ({1})...", emailAddress, name);
            Recipient recipient = string.IsNullOrWhiteSpace(name) ? new Recipient(emailAddress) : new Recipient(emailAddress, name);
            emailMessage.CcAddresses.Add(recipient);
            logger.Debug("The recipient has been added.");
            return this;
        }

        public IEmailBuilder CC(IList<Recipient> recipientAddresses)
        {
            logger.Debug("Adding recipients (Count= {0]) into the CC list...", recipientAddresses.Count);
            emailMessage.CcAddresses.AddRange(recipientAddresses);
            logger.Debug("The recipients have been added.");
            return this;
        }

        public IEmailBuilder BCC(string emailAddress, string name = "")
        {
            logger.Debug("Adding a new recipient into the BCC list: {0] ({1})...", emailAddress, name);
            Recipient recipient = string.IsNullOrWhiteSpace(name) ? new Recipient(emailAddress) : new Recipient(emailAddress, name);
            emailMessage.BccAddresses.Add(recipient);
            logger.Debug("The recipient has been added.");
            return this;
        }

        public IEmailBuilder BCC(IList<Recipient> recipientAddresses)
        {
            logger.Debug("Adding recipients (Count= {0]) into the BCC list...", recipientAddresses.Count);
            emailMessage.BccAddresses.AddRange(recipientAddresses);
            logger.Debug("The recipients have been added.");
            return this;
        }

        public IEmailBuilder ReplyTo(string emailAddress, string name = "")
        {
            logger.Debug("Adding a new recipient into the ReplyTo list: {0] ({1})...", emailAddress, name);
            Recipient recipient = string.IsNullOrWhiteSpace(name) ? new Recipient(emailAddress) : new Recipient(emailAddress, name);
            emailMessage.ReplyToAddresses.Add(recipient);
            logger.Debug("The recipient has been added.");
            return this;
        }

        public IEmailBuilder ReplyTo(IList<Recipient> recipientAddresses)
        {
            logger.Debug("Adding recipients (Count= {0]) into the ReplyTo list...", recipientAddresses.Count);
            emailMessage.ReplyToAddresses.AddRange(recipientAddresses);
            logger.Debug("The recipients have been added.");
            return this;
        }

        public IEmailBuilder From(string emailAddress, string name = "")
        {
            logger.Debug("Adding a new recipient into the From field: {0] ({1})...", emailAddress, name);
            if (emailMessage.FromAddress != null)
                logger.Warning("Another recipient is existed ({0}) . It will be overwritten.", emailMessage.FromAddress);
            Recipient recipient = string.IsNullOrWhiteSpace(name) ? new Recipient(emailAddress) : new Recipient(emailAddress, name);
            emailMessage.FromAddress = recipient;
            logger.Debug("The recipient has been added.");
            return this;
        }

        public IEmailBuilder Subject(string subject)
        {
            logger.Debug("Changing subject to: {0}...", subject);
            emailMessage.Subject = subject;
            logger.Debug("The subject has been changed");
            return this;
        }

        public IEmailBuilder SetPriority(EmailPriotiry emailPriotiry)
        {
            logger.Debug("Changing priority to: {0}...", emailPriotiry);
            emailMessage.Priority = emailPriotiry; 
            logger.Debug("The priority has been changed");            
            return this;
        }

        public IEmailBuilder Body(string body, BodyType bodyType = BodyType.Html)
        {
            string item = body == null ? "" : body.GetPart(40);
            logger.Debug("Changing body ({1}) to: {0}...", item, bodyType);
            emailMessage.Body = body ?? "";
            logger.Debug("The body has been changed");            
            return this;
        }

        public IEmailBuilder Body<TModel>(string path, TModel model, Assembly assembly, BodyType bodyType = BodyType.Html)
        {
            logger.Debug("Changing body using resource: {0}/{1}...", path, assembly.FullName);
            string template = assembly.GetResourceAsString(path);
            IEmailBuilder result = Body(template, model, bodyType);
            logger.Debug("The body has bee changed");
            return result;
        }

        public IEmailBuilder Body<TModel>(string filename, TModel model, CultureInfo culture, BodyType bodyType = BodyType.Html)
        {
            logger.Debug("Changing body using file and culture: {0}...", filename);
            filename = GetCultureFileName(filename, culture);

            string template;

            using (var reader = new StreamReader(File.OpenRead(filename)))
            {
                template = reader.ReadToEnd();
            }

            IEmailBuilder result = Body(template, model, bodyType);
            logger.Debug("The body has bee changed");
            return result;
        }

        public IEmailBuilder Body<TModel>(string template, TModel model, BodyType bodyType = BodyType.Html)
        {
            logger.Debug("Changing body using template: {0}...", template == null ? "" : template.GetPart(50));
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
            logger.Debug("The body has bee changed");
            return this;
        }

        public IEmailBuilder Attach(FileAttachment fileAttachment)
        {
            logger.Debug("Attaching new file: {0}...", fileAttachment);
            if (emailMessage.Attachments.Contains(fileAttachment) == true)
            {
                logger.Error("The file has been already added.");
            }
            else
            {
                emailMessage.Attachments.Add(fileAttachment);
                logger.Debug("The file has been added");
            }
            return this;
        }

        public IEmailBuilder Attach(IList<FileAttachment> fileAttachments)
        {
            logger.Debug("Adding list of files: {0}...", fileAttachments.Count);
            foreach (FileAttachment fileAttachment in fileAttachments)
            {
                Attach(fileAttachment);
            }
            logger.Debug("List of files has been added.");
            return this;
        }

        public EmailMessage Build()
        {
            return emailMessage;
        }

        #endregion        

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
