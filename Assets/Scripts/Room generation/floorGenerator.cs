using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorGenerator : MonoBehaviour
{
    public GameObject room;
    public GameObject startingRoom;
    public GameObject room2;
    public GameObject room3;
    public Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        startingRoom = Instantiate(room2, grid.WorldToCell(new Vector2(20, 20)), Quaternion.identity);
        startingRoom.transform.SetParent(grid.transform, false);

        GameObject placedRoom = generateRoom(startingRoom, 1, room2);
        generateRoom(placedRoom, 1, room3);
        Debug.Log(placedRoom.transform.position);
    }

    // Door represents the direction the room should connect on, reletive to the old room
    // 0 == Up,
    // 1 == Right,
    // 2 == down,
    // 3 == left
    GameObject generateRoom(GameObject currentRoom, int door, GameObject newRoom)
    {
        Transform currentDoor = getDoorTransform(currentRoom, door);

        if (currentDoor != null)
        {
            Vector3 newRoomSpawnPoint = grid.WorldToCell(currentDoor.position);
            Debug.Log("HI" + newRoomSpawnPoint);
            //Vector3 newRoomConnectorDoor = grid.WorldToCell(getDoorTransform(newRoom, (door + 2) % 4).);
            Vector3 offset = grid.WorldToCell(getDoorTransform(newRoom,(door+2)%4).localPosition);
            GameObject newRoomPlaced = Instantiate(newRoom, grid.WorldToCell(newRoomSpawnPoint - offset), Quaternion.identity);
            newRoomPlaced.transform.SetParent(grid.transform, false);
            return newRoomPlaced;
        }
        return null;
    }

    Transform getDoorTransform(GameObject room, int door)
    {
        if(door == 0){
            return room.transform.Find("Doors").Find("UpperDoor");

        }else if(door == 1){
            return room.transform.Find("Doors").Find("RightDoor");

        }else if(door == 2){
            return room.transform.Find("Doors").Find("LowerDoor");

        }
        else if (door == 3){
            return room.transform.Find("Doors").Find("LeftDoor");

        }

        return null;
    }

}
