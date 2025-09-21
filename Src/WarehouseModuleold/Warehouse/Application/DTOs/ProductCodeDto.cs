using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.DTOs
{
    public record class ProductCodeDto(Int64 Id, int CompanyId, int FirstLevelCode, string FirstLevelName, int SecondLevelCode, string SecondLevelName, bool SecondNextLevel,
                                       int ThirdLevelCode, string ThirdLevelName, bool ThirdNextLevel, 
                                       int FourthLevelCode, string FourthLevelName, bool FourthNextLevel,
                                       int FifthLevelCode, string FifthLevelName, bool FifthNextLevel, 
                                       int SixthLevelCode, string SixthLevelName, bool SixthNextLevel, 
                                       int SeventhLevelCode, string SeventhLevelName, bool SeventhNextLevel);
}
