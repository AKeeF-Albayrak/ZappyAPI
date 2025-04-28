using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Domain.Enums
{
    public enum AuditLog_TargetType
    {
        User,
        Message,
        Group,
        Friendship,
        GroupInvite,
        Report,
        Notification,
        RefreshToken,
        Session,
        LoginHistory,
        UserStatus
    }

}
