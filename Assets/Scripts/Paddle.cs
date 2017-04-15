using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Paddle : MonoBehaviour {
    public Transform muzzle;
    public RopeCreator ropeCreatorPrefab;
    public Color ropeColor;
    public float gravity;
    public Vector3 gravityDirection;

    public float paint = 3f;

    Rigidbody body;
    RopeCreator ropeCreator;
    bool drawing = false;
    int ropesCreated = 0;
    
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HandleInput(bool left, bool right, bool drawRope, bool stopDraw)
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

        if (drawRope && !drawing)
        {
            ropeCreator = Instantiate(ropeCreatorPrefab, muzzle.position, Quaternion.identity);
            ropeCreator.SetupRopeCreator(muzzle.position.x, this);
            ropeCreator.index = ropesCreated;
            ropesCreated++;
            drawing = true;
        }
        else if (stopDraw && ropeCreator != null)
        {
            ropeCreator.Release();
            ropeCreator = null;
            drawing = false;
        }
    }
}
