using System;
using DotNetCraft.FunctionManager.Implementation.Builders;
using DotNetCraft.FunctionManager.Implementation.ExceptionManagement.Managers;
using DotNetCraft.FunctionManager.Implementation.Strategies;
using DotNetCraft.FunctionManager.Implementation.Strategies.AttemptParameters;
using DotNetCraft.FunctionManager.Interfaces.Builders;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Managers;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager
{
    public static class FunctionBuilder
    {
        public static ISimpleManagerBuilder<TUserContext> RegisterException<TUserContext, TException>()
            where TException : Exception
            where TUserContext : IUserContext
        {
            SimpleManagerBuilder<TUserContext> manager = new SimpleManagerBuilder<TUserContext>();
            ISimpleManagerBuilder<TUserContext> exceptionRegistration = manager.RegisterException<TException>();
            return exceptionRegistration;
        }

        public static ISimpleManagerBuilder<TUserContext> RegisterException<TUserContext, TException>(Func<TException, bool> func) where TException : Exception where TUserContext : IUserContext
        {
            SimpleManagerBuilder<TUserContext> manager = new SimpleManagerBuilder<TUserContext>();
            ISimpleManagerBuilder<TUserContext> exceptionRegistration = manager.RegisterException<TException>(func);
            return exceptionRegistration;
        }

        public static ISmartManagerBuilder<TUserContext> OnException<TUserContext, TException>() where TException : Exception where TUserContext : IUserContext
        {
            SmartManagerBuilder<TUserContext> managerBuilder = new SmartManagerBuilder<TUserContext>();
            ISmartManagerBuilder<TUserContext> simpleManagerBuilder = managerBuilder.OnException<TException>();
            return simpleManagerBuilder;
        }
    }
}
