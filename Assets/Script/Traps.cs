using UnityEngine;
using UnityEngine.Tilemaps;

public class Traps : MonoBehaviour
{
 
    public TilemapCollider2D col;

    void Start()
    {
        col = GetComponent<TilemapCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SpawnManager.instance.Kill();
        }
    }
}
