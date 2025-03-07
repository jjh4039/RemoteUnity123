using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
 
    public object collider;

    void Start()
    {
        if(tag == "Traps")
        {
            collider = GetComponent<TilemapCollider2D>();
        }
        else if(tag == "Enemy")
        {
            collider = GetComponent<Collider2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SpawnManager.instance.Kill();
        }
    }
    void Update()
    {
        
    }
}
