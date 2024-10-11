using UnityEngine;

namespace Code.Scripts
{
    public class PackageBox : MonoBehaviour
    {
        public bool isDamaged; // Whether this package is damaged
        public Material damagedMaterial; // Material to use if the package is damaged
        public Material normalMaterial; // Material to use if the package is not damaged
        public float destroyTime = 30f; // Time in seconds before the package is destroyed if not moved
        public AudioClip pickupSound; // Sound to play when the package is picked up
        public AudioClip dropSound; // Sound to play when the package is dropped
        public AudioClip collisionSound; // Sound to play when the package collides with something
        public float collisionSoundCooldown = 0.5f; // Cooldown time in seconds for collision sound

        private Rigidbody _rb;
        private Renderer _renderer;
        private float _timer;
        private Vector3 _lastPosition;
        private AudioSource _audioSource;
        private float _lastCollisionSoundTime; // Time when the last collision sound was played


        private void Start()
        {
            // Packages will start without a Rigidbody to save on unnecessary physics calculations
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true; // Disable physics until picked up

            // Get the Renderer component
            _renderer = GetComponent<Renderer>();

            // Set the initial material based on the damage status
            UpdateMaterial();

            // Initialize timer and last position
            _timer = destroyTime;
            _lastPosition = transform.position;

            // Initialize the AudioSource
            _audioSource = GetComponent<AudioSource>(); // Add this line
        }

        // This function is called when the player picks up the package
        public void Pickup()
        {
            _rb.isKinematic = false; // Enable physics when picked up
            PlaySound(pickupSound); // Play pickup sound
        }
        
        // This function is called when the package is released
        public void Release()
        {
            PlaySound(dropSound); // Play drop sound
        }

        // This function updates the material based on the damage status
        private void UpdateMaterial()
        {
            if (isDamaged)
            {
                _renderer.material = damagedMaterial;
            }
            else
            {
                _renderer.material = normalMaterial;
            }
        }

        private void Update()
        {
            // Check if the position has changed
            if (transform.position != _lastPosition)
            {
                // Reset the timer if the position has changed
                _timer = destroyTime;
                _lastPosition = transform.position;
            }
            else
            {
                // Decrease the timer
                _timer -= Time.deltaTime;

                // Destroy the package if the timer reaches zero
                if (_timer <= 0f)
                {
                    Destroy(gameObject);
                }
            }

            // Destroy the package if it falls below y = 10
            if (transform.position.y < -10f)
            {
                Destroy(gameObject);
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            // Check if enough time has passed since the last collision sound
            if (!(Time.time >= _lastCollisionSoundTime + collisionSoundCooldown)) return;
            PlaySound(collisionSound); // Play collision sound
            _lastCollisionSoundTime = Time.time; // Update the last collision sound time
        }

        // This function plays the given sound
        private void PlaySound(AudioClip clip)
        {
            if (clip && _audioSource)
            {
                _audioSource.PlayOneShot(clip);
            }
        }
    }
}