using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuplementsStore1.Models
{
    //interfejs- prvi korak u popunjavanju baze podataka
    public interface ISeedData
    {
        void EnsurePopulated();
    }
}
