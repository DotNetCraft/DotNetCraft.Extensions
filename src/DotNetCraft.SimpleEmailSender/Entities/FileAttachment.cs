using System;
using System.IO;

namespace DotNetCraft.SimpleEmailSender.Entities
{
    /// <summary>
    /// File attachment
    /// </summary>
    public class FileAttachment : IDisposable
    {
        /// <summary>
        /// Name of the file
        /// </summary>
        public string Filename { get; private set; }

        /// <summary>
        /// File's data as a stream.
        /// </summary>
        public Stream Data { get; private set; }

        /// <summary>
        /// Content's type.
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileName">File's name</param>
        /// <param name="contentType">Content's type</param>
        public FileAttachment(string fileName, string contentType = "unknown")
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");
            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentNullException("contentType");

            Data = File.OpenRead(fileName);
            ContentType = contentType;
        }

        #region Implementation of IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            //TODO: Disposable pattern
            if (Data != null)
            {
                Data.Dispose();
                Data = null;
            }
        }

        #endregion
    }
}
