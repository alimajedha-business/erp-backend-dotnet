using Warehouse.Application.DTOs;
using Warehouse.Application.Interfaces.Services;
using Warehouse.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Morcatko.AspNetCore.JsonMergePatch;
using System.ComponentModel.Design;

namespace Warehouse.Presentation.Controllers
{
    [Route("api/Warehouse/ProductHierarchies")]
    [ApiController]
    public class ProductHierarchiesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ProductHierarchiesController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetProductHierarchy()
        {
            var ProductHierarchies = _service.ProductHierarchyService.GetAll(trackChanges: false);
            return Ok(ProductHierarchies);
        }

        [HttpGet("{CompanyId:int}", Name = "ProductHierarchyById")]
        public IActionResult GetProductHierarchy(int CompanyId)
        {
            var ProductHierarchies = _service.ProductHierarchyService.Get(CompanyId, trackChanges: false);
            return Ok(ProductHierarchies);
        }

        [HttpPost]
        public IActionResult CreateProductHierarchy([FromBody] ProductHierarchyForCreationDto ProductHierarchy)
        {
            if (ProductHierarchy is null)
                return BadRequest("ProductHierarchyForCreationDto object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            var createdProductHierarchy = _service.ProductHierarchyService.Create(ProductHierarchy);
            return CreatedAtRoute("ProductHierarchyById", new {id = createdProductHierarchy.Id }, createdProductHierarchy);
        }

        [HttpGet("collection/({CompanyIds})", Name = "ProductHierarchyCollection")]
        public IActionResult GetProductHierarchyCollection([ModelBinder(BinderType =typeof(ArrayModelBinder))]
        IEnumerable<int> CompanyIds)
        {
            var ProductHierarchies = _service.ProductHierarchyService.GetByIds(CompanyIds, trackChanges: false);
            return Ok(ProductHierarchies);
        }

        [HttpPost("collection")]
        public IActionResult CreateProductHierarchyCollection([FromBody] IEnumerable<ProductHierarchyForCreationDto> ProductHierarchies)
        {
            var result = _service.ProductHierarchyService.CreateCollection(ProductHierarchies);
            return CreatedAtRoute("ProductHierarchyCollection", new {result.CompanyIds }, result.ProductHierarchies);
        }

        [HttpDelete("{CompanyId:int}")]
        public IActionResult DeleteProductHierarchy(int CompanyId)
        {
            _service.ProductHierarchyService.Delete(CompanyId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{CompanyId:int}")]
        public IActionResult UpdateProductHierarchy(int CompanyId, [FromBody] ProductHierarchyForUpdateDto ProductHierarchy)
        {
            if (ProductHierarchy is null)
                return BadRequest("ProductHierarchy object is null");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _service.ProductHierarchyService.Update(CompanyId, ProductHierarchy, trackChanges: true);
            return NoContent();
        }

        [HttpPatch("{CompanyId}")]
        [Consumes(JsonMergePatchDocument.ContentType)]
        public IActionResult PatchProductHierarchy(int CompanyId, [FromBody] JsonMergePatchDocument<ProductHierarchyForUpdateDto> ProductHierarchyPatch)
        {
            if (ProductHierarchyPatch is null)
            {
                return BadRequest("ProductHierarchy object is null.");
            }
            var result = _service.ProductHierarchyService.GetProductHierarchyForPatch(CompanyId, trackChanges: true);
            ProductHierarchyPatch.ApplyTo(result.ProductHierarchyForUpdate);
            TryValidateModel(result.ProductHierarchyForUpdate);
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _service.ProductHierarchyService.SaveChangesForPatch(result.ProductHierarchyForUpdate, result.ProductHierarchyEntity);

            return NoContent();
        }
    }
}
