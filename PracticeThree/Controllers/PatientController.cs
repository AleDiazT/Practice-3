using Microsoft.AspNetCore.Mvc;
using UPB.CoreLogic.Models;
using UPB.CoreLogic.Managers;

namespace UPB.PracticeThree.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly PatientManager _patientManager;
    public PatientController(PatientManager patientManager)
    {       
        _patientManager = patientManager;
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
    public Patient Put([FromRoute] int CI,[FromBody]Patient patientToUpdate)
    {
        return _patientManager.Update(CI);
    }
    [HttpPost]
    public Patient Post([FromBody]Patient patientToCreate)
    {
        return _patientManager.Create(patientToCreate.Name,patientToCreate.LastName,patientToCreate.CI);
    }
    [HttpDelete]
    public Patient Delete(int CI)
    {
        return _patientManager.Delete(CI);;
    }
}
