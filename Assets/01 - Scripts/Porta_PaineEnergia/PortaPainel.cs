using Oculus.Interaction;
using UnityEngine;
using System;

public class PortaPainel : MonoBehaviour
{
    [SerializeField] private GameObject porta;
    [SerializeField] private GrabInteractable grabInteractable;
    [SerializeField] private OneGrabRotateTransformer oneGrabRotateTransformer;

    [Header("Configurações")]
    [SerializeField] private float anguloMinimo = 15f; // Ângulo mínimo para considerar "aberta"
    [SerializeField] private float velocidadeFechamento = 5f; // Velocidade de retorno à posição fechada

    private bool estaSegurando = false;
    private bool portaAberta = false;

    // Eventos (opcional, igual à alavanca)
    public event Action AoAbrir;
    public event Action AoFechar;

    private void Start()
    {
        if (grabInteractable == null)
            grabInteractable = GetComponent<GrabInteractable>();
        if (oneGrabRotateTransformer == null)
            oneGrabRotateTransformer = GetComponent<OneGrabRotateTransformer>();

        grabInteractable.WhenPointerEventRaised += TratarEvento;
    }

    void TratarEvento(PointerEvent evento)
    {
        if (evento.Type == PointerEventType.Select)
        {
            estaSegurando = true;
            oneGrabRotateTransformer.enabled = true;
        }
        else if (evento.Type == PointerEventType.Unselect)
        {
            estaSegurando = false;
            oneGrabRotateTransformer.enabled = false;
        }
    }

    private void Update()
    {
        if (!estaSegurando)
        {
            PosicionarPorta();
        }
    }

    private bool ChecarPorta()
    {
        float anguloAtual = porta.transform.localEulerAngles.y;

        // Normaliza o ângulo para -180 a 180
        if (anguloAtual > 180f)
            anguloAtual -= 360f;

        return Mathf.Abs(anguloAtual) > anguloMinimo;
    }

    private void PosicionarPorta()
    {
        bool estadoAnterior = portaAberta;
        bool portaEstaAberta = ChecarPorta();

        if (portaEstaAberta)
        {
            portaAberta = true;

            // Dispara evento apenas na mudança de estado
            if (!estadoAnterior)
            {
                AoAbrir?.Invoke();
                Debug.Log("Porta aberta!");
            }
        }
        else
        {
            // Fecha a porta suavemente
            Quaternion rotacaoAtual = porta.transform.localRotation;
            Quaternion rotacaoFechada = Quaternion.Euler(0f, 0f, 0f);

            porta.transform.localRotation = Quaternion.Lerp(
                rotacaoAtual,
                rotacaoFechada,
                Time.deltaTime * velocidadeFechamento
            );

            // Verifica se chegou na posição fechada
            if (Quaternion.Angle(rotacaoAtual, rotacaoFechada) < 1f)
            {
                porta.transform.localRotation = rotacaoFechada;

                if (estadoAnterior)
                {
                    portaAberta = false;
                    AoFechar?.Invoke();
                    Debug.Log("Porta fechada!");
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
            grabInteractable.WhenPointerEventRaised -= TratarEvento;
    }
}