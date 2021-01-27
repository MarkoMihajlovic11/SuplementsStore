using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SuplementsStore1.Infrastructure;
using System;
using Newtonsoft.Json;

namespace SuplementsStore1.Models
{
    //prvi korak koriscenju servisa za cart
    public class SessionCart : Cart
    {
        //omogućava kreiranje SessionCart objekata, kako bi se objekti mogli skladištiti
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")
            ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }
        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}