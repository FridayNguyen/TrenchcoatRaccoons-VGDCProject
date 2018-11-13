using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RacoonSelector : MonoBehaviour {

    private static readonly int MAX_RACOONS = 10;
    private static int currentAliveCount = 10;

    private static List<GameObject> allRacoonGameObjects = new List<GameObject>();
    private static int selectedIndex = 0;
    
	void Start () {
        if(allRacoonGameObjects.Count != MAX_RACOONS)
        {
            allRacoonGameObjects.Clear();

            allRacoonGameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            allRacoonGameObjects = allRacoonGameObjects.OrderBy(o => o.name).ToList();
            foreach(GameObject gameObject in allRacoonGameObjects)
            {
                print(gameObject.name);
            }

            print("Racoon Selector Initialization " +
                (allRacoonGameObjects.Count == MAX_RACOONS ?
                    "Successful" :
                    "Failed\nCount: " + allRacoonGameObjects.Count));
        }
    }
	
	void Update () {
        //if (allRacoonGameObjects[selectedIndex].GetComponent<Transform>().position.y < 0)
        //{
        //    if(selectedIndex == 0) SelectUp();
        //    currentAliveCount--;
        //}
	}

    public static int GetTopStack()
    {
        return currentAliveCount - selectedIndex;
    }

    public static GameObject Select(int index)
    {
        if (index >= allRacoonGameObjects.Count)
        {
            print("Error when selecting racoon: index out of bounds");
            return null;
        }
        selectedIndex = index;
        print("Current Index: " + selectedIndex);
        return allRacoonGameObjects[selectedIndex];
    }

    public static GameObject SelectUp()
    {
        if (selectedIndex < currentAliveCount - 1)
            selectedIndex++;

        print("Current Index: " + selectedIndex);
        return allRacoonGameObjects[selectedIndex];
    }

    public static GameObject SelectDown()
    {
        if (selectedIndex > 0)
            selectedIndex--;

        print("Current Index: " + selectedIndex);
        return allRacoonGameObjects[selectedIndex];
    }

}
