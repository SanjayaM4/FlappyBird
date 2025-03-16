using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    public float moveDistance = 2f;

    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        transform.position = new Vector3(startPosition.x + newX, startPosition.y, startPosition.z);
    }
}
