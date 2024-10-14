using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class food : MonoBehaviour
{
    static Text scoreText; 
    public static int score=0;
    public static int eatenFood=0;
    static button buttonScript;
    static AudioSource mainNodeAu;

    void Start()
    {
        scoreText = GameObject.Find("score").GetComponent<Text>();
        buttonScript= GameObject.Find("Canvas1").GetComponent<button>();
        mainNodeAu = transform.parent.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mainNodeAu.Stop();
            mainNodeAu.Play();
            eatenFood++;
            score += 1;
            scoreText.text = "Score:" + score;

            if (eatenFood >= 298)
            {
                buttonScript.Win();
            }

            gameObject.SetActive(false);
        }
    }
}