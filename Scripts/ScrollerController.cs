using UnityEngine;
using System.Collections;

public class ScrollerController : MonoBehaviour {

    public Transform cam;
    public float tileSizeY;
    public float parallaxFactor = 1.0f;
    // public float speed;
    // public float dampTime = 0.15f;

    private float newPositionY;
    private float prevCamPositionY;
    // private Vector3 velocity = Vector3.zero;
    
    // Use this for initialization
	void Start () {
        prevCamPositionY = cam.position.y;
	}
	
	// Update is called once per frame
	void Update () {

        ScrollerOne();

        /*
        if (transform.localPosition.y > destination.y)
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, destination,
                ref velocity, dampTime);
        else
            transform.localPosition = destination;
            */

        // transform.position = Vector2.MoveTowards(transform.position,
        //    moveTemp, speed * Time.deltaTime);
	
	}

    void ScrollerOne()
    {
        if (cam.position.y > 0)
            newPositionY = Mathf.Repeat(cam.position.y * parallaxFactor, tileSizeY);
        else if (cam.position.y < 0)
        {
            float tempPosition = -cam.position.y * parallaxFactor;
            newPositionY = Mathf.Repeat(tempPosition, tileSizeY);
            newPositionY *= -1f;
        }

        Vector3 destination = new Vector3(transform.localPosition.x, -newPositionY, transform.localPosition.z);

        transform.localPosition = destination;
    }

    void ScrollerTwo()
    {
        if (cam.position.y != prevCamPositionY)
        {
            float deltaCamY = cam.position.y - prevCamPositionY;
            float deltaScrollerY = deltaCamY * parallaxFactor;

            if (cam.position.y > 0)
                newPositionY = Mathf.Repeat(cam.position.y + deltaScrollerY, tileSizeY);
            else if (cam.position.y < 0)
            {
                float tempPosition = -cam.position.y + deltaScrollerY;
                newPositionY = Mathf.Repeat(tempPosition, tileSizeY);
                newPositionY *= -1f;
            }
        }

        transform.localPosition = new Vector3(transform.localPosition.x, -newPositionY, transform.position.z);
        prevCamPositionY = cam.position.y;

    }

    void ScrollerThree()
    {
        float deltaCamY = cam.position.y - prevCamPositionY;
        float deltaScrollerY = deltaCamY * parallaxFactor;

        if (cam.position.y > 0)
            newPositionY = Mathf.Repeat(cam.position.y + deltaScrollerY, tileSizeY);
        else if (cam.position.y < 0)
        {
            float tempPosition = -cam.position.y + deltaScrollerY;
            newPositionY = Mathf.Repeat(tempPosition, tileSizeY);
            newPositionY *= -1f;
        }

    }
}
