using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knila.BookStore.Domain
{
    public class WebDomain
    {
        public string domain { get; set; }
        public string domain_id { get; set; }
        public string status { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
        public DateTime expire_date { get; set; }
        public int domain_age { get; set; }
        public string whois_server { get; set; }
        public Registrar registrar { get; set; }
        public Registrant registrant { get; set; }
        public Admin admin { get; set; }
        public Tech tech { get; set; }
        public Billing billing { get; set; }
        public List<string> nameservers { get; set; }
    }

    public class Admin
    {
        public string name { get; set; }
        public string organization { get; set; }
        public string street_address { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string zip_code { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
    }

    public class Billing
    {
        public string name { get; set; }
        public string organization { get; set; }
        public string street_address { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string zip_code { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
    }

    public class Registrant
    {
        public string name { get; set; }
        public string organization { get; set; }
        public string street_address { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string zip_code { get; set; }
        public string country { get; set; }

        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
    }

    public class Registrar
    {
        public string iana_id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Tech
    {
        public string name { get; set; }
        public string organization { get; set; }
        public string street_address { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string zip_code { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
    }
}