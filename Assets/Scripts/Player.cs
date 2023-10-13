using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float gravity;
    public Vector2 velocity;
    public float acceleration = 10f;
    public float maxAcceleration = 10f;
    public float maxXVelocity = 90f;
    public float groundHeight = 5f;
    public bool isGrounded = false;
    public float jumpVelocity = 5f;
    public float distance = 0f;

    public bool isTouched = false;
    public float maxTouchedJumpTime = 0.2f;
    public float touchJumpTimer = 0.0f;


    public float jumpGroundThreshold = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);

        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            if(Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Began) {
                isGrounded = false;
                velocity.y = jumpVelocity;   
                touchJumpTimer = 0;
                if (Input.GetTouch(0).phase == TouchPhase.Stationary) {
                    isTouched = true;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    isTouched = false;
                }
            }
           
        }

       
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if (!isGrounded)
        {

            if (isTouched)
            {
                touchJumpTimer += Time.fixedDeltaTime;
                if(touchJumpTimer > maxTouchedJumpTime) {
                    isTouched = false;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;
            if(!isTouched)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
            if(pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded=true;
            }
           
        }
        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }
        }
        transform.position = pos;
    }

}
