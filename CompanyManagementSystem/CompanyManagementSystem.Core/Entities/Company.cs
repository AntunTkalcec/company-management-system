using CompanyManagementSystem.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagementSystem.Core.Entities;

[Table("Company")]
public class Company : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public byte[]? CompanyImage { get; set; }

    public int PTO { get; set; }

    #region Relations
    public virtual ICollection<User> Staff { get; set; }
    #endregion
}
