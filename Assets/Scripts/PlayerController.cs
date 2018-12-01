using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private static readonly int MAX_RACCOONS = 10;

    public List<GameObject> aliveRaccoonGameObjects = new List<GameObject>();
    public int selectedIndex = 0;

    
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
        for (int i = selectedIndex; i < aliveRaccoonGameObjects.Count; i++)
        {
            if (aliveRaccoonGameObjects[i].GetComponent<RaccoonAction>().grounded || aliveRaccoonGameObjects[i].GetComponent<RaccoonAction>().hasCoonBelow)
            {
                RaccoonAction raccoonAction = aliveRaccoonGameObjects[i].GetComponent<RaccoonAction>();
                raccoonAction.hasCoonBelow = false;
                raccoonAction.Jump();
            }
        }  
    }

    private void UpdateCoonVar()
    {
        if (aliveRaccoonGameObjects.Count > 0)
        {
            for (int i = 0; i < aliveRaccoonGameObjects.Count; i++)
            {
                aliveRaccoonGameObjects[i].GetComponent<RaccoonAction>().coonIndex = i;                
            }
        }
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
        aliveRaccoonGameObjects[selectedIndex].GetComponent<RaccoonAction>().Shoot();
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
            (aliveRaccoonGameObjects.Count > 0 ?
                "Successful" :
                "Failed\nCount: " + aliveRaccoonGameObjects.Count));

        return aliveRaccoonGameObjects;
    }

}
