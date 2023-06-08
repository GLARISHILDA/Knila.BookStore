namespace Knila.BookStore.Domain.Authentication
{
    //Poco
    public class User
    {
        public long UserId { get; set; }  //Properties

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string CountryName { get; set; }

        public string Password { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public int? AuthVersion { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsBlocked { get; set; }

        public bool? IsEmailVerified { get; set; }

        public bool? IsForgetPasswordActive { get; set; }

        public string ShowNotification { get; set; }

        public bool? IsAgreePrivacyAndSecurity { get; set; }

        public string ReferralId { get; set; }

        public string UserGUID { get; set; }

        public string UserAESKey { get; set; }

        public string UserAESIv { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? CreatedBy { get; set; }

        public string CreatedIpAddress { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public long? ModifiedBy { get; set; }

        public string ModifiedIpAddress { get; set; }
    }
}