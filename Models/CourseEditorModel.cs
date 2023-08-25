using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Stanford_University.Models
{
    public class CourseEditorModel
    {
        [HiddenInput]
        public int Courseid { get; set; }

        [Required(ErrorMessage = "Please Enter CourseName")]
        [StringLength(50)]
        [Display(Name = "CourseName")]
        public string CourseName { get; set; }

        
        public int Duration { get; set; }

        [Required(ErrorMessage = "Please Enter Price in INR")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

    }
}
