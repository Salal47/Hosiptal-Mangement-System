using Appointments;
using Doctors;
using HistoryLogger;
using Microsoft.Data.SqlClient;
using Patients;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appointmentDataAccess
{
    public class appointmentData
    {
        public static void InsertAppointment(Appointment appo)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            try
            {

                string Query = $"INSERT INTO Appointments VALUES (@pId, @DId , @date);";

                SqlCommand commad = new SqlCommand(Query, conn);


                SqlParameter sqlpatient = new SqlParameter("pId", appo.patientId);
                SqlParameter sqldocID = new SqlParameter("Did", appo.doctorId); ;
                SqlParameter sqldate = new SqlParameter("date", appo.AppointmentDate);

                commad.Parameters.Add(sqlpatient);
                commad.Parameters.Add(sqldocID);
                commad.Parameters.Add(sqldate);

                commad.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Appointment> GetAllAppointmentsFromDatabase()
        {
            List<Appointment> appointmentList = new List<Appointment>();

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            string Query = "select * from Appointments";
            SqlCommand commad = new SqlCommand(Query, conn);

            SqlDataReader read = commad.ExecuteReader();

            while (read.Read())
            {
                Appointment pat = new Appointment { id = read.GetInt32(0), patientId = read.GetInt32(1), doctorId = read.GetInt32(2), AppointmentDate = read.GetDateTime(3) };
                appointmentList.Add(pat);
            }
            read.Close();
            conn.Close();
            return appointmentList;
        }
        public static void UpdateAppointmentInDatabase(Appointment appo)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();
            try
            {
                string query = $"UPDATE Patient SET PatientID= @pid ,DoctorID = @Did , appointmentDate= @date  WHERE AppointmentID = @id";
                SqlCommand commad = new SqlCommand(query, conn);

                commad.Parameters.AddWithValue("id", appo.id);
                commad.Parameters.AddWithValue("pId", appo.patientId);
                commad.Parameters.AddWithValue("Did", appo.doctorId);
                commad.Parameters.AddWithValue("date", appo.AppointmentDate);
                commad.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DeleteAppointmentFromDatabase(int appointmentId)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();
            try
            {
                List<Appointment> appList = new List<Appointment>();
                appList = GetAllAppointmentsFromDatabase();
                int i = 0;
                while (i < appList.Count)
                {
                    if (appList[i].id == appointmentId)
                    {
                        break;
                    }
                    i++;
                }
                History.addInHistoryOfAppointments(appList[i]);

                string query = $"delete from appointments where AppointmentID= @id";
                SqlCommand commad = new SqlCommand(query, conn);
                commad.Parameters.AddWithValue("id", appointmentId);

                commad.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Appointment> SearchAppointmentsInDatabase(int doctorId, int patientId)
        {
            List<Appointment> appointmentList = new List<Appointment>();

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();
            try
            {
                string Query = $"select * from Appointments where DoctorID  = @Did and PatientID = @pId";
                SqlCommand commad = new SqlCommand(Query, conn);
                commad.Parameters.AddWithValue("Did", doctorId);
                commad.Parameters.AddWithValue("pId", patientId);

                SqlDataReader read = commad.ExecuteReader();

                while (read.Read())
                {
                    Appointment pat = new Appointment { id = read.GetInt32(0), patientId = read.GetInt32(1), doctorId = read.GetInt32(2), AppointmentDate = read.GetDateTime(3) };
                    appointmentList.Add(pat);
                }
                read.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return appointmentList;
        }
    }
}
