using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FurnitureMarketBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceServer _authService;

        public AuthController(IAuthServiceServer authService) => _authService = authService;

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
        {
            var response = await _authService.Register(new User { Email = request.Email }, request.Password);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
        {
            var response = await _authService.Login(request.Email, request.Password);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /*
            * Метод на изменение пароля пользователя. Он требует аутентификации пользователя, 
              извлекает идентификатор пользователя из аутентификационных данных, вызывает 
              соответствующий сервис для изменения пароля пользователя, а затем возвращает ответ клиенту 
              в зависимости от результата операции.
        */
        [HttpPost("change-password"), Authorize] //  указывает на то, что данный метод принимает POST запрос на эндпоинт "change-password" и требует аутентификацию пользователя.
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPassword) // Входной параметр [FromBody] string newPassword указывает, что новый пароль будет отправлен в теле запроса в виде строки.
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // получается идентификатор пользователя (userId) из аутентификационных данных. ClaimTypes.NameIdentifier представляет идентификатор пользователя, который был включен в токен аутентификации.
            var response = await _authService.ChangePassword(int.Parse(userId), newPassword);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
