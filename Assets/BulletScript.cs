using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float deadZone = -45;
    private SpriteRenderer spriteRenderer;
    public Sprite explosionSprite;
    public float moveSpeed = 10;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Static;
        spriteRenderer.sprite = explosionSprite;
        Destroy(gameObject, 0.1f);
    }
}
