namespace DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies
{
    /// <summary>
    /// Interface shows that object is an exception's strategy.
    /// </summary>
    public interface IExceptionStrategy<TUserContext> : IExceptionDecisionMaker<TUserContext>
    {        
    }
}
