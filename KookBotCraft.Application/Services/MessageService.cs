using KookBotCraft.Database.DbContexts;
using KookBotCraft.Entity.Entity.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KookBotCraft.Application.Services {
    public class MessageService {
        private readonly DbSet<MessageEntity> _messageEntities;
        private readonly DefaultDbContext _dbContext;

        public MessageService(DefaultDbContext dbContext) {
            _messageEntities = dbContext.Set<MessageEntity>();
            _dbContext = dbContext;
        }

        public async ValueTask<MessageEntity> CreateMessageAsync(string message) {
            var result = await _messageEntities.AddAsync(new MessageEntity { Message = message });
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async ValueTask<MessageEntity[]> GetMessagesAsync() {
            return await _messageEntities.AsQueryable().ToArrayAsync();
        }

        public async ValueTask<MessageEntity> GetMessageAsync(long id) {
            var result = await _messageEntities.FindAsync(id);
            return result != null ? result : throw new Exception($"没有 id 为 {id} 的留言");
        }
    }
}
