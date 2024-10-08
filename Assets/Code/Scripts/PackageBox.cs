using UnityEngine;

public class PackageBox : MonoBehaviour
{
    public bool isDamaged;

    void Start()
    {
        // Randomly flag some packages as damaged
        isDamaged = Random.value > 0.8f;
    }
}