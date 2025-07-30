using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.DataAccess.Repositories.Extensions
{
    public static class RepositoryCountryExtensions
    {
        //public static IQueryable<Country> FilterEmployees(this IQueryable<Country> countries, uint minAge, uint maxAge) =>
        //    countries.Where(e => (e.Age >= minAge && e.Age <= maxAge));
        public static IQueryable<Country> Search(this IQueryable<Country> countries, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return countries;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return countries.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}
