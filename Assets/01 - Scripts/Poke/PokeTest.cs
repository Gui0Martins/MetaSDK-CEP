using UnityEngine;
using UnityEngine.Events;
using Oculus.Interaction;

public class PokeTest : MonoBehaviour
{
    [SerializeField] private PokeInteractable pokeInteractable;

    [Header("Eventos Configuráveis")]
    public UnityEvent onPokePressed;
    public UnityEvent onPokeReleased;
    public UnityEvent onPokeHover;
    public UnityEvent onPokeUnhover;

    private void Start()
    {
        if (pokeInteractable == null)
        {
            pokeInteractable = GetComponent<PokeInteractable>();
        }

        if (pokeInteractable != null)
        {
            pokeInteractable.WhenSelectingInteractorViewAdded += OnPokePressed;
            pokeInteractable.WhenSelectingInteractorViewRemoved += OnPokeReleased;
            pokeInteractable.WhenInteractorViewAdded += OnPokeHover;
            pokeInteractable.WhenInteractorViewRemoved += OnPokeUnhover;
        }
    }

    private void OnPokePressed(IInteractorView interactor)
    {
        Debug.Log("Botão pressionado!");
        onPokePressed?.Invoke();
    }

    private void OnPokeReleased(IInteractorView interactor)
    {
        Debug.Log("Botão solto!");
        onPokeReleased?.Invoke();
    }

    private void OnPokeHover(IInteractorView interactor)
    {
        Debug.Log("Hover começou!");
        onPokeHover?.Invoke();
    }

    private void OnPokeUnhover(IInteractorView interactor)
    {
        Debug.Log("Hover terminou!");
        onPokeUnhover?.Invoke();
    }

    private void OnDestroy()
    {
        if (pokeInteractable != null)
        {
            pokeInteractable.WhenSelectingInteractorViewAdded -= OnPokePressed;
            pokeInteractable.WhenSelectingInteractorViewRemoved -= OnPokeReleased;
            pokeInteractable.WhenInteractorViewAdded -= OnPokeHover;
            pokeInteractable.WhenInteractorViewRemoved -= OnPokeUnhover;
        }
    }
}