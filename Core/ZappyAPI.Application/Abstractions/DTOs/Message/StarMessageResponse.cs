using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.ViewModels.StarMessage;

namespace ZappyAPI.Application.Abstractions.DTOs.Message
{
    public class StarMessageResponse
    {
        public List<StarredMessageViewModel> StarredMessageViewModel { get; set; }
        public bool Succeeded { get; set; }
    }
}
