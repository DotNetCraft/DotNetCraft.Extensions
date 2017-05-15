using DotNetCraft.Common.Core.Utils;
using DotNetCraft.Common.Utils;
using DotNetCraft.SimpleEmailSender.Builders;
using DotNetCraft.SimpleEmailSender.ContentBuilders;
using DotNetCraft.SimpleEmailSender.Interfaces;

namespace DotNetCraft.SimpleEmailSender
{
    public static class EmailManager
    {
        public static IEmailBuilder Builder()
        {
            IPropertyManager propertyManager = PropertyManager.Manager;
            IContentBuilder contentBuilder = new SimpleContentBuilder(propertyManager);
            return new EmailBuilder(contentBuilder);
        }

        public static IEmailBuilder Builder(IContentBuilder contentBuilder)
        {
            return new EmailBuilder(contentBuilder);
        }
    }
}
