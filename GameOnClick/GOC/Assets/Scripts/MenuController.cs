using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Panel de Introducción / Cinemática")]
    [SerializeField] private GameObject panelIntroduccion; // El objeto que contiene las imágenes o vídeo de intro

    // Este método se llamará al hacer clic en el botón "Jugar"
    public void BotonJugarPresionado()
    {
        if (panelIntroduccion != null)
        {
            // Si tienes cinemática en imágenes/vídeo, activamos el panel antes de cambiar de escena
            panelIntroduccion.SetActive(true);
        }
        else
        {
            // Si no hay intro, carga directamente la escena del juego (posición 1)
            SceneManager.LoadScene(1);
        }
    }

    // Método para pasar a la escena del juego después de ver la cinemática
    public void CargarEscenaJuego()
    {
        SceneManager.LoadScene(1); // Carga la escena del salón (índice 1 en Build Settings)
    }

    public void BotonSalirPresionado()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}