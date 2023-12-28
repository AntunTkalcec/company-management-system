using System.ComponentModel.DataAnnotations;

namespace CompanyManagementSystem.Core.Authentication;

public class UserLogin
{
    [Required(ErrorMessage = "This field is required")]
    public string EmailOrUserName { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [StringLength(50)]
    public string Password { get; set; }
}
