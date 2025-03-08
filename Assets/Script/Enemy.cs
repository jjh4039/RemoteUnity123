using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
 
    public object collider;
    bool[] tags = new bool[2];

    void Start()
    {
        if(tag == "Traps")
        {
            collider = GetComponent<TilemapCollider2D>();
            tags[0] = true;
        }
        else if(tag == "Enemy")
        {
            collider = GetComponent<Collider2D>();
            tags[1] = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (tags[0] == true)
            {
                SpawnManager.instance.Kill();
            }
            else if (tags[1] == true)
            {
                //SpawnManager.instance.Kill();
            }
            
        }
    }
    void Update()
    {
        
    }
}
