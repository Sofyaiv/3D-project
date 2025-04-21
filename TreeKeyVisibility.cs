using UnityEngine;

public class TreeKeyVisibility : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject keyInHand;   // The key object in the player's hand

    private bool _isInTriggerZone = false;
    private bool _hasPickedUpKey = false;

    [Header("Assign Cage Interaction Scripts in Inspector")]
    public CageInteraction birdCageInteractionScript;  // Reference to the bird cage's CageInteraction script
    public CageInteraction kangarooCageInteractionScript;  // Reference to the kangaroo cage's CageInteraction script

    void Start()
    {
        // Ensure the key in hand is initially hidden
        if (keyInHand != null)
        {
            keyInHand.SetActive(false);
        }
    }

    void Update()
    {
        // Check for E key press to pick up the key when inside the trigger zone
        if (_isInTriggerZone && Input.GetKeyDown(KeyCode.E) && !_hasPickedUpKey)
        {
            PickUpKey();
        }
    }

    private void PickUpKey()
    {
        _hasPickedUpKey = true;

        // Hide the key on the tree
        gameObject.SetActive(false);

        // Show the key in the player's hand
        if (keyInHand != null)
        {
            keyInHand.SetActive(true);
        }

        // Notify both CageInteraction scripts that the player has the key in hand
        if (birdCageInteractionScript != null)
        {
            birdCageInteractionScript.SetHasKeyInHand(true);
        }
        if (kangarooCageInteractionScript != null)
        {
            kangarooCageInteractionScript.SetHasKeyInHand(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInTriggerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInTriggerZone = false;
        }
    }
}
