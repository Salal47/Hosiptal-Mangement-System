using Appointments;
using Doctors;
using Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HistoryLogger
{
    public class History
    {
        public static void addInHistoryOfPatient(Patient pat)
        {
            string json = JsonSerializer.Serialize(pat);
            StreamWriter writer = new StreamWriter("PatientHistory.txt", append: true);
            writer.WriteLine(json);
            writer.Close();
        }

        public static void addInHistoryOfDoctor(Doctor doc)
        {
            string json = JsonSerializer.Serialize(doc);
            StreamWriter writer = new StreamWriter("DoctorHistory.txt", append: true);
            writer.WriteLine(json);
            writer.Close();
        }

        public static void addInHistoryOfAppointments(Appointment app)
        {
            string json = JsonSerializer.Serialize(app);
            StreamWriter writer = new StreamWriter("AppointmentHistory.txt", append: true);
            writer.WriteLine(json);
            writer.Close();
        }

        public static List<Patient> getPatientHistory()
        {
            List<Patient> patientList = new List<Patient>();
            StreamReader reader = new StreamReader("PatientHistory.txt");
            try
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Patient pat = JsonSerializer.Deserialize<Patient>(line);
                    patientList.Add(pat);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                reader.Close();
            }
            return patientList;
        }

        public static List<Doctor> getDoctorHistory()
        {
            List<Doctor> doctorList = new List<Doctor>();
            StreamReader reader = new StreamReader("DoctorHistory.txt");
            try
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Doctor doc = JsonSerializer.Deserialize<Doctor>(line);
                    doctorList.Add(doc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                reader.Close();
            }
            return doctorList;
        }

        public static List<Appointment> getAppointmentHistory()
        {
            List<Appointment> appointmentList = new List<Appointment>();
            StreamReader reader = new StreamReader("AppointmentHistory.txt");
            try
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Appointment app = JsonSerializer.Deserialize<Appointment>(line);
                    appointmentList.Add(app);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                reader.Close();
            }
            return appointmentList;
        }
    }
}
