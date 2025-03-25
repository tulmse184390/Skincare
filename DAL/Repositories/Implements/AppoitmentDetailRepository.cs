using DAL.Datas;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
    public class AppoitmentDetailRepository : IAppoitmentDetailRepository
    {
        private SkinCareContext dbContext;

        public AppoitmentDetailRepository()
        {
            dbContext = new();
        }

        public async Task<AppointmentDetail> AddAppointmentDetailAsync(AppointmentDetail appointmentDetail)
        {
            await dbContext.AppointmentDetails.AddAsync(appointmentDetail);
            await dbContext.SaveChangesAsync();

            return appointmentDetail;
        }
    }
}
