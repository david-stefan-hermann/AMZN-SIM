using UnityEngine;
using TMPro;

namespace Code.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public int playerMoney = 0;
        public int moneyIncreaseAmount = 10;
        public int moneyDecreaseAmount = 5;
        public TextMeshProUGUI moneyText; // Reference to the TextMeshProUGUI element

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            UpdateMoneyText();
        }

        public void PackageSortedSuccess(bool isDamaged)
        {
            if (isDamaged)
            {
                playerMoney -= moneyDecreaseAmount;
            }
            else
            {
                playerMoney += moneyIncreaseAmount;
            }

            UpdateMoneyText();
        }

        public void UpdateMoneyText()
        {
            moneyText.text = $"${playerMoney}";
        }
    }
}