using UnityEngine;

public class PackageBox : MonoBehaviour
{
    public bool isDamaged; // Whether this package is damaged
    private Rigidbody rb;

    void Start()
    {
        // Packages will start without a Rigidbody to save on unnecessary physics calculations
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Disable physics until picked up
    }

    // This function is called when the player picks up the package
    public void Pickup()
    {
        rb.isKinematic = false; // Enable physics when picked up
    }
}
