using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject Pipe;
    public float spawnRate;
    private float Timer = 0;
    private int max = 0;
    public float heightOffset = 10;

    public LogicScript logic;
    public PipeMoveScript pipeMoveScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pipeMoveScript.moveSpeed = 10;
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.playerScore % 5 == 0 && logic.playerScore > max)
        {
            max = logic.playerScore;
            spawnRate *= 0.9f;
            pipeMoveScript.moveSpeed *= 1.1f;
            PipeMoveScript[] pipeMoveScripts = FindObjectsOfType<PipeMoveScript>();
            foreach (PipeMoveScript pipeMoveScript in pipeMoveScripts)
            {
                pipeMoveScript.moveSpeed = pipeMoveScript.moveSpeed * 1.1f;
            }
        }

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
