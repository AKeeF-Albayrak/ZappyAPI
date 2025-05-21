using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.ViewModels.User;

namespace ZappyAPI.Application.Features.Query.User.GetUserById
{
    public class GetUserByIdQueryResponse
    {
        public bool Succeeded { get; set; }
        public UserViewModel User { get; set; }
    }
}
