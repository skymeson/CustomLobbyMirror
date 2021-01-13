using UnityEngine;
using System.Collections;


public class BreathexyzRot : MonoBehaviour {
    Vector3 startPos;

//	protected void Start() {
//		startPos = transform.position;
//	}
//	
//	protected void Update() {
//		float distance = Mathf.Sin(Time.timeSinceLevelLoad);
//		transform.position = startPos + Vector3.up * distance;
//	}

    public float amplitude = 10f;
    public float period = 5f;

    protected void Start() {
        startPos = transform.localPosition;
    }

    protected void Update() {

	//	float distance = Mathf.Sin(Time.timeSinceLevelLoad);
		//		transform.position = startPos + Vector3.up * distance;
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
//       // transform.position = startPos + Vector3.up * distance;
//		//transform.localPosition = startPos + Vector3.left * distance;
		transform.rotation = Quaternion.Euler(startPos + Vector3.up * distance);
//transform.localScale = Vector3.forward/distance/transform.localScale.x;


    }
}