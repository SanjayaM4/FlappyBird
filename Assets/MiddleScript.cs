using UnityEngine;

public class MiddleScript : MonoBehaviour
{
    public LogicScript logic;
    public AudioClip audioClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            logic.AddScore();
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
        }
    }
}
