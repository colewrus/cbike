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
    public float xVel;

    bool decayJump;
    public bool boosting;

    //button timer
    float downTime;
    public float pressTime;

	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        grounded = false;
      
        pressTime = 0;
        yVel = -2;
        decayJump = false;
        boosting = false;
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
        BoostCheck();
        Dash();
        DeathCheck();

    }

    void DeathCheck()
    {
        if (transform.position.y <= 1.0f)
        {
            this.gameObject.transform.position = startPos.position + (transform.up * 0.5f);
            camFollow.instance.groundSpeed = camFollow.instance.startSpeed;
            spawnScript.instance.timeToSpawn = spawnScript.instance.spawnStart;            
            Debug.Log(camFollow.instance.groundSpeed);
            spawnObj.GetComponent<spawnScript>().ResetGround();
            Debug.Log(spawnObj.GetComponent<spawnScript>().runSpawn);
            startObj.transform.position = startPos.position;
            startObj.gameObject.SetActive(true);
            spawnScript.instance.SpawnPlatform(new Vector3(2.2f, 2.5f, 0));
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

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!boosting)
            {
                Boost(3);
            }
            
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

    void BoostCheck()
    {
        float bandtime = 0;
        if (xVel > 0 && boosting)
        {
            xVel -= 10 * Time.deltaTime;
            
            float curr_Time = 0;
            curr_Time += Time.deltaTime;
            Vector3 boostPos = transform.position + transform.right * xVel;
            transform.position = Vector3.Lerp(transform.position, boostPos, curr_Time);
        }

        if (boosting && xVel <= 0f)
        {
            boosting = false;
        }
        

    }

    void Boost(float newX)
    {
        boosting = true;        
        xVel = newX;
        yVel = 0;
        grounded = true;
    }
 
    void JumpDecay()
    {
       if(yVel > -2)
        {
            yVel -= 8 * Time.deltaTime;           
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
