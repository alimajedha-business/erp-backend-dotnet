using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace General.Domain.Entities;

[Table("users", Schema = "general")]
[Index("PersonId", Name = "UQ__users__543848DE030F1F1D", IsUnique = true)]
[Index("Username", Name = "UQ__users__F3DBC57279382736", IsUnique = true)]
[Index("LanguageId", Name = "users_language_id_9c707b57")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("password")]
    [StringLength(128)]
    public string Password { get; set; } = null!;

    [Column("username")]
    [StringLength(150)]
    public string Username { get; set; } = null!;

    [Column("username_encrypted")]
    [StringLength(150)]
    public string? UsernameEncrypted { get; set; }

    [Column("is_superuser")]
    public bool IsSuperuser { get; set; }

    [Column("is_staff")]
    public bool IsStaff { get; set; }

    [Column("is_report_viewer")]
    public bool IsReportViewer { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("auth_send_type")]
    [StringLength(12)]
    public string? AuthSendType { get; set; }

    [Column("color_pallet")]
    [StringLength(10)]
    public string ColorPallet { get; set; } = null!;

    [Column("font_family")]
    [StringLength(10)]
    public string FontFamily { get; set; } = null!;

    [Column("font_size")]
    public double FontSize { get; set; }

    [Column("new_tab")]
    public bool NewTab { get; set; }

    [Column("last_login")]
    public DateTime? LastLogin { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("language_id")]
    public int LanguageId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("access_token")]
    public string? AccessToken { get; set; }

    [Column("refresh_token")]
    public string? RefreshToken { get; set; }

    [InverseProperty("Creator")]
    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();

    [InverseProperty("Admin")]
    public virtual ICollection<CompanyAdmin> CompanyAdminAdmins { get; set; } = new List<CompanyAdmin>();

    [InverseProperty("Creator")]
    public virtual ICollection<CompanyAdmin> CompanyAdminCreators { get; set; } = new List<CompanyAdmin>();

    [InverseProperty("Creator")]
    public virtual ICollection<EducationalDegree> EducationalDegrees { get; set; } = new List<EducationalDegree>();

    [InverseProperty("Creator")]
    public virtual ICollection<EmploymentContractDescription> EmploymentContractDescriptions { get; set; } = new List<EmploymentContractDescription>();

    [InverseProperty("Creator")]
    public virtual ICollection<EmploymentContractTitle> EmploymentContractTitles { get; set; } = new List<EmploymentContractTitle>();

    [InverseProperty("User")]
    public virtual ICollection<ErrorLog> ErrorLogs { get; set; } = new List<ErrorLog>();

    [InverseProperty("Creator")]
    public virtual ICollection<ForeignLanguage> ForeignLanguages { get; set; } = new List<ForeignLanguage>();

    [InverseProperty("Creator")]
    public virtual ICollection<HousingStatus> HousingStatuses { get; set; } = new List<HousingStatus>();

    [InverseProperty("Creator")]
    public virtual ICollection<JobPosition> JobPositions { get; set; } = new List<JobPosition>();

    [InverseProperty("Creator")]
    public virtual ICollection<JobRank> JobRanks { get; set; } = new List<JobRank>();

    [InverseProperty("Creator")]
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    [ForeignKey("LanguageId")]
    [InverseProperty("Users")]
    public virtual Language Language { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    [InverseProperty("Creator")]
    public virtual ICollection<MeasurementUnit> MeasurementUnits { get; set; } = new List<MeasurementUnit>();

    [InverseProperty("Creator")]
    public virtual ICollection<MilitaryServiceStatus> MilitaryServiceStatuses { get; set; } = new List<MilitaryServiceStatus>();

    [InverseProperty("Creator")]
    public virtual ICollection<MissionType> MissionTypes { get; set; } = new List<MissionType>();

    [InverseProperty("User2")]
    public virtual ICollection<Notification> NotificationUser2s { get; set; } = new List<Notification>();

    [InverseProperty("User")]
    public virtual ICollection<Notification> NotificationUsers { get; set; } = new List<Notification>();

    [ForeignKey("PersonId")]
    [InverseProperty("User")]
    public virtual Person Person { get; set; } = null!;

    [InverseProperty("Creator")]
    public virtual ICollection<PersonEducationalDegree> PersonEducationalDegrees { get; set; } = new List<PersonEducationalDegree>();

    [InverseProperty("Creator")]
    public virtual ICollection<PersonRelative> PersonRelatives { get; set; } = new List<PersonRelative>();

    [InverseProperty("Creator")]
    public virtual ICollection<Religion> Religions { get; set; } = new List<Religion>();

    [InverseProperty("User")]
    public virtual ICollection<SelectLog> SelectLogs { get; set; } = new List<SelectLog>();

    [InverseProperty("User")]
    public virtual ICollection<UserConfig> UserConfigs { get; set; } = new List<UserConfig>();

    [InverseProperty("Creator")]
    public virtual ICollection<WorkDepartment> WorkDepartments { get; set; } = new List<WorkDepartment>();

    [InverseProperty("Creator")]
    public virtual ICollection<WorkOperation> WorkOperations { get; set; } = new List<WorkOperation>();

    [InverseProperty("Creator")]
    public virtual ICollection<Workplace> Workplaces { get; set; } = new List<Workplace>();
}
