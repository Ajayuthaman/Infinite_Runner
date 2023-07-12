using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] groundPrefabs;

    private Transform playerTranform;
    private float spawnZ = 0.0f;
    private float groundLength = 30.0f;
    private int amountOfGrounds = 5;
    private float safeZone = 18.0f;

    //random generating
    private int lastPrefabIndex = 0;

    private List<GameObject> activeGrounds;

    // Start is called before the first frame update
    void Start()
    {
        activeGrounds = new List<GameObject>();

        playerTranform = GameObject.FindGameObjectWithTag("Player").transform;

        for(int i = 0; i < amountOfGrounds; i++)
        {
            if (i < 1)
            {
                SpawnGrounds(0);
            }
            else
            {
                SpawnGrounds();
            }
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(playerTranform.position.z -  safeZone > (spawnZ - amountOfGrounds * groundLength))
        {
            SpawnGrounds();
            DeleteGrounds();
        }
        
    }

    void SpawnGrounds(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
        {
            go = Instantiate(groundPrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(groundPrefabs[prefabIndex]) as GameObject;
        }
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;

        spawnZ += groundLength;

        activeGrounds.Add(go);
    }

    void DeleteGrounds()
    {
        Destroy(activeGrounds[0]);

        activeGrounds.RemoveAt(0); 
    }

    private int RandomPrefabIndex()
    {
        if(groundPrefabs.Length <= 0)
        
            return 0;


        int randomIndex = lastPrefabIndex;
        
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, groundPrefabs.Length);
        }
        lastPrefabIndex = randomIndex;

         return randomIndex;
    }

}
