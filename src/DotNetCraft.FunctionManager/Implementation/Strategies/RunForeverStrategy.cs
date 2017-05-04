using System;
using DotNetCraft.FunctionManager.Implementation.Strategies.AttemptParameters;
using DotNetCraft.FunctionManager.Interfaces;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Implementation.Strategies
{
    public class RunForeverStrategy<TUserContext> : BaseStrategy<TUserContext, AttemptInformation> where TUserContext : IUserContext
    {
        public RunForeverStrategy() : base()
        {
        }

        public RunForeverStrategy(Action<Exception, TUserContext, AttemptInformation> onRetry): base(onRetry)
        {            
        }

        #region Overrides of BaseStrategy

        protected override bool OnContinueRunning(int currentAttempt, Exception exception, TUserContext userContext)
        {
            return true;
        }

        #endregion
    }
}
