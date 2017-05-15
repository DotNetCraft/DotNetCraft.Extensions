using System;
using System.IO;
using DotNetCraft.Common.Utils.Disposal;

namespace DotNetCraft.SimpleEmailSender.Entities
{
    /// <summary>
    /// File attachment
    /// </summary>
    public class FileAttachment : DisposableObject
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

        #region Overrides of DisposableObject

        public override void Dispose(bool disposing)
        {
            if (disposing && Data != null)
            {
                Data.Dispose();
                Data = null;
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Filename;
        }

        #endregion
    }
}
