using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Stanford_University.Models
{
    public class StudentEditorModel
    {
        [HiddenInput]
        public int Studentid { get; set; }

        [Required(ErrorMessage = "Please Enter RollNumber")]
        [StringLength(10)]
        [Display(Name = "RollNumber")]
        public string? RollNo { get; set; }

        [Required(ErrorMessage = "Please Enter StudentName")]
        [StringLength(25)]
        [Display(Name = "StudentName")]
        public string SName { get; set; } = null!;

        [StringLength(10)]
        public string gender { get; set; } = null!;

        [Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime dob { get; set; }

        [Required(ErrorMessage = "Enter the Mobile Number")]
        [Display(Name = "MobileNumber")]
        [DataType(DataType.PhoneNumber)]
        public string? mobilenumber { get; set; }

        
        
        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email")]
        [StringLength(20)]
        [EmailAddress]
        public string? email { get; set; }

        [Required(ErrorMessage = "Please select a Category")]
        [Display(Name = "Category")]
        public int? categoryid { get; set; }

        [Required(ErrorMessage = "Please select a Country")]
        [Display(Name = "Country")]
        public int? countryid { get; set; }

        [Required(ErrorMessage = "Please select a Course")]
        [Display(Name = "Course")]
        public int? courseid { get; set; }

        [IgnoreDataMember]
        public List<SelectListItem> Categories { get; set; }

        [IgnoreDataMember]
        public List<SelectListItem> Countries { get; set; }

        [IgnoreDataMember]
        public List<SelectListItem> Courses { get; set; }


    }
}
