using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    moveController mvControl;

    private void Start()
    {
        mvControl = GetComponent<moveController>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mvControl.SetDirection("left");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            mvControl.SetDirection("right");
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            mvControl.SetDirection("up");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            mvControl.SetDirection("down");
        }
    }
}
