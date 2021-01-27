using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuplementsStore1.Models
{
    //interfejs-nacin za dobavljanje podataka iz baze
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
