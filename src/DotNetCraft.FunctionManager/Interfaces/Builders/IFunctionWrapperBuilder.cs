using System;
using DotNetCraft.FunctionManager.Implementation.Strategies.AttemptParameters;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Interfaces.Builders
{
    public interface IFunctionWrapperBuilder<TUserContext, TResult>
        where TUserContext : IUserContext
    {
        TResult WaitAndRetry(TimeSpan[] durations, Action<Exception, TUserContext, TimeSpanAttemptInformation> func);
        TResult RunForever(Action<Exception, TUserContext, AttemptInformation> func);
        TResult RunSeveralTimes(int maxAttempts, Action<Exception, TUserContext, AttemptInformation> func);
    }
}
