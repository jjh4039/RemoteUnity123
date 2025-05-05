using UnityEngine;

public class Box : MonoBehaviour
{
    public Collider2D colider;
    private float posY;
    void Start()
    {
        colider = GetComponent<Collider2D>();
        posY = transform.position.y;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y < posY + 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        
    }
}
