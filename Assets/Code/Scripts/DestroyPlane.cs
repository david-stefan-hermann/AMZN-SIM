using Code.Scripts;
using UnityEngine;


public class DestroyPlane : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var package = collision.gameObject.GetComponent<PackageBox>();
        if (!package) return;
        var isSuccess = !package.isDamaged;
        GameController.Instance.PackageSortedSuccess(isSuccess);
        Destroy(package.gameObject);
    }
}