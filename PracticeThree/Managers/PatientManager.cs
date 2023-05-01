using UPB.PracticeThree.Models;

namespace UPB.PracticeThree.Managers;

public class PatientManager
{
    private List<Patient> _patients;
    public PatientManager()
    {
        _patients = new List<Patient>();
    }
    public List<Patient> GetAll()
    {
        return new List<Patient>();
    }

    public Patient GetByCI(int ci)
    {
        return _patients.Find(patient => patient.CI == ci);
    }

    public Patient Update(int ci)
    {
        Patient patientfound = _patients.Find(patient => patient.CI == ci);
        patientfound.Name = "Cambiado";
        return patientfound;
    }

    public Patient Create(string name, string lastname, int ci)
    {
        string bloodtype = RndBloodType();
        Patient createPatient = new Patient()
        {
            Name = name, 
            LastName = lastname, 
            CI = ci, 
            Btype = bloodtype
        };
        _patients.Add(createPatient);
        return createPatient;
    }

    public Patient Delete(int ci)
    {
        int patientDeletedIndex = _patients.FindIndex(patient => patient.CI == ci);
        Patient patientDeleted = _patients[patientDeletedIndex];
        _patients.RemoveAt(patientDeletedIndex);
        return patientDeleted;
    }

    public String RndBloodType()
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
}