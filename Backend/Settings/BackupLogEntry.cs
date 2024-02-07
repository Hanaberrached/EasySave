namespace Backend.Settings
{
    /// <summary>
    /// Represents an entry for a backup log.
    /// </summary>
    public class BackupLogEntry
    {
        public string Name { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public long FileSize { get; set; }
        public float FileTransferTime { get; set; }
        public float EncryptTime { get; set; }
        public string Time => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

    }
}
