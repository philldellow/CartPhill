namespace CartPhill.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Product : DbContext
    {
        // Your context has been configured to use a 'Product' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CartPhill.Models.Product' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Product' 
        // connection string in the application configuration file.
        public Product()
            : base("name=Product")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<OPP> Hoards { get; set; }
    }

    public class OPP
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Catagory { get; set; }
    }
}