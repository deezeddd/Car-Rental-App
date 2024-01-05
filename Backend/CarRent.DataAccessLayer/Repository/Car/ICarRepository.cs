using CarRent.DataAccessLayer.Model;

namespace CarRent.DataAccessLayer.Repository.Car
{
    public interface ICarRepository
    {
        public Task AddCarAsync(CarModel car);
        public Task EditCarAsync(CarModel car);
        public Task DeleteCarAsync(int id);
        public CarModel GetCarById(int id);
        public Task<IEnumerable<CarModel>> GetAllCars();
        public int CountTotalCar();
        public  Task<IEnumerable<CarModel>> FilterByCost(int Cost);
        public Task<IEnumerable<CarModel>> FilterByModel(string Model);
        public Task<IEnumerable<CarModel>> FilterByMaker(string Maker);



    }

}