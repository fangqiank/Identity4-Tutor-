using API.Model;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Service
{
    public class CoffeeShopService : ICoffeeShopService
    {
        private readonly ApplicationDbContext _db;

        public CoffeeShopService(ApplicationDbContext db)
        {
           _db = db;
        }
        public async Task<List<CoffeeShopModel>> List()
        {
            var coffeeShops = await (from shop in _db.CoffeeShops select new CoffeeShopModel() { 
                Id = shop.Id,
                Name = shop.Name,
                OpeningHours = shop.OpeningHours,
                Address = shop.Address
            }).ToListAsync();

            return coffeeShops;
        }
    }
}
