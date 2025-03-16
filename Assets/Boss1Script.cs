using UnityEngine;

public class Boss1Script : MonoBehaviour
{
    public int health = 3;
    public GameObject bullet;
    public GameObject ally;
    public float shootRate = 1;
    private float Timer = 0;
    public bool isAlive;
    public float moveSpeed = 2f;
    public float height = 10f;
    private bool isMovingToPlace = true;
    public float leftMoveDistance = 15f;
    private float movedDistance = 0f;
    public Vector3 startPos;
    private float timeOffset;
    private float sinTime;
    private int numShots = 0;
    private GameObject pipeSpawner;
    private PipeSpawnScript pipeSpawnScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAlive = true;
        startPos = new Vector3(25, 0, 0);
        pipeSpawner = GameObject.FindGameObjectWithTag("Spawner");
        pipeSpawnScript = pipeSpawner.GetComponent<PipeSpawnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingToPlace)
        {
            float moveAmount = moveSpeed*5 * Time.deltaTime;
            transform.position += new Vector3(-moveAmount, 0, 0);

            movedDistance += moveAmount;

            if (movedDistance >= leftMoveDistance)
            {
                isMovingToPlace = false;
                timeOffset = Time.time;
            }
        }
        else
        {
            sinTime = Time.time - timeOffset;
            float newY = Mathf.Sin(sinTime * moveSpeed) * height;
            transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
        }
        
        if (isMovingToPlace == false)
        {
            if (Timer < shootRate)
            {
                Timer += Time.deltaTime;
            }
            else
            {
                Shoot();
                numShots++;
                Timer = 0;

                if (numShots >= 5)
                {
                    SpawnAlly();
                    numShots = 0;
                }
            }
        }
    }

    public void BossHit()
    {
        health--;
        if (health <= 0)
        {
            isAlive = false;
            pipeSpawnScript.BossAlive = false;
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90));
    }

    public void SpawnAlly()
    {
        GameObject allyInstance = Instantiate(ally, transform.position, Quaternion.identity);
    }
}
