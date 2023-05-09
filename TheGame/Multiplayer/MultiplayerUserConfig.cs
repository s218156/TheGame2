﻿using System;
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
                using (var stream = new FileStream(path + "/multiplayerConfig.xml", FileMode.Open))
                {
                    var XML = new XmlSerializer(typeof(MultiplayerData));
                    return (MultiplayerData)XML.Deserialize(stream);
                }
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
