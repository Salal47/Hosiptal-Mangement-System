using Doctors;
using HistoryLogger;
using Microsoft.Data.SqlClient;
using Patients;
using System.Numerics;

namespace PatientDataAccess
{
    public class PatientData
    {
        public static void InsertPatient(Patient pat)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();
            try
            {

                string Query = $"INSERT INTO Patient VALUES(@name, @email ,@disease)";

                SqlCommand commad = new SqlCommand(Query, conn);

                commad.Parameters.AddWithValue("name", pat.name);
                commad.Parameters.AddWithValue("email", pat.email);
                commad.Parameters.AddWithValue("disease", pat.disease);

                commad.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        public static List<Patient> GetAllPatientsFromDatabase()
        {
            List<Patient> PatientList = new List<Patient>();

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();

            string Query = "select * from Patient";
            SqlCommand commad = new SqlCommand(Query, conn);

            SqlDataReader read = commad.ExecuteReader();

            while (read.Read())
            {
                Patient pat = new Patient { patientId = read.GetInt32(0), name = read.GetString(1), email = read.GetString(2), disease = read.GetString(3) };
                PatientList.Add(pat);
            }

            conn.Close();
            return PatientList;
        }
        public static void UpdatePatientInDatabase(Patient pat)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();
            try
            {
                string query = $"UPDATE Patient SET Name = @name ,Email = @email ,Disease = @disease  WHERE PatientId = @id";

                SqlCommand commad = new SqlCommand(query, conn);

                commad.Parameters.AddWithValue("id", pat.patientId);
                commad.Parameters.AddWithValue("name", pat.name);
                commad.Parameters.AddWithValue("email", pat.email);
                commad.Parameters.AddWithValue("disease", pat.disease);

                commad.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        public static void DeletePatientFromDatabase(int patientId)
        {

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();
            try
            {
                List<Patient> patientList = GetAllPatientsFromDatabase();
                int i = 0;
                while (patientList[i].patientId != patientId)
                {
                    i++;
                }
                History.addInHistoryOfPatient(patientList[i]);

                string query = $"delete from Patient where PatientId= @id";
                SqlCommand commad = new SqlCommand(query, conn);
                commad.Parameters.AddWithValue("id", patientId);
                commad.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public static List<Patient> SearchPatientsInDatabase(string name)
        {
            List<Patient> PatientList = new List<Patient>();

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDataBase;Integrated Security=True;");
            conn.Open();
            try
            {
                string Query = $"select * from Patient where name = @name ";
                SqlCommand commad = new SqlCommand(Query, conn);

                commad.Parameters.AddWithValue("name", name);

                SqlDataReader read = commad.ExecuteReader();

                while (read.Read())
                {
                    Patient pat = new Patient { patientId = read.GetInt32(0), name = read.GetString(1), email = read.GetString(2), disease = read.GetString(3) };
                    PatientList.Add(pat);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return PatientList;
        }
    }
}
