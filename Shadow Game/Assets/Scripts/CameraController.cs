using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraXOffset = 3;
    [SerializeField] private float cameraYOffset = 2;

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.position.x + cameraXOffset, player.position.y + cameraYOffset, transform.position.z);
    }
}
