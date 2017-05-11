namespace DotNetCraft.SimpleEmailSender.Interfaces
{
    /// <summary>
    /// Interface shows that object is a content builder.
    /// </summary>
    public interface IContentBuilder
    {
        /// <summary>
        /// Create a content using template and model.
        /// </summary>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <param name="template">The template.</param>
        /// <param name="model">The model.</param>
        /// <returns>The content.</returns>
        string Build<TModel>(string template, TModel model);

        /// <summary>
        /// Create a HTML content using template and model.
        /// </summary>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <param name="template">The HTML template.</param>
        /// <param name="model">The model.</param>
        /// <returns>The HTML content.</returns>
        string BuildHtml<TModel>(string template, TModel model);
    }
}
