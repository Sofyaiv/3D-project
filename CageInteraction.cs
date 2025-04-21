using System.Collections;
using UnityEngine;

public class CageInteraction : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject keyInHand;      // The key in the player's hand
    private bool _isInCageTriggerZone = false;   // If the player is in the cage's trigger zone
    private bool _hasKeyInHand = false;          // If the player has the key in hand

    private bool isMoving = false;
    public float moveSpeed = 2f;
    public float moveDistance = 20f;

    void Update()
    {
        if (_isInCageTriggerZone && Input.GetKeyDown(KeyCode.E))
        {
            if (_hasKeyInHand)
            {
                FreeAnimals(); // This function will handle freeing the animals or whatever action you want
            }
        }
    }

    private void FreeAnimals()
    {
        if (!isMoving)
        {
            // Start moving the cage upwards
            isMoving = true;

            // Start the move coroutine
            StartCoroutine(MoveCageUpwards());
        }
    }

    private IEnumerator MoveCageUpwards()
    {
        // Get the original position of the cage
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * moveDistance;

        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        // Smoothly move the cage upwards until it reaches the target position
        while (transform.position.y < targetPosition.y)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

            yield return null;
        }

        // Ensure the cage reaches the exact target position
        transform.position = targetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInCageTriggerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInCageTriggerZone = false;
        }
    }

    // Call this method when the player picks up the key (from the previous script)
    public void SetHasKeyInHand(bool hasKey)
    {
        _hasKeyInHand = hasKey;
    }
}
