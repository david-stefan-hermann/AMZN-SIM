using UnityEngine;

namespace Code.Scripts
{
    public class WorkButtons : MonoBehaviour
    {
        public RoomManager roomManager;

        public void StartWork()
        {
            roomManager.isWorking = true;
            Debug.Log("Work started.");
        }

        public void StopWork()
        {
            roomManager.isWorking = false;
            Debug.Log("Work stopped.");
        }
    }
}
