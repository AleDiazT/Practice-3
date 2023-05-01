using Microsoft.AspNetCore.Mvc;
using UPB.PracticeThree.Models;
using UPB.PracticeThree.Managers;

namespace UPB.PracticeThree.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private PatientManager _patientManager;
    public PatientController()
    {       
        _patientManager = new PatientManager();
    }

    [HttpGet]
    public List<Patient> Get()
    {
        return _patientManager.GetAll();
    }
    [HttpGet]
    [Route("{CI}")]
    public Patient GetByCI([FromRoute] int CI)
    {
        return _patientManager.GetByCI(CI);
    }
    [HttpPut]
    [Route("{CI}")]
    public Patient Put([FromRoute] int CI)
    {
        return _patientManager.Update(CI);
    }
    [HttpPost]
    public Patient Post()
    {
        return _patientManager.Create();
    }
    [HttpDelete]
    public Patient Delete(int CI)
    {
        return _patientManager.Delete(CI);;
    }
}
