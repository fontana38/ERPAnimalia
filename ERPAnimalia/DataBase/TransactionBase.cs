using ERPAnimalia.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPAnimalia.DataBase
{
    public class TransactionBase
    {
        public Product Product { get; set; }

        public TransactionBase(Product product)
        {
            Product = product;
        }

        //public void GetTransaction()
        //{ 
        //    using (var context = new AnimaliaPetShopEntities())
        //    {
        //        using (var dbContextTransaction = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                context.SaveChanges();

        //                dbContextTransaction.Commit();
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }
        //    }

        //    //        using (var context = new BloggingContext()) 
        //    //{ 
        //    //    using (var dbContextTransaction = context.Database.BeginTransaction()) 
        //    //    { 
        //    //        try 
        //    //        { 
        //    //            context.Database.ExecuteSqlCommand( 
        //    //                @"UPDATE Blogs SET Rating = 5" + 
        //    //                    " WHERE Name LIKE '%Entity Framework%'" 
        //    //                ); 

        //    //            var query = context.Posts.Where(p => p.Blog.Rating >= 5); 
        //    //            foreach (var post in query) 
        //    //            { 
        //    //                post.Title += "[Cool Blog]"; 
        //    //            }

        //    //context.SaveChanges(); 

        //    //            dbContextTransaction.Commit(); 
        //    //        } 
        //    //        catch (Exception) 
        //    //        { 
        //    //            dbContextTransaction.Rollback(); //Required according to MSDN article 
        //    //            throw; //Not in MSDN article, but recommended so the exception still bubbles up
        //    //        } 
        //    //    } 
        //    //} 

        //}
    }
}