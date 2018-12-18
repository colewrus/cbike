using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StartZone
{
    public GameObject platform;
    public Vector3 pos;
}

public class GM : MonoBehaviour {
    
    
    

    public List<StartZone> StartPlatforms = new List<StartZone>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
