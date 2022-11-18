using System.Collections.Generic;

namespace Trainin_App_for_Repository.Data.DTO
{
    public class PlaceDto
    {
        public List<ResultObjectPlace> Results { get; set; }
        public string Status { get; set; }  
    }

    public class ResultObjectPlace
    {
        public List<address_components> address_components { get; set; }
        public GeometryObject Geometry { get; set; }
    }

    public class GeometryObject
    {
        public Bounds Bounds { get; set; }
        public Location Location { get; set; }
        public string location_type { get; set; }
        public Viewport Viewport { get; set; }
    }

    public class Bounds
    {
        public Norteast Norteast { get; set; }
        public Soutwest Soutwest { get; set; }
    }
    public class address_components
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> Types { get; set; }   
    }
}
