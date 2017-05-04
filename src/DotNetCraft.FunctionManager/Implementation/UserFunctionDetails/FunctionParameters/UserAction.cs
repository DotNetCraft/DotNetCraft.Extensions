using System;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Implementation.UserFunctionDetails.FunctionParameters
{
    class UserAction<TUserContext>: IUserAction<TUserContext>
        where TUserContext: IUserContext
    {
        private readonly Action<TUserContext> _action;

        public UserAction(Action<TUserContext> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            _action = action;
        }

        #region Implementation of IUserAction

        public void Execute(TUserContext userContext)
        {
            _action.Invoke(userContext);
        }

        #endregion
    }
}
