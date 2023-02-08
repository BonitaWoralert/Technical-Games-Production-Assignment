using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private GameObject player;
    public RoomList roomList;
    public GameObject roomSpawnPoint;
    public List<GameObject> exitDoors;
    public List<GameObject> enterDoors;
    public bool roomSpawned;
    private DoorOpener doorOpener;
    public InfiniteStats stats;
    public DoorOpener.DoorType roomType;
    public int roomMinCoins;
    public int roomMaxCoins;


    // Start is called before the first frame update
    void Start()
    {
        roomList = FindObjectOfType<RoomList>();
        player = roomList.player;
        stats = FindObjectOfType<InfiniteStats>();
        StartCoroutine(LateStart(0.05f));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call
        foreach (var i in enterDoors)
        {
            if (i.GetComponent<DoorOpener>().doorType == roomType)
            {
                i.SetActive(false);
            }
            else
            {
                i.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (exitDoors != null && roomSpawned == false)
        {
            //Distance Checker for all exitDoors
            foreach (var i in exitDoors)
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
                            GameObject room = Instantiate(roomList.downRooms[Random.Range(0, roomList.downRooms.Count)], doorOpener.roomPlacement.transform.position, Quaternion.identity);
                            room.GetComponent<RoomGenerator>().roomType = DoorOpener.DoorType.Up;
                        }

                        if (doorOpener.doorType == DoorOpener.DoorType.Down)
                        {
                            roomSpawned = true;
                            GameObject room = Instantiate(roomList.upRooms[Random.Range(0, roomList.upRooms.Count)], doorOpener.roomPlacement.transform.position, Quaternion.identity);
                            room.GetComponent<RoomGenerator>().roomType = DoorOpener.DoorType.Down;
                        }

                        if (doorOpener.doorType == DoorOpener.DoorType.Left)
                        {
                            roomSpawned = true;
                            GameObject room = Instantiate(roomList.rightRooms[Random.Range(0, roomList.rightRooms.Count)], doorOpener.roomPlacement.transform.position, Quaternion.identity);
                            room.GetComponent<RoomGenerator>().roomType = DoorOpener.DoorType.Left;
                        }

                        if (doorOpener.doorType == DoorOpener.DoorType.Right)
                        {
                            roomSpawned = true;
                            GameObject room = Instantiate(roomList.leftRooms[Random.Range(0, roomList.leftRooms.Count)], doorOpener.roomPlacement.transform.position, Quaternion.identity);
                            room.GetComponent<RoomGenerator>().roomType = DoorOpener.DoorType.Right;
                        }

                        stats.coins += Random.Range(roomMinCoins, roomMaxCoins + stats.roomCoins);
                        exitDoors.Remove(i);
                        Destroy(i);
                    }
                }
            }
        }
    }
}
