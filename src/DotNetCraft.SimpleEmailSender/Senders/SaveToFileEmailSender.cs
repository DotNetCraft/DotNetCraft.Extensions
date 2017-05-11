using System;
using System.IO;
using System.Threading;
#if (!NET40)
using System.Threading.Tasks;
#endif
using DotNetCraft.SimpleEmailSender.Entities;
using DotNetCraft.SimpleEmailSender.Interfaces;

namespace DotNetCraft.SimpleEmailSender.Senders
{
    public class SaveToFileEmailSender : IEmailSender
    {
        private readonly string _directory;

        public SaveToFileEmailSender(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
                throw new ArgumentNullException("directory");

            _directory = directory;
        }

#if (NET40)
        public SendResponse Send(EmailMessage emailMessage, CancellationToken? token = null)
        {
            string filename = string.Format("{0}\\{yyyy-MM-dd_hh-mm-ss}", _directory.TrimEnd('\\'), DateTime.Now);

            using (FileStream fileStream = File.OpenWrite(filename))
            {
                using (var sw = new StreamWriter(fileStream))
                {
                    string str = emailMessage.ShowDetails();
                    sw.WriteLine(str);
                    sw.Write(emailMessage.Body);
                }
            }

            return new SendResponse();
        }
#else
        public SendResponse Send(EmailMessage emailMessage, CancellationToken? token = null)
        {
            return SendAsync(emailMessage, token).GetAwaiter().GetResult();
        }

        public async Task<SendResponse> SendAsync(EmailMessage emailMessage, CancellationToken? token = null)
        {
            SendResponse result = await SaveEmailToDisk(emailMessage);
            return result;
        }

        private async Task<SendResponse> SaveEmailToDisk(EmailMessage emailMessage)
        {
            string filename = string.Format("{0}\\{yyyy-MM-dd_hh-mm-ss}", _directory.TrimEnd('\\'), DateTime.Now);

            using (FileStream fileStream = File.OpenWrite(filename))
            {
                using (var sw = new StreamWriter(fileStream))
                {
                    string str = emailMessage.ShowDetails();
                    sw.WriteLine(str);
                    await sw.WriteAsync(emailMessage.Body);
                }
            }

            return new SendResponse();
        }
#endif
    }
}
