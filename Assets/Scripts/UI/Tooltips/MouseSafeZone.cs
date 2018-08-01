using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSafeZone : MonoBehaviour {
    
	void Update () {
        transform.position = Input.mousePosition;
	}
}
