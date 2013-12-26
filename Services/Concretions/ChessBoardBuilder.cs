using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace Services.Concretions
{
    public class ChessBoardBuilder : IBoardBuilder
    {

        public object BuildBoard(IEnumerable<Move> moves)
        {
            ChessBoard board = new ChessBoard();
            foreach (Move move in moves)
            {
                board.ForceMove(move);
            }
            return board;
        }
    }
}
