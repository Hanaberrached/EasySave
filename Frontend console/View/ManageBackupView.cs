using Backend.Backup;

namespace Frontend_console.View
{

    /// <summary>
    /// Represents the view for managing backups in the console application.
    /// </summary>
    public class ManageBackupView
    {

        /// <summary>
        /// Displays prompts and options for managing a specific backup using the provided BackupManager.
        /// </summary>
        /// <param name="backup">The backup instance to be managed.</param>
        /// <param name="backupManager">The instance of BackupManager for managing backups.</param>
        public static void ManageBackup(ABackup backup , BackupManager backupManager)
        {
            Console.WriteLine(string.Format(backupManager.settings.LanguageSettings.LanguageData["managing_backup"], backup.Name));
            Console.WriteLine(string.Format(backupManager.settings.LanguageSettings.LanguageData["current_state"], backup.State.State));

            backup.ScanFiles();

            switch (backup.State.State)
            {
                case EnumState.NotStarted:
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["options"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["launch_backup"]);
       
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["back_option"]);
                    break;
                case EnumState.InProgress:
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["options"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["pause_backup"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["cancel_backup"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["back_option"]);
                    break;
                case EnumState.Paused:
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["options"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["cancel_backup"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["resume_backup"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["back_option"]);
                    break;
                case EnumState.Finished:
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["options"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["launch_backup"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["back_option"]);
                    break;
                case EnumState.Failed:
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["options"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["retry_backup"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["back_option"]);
                    break;
                case EnumState.Cancelled:
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["options"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["launch_backup"]);
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["back_option"]);
                    break;
                default:
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["invalid_state"]);
                    break;
            }

            Console.Write(backupManager.settings.LanguageSettings.LanguageData["enter_your_choice"]);
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    backup.PerformBackup();
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["invalid_choice"]);
                    break;
            }
        }
    }
}
