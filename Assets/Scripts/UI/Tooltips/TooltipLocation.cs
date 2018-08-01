using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipLocation : MonoBehaviour {
    
    Vector3 location;
    public Vector3 finalLocation;
    Vector3[] mouseSafeZoneCorners = new Vector3[4];
    public RectTransform mouseSafeZone;
    public RectTransform tooltipBox;
    public bool invertedWidth = false;
    public bool invertedHeight = false;
    private float xLocation;
    private float yLocation;
    public float yPivot;
    public float xPivot;

    void Start()
    {
        tooltipBox = gameObject.GetComponent<RectTransform>();
        mouseSafeZone = GameObject.Find("MouseSafeZone").GetComponent<RectTransform>();
    }
	void Update () {
        SetLocation();
	}

    public void SetLocation()
    {
        mouseSafeZone.GetWorldCorners(mouseSafeZoneCorners);
        //Set correct pivots
        if (invertedHeight)
        {
            yPivot = 0;
            yLocation = mouseSafeZoneCorners[0].y;
        } else
        {
            yPivot = 1;
            yLocation = mouseSafeZoneCorners[3].y;
        }
        if (invertedWidth)
        {
            xPivot = 1;
            xLocation = mouseSafeZoneCorners[0].x;
        }
        else
        {
            xPivot = 0;
            xLocation = mouseSafeZoneCorners[3].x;
        }
        tooltipBox.pivot = new Vector2(xPivot, yPivot);

        //Actually move the tooltip
        location = new Vector3(xLocation, yLocation);
        tooltipBox.transform.position = location;
    }
}
