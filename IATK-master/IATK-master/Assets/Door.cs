using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public Transform door;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, door.position);

        if (distance <= 5)
        {
            animator.SetBool("Near", true);
        }
        else
        {
            animator.SetBool("Near", false);
        }

        if (distance >= 5)
        {
            animator.SetBool("Disappearing", true);
        }
        else
        {
            animator.SetBool("Disappearing", false);
        }
    }
}
