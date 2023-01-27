using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChange : MonoBehaviour
{
    /* Notes for using prefab
     * Delete the old camera :)
     * Move puzzle camera X and Y to desired location, you can also change the FOV. 
     * The box collider 2D under the "Cameras" gameobject will change the camera to Puzzle cam when entered, edit as required
     */

    [SerializeField] //Assign platformer cam and puzzle cam in editor where this script is
    private CinemachineVirtualCamera platformerCam, puzzleCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) //if collision object is player layer switch to puzzle cam
        {
            puzzleCam.Priority += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) //if collision object is player layer go back to platformer cam
        {
            puzzleCam.Priority -= 1;
        }
    }
}
