using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Son : MonoBehaviour {

    public float speed;
    public float jumpPower;
    public GameObject startObj;
    public Transform startPos;
    public GameObject spawnObj;
    public GameObject postdeathPanel;

    bool checkScore;
    Rigidbody2D rb;
    public bool grounded; //did you touch the ground recently?

    public float gravity;

    float yVel; //variable y velocity to help manage jumping & falling
    public float xVel;
    Vector3 storedVelocity;

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
        yVel = gravity;
        decayJump = false;
        boosting = false;

        checkScore = false;
        postdeathPanel.SetActive(false);
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
    }

    void DeathCheck()
    {
        camFollow.instance.runScore = false;
        if(checkScore)
            HighScore();
        //pause shit
        spawnScript.instance.runSpawn = false;
        camFollow.instance.groundSpeed = 0;
        spawnScript.instance.timeToSpawn = 0;
        speed = 0;
        camFollow.instance.score = 0;
        postdeathPanel.SetActive(true);
    }


    public void PostDeathResume()
    {
        startObj.transform.position = startPos.position;
        this.gameObject.transform.position = startPos.position + (transform.up * 1.5f) + (transform.right*-1);
        spawnObj.GetComponent<spawnScript>().ResetGround();
        startObj.gameObject.SetActive(true);
        spawnScript.instance.SpawnPlatform(new Vector3(1, 2.5f, 0), 0);
        spawnScript.instance.runSpawn = true;
        camFollow.instance.groundSpeed = camFollow.instance.startSpeed;
        spawnScript.instance.timeToSpawn = spawnScript.instance.spawnStart;
        camFollow.instance.runScore = true;
        postdeathPanel.SetActive(false);
    }

    void HighScore()
    {

        for(int i=0; i < camFollow.instance.HighScore.Count; i++)
        {

            spawnScript.instance.runSpawn = false;
            this.gameObject.transform.position = startPos.position + (transform.up * 0.5f);
            camFollow.instance.groundSpeed = camFollow.instance.startSpeed;
            spawnScript.instance.timeToSpawn = spawnScript.instance.spawnStart;            
            
           spawnScript.instance.ResetGround();
            
            startObj.transform.position = startPos.position;
            startObj.gameObject.SetActive(true);
            
            camFollow.instance.score = 0;
            spawnScript.instance.SpawnPlatform(new Vector3(0f, 2.5f, 0), 0);
            spawnScript.instance.runSpawn = true;


        }
        PlayerPrefs.SetInt(camFollow.instance.key_0, camFollow.instance.HighScore[0]);
        PlayerPrefs.SetInt(camFollow.instance.key_1, camFollow.instance.HighScore[1]);
        PlayerPrefs.SetInt(camFollow.instance.key_2, camFollow.instance.HighScore[2]);
        postdeathPanel.transform.GetChild(0).GetComponent<Text>().text = "" + camFollow.instance.HighScore[0];
        postdeathPanel.transform.GetChild(1).GetComponent<Text>().text = "" + camFollow.instance.HighScore[1];
        postdeathPanel.transform.GetChild(2).GetComponent<Text>().text = "" + camFollow.instance.HighScore[2];
        checkScore = false;
        //Debug.Log(PlayerPrefs.GetInt(camFollow.instance.key_0).ToString());

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

        if (collision.gameObject.tag == "reset")
        {
            Debug.Log("reset");
            checkScore = true;
            DeathCheck();
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
            grounded = false;
        }

        if (boosting && xVel <= 0f)
        {
            boosting = false;
            grounded = true;
            yVel = gravity;
        }
        

    }

    void Boost(float newX)
    {
        boosting = true;        
        xVel = newX;
        yVel = 0;        
    }
 
    void JumpDecay()
    {
       if(yVel > gravity)
        {
            yVel -= 12 * Time.deltaTime;           
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
        yVel = gravity;
    }




}
