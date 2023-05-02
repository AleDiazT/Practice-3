using UPB.CoreLogic.Models;
using System.Text.RegularExpressions;

namespace UPB.CoreLogic.Managers;

public class PatientManager
{
    private List<Patient> _patients;
    private String _path = "../listPatients.txt";
    public PatientManager()
    {
        _patients = loadPatients(_path);
    }
    public List<Patient> GetAll()
    {
        return _patients;
    }

    public Patient GetByCI(int ci)
    {
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        }
        Patient patientfound;
        patientfound = _patients.Find(patient => patient.CI == ci);
        if (patientfound == null)
        {
            throw new Exception("Patient not found");
        }
        return patientfound;
    }

    public Patient Update(int ci,string name, string lastname)
    {
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        }
        Patient patientfound;
        Patient updatedPatient;
        patientfound = _patients.Find(patient => patient.CI == ci);
        if (patientfound == null)
        {
            throw new Exception("Patient not found");
        }
        else if (letters(name) || letters(name))
        {
           throw new Exception("Invalid Name"); 
        }
        int patientfoundIndex = _patients.FindIndex(patient => patient.CI == ci);
        updatedPatient = patientfound;
        updatedPatient.Name = name;
        updatedPatient.LastName = lastname;
        _patients[patientfoundIndex] = updatedPatient;
        UpdatePatient(updatedPatient);
        return updatedPatient;
    }

    public Patient Create(string name, string lastname, int ci)
    {
        string bloodtype = RndBloodType();
        if(ci < 0)
        {
            throw new Exception("Invalid CI");
        }
        else if (_patients.Any(p => p.CI == ci))
        {
            throw new Exception("CI saved with other name");
        }
        else if (letters(name) || letters(name))
        {
           throw new Exception("Invalid Name"); 
        }
        Patient createPatient = new Patient()
        {
            Name = capitalize(name), 
            LastName = capitalize(lastname), 
            CI = ci, 
            Btype = bloodtype
        };
        _patients.Add(createPatient);
        savePatient(createPatient);
        return createPatient;
    }

    public Patient Delete(int ci)
    {
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        }
        int patientDeletedIndex = _patients.FindIndex(patient => patient.CI == ci);
        if (patientDeletedIndex == -1)
        {
            throw new Exception("Patient not found");
        }
        Patient patientDeleted = _patients[patientDeletedIndex];
        _patients.RemoveAt(patientDeletedIndex);
        DeletePatient(patientDeleted);
        return patientDeleted;
    }


    // Funciones Auxiliares

    public static String RndBloodType()
    {
        Random rand = new Random();
        int bloodtype = rand.Next(4);
        int rh = rand.Next(2);
        string bloodtypeString = "";
        
        switch (bloodtype)
        {
            case 0:
                bloodtypeString = "o";
                break;
            case 1:
                bloodtypeString = "a";
                break;
            case 2:
                bloodtypeString = "b";
                break;
            case 3:
                bloodtypeString = "ab";
                break;
        }
        string rhString = rh == 0 ? "+" : "-";
        return bloodtypeString + rhString;

    }
    public static String capitalize(String palabra)
    {
        if (string.IsNullOrEmpty(palabra))
        {
            return palabra;
        }
        string firstChar = palabra.Substring(0, 1).ToUpper();
        string restOfStr = palabra.Substring(1).ToLower();
        
        return firstChar + restOfStr;
    }
    public static bool letters(string str)
    {
        return !Regex.IsMatch(str, "^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+$");
    }
    public static List<Patient> loadPatients(string filePath)
    {
        List<Patient> patients = new List<Patient>();
        if (File.Exists(filePath))
        {
            StreamReader reader = new StreamReader(filePath);
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    string name = parts[0];
                    string lastName = parts[1];
                    int ci = int.Parse(parts[2]);
                    string bloodType = parts[3];
                    Patient patient = new Patient()
                    {Name = name, 
                    LastName = lastName,
                    CI = ci, 
                    Btype = bloodType};
                    patients.Add(patient);
                }
            }
            reader.Close();
        }
        return patients;
    }
    public void savePatient(Patient patient)
    {
        StreamWriter writer = new StreamWriter(_path, true);
        {
            writer.WriteLine($"{patient.Name},{patient.LastName},{patient.CI},{patient.Btype}");
        }
        writer.Close();
    }
    public void UpdatePatient(Patient patientUpdate)
    {
        List<Patient> updatedPatients = new List<Patient>();

        using (StreamReader reader = new StreamReader(_path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] patientData = line.Split(';');
                int currentPatientCI = int.Parse(patientData[2]);
                if (currentPatientCI == patientUpdate.CI)
                {
                    updatedPatients.Add(patientUpdate);
                }
                else
                {
                    Patient currentPatient = new Patient()
                    {
                        CI = currentPatientCI,
                        Name = patientData[0],
                        LastName = patientData[1],
                        Btype = patientData[3]
                    };
                    updatedPatients.Add(currentPatient);
                }
            }
        }
        using (StreamWriter writer = new StreamWriter(_path, false))
        {
            foreach (Patient patient in updatedPatients)
            {
                writer.WriteLine($"{patient.Name},{patient.LastName},{patient.CI},{patient.Btype}");
            }
        }
    }
    public void DeletePatient(Patient patientDelete)
    {
        List<Patient> updatedPatients = new List<Patient>();

        using (StreamReader reader = new StreamReader(_path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] patientData = line.Split(';');
                int currentPatientCI = int.Parse(patientData[2]);
                if (currentPatientCI != patientDelete.CI)
                {
                    Patient currentPatient = new Patient()
                    {
                        CI = currentPatientCI,
                        Name = patientData[0],
                        LastName = patientData[1],
                        Btype = patientData[3]
                    };
                    updatedPatients.Add(currentPatient);
                }     
            }
        }
        using (StreamWriter writer = new StreamWriter(_path, false))
        {
            foreach (Patient patient in updatedPatients)
            {
                writer.WriteLine($"{patient.Name},{patient.LastName},{patient.CI},{patient.Btype}");
            }
        }
    }
}