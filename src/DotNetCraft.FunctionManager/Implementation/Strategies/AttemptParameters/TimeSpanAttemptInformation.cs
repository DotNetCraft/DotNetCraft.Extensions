using System;

namespace DotNetCraft.FunctionManager.Implementation.Strategies.AttemptParameters
{
    public class TimeSpanAttemptInformation: AttemptInformation
    {
        public TimeSpan CurrentTimeSpan { get; private set; }
        public TimeSpanAttemptInformation(int attemptNumber, TimeSpan currentTimeSpan) : base(attemptNumber)
        {
            CurrentTimeSpan = currentTimeSpan;
        }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("CurrentTimeSpan: {0}; {1}", CurrentTimeSpan, base.ToString());
        }

        #endregion
    }
}
