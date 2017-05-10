using System;
using DotNetCraft.FunctionManager.Implementation.Builders;
using DotNetCraft.FunctionManager.Interfaces.Builders;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager
{
    public static class FunctionBuilder
    {
        public static ISimpleExceptionManagerBuilder<TUserContext> RegisterException<TUserContext, TException>()
            where TException : Exception
            where TUserContext : IUserContext
        {
            SimpleExceptionExceptionManagerBuilder<TUserContext> exceptionExceptionManager = new SimpleExceptionExceptionManagerBuilder<TUserContext>();
            ISimpleExceptionManagerBuilder<TUserContext> exceptionRegistration = exceptionExceptionManager.RegisterException<TException>();
            return exceptionRegistration;
        }

        public static ISimpleExceptionManagerBuilder<TUserContext> RegisterException<TUserContext, TException>(Func<TException, bool> func) where TException : Exception where TUserContext : IUserContext
        {
            SimpleExceptionExceptionManagerBuilder<TUserContext> exceptionExceptionManager = new SimpleExceptionExceptionManagerBuilder<TUserContext>();
            ISimpleExceptionManagerBuilder<TUserContext> exceptionRegistration = exceptionExceptionManager.RegisterException(func);
            return exceptionRegistration;
        }

        public static ISmartExceptionManagerBuilder<TUserContext> OnException<TUserContext, TException>() where TException : Exception where TUserContext : IUserContext
        {
            SmartExceptionManagerBuilder<TUserContext> exceptionManagerBuilder = new SmartExceptionManagerBuilder<TUserContext>();
            ISmartExceptionManagerBuilder<TUserContext> simpleExceptionManagerBuilder = exceptionManagerBuilder.OnException<TException>();
            return simpleExceptionManagerBuilder;
        }
    }
}
