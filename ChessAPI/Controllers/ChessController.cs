using Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChessAPI.Controllers
{
    public class ChessController : ApiController
    {

        private IGameService GameService { get; set; }

        public ChessController(IGameService gameService)
        {
            this.GameService = gameService;
        }

        // GET api/<controller>/5
        public GameStateVM Get(int id)
        {
            return this.GameService.GetGame(id);
        }

        // POST api/<controller>
        public void Post(MoveFM data)
        {

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}