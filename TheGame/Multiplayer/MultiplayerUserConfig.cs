using System;
using System.IO;
using System.Xml.Serialization;

namespace TheGame.Multiplayer
{
    public class MultiplayerUserConfig
    {
        MultiplayerData data;
#if DESKTOP
        private string path = Environment.ExpandEnvironmentVariables("%userprofile%/documents/TheGame");
#endif
#if ANDROID
        private string path = "/sdcard/TheGame";
#endif

        public MultiplayerUserConfig()
        {
            data = new MultiplayerData();
        }


        public bool CheckIfUserIsConfigured()
        {
            if (File.Exists(path + "/multiplayerConfig.xml"))
            {
                try
                {
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void UpdateMultiplayerData(MultiplayerData data)
        {
            this.data = data;
        }

        public void SaveUserConfiguration(MultiplayerData data)
        {
            this.data = data;

            if (!(File.Exists(path)))
                Directory.CreateDirectory(path);
            using (var stream = new FileStream(path + "/multiplayerConfig.xml", FileMode.Create))
            {
                var XML = new XmlSerializer(typeof(MultiplayerData));
                XML.Serialize(stream, data);
            }
        }

        public MultiplayerData GetUserConfigFromFile()
        {
            try
            {
                if (CheckIfUserIsConfigured())
                {
                    using (var stream = new FileStream(path + "/multiplayerConfig.xml", FileMode.Open))
                    {
                        var XML = new XmlSerializer(typeof(MultiplayerData));
                        return (MultiplayerData)XML.Deserialize(stream);
                    }
                }
                else
                    return null;
                
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public void RemoveConfigFile()
        {
            if (File.Exists(path + "/multiplayerConfig.xml"))
                File.Delete(path + "/multiplayerConfig.xml");
        }
    }
}
