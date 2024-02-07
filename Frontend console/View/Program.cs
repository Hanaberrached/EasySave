//Frontend console version
using Backend.Backup;

using Frontend_console.View;

BackupManager backupManager;
backupManager = new BackupManager();
bool run = true;

while (run)
{
    try
    {

        Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["choose_options"]);
        Console.WriteLine("1. " + backupManager.settings.LanguageSettings.LanguageData["new_backup_access"]);
        Console.WriteLine("2. " + backupManager.settings.LanguageSettings.LanguageData["settings_access"]);
        Console.WriteLine("3. " + backupManager.settings.LanguageSettings.LanguageData["my_backups_title"]);
        Console.WriteLine("4. " + backupManager.settings.LanguageSettings.LanguageData["logs_access"]);
        Console.WriteLine("5. " + backupManager.settings.LanguageSettings.LanguageData["exit"]);

        string userInput = Console.ReadLine();

        switch (userInput)
        {
            case "1":
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["creating_backup"]);
                CreateBackupView.CreateBackup(backupManager);
                break;
            case "2":
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["accessing_settings"]);
                SettingsView.AccessSettings(backupManager);
                break;
            case "3":
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["accessing_backups"]);
                BackupsView.ListBackups(backupManager);
                break;
            case "4":
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["accessing_logs"]);

                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string logFolder = Path.Combine(basePath, "Logs");

                if (Directory.Exists(logFolder))
                {                  
                    System.Diagnostics.Process.Start("explorer.exe", logFolder);
                }
                else
                {
                    Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["logs_folder_doest_exist"]);
                }
                break;
            case "5":
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["exiting"]);
                run = false;
                break;
            default:
                Console.WriteLine(backupManager.settings.LanguageSettings.LanguageData["invalid_choice"]);
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error : {ex.Message}");
        run = false;
    }
}