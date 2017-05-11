using System.Collections.Generic;

namespace DotNetCraft.SimpleEmailSender.Entities
{
    public class SendResponse
    {
        public List<string> ErrorMessages { get; private set; }

        public bool IsSuccessed
        {
            get { return ErrorMessages.Count == 0; }
        }

        public SendResponse()
        {
            ErrorMessages = new List<string>();
        }
    }
}
