using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CiklumTest.Models.Settings
{
    public class AuthOptions
    {
		public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int LifeTime { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
}
