using System;

namespace DotNetCraft.SimpleEmailSender.Entities
{
    /// <summary>
    /// Recipient
    /// </summary>
    public class Recipient
    {
        /// <summary>
        /// Recipient's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Recipients' email address
        /// </summary>
        public string EmailAddress { get; set; }

        #region Constructors...

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="emailAddress">Recipients' email address</param>
        /// <exception cref="ArgumentNullException"><paramref name="emailAddress"/> is <see langword="null"/></exception>
        public Recipient(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentNullException("emailAddress");

            EmailAddress = emailAddress;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="emailAddress">Recipients' email address</param>
        /// <param name="name">Recipient's name</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/></exception>
        public Recipient(string emailAddress, string name) : this(emailAddress)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            Name = name;
        }

        #endregion
    }
}
