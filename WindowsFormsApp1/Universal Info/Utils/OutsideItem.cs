using BaseDonneeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Universal_Info.Utils
{
    public interface OutsideItem
    {
        ProduitDB getProduit();
        ProduitDB[] getChildProducts();
    }
}
