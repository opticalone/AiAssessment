using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiSpin : MonoBehaviour {


    public float spinSpeed= 0f;

    float input;
    void Update()
    {
        input = Input.GetAxis("Vertical") *spinSpeed;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
       
        
            transform.Rotate(new Vector3(0f, input, 0f));
        
        
	}
}
