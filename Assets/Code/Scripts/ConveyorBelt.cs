using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject packagePrefab; // Reference to the package prefab
    public float beltSpeed = 2f; // Speed of the conveyor belt
    public float spawnInterval = 2f; // Time between spawns
    public float destroyXThreshold = 10f; // Packages are destroyed if their x value exceeds this

    public Transform spawnPoint; // Where packages spawn
    public Material normalMaterial;
    public Material damagedMaterial;

    private float nextSpawnTime;

    void Update()
    {
        MovePackages();

        if (Time.time >= nextSpawnTime)
        {
            SpawnPackage();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void MovePackages()
    {
        // Move all child objects (packages) of the conveyor belt
        foreach (Transform child in transform)
        {
            child.Translate(Vector3.right * beltSpeed * Time.deltaTime);

            // Destroy the package if it exceeds the x threshold
            if (child.position.x > destroyXThreshold)
            {
                Destroy(child.gameObject);
            }
        }
    }

    void SpawnPackage()
    {
        GameObject newPackage = Instantiate(packagePrefab, spawnPoint.position, Quaternion.identity);
        newPackage.transform.parent = transform; // Attach to the conveyor belt

        // Randomize size
        float randomScale = Random.Range(0.8f, 1.2f);
        newPackage.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        // Randomly determine if the package is damaged
        PackageBox packageScript = newPackage.GetComponent<PackageBox>();
        packageScript.isDamaged = Random.value > 0.8f; // 20% chance of being damaged

        // Set material based on whether the package is damaged
        Renderer packageRenderer = newPackage.GetComponent<Renderer>();
        packageRenderer.material = packageScript.isDamaged ? damagedMaterial : normalMaterial;
    }
}