using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorGenerator : MonoBehaviour
{
    public GameObject room;
    public GameObject startingRoom;
    public GameObject room2;
    public Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        startingRoom = Instantiate(room2, grid.WorldToCell(new Vector2(0, 0)), Quaternion.identity);
        startingRoom.transform.SetParent(grid.transform, false);

        GameObject placedRoom = generateRoom(startingRoom, 1, room2);
        generateRoom(placedRoom, 1, room2);
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
            //Vector3 newRoomConnectorDoor = grid.WorldToCell(getDoorTransform(newRoom, (door + 2) % 4).);
            Vector3 offset = grid.WorldToCell(getDoorTransform(newRoom,(door+2)%4).position - newRoom.transform.position);
            GameObject newRoomPlaced = Instantiate(newRoom, grid.WorldToCell(newRoomSpawnPoint - offset), Quaternion.identity);
            newRoomPlaced.transform.SetParent(grid.transform, false);
            return newRoomPlaced;
        }
        return null;
    }

    Transform getDoorTransform(GameObject room, int door)
    {
        if(door == 0){
            //return startingRoom.transform.Find("Doors").Find("UpperDoor");

        }else if(door == 1){
            return room.transform.Find("Doors").Find("RightDoor");

        }else if(door == 2){


        }else if (door == 3){
            return room.transform.Find("Doors").Find("LeftDoor");

        }

        return null;
    }

    /* // Find the "Doors" GameObject
        Transform doorsTransform = startingRoom.transform.Find("Doors");

        if (doorsTransform != null)
        {
            // Now find the "RightDoor" child using the Transform component
            Transform rightDoorTransform = doorsTransform.transform.Find("RightDoor");
            Debug.Log("BRUH" + rightDoorTransform.position);

            if (rightDoorTransform != null)
            {
                Debug.Log("Right Door found: " + rightDoorTransform.gameObject.name);
                Vector3 spawnPosition = grid.WorldToCell(rightDoorTransform.position);
                Debug.Log("SpawnPosition" + spawnPosition);
                Debug.Log("Right door" + rightDoorTransform.position);
                Vector3 offset = grid.WorldToCell(room.transform.position - room.transform.Find("Doors").transform.Find("LeftDoor").position);
                Debug.Log("offset" + offset);
                GameObject newRoom = Instantiate(newRoomPrefab, grid.WorldToCell(spawnPosition + offset), Quaternion.identity);
                newRoom.transform.SetParent(grid.transform, false);
            }
            else
            {
                Debug.LogError("Right Door not found!");
            }
        }
        else
        {
            Debug.LogError("Doors GameObject not found!");
        }
    }
    */
}
