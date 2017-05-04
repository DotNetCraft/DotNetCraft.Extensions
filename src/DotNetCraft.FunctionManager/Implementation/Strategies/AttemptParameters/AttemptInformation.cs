using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies.AttemptParameters;

namespace DotNetCraft.FunctionManager.Implementation.Strategies.AttemptParameters
{
    public class AttemptInformation: IAttemptInformation
    {
        #region Implementation of IAttemptInformation

        public int AttemptNumber { get; private set; }

        #endregion

        public AttemptInformation(int attemptNumber)
        {
            AttemptNumber = attemptNumber;
        }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("AttemptNumber: {0}", AttemptNumber);
        }

        #endregion
    }
}
