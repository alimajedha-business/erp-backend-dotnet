using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class CategoryRepository(MainDbContext context) :
    RepositoryWithCompany<Category>(context),
    ICategoryRepository

{
    public async Task<CategoryCodeDto?> GetCategoryCodeAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var category = await _context.Set<Category>()
        .AsNoTracking()
        .Where(c => c.CompanyId == companyId && c.Id == id)
        .Select(c => new
        {
            c.Id,
            c.Code,
            c.LevelNo,
            c.ParentCategoryId
        })
        .SingleOrDefaultAsync(ct);

        if (category is null)
            return null;

        var codes = new List<string> { category.Code };
        var parentId = category.ParentCategoryId;

        while (parentId is not null)
        {
            var parent = await _context.Set<Category>()
                .AsNoTracking()
                .Where(c => c.CompanyId == companyId && c.Id == parentId.Value)
                .Select(c => new
                {
                    c.Code,
                    c.ParentCategoryId
                })
                .SingleOrDefaultAsync(ct);

            if (parent is null)
                break;

            codes.Insert(0, parent.Code);
            parentId = parent.ParentCategoryId;
        }

        return new CategoryCodeDto(
            category.Id,
            category.Code,
            string.Join("-", codes),
            category.LevelNo
        );
    }
}
