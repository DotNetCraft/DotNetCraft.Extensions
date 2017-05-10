using System;
using DotNetCraft.FunctionManager.Implementation.UserFunctionDetails;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Interfaces.Builders
{
    /// <summary>
    /// Interface shows that object ca build a simple exception manager.
    /// </summary>
    /// <typeparam name="TUserContext"></typeparam>
    public interface ISimpleExceptionManagerBuilder<TUserContext> : IFunctionWrapperBuilder<TUserContext, FunctionWrapper<TUserContext>> 
        where TUserContext : IUserContext
    {
        /// <summary>
        /// Register exception.
        /// </summary>
        /// <typeparam name="TException">Type of exception.</typeparam>
        /// <returns>The simple exception manager.</returns>
        ISimpleExceptionManagerBuilder<TUserContext> RegisterException<TException>()
            where TException : Exception;

        /// <summary>
        /// Register exception.
        /// </summary>
        /// <typeparam name="TException">Type of exception.</typeparam>
        /// <param name="expression">Expression.</param>
        /// <returns>The simple exception manager.</returns>
        ISimpleExceptionManagerBuilder<TUserContext> RegisterException<TException>(Func<TException, bool> expression)
            where TException : Exception;
    }
}