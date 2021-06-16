using System;

namespace TaskManagementSystem.Domain.Common.Exceptions
{
    [Serializable]
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string errorMessage) : this(errorMessage, null)
        {
            
        } 
        public UserFriendlyException(string errorMessage, Exception exc) : base($"{errorMessage}", exc)
        {
            
        }
    }
}
