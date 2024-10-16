using System;
using System.Collections;
using System.Collections.Generic;
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
        public AudioClip openUISound;
        public AudioClip closeUISound;
        public AudioClip purchaseSuccessSound; // Sound to play when an item is successfully purchased
        public AudioClip insufficientFundsSound; // Sound to play when there are insufficient funds
        
        private FirstPersonController _firstPersonController;
        private AudioSource _audioSource;

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

            // Initialize the AudioSource
            _audioSource = GetComponent<AudioSource>(); // Add this line
        }

        private void Update()
        {
            if (uiPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
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

            // Play the open UI sound
            if (openUISound && _audioSource) // Add this block
            {
                _audioSource.PlayOneShot(openUISound);
            }
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
            
            // Play the close UI sound
            if (closeUISound && _audioSource) // Add this block
            {
                _audioSource.PlayOneShot(closeUISound);
            }
        }

        private void PopulateDecorationList()
        {
            // Populate the UI list with decoration items
            foreach (Transform decoration in decorationsHolder)
            {
                if (decoration != null)
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
        }

        private void UpdateDecorationButton(DecorationItem item)
        {
            foreach (Transform child in decorationListContent)
            {
                var buttonScript = child.GetComponent<DecorationButton>();
                if (buttonScript != null && buttonScript.GetDecorationItem() == item)
                {
                    buttonScript.Setup(item, this);
                    break;
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
                UpdateDecorationButton(item); // Update only the purchased item

                // Play purchase success sound
                if (purchaseSuccessSound != null && _audioSource != null)
                {
                    _audioSource.PlayOneShot(purchaseSuccessSound);
                }
            }
            else
            {
                StartCoroutine(DisplayInsufficientFundsMessage(buttonScript));

                // Play insufficient funds sound
                if (insufficientFundsSound != null && _audioSource != null)
                {
                    _audioSource.PlayOneShot(insufficientFundsSound);
                }
            }
            
        }

        private static IEnumerator DisplayInsufficientFundsMessage(DecorationButton buttonScript)
        {
            if (!buttonScript) yield break;
            buttonScript.ShowInsufficientFunds();
            yield return new WaitForSeconds(3f);
            if (buttonScript) // Check again in case it was destroyed during the wait
            {
                buttonScript.HideInsufficientFunds();
            }
        }
        
        public bool UIIsVisible()
        {
            return uiPanel.activeSelf;
        }
    }
}