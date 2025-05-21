using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.ViewModels.Friendship;

namespace ZappyAPI.Application.Features.Query.Friendship.GetFriends
{
    public class GetFriendsQueryResponse
    {
        public List<FriendViewModel> Friends { get; set; }
        public bool Succeeded { get; set; }
    }
}
