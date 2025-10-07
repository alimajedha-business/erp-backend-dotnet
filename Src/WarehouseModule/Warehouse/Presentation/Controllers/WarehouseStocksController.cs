using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Morcatko.AspNetCore.JsonMergePatch;
using System.ComponentModel.Design;
using Warehouse.Application.DTOs;
using Warehouse.Application.Interfaces.Services;
using Warehouse.Presentation.ModelBinders;

namespace Warehouse.Presentation.Controllers
{
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1-warehouse")]
    [Route("api/v{version:apiVersion}/Warehouse/WarehouseStocks")]
    [ApiController]
    public class WarehouseStocksController : ControllerBase
    {
        private readonly IServiceManager _service;

        public WarehouseStocksController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetWarehouseStock()
        {
            var WarehouseStocks = _service.WarehouseStockService.GetAll(trackChanges: false);
            return Ok(WarehouseStocks);
        }

        [HttpGet("{id:int}", Name = "WarehouseStockById")]
        public IActionResult GetWarehouseStock(int id)
        {
            var WarehouseStocks = _service.WarehouseStockService.Get(id, trackChanges: false);
            return Ok(WarehouseStocks);
        }

        [HttpPost]
        public IActionResult CreateWarehouseStock([FromBody] WarehouseStockForCreationDto WarehouseStock)
        {
            if (WarehouseStock is null)
                return BadRequest("WarehouseStockForCreationDto object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            var createdWarehouseStock = _service.WarehouseStockService.Create(WarehouseStock);
            return CreatedAtRoute("WarehouseStockById", new {id = createdWarehouseStock.Id }, createdWarehouseStock);
        }

        [HttpGet("collection/({ids})", Name = "WarehouseStockCollection")]
        public IActionResult GetWarehouseStockCollection([ModelBinder(BinderType =typeof(ArrayModelBinder))]
        IEnumerable<int> ids)
        {
            var WarehouseStocks = _service.WarehouseStockService.GetByIds(ids, trackChanges: false);
            return Ok(WarehouseStocks);
        }

        [HttpPost("collection")]
        public IActionResult CreateWarehouseStockCollection([FromBody] IEnumerable<WarehouseStockForCreationDto> WarehouseStocks)
        {
            var result = _service.WarehouseStockService.CreateCollection(WarehouseStocks);
            return CreatedAtRoute("WarehouseStockCollection", new {result.ids }, result.WarehouseStocks);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteWarehouseStock(int id)
        {
            _service.WarehouseStockService.Delete(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateWarehouseStock(int id, [FromBody] WarehouseStockForUpdateDto WarehouseStock)
        {
            if (WarehouseStock is null)
                return BadRequest("WarehouseStock object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _service.WarehouseStockService.Update(id, WarehouseStock, trackChanges: true);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [Consumes(JsonMergePatchDocument.ContentType)]
        public IActionResult PatchWarehouseStock(int id, [FromBody] JsonMergePatchDocument<WarehouseStockForUpdateDto> WarehouseStockPatch)
        {
            if (WarehouseStockPatch is null)
            {
                return BadRequest("WarehouseStock object is null.");
            }
            var result = _service.WarehouseStockService.GetWarehouseStockForPatch(id, trackChanges: true);
            WarehouseStockPatch.ApplyTo(result.WarehouseStockForUpdate);
            TryValidateModel(result.WarehouseStockForUpdate);
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _service.WarehouseStockService.SaveChangesForPatch(result.WarehouseStockForUpdate, result.WarehouseStockEntity);

            return NoContent();
        }
    }
}
