using CarRent.DataAccessLayer.Context;
using CarRent.DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.DataAccessLayer.Repository.Car
{
    public class CarRepository :ICarRepository
    {
        private readonly AppDbContext _appDbContext;

        public CarRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddCarAsync(CarModel car)
        {
            if (car != null)
            {
                _appDbContext.CarDetails.Add(car);
                car.Availability = "Available";
                await _appDbContext.SaveChangesAsync();
            }
        }

           public async Task DeleteCarAsync(int id)
        {
            var car = _appDbContext.CarDetails.FirstOrDefault(p => p.Id == id);

            if (car != null)
            {
                _appDbContext.CarDetails.Remove(car);
                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task EditCarAsync(CarModel car)
        {
            if(car != null)
            {
                    _appDbContext.CarDetails.Update(car);
                await _appDbContext.SaveChangesAsync();
            }
            
        }


        public CarModel GetCarById(int id)
        {
            var car = _appDbContext.CarDetails.FirstOrDefault(p => p.Id == id);
            if (car == null)
            {
                return null;
            }
            return car;
        }

      
        public async Task <IEnumerable<CarModel>>  GetAllCars()
        {
            return await _appDbContext.CarDetails.ToListAsync();
        }

        public int CountTotalCar()
        {
            return _appDbContext.CarDetails.Count();
        }

        public async Task<IEnumerable<CarModel>> FilterByMaker(string Maker)
        {
            var car = await _appDbContext.CarDetails.Where(p => p.Maker == Maker).ToListAsync();
            return car;
        }
        public async Task<IEnumerable<CarModel>> FilterByModel(string Model)
        {
            var car = await _appDbContext.CarDetails.Where(p => p.Model == Model).ToListAsync();
            return car;
        }
        public async Task<IEnumerable<CarModel>> FilterByCost(int Cost)
        {
            var car = await _appDbContext.CarDetails.Where(p => p.Cost >= Cost).ToListAsync();
            return car;
        }
    }
}
