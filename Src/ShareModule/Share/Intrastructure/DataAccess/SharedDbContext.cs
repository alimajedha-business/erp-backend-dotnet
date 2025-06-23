using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Entities;

namespace Shared.Intrastructure.DataAccess;

public partial class SharedDbContext : DbContext
{
    public SharedDbContext()
    {
    }

    public SharedDbContext(DbContextOptions<SharedDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BankAccount> BankAccounts { get; set; }

    public virtual DbSet<BankBranch> BankBranches { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Cashier> Cashiers { get; set; }

    public virtual DbSet<CompanySetting> CompanySettings { get; set; }

    public virtual DbSet<CompanyUnit> CompanyUnits { get; set; }

    public virtual DbSet<CostCenter> CostCenters { get; set; }

    public virtual DbSet<Foo> Foos { get; set; }

    public virtual DbSet<PersonCompany> PersonCompanies { get; set; }

    public virtual DbSet<PersonGroupMember> PersonGroupMembers { get; set; }

    public virtual DbSet<PettyCashier> PettyCashiers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<ReportTemplate> ReportTemplates { get; set; }

    public virtual DbSet<Restriction> Restrictions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMember> RoleMembers { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<RolePermissionCommand> RolePermissionCommands { get; set; }

    public virtual DbSet<RolePermissionConstraint> RolePermissionConstraints { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\sql19;Database=NGERP;User Id=sa;Password=123;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Persian_100_CI_AI");

        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bank_acc__3213E83F744A2E6C");

            entity.HasIndex(e => new { e.CompanyId, e.BankId, e.AccountNumber }, "bank_accounts_company_id_bank_id_account_number_dde4bd1e_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [account_number] IS NOT NULL)");

            entity.HasIndex(e => new { e.CompanyId, e.BankId, e.CardNumber }, "bank_accounts_company_id_bank_id_card_number_fbf237d1_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [card_number] IS NOT NULL)");

            entity.HasIndex(e => new { e.CompanyId, e.BankId, e.Iban }, "bank_accounts_company_id_bank_id_iban_077a9abd_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [iban] IS NOT NULL)");

            entity.HasOne(d => d.BankBranch).WithMany(p => p.BankAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bank_accounts_bank_branch_id_f4462cb3_fk_bank_branches_id");
        });

        modelBuilder.Entity<BankBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bank_bra__3213E83F7C6E8FEC");

            entity.HasIndex(e => new { e.CompanyId, e.BankId, e.BranchNumber }, "bank_branches_company_id_bank_id_branch_number_f4f0441e_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [branch_number] IS NOT NULL)");

            entity.HasIndex(e => new { e.CompanyId, e.BankId, e.Name }, "bank_branches_company_id_bank_id_name_e96f3efa_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [name] IS NOT NULL)");

            entity.HasIndex(e => new { e.CompanyId, e.Code }, "bank_branches_company_id_code_edffeec5_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [code] IS NOT NULL)");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__branches__3213E83F59453D0A");

            entity.HasIndex(e => new { e.CompanyId, e.Code }, "branches_company_id_code_2a76aa26_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [code] IS NOT NULL)");

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "branches_company_id_name_0b77f2d0_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [name] IS NOT NULL)");
        });

        modelBuilder.Entity<Cashier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cashiers__3213E83FE910213E");

            entity.HasIndex(e => new { e.CompanyId, e.PersonId }, "cashiers_company_id_person_id_f0a0aa02_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [person_id] IS NOT NULL)");
        });

        modelBuilder.Entity<CompanySetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__company___3213E83F44690446");
        });

        modelBuilder.Entity<CompanyUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__company___3213E83FE6702795");

            entity.HasIndex(e => new { e.CompanyId, e.Code }, "company_units_company_id_code_af9699c4_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [code] IS NOT NULL)");

            entity.HasIndex(e => new { e.CompanyId, e.Name, e.ParentId }, "company_units_company_id_name_parent_id_131855b6_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [name] IS NOT NULL AND [parent_id] IS NOT NULL)");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("company_units_parent_id_c41b621a_fk_company_units_id");
        });

        modelBuilder.Entity<CostCenter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cost_cen__3213E83FF8026CF8");

            entity.HasIndex(e => new { e.CompanyId, e.Code }, "cost_centers_company_id_code_153ce7ab_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [code] IS NOT NULL)");

            entity.HasIndex(e => new { e.CompanyId, e.Name, e.ParentId }, "cost_centers_company_id_name_parent_id_44518991_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [name] IS NOT NULL AND [parent_id] IS NOT NULL)");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("cost_centers_parent_id_eba90f6c_fk_cost_centers_id");
        });

        modelBuilder.Entity<Foo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__foos__3213E83F373F6C6C");

            entity.HasOne(d => d.Product).WithMany(p => p.Foos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("foos_product_id_64577d3d_fk_products_id");
        });

        modelBuilder.Entity<PersonCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_c__3213E83FDD636BD2");

            entity.HasIndex(e => new { e.PersonId, e.CompanyId }, "person_companies_person_id_company_id_7ccc3e32_uniq")
                .IsUnique()
                .HasFilter("([person_id] IS NOT NULL AND [company_id] IS NOT NULL)");
        });

        modelBuilder.Entity<PersonGroupMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_g__3213E83F33F883A4");

            entity.HasIndex(e => new { e.CompanyId, e.PersonGroupId, e.MemberId }, "person_group_members_company_id_person_group_id_member_id_b5c20e26_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [person_group_id] IS NOT NULL AND [member_id] IS NOT NULL)");
        });

        modelBuilder.Entity<PettyCashier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__petty_ca__3213E83F7F5C9154");

            entity.HasIndex(e => new { e.CompanyId, e.PersonId }, "petty_cashiers_company_id_person_id_d434ff7c_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [person_id] IS NOT NULL)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83FA586B203");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("products_parent_id_6d11d0e1_fk_products_id");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Purchase__3213E83F5BE5247E");

            entity.HasOne(d => d.Product).WithMany(p => p.Purchases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Purchases_product_id_d1413dc0_fk_products_id");
        });

        modelBuilder.Entity<ReportTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__report_t__3213E83F97AEB692");

            entity.HasIndex(e => new { e.CompanyId, e.ReportKey, e.Name }, "report_templates_company_id_report_key_name_f3282f81_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [report_key] IS NOT NULL AND [name] IS NOT NULL)");
        });

        modelBuilder.Entity<Restriction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__restrict__3213E83F37293154");

            entity.HasIndex(e => new { e.CompanyId, e.EntityTypeId, e.UserId }, "restrictions_company_id_entity_type_id_user_id_2296a366_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [entity_type_id] IS NOT NULL AND [user_id] IS NOT NULL)");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83FF1D516A7");

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "roles_company_id_name_35c99075_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [name] IS NOT NULL)");
        });

        modelBuilder.Entity<RoleMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role_mem__3213E83FA3102DA6");

            entity.HasIndex(e => new { e.RoleId, e.MemberId }, "role_members_role_id_member_id_22328589_uniq")
                .IsUnique()
                .HasFilter("([role_id] IS NOT NULL AND [member_id] IS NOT NULL)");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_members_role_id_3742ecb7_fk_roles_id");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role_per__3213E83FADCE6844");

            entity.HasIndex(e => new { e.RoleId, e.EntityTypeId }, "role_permissions_role_id_entity_type_id_285eba22_uniq")
                .IsUnique()
                .HasFilter("([role_id] IS NOT NULL AND [entity_type_id] IS NOT NULL)");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permissions_role_id_216516f2_fk_roles_id");
        });

        modelBuilder.Entity<RolePermissionCommand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role_per__3213E83F6F6DDE28");

            entity.HasIndex(e => new { e.RolePermissionId, e.EntityTypeCommandId }, "role_permission_commands_role_permission_id_entity_type_command_id_9f7be0a3_uniq")
                .IsUnique()
                .HasFilter("([role_permission_id] IS NOT NULL AND [entity_type_command_id] IS NOT NULL)");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissionCommands)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_commands_role_id_5d1758cb_fk_roles_id");

            entity.HasOne(d => d.RolePermission).WithMany(p => p.RolePermissionCommands)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_commands_role_permission_id_40a8626f_fk_role_permissions_id");
        });

        modelBuilder.Entity<RolePermissionConstraint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role_per__3213E83F595C377F");

            entity.HasIndex(e => new { e.RolePermissionId, e.FieldName }, "role_permission_constraints_role_permission_id_field_name_6671fb32_uniq")
                .IsUnique()
                .HasFilter("([role_permission_id] IS NOT NULL AND [field_name] IS NOT NULL)");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissionConstraints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_constraints_role_id_3ad70fc8_fk_roles_id");

            entity.HasOne(d => d.RolePermission).WithMany(p => p.RolePermissionConstraints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_constraints_role_permission_id_678eb8f6_fk_role_permissions_id");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sales__3213E83FDEDC98F0");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_product_id_a179a813_fk_products_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
