using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private float R_Max = 10.0f;
    private float L_Max = -10.0f;
    private float currentPos;
    private float direction = 1.0f;
    private float speed = 2.0f;
    private float change = 0;

    void Start()
    {
        currentPos = transform.position.x;
    }


    void Update()
    {
        Move();
    }
    void Move()
    {
        float tmp = speed * direction * Time.deltaTime;
        change += tmp;
        currentPos += tmp;
        transform.position = new Vector2(currentPos, transform.position.y);
        if (change >= R_Max)
        {
            direction = -1.0f;
            change = 0;
        }
        else if (change <= L_Max)
        {
            direction = 1.0f;
            change = 0;
        }
    }
    public void attacked()
    {
        Destroy(gameObject);
    }


}
