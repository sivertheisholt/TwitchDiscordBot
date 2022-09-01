using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuskyBot.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Messagecount { get; set; }

        public int Xp { get; set; }

        public int Level { get; set; }

    }
}