using Warehouse.Application.DTOs;
using Warehouse.Application.Interfaces.Services;
using Warehouse.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Morcatko.AspNetCore.JsonMergePatch;
using System.ComponentModel.Design;

namespace Warehouse.Presentation.Controllers
{
    [Route("api/Warehouse/WarehouseType")]
    [ApiController]
    public class WarehouseTypesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public WarehouseTypesController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetWarehouseType()
        {
            var WarehouseTypes = _service.WarehouseTypeService.GetAll(trackChanges: false);
            return Ok(WarehouseTypes);
        }

        [HttpGet("{id:int}", Name = "WarehouseTypeById")]
        public IActionResult GetWarehouseType(int id)
        {
            var WarehouseTypes = _service.WarehouseTypeService.Get(id, trackChanges: false);
            return Ok(WarehouseTypes);
        }

        [HttpPost]
        public IActionResult CreateWarehouseType([FromBody] WarehouseTypeForCreationDto WarehouseType)
        {
            if (WarehouseType is null)
                return BadRequest("WarehouseTypeForCreationDto object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            var createdWarehouseType = _service.WarehouseTypeService.Create(WarehouseType);
            return CreatedAtRoute("WarehouseTypeById", new {id = createdWarehouseType.Id }, createdWarehouseType);
        }

        [HttpGet("collection/({ids})", Name = "WarehouseTypeCollection")]
        public IActionResult GetWarehouseTypeCollection([ModelBinder(BinderType =typeof(ArrayModelBinder))]
        IEnumerable<int> ids)
        {
            var WarehouseTypes = _service.WarehouseTypeService.GetByIds(ids, trackChanges: false);
            return Ok(WarehouseTypes);
        }

        [HttpPost("collection")]
        public IActionResult CreateWarehouseTypeCollection([FromBody] IEnumerable<WarehouseTypeForCreationDto> WarehouseTypes)
        {
            var result = _service.WarehouseTypeService.CreateCollection(WarehouseTypes);
            return CreatedAtRoute("WarehouseTypeCollection", new {result.ids }, result.WarehouseTypes);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteWarehouseType(int id)
        {
            _service.WarehouseTypeService.Delete(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateWarehouseType(int id, [FromBody] WarehouseTypeForUpdateDto WarehouseType)
        {
            if (WarehouseType is null)
                return BadRequest("WarehouseType object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _service.WarehouseTypeService.Update(id, WarehouseType, trackChanges: true);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [Consumes(JsonMergePatchDocument.ContentType)]
        public IActionResult PatchWarehouseType(int id, [FromBody] JsonMergePatchDocument<WarehouseTypeForUpdateDto> WarehouseTypePatch)
        {
            if (WarehouseTypePatch is null)
            {
                return BadRequest("WarehouseType object is null.");
            }
            var result = _service.WarehouseTypeService.GetWarehouseTypeForPatch(id, trackChanges: true);
            WarehouseTypePatch.ApplyTo(result.WarehouseTypeForUpdate);
            TryValidateModel(result.WarehouseTypeForUpdate);
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _service.WarehouseTypeService.SaveChangesForPatch(result.WarehouseTypeForUpdate, result.WarehouseTypeEntity);

            return NoContent();
        }
    }
}
