using System.Collections.Generic;

namespace Trainin_App_for_Repository.Data.DTO
{
    public class StationDto
    {
        public List<string> html_attributions { get; set; }
        public List<ResultObjectStation> Results { get; set; }
        public string Status { get; set; }  

    }

    public class ResultObjectStation
    {
        public Geometry Geometry { get; set; }  
        public string Name { get; set; }   
        public string place_id { get; set; }   
        
    }

    public class Geometry
    {
        public Location Location { get; set; }  
        public Viewport Viewport { get; set; }  
    }

    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; } 
    }
    public class Viewport
    {
        public Norteast Norteast { get; set; }
        public Soutwest Soutwest { get; set; }
    }

    public class Soutwest
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Norteast
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
