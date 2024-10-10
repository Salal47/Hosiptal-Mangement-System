using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments
{
    public class Appointment
    {
        public int id { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }

        public DateTime AppointmentDate { get; set; }

    }
}