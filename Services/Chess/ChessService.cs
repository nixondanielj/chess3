using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models;
using DataAccess.Model;
using DataAccess;

namespace Services.Chess
{
    public class ChessService : Service, IGameService
    {

        public GameStateVM GetGame(int gameId, int playerId)
        {
            GameStateVM vm = new GameStateVM();
            Game game;
            using (var uow = new UnitOfWork())
            {
                game = uow.GameRepository.GetById(gameId).SingleOrDefault();
            }
            vm.IsRequestingClientsTurn = (game.TurnPlayerId == playerId);
            vm.Board = null;
            return vm;
        }

        public void Move(MoveFM move)
        {
            
        }
    }
}
