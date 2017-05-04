using System;
using DotNetCraft.FunctionManager.Exceptions;
using DotNetCraft.FunctionManager.Implementation.UserFunctionDetails.FunctionParameters;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager
{
    public class FunctionWrapper<TUserContext> where TUserContext : IUserContext
    {
        private readonly IExceptionDecisionMaker<TUserContext> _exceptionStrategiesManager;

        public FunctionWrapper(IExceptionDecisionMaker<TUserContext> exceptionStrategiesManager)
        {
            if (exceptionStrategiesManager == null)
                throw new ArgumentNullException(nameof(exceptionStrategiesManager));

            _exceptionStrategiesManager = exceptionStrategiesManager;
        }

        public void Execute(Action<TUserContext> action, TUserContext userContext) 
        {
            UserAction<TUserContext> userAction = new UserAction<TUserContext>(action);
            Execute(userAction, userContext);
        }

        private void Execute(IUserAction<TUserContext> userAction, TUserContext userContext)
        {
            while (true)
            {
                try
                {
                    userAction.Execute(userContext);
                    break;
                }
                catch (Exception ex)
                {
                    bool continueRunning = _exceptionStrategiesManager.ContinueRunning(ex, userContext);
                    if (continueRunning)
                        continue;

                    throw new FunctionManagerException("All attempts have been executed but there is still an exception", ex);
                }
            }
        }
    }
}
