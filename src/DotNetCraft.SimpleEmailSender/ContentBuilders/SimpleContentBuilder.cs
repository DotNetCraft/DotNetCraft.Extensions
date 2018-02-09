using System;
using System.Reflection;
using DotNetCraft.Common.Core.Utils.Logging;
using DotNetCraft.Common.Core.Utils.ReflectionExtensions;
using DotNetCraft.Common.Utils.Logging;
using DotNetCraft.SimpleEmailSender.Interfaces;

namespace DotNetCraft.SimpleEmailSender.ContentBuilders
{
    /// <summary>
    /// Simple content builder.
    /// </summary>
    /// <remarks>
    /// Searching in the template special placeholders (#{propertyName}) and replace them by properties' values.
    /// </remarks>
    public class SimpleContentBuilder : IContentBuilder
    {
        private static readonly ICommonLogger logger = LogManager.GetCurrentClassLogger();

        private readonly IReflectionManager reflectionManager;

        public const string placeHolder = "#{{{0}}}";

        public SimpleContentBuilder(IReflectionManager reflectionManager)
        {
            if (reflectionManager == null)
                throw new ArgumentNullException("reflectionManager");

            this.reflectionManager = reflectionManager;
        }

        #region Implementation of IContentBuilder

        /// <summary>
        /// Create a content using template and model.
        /// </summary>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <param name="template">The template.</param>
        /// <param name="model">The model.</param>
        /// <returns>The content.</returns>
        public string Build<TModel>(string template, TModel model)
        {
            Type type = typeof(TModel);
            //TODO: Collect properties from the inner objects.
            //TODO: Add attributes;
            //TODO: Do it in the separate class a-ka PropertyManager.
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                string item = string.Format(placeHolder, propertyInfo.Name);
                object propertyValue = propertyInfo.GetValue(model, null);
                if (propertyValue == null)
                    template = template.Replace(item, string.Empty);
                else
                    template = template.Replace(item, propertyValue.ToString());
            }

            return template;
        }

        /// <summary>
        /// Create a HTML content using template and model.
        /// </summary>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <param name="template">The HTML template.</param>
        /// <param name="model">The model.</param>
        /// <returns>The HTML content.</returns>
        public string BuildHtml<TModel>(string template, TModel model)
        {
            return Build(template, model);
        }

        #endregion
    }
}
