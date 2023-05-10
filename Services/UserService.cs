namespace auth.Services
{
    public class UserService
    {
        public static string HashPassword(string password)
        {
            // Mã hóa mật khẩu bằng thuật toán hash
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password,string passwordHash)
        {
            // Kiểm tra mật khẩu có khớp với mật khẩu đã lưu hay không
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}