using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    private float NoteSpeed = 8;
    private bool start;

    private void Start()
    {
        NoteSpeed = GManager.Instance.noteSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Game Start");
            start = true;
        }

        if (start)
        {
            transform.position -= transform.forward * (Time.deltaTime * NoteSpeed);
        }
        
    }
}
