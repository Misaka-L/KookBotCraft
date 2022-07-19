using KaiHeiLa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KookBotCraft.Core.CardMessages {
    public interface ICardMessage {
        Card[] Build();
    }
}
