
using UnityEngine;

public class WindyEnvironment : MonoBehaviour {

    public float rotationSpeed=100f;
    public float rotationAmount = 100f;
	void Update () {
        foreach (Transform child in transform)
        {
            child.Rotate(new Vector3((Mathf.Sin(Time.time * rotationSpeed-Mathf.PI/2))/rotationAmount, 0f, 0f));
        }
       /* if ( Time.time%rotationAmount < rotationAmount/2)
		    foreach (Transform child in transform)
            {
                child.Rotate(new Vector3(Time.deltaTime*rotationSpeed, 0f ,0f));
            }
        else
            foreach (Transform child in transform)
            {
                child.Rotate(new Vector3(-Time.deltaTime * rotationSpeed, 0f, 0f));
            }*/
    }
}
