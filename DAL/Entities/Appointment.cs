namespace DAL.Entities;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int UserId { get; set; }

    public decimal Total { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public DateTime CreateDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<AppointmentDetail> AppointmentDetails { get; set; } = new List<AppointmentDetail>();

    public virtual User User { get; set; } = null!;
}
