using System.Text.RegularExpressions;

namespace CompanyManagementSystem.Infrastructure.Helpers;

public static partial class RegExHelper
{
    public const string DateTimeFormat = "dd.MM.yyyy HH:mm";
    public const string DateFormat = "dd.MM.yyyy";

    [GeneratedRegex(@"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{0,10}$")]
    public static partial Regex UserNameRegex();
    [GeneratedRegex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
    public static partial Regex PasswordRegex();
    [GeneratedRegex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")]
    public static partial Regex EmailRegex();
}
