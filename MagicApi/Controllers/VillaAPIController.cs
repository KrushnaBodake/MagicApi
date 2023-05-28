using MagicApi.Data;
using MagicApi.Model;
using MagicApi.Model.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace MagicApi.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]             //notify the application that this will be an api controller
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        public VillaAPIController(ILogger<VillaAPIController> logger)
        {
            _logger = logger;
        }
        /* [HttpGet]
         public IEnumerable<VillaDto> GetVillas()
         {
             *//* List<VillaDto> villaList = new List<VillaDto>();

              villaList.Add(new VillaDto { Id = 1, Name = "Pool View" });
              villaList.Add(new VillaDto { Id = 2, Name = "Beach View" });
              return villaList;*//*
             return VillaStore.villaList;
         }*/


        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVilla()
        {
            _logger.LogInformation("Getting all villas");
            return Ok(VillaStore.villaList);
        }
        /*[HttpGet("{id:int}")]
        public VillaDto GetVillas(int id)
        {
            *//* List<VillaDto> villaList = new List<VillaDto>();

             villaList.Add(new VillaDto { Id = 1, Name = "Pool View" });
             villaList.Add(new VillaDto { Id = 2, Name = "Beach View" });
             return villaList;*//*

            var villaList= VillaStore.villaList.Where(e=>e.Id==id).FirstOrDefault();
            return villaList;
        }*/


        [HttpGet("{id:int}", Name = "GetVilla")]
        /* [ProducesResponseType(200, Type=typeof(VillaDto))]*/
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDto> GetVillas(int id)
        {
            /* List<VillaDto> villaList = new List<VillaDto>();

             villaList.Add(new VillaDto { Id = 1, Name = "Pool View" });
             villaList.Add(new VillaDto { Id = 2, Name = "Beach View" });
             return villaList;*/
            if (id == 0)
            {
                _logger.LogError("Get villa Error with id" + id);
                return BadRequest();
            }

            var villa = VillaStore.villaList.Where(e => e.Id == id).FirstOrDefault();

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDto> CreateVillas([FromBody] VillaDto villaDto)
        {

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            else
            {
                VillaStore.villaList.Add(villaDto);
                return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
            }
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var villa = VillaStore.villaList.Where(v => v.Id == id).FirstOrDefault();
            if (villa == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }
        [HttpPut("{id:int}",Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id,[FromBody]VillaDto villaDto)
        {
            var villa= VillaStore.villaList.Where(v => v.Id == id).FirstOrDefault();
            if(villa!= null)
            {
                villa.Id = villaDto.Id;
                villa.Name = villaDto.Name;
            }
            return NoContent();
        }
        [HttpPatch("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult PartialUpdateVilla(int id,JsonPatchDocument<VillaDto> patchDto)
        {
            if(patchDto == null || id==0)
            {
                return BadRequest();
            }
            var villa=VillaStore.villaList.Where(v=>v.Id==id).FirstOrDefault();
            if(villa == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(villa, ModelState);
            if(ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}
