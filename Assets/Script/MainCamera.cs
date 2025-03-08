using UnityEngine;

public class MainCamera : MonoBehaviour
{
    void Start()
    {
        transform.position = GameManager.Instance.player.transform.position;
    }

    void Update()
    {
       // if(!SpawnManager.instance.isDead)

        transform.position = new Vector3(GameManager.Instance.player.transform.position.x + 3f, 0.265f, -10);

        if (transform.position.x < 1.53)
        {
            transform.position = new Vector3(1.53f, 0.265f, -10);
        }
    }
}
