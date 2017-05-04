using System;
using DotNetCraft.FunctionManager.Implementation.Strategies.AttemptParameters;
using DotNetCraft.FunctionManager.Interfaces;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies.AttemptParameters;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Implementation.Strategies
{
    public abstract class BaseStrategy<TUserContext, TAttemptInformation> : IExceptionStrategy<TUserContext>
        where TUserContext: IUserContext
        where TAttemptInformation: IAttemptInformation
    {
        private int _currentAttempt;
        private readonly Action<Exception, TUserContext, TAttemptInformation> _onRetry;

        protected BaseStrategy()
        {
        }

        protected BaseStrategy(Action<Exception, TUserContext, TAttemptInformation> onRetry)
        {
            if (onRetry == null)
                throw new ArgumentNullException(nameof(onRetry));

            _onRetry = onRetry;
        }

        #region Implementation of IExceptionStrategy

        protected abstract bool OnContinueRunning(int currentAttempt, Exception exception, TUserContext userContext);

        public bool ContinueRunning(Exception exception, TUserContext userContext)
        {
            if (_onRetry != null)
            {
                try
                {
                    IAttemptInformation attemptInformation = CreateAttemptInformation(_currentAttempt);
                    _onRetry(exception, userContext, (TAttemptInformation) attemptInformation);
                }
                catch (Exception ex)
                {
                    //TODO: Log exception
                }
            }

            bool result = OnContinueRunning(_currentAttempt, exception, userContext);
            _currentAttempt++;
            return result;
        }

        protected virtual IAttemptInformation CreateAttemptInformation(int currentAttempt)
        {
            return new AttemptInformation(currentAttempt);
        }

        #endregion
    }
}
