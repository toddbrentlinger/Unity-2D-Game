using UnityEngine;
using System.Collections;

public class BGScrollerAdvanced : MonoBehaviour {

    public float scrollSpeedX;
    public float tileSizeX;
    public float scrollSpeedY;
    public float tileSizeY;

    private Vector3 startPosition;
    private float newPositionX;
    private float newPositionY;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (scrollSpeedX != 0 && tileSizeX != 0)
        {
            newPositionX = Mathf.Repeat(Time.time * scrollSpeedX, tileSizeX);
        }

        if (scrollSpeedY != 0 && tileSizeY != 0)
        {
            newPositionY = Mathf.Repeat(Time.time * scrollSpeedY, tileSizeY);
        }

        Vector3 translateVec = Vector3.down * newPositionY + Vector3.left * newPositionX;
        transform.localPosition = startPosition + translateVec;

    }
}
