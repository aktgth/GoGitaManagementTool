using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGitaReportsApp.Models
{
    public class Order
    {
        public string id { get; set; }
        public string parent_id { get; set; }
        public string status { get; set; }
        public string currency { get; set; }
        public string version { get; set; }
        public string prices_include_tax { get; set; }
        public string date_created { get; set; }
        public string date_modified { get; set; }
        public string discount_total { get; set; }
        public string discount_tax { get; set; }
        public string shipping_total { get; set; }
        public string shipping_tax { get; set; }
        public string cart_tax { get; set; }
        public string total { get; set; }
        public string total_tax { get; set; }
        public string customer_id { get; set; }
        public string order_key { get; set; }
        public AddressInfo billing { get; set; }
        public AddressInfo shipping { get; set; }
        public string payment_method { get; set; }
        public string payment_method_title { get; set; }
        public string transaction_id { get; set; }
        public string customer_ip_address { get; set; }
        public string customer_user_agent { get; set; }
        public string created_via { get; set; }
        public string customer_note { get; set; }
        public string date_completed { get; set; }
        public string date_paid { get; set; }
        public string cart_hash { get; set; }
        public string number { get; set; }
        public List<MetaInfo> meta_data { get; set; }
        public List<BookInfo> line_items { get; set; }
        public List<object> tax_lines { get; set; }
        public List<ShippingInfo> shipping_lines { get; set; }
        public List<object> fee_lines { get; set; }
        public List<object> coupon_lines { get; set; }
        public List<object> refunds { get; set; }
        public string payment_url { get; set; }
        public string is_editable { get; set; }
        public string needs_payment { get; set; }
        public string needs_processing { get; set; }
        public string date_created_gmt { get; set; }
        public string date_modified_gmt { get; set; }
        public string date_completed_gmt { get; set; }
        public string date_paid_gmt { get; set; }
        public string currency_symbol { get; set; }
    }

}
