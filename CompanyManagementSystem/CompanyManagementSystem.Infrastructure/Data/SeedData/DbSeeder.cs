using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Infrastructure.Helpers;

namespace CompanyManagementSystem.Infrastructure.Data.SeedData;

public static class DbSeeder
{
    public static readonly List<Company> Companies =
        [
            new Company 
            {
                Name = "Facebook"
            }
        ];

    public static readonly List<User> Users =
        [
            new User
            {
                UserName = "mzuck",
                Password = HashHelper.Hash("zuck@gmail.com", "Password1"),
                FirstName = "Mark",
                LastName = "Zuckerberg",
                Email = "zuck@gmail.com",
                CompanyId = 1
            },
            new User
            {
                UserName = "jbezos",
                Password = HashHelper.Hash("jeff@gmail.com", "Password2"),
                FirstName = "Jeff",
                LastName = "Bezos",
                Email = "jeff@gmail.com",
            }
        ];
}
