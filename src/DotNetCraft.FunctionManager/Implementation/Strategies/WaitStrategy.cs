using System;
using System.Collections.Generic;
using System.Threading;
using DotNetCraft.FunctionManager.Implementation.Strategies.AttemptParameters;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies.AttemptParameters;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Implementation.Strategies
{
    public class WaitStrategy<TUserContext> : BaseStrategy<TUserContext, TimeSpanAttemptInformation>
        where TUserContext : IUserContext
    {
        private readonly List<TimeSpan> _durations;

        public WaitStrategy(TimeSpan[] durations)
        {
            if (durations == null)
                throw new ArgumentNullException(nameof(durations));

            _durations = new List<TimeSpan>(durations);
        }

        public WaitStrategy(TimeSpan[] durations, Action<Exception, TUserContext, TimeSpanAttemptInformation> onRetry): base(onRetry)
        {
            if (durations == null)
                throw new ArgumentNullException(nameof(durations));

            _durations = new List<TimeSpan>(durations);
        }

        #region Overrides of BaseStrategy

        protected override bool OnContinueRunning(int currentAttempt, Exception exception, TUserContext userContext)
        {
            if (currentAttempt >= _durations.Count)
                return false;

            TimeSpan sleepTime = _durations[currentAttempt];
            Thread.Sleep(sleepTime);
            return true;
        }

        #region Overrides of BaseStrategy<TimeSpanAttemptInformation>

        protected override IAttemptInformation CreateAttemptInformation(int currentAttempt)
        {
            TimeSpan currentTimeSpan = currentAttempt < _durations.Count ? _durations[currentAttempt] : TimeSpan.Zero;
            IAttemptInformation result = new TimeSpanAttemptInformation(currentAttempt, currentTimeSpan);
            return result;
        }

        #endregion

        #endregion
    }
}
