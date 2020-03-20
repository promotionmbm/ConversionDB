using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class ItemCategorie
    {
        public String nomCategorie { get; set; }
        public String[] motsCategorie { get; set; }
        public String categorieParent { get; set; }
        public String categorieContext { get; set; }

        public ItemCategorie()
        {

        }

        public ItemCategorie(String nom, String[] mots, String parent, String context)
        {
            nomCategorie = nom;
            motsCategorie = mots;
            categorieParent = parent;
            categorieContext = context;
        }
    }
}
