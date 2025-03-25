using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IAppoitmentDetailRepository
    {
        Task<AppointmentDetail> AddAppointmentDetailAsync(AppointmentDetail appointmentDetail);
    }
}
