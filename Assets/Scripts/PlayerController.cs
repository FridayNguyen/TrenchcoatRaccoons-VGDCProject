using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private static readonly int MAX_RACCOONS = 10;

    private static List<GameObject> aliveRaccoonGameObjects = new List<GameObject>();
    private static int selectedIndex = 0;
    
    void Start () {
        aliveRaccoonGameObjects = InitRaccoons();
	}
	
	void Update () {
        ListenControls();
	}
    
    private void ListenControls()
    {
        if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectUp();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SelectDown();
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Shoot();
            }
        }
    }

    private void Jump()
    {
        // If the selected raccoon is grounded, then make all the raccoon on top of it jump
        //if(aliveRaccoonGameObjects[selectedIndex].GetComponent<RaccoonAction>().grounded)
        //{
            for (int i = selectedIndex; i < aliveRaccoonGameObjects.Count; i++)
            {
                aliveRaccoonGameObjects[i].GetComponent<RaccoonAction>().Jump();
            }
        //}
    }

    private void SelectUp()
    {
        if (selectedIndex < aliveRaccoonGameObjects.Count - 1)
            selectedIndex++;

        print("Current Index: " + selectedIndex);
    }

    private void SelectDown()
    {
        if (selectedIndex > 0)
            selectedIndex--;

        print("Current Index: " + selectedIndex);
    }

    private void Shoot()
    {
        aliveRaccoonGameObjects[selectedIndex].GetComponent<pickup_detector>().Shoot();
    }

    private void Select(int index)
    {
        if (index >= aliveRaccoonGameObjects.Count)
        {
            print("Error when selecting racoon: index out of bounds");
            return;
        }
        selectedIndex = index;
        print("Current Index: " + selectedIndex);
    }

    private List<GameObject> InitRaccoons()
    {
        List<GameObject> aliveRaccoonGameObjects = new List<GameObject>();

        aliveRaccoonGameObjects.Clear();

        aliveRaccoonGameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        aliveRaccoonGameObjects = aliveRaccoonGameObjects.OrderBy(o => o.name).ToList();

        print("Raccoon Selector Initialization " +
            (aliveRaccoonGameObjects.Count == MAX_RACCOONS ?
                "Successful" :
                "Failed\nCount: " + aliveRaccoonGameObjects.Count));

        return aliveRaccoonGameObjects;
    }

    private void AddRaccoon(GameObject raccoonGameObject)
    {
        aliveRaccoonGameObjects.Add(raccoonGameObject);
    }
}
