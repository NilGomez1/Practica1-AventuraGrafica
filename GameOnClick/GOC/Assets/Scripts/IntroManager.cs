using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [Header("Referencias de UI")]
    [SerializeField] private Image imagenViñetaUI; // Arrastra aquí el componente Image de las viñetas
    [SerializeField] private Sprite[] listaViñetas; // Arrastra aquí tus 3 imágenes generadas

    private int indiceActual = 0;

    private void Start()
    {
        // Seguridad: Asegurar que al arrancar se muestra la primera viñeta si hay elementos
        ActualizarImagenVisual();
    }

    public void SiguienteViñeta()
    {
        indiceActual++;

        // Si aún quedan viñetas por mostrar
        if (indiceActual < listaViñetas.Length)
        {
            ActualizarImagenVisual();
        }
        else
        {
            // Si ya pasamos la última viñeta, cargamos la escena del juego (Salón/Cocina)
            SceneManager.LoadScene(1); // Índice 1 en Build Settings
        }
    }

    private void ActualizarImagenVisual()
    {
        if (imagenViñetaUI != null && listaViñetas != null && listaViñetas.Length > indiceActual)
        {
            if (listaViñetas[indiceActual] != null)
            {
                imagenViñetaUI.sprite = listaViñetas[indiceActual];
            }
            else
            {
                Debug.LogWarning("Falta asignar un Sprite en la posición " + indiceActual + " del IntroManager.");
            }
        }
        else
        {
            Debug.LogError("La referencia de 'Imagen Viñeta UI' está perdida o vacía en el Inspector.");
        }
    }
}