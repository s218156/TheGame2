using System;
using System.Threading;

namespace TheGame.Multiplayer
{
    public class MultiplayerConfigWorker
    {

        private MultiplayerData multiplayerData;
        public bool attemptBegan = false;
        public bool userConfirmed = false;
        public bool attemptCompleted = false;
        string password;
        private Thread worker;

        public void UpdateMultiplayerData(MultiplayerData multiplayerData)
        {
            this.multiplayerData = multiplayerData;
        }

        public MultiplayerData GetMultiplayerData()
        {
            return this.multiplayerData;
        }
        private async void VerifyUser()
        {
            if(password != null)
            {
                try
                {
                    multiplayerData.userPrivateKey = await MultiplayerCommunicationService.AuthenticateUser(multiplayerData.username, password);
                    if (multiplayerData.userPrivateKey.Length > 100)
                        userConfirmed = true;
                }catch (Exception ex)
                {

                }
                
            }
            else
            {
                try
                {
                    await MultiplayerCommunicationService.VerifyUserConfig(multiplayerData.username, multiplayerData.userPrivateKey);
                    userConfirmed = true;
                }
                catch (Exception e)
                {
                    userConfirmed = false;
                    throw e;
                }
                

            }
                
            attemptCompleted = true;
            



        }

        public void ValidatePlayerData(MultiplayerData data)
        {
            if (data != null)
            {
                attemptBegan = true;
                multiplayerData = data;
                worker = new Thread(VerifyUser);
                worker.Start();
            }
            else
            {
                attemptBegan = true;
                attemptCompleted = true;
            }
        }

        public void StartWorker(string username, string password)
        {
            this.password = password;
            attemptBegan = true;
            multiplayerData = new MultiplayerData();
            multiplayerData.username = username;
            worker = new Thread(VerifyUser);
            worker.Start();
        }


    }
}
