namespace Rideally.Data.EFRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "FromAddress_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Schedules", "Offerer_EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Schedules", "ToAddress_AddressId", "dbo.Addresses");
            DropIndex("dbo.Schedules", new[] { "FromAddress_AddressId" });
            DropIndex("dbo.Schedules", new[] { "Offerer_EmployeeID" });
            DropIndex("dbo.Schedules", new[] { "ToAddress_AddressId" });
            RenameColumn(table: "dbo.Schedules", name: "FromAddress_AddressId", newName: "EmployeeID");
            RenameColumn(table: "dbo.Schedules", name: "Offerer_EmployeeID", newName: "EmployeeID");
            RenameColumn(table: "dbo.Schedules", name: "ToAddress_AddressId", newName: "ToAddressID");
            AddColumn("dbo.Schedules", "FromAddressID", c => c.Int(nullable: false));
            AlterColumn("dbo.Schedules", "EmployeeID", c => c.Int(nullable: false));
            AlterColumn("dbo.Schedules", "EmployeeID", c => c.Int(nullable: false));
            AlterColumn("dbo.Schedules", "ToAddressID", c => c.Int(nullable: false));
            CreateIndex("dbo.Schedules", "EmployeeID");
            CreateIndex("dbo.Schedules", "ToAddressID");
            AddForeignKey("dbo.Schedules", "EmployeeID", "dbo.Addresses", "AddressId", cascadeDelete: true);
            AddForeignKey("dbo.Schedules", "EmployeeID", "dbo.Employees", "EmployeeID", cascadeDelete: true);
            AddForeignKey("dbo.Schedules", "ToAddressID", "dbo.Addresses", "AddressId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "ToAddressID", "dbo.Addresses");
            DropForeignKey("dbo.Schedules", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Schedules", "EmployeeID", "dbo.Addresses");
            DropIndex("dbo.Schedules", new[] { "ToAddressID" });
            DropIndex("dbo.Schedules", new[] { "EmployeeID" });
            AlterColumn("dbo.Schedules", "ToAddressID", c => c.Int());
            AlterColumn("dbo.Schedules", "EmployeeID", c => c.Int());
            AlterColumn("dbo.Schedules", "EmployeeID", c => c.Int());
            DropColumn("dbo.Schedules", "FromAddressID");
            RenameColumn(table: "dbo.Schedules", name: "ToAddressID", newName: "ToAddress_AddressId");
            RenameColumn(table: "dbo.Schedules", name: "EmployeeID", newName: "Offerer_EmployeeID");
            RenameColumn(table: "dbo.Schedules", name: "EmployeeID", newName: "FromAddress_AddressId");
            CreateIndex("dbo.Schedules", "ToAddress_AddressId");
            CreateIndex("dbo.Schedules", "Offerer_EmployeeID");
            CreateIndex("dbo.Schedules", "FromAddress_AddressId");
            AddForeignKey("dbo.Schedules", "ToAddress_AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.Schedules", "Offerer_EmployeeID", "dbo.Employees", "EmployeeID");
            AddForeignKey("dbo.Schedules", "FromAddress_AddressId", "dbo.Addresses", "AddressId");
        }
    }
}
