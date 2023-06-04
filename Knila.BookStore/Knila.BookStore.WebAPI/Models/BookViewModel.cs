using Faker;

namespace Knila.BookStore.WebAPI.Models
{
    public class BookViewModel
    {
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public decimal Price { get; set; }

        public string ModernLanguageAssociationCitation
        {
            get
            {
                return AuthorLastName + ", " + AuthorFirstName + "." + "“" + Title + "”" + "," + Publisher + ",";
            }
        }

        public string ChicagoManualOfStyleCitation
        {
            get
            {
                return AuthorLastName + ", " + AuthorFirstName + "." + "“" + Title + "”" + ",";
            }
        }
    }
}