using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followAIkangaroo : MonoBehaviour
{
    public NavMeshAgent ai;
    public Transform player;
    public Animator aiAnim;
    Vector3 dest;

    void Update()
    {
        dest = player.position;
        ai.destination = dest;
        if (ai.remainingDistance <= ai.stoppingDistance)
        {
            aiAnim.ResetTrigger("kangaroowalk");
            aiAnim.SetTrigger("kangaroostop");
        }
        else
        {
            aiAnim.ResetTrigger("kangaroostop");
            aiAnim.SetTrigger("kangaroowalk");
        }
    }
}