using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Son : MonoBehaviour {

    public float speed;
    public float jumpPower;
    public GameObject startObj;
    public Transform startPos;
    public GameObject spawnObj;


    Rigidbody2D rb;
    bool grounded; //did you touch the ground recently?
    
    float yVel; //variable y velocity to help manage jumping & falling

    bool decayJump;

    //button timer
    float downTime;
    public float pressTime;

	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        grounded = false;
      
        pressTime = 0;
        yVel = -1;
        decayJump = false;
	}

    void Update()
    {


        rb.velocity = new Vector3(0, yVel, 0);

        if (Input.GetKey(KeyCode.Space)) //give a certain amoutn of control for holding down the space key, 0.1-0.3 seconds to "charge" a jump
        {
            pressTime += 1* Time.deltaTime;
            if (grounded)
            {             
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                yVel = 1f;
                grounded = false; 
            }
        }

        JumpChecks();
 
        if(transform.position.y <= 1.0f)
        {
            spawnObj.GetComponent<spawnScript>().ResetGround();
            Debug.Log(spawnObj.GetComponent<spawnScript>().runSpawn);
            startObj.transform.position = startPos.position;
            startObj.gameObject.SetActive(true);
            this.gameObject.transform.position = startPos.position + (transform.up*0.5f);
            
        }
 
    }

    void JumpChecks()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            decayJump = true;
            pressTime = 0;
        }

        if (pressTime >= 0.7f)
        {
            decayJump = true;
        }

        if (decayJump)
        {
            JumpDecay();
        }
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!grounded)
        {
            if (collision.gameObject.tag == "ground")
            {
                grounded = true;
                pressTime = 0;
            }
        }
    }
 
    void JumpDecay()
    {
       if(yVel > -1)
        {
            yVel -= 2 * Time.deltaTime;           
        }
        else
        {
            decayJump = false;
        }
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(0.3f);
        decayJump = true;
        yVel = -1;
    }




}
