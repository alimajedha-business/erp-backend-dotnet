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
    [Route("api/v{version:apiVersion}/Warehouse/ProductCodes")]
    [ApiController]
    public class ProductCodesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ProductCodesController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetProductCode()
        {
            var ProductCodes = _service.ProductCodeService.GetAll(trackChanges: false);
            return Ok(ProductCodes);
        }

        [HttpGet("{CompanyId:int}", Name = "ProductCodeById")]
        public IActionResult GetProductCode(int CompanyId)
        {
            var ProductCodes = _service.ProductCodeService.Get(CompanyId, trackChanges: false);
            return Ok(ProductCodes);
        }

        [HttpPost]
        public IActionResult CreateProductCode([FromBody] ProductCodeForCreationDto ProductCode)
        {
            if (ProductCode is null)
                return BadRequest("ProductCodeForCreationDto object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            var createdProductCode = _service.ProductCodeService.Create(ProductCode);
            return CreatedAtRoute("ProductCodeById", new {id = createdProductCode.Id }, createdProductCode);
        }

        [HttpGet("collection/({CompanyIds})", Name = "ProductCodeCollection")]
        public IActionResult GetProductCodeCollection([ModelBinder(BinderType =typeof(ArrayModelBinder))]
        IEnumerable<int> CompanyIds)
        {
            var ProductCodes = _service.ProductCodeService.GetByIds(CompanyIds, trackChanges: false);
            return Ok(ProductCodes);
        }

        [HttpPost("collection")]
        public IActionResult CreateProductCodeCollection([FromBody] IEnumerable<ProductCodeForCreationDto> ProductCodes)
        {
            var result = _service.ProductCodeService.CreateCollection(ProductCodes);
            return CreatedAtRoute("ProductCodeCollection", new {result.CompanyIds }, result.ProductCodes);
        }

        [HttpDelete("{CompanyId:int}")]
        public IActionResult DeleteProductCode(int CompanyId)
        {
            _service.ProductCodeService.Delete(CompanyId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{CompanyId:int}")]
        public IActionResult UpdateProductCode(int CompanyId, [FromBody] ProductCodeForUpdateDto ProductCode)
        {
            if (ProductCode is null)
                return BadRequest("ProductCode object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _service.ProductCodeService.Update(CompanyId, ProductCode, trackChanges: true);
            return NoContent();
        }

        [HttpPatch("{CompanyId}")]
        [Consumes(JsonMergePatchDocument.ContentType)]
        public IActionResult PatchProductCode(int CompanyId, [FromBody] JsonMergePatchDocument<ProductCodeForUpdateDto> ProductCodePatch)
        {
            if (ProductCodePatch is null)
            {
                return BadRequest("ProductCode object is null.");
            }
            var result = _service.ProductCodeService.GetProductCodeForPatch(CompanyId, trackChanges: true);
            ProductCodePatch.ApplyTo(result.ProductCodeForUpdate);
            TryValidateModel(result.ProductCodeForUpdate);
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _service.ProductCodeService.SaveChangesForPatch(result.ProductCodeForUpdate, result.ProductCodeEntity);

            return NoContent();
        }
    }
}
