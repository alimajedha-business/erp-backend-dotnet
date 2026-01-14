using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace NGErp.HCM.API.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1-general")]
    [Route("api/v{version:apiVersion}/general/Companies")]
    [JwtAuthorize] // Require authentication for all endpoints
    public class DepartmentController: ControllerBase
    {
    }
}
