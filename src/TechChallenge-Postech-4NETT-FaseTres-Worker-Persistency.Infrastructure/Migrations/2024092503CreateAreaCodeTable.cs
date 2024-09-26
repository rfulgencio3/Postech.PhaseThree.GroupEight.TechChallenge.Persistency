using FluentMigrator;

namespace Postech.TechChallenge.Persistency.Infra.Migrations;

[Migration(2024092503)]
public class CreateAreaCodeTable : Migration
{
    public override void Up()
    {
        if (!Schema.Table("tb_area_code").Exists())
        {
            Create.Table("tb_area_code").InSchema("contacts")
                .WithColumn("id").AsInt16().PrimaryKey().Identity()
                .WithColumn("value").AsFixedLengthString(2).NotNullable()
                .WithColumn("region_id").AsInt16().NotNullable()
                .ForeignKey("fk_tb_region_tb_area_code", "contacts", "tb_region", "id")
                .OnDelete(System.Data.Rule.Cascade);
        }
    }

    public override void Down()
    {
        if (Schema.Table("tb_area_code").Exists())
        {
            Delete.Table("tb_area_code").InSchema("contacts");
        }
    }
}
