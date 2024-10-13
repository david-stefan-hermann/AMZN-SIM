using TMPro;
using UnityEngine;

namespace Code.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float interactDistance = 3f; // Distance within which the player can interact with packages
        public TextMeshProUGUI packageTooltipText; // Reference to the TextMeshProUGUI element for the tooltip
        public TextMeshProUGUI pcTooltipText; // Reference to the TextMeshProUGUI element for the PC tooltip
        public TextMeshProUGUI doorTooltipText; // Reference to the TextMeshProUGUI element for the Door tooltip
        public Transform carryPosition; // Position in front of the player where the package will be carried
        public float throwForce = 10f; // Force with which the package is thrown

        private PackageBox _carriedPackage;
        private Collider _playerCollider;

        private void Start()
        {
            // Get the collider from the child object named "Capsule"
            _playerCollider = transform.Find("Capsule").GetComponent<Collider>();
        }

        private void Update()
        {
            if (!_carriedPackage)
            {
                CheckForInteraction();
            }
            else
            {
                CarryPackage();
            }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
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

        private void CheckForInteraction()
        {
            if (Camera.main)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
                {
                    var package = hit.transform.GetComponent<PackageBox>();
                    var homePC = hit.transform.GetComponent<HomePC>();
                    var door = hit.transform.GetComponent<DoorController>();

                    if (package)
                    {
                        ShowTooltip(packageTooltipText);
                    }
                    else if (homePC)
                    {
                        ShowTooltip(pcTooltipText);

                        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                        {
                            if (homePC.UIIsVisible()) return;
                            homePC.OpenUI();
                        }
                    }
                    else if (door)
                    {
                        ShowTooltip(doorTooltipText);
                        
                        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                        {
                            door.ToggleDoor();
                        }
                    }
                    else
                    {
                        ShowTooltip();
                    }
                }
                else
                {
                    ShowTooltip();
                }
            }
        }

        private void ShowTooltip()
        {
            packageTooltipText.enabled = false;
            pcTooltipText.enabled = false;
            doorTooltipText.enabled = false;
        }
        private void ShowTooltip(TextMeshProUGUI toolTip)
        {
            ShowTooltip();
            toolTip.enabled = true;
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
                        _carriedPackage.GetComponent<Collider>().enabled = false; // Disable the package's collider
                        ShowTooltip();
                    }
                }
            }
        }

        private void CarryPackage()
        {
            // Smoothly update the package position to avoid physics interactions
            _carriedPackage.transform.position = Vector3.Lerp(_carriedPackage.transform.position, carryPosition.position, Time.deltaTime * 10f);
        }

        private void ReleasePackage()
        {
            _carriedPackage.GetComponent<Collider>().enabled = true; // Re-enable the package's collider

            _carriedPackage.Release();
            _carriedPackage.transform.SetParent(null);
            var rb = _carriedPackage.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            if (Camera.main) rb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.VelocityChange);
            _carriedPackage = null;
        }
    }
}