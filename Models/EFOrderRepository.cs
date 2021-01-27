using Microsoft.EntityFrameworkCore;
using SuplementsStore1.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuplementsStore1.Models
{
    //klasa omogućava dobavljanje sačuvanih Order objekata, kao i izmenu i čuvanje objekata
    public class EFOrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> Orders => context.Orders.
            Include(o=>o.Lines) //kako bi se navelo da kada se ucitava order objekat
                                //treba ucitati i kolekciju sa lines propertijem      
            .ThenInclude(l=>l.Product);//kao i svaki product objekat

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
