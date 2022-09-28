using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGitaReportsApp.Models
{
    public class BookInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string product_id { get; set; }
        public string variation_id { get; set; }
        public int quantity { get; set; }
        public string tax_class { get; set; }
        public string subtotal { get; set; }
        public string subtotal_tax { get; set; }
        public string total { get; set; }
        public string total_tax { get; set; }
        public List<object> taxes { get; set; }
        public List<MetaInfo> meta_data { get; set; }
        public string sku { get; set; }
        public string price { get; set; }
        public ImageInfo image { get; set; }
        public string parent_name { get; set; }
    }
}
