using UnityEngine;

public class CollorChange : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Transform initialPosition;

    private void ChangeColor(Color newColor)
    {
        if (targetObject != null)
        {
            Renderer renderer = targetObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = newColor;
            }
        }
    }
    public void Red()
    {
        ChangeColor(Color.red);
    }

    public void Green()
    {
        ChangeColor(Color.green);
    }

    public void Yellow()
    {
        ChangeColor(Color.yellow);
    }

    public void Black()
    {
        ChangeColor(Color.black);
    }

    public void Blue()
    {
        ChangeColor(Color.blue);
    }

    public void ResetPositon()
    {
        if (targetObject != null && initialPosition != null)
        {
            targetObject.transform.position = initialPosition.position;
        }
    }
}
