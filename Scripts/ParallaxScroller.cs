using UnityEngine;
using System.Collections;

public class ParallaxScroller : MonoBehaviour {

    public Transform[] backgrounds; // array of all backgrounds, and foregrounds, to be parallaxed
    public float[] tileSizesY;
    public float[] parallaxScales;  // proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;    // how smooth the parallax is going to be. Must be above 0 otherwise parallax will NOT work

    private Transform cam;  // reference to the camera's transform
    private Vector3 previousCamPos; // position of the camera in the previous frame

    // called before Start(), used to assign references
    void Awake() {
        // assign main camera to reference
        cam = Camera.main.transform;

    }

	// Use this for initialization
	void Start () {
        // store previous frame
        previousCamPos = cam.position;

	}
	
	// Update is called once per frame
	void Update () {
        // for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // parallax is the opposite of the camera movement because the previous frame is multiplied by the scale
            float parallax = (previousCamPos.y - cam.position.y) * parallaxScales[i];

            // set targetY position that is the current position plus the parallax
            float backgroundTargetPosY = backgrounds[i].localPosition.y + parallax;

            backgroundTargetPosY = Mathf.Repeat(backgroundTargetPosY, tileSizesY[i]);

            // create targetPosition which is the backgrounds current position with it's targetY position
            Vector3 backgroundTargetPos = new Vector3(backgrounds[i].position.x,
                backgroundTargetPosY, backgrounds[i].position.z);



            // fade between current position and target position using Lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position,
                backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // set up previousCamPos to the camera's position at end of the frame
        previousCamPos = cam.position;
	
	}
}
