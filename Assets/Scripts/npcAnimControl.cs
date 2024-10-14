using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcAnimControl : MonoBehaviour
{
    NavMeshAgent nm;
    Animator anim;

    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(GetComponent<npcDead>().navMeshState)
            LogMovementDirection();
    }

    void LogMovementDirection()
    {
        float horizontalMovement = nm.velocity.x;
        float verticalMovement = nm.velocity.y;

        if (Mathf.Abs(horizontalMovement) > Mathf.Abs(verticalMovement))
        {
            if (horizontalMovement > 0)
            {
                anim.SetInteger("dir", 3);
            }
            else if (horizontalMovement < 0)
            {
                anim.SetInteger("dir", 2);
            }
        }
        else
        {
            if (verticalMovement > 0)
            {
                anim.SetInteger("dir", 0);
            }
            else if (verticalMovement < 0)
            {
                anim.SetInteger("dir", 1);
            }
        }
    }
}
