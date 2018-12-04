using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public List<GameObject> aliveRaccoonGameObjects = new List<GameObject>();
    public int selectedIndex = 0;
    public GameObject raccoon;

    private AudioSource SelectSound;
    private bool raccoonSpawned;
    private float timer;


    void Start () {
        aliveRaccoonGameObjects = InitRaccoons();
        SelectSound = GameObject.Find("SelectSound").GetComponent<AudioSource>();
        raccoonSpawned = false;
        timer = .1f;
	}
	
	void Update () {
        ListenControls();
        if (raccoonSpawned)
        {
            timer -= Time.deltaTime;
            if(timer < 0f)
            {
                raccoonSpawned = false;
                timer = .1f;
            }
        }
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
                //raccoonAction.hasCoonBelow = false;
                raccoonAction.Jump();
            }
        }  
    }

    private void Shoot()
    {
        aliveRaccoonGameObjects[selectedIndex].GetComponent<RaccoonAction>().Shoot();
    }

   /* private void UpdateCoonVar()
    {
        if (aliveRaccoonGameObjects.Count > 0)
        {
            for (int i = 0; i < aliveRaccoonGameObjects.Count; i++)
            {
                aliveRaccoonGameObjects[i].GetComponent<RaccoonAction>().coonIndex = i;                
            }
        }
    }*/

    public void SelectUp()
    {
        if (selectedIndex < aliveRaccoonGameObjects.Count - 1)
            selectedIndex++;
        SelectSound.Play();
        print("Current Index: " + selectedIndex);
    }

    public void SelectDown()
    {
        if (selectedIndex > 0)
            selectedIndex--;
        SelectSound.Play();
        print("Current Index: " + selectedIndex);
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

        print("Raccoon Initialization " +
            (aliveRaccoonGameObjects.Count == 1 ?
                "Successful" :
                "Failed"));

        return aliveRaccoonGameObjects;
    }
    public void SpawnRaccoon()
    {
        if (!raccoonSpawned)
        {
            raccoonSpawned = true;
            GameObject topRaccoon = aliveRaccoonGameObjects[aliveRaccoonGameObjects.Count - 1];
            GameObject mainCamera = GameObject.Find("Main Camera");

            Quaternion rotation = transform.rotation;
            Transform parent = GameObject.Find("AllCoons-DoNotRename").transform;
            float x = topRaccoon.transform.GetChild(0).transform.position.x;
            float y = mainCamera.transform.position.y + mainCamera.transform.localScale.y * (0.6f);
            Vector3 spawnPoint = new Vector3(x, y);

            aliveRaccoonGameObjects.Add(Instantiate(raccoon, spawnPoint, rotation, parent));
        }
    }
}
