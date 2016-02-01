namespace Rideally.Data.EFRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        type = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Authentications",
                c => new
                    {
                        AuthenticationId = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.AuthenticationId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        VType_VehicleTypeMasterID = c.Int(),
                    })
                .PrimaryKey(t => t.BrandId)
                .ForeignKey("dbo.VehicleTypeMasters", t => t.VType_VehicleTypeMasterID)
                .Index(t => t.VType_VehicleTypeMasterID);
            
            CreateTable(
                "dbo.VehicleTypeMasters",
                c => new
                    {
                        VehicleTypeMasterID = c.Int(nullable: false, identity: true),
                        VehicleMasterType = c.String(),
                    })
                .PrimaryKey(t => t.VehicleTypeMasterID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        Gender = c.String(),
                        MobileNo = c.String(),
                        EmailID = c.String(),
                        HomeAddress_AddressId = c.Int(),
                        OfficeAddress_AddressId = c.Int(),
                        UserAuthentication_AuthenticationId = c.Int(),
                        Vehicle_VehicleId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Addresses", t => t.HomeAddress_AddressId)
                .ForeignKey("dbo.Addresses", t => t.OfficeAddress_AddressId)
                .ForeignKey("dbo.Authentications", t => t.UserAuthentication_AuthenticationId)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_VehicleId)
                .Index(t => t.HomeAddress_AddressId)
                .Index(t => t.OfficeAddress_AddressId)
                .Index(t => t.UserAuthentication_AuthenticationId)
                .Index(t => t.Vehicle_VehicleId);
            
            CreateTable(
                "dbo.EmployeeVehicles",
                c => new
                    {
                        EmployeeVehicleId = c.Int(nullable: false, identity: true),
                        Color = c.String(),
                        VehicleNumber = c.String(),
                        Employee_EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeVehicleId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeID)
                .Index(t => t.Employee_EmployeeID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        ModelName = c.String(),
                        Brand_BrandId = c.Int(),
                        VehicleType_VehicleTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.Brands", t => t.Brand_BrandId)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleType_VehicleTypeID)
                .Index(t => t.Brand_BrandId)
                .Index(t => t.VehicleType_VehicleTypeID);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        VehicleTypeID = c.Int(nullable: false, identity: true),
                        VehicleTypeDesc = c.String(),
                        NoOfSeats = c.Int(nullable: false),
                        VehicleTypeMaster_VehicleTypeMasterID = c.Int(),
                    })
                .PrimaryKey(t => t.VehicleTypeID)
                .ForeignKey("dbo.VehicleTypeMasters", t => t.VehicleTypeMaster_VehicleTypeMasterID)
                .Index(t => t.VehicleTypeMaster_VehicleTypeMasterID);
            
            CreateTable(
                "dbo.RiderMasters",
                c => new
                    {
                        RiderMasterID = c.Int(nullable: false, identity: true),
                        Seeker_EmployeeID = c.Int(),
                        Schedule_ScheduleId = c.Int(),
                    })
                .PrimaryKey(t => t.RiderMasterID)
                .ForeignKey("dbo.Employees", t => t.Seeker_EmployeeID)
                .ForeignKey("dbo.Schedules", t => t.Schedule_ScheduleId)
                .Index(t => t.Seeker_EmployeeID)
                .Index(t => t.Schedule_ScheduleId);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    { 
                        ScheduleId = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        FromAddressID = c.Int(nullable: false),
                        ToAddressID = c.Int(nullable: false),
                        ScheduledDate = c.DateTime(nullable: false),
                        ScheduledTime = c.String(),
                        ScheduledStatus = c.String(),
                        SeatsAvailable = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleId)
                .ForeignKey("dbo.Addresses", t => t.FromAddressID, cascadeDelete: false)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.ToAddressID, cascadeDelete: false)
                .Index(t => t.EmployeeID)
                .Index(t => t.FromAddressID)
                .Index(t => t.ToAddressID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "ToAddressID", "dbo.Addresses");
            DropForeignKey("dbo.RiderMasters", "Schedule_ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Schedules", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Schedules", "FromAddressID", "dbo.Addresses");
            DropForeignKey("dbo.RiderMasters", "Seeker_EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "Vehicle_VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "VehicleType_VehicleTypeID", "dbo.VehicleTypes");
            DropForeignKey("dbo.VehicleTypes", "VehicleTypeMaster_VehicleTypeMasterID", "dbo.VehicleTypeMasters");
            DropForeignKey("dbo.Vehicles", "Brand_BrandId", "dbo.Brands");
            DropForeignKey("dbo.Employees", "UserAuthentication_AuthenticationId", "dbo.Authentications");
            DropForeignKey("dbo.Employees", "OfficeAddress_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Employees", "HomeAddress_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.EmployeeVehicles", "Employee_EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Brands", "VType_VehicleTypeMasterID", "dbo.VehicleTypeMasters");
            DropIndex("dbo.Schedules", new[] { "ToAddressID" });
            DropIndex("dbo.Schedules", new[] { "FromAddressID" });
            DropIndex("dbo.Schedules", new[] { "EmployeeID" });
            DropIndex("dbo.RiderMasters", new[] { "Schedule_ScheduleId" });
            DropIndex("dbo.RiderMasters", new[] { "Seeker_EmployeeID" });
            DropIndex("dbo.VehicleTypes", new[] { "VehicleTypeMaster_VehicleTypeMasterID" });
            DropIndex("dbo.Vehicles", new[] { "VehicleType_VehicleTypeID" });
            DropIndex("dbo.Vehicles", new[] { "Brand_BrandId" });
            DropIndex("dbo.EmployeeVehicles", new[] { "Employee_EmployeeID" });
            DropIndex("dbo.Employees", new[] { "Vehicle_VehicleId" });
            DropIndex("dbo.Employees", new[] { "UserAuthentication_AuthenticationId" });
            DropIndex("dbo.Employees", new[] { "OfficeAddress_AddressId" });
            DropIndex("dbo.Employees", new[] { "HomeAddress_AddressId" });
            DropIndex("dbo.Brands", new[] { "VType_VehicleTypeMasterID" });
            DropTable("dbo.Schedules");
            DropTable("dbo.RiderMasters");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Vehicles");
            DropTable("dbo.EmployeeVehicles");
            DropTable("dbo.Employees");
            DropTable("dbo.VehicleTypeMasters");
            DropTable("dbo.Brands");
            DropTable("dbo.Authentications");
            DropTable("dbo.Addresses");
        }
    }
}
