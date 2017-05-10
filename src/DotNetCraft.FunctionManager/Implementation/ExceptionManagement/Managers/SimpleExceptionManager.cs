using System;
using System.Collections.Generic;
using DotNetCraft.FunctionManager.Implementation.Strategies;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Managers;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Strategies;

namespace DotNetCraft.FunctionManager.Implementation.ExceptionManagement.Managers
{
    /// <summary>
    /// Simple exception manager that has only one strategy how to handle an exception.
    /// </summary>
    public class SimpleExceptionManager<TUserContext>: ISimpleExceptionManager<TUserContext>
    {
        #region Fields...
        /// <summary>
        /// List of acceptable exceptions.
        /// </summary>
        private readonly List<ExceptionPredicate> exceptionPredicates;

        /// <summary>
        /// The exception strategy.
        /// </summary>
        private IExceptionStrategy<TUserContext> exceptionStrategy;

        #endregion

        #region Constructors...

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <remarks>
        /// This constructor doesn't create any strategies.
        /// Thus, before executing user's function the strategy should be registered by invoking RegisterStrategy method. 
        /// </remarks>
        public SimpleExceptionManager()
        {
            exceptionStrategy = null;
            exceptionPredicates = new List<ExceptionPredicate>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exceptionStrategy">The exception strategy.</param>
        /// <exception cref="ArgumentNullException"><paramref name="exceptionStrategy"/> is <see langword="null"/></exception>
        public SimpleExceptionManager(IExceptionStrategy<TUserContext> exceptionStrategy)
        {
            if (exceptionStrategy == null)
                throw new ArgumentNullException(nameof(exceptionStrategy));

            this.exceptionStrategy = exceptionStrategy;
            exceptionPredicates = new List<ExceptionPredicate>();
        }

        #endregion

        #region Implementation of IExceptionStrategiesManager

        public void RegisterStrategy(IExceptionStrategy<TUserContext> exceptionStrategy)
        {
            if (this.exceptionStrategy != null)
                throw new ArgumentOutOfRangeException();

            this.exceptionStrategy = exceptionStrategy;
        }

        public void RegisterException<TException>() 
            where TException : Exception
        {
            ExceptionPredicate predicate = exception => exception is TException;
            exceptionPredicates.Add(predicate);
        }

        public void RegisterException<TException>(Func<TException, bool> exceptionPredicate) 
            where TException : Exception
        {
            ExceptionPredicate predicate = exception => exception is TException && exceptionPredicate((TException)exception);
            exceptionPredicates.Add(predicate);
        }

        public bool ContinueRunning(Exception exception, TUserContext userContext)
        {
            foreach (ExceptionPredicate exceptionPredicate in exceptionPredicates)
            {
                var result = exceptionPredicate.Invoke(exception);
                if (result)
                {
                    return exceptionStrategy.ContinueRunning(exception, userContext);
                }
            }

            return false;
        }

        #endregion
    }
}
