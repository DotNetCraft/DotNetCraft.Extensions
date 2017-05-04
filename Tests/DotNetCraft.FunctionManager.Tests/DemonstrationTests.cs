using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCraft.FunctionManager.Implementation.UserFunctionDetails;
using NUnit.Framework;

namespace DotNetCraft.FunctionManager.Tests
{
    [TestFixture]
    public class DemonstrationTests
    {
        private class MyUserContext : UserContext
        {
            public bool CanExit{ get; set; }
        }

        [Test]
        public void SingleExceptionTest()
        {
            MyUserContext userContext = new MyUserContext { CanExit = false };
            FunctionWrapper<MyUserContext> function = FunctionBuilder.RegisterException<MyUserContext, IndexOutOfRangeException>().RunSeveralTimes(10, (exception, context, attemptInfo) =>
            {
                if (attemptInfo.AttemptNumber == 5)
                    context.CanExit = true;
            });

            function.Execute(context => 
            {
                if (context.CanExit == false)
                    throw new IndexOutOfRangeException();

            }, userContext);
        }
    }
}
