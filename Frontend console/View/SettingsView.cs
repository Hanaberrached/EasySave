using Backend.Backup;
using Backend.Settings;

namespace Frontend_console.View
{

    /// <summary>
    /// Represents the view for accessing and modifying application settings in the console application.
    /// </summary>
    public class SettingsView
    {

        /// <summary>
        /// Displays prompts and options for accessing and modifying application settings.
        /// </summary>
        /// <param name="backupManager">The instance of BackupManager for managing backups.</param>
        public static void AccessSettings(BackupManager backupManager)
        {
            string inputLanguage;
            string inputlogs;

            Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["enter_settings"]);
            Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["go_back"]);
            Console.WriteLine("2. " + backupManager.settings.LanguageSettings.LanguageData["logs_format_title"]);
            Console.WriteLine("3. " + backupManager.settings.LanguageSettings.LanguageData["language_title"]);
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    break;
                case "2":
                    Console.WriteLine("Choisissez le ou les formats de logs à activer/désactiver :");

                    // Display all available log formats
                    foreach (var enum_log in Enum.GetValues(typeof(EnumLogFormat)))
                    {
                        Console.WriteLine(enum_log);
                    }
                    inputlogs = Console.ReadLine();

                    if (Enum.TryParse(inputlogs, true, out EnumLogFormat selectedLogFormat))
                    {
                        bool currentState = backupManager.settings.LogSettings.GetLogFormatState(selectedLogFormat);
                        Console.WriteLine($"Le format de log '{selectedLogFormat}' est actuellement {(currentState ? "activé" : "désactivé")}.");

                        // Ask the user to modify the state
                        Console.WriteLine("Entrez 'true' pour activer ou 'false' pour désactiver ce format de log:");
                        string newStateInput = Console.ReadLine();
                        if (bool.TryParse(newStateInput, out bool newState))
                        {
                            if (currentState == newState)
                            {
                                Console.WriteLine($"Ce format de log est déjà sur '{newState}'.");
                            }
                            else
                            {
                                backupManager.settings.LogSettings.SetLogFormatState(selectedLogFormat, newState);
                                Console.WriteLine($"Le format de log '{selectedLogFormat}' a été {(newState ? "activé" : "désactivé")}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Entrée invalide. Veuillez entrer 'true' ou 'false'.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sélection de logs invalide.");
                    }
                    break;
                case "3":
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["choose_language"]);

                    // Display all available languages
                    foreach (var enum_language in Enum.GetValues(typeof(EnumLanguages)))
                    {
                        Console.WriteLine(enum_language);
                    }

                    // Get user input
                    inputLanguage = Console.ReadLine();

                    // Try to convert the input into EnumLanguages
                    if (Enum.TryParse(inputLanguage, true, out EnumLanguages selectedLanguage))
                    {
                        // Call SetLanguage with the selected language
                        backupManager.settings.SetLanguage(selectedLanguage);
                        Console.WriteLine("Language changed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid language selection.");
                    }
                    break;

                default:
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["invalid_choice"]);
                    break;
            }





        }

    }
}
