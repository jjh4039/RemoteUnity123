using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool checkPointReached = false;
    public BoxCollider2D Colider;
    public Vector3 SpawnPoint;
    public GameObject Check;
    public Spawner spawner;

    void Start()
    {
        Colider = GetComponent<BoxCollider2D>();
        spawner = GetComponent<Spawner>();
        Check = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkPointReached = true;
            spawner.SpawnPoint = transform.position;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkPointReached = false;
        }
    }
}

