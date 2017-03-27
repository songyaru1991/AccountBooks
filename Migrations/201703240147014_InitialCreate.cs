namespace AccountBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChargeModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Category = c.String(),
                        Amount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChargeModels");
        }
    }
}
