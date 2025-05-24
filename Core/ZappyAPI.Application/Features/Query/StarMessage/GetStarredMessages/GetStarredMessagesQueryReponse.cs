using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.ViewModels.StarMessage;

namespace ZappyAPI.Application.Features.Query.StarMessage.GetStarredMessages
{
    public class GetStarredMessagesQueryReponse
    {
        public bool Succeeded { get; set; }
        public List<StarredMessageViewModel> StarredMessages { get; set; }
    }
}
