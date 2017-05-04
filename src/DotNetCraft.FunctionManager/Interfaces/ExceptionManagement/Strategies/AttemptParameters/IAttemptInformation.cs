namespace DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies.AttemptParameters
{
    /// <summary>
    /// Interface shows that object has information about re-executing attempt
    /// </summary>
    public interface IAttemptInformation
    {
        /// <summary>
        /// Number of attempt.
        /// </summary>
        int AttemptNumber { get; }
    }
}
