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
            // Create a temporary camera
            GameObject tempCameraObj = new GameObject("TempCamera");
            Camera tempCamera = tempCameraObj.AddComponent<Camera>();
            tempCamera.backgroundColor = Color.clear;
            tempCamera.clearFlags = CameraClearFlags.SolidColor;

            // Create a RenderTexture
            RenderTexture renderTexture = new RenderTexture(256, 256, 24);
            tempCamera.targetTexture = renderTexture;

            // Position the camera
            tempCamera.transform.position = obj.transform.position + new Vector3(0, 0, -5);
            tempCamera.transform.LookAt(obj.transform);

            // Render the object
            tempCamera.Render();

            // Convert RenderTexture to Texture2D
            RenderTexture.active = renderTexture;
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();

            // Clean up
            RenderTexture.active = null;
            tempCamera.targetTexture = null;
            Destroy(tempCameraObj);
            Destroy(renderTexture);

            // Create a sprite from the Texture2D
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
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