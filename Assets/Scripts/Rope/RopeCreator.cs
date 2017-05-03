using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCreator : MonoBehaviour {

    public Rope ropePrefab;
    public RopeSection ropeSectionPrefab;
    public float initialPaintCost;
    Paddle creatorPaddle;
    Vector2 ropeEnds;
    LineRenderer line;
    public int index = 0;
    public float usedPaint = 0f;
    float paintAvailable = 0f;
    

	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        line.material.SetColor("_EmissionColor", creatorPaddle.ropeColor);
    }

	// Update is called once per frame
	void Update () {
        Vector3 paddlePos = creatorPaddle.transform.position;
        if (usedPaint < paintAvailable)
        {
            if (paddlePos.x < ropeEnds.x)
            {
                ropeEnds.x = paddlePos.x;
            }
            else if (paddlePos.x > ropeEnds.y)
            {
                ropeEnds.y = paddlePos.x;
            }
        }
        usedPaint = initialPaintCost + ropeEnds.y - ropeEnds.x;
        

        line.SetPosition(0, new Vector3(ropeEnds.x, transform.position.y, 0));
        line.SetPosition(1, new Vector3(ropeEnds.y, transform.position.y, 0));
	}

    public void SetupRopeCreator(float xpos, Paddle paddle)
    {
        ropeEnds = new Vector2(xpos, xpos);
        creatorPaddle = paddle;
        paintAvailable = paddle.paint;
    }

    // Create a rope when released
    public virtual void CreateRope()
    {
        Rope rope = CreateRopeWithSectionType(ropeSectionPrefab);
        
        // Customize rope. Different ropeCreators overrides this to add special sections or change the weight
        CustomizeRope(rope);
        rope.Setup(creatorPaddle.ropeColor, creatorPaddle.gravityDirection, creatorPaddle.gravity);

        SetRopeLayer(rope);
        creatorPaddle.paint -= usedPaint;
        Destroy(gameObject);
    }

    protected virtual void CustomizeRope(Rope rope)
    {
        Destroy(rope.sections[0].GetComponent<ConfigurableJoint>());
    }
   

    protected Rope CreateRopeWithSectionType(RopeSection sectionPrefab)
    {
        // Create Rope Container
        Rope ropeContainer = Instantiate(ropePrefab, transform.position, Quaternion.identity) as Rope;
        ropeContainer.name = "Rope " + index;

        // Create First section and remove its joint
        RopeSection lastSection = Instantiate(sectionPrefab, new Vector3(ropeEnds.x, transform.position.y, 0), Quaternion.identity) as RopeSection;

        // Connect to Rope Container
        ropeContainer.AddSection(lastSection);

        // Count variables and section length
        float offset = 0f;
        float sectionLength = lastSection.transform.localScale.x - 0.05f;

        // Calculate number of sections based on draw length
        float distance = ropeEnds.y - ropeEnds.x;
        int num = Mathf.CeilToInt(distance / sectionLength);
        while (num > 0)
        {
            // Count
            num--;
            offset += sectionLength;

            // Add new Section
            RopeSection newSection = Instantiate(sectionPrefab, new Vector3(ropeEnds.x + offset, transform.position.y, 0), Quaternion.identity) as RopeSection;
            newSection.ConnectTo(lastSection);

            // Add to Rope Container
            ropeContainer.AddSection(newSection);

            // Reset for next iteration
            lastSection = newSection;
        }

        return ropeContainer;
    }

    protected void SetRopeLayer(Rope rope)
    {
        rope.SetLayerRecursively(rope.gameObject, LayerMask.NameToLayer("Ropes"));
        //if (creatorPaddle.player == Utility.Player.Player1)
        //    rope.SetLayerRecursively(rope.gameObject, LayerMask.NameToLayer("Player1Ropes"));
        //else
        //    rope.SetLayerRecursively(rope.gameObject, LayerMask.NameToLayer("Player2Ropes"));
    }
}
