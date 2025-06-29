using System.Data;
using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(1)]
public class M0000_InitialMigration : Migration
{
    public override void Up()
    {
        Create.Table("employees")
            .WithColumn("id").AsInt64().Identity().PrimaryKey()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("surname").AsString().NotNullable()
            .WithColumn("phone").AsString().Unique().NotNullable()
            .WithColumn("company_id").AsInt64().NotNullable()
            .WithColumn("department_id").AsInt64().Nullable();
        
        Create.Table("passports")
            .WithColumn("id").AsInt64().Identity().PrimaryKey()
            .WithColumn("type").AsString().NotNullable()
            .WithColumn("number").AsString().NotNullable()
            .WithColumn("employee_id").AsInt64().NotNullable().Unique();
        
        Create.Table("departments")
            .WithColumn("id").AsInt64().Identity().PrimaryKey()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("phone").AsString().Unique();
        
        Create.ForeignKey("FK_Passports_Employees")
            .FromTable("passports").ForeignColumn("employee_id")
            .ToTable("employees").PrimaryColumn("id")
            .OnDelete(Rule.Cascade);
        
        Create.ForeignKey("FK_Employees_Departments")
            .FromTable("employees").ForeignColumn("department_id")
            .ToTable("departments").PrimaryColumn("id")
            .OnDelete(Rule.Cascade);
    }

    public override void Down()
    {
        Delete.Table("employees");
        Delete.Table("passports");
        Delete.Table("departments");
    }
}