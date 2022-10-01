using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuskyBot.Entities
{
    public class UserCommand
    {
        public int Id { get; set; }
        public bool TranslateDoge { get; set; }
        public bool TranslateYoda { get; set; }
    }
}