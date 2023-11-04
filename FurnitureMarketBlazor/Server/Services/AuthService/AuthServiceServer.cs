using System.Security.Cryptography;

namespace FurnitureMarketBlazor.Server.Services.AuthService
{
    public class AuthServiceServer : IAuthServiceServer
    {
        private readonly DataContext _context;

        // Конструктор класса AuthService, принимающий объект DataContext
        public AuthServiceServer(DataContext context) => _context = context;

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

            // Возвращается объект ServiceResponse с данными, содержащими идентификатор пользователя
            return new ServiceResponse<int> { Data = user.Id };
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
    }
}
