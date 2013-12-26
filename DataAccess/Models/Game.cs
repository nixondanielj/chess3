using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Ended { get; set; }
        public virtual ICollection<User> Players { get; set; }
        public User TurnPlayer { get; set; }
        public User Winner { get; set; }
        public GameType Type { get; set; }
        public ICollection<Move> Moves { get; set; }
    }
}
