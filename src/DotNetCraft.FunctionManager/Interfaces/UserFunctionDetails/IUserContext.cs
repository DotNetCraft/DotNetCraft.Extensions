using System;
using System.Collections.Generic;

namespace DotNetCraft.FunctionManager.Interfaces.UserFunctionDetails
{
    /// <summary>
    /// Interface shows that object is a user's context.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// The context's identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// User's data.
        /// </summary>
        IDictionary<string, object> UserData { get; }
    }
}
