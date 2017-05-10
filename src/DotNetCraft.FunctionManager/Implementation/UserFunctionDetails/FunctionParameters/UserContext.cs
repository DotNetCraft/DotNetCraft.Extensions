using System;
using System.Collections.Generic;
using DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails;

namespace DotNetCraft.FunctionManager.Implementation.UserFunctionDetails.FunctionParameters
{
    public class UserContext: IUserContext
    {
        public Guid Id { get; }

        public IDictionary<string, object> UserData { get; private set; }

        public UserContext() : this(Guid.NewGuid())
        {            
        }

        public UserContext(Guid userContextId) : this(userContextId, new Dictionary<string, object>())
        {
        }

        public UserContext(Guid userContextId, Dictionary<string, object> userData)
        {
            if (userData == null)
                throw new ArgumentNullException(nameof(userData));
            if (userContextId == Guid.Empty)
                throw new ArgumentOutOfRangeException(nameof(userContextId));

            Id = userContextId;
            UserData = userData;
        }

        public void AddUserData(string key, object data)
        {
            UserData.Add(key, data);
        }

        public void DeleteUserData(string key)
        {
            UserData.Remove(key);
        }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Id: {0}", Id);
        }

        #endregion
    }
}
