using CarRent.DataAccessLayer.Context;
using CarRent.DataAccessLayer.Migrations;
using CarRent.DataAccessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.DataAccessLayer.Repository.RentAgreement
{
    public class RentRepository : IRentRepository
    {
        private readonly AppDbContext _appDbContext;

        public RentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddRentAsync(RentalAgreementModel rent)
        {
            try
            {
                if (rent != null)
                {
                    var car = _appDbContext.CarDetails.FirstOrDefault(p => p.Id == rent.CarId);
                    if (car.Availability == "Available" && rent.StartDate< rent.EndDate)
                    {
                        rent.Requested = "Not Requested";

                        car.Availability = "Unavailable";
                        _appDbContext.RentAgreement.Add(rent);
                        await _appDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex) { };
        }
        [Authorize(Roles = "Admin")]

        public async Task DeleteRentAsync(int id)
        {
            try
            {
                var rent = _appDbContext.RentAgreement.FirstOrDefault(p => p.AgreementId == id);
                var car = _appDbContext.CarDetails.FirstOrDefault(p => p.Id == rent.CarId);
                if (rent != null)
                {
                    car.Availability = "Available";
                    _appDbContext.RentAgreement.Remove(rent);
                    await _appDbContext.SaveChangesAsync();
                }
            }
            catch(Exception ex) { };
        }
        [Authorize(Roles = "Admin")]

        public async Task EditRentAsync(RentalAgreementModel rent)
        {
            try
            {
                if (rent != null)
                {
                    _appDbContext.RentAgreement.Update(rent);
                    await _appDbContext.SaveChangesAsync();
                }
            }
            catch(Exception ex) { }

        }


        public RentalAgreementModel GetRentById(int id)
        {
            var rent = _appDbContext.RentAgreement.FirstOrDefault(p => p.AgreementId == id);
            if (rent == null)
            {
                return null;
            }
            return rent;
        }

        public List<RentalAgreementModel> GetRentByEmail(string email)
        {
            var rent = _appDbContext.RentAgreement.Where(p => p.Email == email).ToList();
            if (rent == null)
            {
                return null;
            }
            return rent;
        }
        [Authorize(Roles = "Admin")]

        public async Task<IEnumerable<RentalAgreementModel>> GetAllRents()
        {
            return await _appDbContext.RentAgreement.ToListAsync();
        }

        public int CountTotalRents()
        {
            return _appDbContext.RentAgreement.Count();
        }

        public async Task ReqForReturn(int id)
        {
            try
            {
                var rent = _appDbContext.RentAgreement.FirstOrDefault(p => p.AgreementId == id);
                if(rent != null )
                {
                    rent.Requested = "Requested";
                    _appDbContext.RentAgreement.Update(rent);
                    await _appDbContext.SaveChangesAsync();

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex) { }
        }
        [Authorize(Roles = "Admin")]

        public async Task Inspected(int id)
        {
            var rent = _appDbContext.RentAgreement.FirstOrDefault(p => p.AgreementId == id);
            
            if(rent.Requested != null &&rent.Requested == "Requested")
            {
            rent.Requested = "Approved";
            var car = _appDbContext.CarDetails.FirstOrDefault(p => p.Id == rent.CarId);
            car.Availability = "Available";
                _appDbContext.RentAgreement.Update(rent);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                return;
            }

        }

    }
}
