using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

    List<RopeSection> sections;

    private void Awake()
    {
        sections = new List<RopeSection>();
    }

    private void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(sections[0].transform.position);
        if (screenPoint.y < -500f || screenPoint.y > Screen.height + 500f)
        {
            Destroy(gameObject);
        }
    }

    public void AddSection(RopeSection rs)
    {
        sections.Add(rs);
        rs.transform.SetParent(transform);
    }

    public void Setup(Color col, Vector3 gravityDir, float grav)
    {
        foreach (RopeSection rs in sections)
        {
            rs.GetComponent<LineRenderer>().material.SetColor("_EmissionColor", col);
            rs.gravityDirection = gravityDir;
            rs.gravity = grav;
        }
    }

    public void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    private void OnDestroy()
    {
        // Destroy all rope sections
        for (int i = sections.Count - 1; i >= 0; i--)
        {
            Destroy(sections[i]);
        }
    }
}
