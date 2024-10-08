using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float beltSpeed = 2f;

    void Update()
    {
        // Move packages along the belt
        foreach (Transform child in transform)
        {
            child.Translate(Vector3.forward * beltSpeed * Time.deltaTime);
        }
    }
}