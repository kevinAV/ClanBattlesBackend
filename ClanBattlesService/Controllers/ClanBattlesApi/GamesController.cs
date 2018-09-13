using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ClanBattlesService.Models;
using System.Dynamic;
using ClanBattlesService.ConstantValues;
using AutoMapper;
using ClanBattlesService.Dtos;

namespace ClanBattlesService.Controllers
{
    public class GamesController : ApiController
    {
        private ClanBattlesEntities _context;
        private string EntityName = "Game";

        public GamesController()
        {
            _context = new ClanBattlesEntities();
        }

        // GET: /clanbattles/v1/games
        [HttpGet]
        public IHttpActionResult GetGames()
        {

            dynamic Response = new ExpandoObject();
            try {
                Response.Status = ConstantValues.ResponseStatus.OK;
                var games = _context.Games.ToList();
                Response.Games = games.Select(Mapper.Map<Game, GameDto>);
                return Ok(Response);
            }
            catch (Exception e) {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // GET: /clanbattles/v1/games/{id}
        [HttpGet]
        public IHttpActionResult GetGame(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var game = _context.Games.FirstOrDefault(g => g.id == id);
                if (game == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Game = Mapper.Map<Game, GameDto>(game);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        //POST /clanbattles/v1/games
        [HttpPost]
        public IHttpActionResult CreateGame(GameDto gameDto)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = ConstantValues.ErrorMessage.BAD_REQUEST;
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                var game = Mapper.Map<GameDto, Game>(gameDto);
                _context.Games.Add(game);
                _context.SaveChanges();

                gameDto.id = game.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Game = gameDto;

                return Created(new Uri(Request.RequestUri + "/" + game.id), Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // PUT /clanbattles/v1/games/{id}
        [HttpPut]
        public IHttpActionResult UpdateGame(int id, GameDto gameDto)
        {

            dynamic Response = new ExpandoObject();

            try
            {
                if (!ModelState.IsValid)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = ConstantValues.ErrorMessage.BAD_REQUEST;
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                var game = _context.Games.SingleOrDefault(g => g.id == id);

                if (game == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(gameDto, game);
                _context.SaveChanges();

                gameDto.id = game.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Game = gameDto;
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // DELETE /clanbattles/v1/games{id}
        [HttpDelete]
        public IHttpActionResult DeleteGame(int id) {

            dynamic Response = new ExpandoObject();

            try
            {
                var game = _context.Games.FirstOrDefault(g => g.id == id);

                if (game == null) {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                game.status = ConstantValues.EntityStatus.INACTIVE;
                _context.SaveChanges();

                Response.Status = ConstantValues.ResponseStatus.OK;

                return Ok(Response);

            }
            catch
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // GET /clanbattles/v1/games/{id}/clans
        [HttpGet]
        [Route("clanbattles/v1/games/{id:int}/clans")]
        public IHttpActionResult GetClansByGame(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;
                var clans = _context.Clans.Where(c => c.gameId == id).ToList();
                Response.Clans = clans.Select(Mapper.Map<Clan, ClanDto>);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        [HttpGet]
        [Route("clanbattles/v1/games/{gameid:int}/clans/{clanid:int}")]
        public IHttpActionResult GetClanByGame(int gameid, int clanid)
        {
            EntityName = "Clan";
            dynamic Response = new ExpandoObject();

            try
            {
                var clan = _context.Clans.FirstOrDefault(c => c.id == clanid);

                if (clan == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, clanid);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Clan = Mapper.Map<Clan, ClanDto>(clan);
                return Ok(Response);

            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }


        [HttpPost]
        [Route("clanbattles/v1/games/{id:int}/clans")]
        public IHttpActionResult PostClansByGame(int id, [FromBody] ClanDto clanDto)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                if (!ModelState.IsValid)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = ConstantValues.ErrorMessage.BAD_REQUEST;
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                var clan = Mapper.Map<ClanDto, Clan>(clanDto);
                clan.gameId = id;
                _context.Clans.Add(clan);
                _context.SaveChanges();

                clanDto.id = clan.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Clan = clanDto;

                return Created(new Uri(Request.RequestUri + "/" + clanDto.id), Response);

            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        [HttpPut]
        [Route("clanbattles/v1/games/{id:int}/clans/{clanid:int}")]
        public IHttpActionResult UpdateClanByGame(int id, int clanid, [FromBody]ClanDto clanDto)
        {
            EntityName = "Clan";

            dynamic Response = new ExpandoObject();

            try
            {
                if (!ModelState.IsValid)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = ConstantValues.ErrorMessage.BAD_REQUEST;
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                var clan = _context.Clans.SingleOrDefault(c => c.id == clanid);

                if (clan == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, clanid);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(clanDto, clan);
                _context.SaveChanges();

                clanDto.id = clan.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Clan = clanDto;
                return Ok(Response);

            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        [HttpDelete]
        [Route("clanbattles/v1/games/{id:int}/clans/{clanid:int}")]
        public IHttpActionResult DeleteClanByGame(int id, int clanid)
        {
            EntityName = "Clan";

            dynamic Response = new ExpandoObject();

            try
            {
                var clan = _context.Clans.FirstOrDefault(c => c.id == clanid);

                if (clan == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, clanid);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                clan.status = ConstantValues.EntityStatus.INACTIVE;
                _context.SaveChanges();

                Response.Status = ConstantValues.ResponseStatus.OK;

                return Ok(Response);

            }
            catch
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }
     



    }
}