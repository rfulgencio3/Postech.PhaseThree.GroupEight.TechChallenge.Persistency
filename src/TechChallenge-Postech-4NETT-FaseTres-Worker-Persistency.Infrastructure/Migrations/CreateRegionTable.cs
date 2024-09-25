using FluentMigrator;

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infra.Migrations;

[Migration(2024092502)]
public class CreateRegionTable : Migration
{
    public override void Up()
    {
        Create.Table("tb_region").InSchema("contacts")
            .WithColumn("id").AsInt16().PrimaryKey().Identity()
            .WithColumn("name").AsString(12).NotNullable()
            .WithColumn("state_name").AsString(19).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("tb_region").InSchema("contacts");
    }
}
