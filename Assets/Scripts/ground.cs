using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour {


    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * camFollow.instance.groundSpeed * Time.deltaTime);
        if(transform.position.x <= -7)
        {
            this.gameObject.SetActive(false);
        }
       
    } 
    
}
