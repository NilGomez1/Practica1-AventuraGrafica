using UnityEngine;
using UnityEngine.EventSystems;

public class ObjetoInteractuable : MonoBehaviour, IPointerClickHandler
{
    [Header("Referencias")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DialogueManager dialogueManager;

    [Header("Diálogos según acción")]
    [TextArea(2, 3)] [SerializeField] private string[] dialogoMirar;
    [TextArea(2, 3)] [SerializeField] private string[] dialogoUsar;
    [TextArea(2, 3)] [SerializeField] private string[] dialogoCoger;

    [Header("Animación opcional")]
    [SerializeField] private Animator anim;
    [SerializeField] private string boolAnimacion = "isPlotting";

    [Header("Viaje entre habitaciones (Opcional)")]
    [SerializeField] private GameObject habitacionActivar;
    [SerializeField] private GameObject habitacionDesactivar;
    [SerializeField] private Transform puntoAparicionNuevaSala; // Nueva variable para la posición de entrada

    [Header("Inventario (Opcional)")]
    [SerializeField] private GameObject iconoInventario;

    [SerializeField] private bool desactivarAlCoger = true;

    [Header("Movimiento del protagonista")]
    [SerializeField] private ControladorSenor controladorSenor;
    [SerializeField] private Transform puntoInteraccion;

    [Header("Escala en la nueva sala (Opcional)")]
    [SerializeField] private Vector3 nuevaEscalaSenor = Vector3.one;

    [Header("Lógica de la Caja Final (Opcional)")]
    [SerializeField] private bool esCajaMesa = false;
    [TextArea(2, 3)] [SerializeField] private string[] dialogoCajaSinCuchillo;
    [SerializeField] private GameObject cinematicaFinal; // Objeto (video o panel) que reproduce el final
    
    private bool haSidoUsado = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (gameManager == null || gameManager.GetAccionActual() == GameManager.Accion.Ninguna) return;

        // Si hay un punto asignado, primero camina y luego ejecuta la lógica
        if (controladorSenor != null && puntoInteraccion != null)
        {
            controladorSenor.MoverHacia(puntoInteraccion.position, EjecutarAccion);
        }
        else
        {
            EjecutarAccion();
        }
    }

    private void EjecutarAccion()
    {
        switch (gameManager.GetAccionActual())
        {
            case GameManager.Accion.Mirar:
                if (dialogoMirar != null && dialogoMirar.Length > 0 && dialogueManager != null)
                    dialogueManager.MostrarDialogo(dialogoMirar);
                break;

            case GameManager.Accion.Usar:
                // Si interactuamos con la caja
                if (esCajaMesa && gameManager != null)
                {
                    // Comprobamos si el jugador tiene el cuchillo
                    if (!gameManager.TieneCuchillo())
                    {
                        if (dialogoCajaSinCuchillo != null && dialogoCajaSinCuchillo.Length > 0 && dialogueManager != null)
                            dialogueManager.MostrarDialogo(dialogoCajaSinCuchillo);
                    }
                    else
                    {
                        // Ya tiene el cuchillo: arrancamos la cinemática
                        if (cinematicaFinal != null)
                        {
                            cinematicaFinal.SetActive(true);
                        }
                        else
                        {
                            Debug.LogError("¡Falta asignar el objeto CinematicaFinal en el Inspector de la Caja!");
                        }
                    }
                    return; // Terminamos aquí para no ejecutar la lógica normal
                }

                // Lógica de Usar para el resto de objetos de la casa
                if (anim != null && !haSidoUsado)
                {
                    haSidoUsado = true;
                    anim.SetBool(boolAnimacion, true);
                }
                if (dialogoUsar != null && dialogoUsar.Length > 0 && dialogueManager != null)
                    dialogueManager.MostrarDialogo(dialogoUsar);

                if (habitacionActivar != null && habitacionDesactivar != null)
                {
                    habitacionActivar.SetActive(true);
                    habitacionDesactivar.SetActive(false);

                    if (controladorSenor != null && puntoAparicionNuevaSala != null)
                    {
                        Vector3 nuevaPos = puntoAparicionNuevaSala.position;
                        nuevaPos.z = controladorSenor.transform.position.z;
                        controladorSenor.transform.position = nuevaPos;

                        if (nuevaEscalaSenor != Vector3.zero)
                        {
                            controladorSenor.transform.localScale = nuevaEscalaSenor;
                        }
                    }
                }
                break;

            case GameManager.Accion.Coger:
                // 1. BLOQUEO PARA LA CAJA: No se puede coger ni desactivar
                if (esCajaMesa)
                {
                    if (dialogoCoger != null && dialogoCoger.Length > 0 && dialogueManager != null)
                        dialogueManager.MostrarDialogo(dialogoCoger);
                    return; // Sale de inmediato para que la caja no desaparezca
                }

                // 2. Lógica para objetos que sí se pueden coger (ej. el Cuchillo)
                if (iconoInventario != null)
                {
                    iconoInventario.SetActive(true);
                    if (gameManager != null) gameManager.ObtenerCuchillo();
                }

                if (dialogoCoger != null && dialogoCoger.Length > 0 && dialogueManager != null)
                    dialogueManager.MostrarDialogo(dialogoCoger);

                if (desactivarAlCoger)
                {
                    SpriteRenderer sr = GetComponent<SpriteRenderer>();
                    if (sr != null) sr.enabled = false;
                    GetComponent<Collider2D>().enabled = false;
                }
                break;
        }
    }
}