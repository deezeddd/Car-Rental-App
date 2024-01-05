using CarRent.BusinessLayer.AppService.CarService;
using CarRent.DataAccessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Car_Rental_API.Controllers
{
        [ApiController]
        [Route("api/")]
    public class CarController :Controller
    {
        private readonly ICarService _carService;
        

        public CarController(ICarService carService) 
        {
            _carService = carService;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("AddCar")]

        public async Task<IActionResult> AddCar([FromBody] CarModel car)
        {
            if (ModelState.IsValid)
            {
                await _carService.AddCarAsync(car);
                return Ok(new
                {
                    message = "Car Added Successfully"
                });
            }
            return BadRequest(ModelState);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("EditCar/{id}")]

        public async Task<IActionResult> EditCar([FromRoute] int id, [FromBody] CarModel car)
        {
            if (ModelState.IsValid)
            {
                var existingCar = _carService.GetCarById(id);
                if (existingCar == null)
                {
                    return BadRequest(new
                    {
                        message = "Car doesn't exist"
                    });

                }
                existingCar.Maker = car.Maker;
                existingCar.Model = car.Model;
                existingCar.Cost = car.Cost;

                await _carService.EditCarAsync(existingCar);
                return Ok(new
                {
                    existingCar,
                    message = "Car Updated Successfully"
                });

            }
            { return BadRequest(ModelState); }

        }



        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCar/{id}")]

        public async Task<IActionResult> DeleteCar([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                var car = _carService.GetCarById(id);
                if (car != null)
                {
                    await _carService.DeleteCarAsync(id);
                    return Ok(new
                    {
                        car,
                        message = "Car deleted Successfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        message = "Car doesn't exist"
                    });
                }
            }
            { return BadRequest(ModelState); }

        }

        [HttpGet("GetCarById/{id}")]
        public IActionResult GetCarById([FromRoute] int id)
        {
            var car = _carService.GetCarById(id);
            if (car != null)
            {
                return Ok(car);
            }
            return BadRequest(new
            {
                message = "Car Doesn't Exist"
            });
        }

        [HttpGet("GetAllCars")]

        public async Task<IActionResult> GetAllCars()
        {
            var carList = await _carService.GetAllCars();
            var count = _carService.CountTotalCar();
            if (count == 0)
            {
                return BadRequest(new
                {
                    message = "No Car Available"
                });
            }
            return Ok(carList);
        }

        [HttpGet("FilterByMaker")]

        public async Task<IActionResult> FilterByMaker(string Maker)
        {
            var carList = await _carService.FilterByMaker(Maker);
            if(carList == null)
            {
                return NotFound();
            }
            return Ok(carList);
        }


        [HttpGet("FilterByModel")]

        public async Task<IActionResult> FilterByModel(string Model)
        {
            var carList = await _carService.FilterByModel(Model);
            if (carList == null)
            {
                return NotFound();
            }
            return Ok(carList);
        }


        [HttpGet("FilterByCost")]

        public async Task<IActionResult> FilterByCost(int Cost)
        {
            var carList = await _carService.FilterByCost(Cost);
            if (carList == null)
            {
                return NotFound();
            }
            return Ok(carList);
        }





    }
}
