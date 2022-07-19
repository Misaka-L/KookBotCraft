using KaiHeiLa.Commands;
using KookBotCraft.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KookBotCraft.Bot.Modules {
    [Group("message")]
    [Summary("留言模块")]
    public class MessageModule : ModuleBase<SocketCommandContext> {
        private readonly MessageService _messageService;

        public MessageModule(MessageService messageService) {
            _messageService = messageService;
        }

        [Command("create")]
        [Summary("创建留言")]
        public async Task CreateMessageAsync([Remainder][Summary("留言内容")] string message) {
            var result = await _messageService.CreateMessageAsync(message);
            await ReplyKMarkdownAsync($"创建留言成功: {result.Id}");
        }

        [Command("list")]
        [Summary("列出留言")]
        public async Task ListMessagesAsync() {
            var result = await _messageService.GetMessagesAsync();
            string list = "留言:";
            foreach (var item in result) {
                list += $"\n- ({item.Id}) {item.Message} 创建于 {item.CreatedTime}";
            }

            await ReplyKMarkdownAsync(list);
        }

        [Command("get")]
        [Summary("获取留言")]
        public async Task GetMessageAsync(long id) {
            var result = await _messageService.GetMessageAsync(id);
            await ReplyKMarkdownAsync($"- ({result.Id}) {result.Message} 创建于 {result.CreatedTime}");
        }
    }
}
