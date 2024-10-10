using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Code.Scripts
{
    public class DecorationButton : MonoBehaviour
    {
        public TextMeshProUGUI decorationNameText;
        public TextMeshProUGUI priceText;
        public Button buyButton;
        public Image ownedText;
        public Image insufficientFundsImage; // Reference to the insufficient funds image
        public Image previewImage; // Reference to the preview image

        private DecorationItem _decorationItem;
        private HomePC _homePC;

        public void Setup(DecorationItem item, HomePC homePC)
        {
            _decorationItem = item;
            _homePC = homePC;

            decorationNameText.text = item.name;
            priceText.text = $"${item.price}";
            
            insufficientFundsImage.gameObject.SetActive(false);
            
            if (item.isOwned)
            {
                buyButton.gameObject.SetActive(false);
                ownedText.gameObject.SetActive(true);
            }
            else
            {
                buyButton.gameObject.SetActive(true);
                ownedText.gameObject.SetActive(false);
                buyButton.onClick.AddListener(() => _homePC.PurchaseDecoration(item, this));
            }

            // Create preview image from 3D object
            CreatePreviewImage(item);
        }

        private void CreatePreviewImage(DecorationItem item)
        {
            // Assuming you have a method to create a sprite from a 3D object
            previewImage.sprite = GenerateSpriteFrom3DObject(item.gameObject);
            previewImage.gameObject.SetActive(true);
        }

        private Sprite GenerateSpriteFrom3DObject(GameObject obj)
        {
            // Implement your method to generate a sprite from a 3D object
            // This is a placeholder implementation
            return null;
        }

        public void ShowInsufficientFunds()
        {
            buyButton.gameObject.SetActive(false);
            insufficientFundsImage.gameObject.SetActive(true);
        }

        public void HideInsufficientFunds()
        {
            buyButton.gameObject.SetActive(true);
            insufficientFundsImage.gameObject.SetActive(false);
        }
        
        public DecorationItem GetDecorationItem()
        {
            return _decorationItem;
        }
    }
}