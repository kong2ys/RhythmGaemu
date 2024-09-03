using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private int num = 0;
    private float alfa = 0f;
    private Renderer rend;

    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(rend.material.color.a <= 0f))
        {
            rend.material.color = new Color(rend.material.color.r,rend.material.color.g,rend.material.color.b,alfa);
        }
        
        if (num == 1)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                ColorChange();
            }
        }
        if (num == 2)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ColorChange();
            }
        }
        if (num == 3)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                ColorChange();
            }
        }
        if (num == 4)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                ColorChange();
            }
        }
        alfa -= Time.deltaTime * speed;
    }

    void ColorChange()
    {
        alfa = 0.3f;
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
    }
}
