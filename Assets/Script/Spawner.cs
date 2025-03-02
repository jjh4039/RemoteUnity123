using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject checkPoint;
    public Vector3 SpawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
