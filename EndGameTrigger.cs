using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    public Transform[] agents; // Assign both (or more) agents here in the Inspector
    public GameObject endCanvas; // Reference to your end canvas, assign it in the Inspector
    //public GameObject pauseMenu;
    public float minX = 40f;
    public float maxX = 44f;
    public float minZ = -40f;
    public float maxZ = -33f;

    private bool hasTriggered = false;

    void Start()
    {
        // Make sure the end canvas is initially invisible (inactive)
        if (endCanvas != null)
        {
            endCanvas.SetActive(false); // Ensure the canvas is not visible at the start
        }
        else
        {
            Debug.LogWarning("End Canvas is not assigned!");
        }
    }

    void Update()
    {
        if (hasTriggered) return;

        // Check all agents
        foreach (Transform agent in agents)
        {
            float x = agent.position.x;
            float z = agent.position.z;
            //Debug.Log(x);
            //Debug.Log(z);
            // If any one of them is *not* inside the zone, break early
            if (!(x >= minX && x <= maxX && z >= minZ && z <= maxZ))
            {
                return; // At least one agent is outside, so we wait
            }
            else
            {
                // If we get here, all agents are inside the zone
                hasTriggered = true;
                Debug.Log("All agents are inside the target zone. Ending game...");
                TriggerEndGame();
            }
        }

        // If we get here, all agents are inside the zone
        //hasTriggered = true;
       // Debug.Log("All agents are inside the target zone. Ending game...");
        //TriggerEndGame();
    }

    void TriggerEndGame()
    {
        // Your end-of-game logic here
        Debug.Log("Game Over!");

        // Ensure the end canvas is assigned and set it active (show it)
        endCanvas.SetActive(true); // Make the end canvas visible
        Time.timeScale = 0f;
        //SceneManager.LoadSceneAsync(1);
    }
}
