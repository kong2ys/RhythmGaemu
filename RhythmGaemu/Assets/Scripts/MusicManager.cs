using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audio;
    AudioClip Music;
    string songName;
    bool played;

    void Start()
    {
        GManager.instance.Start = false;
        songName = "200";
        audio = GetComponent<AudioSource>();
        Music = (AudioClip)Resources.Load("Music/" + songName);
        played = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !played)
        {
            GManager.instance.Start = true;
            GManager.instance.StartTime = Time.time;
            played = true;
            audio.PlayOneShot(Music);
        }
    }
}
