using FluentMigrator;

namespace Postech.TechChallenge.Persistency.Infra.Migrations;

[Migration(2024092501)]
public class CreateContactsSchema : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE SCHEMA IF NOT EXISTS contacts");
    }

    public override void Down()
    {
        Execute.Sql("DROP SCHEMA IF EXISTS contacts CASCADE");
    }
}
