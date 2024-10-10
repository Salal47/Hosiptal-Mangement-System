using appointmentDataAccess;
using Appointments;
using DoctorDataAccess;
using Doctors;
using HistoryLogger;
using PatientDataAccess;
using Patients;
using System.Collections.Generic;
using System.IO;
using Validation;

namespace Menu
{
    public class MenuClass
    {
        public static void Menu()
        {
            int exist = 0;
            try
            {
                while (exist != 16)
                {
                    Console.WriteLine("Menu Options:");
                    Console.WriteLine("1. Add a new patient");
                    Console.WriteLine("2. Update a patient");
                    Console.WriteLine("3. Delete a patient (and save deleted record to history)");
                    Console.WriteLine("4. Search for patients by name");
                    Console.WriteLine("5. View all patients");
                    Console.WriteLine("6. Add a new doctor");
                    Console.WriteLine("7. Update a doctor");
                    Console.WriteLine("8. Delete a doctor (and save deleted record to history)");
                    Console.WriteLine("9. Search for doctors by specialization");
                    Console.WriteLine("10. View all doctors");
                    Console.WriteLine("11. Book an appointment");
                    Console.WriteLine("12. View all appointments");
                    Console.WriteLine("13. Search appointments by doctor or patient");
                    Console.WriteLine("14. Cancel an appointment (and save deleted appointment to history)");
                    Console.WriteLine("15. View history of deleted records (patients, doctors, or appointments)");
                    Console.WriteLine("16. Exit the application");
                    Console.Write("\nPlease select an option (1-16): ");
                    exist = int.Parse(Console.ReadLine());
                    if (exist == 1)
                    {
                        Patient pat = GetValidPatientInput();
                        PatientData.InsertPatient(pat);
                    }
                    else if (exist == 2)
                    {
                        Patient pat = GetValidPatientInputForUpdation();
                        PatientData.UpdatePatientInDatabase(pat);
                    }
                    else if (exist == 3)
                    {
                        int patientId;
                        do
                        {
                            Console.WriteLine("Enter id: ");
                            patientId = int.Parse(Console.ReadLine());

                            if (ValidationChecks.isUniquePatientId(patientId))
                            {
                                Console.WriteLine("Patient Id Does Not Exist!");
                            }
                        }
                        while (ValidationChecks.isUniquePatientId(patientId));
                        PatientData.DeletePatientFromDatabase(patientId);
                    }
                    else if (exist == 4)
                    {
                        string name;
                        Console.WriteLine("Enter Name : ");
                        name = Console.ReadLine();
                        List<Patient> list = PatientData.SearchPatientsInDatabase(name);
                        printPatients(list);
                    }
                    else if (exist == 5)
                    {
                        printPatients(PatientData.GetAllPatientsFromDatabase());
                    }
                    else if (exist == 6)
                    {
                        Doctor doctor = GetValidDoctorInput();
                        DoctorData.InsertDoctor(doctor);
                    }
                    else if (exist == 7)
                    {
                        Doctor update = GetValidDoctorInputForUpdation();
                        DoctorData.UpdateDoctorInDatabase(update);
                    }
                    else if (exist == 8)
                    {
                        int doctorId = 0;
                        do
                        {
                            Console.WriteLine("Enter Doctor Id");
                            doctorId = int.Parse(Console.ReadLine());
                            if (ValidationChecks.isUniqueDoctorId(doctorId))
                            {
                                Console.WriteLine("Record Does Not Exists");
                            }
                        }
                        while (ValidationChecks.isUniqueDoctorId(doctorId));
                        DoctorData.DeleteDoctorFromDatabase(doctorId);
                    }
                    else if (exist == 9)
                    {
                        string speci;
                        do
                        {
                            Console.WriteLine("Enter Specilization");
                            speci = Console.ReadLine();
                            if (!ValidationChecks.isNameValid(speci))
                            {
                                Console.WriteLine("Invalid Name");
                            }
                        }
                        while (!ValidationChecks.isNameValid(speci));
                        printDoctors(DoctorData.SearchDoctorsInDatabase(speci));
                    }
                    else if (exist == 10)
                    {
                        printDoctors(DoctorData.GetAllDoctorsFromDatabase());
                    }
                    else if (exist == 11)
                    {
                        Appointment appoint = GetValidDoctorAppointment();
                        appointmentData.InsertAppointment(appoint);
                    }
                    else if (exist == 12)
                    {
                        printAppointsments(appointmentData.GetAllAppointmentsFromDatabase());
                    }
                    else if (exist == 13)
                    {
                        int docID;
                        do
                        {
                            Console.WriteLine("Enter Doctor Id");
                            docID = int.Parse(Console.ReadLine());
                            if (ValidationChecks.isUniqueDoctorId(docID))
                            {
                                Console.WriteLine("Doctor Does Not Exits Enter Valid ID");
                            }
                        }
                        while (ValidationChecks.isUniqueDoctorId(docID));

                        int patientID;
                        do
                        {
                            Console.WriteLine("Enter Doctor Id");
                            patientID = int.Parse(Console.ReadLine());
                            if (ValidationChecks.isUniquePatientId(patientID))
                            {
                                Console.WriteLine("Patient Does Not Exist Enter Valid ID");
                            }
                        }
                        while (ValidationChecks.isUniquePatientId(patientID));

                        printAppointsments(appointmentData.SearchAppointmentsInDatabase(docID, patientID));

                    }
                    else if (exist == 14)
                    {
                        int appointID;
                        do
                        {
                            Console.WriteLine("Enter Oppointment ID");
                            appointID = int.Parse(Console.ReadLine());
                            if (ValidationChecks.isUniqueAppointmentsID(appointID))
                            {
                                Console.WriteLine("Patient Does Not Exist Enter Valid ID");
                            }
                        }
                        while (ValidationChecks.isUniqueAppointmentsID(appointID));

                        appointmentData.DeleteAppointmentFromDatabase(appointID);

                    }
                    else if (exist == 15)
                    {
                        int i;
                        Console.WriteLine("     1. Patient History");
                        Console.WriteLine("     2. Doctor History");
                        Console.WriteLine("     3. Appointment History");
                        Console.WriteLine("     4. Print overall History");
                        i = int.Parse(Console.ReadLine());
                        if (i == 2)
                        {
                            Console.WriteLine("----------------(Doctor History)-----------------");
                            if (File.Exists("DoctorHistory.txt"))
                            {
                                printDoctors(History.getDoctorHistory());
                            }
                            else
                            {
                                Console.WriteLine("No Data is Delete!");
                            }
                        }
                        else if (i == 1)
                        {
                            Console.WriteLine("----------------(Patient History)-----------------");
                            if (File.Exists("PatientHistory.txt"))
                            {
                                printPatients(History.getPatientHistory());
                            }
                            else
                            {
                                Console.WriteLine("No Data is Delete!");
                            }
                        }
                        else if (i == 3)
                        {
                            Console.WriteLine("----------------(Appointments History)-----------------");
                            if (File.Exists("AppointmentHistory.txt"))
                            {
                                printAppointsments(History.getAppointmentHistory());
                            }
                            else
                            {
                                Console.WriteLine("No Data is Delete!");
                            }
                        }
                        else if (i == 4)
                        {
                            Console.WriteLine("----------------(Doctor History)-----------------");
                            if (File.Exists("DoctorHistory.txt"))
                            {
                                printDoctors(History.getDoctorHistory());
                            }
                            else
                            {
                                Console.WriteLine("No Doctor Data is Delete!");
                            }
                            Console.WriteLine("----------------(Patient History)-----------------");
                            if (File.Exists("PatientHistory.txt"))
                            {
                                printPatients(History.getPatientHistory());
                            }
                            else
                            {
                                Console.WriteLine("No Patient Data is Delete!");
                            }
                            Console.WriteLine("----------------(Appintment History)-----------------");
                            if (File.Exists("AppointmentHistory.txt"))
                            {
                                printAppointsments(History.getAppointmentHistory());
                            }
                            else
                            {
                                Console.WriteLine("No Appointmenet Data is Delete!");
                            }
                        }
                    }
                    else if (exist == 16)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + "\"" + e.Message + "\"");
            }
        }
        public static Patient GetValidPatientInput()
        {

            //Get Unique id from User
            Patient pat = new Patient();

            //get Valid Name(should be not null);
            do
            {
                Console.WriteLine("Enter Name: ");
                pat.name = Console.ReadLine();
                if (!ValidationChecks.isNameValid(pat.name))
                {
                    Console.WriteLine("Name Cannot Null");
                }
                pat.name = pat.name.Trim();
            }
            while (!ValidationChecks.isNameValid(pat.name));

            //vaild Email(not null and having @)
            do
            {
                Console.WriteLine("Enter Email: ");
                pat.email = Console.ReadLine();

                if (!ValidationChecks.isEmailValid(pat.email))
                {
                    Console.WriteLine("Email is not in Correct Format");
                }

                pat.email = pat.email.Trim();
            }
            while (!ValidationChecks.isEmailValid(pat.email));

            //Vaild diesase name 
            do
            {
                Console.WriteLine("Enter Diesaes name: ");
                pat.disease = Console.ReadLine();

                pat.disease = pat.disease.Trim();
            }
            while (!ValidationChecks.isNameValid(pat.disease));

            return pat;

        }
        public static Patient GetValidPatientInputForUpdation()
        {

            //Get Unique id from User
            Patient pat = new Patient();
            do
            {
                Console.WriteLine("Enter id: ");
                pat.patientId = int.Parse(Console.ReadLine());

                if (ValidationChecks.isUniquePatientId(pat.patientId))
                {
                    Console.WriteLine("id Does Not Exists");
                }
            }
            while (ValidationChecks.isUniquePatientId(pat.patientId));

            //get Valid Name(should be not null);
            do
            {
                Console.WriteLine("Enter Name: ");
                pat.name = Console.ReadLine();
                if (!ValidationChecks.isNameValid(pat.name))
                {
                    Console.WriteLine("Name Cannot Null");
                }
                pat.name = pat.name.Trim();

            }
            while (!ValidationChecks.isNameValid(pat.name));

            //vaild Email(not null and having @)
            do
            {
                Console.WriteLine("Enter Email: ");
                pat.email = Console.ReadLine();
                pat.email = pat.email.Trim();

                if (!ValidationChecks.isEmailValid(pat.email))
                {
                    Console.WriteLine("Email is not in Correct Format");
                }

            }
            while (!ValidationChecks.isEmailValid(pat.email));

            //Vaild diesase name 
            do
            {
                Console.WriteLine("Enter Diesaes name: ");
                pat.disease = Console.ReadLine();
            }
            while (!ValidationChecks.isNameValid(pat.disease));

            return pat;

        }

        public static Doctor GetValidDoctorInput()
        {

            Doctor doc = new Doctor();
            //get Valid Name(should be not null);
            do
            {
                Console.WriteLine("Enter Name: ");
                doc.Name = Console.ReadLine();
                doc.Name = doc.Name.Trim();
                if (ValidationChecks.isNameValid(doc.Name))
                {
                    Console.WriteLine("Name can never null");
                }
            }
            while (!ValidationChecks.isNameValid(doc.Name));

            //vaild Speclization
            do
            {
                Console.WriteLine("Enter Specilization: ");
                doc.Specialization = Console.ReadLine();
                doc.Specialization = doc.Specialization.Trim();
            }
            while (!ValidationChecks.isNameValid(doc.Specialization));

            return doc;

        }


        public static Doctor GetValidDoctorInputForUpdation()
        {

            //Get Unique id from User
            Doctor doc = new Doctor();
            do
            {
                Console.WriteLine("Enter id: ");
                doc.Id = int.Parse(Console.ReadLine());
                if (ValidationChecks.isUniqueDoctorId(doc.Id))
                {
                    Console.WriteLine("Id not found!");
                }
            }
            while (ValidationChecks.isUniqueDoctorId(doc.Id));

            //get Valid Name(should be not null);
            do
            {
                Console.WriteLine("Enter Name: ");
                doc.Name = Console.ReadLine();
                doc.Name = doc.Name.Trim();
                if (ValidationChecks.isNameValid(doc.Name))
                {
                    Console.WriteLine("Name can never null");
                }
            }
            while (!ValidationChecks.isNameValid(doc.Name));

            //vaild Speclization
            do
            {
                Console.WriteLine("Enter Specilization: ");
                doc.Specialization = Console.ReadLine();
                doc.Specialization = doc.Specialization.Trim();
            }
            while (!ValidationChecks.isNameValid(doc.Specialization));

            return doc;

        }

        public static Appointment GetValidDoctorAppointment()
        {

            //Get Unique id from User
            Appointment app = new();

            //check is  doctor id Exist or not ?;
            do
            {
                Console.WriteLine("Enter DoctorId: ");
                app.doctorId = int.Parse(Console.ReadLine());
                if (ValidationChecks.isUniqueDoctorId(app.doctorId))
                {
                    Console.WriteLine("Doctor ID Does Not Exists");
                }
            }
            while (ValidationChecks.isUniqueDoctorId(app.doctorId));


            //check is Patient Exists or not
            do
            {
                Console.WriteLine("Enter Patient Id: ");
                app.patientId = int.Parse(Console.ReadLine());
                if (ValidationChecks.isUniquePatientId(app.patientId))
                {
                    Console.WriteLine("Patient Does Not Exits");
                }
            }
            while (ValidationChecks.isUniquePatientId(app.patientId));

            string dateString = " ";
            do
            {
                Console.WriteLine("Enter AppointmentDate(yyyy-mm-dd hh:mm:ss): ");
                dateString = Console.ReadLine();
                app.AppointmentDate = DateTime.Parse(dateString);

                if (!ValidationChecks.isAppointmentTimeUnqiue(app.AppointmentDate, app.doctorId))
                {
                    Console.WriteLine("Appointment Slot not Free");
                }
                else if (!ValidationChecks.vaildAppointmentTime(app.AppointmentDate))
                {
                    Console.WriteLine("Appointment Time is not Valid");
                }
                else if (!ValidationChecks.isDateTimeInValidFormat(dateString))
                {
                    Console.WriteLine("Date is not correct or in correct Format please Fellow ");
                    Console.WriteLine(" ---(yyyy-mm-dd hh:mm:ss)----- ");
                    Console.WriteLine(" OR ");
                    Console.WriteLine("Enter a Valid Date Time");
                }
            }
            while (!ValidationChecks.isAppointmentTimeUnqiue(app.AppointmentDate, app.doctorId) || !ValidationChecks.vaildAppointmentTime(app.AppointmentDate) || !ValidationChecks.isDateTimeInValidFormat(dateString));

            return app;
        }

        public static Appointment GetValidDoctorAppointmentForUpdation()
        {

            //Get Unique id from User
            Appointment app = new();
            do
            {
                Console.WriteLine("Enter id: ");
                app.id = int.Parse(Console.ReadLine());
                if (ValidationChecks.isUniqueAppointmentsID(app.id))
                {
                    Console.WriteLine("ID Already Exists");
                }
            }
            while (ValidationChecks.isUniqueAppointmentsID(app.id));

            //check is  doctor id Exist or not ?;
            do
            {
                Console.WriteLine("Enter DoctorId: ");
                app.doctorId = int.Parse(Console.ReadLine());
                if (ValidationChecks.isUniqueDoctorId(app.doctorId))
                {
                    Console.WriteLine("Doctor ID Does Not Exists");
                }
            }
            while (ValidationChecks.isUniqueDoctorId(app.doctorId));


            //check is Patient Exists or not
            do
            {
                Console.WriteLine("Enter Patient Id: ");
                app.doctorId = int.Parse(Console.ReadLine());
                if (ValidationChecks.isUniquePatientId(app.doctorId))
                {
                    Console.WriteLine("Patient Does Not Exits");
                }
            }
            while (ValidationChecks.isUniquePatientId(app.id));

            string dateString = " ";
            do
            {
                Console.WriteLine("Enter AppointmentDate(yyyy-mm-dd hh:mm:ss): ");
                dateString = Console.ReadLine();
                app.AppointmentDate = DateTime.Parse(dateString);

                if (!ValidationChecks.isAppointmentTimeUnqiue(app.AppointmentDate, app.doctorId))
                {
                    Console.WriteLine("Appointment Slot not Free");
                }
                else if (!ValidationChecks.vaildAppointmentTime(app.AppointmentDate))
                {
                    Console.WriteLine("Appointment Time is not Valid");
                }
                else if (!ValidationChecks.isDateTimeInValidFormat(dateString))
                {
                    Console.WriteLine("Date is not correct or in correct Format please Fellow ");
                    Console.WriteLine(" ---(yyyy-mm-dd hh:mm:ss)----- ");
                    Console.WriteLine(" OR ");
                    Console.WriteLine("Enter a Valid Date Time");
                }
            }
            while (!ValidationChecks.isAppointmentTimeUnqiue(app.AppointmentDate, app.doctorId) || !ValidationChecks.vaildAppointmentTime(app.AppointmentDate) || !ValidationChecks.isDateTimeInValidFormat(dateString));

            return app;
        }
        public static void printPatients(List<Patient> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Record Does Not Exists");
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("_____________________________________________");
                Console.WriteLine("Patient ID : " + list[i].patientId);
                Console.WriteLine("Patient Name : " + list[i].name);
                Console.WriteLine("Patient Email : " + list[i].email);
                Console.WriteLine("Patient Diseases : " + list[i].disease);
            }
            Console.WriteLine("_____________________________________________");
        }

        public static void printDoctors(List<Doctor> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Record Does Not Exists");
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("_____________________________________________");
                Console.WriteLine("Doctor ID : " + list[i].Id);
                Console.WriteLine("Doctor Name : " + list[i].Name);
                Console.WriteLine("Doctor Specialization : " + list[i].Specialization);
            }
            Console.WriteLine("_____________________________________________");
        }

        public static void printAppointsments(List<Appointment> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Record Does Not Exists");
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("_____________________________________________");
                Console.WriteLine("Appointment ID : " + list[i].id);
                Console.WriteLine("Doctor ID : " + list[i].doctorId);
                Console.WriteLine("Patient ID : " + list[i].patientId);
                Console.WriteLine("Doctor AppointmentDate : " + list[i].AppointmentDate);
            }
            Console.WriteLine("_____________________________________________");
        }
    }
}


