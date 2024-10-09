using UnityEngine;

namespace Code.Scripts
{
    public class RoomManager : MonoBehaviour
    {
        public bool isWorking = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (this.gameObject.name == "HomeTrigger")
                {
                    isWorking = false;
                    Debug.Log("Player is home, not working.");
                }
                else if (this.gameObject.name == "WorkTrigger")
                {
                    isWorking = true;
                    Debug.Log("Player is now working.");
                }
            }
        }
    }
}
