using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnerScript : MonoBehaviour
{
    public GameObject spawner;
    public GameObject freshFishPrefab;
    public Rigidbody fishBody;
    public int fishOnScreen = 2;
    public int fishCap = 5;
    public float fishTimer = 3;
    public float[] spawnerPosition = { -10, -4, -3, 3, 4, 10 };
    public float[] spawnerX = { 5, -5, 5, -5, 5, -5 };
    public float[] spawnerY = { 20, 20, 15, 15, 20, 20 };

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (CanMakeFish()) MakeFish();
	}

    public bool CanMakeFish()
    {
        fishTimer -= Time.deltaTime + Random.Range(-0.05f, 0.05f);
        if (fishTimer <= 0 && fishOnScreen < fishCap)
        {
            fishTimer = 3;
            return true;
        }
        else return false;
    }

    public void MakeFish()
    {
        int spawnerNumber = Random.Range(0, 6);
        GameObject newFish = Instantiate(freshFishPrefab);
        newFish.transform.position = spawner.transform.position + new Vector3(spawnerPosition[spawnerNumber], 0, 0);
        newFish.transform.rotation = spawner.transform.rotation;
        newFish.GetComponent<Rigidbody>().velocity = new Vector3(spawnerX[spawnerNumber], spawnerY[spawnerNumber], 0);
    }
}
