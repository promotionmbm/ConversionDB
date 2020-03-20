using System;
using System.Collections;

namespace WindowsFormsApp1
{
    class DebcoItem
    {
        public String itemID { get; set; }
        public String nameEn { get; set; }
        public String nameFr { get; set; }
        public String descriptionEn { get; set; }
        public String descriptionFr { get; set; }
        public ArrayList quantities { get; set; }
        public ArrayList prices { get; set; }
        public String[] colors { get; set; }
        public String category { get; set; }
        public String majorCategory { get; set; }
        public String imageURL { get; set; }
    }
}
