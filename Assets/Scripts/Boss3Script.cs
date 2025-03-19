using UnityEngine;

public class Boss3Script : MonoBehaviour
{
     public int health = 3;
    public GameObject bullet;
    public GameObject ally;
    public float shootSpreadRate = 0.875f;
    public float shootStraightRate = 0.875f;
    private float SpreadTimer = 0;
    private float StraightTimer = 0;
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
    public AudioClip shootSound;
    public AudioClip victorySound;


     public float spreadAngle = 90f;
    public int bulletCount = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAlive = true;
        startPos = new Vector3(25, 0, 0);
        pipeSpawner = GameObject.FindGameObjectWithTag("Spawner");
        pipeSpawnScript = pipeSpawner.GetComponent<PipeSpawnScript>();

        int bulletLayer = LayerMask.NameToLayer("Bullets");
        Physics2D.IgnoreLayerCollision(bulletLayer, bulletLayer, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingToPlace)
        {
            float moveAmount = moveSpeed * 5 * Time.deltaTime;
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
            if (SpreadTimer < shootSpreadRate)
            {
                SpreadTimer += Time.deltaTime;
            }
            else
            {
                ShootSpread();
                AudioSource.PlayClipAtPoint(shootSound, transform.position);
                numShots++;
                SpreadTimer = 0;

                if (numShots >= 2)
                {
                    SpawnAlly();
                    numShots = 0;
                }
            }

            if (StraightTimer < shootStraightRate)
            {
                StraightTimer += Time.deltaTime;
            }
            else
            {
                ShootStraight();
                AudioSource.PlayClipAtPoint(shootSound, transform.position);
                StraightTimer = 0;
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
            AudioSource.PlayClipAtPoint(victorySound, transform.position);
        }
    }

    public void ShootSpread()
    {
        float angleStep = spreadAngle / (bulletCount - 1);
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, currentAngle + 90);

            GameObject bulletInstance = Instantiate(bullet, transform.position, bulletRotation);
        }
    }

    public void ShootStraight()
    {
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90));
    }

    public void SpawnAlly()
    {
        GameObject allyInstance = Instantiate(ally, transform.position, Quaternion.identity);
    }
}
