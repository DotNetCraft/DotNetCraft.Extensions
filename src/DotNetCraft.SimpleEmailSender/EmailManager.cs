using DotNetCraft.SimpleEmailSender.Builders;
using DotNetCraft.SimpleEmailSender.ContentBuilders;
using DotNetCraft.SimpleEmailSender.Interfaces;

namespace DotNetCraft.SimpleEmailSender
{
    public static class EmailManager
    {
        public static IEmailBuilder Builder()
        {
            IContentBuilder contentBuilder = new SimpleContentBuilder();
            return new EmailBuilder(contentBuilder);
        }

        public static IEmailBuilder Builder(IContentBuilder contentBuilder)
        {
            return new EmailBuilder(contentBuilder);
        }
    }
}
