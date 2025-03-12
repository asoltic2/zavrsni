using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(50)]
    public string Department { get; set; }
}
