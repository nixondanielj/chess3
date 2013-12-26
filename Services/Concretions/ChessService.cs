using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models;
using DataAccess.Models;

namespace Services.Concretions
{
    public class ChessService : Service, IGameService
    {

        public GameStateVM GetGame(int gameId, int playerId)
        {
            GameStateVM vm = new GameStateVM();
            var game = this.UOW.GetRepository<Game>().Get(g => g.Id == gameId).SingleOrDefault();
            vm.IsRequestingClientsTurn = (game.TurnPlayer.Id == playerId);
            vm.Board = null;
            return vm;
        }

        public void Move(Models.MoveFM move)
        {
            throw new NotImplementedException();
        }
    }
}
