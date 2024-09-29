using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorGenerator : MonoBehaviour
{
    // Room layout objects
    public GameObject[] roomPrefabs;

    // Rooms currently existing. Planning to only render two rooms ahead, and two behind for a total of 5
    private List<GameObject> activeRooms = new List<GameObject>();
    public GameObject startingRoom;
    public GameObject roomBarrier;

    public Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        // Set up starting room
        startingRoom = Instantiate(roomPrefabs[0], grid.WorldToCell(new Vector2(20, 20)), Quaternion.identity);
        startingRoom.transform.SetParent(grid.transform, false);
        activeRooms.Add(startingRoom);

        // The door the current room should connect on, given -1 because it's the first door
        int connectingDoor = randomDoor(-1);
        activeRooms.Add(generateRoom(activeRooms[activeRooms.Count - 1], 1, connectingDoor, randomRoomPrefab()));

        for (int i = 0; i<8; i++)
        {
            int newConnectingDoor = randomDoor((connectingDoor + 2) % 4);
            GameObject newRoom = generateRoom(activeRooms[activeRooms.Count - 1], connectingDoor, newConnectingDoor, randomRoomPrefab());
            newRoom.name = i.ToString();
            activeRooms.Add(newRoom);
            connectingDoor = newConnectingDoor;
        }
        
    }




    GameObject generateRoom(GameObject room, int door, int nextDoor, GameObject newRoom)
    {
        /// <summary>
        /// Generates a room and attatches it to another
        /// </summary>
        /// <param name="room"> The room that is having the new room attatched to it </param>
        /// <param name="door">represents the direction the room should connect on, reletive to the old room
        // 0 == Up,
        // 1 == Right,
        // 2 == down,
        // 3 == left
        // </param>
        /// <param name="nextDoor"> The door that the newRoom will leave open to connect to another room </param>
        /// <param name="newRoom"> The room object that should be attatched </param>
        /// <return> The room that has been placed </return>


        ///
        Transform currentDoor = getDoorTransform(room, door);

        if (currentDoor != null)
        {
            // Gets the location of the door we are attarching to
            Vector3 newRoomSpawnPoint = grid.WorldToCell(currentDoor.position);

            // Offset new room must be moved by. Is the distance from new room position to connecting door
            Vector3 offset = grid.WorldToCell(getDoorTransform(newRoom,(door+2)%4).localPosition);
            GameObject newRoomPlaced = Instantiate(newRoom, grid.WorldToCell(newRoomSpawnPoint - offset), Quaternion.identity);
            newRoomPlaced.transform.SetParent(grid.transform, false);

            // door is the connecting door for the previous room, adding 2 and mod 4 gets that connection for the new room
            // Next door is the connecting point for the new room
            placeBarrierOnEmptyDoor(newRoomPlaced, (door+2) % 4, nextDoor);
            return newRoomPlaced;
        }
        Debug.LogError("A door's position could not be found!");
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
        for(int i = 0; i < 4; i++) { 
            // Don't block the exit of room1, the exit to room2, or the entrance of room2 which is equal to (door1+2)%4
            if(i != door1 && i != door2)
            {
                // Rotate connection if exit is at the top or bottom
                if(i % 2 == 0)
                {
                    GameObject barrier = Instantiate(roomBarrier, getDoorTransform(room, i).localPosition, Quaternion.Euler(0, 0, 90));
                    barrier.transform.SetParent(room.transform.Find("Doors").transform, false);
                } else
                {
                    GameObject barrier = Instantiate(roomBarrier, getDoorTransform(room, i).localPosition, Quaternion.identity);
                    barrier.transform.SetParent(room.transform.Find("Doors").transform, false);
                }
                
            }
        }

    }

    GameObject randomRoomPrefab()
    {
        return roomPrefabs[Random.Range(0, roomPrefabs.Length)];
    }

    int randomDoor(int previousDoor)
    {
        // Must pick a door that wasn't just used, or rooms overlap
        int newDoor = Random.Range(0, 3);
        while(newDoor == previousDoor) {
            newDoor = Random.Range(0, 3);
        }

        return newDoor;
    }

}
