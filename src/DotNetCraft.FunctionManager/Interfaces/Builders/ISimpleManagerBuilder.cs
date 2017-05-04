using System;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Interfaces.Builders
{
    public interface ISimpleManagerBuilder<TUserContext> : IFunctionWrapperBuilder<TUserContext, FunctionWrapper<TUserContext>> where TUserContext : IUserContext
    {
        ISimpleManagerBuilder<TUserContext> RegisterException<TException>()
            where TException : Exception;

        ISimpleManagerBuilder<TUserContext> RegisterException<TException>(Func<TException, bool> expression)
            where TException : Exception;
    }
}