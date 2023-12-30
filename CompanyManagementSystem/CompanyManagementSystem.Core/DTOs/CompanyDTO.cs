using CompanyManagementSystem.Core.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagementSystem.Core.DTOs;

public class CompanyDTO : BaseDTO
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public byte[]? CompanyImage { get; set; }

    public int PTO { get; set; }
}
