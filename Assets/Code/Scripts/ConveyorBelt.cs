using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts
{
    public class ConveyorBelt : MonoBehaviour
    {
        public GameObject packagePrefab; // Reference to the package prefab
        public Transform spawnStart; // Start point for spawning packages
        public Transform spawnEnd; // End point for despawning packages
        public float beltSpeed = 2f; // Speed of the conveyor belt
        public float minSpawnInterval = 1f; // Minimum time between spawns
        public float maxSpawnInterval = 3f; // Maximum time between spawns
        public float damageChance = 0.1f; // Chance for a package to be damaged
        public Vector3 baseScale = new Vector3(1f, 1f, 1f); // Base size of the packages
        public Vector3 scaleVariation = new Vector3(0.2f, 0.2f, 0.2f); // Variation for each axis

        private float _nextSpawnTime;

        private void Start()
        {
            ScheduleNextSpawn();
        }

        private void Update()
        {
            MovePackages();

            if (!(Time.time >= _nextSpawnTime)) return;
            SpawnPackage();
            ScheduleNextSpawn();
        }


        private void MovePackages()
        {
            // Move all child objects (packages) of the conveyor belt
            foreach (Transform child in transform)
            {
                if (!child.CompareTag("Package")) continue;

                // Keep the y position constant
                var targetPosition = new Vector3(spawnEnd.position.x, child.position.y, spawnEnd.position.z);
                child.position = Vector3.MoveTowards(child.position, targetPosition, beltSpeed * Time.deltaTime);

                // Destroy the package if it reaches the end point
                if (Vector3.Distance(child.position, targetPosition) < 0.1f)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        private void SpawnPackage()
        {
            var newPackage = Instantiate(packagePrefab, spawnStart.position, Quaternion.identity);
            newPackage.transform.parent = transform; // Attach to the conveyor belt
            newPackage.tag = "Package"; // Set the tag to "Package"

            // Randomize size with individual variations for each axis
            var randomScale = new Vector3(
                baseScale.x + Random.Range(-scaleVariation.x, scaleVariation.x),
                baseScale.y + Random.Range(-scaleVariation.y, scaleVariation.y),
                baseScale.z + Random.Range(-scaleVariation.z, scaleVariation.z)
            );
            newPackage.transform.localScale = randomScale;

            // Randomize rotation along the y-axis
            var randomYRotation = Random.Range(0f, 360f);
            newPackage.transform.rotation = Quaternion.Euler(0f, randomYRotation, 0f);

            // Adjust the y position to align the bottom of the package with the spawn point
            var packageHeight = newPackage.GetComponent<Renderer>().bounds.size.y;
            newPackage.transform.position = new Vector3(
                spawnStart.position.x,
                spawnStart.position.y + packageHeight / 2,
                spawnStart.position.z
            );

            // Set damage status
            var packageBox = newPackage.GetComponent<PackageBox>();
            if (packageBox)
            {
                packageBox.isDamaged = Random.value < damageChance;
            }
        }

        private void ScheduleNextSpawn()
        {
            _nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Package"))
            {
                var rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    other.transform.SetParent(transform); // Set the parent to the conveyor belt
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Package"))
            {
                var rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    other.transform.SetParent(null);
                }
            }
        }
    }
}