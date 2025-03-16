using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D MyRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool isAlive = true;
    public AudioClip flapAudio;
    public AudioClip dieAudio;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            MyRigidbody.linearVelocity = Vector2.up * flapStrength;
            AudioSource.PlayClipAtPoint(flapAudio, transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isAlive = false;
        gameObject.layer = 0;
        logic.GameOver();
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
