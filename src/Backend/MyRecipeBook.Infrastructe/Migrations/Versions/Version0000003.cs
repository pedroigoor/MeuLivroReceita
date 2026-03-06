using FluentMigrator;

namespace MyRecipeBook.Infrastructe.Migrations.Versions
{
    [ Migration(DataBaseVersion.IMAGES_FOR_RECIPES, "Add column img in recipe")]
    public class Version0000003 : VersionBase
    {
        public override void Up()
        {
            Alter.Table("Recipes")
                .AddColumn("ImageIdentifier").AsString().Nullable();

        }
    }
}
