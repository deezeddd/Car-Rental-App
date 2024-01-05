using CarRent.DataAccessLayer.Model;
using CarRent.DataAccessLayer.Repository.RentAgreement;

namespace CarRent.BusinessLayer.AppService.RentAgreement
{
    public interface IRentService
    {
        public Task AddRentAsync(RentalAgreementModel rent);
        public Task EditRentAsync(RentalAgreementModel rent);
        public Task DeleteRentAsync(int id);
        public RentalAgreementModel GetRentById(int id);
        public List<RentalAgreementModel> GetRentByEmail(string email);
        public Task<IEnumerable<RentalAgreementModel>> GetAllRents();
        public int CountTotalRents();
        public Task ReqForReturn(int id);
        public Task Inspected(int id);

    }
}