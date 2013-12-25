using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class GameStateVM
    {
        public bool IsRequestingClientsTurn { get; set; }
        public object Board { get; set; }
    }
}
