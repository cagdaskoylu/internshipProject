using System.Collections.Generic;

namespace Trainin_App_for_Repository.Data.DTO.Brand
{
    public class BrandGetGasolineInfoDto
    {
        public string Lastupdate { get; set; }
        public List<ResultObjectGasoline> Result { get; set; }
        public bool Success { get; set; }
    }

    public class ResultObjectGasoline
    {
        public object Marka { get; set; }
        public object Benzin { get; set; }
        public object Katkili { get; set; }
    }
}
