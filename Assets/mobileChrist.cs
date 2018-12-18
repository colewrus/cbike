using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobileChrist : MonoBehaviour {

    //Movement
    public float direction;
    Rigidbody2D rb;
    public float speed;
    public float jumpMin;

    //Touch vars
    float tapTimer;

    float width;
    float height;


    Vector3 position;

    //VFX
    Animator anim;


    private void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }


    // Use this for initialization
    void Start () {

        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void Movement() 
    {
        
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            tapTimer += 1 * Time.deltaTime;

            if (Camera.main.ScreenToWorldPoint(touch.position).x < transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }


            if(touch.phase = TouchPhase.Moved)
            {
                Vector2 pos = touch.position;

                direction = (pos.x > (Screen.width / 2)) ? 1 : -1;

                rb.velocity = new Vector2((direction * speed), rb.velocity.y);
            }

        }


    }

}
