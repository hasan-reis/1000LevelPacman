using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundsManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public static AudioSource au;

    private void Start()
    {
        au = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void playSoundWithStop(int i)
    {
        au.Stop();
        au.PlayOneShot(sounds[i]);
    }

    public void playSound(int i)
    {
        au.PlayOneShot(sounds[i]);
    }
}
