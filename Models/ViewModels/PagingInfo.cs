using System;

namespace SuplementsStore1.Models.ViewModels
{
    //klasa sluzi za prosleđivanje podataka o broju stanica, trentnoj starnici.. od kontrolera ka pogledu
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages =>
        (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
