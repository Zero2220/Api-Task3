using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;
using Services.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversityApi.Data;
using UniversityApi.Dtos;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly UniDatabase _context;
        private readonly GroupService _groupService;

        public GroupsController(UniDatabase context,GroupService groupService)
        {
            _context = context;
            _groupService = groupService;

        }

        [HttpGet("")]
        public ActionResult<List<GroupGetDto>> GetGroup()
        {
            try
            {
                return StatusCode(201, new { id = _groupService.GetAll() });
            }
            catch (DublicateEntityException e)
            {
                return Conflict();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Bilinmedik bir xeta bas verdi");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> GetById(int id)
        {
            try
            {
                return StatusCode(201, new { id = _groupService.GetById(id) });
            }
            catch (DublicateEntityException e)
            {
                return Conflict();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Bilinmedik bir xeta bas verdi");
            }
        }

        [HttpPost("")]
        public ActionResult Create(CreateDto createDto)
        {
            try
            {
                return StatusCode(201, new { id = _groupService.Create(createDto) });
            }
            catch (DublicateEntityException e)
            {
                return Conflict();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Bilinmedik bir xeta bas verdi");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Edit(int id, EditDto editDto)
        {
            try
            {
                return StatusCode(201, new { id = _groupService.Edit(id,editDto) });
            }
            catch (DublicateEntityException e)
            {
                return Conflict();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Bilinmedik bir xeta bas verdi");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                return StatusCode(201, new { id = _groupService.Delete(id) });
            }
            catch (DublicateEntityException e)
            {
                return Conflict();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Bilinmedik bir xeta bas verdi");
            }
        }
    }
}

