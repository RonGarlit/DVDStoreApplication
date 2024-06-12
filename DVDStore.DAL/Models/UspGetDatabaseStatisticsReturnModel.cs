
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // ****************************************************************************************************
    // DVDStore DAL Code
    // ****************************************************************************************************

    public partial class UspGetDatabaseStatisticsReturnModel
    {
        public string TableName { get; set; }
        public int? Rows { get; set; }
        public string SpaceReservedUsed { get; set; }
        public string DataSpaceUsed { get; set; }
        public string IndexSpaceUsed { get; set; }
        public string UnusedSpace { get; set; }
    }

}
// </auto-generated>
