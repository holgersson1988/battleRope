using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour {

    public Utility.Player player;
    SpriteRenderer sprGlow;
    LevelManager levelManager;
	// Use this for initialization
	void Start () {
        // Find objects
        sprGlow = GetComponent<SpriteRenderer>();
        levelManager = FindObjectOfType<LevelManager>();
        Debug.Assert(levelManager, "No level manager found");
        
        // Set Colors 
        if (player == Utility.Player.Player1)
            sprGlow.color = levelManager.player1Color;
        else
            sprGlow.color = levelManager.player2Color;


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GoalAreaTriggered!");

        Paddle paddle = other.gameObject.GetComponentInParent<Paddle>();
        if (paddle)
        {
            levelManager.GoalScored(paddle);
        }
    }
}
