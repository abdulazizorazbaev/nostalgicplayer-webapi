namespace NostalgicPlayer.Service.Validators;

public class PasswordValidator
{
    private static string Symbols { get; set; } = "~`! @#$%^&*()_-+={[}]|\\:;\"'<,>.?/";

    public static (bool IsValid, string Message) IsStrongPassword(string password)
    {
        if (password.Length < 8) return (IsValid: false, Message: "Password cannot be less than 8 characters!");

        bool isUpperCaseExists = false;
        bool isLowerCaseExists = false;
        bool isDigitExists = false;
        bool isCharacterExists = false;

        foreach (var item in password)
        {
            if (char.IsUpper(item)) isUpperCaseExists = true;
            if (char.IsLower(item)) isLowerCaseExists = true;
            if (char.IsDigit(item)) isDigitExists = true;
            if (Symbols.Contains(item)) isCharacterExists = true;
        }
        if (isDigitExists == false) return (IsValid: false, Message: "Password should contain at least a digit!");
        if (isUpperCaseExists == false) return (IsValid: false, Message: "Password should contain at least an uppercase letter!");
        if (isLowerCaseExists == false) return (IsValid: false, Message: "Password should contain at least an uppercase letter!");
        if (isCharacterExists == false) return (IsValid: false, Message: "Password should contain at least a symbol!");

        return (IsValid: true, "");
    }
}