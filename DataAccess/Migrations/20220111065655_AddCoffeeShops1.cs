using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddCoffeeShops1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                $"insert into CoffeeShops (Name, OpeningHours, Address) values('Tim''1 Coffee','9-5 Mon-Sat', '9079 West Locust St.Buffalo, NY 14221')"
                );
            migrationBuilder.Sql(
                $"insert into CoffeeShops (Name, OpeningHours, Address) values('Tim''2 Coffee','9-5 Mon-Sat', '9080 West Locust St.Buffalo, NY 14221')"
                );
            migrationBuilder.Sql(
                $"insert into CoffeeShops (Name, OpeningHours, Address) values('Tim''3 Coffee','9-5 Mon-Sat', '9081 West Locust St.Buffalo, NY 14221')"
                );
            migrationBuilder.Sql(
                $"insert into CoffeeShops (Name, OpeningHours, Address) values('Tim''4 Coffee','9-5 Mon-Sat', '9082 West Locust St.Buffalo, NY 14221')"
                );
            migrationBuilder.Sql(
                $"insert into CoffeeShops (Name, OpeningHours, Address) values('Tim''5 Coffee','9-5 Mon-Sat', '9083 West Locust St.Buffalo, NY 14221')"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
