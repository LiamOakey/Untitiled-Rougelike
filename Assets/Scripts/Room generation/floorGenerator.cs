using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorGenerator : MonoBehaviour
{
    public GameObject room;
    public GameObject startingRoom;
    public GameObject room2;
    public GameObject room3;
    public GameObject roomBarrier;

    public Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        startingRoom = Instantiate(room2, grid.WorldToCell(new Vector2(20, 20)), Quaternion.identity);
        startingRoom.transform.SetParent(grid.transform, false);

        GameObject currentRoom = generateRoom(startingRoom, 1, room3);
        GameObject PreviousRoom = currentRoom;
    }

    // Door represents the direction the room should connect on, reletive to the old room
    // 0 == Up,
    // 1 == Right,
    // 2 == down,
    // 3 == left
    GameObject generateRoom(GameObject room, int door, GameObject newRoom)
    {
        Transform currentDoor = getDoorTransform(room, door);

        if (currentDoor != null)
        {
            Vector3 newRoomSpawnPoint = grid.WorldToCell(currentDoor.position);
            Vector3 offset = grid.WorldToCell(getDoorTransform(newRoom,(door+2)%4).localPosition);
            GameObject newRoomPlaced = Instantiate(newRoom, grid.WorldToCell(newRoomSpawnPoint - offset), Quaternion.identity);
            newRoomPlaced.transform.SetParent(grid.transform, false);
            placeBarrierOnEmptyDoor(newRoomPlaced, 0, 3);
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

    // Takes the two doors the room is connected on
    void placeBarrierOnEmptyDoor(GameObject room, int door1, int door2){ 
        if(door1 != 0 && door2 != 0)
        {
            GameObject barrier = Instantiate(roomBarrier, getDoorTransform(room, 0).localPosition, Quaternion.Euler(0, 0, 90));
            barrier.transform.SetParent(room.transform.Find("Doors").transform, false);
        }

        if (door1 != 1 && door2 != 1)
        {
            GameObject barrier = Instantiate(roomBarrier, getDoorTransform(room, 1).localPosition, Quaternion.identity);
            barrier.transform.SetParent(room.transform.Find("Doors").transform, false);
        }
        if (door1 != 2 && door2 != 2)
        {
            GameObject barrier = Instantiate(roomBarrier, getDoorTransform(room, 2).localPosition, Quaternion.Euler(0, 0, 90));
            barrier.transform.SetParent(room.transform.Find("Doors").transform, false);
        }
        if (door1 != 3 && door2 != 3)
        {
            GameObject barrier = Instantiate(roomBarrier, getDoorTransform(room, 3).localPosition, Quaternion.identity);
            barrier.transform.SetParent(room.transform.Find("Doors").transform, false);
        }

    }

}
