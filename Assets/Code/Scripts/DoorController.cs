using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public float slideSpeed = 2f; // Speed at which the door slides
    public bool openToLeft = true; // Direction to open the door
    public AudioClip openSound; // Sound to play when the door opens
    public AudioClip closeSound; // Sound to play when the door closes
    [Range(0, 100)] public float slideDistancePercentage = 90f; // Sliding distance as a percentage of the door's width
    public float autoCloseDelay = 5f; // Time in seconds before the door closes automatically

    private Vector3 _closedPosition;
    private Vector3 _openPosition;
    private bool _isOpen;
    private bool _isMoving;
    private Collider _doorCollider;
    private AudioSource _audioSource;
    private Coroutine _autoCloseCoroutine;

    private void Start()
    {
        _doorCollider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
        _closedPosition = transform.localPosition;

        // Calculate the door's width as the greater value of the x and z dimensions of the collider
        var doorWidth = Mathf.Max(_doorCollider.bounds.size.x, _doorCollider.bounds.size.z);
        var slideDistance = doorWidth * (slideDistancePercentage / 100f);

        // Determine the axis along which the door opens
        Vector3 slideDirection;
        if (_doorCollider.bounds.size.x > _doorCollider.bounds.size.z)
        {
            slideDirection = openToLeft ? Vector3.left : Vector3.right;
        }
        else
        {
            slideDirection = openToLeft ? Vector3.back : Vector3.forward;
        }

        // Calculate the open position in local space
        _openPosition = _closedPosition + slideDirection * slideDistance;
    }

    private void Update()
    {
        if (_isMoving)
        {
            MoveDoor();
        }
    }

    public void ToggleDoor()
    {
        _isOpen = !_isOpen;
        _isMoving = true;
        _doorCollider.isTrigger = _isOpen;

        // Play the appropriate sound
        if (_audioSource)
        {
            _audioSource.PlayOneShot(_isOpen ? openSound : closeSound);
        }

        // Manage the auto-close coroutine
        if (_isOpen)
        {
            if (_autoCloseCoroutine != null)
            {
                StopCoroutine(_autoCloseCoroutine);
            }

            _autoCloseCoroutine = StartCoroutine(AutoCloseDoor());
        }
        else
        {
            if (_autoCloseCoroutine == null) return;
            StopCoroutine(_autoCloseCoroutine);
            _autoCloseCoroutine = null;
        }
    }

    private void MoveDoor()
    {
        var targetPosition = _isOpen ? _openPosition : _closedPosition;
        transform.localPosition =
            Vector3.MoveTowards(transform.localPosition, targetPosition, slideSpeed * Time.deltaTime);

        if (!(Vector3.Distance(transform.localPosition, targetPosition) < 0.01f)) return;
        _isMoving = false;
        transform.localPosition = targetPosition; // Ensure the door is exactly at the target position
    }

    private IEnumerator AutoCloseDoor()
    {
        yield return new WaitForSeconds(autoCloseDelay);
        ToggleDoor();
    }
}