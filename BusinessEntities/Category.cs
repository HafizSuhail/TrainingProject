using System;
using System.Collections.Generic;

namespace Stanford_University.BusinessEntities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
