using FluentMigrator;

namespace MyRecipeBook.Infrastructe.Migrations.Versions
{
    [ Migration(DataBaseVersion.TABLE_USER, "Create Table saver the user information")]
    public class Version0000001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Users") 
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable().Unique()
                .WithColumn("Password").AsString(255).NotNullable();
        }
    }
}
