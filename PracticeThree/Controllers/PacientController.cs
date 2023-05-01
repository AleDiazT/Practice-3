using Microsoft.AspNetCore.Mvc;

namespace UPB.PracticeThree.Controllers;

[ApiController]
[Route("[controller]")]
public class PacientController : ControllerBase
{
    public PacientController()
    {       
    }


    public IEnumerable<Pacient> Get()
    {
        return new List<Pacient>();
    }
}
