using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.DTOs
{
    public record class ProductHierarchy(Int64 Id, int CompnyId, byte FirstLevelSize, string FirstLevelType, 
                                                   byte SecondLevelSize, string SecondLevelType,
                                                   byte ThirdLevelSize, string ThirdLevelType,
                                                   byte FourthLevelSize, string FourthLevelType,
                                                   byte FifthLevelSize, string FifthLevelType,
                                                   byte SixthLevelSize, string SixthLevelType,
                                                   byte SeventhLevelSize, string SeventhLeveLtype);
}
