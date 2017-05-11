using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using DotNetCraft.SimpleEmailSender.Entities;
using DotNetCraft.SimpleEmailSender.Entities.Enums;

namespace DotNetCraft.SimpleEmailSender.Interfaces
{
    public interface IEmailBuilder
    {
        IEmailBuilder To(string emailAddress, string name = "");

        IEmailBuilder To(IList<Recipient> recipientAddresses);

        IEmailBuilder CC(string emailAddress, string name = "");

        IEmailBuilder CC(IList<Recipient> recipientAddresses);

        IEmailBuilder BCC(string emailAddress, string name = "");

        IEmailBuilder BCC(IList<Recipient> recipientAddresses);

        IEmailBuilder ReplyTo(string emailAddress, string name = "");

        IEmailBuilder ReplyTo(IList<Recipient> recipientAddresses);

        IEmailBuilder From(string emailAddress, string name = "");

        IEmailBuilder Subject(string subject);

        IEmailBuilder SetPriority(EmailPriotiry emailPriotiry);

        IEmailBuilder Body(string body, BodyType bodyType = BodyType.Html);
        IEmailBuilder Body<TModel>(string path, TModel model, Assembly assembly, BodyType bodyType = BodyType.Html);
        IEmailBuilder Body<TModel>(string filename, TModel model, CultureInfo culture, BodyType bodyType = BodyType.Html);
        IEmailBuilder Body<TModel>(string template, TModel model, BodyType bodyType = BodyType.Html);

        IEmailBuilder Attach(FileAttachment fileAttachment);

        IEmailBuilder Attach(IList<FileAttachment> fileAttachments);

        EmailMessage Build();
    }

}
