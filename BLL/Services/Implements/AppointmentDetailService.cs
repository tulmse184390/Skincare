using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implements
{
    public class AppointmentDetailService : IAppointmentDetailService
    {
        private IAppoitmentDetailRepository appointmentRepository;

        public AppointmentDetailService()
        {
            appointmentRepository = new AppoitmentDetailRepository();
        }

        public async Task<AppointmentDetail> AddAppointmentDetailAsync(AppointmentDetail appointmentDetail)
        {
            return await appointmentRepository.AddAppointmentDetailAsync(appointmentDetail);
        }
    }
}
