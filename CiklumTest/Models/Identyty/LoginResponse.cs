using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CiklumTest.Models.Identyty
{
    public class LoginResponse 
    {
		public string access_token { get; set; }
        public string username { get; set; }
    }
}
