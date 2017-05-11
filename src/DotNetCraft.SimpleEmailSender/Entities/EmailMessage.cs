using System;
using System.Collections.Generic;
using System.Text;
using DotNetCraft.SimpleEmailSender.Entities.Enums;

namespace DotNetCraft.SimpleEmailSender.Entities
{
    /// <summary>
    /// Email message
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// List of recipients who will receive the email
        /// </summary>
        public List<Recipient> ToAddresses { get; set; }

        /// <summary>
        /// List of recipients who will be in CC in current email.
        /// </summary>
        public List<Recipient> CcAddresses { get; set; }

        /// <summary>
        /// List of recipients who will be in BCC in current email.
        /// </summary>
        public List<Recipient> BccAddresses { get; set; }

        /// <summary>
        /// List of recipients who will be in the Reply field
        /// </summary>
        public List<Recipient> ReplyToAddresses { get; set; }

        /// <summary>
        /// File attachments.
        /// </summary>
        public List<FileAttachment> Attachments { get; set; }

        /// <summary>
        /// Address from which this email will be sent.
        /// </summary>
        public Recipient FromAddress { get; set; }

        /// <summary>
        /// The subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The email body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Email's priority
        /// </summary>
        public EmailPriotiry Priority { get; set; }

        /// <summary>
        /// Flag shows what body type this email has.
        /// </summary>
        public BodyType BodyType { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EmailMessage()
        {
            ToAddresses = new List<Recipient>();
            CcAddresses = new List<Recipient>();
            BccAddresses = new List<Recipient>();
            ReplyToAddresses = new List<Recipient>();
            Attachments = new List<FileAttachment>();
        }

        /// <summary>
        /// Print email information into the string.
        /// </summary>
        /// <param name="maxBodySize">Amount of characters that will be displayed in the body section.</param>
        /// <returns>The email information</returns>
        public string ShowDetails(int maxBodySize = Int32.MaxValue)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("From: {0} <{1}>", FromAddress.Name, FromAddress.EmailAddress));
            stringBuilder.AppendLine(string.Format("To: {0}", CombineRecipients(ToAddresses)));
            stringBuilder.AppendLine(string.Format("Cc: {0}", CombineRecipients(CcAddresses)));
            stringBuilder.AppendLine(string.Format("Bcc: {0}", CombineRecipients(BccAddresses)));
            stringBuilder.AppendLine(string.Format("ReplyTo: {0}", CombineRecipients(ReplyToAddresses)));
            stringBuilder.AppendLine(string.Format("Subject: {0}", Subject));
            stringBuilder.AppendLine("--------------------------------------");

            if (string.IsNullOrWhiteSpace(Body))
            {
                stringBuilder.AppendLine("");
            }
            else
            {
                if (Body.Length < maxBodySize)
                {
                    stringBuilder.AppendLine(Body);
                }
                else
                {
                    string str = Body.Substring(0, maxBodySize);
                    stringBuilder.AppendLine(string.Format("{0}...", Body));
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Combine recipients form the list into the string.
        /// </summary>
        /// <param name="recipients">List of recipients</param>
        /// <returns>Recipients in the string.</returns>
        private string CombineRecipients(List<Recipient> recipients)
        {
            string result = string.Empty;
            int count = recipients.Count;
            for (int index = 0; index < count; index++)
            {
                Recipient recipient = recipients[index];
                result += string.Format("{0} <{1}>", recipient.Name, recipient.EmailAddress);
                if (index - 1 < count)
                    result += ", ";
            }
            return result;
        }
    }
}
