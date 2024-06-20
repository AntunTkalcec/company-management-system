using CompanyManagementSystem.Core.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagementSystem.Core.DTOs;

public class CompanyDTO : BaseDTO
{
    [Required]
    [Length(2, 100)]
    public string Name { get; set; }

    public byte[]? CompanyImage { get; set; }

    [Required]
    public int PTO { get; set; }

    #region Relations
    public List<UserDTO> Staff { get; set; }
    #endregion
}
