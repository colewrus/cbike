using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour {

    float xDespawn;

    private void Start()
    {
         xDespawn = camFollow.instance.despawnPoint.position.x;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * camFollow.instance.groundSpeed * Time.deltaTime);
        if (transform.position.x <= xDespawn)
        {
            this.gameObject.SetActive(false);
        }
       
    } 
    
}
