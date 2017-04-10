namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datauseradd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DataUser_Lastname", c => c.String());
            DropColumn("dbo.AspNetUsers", "DataUser_Forname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DataUser_Forname", c => c.String());
            DropColumn("dbo.AspNetUsers", "DataUser_Lastname");
        }
    }
}
