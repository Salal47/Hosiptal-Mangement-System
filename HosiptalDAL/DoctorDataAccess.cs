using Microsoft.Data.SqlClient;
using Doctors;
using System.Collections.Generic;
using System.ComponentModel;
using HistoryLogger;

namespace DoctorDataAccess
{
    public class DoctorData
    {
        public static void InsertDoctor(Doctor doc)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            string Query = $"INSERT INTO Doctor VALUES(@name, @Specialization)";

            SqlCommand commad = new SqlCommand(Query, conn);
            commad.Parameters.AddWithValue("name", doc.Name);
            commad.Parameters.AddWithValue("Specialization", doc.Specialization);

            commad.ExecuteNonQuery();

            conn.Close();

        }
        public static List<Doctor> GetAllDoctorsFromDatabase()
        {
            List<Doctor> DoctorsList = new List<Doctor>();

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            string Query = "select * from Doctor";
            SqlCommand commad = new SqlCommand(Query, conn);

            SqlDataReader read = commad.ExecuteReader();

            while (read.Read())
            {
                Doctor Doctor = new Doctor { Id = read.GetInt32(0), Name = read.GetString(1), Specialization = read.GetString(2) };
                DoctorsList.Add(Doctor);
            }

            conn.Close();
            return DoctorsList;
        }

        public static void DeleteDoctorFromDatabase(int doctorId)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            List<Doctor> doctorsList = GetAllDoctorsFromDatabase();
            int i = 0;
            while (i < doctorsList.Count)
            {
                if (doctorId == doctorsList[i].Id)
                {
                    break;
                }
                i++;
            }
            History.addInHistoryOfDoctor(doctorsList[i]);

            string query = $"delete from doctor where DoctorID = @id";
            SqlCommand commad = new SqlCommand(query, conn);
            commad.Parameters.AddWithValue("id", doctorId);

            commad.ExecuteNonQuery();

            conn.Close();
        }

        public static List<Doctor> SearchDoctorsInDatabase(string specialization)
        {
            List<Doctor> DoctorsList = new List<Doctor>();

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            string Query = $"select * from Doctor where Specialization =@speci";
            SqlCommand commad = new SqlCommand(Query, conn);
            commad.Parameters.AddWithValue("speci", specialization);
            SqlDataReader read = commad.ExecuteReader();

            while (read.Read())
            {
                Doctor Doctor = new Doctor { Id = read.GetInt32(0), Name = read.GetString(1), Specialization = read.GetString(2) };
                DoctorsList.Add(Doctor);
            }

            conn.Close();
            return DoctorsList;
        }

        public static void UpdateDoctorInDatabase(Doctor doctor)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            string query = $"UPDATE doctor SET name = @name , Specialization = @spcei WHERE DoctorID = @id";
            SqlCommand commad = new SqlCommand(query, conn);
            commad.Parameters.AddWithValue("name", doctor.Name);
            commad.Parameters.AddWithValue("spcei", doctor.Specialization);
            commad.Parameters.AddWithValue("id", doctor.Id);
            commad.ExecuteNonQuery();

            conn.Close();
        }
    }
}