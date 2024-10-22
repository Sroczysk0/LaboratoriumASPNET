using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab01Ak.Models;

public class ContactModel
{
    [HiddenInput]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please enter your first name")]
    [MaxLength(length: 20, ErrorMessage = "Name too long")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Please enter your last name")]
    [MaxLength(length: 50, ErrorMessage = "LastName too long")]
    public string LastName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    [RegularExpression("\\d\\d\\d \\d\\d\\d \\d\\d\\d")]
    public string PhoneNumber { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
}