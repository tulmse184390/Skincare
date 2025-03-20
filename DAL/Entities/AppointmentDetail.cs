namespace DAL.Entities;

public partial class AppointmentDetail
{
    public int AppointmentDetailId { get; set; }

    public int AppointmentId { get; set; }

    public int ServiceId { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
