using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Move
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public User Player { get; set; }
        public Game Game { get; set; }
        public DateTime TimeMoved { get; set; }
    }
}
