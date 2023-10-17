using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    Player player;

    public float groundHeight;
    public float groundRight;
    public float screenRight;
    BoxCollider2D coll;

    bool didGenerateCloud = false;

    private void Awake()
    {
        player  = GameObject.Find("Player").GetComponent<Player>();

        coll = GetComponent<BoxCollider2D>();
       
        screenRight = Camera.main.transform.position.x * 2f; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        groundHeight = transform.position.y + (coll.size.y / 2);
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        groundRight = transform.position.x + (coll.size.x / 2) ;

        if(groundRight < -15f)
        {
             Destroy(gameObject);
            return;
        }

        if (!didGenerateCloud)
        {
            Debug.Log("Ground right " + groundRight + " Screen Right" + screenRight);
            if (groundRight < screenRight)
            {
                didGenerateCloud = true;
                generateCloud();
            }

        }
        transform.position = pos;
    }

    void generateCloud()
    {
        GameObject cloud = Instantiate(gameObject);
        BoxCollider2D cloudCollider = cloud.GetComponent<BoxCollider2D>();
        Vector2 pos;
        /*pos.x = screenRight + 25;
        pos.y = transform.position.y;
        cloud.transform.position = pos;*/


        float h1 = player.jumpVelocity * player.maxTouchedJumpTime;
        float t = player.jumpVelocity / -player.gravity;
        float h2 = player.jumpVelocity * t + (0.5f * (player.gravity * (t * t)));
        float maxJumpHeight = h1 + h2;
        float maxY = maxJumpHeight * 0.7f;
        maxY += groundHeight;
        float minY = 1;
        float actualY = UnityEngine.Random.Range(minY, maxY);

        pos.y = actualY - cloudCollider.size.y / 2;
        if (pos.y > 2.7f)
            pos.y = 2.7f;

        float t1 = t + player.maxTouchedJumpTime;
        float t2 = Mathf.Sqrt((2.0f * (maxY - actualY)) / -player.gravity);
        float totalTime = t1 + t2;
        float maxX = totalTime * player.velocity.x;
        maxX *= 0.7f;
        maxX += groundRight + 5;
        float minX = screenRight + 15;
        float actualX = UnityEngine.Random.Range(minX, maxX);

        //pos.x = actualX + cloudCollider.size.x ;
        pos.x = actualX + cloudCollider.size.x;
        cloud.transform.position = pos;

        Cloud goGround = cloud.GetComponent<Cloud>();
        goGround.groundHeight = cloud.transform.position.y + (cloudCollider.size.y / 2);

    }
}
