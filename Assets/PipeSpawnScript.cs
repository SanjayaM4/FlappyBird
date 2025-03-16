using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject Pipe;
    public float spawnRate;
    private float Timer = 0;
    private int max = 0;
    public float heightOffset = 10;
    public GameObject boss;
    public LogicScript logic;
    public PipeMoveScript pipeMoveScript;
    public bool BossAlive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BossAlive = false;
        pipeMoveScript.moveSpeed = 10;
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.playerScore > max)
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

        if (BossAlive == false && logic.playerScore == 9)
        {
            BossAlive = true;
            SpawnBoss();
        }

        if (BossAlive == false)
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
        
    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(Pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0),
         Quaternion.identity);

    }

    void SpawnBoss()
    {
        Instantiate(boss, new Vector3(transform.position.x, transform.position.y, 0),
         Quaternion.identity);
    }
}
