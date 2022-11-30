using BelajarWebApi.Models;
using Microsoft.AspNetCore.Identity;

namespace BelajarWebApi.Data
{
    public class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();

            var user = new User
            {
                Username = "user",
                Password = "user",
                Fullname = "User nih"
            };

            if (context.Users.Any(e => e.Username == user.Username))
            {
                return;
            }

            user.Password = passwordHasher.HashPassword(user, user.Password);

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
