using Appointments;
using Microsoft.Data.SqlClient;
using Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation
{
    public class ValidationChecks
    {
        public static bool isEmailValid(string email)
        {
            if (email == null)
            {
                return false;
            }
            else if (email.IndexOf('@') == -1)
            {
                return false;
            }
            return true;
        }

        public static bool isNameValid(string name)
        {
            if (name == null)
            {
                return false;
            }
            else if (name == "")
            {
                return false;
            }
            return true;
        }

        public static bool isDateTimeInValidFormat(String s1)
        {
            DateTime p;
            bool valid = DateTime.TryParse(s1, out p);
            return valid;
        }

        public static bool vaildAppointmentTime(DateTime dt)
        {
            DateTime s1 = DateTime.Now;
            if (dt > s1)
            {
                return true;
            }
            return false;
        }

        public static bool isUniquePatientId(int id)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            string Query = "select PatientId from Patient";
            SqlCommand commad = new SqlCommand(Query, conn);

            SqlDataReader read = commad.ExecuteReader();

            while (read.Read())
            {
                if (read.GetInt32(0) == id)
                {

                    return false;
                }
            }

            conn.Close();
            return true;
        }

        public static bool isUniqueDoctorId(int id)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            string Query = "select DoctorID from Doctor";
            SqlCommand commad = new SqlCommand(Query, conn);

            SqlDataReader read = commad.ExecuteReader();

            while (read.Read())
            {
                if (read.GetInt32(0) == id)
                {
                    return false;
                }
            }

            conn.Close();
            return true;
        }

        public static bool isUniqueAppointmentsID(int id)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            string Query = "select AppointmentID from Appointments";
            SqlCommand commad = new SqlCommand(Query, conn);

            SqlDataReader read = commad.ExecuteReader();

            while (read.Read())
            {
                if (read.GetInt32(0) == id)
                {
                    return false;
                }
            }

            conn.Close();
            return true;
        }

        public static bool isAppointmentTimeUnqiue(DateTime s1, int DoctorsID)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            string Query = "select appointmentDate , DoctorId from Appointments";
            SqlCommand commad = new SqlCommand(Query, conn);

            SqlDataReader read = commad.ExecuteReader();

            while (read.Read())
            {
                if (read.GetDateTime(0) == s1 && read.GetInt16(1) == DoctorsID)
                {
                    return false;
                }
            }
            read.Close();
            conn.Close();
            return true;
        }
    }
}
