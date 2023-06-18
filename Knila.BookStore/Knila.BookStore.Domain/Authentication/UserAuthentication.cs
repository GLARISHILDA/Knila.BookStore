namespace Knila.BookStore.Domain.Authentication
{
    public class UserAuthentication
    {
        public bool IsAuthentication { get; set; }

        public long UserId { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public int? AuthVersion { get; set; }

        public DateTime? LoggedOn { get; set; }

        public string CreatedIpAddress { get; set; }
    }
}