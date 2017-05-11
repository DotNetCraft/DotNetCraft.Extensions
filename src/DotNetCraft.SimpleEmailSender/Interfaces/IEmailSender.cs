using System.Threading;
#if (!NET40)
using System.Threading.Tasks;
#endif
using DotNetCraft.SimpleEmailSender.Entities;

namespace DotNetCraft.SimpleEmailSender.Interfaces
{
    public interface IEmailSender
    {
        SendResponse Send(EmailMessage emailMessage, CancellationToken? token = null);
#if (!NET40)
        Task<SendResponse> SendAsync(EmailMessage emailMessage, CancellationToken? token = null);
#endif
    }
}
