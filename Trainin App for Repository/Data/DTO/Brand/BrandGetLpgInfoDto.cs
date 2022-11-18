using System.Collections.Generic;

namespace Trainin_App_for_Repository.Data.DTO.Brand
{
    public class BrandGetLpgInfoDto
    {  
        public string Lastupdate { get; set; }
        public List<ResultObjectLpg> Result { get; set; }
        public bool Success { get; set; }
    }

    public class ResultObjectLpg
    {
        public object Lpg { get; set; }   
        public object Marka { get; set; }
    }
}
