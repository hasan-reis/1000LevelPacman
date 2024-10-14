using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacmanControl : MonoBehaviour
{
    public static float speed = 2f;
    bool up, down, right, left;

    private Vector2 velocity = Vector2.zero;
    Animator anim;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    public float minDistanceForSwipe = 40f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        yon_ata(false, false, true, false);
    }

    private void Update()
    {
        keyControl();
        move();

        velocity = Vector2.ClampMagnitude(velocity, speed);

        transform.Translate(velocity * Time.deltaTime);
    }

    void keyControl()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;
                CheckSwipe();
            }
        }
    }

    void CheckSwipe()
    {
        float deltaX = fingerUpPosition.x - fingerDownPosition.x;
        float deltaY = fingerUpPosition.y - fingerDownPosition.y;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (deltaX > minDistanceForSwipe)
            {
                // Sağa doğru kaydırma
                print("pacman Speed:" + speed);
                yon_ata(false, false, true, false);
            }
            else if (deltaX < -minDistanceForSwipe)
            {
                // Sola doğru kaydırma
                Debug.Log("Sola");
                yon_ata(false, false, false, true);
            }
        }
        else
        {
            if (deltaY > minDistanceForSwipe)
            {
                // Yukarı doğru kaydırma
                Debug.Log("Yukarı");
                yon_ata(true, false, false, false);
            }
            else if (deltaY < -minDistanceForSwipe)
            {
                // Aşağı doğru kaydırma
                Debug.Log("Aşağı");
                yon_ata(false, true, false, false);
            }
        }
    }

    public void yon_ata(bool y1, bool y2, bool y3, bool y4)
    {
        up = y1;
        down = y2;
        right = y3;
        left = y4;
    }

    void move()
    {
        if (up)
        {
            velocity.y = speed;
            anim.SetInteger("direction", 2);
        }
        else if (down)
        {
            velocity.y = -speed;
            anim.SetInteger("direction", 3);
        }
        else if (right)
        {
            velocity.x = speed;
            anim.SetInteger("direction", 0);
        }
        else if (left)
        {
            velocity.x = -speed;
            anim.SetInteger("direction", 1);
        }
    }
}

