﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
