using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private GameObject player;
    public RoomList roomList;
    public GameObject roomSpawnPoint;
    public List<GameObject> doors;
    public bool roomSpawned;
    private DoorOpener doorOpener;

    // Start is called before the first frame update
    void Start()
    {
        roomList = FindObjectOfType<RoomList>();
        player = roomList.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (doors != null && roomSpawned == false)
        {
            //Distance Checker for all Doors
            foreach (var i in doors)
            {
                if (Vector2.Distance(player.transform.position, i.transform.position) < 4f)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        doorOpener = i.GetComponent<DoorOpener>();
                        Debug.Log("Opening" + doorOpener.doorType + "Door");

                        if (doorOpener.doorType == DoorOpener.DoorType.Up)
                        {
                            roomSpawned = true;
                            Instantiate(roomList.downRooms[Random.Range(0, roomList.downRooms.Count)], doorOpener.roomPlacement.transform.position, Quaternion.identity);
                        }

                        if (doorOpener.doorType == DoorOpener.DoorType.Down)
                        {
                            roomSpawned = true;
                            Instantiate(roomList.upRooms[Random.Range(0, roomList.upRooms.Count)], doorOpener.roomPlacement.transform.position, Quaternion.identity);
                        }

                        if (doorOpener.doorType == DoorOpener.DoorType.Left)
                        {
                            roomSpawned = true;
                            Instantiate(roomList.rightRooms[Random.Range(0, roomList.rightRooms.Count)], doorOpener.roomPlacement.transform.position, Quaternion.identity);
                        }

                        if (doorOpener.doorType == DoorOpener.DoorType.Right)
                        {
                            roomSpawned = true;
                            Instantiate(roomList.leftRooms[Random.Range(0, roomList.leftRooms.Count)], doorOpener.roomPlacement.transform.position, Quaternion.identity);
                        }

                        doors.Remove(i);
                        Destroy(i);
                    }
                }
            }
        }
    }
}
