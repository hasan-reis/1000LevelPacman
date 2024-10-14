using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeManagment : MonoBehaviour
{
    public Text timeWriting;
    public static float time=16f;
    public static float maxTime= 16f;
    button buttonScript;
    float tempTime;

    private void Start()
    {
        buttonScript = GameObject.Find("Canvas1").GetComponent<button>();
    }

    void coutDown()
    {
        timeWriting.text = "";
        //print("RemainTime:" + tempTime);
        tempTime -= 1;

        //Debug.LogWarning("TimeCout: " + tempTime);

        int minute = Mathf.FloorToInt(tempTime / 60);
        int second = Mathf.FloorToInt(tempTime % 60);

        string formattedTime = string.Format("{0:00}:{1:00}", minute, second);

        timeWriting.text = formattedTime;

        if (tempTime <= 0)
        {
            coutDownCancel();
            buttonScript.Win();
        }
    }

    public void startTime()
    {
        tempTime = time;
        InvokeRepeating("coutDown", 0f, 1f);
    }

    public void coutDownCancel()
    {
        CancelInvoke("coutDown");
    }
}
