namespace DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails
{
    /// <summary>
    /// Interface shows that object has information about user's function.
    /// </summary>
    /// <typeparam name="TUserContext">Type of user's context.</typeparam>
    public interface IUserAction<in TUserContext>
        where TUserContext : IUserContext
    {
        /// <summary>
        /// Execute user's function
        /// </summary>
        /// <param name="userContext">The user's context.</param>
        void Execute(TUserContext userContext);
    }
}
