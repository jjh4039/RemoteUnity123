using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    public FruitManager fruitManager;
    public Player player;
 
    void Awake()
    {
        Instance = this;
    }
}
