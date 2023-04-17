using System;
using System.IO;
using System.Xml.Serialization;

namespace TheGame.Multiplayer
{
    public class MultiplayerUserConfig
    {
        MultiplayerData data;
        private string path = Environment.ExpandEnvironmentVariables("%userprofile%/documents/TheGame");

        public MultiplayerUserConfig()
        {
            data = new MultiplayerData();
        }


        public bool CheckIfUserIsConfigured()
        {
            if (File.Exists(path + "/mulpiplayerConfig.xml"))
            {
                try
                {
                    //login
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

        public void SaveUserConfiguration(string username, string password)
        {
            //ask API for userPrivateKey
            data.userPrivateKey = "";
            data.username = username;

            if (!(File.Exists(path)))
                Directory.CreateDirectory(path);
            using (var stream = new FileStream(path + "/mulpiplayerConfig.xml", FileMode.Create))
            {
                var XML = new XmlSerializer(typeof(MultiplayerData));
                XML.Serialize(stream, data);
            }

        }
    }
}
