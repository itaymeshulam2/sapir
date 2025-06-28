namespace SapirServer.Helper;

public class GeneralHelper
{
    public static string GetBasePathLocation(string subFolder = null, bool shouldCreateFolder = true)
    {
        var res = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subFolder ?? "");
        if (shouldCreateFolder && !Directory.Exists(res))
        {
            Directory.CreateDirectory(res);
        }

        return res;
    }

    public static string GetPath()
    {
        return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
            "file/חוברת סיכום שנה ותיכנון החופש הגדול.pdf");
    }
}