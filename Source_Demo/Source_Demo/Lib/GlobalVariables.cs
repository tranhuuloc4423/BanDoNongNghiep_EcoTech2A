namespace Source_Demo.Lib
{
    public static class GlobalVariables
    {
        public static bool is_development = true;

        public static readonly string defaultCountryId = "1"; //VN
        public static string url_api = "https://api-v2-geoapp.ecotech2a.com/";
        public static string url_api_image = "https://image-v2-geoapp.ecotech2a.com/";

        public static string logo_url = "/images/h2a-logo.png";
        public static string logo_url_small = "/images/h2a-logo-sm.png";
        public static string favicon_url = "/favicon-h2a.ico";
        public static void SetVariablesEnviroment(bool isDevelop)
        {
            is_development = isDevelop;
            if (isDevelop)
            {
                url_api = "https://api-interns.h2aits.com/";
                logo_url = "/images/h2a-logo.png";
                logo_url_small = "/images/h2a-logo-sm.png";
                favicon_url = "/favicon-h2a.ico";
            }
            else
            {
                url_api = "https://api-interns.h2aits.com/";
                logo_url = "/images/logo.jpg";
                logo_url_small = "/images/logo-sm.png";
                favicon_url = "/favicon.ico";
            }
        }
    }
}
