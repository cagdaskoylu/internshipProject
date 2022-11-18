using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Data
{
    public class FuelPriceDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public FuelPriceDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<UsersEntity> User { get; set; }
        public virtual DbSet<AddressesEntity> Address { get; set; }
        public virtual DbSet<CarsEntity> Car { get; set; }
        public virtual DbSet<BrandsEntity> Brand { get; set; }
        //public virtual DbSet<StationsEntity> Station { get; set; }
        public virtual DbSet<CitiesEntity> City { get; set; }
        public virtual DbSet<DistrictsEntity> District { get; set; }
        public virtual DbSet<FavStationsEntity> FavStations { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "ankara" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "istanbul" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "izmir" });

            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "adana" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "adiyaman" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "afyonkarahisar" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "agri" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "amasya" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "antalya" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "artvin" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "aydin" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "balikesir" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "bilecik" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "bingol" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "bitlis" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "bolu" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "burdur" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "bursa" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "canakkale" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "cankiri" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "corum" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "denizli" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "diyarbakir" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "edirne" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "elazig" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "erzincan" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "erzurum" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "eskisehir" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "gaziantep" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "giresun" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "gumushane" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "hakkari" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "hatay" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "isparta" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "mersin" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "kars" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "kastamonu" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "kayseri" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "kirklareli" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "kirsehir" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "kocaeli" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "konya" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "kutahya" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "malatya" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "manisa" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "kahramanmaras" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "mardin" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "mugla" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "mus" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "nevsehir" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "nigde" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "ordu" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "rize" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "sakarya" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "samsun" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "siirt" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "sinop" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "sivas" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "tekirdag" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "tokat" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "trabzon" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "tunceli" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "sanliurfa" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "usak" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "van" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "yozgat" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "zonguldak" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "aksaray" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "bayburt" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "karaman" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "kirikkale" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "batman" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "sirnak" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "bartin" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "ardahan" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "igdir" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "yalova" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "karabuk" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "kilis" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "osmaniye" });
            //model.Entity<CitiesEntity>().HasData(new CitiesEntity() { Id = System.Guid.NewGuid(), Name = "duzce" });

            //Guid CityId = Guid.Parse("B37BA70D-B1C1-45FE-9D4E-FAAAF9A6418B");
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "akyurt" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "altindag" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "ayas" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "bala" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "beypazari" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "camlidere" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "cankaya" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "cubuk" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "elmadag" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "evren" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "golbasi" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "gudul" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "haymana" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "kahramankazan" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "kalecik" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "kecioren" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "kizilcahamam" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "mamak" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "nallihan" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "polatli" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "pursaklar" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "sincan" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "sereflikochisar" });
            //model.Entity<DistrictsEntity>().HasData(new DistrictsEntity() { Id = System.Guid.NewGuid(), CitiesEntityId = CityId, Name = "yenimahalle" });

        }

    }
}
