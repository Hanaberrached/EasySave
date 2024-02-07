using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.IO;
using FrontendWPF;
using Backend.Backup;
using Microsoft.Win32; 


namespace WpfApp1
{
    /// <summary>
    /// PageNew is a class representing the new backup configuration page.
    /// </summary>
    public partial class PageNew : Page
    {

        
        private Dictionary<string, string> localizedResources;
        private BackupManager backupManager;

        /// <summary>
        /// Constructor for the PageNew class.
        /// </summary>
        /// <param name="backupManager">The backup manager used by the page.</param>
        public PageNew(BackupManager backupManager)
        {
            InitializeComponent();
            UpdateLanguage(App.CurrentLanguage); // Use global language setting
            App.LanguageChanged += UpdateLanguage; // Subscribe to the global event
            this.backupManager = backupManager;

        }

        /// <summary>
        /// Event triggered when an instance of PageNew is unloaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageNew_Unloaded(object sender, RoutedEventArgs e)
        {
            App.LanguageChanged -= UpdateLanguage; // Unsubscribe from the global event
        }

        /// <summary>
        /// Click event for the "Create Backup" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateBackup_Click(object sender, RoutedEventArgs e)
        {
            string backupName = txtBackupName.Text;
            string sourceDirectory = txtSourceDirectory.Text;
            string targetDirectory = txtTargetDirectory.Text;
            string backupType = (string)cmbBackupType.SelectedValue;

            if (string.IsNullOrEmpty(backupName) || string.IsNullOrEmpty(sourceDirectory) ||
                string.IsNullOrEmpty(targetDirectory) || string.IsNullOrEmpty(backupType))
            {
                MessageBox.Show(localizedResources["FillAllFieldsAndSelectType"]);
                return;
            }

            // Validate input
            if (string.IsNullOrWhiteSpace(backupName) || string.IsNullOrWhiteSpace(sourceDirectory) || string.IsNullOrWhiteSpace(targetDirectory))
            {
                MessageBox.Show(localizedResources["FillAllFieldsAndSelectType"]);
                return;
            }

            if (!Directory.Exists(sourceDirectory))
            {
                MessageBox.Show(localizedResources["SourceDirectoryNotExist"]);
                return;
            }

            if (!Directory.Exists(targetDirectory))
            {
                MessageBox.Show(localizedResources["TargetDirectoryNotExist"]);
                return;
            }
            // if the paths of  source and destination are the same 

            if (sourceDirectory.Equals(targetDirectory, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(localizedResources["SourceTargetSame"]);
                return;
            }
            // Translate backup type to a type recognized by the backend
            string backendBackupType = backupType == localizedResources["totalSave"] ? "1" : "2";

            // Add the backup configuration
            try
            {
                backupManager.AddBackup(backendBackupType, backupName, sourceDirectory, targetDirectory);
                MessageBox.Show(localizedResources["BackupConfigSaved"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(localizedResources["FailedToSaveBackup"], ex.Message));
            }


        }

        /// <summary>
        /// Click event for the "Browse Source" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseSource_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ValidateNames = false;
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.FileName = "Dossier sélection"; 

            if (openFileDialog.ShowDialog() == true)
            {
                txtSourceDirectory.Text = Path.GetDirectoryName(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Click event for the "Browse Target" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseTarget_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ValidateNames = false; 
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.FileName = "Dossier sélection";

            if (openFileDialog.ShowDialog() == true)
            {
                txtTargetDirectory.Text = Path.GetDirectoryName(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Updates the language of the page based on the specified culture code.
        /// </summary>
        /// <param name="cultureCode">The culture code specifying the language.</param>
        private void UpdateLanguage(string cultureCode)
        {
            string relativePath = $"../../../../Backend/Data/Languages/{cultureCode}.json";
            string fullPath = Path.GetFullPath(relativePath, AppDomain.CurrentDomain.BaseDirectory);

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                localizedResources = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                if (localizedResources != null)
                {
                    gbNewBackup.Header = localizedResources["NewBackupHeader"];
                    saveName.Text = localizedResources["saveName"];
                    saveType.Text = localizedResources["saveType"];
                    totalSave.Content = localizedResources["totalSave"];
                    diffSave.Content = localizedResources["diffSave"];
                    btnBrowseSource.Content = localizedResources["browse"];
                    sourcePath.Text = localizedResources["sourcePath"];
                    destinationPath.Text = localizedResources["destinationPath"];
                    btnBrowseTarget.Content = localizedResources["browse2"];
                    btnCreateBackup.Content = localizedResources["createSave"];

                }
            }
            else
            {
                MessageBox.Show($"Language file not found: {fullPath}");
            }
        }
    }
}