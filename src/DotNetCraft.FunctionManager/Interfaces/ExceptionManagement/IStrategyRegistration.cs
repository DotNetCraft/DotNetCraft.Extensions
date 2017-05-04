using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies;

namespace DotNetCraft.FunctionManager.Interfaces.ExceptionManagement
{
    /// <summary>
    /// Interface shows that object can register an exception strategy.
    /// </summary>
    public interface IStrategyRegistration<TUserContext>
    {
        /// <summary>
        /// Register exception strategy.
        /// </summary>
        /// <param name="exceptionStrategy">The exception strategy.</param>
        void RegisterStrategy(IExceptionStrategy<TUserContext> exceptionStrategy);
    }
}
