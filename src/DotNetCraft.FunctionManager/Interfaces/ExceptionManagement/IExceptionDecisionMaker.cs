using System;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Interfaces.ExceptionManagement
{
    /// <summary>
    /// Interface shows that object can make a decision what to do with the exception that will be provided into it.
    /// </summary>
    public interface IExceptionDecisionMaker<TUserContext>
    {
        /// <summary>
        /// Determine whether or not keep trying to execute user function
        /// </summary>
        /// <param name="exception">The exception that has been occured during user's function execution.</param>
        /// <param name="userContext">The user context.</param>
        /// <returns>True if execution should be continued. Otherwise, false.</returns>
        bool ContinueRunning(Exception exception, TUserContext userContext);
    }
}
