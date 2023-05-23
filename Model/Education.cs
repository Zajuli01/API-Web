using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Web.Model;

[Table("tb_m_educations")]
public class Education : BaseEntity
{
    [Column("major", TypeName ="nvarchar(100)")]
    public string Major { get; set; }
    [Column("degree", TypeName = "nvarchar(10)")]
    public string Degree { get; set; }
    [Column("gpa")]
    public float GPA { get; set; }
    [Column("university_guid")]
    public Guid UniversityGuid { get; set; }

    //Cardinality
    public University? University { get; set; }
    public Employee? Employee { get; set; }
}
