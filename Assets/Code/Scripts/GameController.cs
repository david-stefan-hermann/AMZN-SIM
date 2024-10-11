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
        public AudioClip earnMoneyClip; // Audio clip for earning money
        public AudioClip loseMoneyClip; // Audio clip for losing money
        private AudioSource _audioSource; // Audio source to play the clips

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
            _audioSource = GetComponent<AudioSource>();
            UpdateMoneyText();
        }

        public void PackageSortedSuccess(bool isDamaged)
        {
            if (isDamaged)
            {
                playerMoney -= moneyDecreaseAmount;
                PlaySound(loseMoneyClip);
            }
            else
            {
                playerMoney += moneyIncreaseAmount;
                PlaySound(earnMoneyClip);
            }

            UpdateMoneyText();
        }

        private void PlaySound(AudioClip clip)
        {
            if (clip != null)
            {
                _audioSource.PlayOneShot(clip);
            }
        }

        public void UpdateMoneyText()
        {
            moneyText.text = $"${playerMoney}";
        }
    }
}