using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave_Lighting : MonoBehaviour
{
    private Light global;
    private bool cavelit = false;
    void Start()
    {
        global = GetComponent<Light>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!cavelit)
            {
                global.intensity = Mathf.PingPong(Time.time, 8);
                cavelit = true;
            }
            else
            {
                global.intensity = Mathf.PingPong(Time.time, 8);
                cavelit = false;
            }
        }
            
    }
}
