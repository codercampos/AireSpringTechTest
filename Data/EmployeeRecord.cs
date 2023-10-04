using System.ComponentModel.DataAnnotations;

namespace AireSpringTechTest.Data;

public class EmployeeRecord
{
    public int EmployeeId { get; set; }
    [Required]
    public string EmployeeLastName { get; set; }
    [Required]
    public string EmployeeFirstName { get; set; }
    [Required]
    public string EmployeePhone { get; set; }
    public string EmployeeZip { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = @"{0:MM\/dd\/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime HireDate { get; set; }
}