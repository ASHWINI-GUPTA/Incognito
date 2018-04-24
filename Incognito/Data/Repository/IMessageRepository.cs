using Incognito.Models;
using System.Collections.Generic;

namespace Incognito.Data
{
    public interface IMessageRepository
    {
        IList<Message> GetAllMessages();

        IList<ReportMessage> GetReportedMessage();

        IList<Message> GetUserMessages(string userId);

        IList<Message> GetUserArchives(string userId);

        IList<Message> GetUserDelete(string userId);

        Message GetMessage(int id);

        bool CheckMessageExists(int id);

        Message GetMessageByUserId(int id, string userId);
    }
}
