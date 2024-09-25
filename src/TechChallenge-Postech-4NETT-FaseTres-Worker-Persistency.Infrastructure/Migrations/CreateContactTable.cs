using FluentMigrator;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infra.Migrations;

[Migration(2024092505)]
public class CreateContactTable : Migration
{
    public override void Up()
    {
        Create.Table("tb_contact").InSchema("contacts")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("first_name").AsString(40).NotNullable()
            .WithColumn("last_name").AsString(60).NotNullable()
            .WithColumn("email").AsString(60).NotNullable()
            .WithColumn("contact_phone_area_code").AsInt16().NotNullable()
            .WithColumn("contact_phone").AsInt32().NotNullable()
            .WithColumn("created_at").AsCustom("TIMESTAMP WITH TIME ZONE").WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("modified_at").AsCustom("TIMESTAMP WITH TIME ZONE").WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("active").AsBoolean().WithDefaultValue(true);
    }

    public override void Down()
    {
        Delete.Table("tb_contact").InSchema("contacts");
    }
}
