using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject Pipe;
    public float spawnRate;
    private float Timer = 0;
    private int max = 0;
    public float heightOffset = 10;
    public GameObject boss1;
    public GameObject boss2;
    public GameObject boss3;
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
            spawnRate *= 0.95f;
            pipeMoveScript.moveSpeed *= 1.05f;
            PipeMoveScript[] pipeMoveScripts = FindObjectsOfType<PipeMoveScript>();
            foreach (PipeMoveScript pipeMoveScript in pipeMoveScripts)
            {
                pipeMoveScript.moveSpeed = pipeMoveScript.moveSpeed * 1.05f;
            }
        }

        if (BossAlive == false && logic.playerScore == 9)
        {
            BossAlive = true;
            SpawnBoss1();
        }
        else if (BossAlive == false && logic.playerScore == 19)
        {
            BossAlive = true;
            SpawnBoss2();
        }
        else if (BossAlive == false && logic.playerScore == 29)
        {
            BossAlive = true;
            SpawnBoss3();
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

    void SpawnBoss1()
    {
        Instantiate(boss1, new Vector3(transform.position.x, transform.position.y, 0),
         Quaternion.identity);
    }

    void SpawnBoss2()
    {
        Instantiate(boss2, new Vector3(transform.position.x, transform.position.y, 0),
         Quaternion.identity);
    }

    void SpawnBoss3()
    {
        Instantiate(boss3, new Vector3(transform.position.x, transform.position.y, 0),
         Quaternion.identity);
    }
}
