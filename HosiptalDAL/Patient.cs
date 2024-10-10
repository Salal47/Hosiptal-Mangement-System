namespace Patients
{
    public class Patient
    {
        int PatientId;
        public int patientId
        {
            get { return PatientId; }
            set { PatientId = value; }
        }

        string Name;
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        string Email;
        public string email
        {
            get { return Email; }
            set { Email = value; }
        }

        string Disease;
        public string disease
        {
            get { return Disease; }
            set { Disease = value; }
        }
    }
}
