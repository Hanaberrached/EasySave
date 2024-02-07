
namespace Backend.Settings
{
    /// <summary>
    /// Singleton class responsible for managing application settings.
    /// </summary>
    public class Settings
    {
        // Private static instance of the singleton
        private static Settings _instance;
        public Language LanguageSettings { get; set; }
        public Logs LogSettings { get; set; }
        public List<string> PriorityExtensionsToBackup { get; set; }
        public List<string> ExtensionsToEncrypt { get; set; }
        public int MaxParallelTransferSizeKB { get; set; }
        private int cumulativeTransferSizeKB = 0;
        private readonly object lockObject = new object();

        public string BusinessSoftware { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        private Settings()
        {
            // Initialize the default language, can be changed later
            LanguageSettings = new Language();
            LogSettings = new Logs();
            ExtensionsToEncrypt = new List<string>();
            PriorityExtensionsToBackup = new List<string>();
            AddExtensionsToEncrypt(".txt");
            AddPriorityExtensionToBackup(".png");
            MaxParallelTransferSizeKB = 2048;
            BusinessSoftware = "CalculatorApp";
        }

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static Settings GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Settings();
            }
            return _instance;
        }

        /// <summary>
        /// Gets the current language.
        /// </summary>
        /// <returns>The current language.</returns>
        public EnumLanguages GetLanguage()
        {
            return LanguageSettings.CurrentLanguage;
        }

        /// <summary>
        /// Sets the application language.
        /// </summary>
        /// <param name="newLanguage">The new language to set.</param>
        public void SetLanguage(EnumLanguages newLanguage)
        {
            LanguageSettings.CurrentLanguage = newLanguage;
            string languageCode = LanguageSettings.ConvertEnumToLanguageCode(newLanguage);
            LanguageSettings.LanguageFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "languages", $"{languageCode}.json");
            LanguageSettings.loadFileLocal();
            LanguageSettings.CreateLanguageFile();
        }

        /// <summary>
        /// Adds an extension to the list of extensions to encrypt.
        /// </summary>
        /// <param name="extension">The extension to add.</param>
        public void AddExtensionsToEncrypt(string extension)
        {
            ExtensionsToEncrypt.Add(extension);
        }

        /// <summary>
        /// Removes an extension from the list of extensions to encrypt.
        /// </summary>
        /// <param name="extension">The extension to remove.</param>
        public void RemoveExtensionToEncrypt(string extension)
        {
            ExtensionsToEncrypt.Remove(extension);
        }

        /// <summary>
        /// Adds a priority extension to the list of extensions to backup.
        /// </summary>
        /// <param name="extension">The priority extension to add.</param>
        public void AddPriorityExtensionToBackup(string extension)
        {
            PriorityExtensionsToBackup.Add(extension);
        }

        /// <summary>
        /// Removes a priority extension from the list of extensions to backup.
        /// </summary>
        /// <param name="extension">The priority extension to remove.</param>
        public void RemovePriorityExtensionToBackup(string extension)
        {
            PriorityExtensionsToBackup.Remove(extension);
        }

        /// <summary>
        /// Sets the file name of the business software.
        /// </summary>
        /// <param name="fileName">The file name of the business software.</param>
        public void SetBusinessSoftware(string fileName)
        {
            BusinessSoftware = fileName;
        }

        /// <summary>
        /// Retrieves the file name of the business software.
        /// </summary>
        /// <returns>The file name of the business software.</returns>
        public string GetBusinessSoftware()
        { 
            return BusinessSoftware; 
        }

        /// <summary>
        /// Gets or sets the cumulative transfer size in kilobytes.
        /// </summary>
        public int CumulativeTransferSizeKB
        {
            get
            {
                lock (lockObject)
                {
                    return cumulativeTransferSizeKB;
                }
            }
            set
            {
                lock (lockObject)
                {
                    cumulativeTransferSizeKB = value;
                }
            }
        }

        /// <summary>
        /// Sets the maximum parallel transfer size in kilobytes.
        /// </summary>
        /// <param name="maxkB">The maximum parallel transfer size in kilobytes to be set.</param>
        public void SetMaxParallelTransferSizeKB(int maxkB)
        {
            MaxParallelTransferSizeKB = maxkB;
        }
    }
}
