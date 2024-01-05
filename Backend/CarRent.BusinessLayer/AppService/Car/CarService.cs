using CarRent.DataAccessLayer.Model;
using CarRent.DataAccessLayer.Repository.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.BusinessLayer.AppService.CarService
{
    public class CarService :ICarService
    {
        private readonly ICarRepository _carRepository;
        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task AddCarAsync(CarModel car)
        {
            await _carRepository.AddCarAsync(car);

        }

        public async Task EditCarAsync(CarModel car)
        {
            await _carRepository.EditCarAsync(car);
        }
        public async Task DeleteCarAsync(int id)
        {
            await _carRepository.DeleteCarAsync(id);
        }

        public CarModel GetCarById(int id)
        {
            return _carRepository.GetCarById(id);
        }

        public async Task<IEnumerable<CarModel>> GetAllCars()
        {
            return await _carRepository.GetAllCars();
        }

        public int CountTotalCar()
        {
            return _carRepository.CountTotalCar();
        }
        public async Task<IEnumerable<CarModel>> FilterByMaker(string Maker)
        {
            return await _carRepository.FilterByMaker(Maker);
        }
        public async Task<IEnumerable<CarModel>> FilterByCost(int Cost)
        {
            return await _carRepository.FilterByCost(Cost); 
        }
        public async Task<IEnumerable<CarModel>> FilterByModel(string Model)
        {
            return await _carRepository.FilterByModel(Model);
        }

    }
}
