using UnityEngine;

public class PoacherBehaviour : MonoBehaviour
{
    [Header("References")]
    public Transform ObjectToOrbit;
    public Transform player;

    [Header("Orbit Settings")]
    public float orbitSpeed = 20f;

    [Header("Player Detection")]
    public float detectionRange = 5f;
    public float resumeDelay = 3f;

    [Header("Looking At Player")]
    public float lookSpeed = 5f;

    [Header("Audio")]
    public AudioSource npcAudioSource;
    public AudioClip greetClip;

    private bool isPlayerClose = false;
    private bool hasPlayedSound = false;
    private float timeSincePlayerLeft = 0f;

    void Update()
    {
        if (ObjectToOrbit == null || player == null)
            return;

        // measure distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool currentlyClose = distanceToPlayer <= detectionRange;

        if (currentlyClose)
        {
            // interacting with player
            isPlayerClose = true;
            timeSincePlayerLeft = 0f;

            // play greeting once
            if (!hasPlayedSound && npcAudioSource != null && greetClip != null)
            {
                npcAudioSource.PlayOneShot(greetClip);
                hasPlayedSound = true;
            }

            // smoothly look at player (Y-axis only)
            Vector3 lookDir = player.position - transform.position;
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude > 0.01f)
            {
                Quaternion goal = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, goal, lookSpeed * Time.deltaTime);
            }
        }
        else
        {
            // handle exit
            if (isPlayerClose)
            {
                timeSincePlayerLeft += Time.deltaTime;
                if (timeSincePlayerLeft >= resumeDelay)
                {
                    isPlayerClose = false;
                    hasPlayedSound = false; // reset for next entry
                }
            }

            // orbit and face tangent every frame
            if (!isPlayerClose)
            {
                // move around
                transform.RotateAround(ObjectToOrbit.position, Vector3.up, orbitSpeed * Time.deltaTime);

                // compute tangent direction
                Vector3 radial = transform.position - ObjectToOrbit.position;
                radial.y = 0f;
                Vector3 tangent = Vector3.Cross(Vector3.up, radial).normalized;

                // face along tangent
                if (tangent.sqrMagnitude > 0.01f)
                    transform.rotation = Quaternion.LookRotation(tangent);
            }
        }
    }
}