using System;
using System.IO;
using System.Text;

namespace Lair.SettingsManager
{
    public class LocalSettingsStore
    {
        public string LocalPath { get { return AppDomain.CurrentDomain.BaseDirectory; } }

        public void WriteTextFile(string filename, string fileContents)
        {
            FileStream fileStream = null;
            try
            {
                string path = Path.Combine(LocalPath, filename);

                if (!File.Exists(path))
                {
                    fileStream = File.Create(path);
                }
                byte[] bytes = Encoding.Default.GetBytes(fileContents);
                if (fileStream == null)
                {
                    fileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                }
                if (fileStream != null)
                {
                    fileStream.Write(bytes, 0, bytes.Length);
                    fileStream.Flush();
                    fileStream.Close();
                }
            }
            catch (Exception)
            {
                if (fileStream != null) fileStream.Close();
                throw;
            }
        }

        public string ReadTextFile(string filename)
        {
            string path = Path.Combine(LocalPath, filename);

            if (File.Exists(path))
            {
                var fileContent = File.ReadAllText(path);
                return fileContent;
            }
            return string.Empty;
        }
    }
}
