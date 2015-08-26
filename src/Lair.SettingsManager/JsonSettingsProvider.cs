using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;

namespace Lair.SettingsManager
{
    public class JsonSettingsProvider
    {
        private readonly LocalSettingsStore _settingsStorage;
        public JsonSettingsProvider(LocalSettingsStore settingsStorage)
        {
            _settingsStorage = settingsStorage;
        }

        public T GetSettings<T>(bool freshCopy = false) where T : new()
        {
            var fileName = typeof(T).Name + ".settings";
            var settingsText = _settingsStorage.ReadTextFile(fileName);
            var settings = JsonConvert.DeserializeObject<T>(settingsText);
            if (settings == null)
            {
                settings = new T();
            }
            if (settings != null)
            {
                ReadDefaults(settings);
            }
            return settings;
        }

        public void SaveSettings<T>(T settings)
        {
            var fileName = typeof(T).Name + ".settings";
            string settingsText = JsonConvert.SerializeObject(settings);
            _settingsStorage.WriteTextFile(fileName, settingsText);
        }


        public void ReadDefaults(object settings)
        {
            foreach (var propertyInfo in settings.GetType().GetProperties())
            {
                var value = propertyInfo.GetValue(settings);
                if (value == null)
                {
                    PropertyInfo info = propertyInfo;
                    propertyInfo.ReadAttribute<DefaultValueAttribute>(d => info.SetValue(settings, d.Value));
                }
            }
        }
    }
}
