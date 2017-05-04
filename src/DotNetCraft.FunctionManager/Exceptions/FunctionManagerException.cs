using System;

namespace DotNetCraft.FunctionManager.Exceptions
{
    /// <summary>
    /// Exception is raised if something goes wrong during user's function executing
    /// </summary>
    [Serializable]
    public class FunctionManagerException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception</param>
        public FunctionManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
