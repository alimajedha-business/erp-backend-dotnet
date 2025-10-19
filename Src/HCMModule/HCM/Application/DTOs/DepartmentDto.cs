// Ignore Spelling: HCM Dto

using General.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Application.DTOs
{
    public record DepartmentDto(int Id, int CreatorId, User Creator, DateTime CreatedAt, User Modifier, DateTime? ModifiedAt,
         string? Code, string Name, bool Status, DateTime? StatusChangeDate);
}
