using Backend.Backup;
using FrontendWPF;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace WpfApp1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private Dictionary<string, string> localizedResources;
        private BackupManager backupManager = new BackupManager();

        /// <summary>
        /// Constructor for the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            backupManager.AddBackup("1", "monke", "C:\\Users\\romeo\\OneDrive\\Bureau\\test", "C:\\Users\\romeo\\OneDrive\\Bureau\\Test2");
            backupManager.AddBackup("1", "monke", "C:\\Users\\romeo\\OneDrive\\Bureau\\test", "C:\\Users\\romeo\\OneDrive\\Bureau\\Test3");
            backupManager.AddBackup("1", "monke", "C:\\Users\\romeo\\OneDrive\\Bureau\\test", "C:\\Users\\romeo\\OneDrive\\Bureau\\Test4");

            InitializeComponent();
            UpdateLanguage(App.CurrentLanguage); // Use global language setting
            App.LanguageChanged += UpdateLanguage; // Subscribe to the global event

            MainContentFrame.Navigate(new PageTrack(backupManager));
        }

        /// <summary>
        /// Updates the language of the MainWindow based on the specified culture code.
        /// </summary>
        /// <param name="cultureCode"></param>
        private void UpdateLanguage(string cultureCode)
        {
            // Form the relative path from the executable to the JSON files
            string relativePath = $"../../../../Backend/Data/Languages/{cultureCode}.json";
            string fullPath = Path.GetFullPath(relativePath, AppDomain.CurrentDomain.BaseDirectory);

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                localizedResources = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                if (localizedResources != null)
                {
                    NewSaveButton.Content = localizedResources["NewBackup"];
                    LogsButton.Content = localizedResources["AccessLogs"];
                    SettingsButton.Content = localizedResources["Settings"];
                    TrackSavesButton.Content = localizedResources["TrackSaves"];
                    CloseButton.Content = localizedResources["CloseButton"];
                }
            }
            else
            {
                MessageBox.Show($"Language file not found: {fullPath}");
            }
        }

        /// <summary>
        /// Event handler for the CloseButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the application when the CloseButton is clicked
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Event handler for the NewSaveButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewSaveButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new PageNew(backupManager));
        }

        /// <summary>
        /// Event handler for the SettingsButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new PageParam());
        }

        /// <summary>
        /// Event handler for the LogsButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogsButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("LogsButton_Click called");

            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            try
            {
                if (Directory.Exists(logPath))
                {
                    System.Diagnostics.Debug.WriteLine("Directory exists, attempting to open.");


                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = "explorer.exe",
                        Arguments = logPath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Le dossier des logs n'existe pas dans le répertoire de l'application.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Impossible d'ouvrir le dossier des logs : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Event handler for the TracksButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TracksButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new PageTrack(backupManager));
        }
    }
}
