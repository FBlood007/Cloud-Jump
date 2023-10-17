using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    Player player;
    TextMeshProUGUI distTravelled;

    public void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distTravelled = GameObject.Find("DistanceTravelled").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int distance  =  Mathf.FloorToInt(player.distance);
        distTravelled.text = "Distance Travelled "+ distance.ToString() + " m";
    }
}
