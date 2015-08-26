namespace Lair.SettingsManager
{
    public class SettingsManager<T> where T : class, new()
    {
        private readonly JsonSettingsProvider _settingsProvider;
        private static SettingsManager<T> _instance;
        private readonly LocalSettingsStore _localSettingsStore;

        public static SettingsManager<T> Instance
        {
            get { return _instance ?? (_instance = new SettingsManager<T>()); }
        }

        public T Settings { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        private SettingsManager()
        {
            if (_localSettingsStore == null)
            {
                _localSettingsStore = new LocalSettingsStore();
            }

            if (_settingsProvider == null)
            {
                _settingsProvider = new JsonSettingsProvider(_localSettingsStore);
            }
            LoadSettings();
        }

        public void SaveSettings()
        {
            if (!Settings.Equals(null))
            {
                _settingsProvider.SaveSettings(Settings);
            }
        }

        private void LoadSettings()
        {
            Settings = _settingsProvider.GetSettings<T>();
        }
    }
}
