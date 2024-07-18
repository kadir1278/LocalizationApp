namespace LocalizationApp.Helper
{
    public static class ConfigurationHelper
    {
        #region Constructor
        private static IConfigurationRoot _configurationRoot;
        private static IConfigurationRoot configurationRoot
        {
            get
            {
                string jsonFile = "appsettings.json";

                if (_configurationRoot == null)
                    _configurationRoot = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile(jsonFile)
                        .Build();

                return _configurationRoot;
            }
        }
        #endregion

        public static string GetSqlConnectionString() => configurationRoot.GetConnectionString("SqlServerConnectionString");
    }
}
