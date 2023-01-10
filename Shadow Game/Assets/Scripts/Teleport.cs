using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Pos1;
    public GameObject Pos2;
    public bool tel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (tel == true)
            {
                gameObject.transform.position = Pos1.transform.position;
                tel = false;
            }
            else if (tel == false)
            {
                gameObject.transform.position = Pos2.transform.position;
                tel = true;
            }
        }
    }
}
