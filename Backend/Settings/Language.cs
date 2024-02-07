using System.Text.Json;

namespace Backend.Settings
{

    /// <summary>
    /// Class representing language management in the application.
    /// </summary>
    public class Language
    {
        public string LanguageFile { get; set; }
        public EnumLanguages CurrentLanguage { get; set; }
        public Dictionary<string, string> LanguageData { get; set; }


        /// <summary>
        /// Constructor of the Language class.
        /// </summary>
        public Language()
        {
            CurrentLanguage = EnumLanguages.FR;
            string basePath = AppDomain.CurrentDomain.BaseDirectory; // Get the local path of the app

            string languageCode = ConvertEnumToLanguageCode(CurrentLanguage);

            LanguageFile = Path.Combine(basePath, "Data", "languages", $"{languageCode}.json"); // Create a local path to the json file language
            
            CreateLanguageFile();
        }


        /// <summary>
        /// Converts the language enum to a language code used in the file name.
        /// </summary>
        /// <param name="language">Enum representing the language.</param>
        /// <returns>Language code.</returns>
        public string ConvertEnumToLanguageCode(EnumLanguages language)
        {
            string code = language.ToString().ToLower();
            return $"{code}-{code.ToUpper()}";
        }
        /// <summary>
        /// Creates the language file if it does not exist and loads the language data.
        /// </summary>
        public void CreateLanguageFile()
        {
            if (LanguageData == null || !LanguageData.Any())
            {
                loadFileLocal();
            }
        }

        /// <summary>
        /// Loads the language file locally from the file system.
        /// </summary>
        public void loadFileLocal()
        {
            if (File.Exists(LanguageFile)) // Verify if the file exist
            {
                string jsonString = File.ReadAllText(LanguageFile);

                try
                {
                    LanguageData = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString); //affect deserialized data to the LanguageData property
                }
                catch (JsonException e)
                {
                    Console.WriteLine($"An error occurred during JSON deserialization: {e.Message}");
                }

            }
            else
            {
                Console.WriteLine("Language file not found");
            }
        }
    }
}
