using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowForm : MonoBehaviour
{
    public bool isInShadowForm;
    public bool isInDarkness;

    // Start is called before the first frame update
    void Start()
    {
        isInShadowForm = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Logic for if Player is in Darkness

        Change();
    }

    void Change()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInDarkness)
        {
            isInShadowForm = !isInShadowForm;
        }
        //IS in Shadow Form
        if (isInShadowForm)
        {

        }

        //ISNT in Shadow Form
        if (!isInShadowForm)
        {

        }
    }
}
