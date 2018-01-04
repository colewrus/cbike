using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour {

    public static spawnScript instance = null;

    public List<GameObject> Platforms = new List<GameObject>();
    public float timeToSpawn; //like tears in rain
    public float spawnStart;

    List<GameObject> currentObj = new List<GameObject>();
    public bool runSpawn;

    float stopWatch;
    public int selection;
    public int selector;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        spawnStart = timeToSpawn;
        stopWatch = 1;
        selection = Random.Range(0, Platforms.Count);
        Init_Pool();
        runSpawn = true;

        selector = Random.Range(0, Platforms.Count);
        Vector3 pos = new Vector3(this.transform.position.x+1, Random.Range(2.6f, 2.35f), transform.position.z);
        GameObject tempObj = (GameObject)Instantiate(Platforms[selector], pos, Quaternion.identity);
        currentObj.Add(tempObj);        
    }

    public void SpawnPlatform(Vector3 pos, int ID)
    {
       
        selector = Random.Range(0, Platforms.Count);        
        Debug.Log(pos);
        GameObject tempObj = (GameObject)Instantiate(Platforms[ID], pos, Quaternion.identity);
        currentObj.Add(tempObj);
    }

    void Init_Pool()
    {
        Vector3 pos = new Vector3(this.transform.position.x, Random.Range(2, 2.35f), transform.position.z);
        for (int i = 0; i < Platforms.Count; i++)
        {
            GameObject tempObj = (GameObject)Instantiate(Platforms[i], pos, Quaternion.identity);
            currentObj.Add(tempObj);
            tempObj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnToggle();
    }

    public void SpawnToggle()
    {

        if (stopWatch < timeToSpawn && runSpawn)
        {
            stopWatch += Time.deltaTime;
        }
        else
        {
            selector = Random.Range(0, Platforms.Count);
            if (selector != selection)
            {
                selection = selector;
            }
            else
            {
                return;
            }
            Vector3 pos = new Vector3(this.transform.position.x, Random.Range(2, 2.35f), transform.position.z);
            GameObject tempObj = (GameObject)Instantiate(Platforms[selector], pos, Quaternion.identity);
            currentObj.Add(tempObj);

            //currentObj[selection].transform.position = pos;
            //currentObj[selection].SetActive(true);
            stopWatch = 0;
        }
        
    }



    public void ResetGround()
    {
        Debug.Log("respawn");
        
        for (int i = 0; i < currentObj.Count; i++)
        {       
            currentObj[i].SetActive(false);
            /*
            currentObj[i].transform.position = this.transform.position;
            */
            stopWatch = 0;
            selector = Random.Range(0, Platforms.Count);
            Vector3 pos = new Vector3(this.transform.position.x, Random.Range(2, 2.45f), transform.position.z);
            currentObj[selection].transform.position = pos;
            currentObj[selection].SetActive(true);
            
        }
        
    }
}
