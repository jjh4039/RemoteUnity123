using UnityEngine;

public class Box : MonoBehaviour
{
    public Collider2D colider;
    public GameObject fruit;
    private float posY;
    public bool isNomal = false;
    private bool isBreakable = false;
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
                if (isNomal == false )
                {
                    if(isBreakable == false)
                    {
                        Instantiate(fruit, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        isBreakable = true;
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void Update()
    {
        
    }
}
