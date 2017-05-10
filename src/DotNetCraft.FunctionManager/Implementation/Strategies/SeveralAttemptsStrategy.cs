using System;
using DotNetCraft.FunctionManager.Implementation.Strategies.AttemptParameters;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies.AttemptParameters;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Implementation.Strategies
{
    internal delegate bool ExceptionPredicate(Exception ex);

    public class SeveralAttemptsStrategy<TUserContext> : BaseStrategy<TUserContext, AttemptInformation>
        where TUserContext : IUserContext
    {
        private readonly int _maxAttempts;
       
        public SeveralAttemptsStrategy(int maxAttempts)
        {
            _maxAttempts = maxAttempts;
        }

        public SeveralAttemptsStrategy(int maxAttempts, Action<Exception, TUserContext, AttemptInformation> onRetry) : base(onRetry)
        {
            _maxAttempts = maxAttempts;
        }

        #region Overrides of BaseStrategy

        protected override bool OnContinueRunning(int currentAttempt, Exception exception, TUserContext userContext)
        {
            return currentAttempt <= _maxAttempts;
        }

        #region Overrides of BaseStrategy<AttemptInformation>

        protected override IAttemptInformation CreateAttemptInformation(int currentAttempt)
        {
            IAttemptInformation result = base.CreateAttemptInformation(currentAttempt);
            return result;
        }

        #endregion

        #endregion
    }
}
