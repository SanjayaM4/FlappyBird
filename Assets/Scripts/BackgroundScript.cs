using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    public float moveDuration = 2f;
    private float elapsedTimeLeft = 0f;
    private float elapsedTimeRight = 0f;
    public PipeSpawnScript pipeSpawnScript;
    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pipeSpawnScript.BossAlive == false)
        {
            if (elapsedTimeLeft < moveDuration)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;

                elapsedTimeLeft += Time.deltaTime;
                elapsedTimeRight = 0;
            }
        }
        else
        {
            if (elapsedTimeRight < moveDuration)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;

                elapsedTimeRight += Time.deltaTime;
                elapsedTimeLeft = 0;
            }
        }
        

    }
}
