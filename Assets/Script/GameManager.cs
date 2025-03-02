using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
