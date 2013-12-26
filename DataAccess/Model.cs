using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
namespace DataAccess.Model
{
    public partial class User
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public partial class GameType
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
    }

    public partial class Game
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int TurnPlayerId { get; set; }
        public int? WinnerId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Ended { get; set; }
    }

    public partial class Move
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public DateTime TimeMade { get; set; }
        public string Value { get; set; }
    }

    public partial class Token
    {
        public int UserId { get; set; }
        public DateTime Issued { get; set; }
        public string Key { get; set; }
    }

}


