using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public TilemapCollider2D collider;

    void Start()
    {
        collider = GetComponent<TilemapCollider2D>();
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
