using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCraft.FunctionManager.Interfaces;
using DotNetCraft.FunctionManager.Interfaces.ExceptionManagement.Managers;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Implementation.ExceptionManagement.Managers
{
    public class SmartExceptionStrategiesManager<TUserContext> : ISmartExceptionStrategiesManager<TUserContext>
    {
        private readonly List<ISimpleExceptionManager<TUserContext>> exceptionStrategiesManagers;

        public SmartExceptionStrategiesManager()
        {
            exceptionStrategiesManagers = new List<ISimpleExceptionManager<TUserContext>>();
        }

        #region Implementation of IExceptionDecisionMaker

        public bool ContinueRunning(Exception exception, TUserContext userContext)
        {
            foreach (ISimpleExceptionManager<TUserContext> simpleExceptionStrategiesManager in exceptionStrategiesManagers)
            {
                bool result = simpleExceptionStrategiesManager.ContinueRunning(exception, userContext);
                if (result)
                    return true;
            }

            return false;
        }

        public void RegisterStrategyManager(ISimpleExceptionManager<TUserContext> exceptionManager)
        {
            exceptionStrategiesManagers.Add(exceptionManager);
        }

        #endregion
    }
}
