using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private GameObject player;
    public RoomList roomList;
    public GameObject MidRoom;
    public GameObject roomSpawnPoint;
    public bool roomSpawned;

    // Start is called before the first frame update
    void Start()
    {
        roomList = FindObjectOfType<RoomList>();
        player = roomList.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > MidRoom.transform.position.x && !roomSpawned)
        {
            roomSpawned = true;
            Instantiate(roomList.rooms[Random.Range(0, roomList.rooms.Count)], roomSpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
