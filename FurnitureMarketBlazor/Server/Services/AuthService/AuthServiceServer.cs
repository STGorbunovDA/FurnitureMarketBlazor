using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FurnitureMarketBlazor.Server.Services.AuthService
{
    public class AuthServiceServer : IAuthServiceServer
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthServiceServer(DataContext context, IConfiguration configuration) => (_context, _configuration) = (context, configuration);

        // Метод для аутентификации пользователя
        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                // Создание токена для пользователя в случае успешной аутентификации
                response.Data = CreateToken(user);
            }

            return response;
        }

        // Метод для регистрации пользователя
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                // Если пользователь с указанным email уже существует,
                // возвращается объект ServiceResponse с флагом Success равным false
                // и сообщением об ошибке
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exists."
                };
            }

            // Создание хэша пароля и "соли" для хранения в базе данных
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            // Установка хэша пароля и "соли" для пользователя
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Добавление пользователя в контекст и сохранение изменений в базе данных
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Возвращается объект ServiceResponse с данными, содержащими идентификатор пользователя и сообщением
            return new ServiceResponse<int> { Data = user.Id, Message = "Registration successful!" };
        }

        // Метод для проверки существования пользователя с указанным email
        public async Task<bool> UserExists(string email)
        {
            // Если хотя бы один пользователь в базе данных имеет email, 
            // совпадающий с указанным email (регистр не учитывается),
            // возвращается true, иначе false
            if (await _context.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())))
                return true;

            return false;
        }

        // Метод для создания хэша пароля и генерации "соли"
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // Создание экземпляра класса HMACSHA512 для генерации ключа и хэширования
            using (var hmac = new HMACSHA512())
            {
                // Генерация случайной "соли"
                passwordSalt = hmac.Key;

                // Хэширование пароля с использованием UTF-8 кодировки
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // Метод для проверки соответствия введенного пароля хранимому хэшу пароля
        /*
            * Метод VerifyPasswordHash используется для проверки соответствия введенного пароля хранящемуся хэшу пароля. 
              Он принимает в качестве параметров введенный пароль (password), 
              хранящийся хэш пароля (passwordHash) и "соль" (passwordSalt).

            * Внутри метода создается экземпляр класса HMACSHA512, который используется для вычисления хэша пароля 
              с применением "соли". "Соль" является случайным значением, которое добавляется к паролю перед его хэшированием. 
              Он используется для усложнения процесса взлома пароля при использовании предварительно вычисленных 
              таблиц ("радужных таблиц").

            * С помощью метода ComputeHash объекта hmac вычисляется хэш введенного пароля. В данном случае, 
              пароль представлен в виде массива байтов, преобразованных из строки UTF-8.

            * Затем, сравнивается вычисленный хэш (computedHash) с хранящимся хэшем пароля (passwordHash) путем вызова 
              метода SequenceEqual(), который сравнивает содержимое двух массивов байтов.

            * Если содержимое массивов совпадает, метод возвращает true, что означает успешную проверку пароля. 
              В противном случае, возвращается false, указывая на несовпадение паролей.

            Таким образом, метод VerifyPasswordHash позволяет производить проверку пароля, что является важным шагом 
            в процессе аутентификации пользователей.
        */
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        // Метод для создания JWT-токена на основе данных пользователя
        /*
            * CreateToken используется для создания JSON Web Token (JWT), который является механизмом безопасной 
              передачи информации в формате токена. JWT состоит из трех частей: заголовка, полезной нагрузки и подписи.

            * В начале метода создается пустой список утверждений (claims), который будет включен в полезную нагрузку токена. 
              В данном случае, создаются два утверждения: идентификатор (ClaimTypes.NameIdentifier) 
              и электронная почта (ClaimTypes.Name) пользователя. Идентификатор представлен в виде строки, преобразованной 
              из идентификатора пользователя (user.Id.ToString()), который, вероятно, является числовым значением. 
              Электронная почта пользователя (user.Email) также добавляется в утверждения.

            * Далее, метод получает ключ (key) для создания подписи токена из конфигурационного файла. 
              Файл конфигурации содержит настройки приложения, включая секретный ключ для создания и проверки подписи токена.

            * Создается экземпляр класса SymmetricSecurityKey, который принимает массив байтов, полученных из строки 
              UTF-8, содержащей значение ключа (_configuration.GetSection("AppSettings:Token").Value). Таким образом, 
              ключ токена преобразуется из строки в байты.

            * Затем, создается объект SigningCredentials, который используется для подписывания токена. 
              В данном случае, используется алгоритм HMAC-SHA512 (SecurityAlgorithms.HmacSha512Signature) для генерации подписи.

            * Создается экземпляр класса JwtSecurityToken, который принимает следующие параметры:

                * claims: список утверждений, которые будут включены в полезную нагрузку токена.
                * expires: срок действия токена, указанный как текущая дата и время, увеличенные на один день (DateTime.Now.AddDays(1)).
                * signingCredentials: объект SigningCredentials, содержащий ключ для подписи токена.
                
            * Затем, используя экземпляр класса JwtSecurityTokenHandler, создается строковое представление 
              токена (jwt) с помощью метода WriteToken(). Это строка, которая будет передаваться клиенту 
              и использоваться для проверки подлинности и авторизации запросов.

            * Наконец, из метода возвращается строковое представление токена (jwt).

            Метод CreateToken обеспечивает безопасную передачу информации и аутентификацию пользователей в приложении. 
            JWT-токены широко используются в веб-разработке для обеспечения защиты и безопасности при передаче 
            данных между клиентом и сервером.
        */
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                // Создание утверждений (claims) для токена, включающих идентификатор и email пользователя
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            // Получение ключа для создания подписи токена из конфигурационного файла
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            // Создание учений для подписывания токена с использованием ключа
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Создание JWT-токена с указанными утверждениями, сроком действия и ключом для подписи
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            // Получение строкового представления токена
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Password has been changed." };
        }
    }
}
