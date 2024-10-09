using UnityEngine;

namespace Code.Scripts
{
    public class HomePC : MonoBehaviour
    {
        public GameObject[] decorations;
        public Transform[] decorationPositions;
        private int decorationIndex = 0;

        public void PurchaseDecoration()
        {
            if (decorationIndex < decorations.Length)
            {
                Instantiate(decorations[decorationIndex], decorationPositions[decorationIndex].position, Quaternion.identity);
                decorationIndex++;
            }
            else
            {
                Debug.Log("No more decorations available.");
            }
        }
    }
}
