using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour {

    public static camFollow instance = null;

    public GameObject jesu;
    public Vector3 offset;
    public float groundSpeed;

    public Transform despawnPoint;

    public float startSpeed;

    public float tick_length;
    float timer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        timer = 0;
        startSpeed = groundSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        SpeedModifier();
        this.transform.position = new Vector3(jesu.transform.position.x + offset.x, Mathf.Clamp(jesu.transform.position.y, 2.2f, 3), offset.z);
	}

    void SpeedModifier()
    {
        if(timer < tick_length)
        {
            timer += 1 * Time.deltaTime;
        }
        else
        {
            groundSpeed += 0.1f;
            if(spawnScript.instance.timeToSpawn > 1.5f)
            {
                spawnScript.instance.timeToSpawn -= 0.25f;
            }

            //Debug.Log("GS: " + groundSpeed + " TTS: " + spawnScript.instance.timeToSpawn);
            timer = 0;
        }
    }


    /*
    void HollyStinks(bool Correct)
    {
        Debug.Log(Correct);
    }
    */
}
