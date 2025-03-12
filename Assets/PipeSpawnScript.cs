using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject Pipe;
    public float spawnRate = 2;
    private float Timer = 0;
    public float heightOffset = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer < spawnRate)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            SpawnPipe();
            Timer = 0;
        }
    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(Pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0),
         Quaternion.identity);
    }
}
