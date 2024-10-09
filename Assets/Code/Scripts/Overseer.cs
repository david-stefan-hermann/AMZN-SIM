using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public class Overseer : MonoBehaviour
    {
        public Transform player;
        public float whipCooldown = 10f;
        private bool canWhip = true;

        void Update()
        {
            if (canWhip && PlayerMissedPackage()) 
            {
                WhipPlayer();
                StartCoroutine(WhipCooldown());
            }
        }

        bool PlayerMissedPackage()
        {
            // Logic to detect if a damaged package is missed by the player
            return Random.value > 0.9f;  // Example placeholder logic
        }

        void WhipPlayer()
        {
            // Trigger animation or damage player health
            Debug.Log("Player whipped!");
        }

        IEnumerator WhipCooldown()
        {
            canWhip = false;
            yield return new WaitForSeconds(whipCooldown);
            canWhip = true;
        }
    }
}
