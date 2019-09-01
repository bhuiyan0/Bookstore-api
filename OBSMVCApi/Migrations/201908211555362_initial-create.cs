namespace OBSMVCApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(nullable: false, maxLength: 50),
                        DoB = c.DateTime(storeType: "date"),
                        ContactNo = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 200),
                        AuthorInfo = c.String(),
                        ImageUrl = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        BookName = c.String(nullable: false, maxLength: 50),
                        CategoryId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        Descriptions = c.String(),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Edition = c.String(maxLength: 50),
                        ISBN = c.String(maxLength: 30),
                        TranslatorId = c.Int(nullable: false),
                        NumberOfPage = c.Int(nullable: false),
                        Language = c.String(maxLength: 30),
                        ImageUrl = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.AuthorId)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.BookReviews",
                c => new
                    {
                        BookReviewId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ReviewDate = c.DateTime(nullable: false, storeType: "date"),
                        Comments = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.BookReviewId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 20),
                        LastName = c.String(maxLength: 20),
                        DoB = c.DateTime(storeType: "date"),
                        Gender = c.String(maxLength: 20),
                        Address = c.String(maxLength: 200),
                        ImageUrl = c.String(),
                        UserType = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderStatus = c.String(),
                        UserID = c.String(maxLength: 128),
                        OrderDate = c.DateTime(nullable: false, storeType: "date"),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VatAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingAddressId = c.Int(nullable: false),
                        PaymentId = c.Int(nullable: false),
                        ShippingAddress_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Payments", t => t.PaymentId, cascadeDelete: true)
                .ForeignKey("dbo.ShippingAddresses", t => t.ShippingAddress_AddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.PaymentId)
                .Index(t => t.ShippingAddress_AddressId);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        OrderLineId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderLineId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        PaymentType = c.String(),
                        PaymentMethod = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BankName = c.String(),
                        AccountNo = c.String(),
                        TransactionId = c.String(),
                        PaymentNote = c.String(),
                        PaymentTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId);
            
            CreateTable(
                "dbo.ShippingAddresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        District = c.String(maxLength: 20),
                        Address = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        ReferenceNo = c.String(),
                        PaymentStatus = c.String(),
                        PublisherId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        PurchaseDate = c.DateTime(nullable: false, storeType: "date"),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VatAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("dbo.Payments", t => t.PaymentId, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PublisherId)
                .Index(t => t.UserId)
                .Index(t => t.PaymentId);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        PublisherName = c.String(nullable: false, maxLength: 50),
                        ContactNo = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PublisherId);
            
            CreateTable(
                "dbo.PurchaseLines",
                c => new
                    {
                        PurchaseLineId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseLineId)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.PurchaseId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        WishListId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WishListId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Email = c.String(),
                        FeedbackDate = c.DateTime(nullable: false, storeType: "date"),
                        Comments = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.FeedbackId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ReorderLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.WishLists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WishLists", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Carts", "BookId", "dbo.Books");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Purchases", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PurchaseLines", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.Books", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.Purchases", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.Orders", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ShippingAddress_AddressId", "dbo.ShippingAddresses");
            DropForeignKey("dbo.Orders", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.OrderLines", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderLines", "BookId", "dbo.Books");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookReviews", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookReviews", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.WishLists", new[] { "BookId" });
            DropIndex("dbo.WishLists", new[] { "UserId" });
            DropIndex("dbo.Carts", new[] { "BookId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.PurchaseLines", new[] { "PurchaseId" });
            DropIndex("dbo.Purchases", new[] { "PaymentId" });
            DropIndex("dbo.Purchases", new[] { "UserId" });
            DropIndex("dbo.Purchases", new[] { "PublisherId" });
            DropIndex("dbo.OrderLines", new[] { "OrderId" });
            DropIndex("dbo.OrderLines", new[] { "BookId" });
            DropIndex("dbo.Orders", new[] { "ShippingAddress_AddressId" });
            DropIndex("dbo.Orders", new[] { "PaymentId" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BookReviews", new[] { "UserId" });
            DropIndex("dbo.BookReviews", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "PublisherId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropIndex("dbo.Books", new[] { "CategoryId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.WishLists");
            DropTable("dbo.Categories");
            DropTable("dbo.Carts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.PurchaseLines");
            DropTable("dbo.Publishers");
            DropTable("dbo.Purchases");
            DropTable("dbo.ShippingAddresses");
            DropTable("dbo.Payments");
            DropTable("dbo.OrderLines");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BookReviews");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
