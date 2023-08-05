using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.Entities;
using System.Data.Entity;

namespace Palmfit.Core.Implementations
{
    public class FoodInterfaceRepository : IFoodInterfaceRepository
    {




        private readonly PalmfitDbContext _db;

        public FoodInterfaceRepository(PalmfitDbContext db)
        {
            _db = db;
        }

        public async Task<List<Food>> GetAllFoodAsync()
        {
            return await _db.Foods.ToListAsync();
        }

        //get food list by category
        public async Task<ICollection<FoodDto>> GetFoodByCategory(string id)
        {

            var getFoodData = await _db.Foods.Where(x => x.FoodClassId == id).ToListAsync();
            if (getFoodData.Count() == 0)
                return null;

            List<FoodDto> result = null;

            foreach (var food in getFoodData)
            {
                FoodDto newEntry = new()
                {
                    Name = food.Name,
                    Description = food.Description,
                    Details = food.Details,
                    Origin = food.Origin,
                    Image = food.Image,
                    Calorie = food.Calorie,
                    Unit = food.Unit,
                    FoodClassId = food.FoodClassId,
                };

                result.Add(newEntry);
            }

            return result;
        }

    }
}
