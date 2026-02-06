using UnityEngine;
using Oculus.Interaction;
using UnityEngine.Events;

public class Alavanca : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject alavanca;
    [SerializeField] private GrabInteractable agarravel;
    [SerializeField] private OneGrabRotateTransformer transformadorRotacao;

    [Header("Eventos")]
    public UnityEvent aoLigar;
    public UnityEvent aoDesligar;

    private bool estaSegurando = false;
    private bool isOn = false;

    void Start()
    {
        if (agarravel == null)
            agarravel = GetComponent<GrabInteractable>();

        if (transformadorRotacao == null)
            transformadorRotacao = GetComponent<OneGrabRotateTransformer>();

        agarravel.WhenPointerEventRaised += TratarEvento;
    }

    void TratarEvento(PointerEvent evento)
    {
        if (evento.Type == PointerEventType.Select)
        {
            estaSegurando = true;
            transformadorRotacao.enabled = true;
        }
        else if (evento.Type == PointerEventType.Unselect)
        {
            estaSegurando = false;
            transformadorRotacao.enabled = false;
        }
    }

    void Update()
    {
        if (!estaSegurando)
        {
            PosicionarAlavanca();
        }

        Debug.Log("Alavanca isOn: " + isOn);
    }

    private bool ChecarAlavanca()
    {
        float anguloAtual = alavanca.transform.localEulerAngles.x;
        if (anguloAtual > 180f) anguloAtual -= 360f;

        if (anguloAtual < -40f)
            return true;
        else
            return false;
    }

    private void PosicionarAlavanca()
    {
        bool estadoAnterior = isOn;

        if (ChecarAlavanca())
        {
            alavanca.transform.localRotation = Quaternion.Euler(-80f, 0f, 0f);
            isOn = true;
        }
        else
        {
            alavanca.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            isOn = false;
        }

        // Detecta mudança de estado e chama os eventos
        if (isOn != estadoAnterior)
        {
            if (isOn)
                aoLigar?.Invoke();
            else
                aoDesligar?.Invoke();
        }
    }

    void OnDestroy()
    {
        if (agarravel != null)
            agarravel.WhenPointerEventRaised -= TratarEvento;
    }
}