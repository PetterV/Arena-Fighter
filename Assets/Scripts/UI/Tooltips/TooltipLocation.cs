using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipLocation : MonoBehaviour {
    
    Vector3 location;
    Vector3 finalLocation = new Vector3 (-30, 30);
    public Vector3 offset;

	void Update () {
        location = Input.mousePosition;
        finalLocation = location - offset;
        transform.position = finalLocation;
	}
}
