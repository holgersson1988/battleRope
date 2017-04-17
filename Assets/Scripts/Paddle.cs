using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Paddle : MonoBehaviour {

    public Utility.Player player;

    public Transform muzzle;
    public RopeCreator ropeCreatorPrefab;
    public Color ropeColor;
    public float gravity;
    public Vector3 gravityDirection;

    public float paintMax;
    public float paint;
    public float paintRefillRate;

    public SpriteRenderer sprPaddle;
    public SpriteRenderer sprPaint;
    Vector3 paintSpriteScaleDefault;
    Vector3 paintSpriteScale;

    public List<RopeCreator> ropeTypes;

    public Vector3 startPos;

    Rigidbody body;
    RopeCreator ropeCreator;
    bool drawing = false;
    int ropesCreated = 0;
    
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        startPos = transform.position;

        // Paint stats
        paintMax = 5;
        paintRefillRate = 2.5f;
        paint = paintMax / 2f;

        sprPaint.color = ropeColor;
        sprPaddle.color = Color.black;

        paintSpriteScaleDefault = sprPaint.transform.localScale;
        paintSpriteScale.x = paintSpriteScaleDefault.x * (paint / paintMax);

        if (player == Utility.Player.Player1)
        {
            sprPaint.color = FindObjectOfType<LevelManager>().player1Color;
        }
        else
        {
            sprPaint.color = FindObjectOfType<LevelManager>().player2Color;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (ropeCreator == null)
        {
            sprPaint.transform.localScale = new Vector3(paintSpriteScaleDefault.x * (paint / paintMax), paintSpriteScaleDefault.y, paintSpriteScaleDefault.z);
        }
        else
        {
            sprPaint.transform.localScale = new Vector3(paintSpriteScaleDefault.x * ( (paint - ropeCreator.usedPaint) / paintMax), paintSpriteScaleDefault.y, paintSpriteScaleDefault.z);
        }

        // Refill paint
        if (!drawing)
        {
            paint += paintRefillRate * Time.deltaTime;
            if (paint > paintMax)
                paint = paintMax;
        }
    }

    public void HandleInput(bool left, bool right, int drawRope, bool stopDraw)
    {
        float acc = 2000f * Time.deltaTime;
        if (left)
        {
            body.AddForce(new Vector2(-acc, 0f));
        }
        if (right)
        {
            body.AddForce(new Vector2(acc, 0f));
        }

        if (drawRope != -1 && !drawing)
        {
            ropeCreator = Instantiate(ropeTypes[drawRope], muzzle.position, Quaternion.identity);
            ropeCreator.SetupRopeCreator(muzzle.position.x, this);
            ropeCreator.index = ropesCreated;
            ropesCreated++;
            drawing = true;
        }
        else if (stopDraw && ropeCreator != null)
        {
            ropeCreator.CreateRope();
            ropeCreator = null;
            drawing = false;
        }
    }

    // Reset the Paddle to original color values and start position
    public void ResetLevel()
    {
        
        body.velocity = Vector3.zero;
        transform.position = startPos;

        paint = paintMax / 2f;
    }
}
