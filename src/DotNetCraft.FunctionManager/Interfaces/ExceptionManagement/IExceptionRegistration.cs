using System;

namespace DotNetCraft.FunctionManager.Interfaces.ExceptionManagement
{
    /// <summary>
    /// Interface shows that  object can register exceptions.
    /// </summary>
    public interface IExceptionRegistration
    {
        /// <summary>
        /// Register exception by it's type.
        /// </summary>
        /// <typeparam name="TException">Type of exception</typeparam>
        void RegisterException<TException>()
           where TException : Exception;

        /// <summary>
        /// Register exception by it's type and special expression.
        /// </summary>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="expression">Special expression.</param>
        void RegisterException<TException>(Func<TException, bool> expression)
            where TException : Exception;
    }
}
