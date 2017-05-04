using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Interfaces.Builders
{
    public interface ISmartManagerBuilder<TUserContext> : IFunctionWrapperBuilder<TUserContext, ISmartManagerBuilder<TUserContext>> where TUserContext : IUserContext
    {
        ISmartManagerBuilder<TUserContext> OnException<TException>()
           where TException : Exception;

        ISmartManagerBuilder<TUserContext> OnException<TException>(Func<TException, bool> expression)
            where TException : Exception;

        FunctionWrapper<TUserContext> Build();
    }
}
