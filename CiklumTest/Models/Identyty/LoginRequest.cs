﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CiklumTest.Models.Identyty
{
    public class LoginRequest 
    {
		public string Email { get; set; }
        public string Password { get; set; }
    }
}
