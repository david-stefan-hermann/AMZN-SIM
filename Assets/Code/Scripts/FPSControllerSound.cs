using UnityEngine;

public class FPSControllerSound : MonoBehaviour
{
    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    private AudioSource _audioSource;
    private CharacterController _characterController;
    private bool _isGrounded;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_characterController.isGrounded && !_isGrounded)
        {
            PlaySound(landSound);
        }

        if (_characterController.isGrounded && _characterController.velocity.magnitude > 2f && !_audioSource.isPlaying)
        {
            PlaySound(walkSound);
        }

        if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            PlaySound(jumpSound);
        }

        _isGrounded = _characterController.isGrounded;
    }

    private void PlaySound(AudioClip clip)
    {
        if (!clip) return;
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}