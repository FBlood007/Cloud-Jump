using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float depth = 1;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();  
    }

    // Update is called once per frame
    void Update()
    {
        float velocityReal = player.velocity.x / depth * 0.025f;
        Vector2 pos = transform.position;
        pos.x -= velocityReal * Time.fixedDeltaTime;

        if(gameObject.tag != "Background")
        {
            if (pos.x <= -25)
            pos.x = 25; 

        }

        transform.position = pos;

    }
}
