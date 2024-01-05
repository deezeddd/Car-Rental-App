using CarRent.DataAccessLayer.Model;
using CarRent.DataAccessLayer.Repository.Car;
using CarRent.DataAccessLayer.Repository.RentAgreement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.BusinessLayer.AppService.RentAgreement
{
    public class RentService : IRentService
    {
        private readonly IRentRepository _rentRepository;
        public RentService(IRentRepository rentRepository)
        {
            _rentRepository = rentRepository;
        }

        public async Task AddRentAsync(RentalAgreementModel rent)
        {
            await _rentRepository.AddRentAsync(rent);

        }

        public async Task EditRentAsync(RentalAgreementModel rent)
        {
            await _rentRepository.EditRentAsync(rent);
        }
        public async Task DeleteRentAsync(int id)
        {
            await _rentRepository.DeleteRentAsync(id);
        }

        public RentalAgreementModel GetRentById(int id)
        {
            return _rentRepository.GetRentById(id);
        }
        public List<RentalAgreementModel> GetRentByEmail(string email)
        {
            return _rentRepository.GetRentByEmail(email);
        }

        public async Task<IEnumerable<RentalAgreementModel>> GetAllRents()
        {
            return await _rentRepository.GetAllRents();
        }

        public int CountTotalRents()
        {
            return _rentRepository.CountTotalRents();
        }

        public Task ReqForReturn(int id)
        {
            return _rentRepository.ReqForReturn(id);
        }
        public Task Inspected(int id)
        {
            return _rentRepository.Inspected(id);
        }

    }
}
