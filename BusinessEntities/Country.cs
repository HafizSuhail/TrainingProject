using System;
using System.Collections.Generic;

namespace Stanford_University.BusinessEntities;

public partial class Country
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
