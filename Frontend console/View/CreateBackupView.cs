using Backend.Backup;

namespace Frontend_console.View
{

    /// <summary>
    /// Represents the view for creating a new backup in the console application.
    /// </summary>
    public class CreateBackupView
    {

        /// <summary>
        /// Displays prompts and collects user input to create a new backup using the provided BackupManager.
        /// </summary>
        /// <param name="backupManager">The instance of BackupManager for managing backups.</param>
        public static void CreateBackup(BackupManager backupManager)
        {
            Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["creating_new_backup"]);
            string backupNumber;
            string backupType;
            do
            {
                Console.WriteLine($"{backupManager.settings.LanguageSettings.LanguageData["enter_backup_type"]}");
                Console.WriteLine($"1. {backupManager.settings.LanguageSettings.LanguageData["backup_full"]}");
                Console.WriteLine($"2. {backupManager.settings.LanguageSettings.LanguageData["backup_differential"]}");
                backupNumber = Console.ReadLine();

                if (backupNumber != "1" && backupNumber != "2")
                {
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["invalid_backup_type_prompt"]);
                }
            } while (backupNumber != "1" && backupNumber != "2");
            Console.Write(backupManager.settings.LanguageSettings.LanguageData["enter_backup_name"]);
            string name = Console.ReadLine();
            Console.Write(backupManager.settings.LanguageSettings.LanguageData["enter_source_directory"]);
            string sourceDirectory = Console.ReadLine();
            Console.Write(backupManager.settings.LanguageSettings.LanguageData["enter_target_directory"]);
            string targetDirectory = Console.ReadLine();

            try
            {
                backupManager.AddBackup(backupNumber, name, sourceDirectory, targetDirectory);
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["backup_created_successfully"]);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(string.Format(backupManager.settings.LanguageSettings.LanguageData["error_creating_backup"], ex.Message));

            }
        }
    }
}
