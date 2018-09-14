namespace Projeto01.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Persistencia.DAL.IdentityDbContextAplicacao>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Projeto01.DAL.IdentityDbContextAplicacao";
        }

        protected override void Seed(Persistencia.DAL.IdentityDbContextAplicacao context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}