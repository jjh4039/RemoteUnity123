using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    public FruitManager fruitManager;
    public Player player;
    public IntroduceTextManager introduceTextManager;
    public Bar bar;

    public int deathCount;

    void Awake()
    {
        deathCount = 0;
        Instance = this;
    }
}
