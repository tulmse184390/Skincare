using DAL.Entities;

namespace BLL.Services.Interfaces
{
    public interface IAppointmentDetailService
    {
        Task<AppointmentDetail> AddAppointmentDetailAsync(AppointmentDetail appointmentDetail);
    }
}
