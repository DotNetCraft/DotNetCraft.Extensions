namespace DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Managers
{
    /// <summary>
    /// Interface shows that object is a simple exception manager that has only one strategy how to handle an exception.
    /// </summary>
    public interface ISimpleExceptionManager<TUserContext> : IExceptionDecisionMaker<TUserContext>, IStrategyRegistration<TUserContext>, IExceptionRegistration
    {
    }
}
