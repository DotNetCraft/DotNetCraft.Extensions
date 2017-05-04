namespace DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Managers
{
    /// <summary>
    /// Interface shows that object is a smart exception manager that has many strategies how to handle an exception.
    /// </summary>
    public interface ISmartExceptionStrategiesManager<TUserContext> : IExceptionDecisionMaker<TUserContext>
    {
        /// <summary>
        /// Register simple strategy manager.
        /// </summary>
        /// <param name="exceptionManager">The simple strategy manager.</param>
        void RegisterStrategyManager(ISimpleExceptionManager<TUserContext> exceptionManager);
    }
}
