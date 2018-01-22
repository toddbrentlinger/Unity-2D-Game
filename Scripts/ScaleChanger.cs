using UnityEngine;
using System.Collections;

public class ScaleChanger : MonoBehaviour {

    public float maxHeight;
    public float finalScale;

    private Vector3 initialScale;
    private float scale;
    private float initialPosition;
    
    // Use this for initialization
	void Start () {

        initialScale = transform.localScale;
        scale = initialScale.x;
        initialPosition = transform.position.y;
	
	}
	
	// Update is called once per frame
	void Update () {

        float delta = transform.position.y - initialPosition;
        float ratio = Mathf.InverseLerp(initialPosition, maxHeight, delta);
        // float scale = Mathf.Lerp(initialScale.x, finalScale, ratio);
        if (ratio > 0 && ratio < 1)
            scale = Mathf.LerpUnclamped(initialScale.x, finalScale, ratio);

        transform.localScale = new Vector3(scale, initialScale.y, initialScale.z);
        Debug.Log("Scale: " + transform.localScale.x + " Ratio: " + ratio 
            + " Delta: " + delta + " transform.position.y: " + transform.position.y);
	
	}
}
