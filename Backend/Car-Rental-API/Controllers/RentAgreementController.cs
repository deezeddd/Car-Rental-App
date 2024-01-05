using CarRent.BusinessLayer.AppService.CarService;
using CarRent.BusinessLayer.AppService.RentAgreement;
using CarRent.DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class RentAgreementController : Controller
    {
        private readonly IRentService _rentService;
        public RentAgreementController(IRentService rentService)
        {
               _rentService = rentService;
        }

        [HttpPost("AddRent")]
        public async Task<IActionResult> AddRent([FromBody] RentalAgreementModel rent)
        {
            if (ModelState.IsValid)
            {
                await _rentService.AddRentAsync(rent);
                return Ok(new
                {
                    message = "Rent Added Successfully"
                });
            }
            return BadRequest(ModelState);

        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("EditRent/{id}")]

        public async Task<IActionResult> EditRent([FromRoute] int id, [FromBody] RentalAgreementModel rent)
        {
            if (ModelState.IsValid)
            {
                var existingRent = _rentService.GetRentById(id);
                if (existingRent == null)
                {
                    return BadRequest(new
                    {
                        message = "Rent doesn't exist"
                    });

                }
                existingRent.RentalCost = rent.RentalCost;
                existingRent.EndDate = rent.EndDate;
                existingRent.TotalCost = rent.TotalCost;


                await _rentService.EditRentAsync(existingRent);
                return Ok(new
                {
                    existingRent,
                    message = "Rent Updated Successfully"
                });

            }
            { return BadRequest(ModelState); }

        }



        //[Authorize(Roles = "Admin")]
        [HttpDelete("DeleteRent/{id}")]

        public async Task<IActionResult> DeleteRent([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                var rent = _rentService.GetRentById(id);
                if (rent != null)
                {
                    await _rentService.DeleteRentAsync(id);
                    return Ok(new
                    {
                        rent,
                        message = "Rent deleted Successfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        message = "rent doesn't exist"
                    });
                }
            }
            { return BadRequest(ModelState); }

        }

        [HttpGet("GetRentById/{id}")]
        public IActionResult GetRentById([FromRoute] int id)
        {
            var rent = _rentService.GetRentById(id);
            if (rent != null)
            {
                return Ok(rent);
            }
            return BadRequest(new
            {
                message = "Rent Doesn't Exist"
            });
        }

        [HttpGet("GetRentByEmail")]
        public IActionResult GetRentByEmail(string email)
        {
            var rent = _rentService.GetRentByEmail(email);
            if (rent != null)
            {
                return Ok(rent);
            }
            else
            {
                return BadRequest(new
                {
                    message = "No Agreement Found"
                });
            }
          
        }

        [HttpGet("GetAllRents")]

        public async Task<IActionResult> GetAllRents()
        {
            var rentList = await _rentService.GetAllRents();
            var count = _rentService.CountTotalRents();
            if (count == 0)
            {
                return BadRequest(new
                {
                    message = "No Rent Available"
                });
            }
            return Ok(rentList);
        }


        [HttpPut("ReqForReturn")]

        public async Task<IActionResult> ReqForReturn(int id)
        {

          await _rentService.ReqForReturn(id);
            return Ok(new
            {
                message = "ReqForReturn -> API Success Ran"
            });
        }

        [HttpPut("Inspection")]

        public async Task<IActionResult> Inspected(int id)
        {
          await  _rentService.Inspected(id);
            return Ok(new
            {
                message = "Inspected -> API Success"
            });

        }


    }
}
