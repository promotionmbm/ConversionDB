using System;
using System.Collections;

namespace BaseDonneeConversion
{
    class BankersItem
    {
        public String sku { get; set; }
        public String productName { get; set; }
        public String productNameEn { get; set; }
        public String description { get; set; }
        public String descriptionEn { get; set; }
        public String lineName { get; set; }
        public String[] categories { get; set; }
        public String[] searchKeywords { get; set; }
        public String defaultImage { get; set; }
        public String[] colors { get; set; }
        public String size { get; set; }
        public ArrayList quantities { get; set; }
        public ArrayList prices { get; set; }

        public BankersItem()
        {
            quantities = new ArrayList();
            prices = new ArrayList();
        }
    }
}
