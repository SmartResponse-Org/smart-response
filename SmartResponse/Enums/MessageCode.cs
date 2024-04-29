using System.ComponentModel;

namespace SmartResponse.Enums
{
    public enum MessageCode
    {
        // Input Validation

        /// <summary>Invalid {property}</summary>
        [Description("SR-IV-6000")]
        InvalidValue = 6000,

        /// <summary>{property} Is Required</summary>
        [Description("SR-IV-6001")]
        Required = 6001,
        
        /// <summary>{property} must be between {min} and {max}</summary>
        [Description("SR-IV-6002")]
        InbetweenValue = 6002,

        /// <summary>{property} must be greater than {min}</summary>
        [Description("SR-IV-6003")]
        InvalidMinLength = 6003,

        /// <summary>{property} must be less than {max}</summary>
        [Description("SR-IV-6004")]
        InvalidMaxLength = 6004,

        /// <summary>{property} must contain at least one digit</summary>
        [Description("SR-IV-6007")]
        MustContainAtLeastOneDigit = 6007,

        /// <summary>{property} must contain at least one capital letter</summary>
        [Description("SR-IV-6008")]
        MustContainAtLeastOneCapitalLetter = 6008,

        /// <summary>{property} must contain at least one small letter</summary>
        [Description("SR-IV-6009")]
        MustContainAtLeastOneSmallLetter = 6009,

        /// <summary>{property} must contain at least one special character</summary>
        [Description("SR-IV-6010")]
        MustContainAtLeastOneSpecialCharacter = 6010,

        /// <summary>{Key} must have at least {count} selected</summary>
        [Description("SR-IV-6011")]
        MustSelectAtLeastItem = 6011,
        
        //Business Validation
        
        /// <summary>{key} already exist</summary>
        [Description("SR-BV-7001")]
        AlreadyExist = 7001,

        /// <summary>{key} not found</summary>
        [Description("SR-BV-7002")]
        NotFound = 7002,

        /// <summary>{key} not supported</summary>
        [Description("SR-BV-7003")]
        NotSupported = 7003,
        
        /// <summary>This action is not allowed</summary>
        [Description("SR-BV-7004")]
        NotAllowedAction = 7004, 
        
        /// <summary>{key} not verified</summary>
        [Description("SR-BV-7005")]
        NotVerified = 7005,

        /// <summary>{key} already verified</summary>
        [Description("SR-BV-7006")]
        AlreadyVerified = 7006,

        // expired, not expired, exceed limit, not available, not identical, x must be greater than y and vice versa, 
        [Description("Failed :Send count exceeded")]
        SendCountExceeded = 7022,
        [Description("Failed :Phone code expired")]
        PhoneCodeExpired = 7023,
        [Description("Failed :Verification link expired")]
        VerificationLinkExpired = 7024,
        [Description("Failed :Invalid verification link")]
        InvalidVerificationLink = 7025,
        [Description("Failed :Invalid login credentials")]
        InvalidLoginCredentials = 7026,
        [Description("Failed :Invalid token")]
        InvalidToken = 7027,
        [Description("Failed :Token not expired yet")]
        TokenNotExpiredYet = 7028,
        [Description("Failed :Token and refresh token don't match")]
        TokensDoNotMatch = 7029,
        [Description("Failed :Email not verified")]
        EmailNotVerified = 7030,
        [Description("Failed :Password already defined for you before")]
        NewPasswordAlreadyDefined = 7031,
        [Description("Failed :Email is already primary")]
        EmailIsAlreadyPrimary = 7032,
        [Description("Failed :{0} Is InActive .. You must activate one at least")]
        ActiveEntityCount = 7033,
        [Description("Failed :{0} Is Default .. You must select another one to be default first")]
        DefaultEntityCount = 7034,
        [Description("Failed : Invalid password or link")]
        InvalidPasswordOrLink = 7035,
        [Description("Failed :{0} has unpaid invoices")]
        HasUnPaidInvoicesCount = 7036,
        [Description("Failed :There is an Unpaid {0} ")]
        HasUnPaidInvoices = 7037,
        [Description("Failed :Please, correct device serial")]
        OldEqualNewDevice = 7038,
        [Description("Failed : licenses limit has been exceeded")]
        ExceededLicensesLimit = 7039,
        [Description("Failed :You have exceeded the validity period")]
        ExceededPeriod = 7040,
        [Description("Failed : {0} status is Invalid")]
        InvalidInvoiceStatus = 7041,
        [Description("Failed : {0} Not Available in your Country")]
        NotAvailableInYourCountry = 7042,
        [Description("Failed : {0} Is Not Generated Yet")]
        IsNotGenerated = 7043,
        [Description("Failed : New Password not identical to Confirm Password")]
        MismatchNewConfirmPassword = 7044,
        [Description("Failed : license is not expired yet")]
        LicenseNotExpiredYet = 7045,
        [Description("Failed : There's no paid invoice.")]
        NoPaidInvoice = 7046,
        [Description("Failed : Must Purchase Version First.")]
        MustPurchaseVersionFirst = 7047,
        [Description("Failed : has more than one invoice")]
        HasMoreThanOneInvoice = 7048,
        [Description("Failed : Sorry the selected role is used.")]
        RoleAssign = 7049,
        [Description("Failed : request is already refunded.")]
        RequestAlreadyRefunded = 7050,
        [Description("Failed : request has been rejected.")]
        RequestAlreadyRejected = 7051,
        [Description("Failed : EndDate must be greater than StartDate.")]
        DatePeriod = 7052,
        [Description("Failed : Payment Failed.")]
        PaymentFailed = 7053,
        [Description("Failed : There're related addons have not been refunded yet.")]
        RelatedAddonNotRefunded = 7054,
        [Description("Failed : Invalid payment method.")]
        InvalidPaymentMethod = 7055,
        [Description("Failed : Can't Deactivate Featured Tag.")]
        DeactivateFeaturedTag = 7056,
        [Description("Failed :{0} Not Authroize show this page .")]
        PageNotAllowed = 7057,
        [Description("Failed :Page or Action not selected .")]
        PageNotSelected = 7058,
        [Description("Failed :You Must Select At least one country .")]
        CountrySelected = 7059,
        [Description("Failed :Email belongs to other mobile.")]
        EmailBelongsToOtherMobile = 7060,
        [Description("Failed :Mobile belongs to other email.")]
        MobileBelongsToOtherEmail = 7061,
        [Description("Failed :You must select paid versionSupscription.")]
        PaidVersionsubscription = 7062,
    }
}
