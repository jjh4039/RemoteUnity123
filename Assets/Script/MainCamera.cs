using UnityEngine;

public class MainCamera : MonoBehaviour
{
    void Start()
    {
        transform.position = GameManager.Instance.player.transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(GameManager.Instance.player.transform.position.x + 3f, GameManager.Instance.player.transform.position.y + 2f, -10);

        if (transform.position.x < 1.53)
        {
            transform.position = new Vector3(1.53f, GameManager.Instance.player.transform.position.y + 2f, -10);
        }
    }
}
