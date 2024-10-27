using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audio;
    private AudioClip _music;
    private string _songName;
    private bool _played;

    private void Start()
    {
        GManager.Instance.Start = false;
        _songName = "エンドマークに希望と涙を添えて";
        _audio = GetComponent<AudioSource>();
        _music = (AudioClip)Resources.Load("Musics/" + _songName);
        _played = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_played)
        {
            GManager.Instance.Start = true;
            GManager.Instance.StartTime = Time.time;
            _played = true;
            _audio.PlayOneShot(_music);
        }
    }
}
