using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour {

    public static camFollow instance = null;

    public GameObject jesu;
    public Vector3 offset;
    public float groundSpeed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(jesu.transform.position.x + offset.x, Mathf.Clamp(jesu.transform.position.y, 2.2f, 3), offset.z);
	}

    void HollyStinks(bool Correct)
    {
        Debug.Log(Correct);
    }
}
