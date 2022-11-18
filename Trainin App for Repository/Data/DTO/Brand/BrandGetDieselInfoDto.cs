using System;
using System.Collections.Generic;

namespace Trainin_App_for_Repository.Data.DTO.Brand
{
    public class BrandGetDieselInfoDto
    {
        public string Lastupdate { get; set; }
        public List<ResultObjectDiesel> Result { get; set; }
        public bool Success { get; set; }
    }

    public class ResultObjectDiesel
    {
        public object Marka { get; set; }
        public object Dizel { get; set; }
        public object Katkili { get; set; }
    }
}
