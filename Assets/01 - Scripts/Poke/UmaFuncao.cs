using UnityEngine;

public class UmaFuncao : MonoBehaviour
{
    [SerializeField] GameObject objeto;
    [SerializeField] Color newColor;

    public void MudarCor()
    {
        if (objeto == null) return;

        MeshRenderer renderer = objeto.GetComponent<MeshRenderer>();
        renderer.material.color = newColor;
    }
}
