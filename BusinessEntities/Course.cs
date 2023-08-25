using System;
using System.Collections.Generic;

namespace Stanford_University.BusinessEntities;

public partial class Course
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public int Duration { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
