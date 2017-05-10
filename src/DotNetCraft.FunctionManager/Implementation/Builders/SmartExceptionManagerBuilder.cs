using System;
using DotNetCraft.FunctionManager.Implementation.ExceptionManagement.Managers;
using DotNetCraft.FunctionManager.Implementation.Strategies;
using DotNetCraft.FunctionManager.Implementation.Strategies.AttemptParameters;
using DotNetCraft.FunctionManager.Implementation.UserFunctionDetails;
using DotNetCraft.FunctionManager.Interfaces.Builders;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Managers;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Implementation.Builders
{
    public class SmartExceptionManagerBuilder<TUserContext> : ISmartExceptionManagerBuilder<TUserContext>
        where TUserContext : IUserContext
    {
        private ISimpleExceptionManager<TUserContext> exceptionManager;
        private ISmartExceptionStrategiesManager<TUserContext> mainManager;

        public SmartExceptionManagerBuilder()
        {
            mainManager = new SmartExceptionStrategiesManager<TUserContext>();
        }

        #region Implementation of ISmartExceptionManagerBuilder

        public ISmartExceptionManagerBuilder<TUserContext> WaitAndRetry(TimeSpan[] durations, Action<Exception, TUserContext, TimeSpanAttemptInformation> func)
        {
            IExceptionStrategy<TUserContext> exceptionStrategy = new WaitStrategy<TUserContext>(durations, func);
            exceptionManager.RegisterStrategy(exceptionStrategy);
            mainManager.RegisterStrategyManager(exceptionManager);
            exceptionManager = null;
            return this;
        }

        public ISmartExceptionManagerBuilder<TUserContext> RunForever(Action<Exception, TUserContext, AttemptInformation> func)
        {
            IExceptionStrategy<TUserContext> exceptionStrategy = new RunForeverStrategy<TUserContext>(func);
            exceptionManager.RegisterStrategy(exceptionStrategy);
            mainManager.RegisterStrategyManager(exceptionManager);
            exceptionManager = null;
            return this;
        }

        public ISmartExceptionManagerBuilder<TUserContext> RunSeveralTimes(int maxAttempts, Action<Exception, TUserContext, AttemptInformation> func)
        {
            IExceptionStrategy<TUserContext> exceptionStrategy = new SeveralAttemptsStrategy<TUserContext>(maxAttempts, func);
            exceptionManager.RegisterStrategy(exceptionStrategy);
            mainManager.RegisterStrategyManager(exceptionManager);
            exceptionManager = null;
            return this;
        }

        public ISmartExceptionManagerBuilder<TUserContext> OnException<TException>() 
            where TException : Exception
        {
            if (exceptionManager == null)
                exceptionManager = new SimpleExceptionManager<TUserContext>();
            exceptionManager.RegisterException<TException>();
            return this;
        }

        public ISmartExceptionManagerBuilder<TUserContext> OnException<TException>(Func<TException, bool> expression)
            where TException : Exception
        {
            if (exceptionManager == null)
                exceptionManager = new SimpleExceptionManager<TUserContext>();
            exceptionManager.RegisterException(expression);
            return this;
        }

        public FunctionWrapper<TUserContext> Build()
        {
            FunctionWrapper<TUserContext> userActionWrapper = new FunctionWrapper<TUserContext>(mainManager);
            return userActionWrapper;
        }

        #endregion
    }
}
