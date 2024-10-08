using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform holdPosition;
    private GameObject heldObject;

    void Update()
    {
        // Player movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        // Pickup and drop mechanics
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                // Pickup object
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
                {
                    if (hit.collider.CompareTag("Package"))
                    {
                        heldObject = hit.collider.gameObject;
                        heldObject.transform.position = holdPosition.position;
                        heldObject.transform.parent = holdPosition;

                        // Enable physics (Rigidbody) when picked up
                        PackageBox package = heldObject.GetComponent<PackageBox>();
                        if (package != null)
                        {
                            package.Pickup();
                        }
                    }
                }
            }
            else
            {
                // Drop object
                heldObject.transform.parent = null;
                heldObject = null;
            }
        }
    }
}