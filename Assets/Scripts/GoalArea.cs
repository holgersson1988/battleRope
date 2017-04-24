using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour {

    public Utility.Player player;
    SpriteRenderer sprGlow;
    LevelManager levelManager;
    float lifeTime = 0;
    float glowSpeed = 3f;
    // Use this for initialization
    void Start () {
        // Find objects
        sprGlow = GetComponent<SpriteRenderer>();
        levelManager = FindObjectOfType<LevelManager>();
        Debug.Assert(levelManager, "No level manager found");

        // Set Colors 
        sprGlow.color = levelManager.goalColor;
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime += Utility.DeltaTime();

        sprGlow.color = new Color(sprGlow.color.r, sprGlow.color.g, sprGlow.color.b, ( (Mathf.Sin(lifeTime*glowSpeed) + 1f) / 2f ) * 0.2f + 0.5f);
	}

    private void OnTriggerEnter(Collider other)
    {
        Paddle paddle = other.gameObject.GetComponentInParent<Paddle>();
        if (paddle)
        {
            levelManager.GoalScored(paddle);
        }
    }
}
