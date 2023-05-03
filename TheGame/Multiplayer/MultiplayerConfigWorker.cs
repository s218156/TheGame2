using System.Threading;

namespace TheGame.Multiplayer
{
    public class MultiplayerConfigWorker
    {

        private MultiplayerData multiplayerData;
        public bool attemptBegan = false;
        public bool userConfirmed = false;
        public bool attemptCompleted = false;
        private Thread worker;

        public void UpdateMultiplayerData(MultiplayerData multiplayerData)
        {
            this.multiplayerData = multiplayerData;
        }

        public MultiplayerData GetMultiplayerData()
        {
            return this.multiplayerData;
        }
        private void VerifyUser()
        {

            Thread.Sleep(5000);
            multiplayerData.userPrivateKey = "testUserPrivateKeyFromWorker";
            attemptCompleted = true;

            userConfirmed = true;

        }
        public void StartWorker(string username, string password)
        {
            attemptBegan = true;
            multiplayerData = new MultiplayerData();
            multiplayerData.username = username;
            worker = new Thread(VerifyUser);
            worker.Start();
        }


    }
}
