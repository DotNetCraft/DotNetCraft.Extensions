using DotNetCraft.Common.Core.Utils.ReflectionExtensions;
using DotNetCraft.Common.Utils.ReflectionExtensions;
using DotNetCraft.SimpleEmailSender.Builders;
using DotNetCraft.SimpleEmailSender.ContentBuilders;
using DotNetCraft.SimpleEmailSender.Interfaces;

namespace DotNetCraft.SimpleEmailSender
{
    public static class EmailManager
    {
        public static IEmailBuilder Builder()
        {
            IReflectionManager reflectionManager = ReflectionManager.Manager;
            IContentBuilder contentBuilder = new SimpleContentBuilder(reflectionManager);
            return new EmailBuilder(contentBuilder);
        }

        public static IEmailBuilder Builder(IContentBuilder contentBuilder)
        {
            return new EmailBuilder(contentBuilder);
        }
    }
}
