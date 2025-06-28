using Serilog;

namespace SapirServer.Model
{
    public class SettingsDetails
    {

        public static void LoadAllSettings()
        {
            Log.Information("Load SettingsDetails");
            var a = EmailPassword;
            Log.Information("Done Load SettingsDetails");
        }

        public const string DATE_FORMAT_SHORT = "yyyy-MM-dd";
        public const string DATE_FORMAT_LONG = "yyyy-MM-dd HH:mm:ss";
        

        private static string _EmailPassword;
        public static string EmailPassword
        {
            get
            {
                if (string.IsNullOrEmpty(_EmailPassword))
                {
                    _EmailPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
                    Log.Information($"EmailPassword: [{_EmailPassword}]");
                }
                return _EmailPassword;
            }
        }
    }
}
