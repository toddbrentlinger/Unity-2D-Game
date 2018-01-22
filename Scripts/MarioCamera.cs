using UnityEngine;
using System.Collections;

public class MarioCamera : MonoBehaviour {

    public GameObject character = null;
    public float movementThreshold = 6f;

    public float dampTime = 0.15f;
    public float viewportCenterOffset = 0f;
    public bool limitUpwardsMovement = true;

    private Vector3 velocity = Vector3.zero;
    private Vector3 moveTemp;
    private float initialCamHeight;
    private float initialPlayerHeight;
    private float initialDelta;
    // private float yDifference;

    Camera mainCamera;

    void Start () {
        mainCamera = GetComponent<Camera>();
        initialCamHeight = transform.position.y;
        initialPlayerHeight = character.transform.position.y;
        initialDelta = initialCamHeight - initialPlayerHeight;
	}
    
	void LateUpdate () {
        if (character != null)
        {
            if (character.transform.position.y > initialPlayerHeight)
                if (!character.GetComponent<PlayerController>().GetIsDead())
                    AdvancedCamera();
                else
                {
                    if ((transform.position.y - character.transform.position.y) > initialDelta)
                        transform.position = new Vector3(transform.position.x,
                            character.transform.position.y + initialDelta, transform.position.z);
                }

            else
            {
                transform.position = new Vector3(transform.position.x, initialCamHeight, transform.position.z);
                // Vector3 destination = new Vector3(transform.position.x, initialCamHeight, transform.position.z);
                // transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
        }
    }

    void SimpleCamera()
    {
        if (character)
        {
            Vector3 point = mainCamera.WorldToViewportPoint(character.transform.position);

            Vector3 delta = character.transform.position - mainCamera.ViewportToWorldPoint(
                new Vector3(0.5f, 0.5f, point.z));

            Vector3 destination = new Vector3(0f, transform.position.y + delta.y, 
                transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position,
                    destination, ref velocity, dampTime);
        }
    }

    void AdvancedCamera()
    {
        if (character)
        {
            Vector3 point = mainCamera.WorldToViewportPoint(character.transform.position);

            float delta = character.transform.position.y - mainCamera.ViewportToWorldPoint(
                new Vector3(0.5f, 0.5f + viewportCenterOffset, point.z)).y;

            if (delta < 0 && limitUpwardsMovement)
                delta = 0;

            Vector3 destination = new Vector3(0f, transform.position.y + delta, 
                transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position,
                    destination, ref velocity, dampTime);
        }
    }
}
