using KookBotCraft.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KookBotCraft.Entity.Entity.Messages {
    public class MessageEntity : BaseEntity {
        public string Message { get; set; }

        public static void Configure(ModelBuilder modelBuilder) {
            modelBuilder.Entity<MessageEntity>();
        }
    }
}
