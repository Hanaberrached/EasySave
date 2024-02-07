using Backend.Backup;

namespace Frontend_console.View
{

    /// <summary>
    /// Represents the view for managing and listing backups in the console application.
    /// </summary>
    public class BackupsView
    {
        /// <summary>
        /// Lists all available backups and provides options for managing and launching them.
        /// </summary>
        /// <param name="backupManager">The instance of BackupManager containing the backup list.</param>
        public static void ListBackups(BackupManager backupManager)
        {
            Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["list_of_backups"]);

            if (backupManager.BackupList.Count == 0)
            {
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["no_backups_available"]);
            }
            else
            {
                foreach (var backup in backupManager.BackupList)
                {
                    Console.WriteLine($"- {backup.Name} ({backup.GetType().Name}):");
                    Console.WriteLine(string.Format(backupManager.settings.LanguageSettings.LanguageData["source_directory"], backup.SourceDirectory));
                    Console.WriteLine(string.Format(backupManager.settings.LanguageSettings.LanguageData["target_directory"], backup.TargetDirectory));
                }
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["options"]);
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["go_back"]);
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["manage_backup"]);
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["launch_all_backups"]);

                string userInputOption = Console.ReadLine();

                switch (userInputOption)
                {
                    case "1":
                        // Do nothing, simply return to the main menu
                        break;
                    case "2":
                        Console.WriteLine(string.Format(backupManager.settings.LanguageSettings.LanguageData["backup_number_prompt"], backupManager.BackupList.Count));
                        string userInputBackup = Console.ReadLine();
                        if (int.TryParse(userInputBackup, out int backupIndex) && backupIndex > 0 && backupIndex <= backupManager.BackupList.Count)
                        {
                            // Pass the selected backup and the instance of BackupManager
                            ManageBackupView.ManageBackup(backupManager.BackupList[backupIndex - 1], backupManager);
                        }
                        else
                        {
                            Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["invalid_backup_number"]);
                        }
                        break;
                    case "3":
                        backupManager.PerformAllBackups();
                        break;
                    default:
                        Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["invalid_choice"]);
                        break;
                }
            }
        }
    }
}
