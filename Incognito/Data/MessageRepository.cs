using Incognito.Models;
using System.Collections.Generic;
using System.Linq;

namespace Incognito.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageContext messageContext;

        public MessageRepository(MessageContext messageContext)
        {
            this.messageContext = messageContext;
        }

        public IList<Message> GetAllMessages()
        {
            return messageContext.Messages
                .Where(m => !m.IsDeleted)
                .OrderByDescending(d => d.SentTime)
                .ToList();
        }

        public IList<Message> GetUserMessages(string userId)
        {
            return messageContext.Messages
                .Where(u => u.RecevierId == userId && !u.IsDeleted && !u.IsArchived)
                .OrderByDescending(d => d.SentTime)
                .ToList();
        }

        public IList<Message> GetUserArchives(string userId)
        {
            return messageContext.Messages
                .Where(u => u.RecevierId == userId && u.IsArchived)
                .OrderByDescending(d => d.SentTime)
                .ToList();
        }

        public IList<Message> GetUserDelete(string userId)
        {
            return messageContext.Messages
                .Where(u => u.RecevierId == userId && u.IsDeleted)
                .OrderByDescending(d => d.SentTime)
                .ToList();
        }

        public Message GetMessage(int id)
        {
            return messageContext.Messages
                .SingleOrDefault(m => m.Id == id);
        }

        public bool CheckMessageExists(int id)
        {
            return messageContext.Messages
                .Any(e => e.Id == id);
        }
    }
}
