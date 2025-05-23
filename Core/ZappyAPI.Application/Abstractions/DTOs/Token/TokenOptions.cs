﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZappyAPI.Application.Abstractions.DTOs.Token
{
    public class TokenOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        //public string Audience { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
