using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Start()
    {
        instance = this; // 안나오냐 ?
    }

    void Update()
    {
        
    }
}
