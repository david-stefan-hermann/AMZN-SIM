using System.Collections;
using StarterAssets;
using UnityEngine;

namespace Code.Scripts
{
    public class HomePC : MonoBehaviour
    {
        public GameObject uiPanel;
        public Transform decorationsHolder;
        public GameObject decorationButtonPrefab;
        public Transform decorationListContent;

        private FirstPersonController _firstPersonController;

        private void Start()
        {
            // Make all decorations invisible at the start
            foreach (Transform decoration in decorationsHolder)
            {
                decoration.gameObject.SetActive(false);
            }

            // Populate the UI list
            PopulateDecorationList();

            // Get the player controller
            _firstPersonController = FindFirstObjectByType<FirstPersonController>();

            // Close the UI panel at the start
            CloseUI();
        }

        private void Update()
        {
            if (uiPanel.activeSelf && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
            {
                CloseUI();
            }
        }

        public void OpenUI()
        {
            uiPanel.SetActive(true);
            if (_firstPersonController)
            {
                _firstPersonController.enabled = false; // Disable player movement
            }

            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Show the cursor
        }

        public void CloseUI()
        {
            uiPanel.SetActive(false);
            if (_firstPersonController)
            {
                _firstPersonController.enabled = true; // Enable player movement
            }

            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false; // Hide the cursor
        }

        private void PopulateDecorationList()
        {
            foreach (Transform decoration in decorationsHolder)
            {
                var decorationItem = decoration.GetComponent<DecorationItem>();
                if (decorationItem != null)
                {
                    var button = Instantiate(decorationButtonPrefab, decorationListContent);
                    var buttonScript = button.GetComponent<DecorationButton>();
                    buttonScript.Setup(decorationItem, this);
                }
            }
        }

        public void PurchaseDecoration(DecorationItem item, DecorationButton buttonScript)
        {
            if (GameController.Instance.playerMoney >= item.price && !item.isOwned)
            {
                GameController.Instance.playerMoney -= item.price;
                item.isOwned = true;
                item.gameObject.SetActive(true);
                GameController.Instance.UpdateMoneyText();
                PopulateDecorationList(); // Refresh the list to update ownership status
            }
            else
            {
                StartCoroutine(DisplayInsufficientFundsMessage(buttonScript));
            }
        }

        private IEnumerator DisplayInsufficientFundsMessage(DecorationButton buttonScript)
        {
            buttonScript.ShowInsufficientFunds();
            yield return new WaitForSeconds(3f);
            buttonScript.HideInsufficientFunds();
        }
    }
}