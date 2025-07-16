[ApiController]
[Route("[controller]")]
public class UsersController(
    IMapper mapper) : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterRecruiter([FromBody] RegisterRecruiterUserModel registerRecruiterUserModel)
    {
        console.writeline("HELLO");
    {

}
