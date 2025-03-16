using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class AllyScript : MonoBehaviour
{
    public float deadZone = -45;
    private SpriteRenderer spriteRenderer;
    public Sprite explosionSprite;
    public Sprite bulletSprite;
    public float moveSpeed = 10;
    private Rigidbody2D rb;
    private GameObject boss;
    private Boss1Script boss1Script;
    private Vector3 bossLocation;
    public AudioClip audioClip;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        boss1Script = boss.GetComponent<Boss1Script>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);

            StartCoroutine(ChangeSprites());

            boss1Script.BossHit();
            Destroy(gameObject, 0.5f);
            
        }
    }

    IEnumerator ChangeSprites()
    {

        spriteRenderer.sprite = bulletSprite;
        
        Vector3 bossLoc = boss.transform.position;

        Vector3 direction = bossLoc - transform.position;
        float distance = direction.magnitude;
        transform.position = (transform.position + bossLoc) / 2;
        transform.localScale = new Vector3(distance * 2.5f, transform.localScale.y * 0.5f, transform.localScale.z);
        transform.up = direction.normalized;
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);



        yield return new WaitForSeconds(0.1f);

        transform.position = bossLoc;
        transform.localScale = new Vector3(3, 3, 1);
        spriteRenderer.sprite = explosionSprite;
    }
}
