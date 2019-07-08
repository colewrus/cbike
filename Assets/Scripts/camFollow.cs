using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public class camFollow : MonoBehaviour {

    public static camFollow instance = null;

    public GameObject jesu;
    public Vector3 offset;
    public float groundSpeed;

    public float score;
    public bool runScore;
    public List<int> HighScore = new List<int>();
    public Text counter;

    public string key_0;
    public string key_1;
    public string key_2;
    public string key_3;

    public float speedRate;

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
        score = 0;
        HighScore[0] = PlayerPrefs.GetInt(key_0, 0);
        HighScore[1] = PlayerPrefs.GetInt(key_1, 0);
        HighScore[2] = PlayerPrefs.GetInt(key_2, 0);
        runScore = true;
	}
	
	// Update is called once per frame
	void Update () {
        SpeedModifier();
        this.transform.position = new Vector3(jesu.transform.position.x + offset.x, Mathf.Clamp(jesu.transform.position.y, 2.2f, 3), offset.z);
        if(runScore)
            ScoreTick();
	}

    void ScoreTick()
    {
        score += 2.5f * Time.deltaTime;
        counter.text = "Score: " + Mathf.FloorToInt(score);
    }

    void SpeedModifier()
    {
        if(timer < tick_length)
        {
            timer += 1 * Time.deltaTime;
        }
        else
        {
            groundSpeed += speedRate;
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
