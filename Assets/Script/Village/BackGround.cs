using UnityEngine;

public class BackGround : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float offset;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.SetColor("_Color", new Color(0.61f, 0f, 0.75f, 1f));
        meshRenderer.sharedMaterial.SetColor("_Color", Color.red);
    }

    void FixedUpdate()
    {
        {
            if ((Input.GetKey(KeyCode.RightArrow)))
            {
                meshRenderer.material.mainTextureOffset += new Vector2(0.0005f, 0);
            }
            if ((Input.GetKey(KeyCode.LeftArrow)))
            {
                meshRenderer.material.mainTextureOffset -= new Vector2(0.0005f, 0);
            }
        }
    }
}
