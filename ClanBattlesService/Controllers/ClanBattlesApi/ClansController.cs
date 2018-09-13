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
using AutoMapper;
using ClanBattlesService.Dtos;

namespace ClanBattlesService.Controllers.ClanBattlesApi
{
    public class ClansController : ApiController
    {
        private ClanBattlesEntities _context;
        private string EntityName = "Clan";

        public ClansController()
        {
            _context = new ClanBattlesEntities();
        }

        [HttpGet]
        [Route("clanbattles/v1/clans")]
        public IHttpActionResult GetClans() {

            dynamic Response = new ExpandoObject();
            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;
                var Clans = _context.Clans.ToList();
                Response.Clans = Clans.Select(Mapper.Map<Clan, ClanDto>);
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
        [Route("clanbattles/v1/clans/{id:int}")]
        public IHttpActionResult GetClan(int id)
        {

            dynamic Response = new ExpandoObject();
            try
            {
                var clan = _context.Games.FirstOrDefault(c => c.id == id);
                if (clan == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Clan = Mapper.Map<Game, GameDto>(clan);
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
        [Route("clanbattles/v1/games/{gameid:int}/clans/{clanid:int}/members")]
        public IHttpActionResult GetMembersByClan(int gameid,int clanid)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;
                var members = _context.Members.Where(m => m.clanId == clanid).ToList();
                Response.Clans = members.Select(Mapper.Map<Member, MemberInfo>);
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
        [Route("clanbattles/v1/games/{gameid:int}/clans/{clanid:int}/members/{memberid:int}")]
        public IHttpActionResult GetMemberByClan(int gameid, int clanid, int memberid)
        {
            EntityName = "Member";
            dynamic Response = new ExpandoObject();

            try
            {
                var member = _context.Members.FirstOrDefault(m => m.id == memberid);

                if (member == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, memberid);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Member = Mapper.Map<Member, MemberInfo>(member);
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
        [Route("clanbattles/v1/games/{gameid:int}/clans/{clanid:int}/members")]
        public IHttpActionResult PostMemberByClan(int gameid, int clanid, [FromBody] MemberDto memberDto) 
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

                var member = Mapper.Map<MemberDto, Member>(memberDto);
                member.clanId = clanid;
                _context.Members.Add(member);
                _context.SaveChanges();

                memberDto.id = member.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Member = memberDto;

                return Created(new Uri(Request.RequestUri + "/" + memberDto.id), Response);

            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        [HttpPut]
        [Route("clanbattles/v1/games/{gameid:int}/clans/{clanid:int}/members/{memberid:int}")]
        public IHttpActionResult UpdateMemberByClan(int gameid, int clanid, int memberid, [FromBody] MemberDto memberdto)
        {
            EntityName = "Member";

            dynamic Response = new ExpandoObject();

            try
            {
                if (!ModelState.IsValid)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = ConstantValues.ErrorMessage.BAD_REQUEST;
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                var member = _context.Members.SingleOrDefault(m => m.id == memberid);

                if (member == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, memberid);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(memberdto, member);
                _context.SaveChanges();

                memberdto.id = member.id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Member = memberdto;
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
        [Route("clanbattles/v1/games/{gameid:int}/clans/{clanid:int}/members/{memberid:int}")]
        public IHttpActionResult DeleteMemberByClan(int gameid, int clanid, int memberid)
        {
            EntityName = "Member";
            dynamic Response = new ExpandoObject();

            try
            {
                var member = _context.Members.FirstOrDefault(m => m.id == memberid);

                if (member == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, memberid);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                member.status = ConstantValues.EntityStatus.INACTIVE;
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