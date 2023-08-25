using System;
using System.Collections.Generic;

namespace Stanford_University.BusinessEntities;

public partial class Student
{
    public int StudentId { get; set; }

    public string? RollNo { get; set; }

    public string StudentName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string? MobileNumber { get; set; }

    public string? Email { get; set; }

    public int? CategoryId { get; set; }

    public int? CountryId { get; set; }

    public int? CourseId { get; set; }

    public int? UserId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User? User { get; set; }
}
