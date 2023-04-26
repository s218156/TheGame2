using System.Threading;

namespace TheGame.Multiplayer
{
    public class MultiplayerConfigWorker
    {
        public bool userConfirmed = false;
        public bool attemptCompleted = false;
        private Thread worker;
        private void VerifyUser()
        {
            Thread.Sleep(5000);
            attemptCompleted = true;
            userConfirmed = true;

        }
        public void StartWorker()
        {
            worker = new Thread(VerifyUser);
            worker.Start();
        }


    }
}
