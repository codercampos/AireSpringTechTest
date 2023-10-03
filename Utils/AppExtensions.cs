using System.Text.RegularExpressions;

namespace AireSpringTechTest.Utils;

public static partial class AppExtensions
{
    /// <summary>
    /// The template used to format a date element.
    /// </summary>
    private const string DateTemplate = "MM/dd/yyyy";
    
    /// <summary>
    /// The template used to format phone numbers in the app.
    /// </summary>
    private const string PhoneNumberTemplate = "($1) $2-$3";
    
    /// <summary>
    /// A Regex pattern to verify if the string being evaluated is a valid phone number.
    /// </summary>
    [GeneratedRegex("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
    private static partial Regex RegexValidPhoneNumber();
    
    /// <summary>
    /// A Regex pattern to verify if the string being evaluated is a valid phone number with the following sections: ### ### ####.
    /// </summary>
    [GeneratedRegex("(\\d{3})(\\d{3})(\\d{4})")]
    private static partial Regex PhoneFormat();

    /// <summary>
    /// An extension that formats a DateTime element into the specified template.
    /// </summary>
    /// <returns>The date with the desired format in DateTemplate</returns>
    public static string FormatDate(this DateTime dateTime)
    {
        return dateTime.ToString(DateTemplate);
    }

    /// <summary>
    /// An extension that formats an string element into a phone number. If the phone number is invalid, it returns the original string.
    /// </summary>
    /// <returns>The phone number with the desired format in DateTemplate</returns>
    public static string FormatPhoneNumber(this string phoneNumber)
    {
        Regex regex = new Regex(@"[^\d]");
        phoneNumber = regex.Replace(phoneNumber, PhoneNumberTemplate);

        if (IsCorrectPhoneFormat(phoneNumber))
        {
            phoneNumber = PhoneFormat().Replace(phoneNumber, "");
        }

        return phoneNumber;
    }
    
    /// <summary>
    /// Verifies if the phone phone number has the correct format.
    /// </summary>
    private static bool IsCorrectPhoneFormat(string phone)
    {
        return !string.IsNullOrEmpty(phone) && RegexValidPhoneNumber().IsMatch(phone);
    }
}