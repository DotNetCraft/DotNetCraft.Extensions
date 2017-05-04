using System;
using DotNetCraft.FunctionManager.Implementation.ExceptionManagement.Managers;
using DotNetCraft.FunctionManager.Implementation.Strategies;
using DotNetCraft.FunctionManager.Implementation.Strategies.AttemptParameters;
using DotNetCraft.FunctionManager.Interfaces.Builders;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Managers;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Implementation.Builders
{
    public class SimpleManagerBuilder<TUserContext> : ISimpleManagerBuilder<TUserContext> where TUserContext : IUserContext
    {
        private ISimpleExceptionManager<TUserContext> exceptionManager;

        public SimpleManagerBuilder()
        {
            exceptionManager = new SimpleExceptionManager<TUserContext>();
        }

        #region Implementation of ISimpleExceptionManager

        public ISimpleManagerBuilder<TUserContext> RegisterException<TException>() where TException : Exception
        {
            exceptionManager.RegisterException<TException>();
            return this;
        }

        public ISimpleManagerBuilder<TUserContext> RegisterException<TException>(Func<TException, bool> expression) where TException : Exception
        {
            exceptionManager.RegisterException<TException>(expression);
            return this;
        }

        public FunctionWrapper<TUserContext> WaitAndRetry(TimeSpan[] durations, Action<Exception, TUserContext, TimeSpanAttemptInformation> func)
        {
            IExceptionStrategy<TUserContext> exceptionStrategy = new WaitStrategy<TUserContext>(durations, func);
            exceptionManager.RegisterStrategy(exceptionStrategy);
            FunctionWrapper<TUserContext> userActionWrapper = new FunctionWrapper<TUserContext>(exceptionManager);
            return userActionWrapper;
        }

        public FunctionWrapper<TUserContext> RunForever(Action<Exception, TUserContext, AttemptInformation> func)
        {
            IExceptionStrategy<TUserContext> exceptionStrategy = new RunForeverStrategy<TUserContext>(func);
            exceptionManager.RegisterStrategy(exceptionStrategy);
            FunctionWrapper<TUserContext> userActionWrapper = new FunctionWrapper<TUserContext>(exceptionManager);
            return userActionWrapper;
        }

        public FunctionWrapper<TUserContext> RunSeveralTimes(int maxAttempts, Action<Exception, TUserContext, AttemptInformation> func)
        {
            IExceptionStrategy<TUserContext> exceptionStrategy = new SeveralAttemptsStrategy<TUserContext>(maxAttempts, func);
            exceptionManager.RegisterStrategy(exceptionStrategy);
            FunctionWrapper<TUserContext> userActionWrapper = new FunctionWrapper<TUserContext>(exceptionManager);
            return userActionWrapper;
        }

        #endregion
    }
}