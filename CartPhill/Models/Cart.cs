using System.Collections.Generic;

namespace CartPhill.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Cart : DbContext
    {
        // Your context has been configured to use a 'Cart' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CartPhill.Models.Cart' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Cart' 
        // connection string in the application configuration file.
        public Cart()
            : base("name=Cart")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<ShoppingCart> ShoppingTrolley { get; set; }
    }

    public class ShoppingCart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OPP> CartcontentsList{ get; set;} 
    }
}