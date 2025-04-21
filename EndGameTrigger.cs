using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    [Header("Assign GameObjects")]
    public GameObject kangaroo;
    public GameObject bird;

    private bool isKangarooInTrigger = false;
    private bool isBirdInTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        // Debugging log to confirm the trigger is detected
        Debug.Log($"{other.gameObject.name} entered the trigger zone.");

        // Check if the entering object is the kangaroo or the bird
        if (other.gameObject == kangaroo)
        {
            isKangarooInTrigger = true;
            Debug.Log("Kangaroo entered the trigger zone.");
        }
        else if (other.gameObject == bird)
        {
            isBirdInTrigger = true;
            Debug.Log("Bird entered the trigger zone.");
        }

        // If both the kangaroo and bird are in the trigger zone, load the end scene
        if (isKangarooInTrigger && isBirdInTrigger)
        {
            Debug.Log("Both animals are in the trigger zone. Loading end scene.");
            LoadEndScene();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Debugging log to confirm when the animals exit the trigger zone
        Debug.Log($"{other.gameObject.name} exited the trigger zone.");

        // If either object leaves the trigger zone, reset the state
        if (other.gameObject == kangaroo)
        {
            isKangarooInTrigger = false;
            Debug.Log("Kangaroo left the trigger zone.");
        }
        else if (other.gameObject == bird)
        {
            isBirdInTrigger = false;
            Debug.Log("Bird left the trigger zone.");
        }
    }

    private void LoadEndScene()
    {
        // Ensure the scene index is correct, here '2' is just an example
        Debug.Log("Loading end scene...");
        SceneManager.LoadScene(2);
    }
}
