using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotation : MonoBehaviour {

    Vector3 rotationVector;
	
	void Start () {
        rotationVector = new Vector3(0f, 0.02f, 0f);
	}
	

	void Update () {
        transform.Rotate(rotationVector);
	}
}
