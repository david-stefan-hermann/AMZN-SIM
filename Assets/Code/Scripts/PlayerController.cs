using TMPro;
using UnityEngine;

namespace Code.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float interactDistance = 3f; // Distance within which the player can interact with packages
        public TextMeshProUGUI tooltipText; // Reference to the TextMeshProUGUI element for the tooltip
        public Transform carryPosition; // Position in front of the player where the package will be carried
        public float throwForce = 10f; // Force with which the package is thrown

        private PackageBox _carriedPackage;

        private void Update()
        {
            if (!_carriedPackage)
            {
                CheckForPackage();
            }
            else
            {
                CarryPackage();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!_carriedPackage)
                {
                    TryPickupPackage();
                }
                else
                {
                    ReleasePackage();
                }
            }
        }

        private void CheckForPackage()
        {
            if (Camera.main)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
                {
                    PackageBox package = hit.transform.GetComponent<PackageBox>();
                    if (package)
                    {
                        tooltipText.text = "E to pick up";
                        tooltipText.enabled = true;
                    }
                    else
                    {
                        tooltipText.enabled = false;
                    }
                }
                else
                {
                    tooltipText.enabled = false;
                }
            }
        }

        private void TryPickupPackage()
        {
            if (Camera.main)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
                {
                    PackageBox package = hit.transform.GetComponent<PackageBox>();
                    if (package)
                    {
                        _carriedPackage = package;
                        _carriedPackage.Pickup();
                        _carriedPackage.transform.SetParent(carryPosition);
                        _carriedPackage.transform.localPosition = Vector3.zero;
                        _carriedPackage.GetComponent<Rigidbody>().isKinematic = true;
                        tooltipText.enabled = false;
                    }
                }
            }
        }

        private void CarryPackage()
        {
            _carriedPackage.transform.position = carryPosition.position;
        }

        private void ReleasePackage()
        {
            _carriedPackage.transform.SetParent(null);
            Rigidbody rb = _carriedPackage.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            if (Camera.main) rb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.VelocityChange);
            _carriedPackage = null;
        }
    }
}