namespace Trainin_App_for_Repository.CQRS.Response.Query
{
    public class Station
    {
        public string SpecificName { get; set; }    
        public string Brand { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double LpgPrice { get; set; }
        public double GasolinePrice { get; set; }
        public double GasolineKatkiliPrice { get; set; }
        public double DieselPrice { get; set; }
        public double DieselKatkiliPrice { get; set; }
        public string LastUpdate { get; set; }
    }
}
