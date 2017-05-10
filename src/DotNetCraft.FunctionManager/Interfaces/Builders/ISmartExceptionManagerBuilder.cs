using System;
using DotNetCraft.FunctionManager.Implementation.UserFunctionDetails;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Interfaces.Builders
{
    /// <summary>
    /// Interface shows that object can build smart exception manager.
    /// </summary>
    /// <typeparam name="TUserContext"></typeparam>
    public interface ISmartExceptionManagerBuilder<TUserContext> : IFunctionWrapperBuilder<TUserContext, ISmartExceptionManagerBuilder<TUserContext>>
        where TUserContext : IUserContext
    {
        /// <summary>
        /// Register an exception.
        /// </summary>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <returns>The smart exception manager.</returns>
        ISmartExceptionManagerBuilder<TUserContext> OnException<TException>()
           where TException : Exception;

        /// <summary>
        /// Register an exception.
        /// </summary>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The smart exception manager.</returns>
        ISmartExceptionManagerBuilder<TUserContext> OnException<TException>(Func<TException, bool> expression)
            where TException : Exception;

        /// <summary>
        /// Build a new instance of the function wrapper.
        /// </summary>
        /// <returns></returns>
        FunctionWrapper<TUserContext> Build();
    }
}
