using System.Windows;

namespace FrontendWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex mutex;
        public static string CurrentLanguage { get; set; } = "fr-FR";

        public static event Action<string> LanguageChanged;
        public static void OnLanguageChanged(string newLanguage)
        {
            CurrentLanguage = newLanguage;
            LanguageChanged?.Invoke(newLanguage);
        }
        /// <summary>
        /// Overrides the OnStartup method to handle the application startup logic.
        /// </summary>
        /// <param name="e">The <see cref="StartupEventArgs"/> instance containing the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            mutex = new Mutex(true, "{12345678-1234-1234-1234-123456789012}");

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                base.OnStartup(e);
            }
            else
            {
                MessageBox.Show("EasySave is already launched.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Current.Shutdown();
            }
        }

        /// <summary>
        /// Overrides the OnExit method to handle the application exit logic.
        /// </summary>
        /// <param name="e">The <see cref="ExitEventArgs"/> instance containing the event data.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            if (mutex != null)
            {
                if (mutex.WaitOne(TimeSpan.Zero, true))
                {
                    mutex.ReleaseMutex();
                }

                mutex.Close();
            }
        }
    }



}
