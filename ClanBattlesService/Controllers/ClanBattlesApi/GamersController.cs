using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using ClanBattlesService.Models;
using ClanBattlesService.Dtos;
using ClanBattlesService.Dtos.Info;

namespace ClanBattlesService.Controllers.ClanBattlesApi
{
    public class GamersController : ApiController
    {
        private ClanBattlesEntities _context;
        private string EntityName = "Gamer";

        public GamersController()
        {
            _context = new ClanBattlesEntities();
        }

        [HttpGet]
        [Route("clanbattles/v1/gamers")]
        public IHttpActionResult GetGamers()
        {

            dynamic Response = new ExpandoObject();
            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;
                var Gamers = _context.Gamers.ToList();
                Response.Gamers = Gamers.Select(Mapper.Map<Gamer, GamerInfo>);
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
        [Route("clanbattles/v1/gamers/{id:int}")]
        public IHttpActionResult GetGamer(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var gamer = _context.Gamers.FirstOrDefault(g => g.id == id);
                if (gamer == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Clan = Mapper.Map<Gamer, GamerInfo>(gamer);
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
        [Route("clanbattles/v1/gamers/{gamerid:int}/members")]
        public IHttpActionResult GetMembersByGamer(int gamerid)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;
                var members = _context.Members.Where(m => m.gamerId == gamerid).ToList();
                Response.Members = members.Select(Mapper.Map<Member, MemberInfo>);
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
        [Route("clanbattles/v1/gamers/{gamerid:int}/reservations")]
        public IHttpActionResult GetReservationsByGamer(int gamerid)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;
                var reservations = _context.Reservations.Where(r => r.gamerId == gamerid).ToList();
                                   
                Response.Reservations = reservations.Select(Mapper.Map<Reservation,ReservationInfo>);
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
        [Route("clanbattles/v1/gamers/{gamerid:int}/reservations")]
        public IHttpActionResult PostReservationByGamer(int gamerid, [FromBody] ReservationDto reservationdto)
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

                var reservation = Mapper.Map<ReservationDto, Reservation>(reservationdto);
                reservation.gamerId = gamerid;
                _context.Reservations.Add(reservation);
                _context.SaveChanges();

                reservationdto.id = reservation.id;
                reservationdto.gamerId = gamerid;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Reservation = reservationdto;

                return Created(new Uri(Request.RequestUri + "/" + reservationdto.id), Response);

            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        [HttpPut]
        [Route("clanbattles/v1/gamers/{gamerid:int}/reservations/{reservationid:int}")]
        public IHttpActionResult UpdateReservationByGamer(int gamerid, int reservationid, [FromBody] ReservationDto reservationdto)
        {
            EntityName = "Reservation";

            dynamic Response = new ExpandoObject();

            try
            {
                if (!ModelState.IsValid)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = ConstantValues.ErrorMessage.BAD_REQUEST;
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                var reservation = _context.Reservations.SingleOrDefault(r => r.id == reservationid);

                if (reservation == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, reservationid);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(reservationdto, reservation);
                _context.SaveChanges();

                reservationdto.id = reservation.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Reservation = reservationdto;
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
        [Route("clanbattles/v1/gamers/{gamerid:int}/reservations/{reservationid:int}")]
        public IHttpActionResult DeleteReservationByGamer(int gamerid, int reservationid )
        {
            EntityName = "Reservation";
            dynamic Response = new ExpandoObject();

            try
            {
                var reservation = _context.Reservations.Where(r => r.status == ConstantValues.EntityStatus.ACTIVE).FirstOrDefault(r => r.id == reservationid);

                if (reservation == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, reservationid);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                reservation.status = ConstantValues.EntityStatus.INACTIVE;
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