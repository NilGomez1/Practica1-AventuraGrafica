using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum Accion { Ninguna, Mirar, Usar, Coger }

    [SerializeField] private Button botonMirar;
    [SerializeField] private Button botonUsar;
    [SerializeField] private Button botonCoger;
    [SerializeField] private Color colorSeleccionado = Color.yellow;
    [SerializeField] private Color colorPorDefecto = Color.white;

    private Accion accionActual = Accion.Ninguna;

    // --- Control del inventario del cuchillo ---
    private bool tieneCuchillo = false;

    public bool TieneCuchillo()
    {
        return tieneCuchillo;
    }

    public void ObtenerCuchillo()
    {
        tieneCuchillo = true;
    }
    // ------------------------------------------

    public Accion GetAccionActual()
    {
        return accionActual;
    }

    public void UseButtonMirar() => SeleccionarAccion(Accion.Mirar, botonMirar);
    public void UseButtonUsar()  => SeleccionarAccion(Accion.Usar,  botonUsar);
    public void UseButtonCoger() => SeleccionarAccion(Accion.Coger, botonCoger);

    private void SeleccionarAccion(Accion accion, Button boton)
    {
        accionActual = (accionActual == accion) ? Accion.Ninguna : accion;
        ActualizarColorBotones();
    }

    private void ActualizarColorBotones()
    {
        botonMirar.GetComponentInChildren<TextMeshProUGUI>().color = (accionActual == Accion.Mirar) ? colorSeleccionado : colorPorDefecto;
        botonUsar.GetComponentInChildren<TextMeshProUGUI>().color = (accionActual == Accion.Usar) ? colorSeleccionado : colorPorDefecto;
        botonCoger.GetComponentInChildren<TextMeshProUGUI>().color = (accionActual == Accion.Coger) ? colorSeleccionado : colorPorDefecto;
     }
     
     public void ResetearBotones()
    {
        accionActual = Accion.Ninguna;
        ActualizarColorBotones();
    }
}