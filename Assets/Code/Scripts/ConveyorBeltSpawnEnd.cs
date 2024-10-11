using UnityEngine;

public class ConveyorBeltSpawnEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Package")) return;

        var packageCollider = other.GetComponent<Collider>();
        var walls = GameObject.FindGameObjectsWithTag("Walls");

        foreach (var wall in walls)
        {
            var wallCollider = wall.GetComponent<Collider>();
            if (wallCollider != null)
            {
                Physics.IgnoreCollision(packageCollider, wallCollider);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Package")) return;

        var packageCollider = other.GetComponent<Collider>();
        var walls = GameObject.FindGameObjectsWithTag("Walls");

        foreach (var wall in walls)
        {
            var wallCollider = wall.GetComponent<Collider>();
            if (wallCollider != null)
            {
                Physics.IgnoreCollision(packageCollider, wallCollider, false);
            }
        }
    }
}