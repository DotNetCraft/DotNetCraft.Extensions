using System;
using DotNetCraft.FunctionManager;
using DotNetCraft.FunctionManager.Implementation.UserFunctionDetails;
using DotNetCraft.FunctionManager.Implementation.UserFunctionDetails.FunctionParameters;
using NUnit.Framework;

namespace FunctionManager.Tests
{
    [TestFixture]
    public class DemonstrationTests
    {
        private class MyUserContext : UserContext
        {
            public bool CanExit{ get; set; }
        }

        [Test]
        public void RetrySeveralTimesExceptionTest()
        {
            int countExceptionHandled = 0;
            MyUserContext userContext = new MyUserContext { CanExit = false };
            FunctionWrapper<MyUserContext> function = FunctionBuilder
                .RegisterException<MyUserContext, IndexOutOfRangeException>()
                .RunSeveralTimes(10, (exception, context, attemptInfo) =>
                {
                    countExceptionHandled++;
                    if (attemptInfo.AttemptNumber == 5)
                        context.CanExit = true;
                });

            function.Execute(context => 
            {
                if (context.CanExit == false)
                    throw new IndexOutOfRangeException();

            }, userContext);

            Assert.AreEqual(6, countExceptionHandled);
        }

        [Test]
        public void WaitAndRetryExceptionTest()
        {
            int countExceptionHandled = 0;
            int totalMs = 0;
            MyUserContext userContext = new MyUserContext { CanExit = false };
            FunctionWrapper<MyUserContext> function = FunctionBuilder
                .RegisterException<MyUserContext, IndexOutOfRangeException>()
                .WaitAndRetry(new[]
                {
                    TimeSpan.FromMilliseconds(10),
                    TimeSpan.FromMilliseconds(20),
                    TimeSpan.FromMilliseconds(30),
                }, (exception, context, attemptInfo) =>
                {
                    totalMs += attemptInfo.CurrentTimeSpan.Milliseconds;
                    countExceptionHandled++;
                    if (attemptInfo.AttemptNumber == 2)
                        context.CanExit = true;
                });

            function.Execute(context =>
            {
                if (context.CanExit == false)
                    throw new IndexOutOfRangeException();

            }, userContext);

            Assert.AreEqual(3, countExceptionHandled);
            Assert.AreEqual(10+20+30, totalMs);
        }
    }
}
