using UnityEngine;
using UnityEngine.UI;

public class SpawnObject : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private GameObject objetoPrefab;
    [SerializeField] private Button botaoSpawn;

    [Header("Modo de Spawn")]
    [SerializeField] private bool usarPosicaoFixa = false;

    [Header("Spawn Relativo ao Canvas")]
    [SerializeField] private Vector3 offsetDoCanvas = new Vector3(0.5f, 0, 0); // 0.5m ao lado

    [Header("Spawn em Posição Fixa")]
    [SerializeField] private Transform pontoSpawnFixo;

    void Start()
    {
        if (botaoSpawn != null)
            botaoSpawn.onClick.AddListener(Spawnar);
    }

    public void Spawnar()
    {
        if (objetoPrefab == null)
        {
            Debug.LogWarning("Prefab não atribuído!");
            return;
        }

        Vector3 posicaoSpawn;
        Quaternion rotacaoSpawn;

        if (usarPosicaoFixa && pontoSpawnFixo != null)
        {
            // Modo 1: Posição fixa
            posicaoSpawn = pontoSpawnFixo.position;
            rotacaoSpawn = pontoSpawnFixo.rotation;
        }
        else
        {
            // Modo 2: Relativo ao Canvas
            posicaoSpawn = transform.position + transform.TransformDirection(offsetDoCanvas);
            rotacaoSpawn = transform.rotation;
        }

        Instantiate(objetoPrefab, posicaoSpawn, rotacaoSpawn);

        Debug.Log($"Objeto spawnado em: {posicaoSpawn}");
    }

    void OnDestroy()
    {
        if (botaoSpawn != null)
            botaoSpawn.onClick.RemoveListener(Spawnar);
    }
}