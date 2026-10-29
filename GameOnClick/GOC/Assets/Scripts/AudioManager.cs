using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instancia;
    private AudioSource audioSource;

    [Header("Pistas de Música")]
    [SerializeField] private AudioClip musicaMenu;
    [SerializeField] private AudioClip musicaJuego;

    private void Awake()
    {
        // Patrón Singleton para que no se duplique al cambiar de escena
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Este método se ejecuta automáticamente cada vez que cargas una escena nueva
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (audioSource == null) return;

        // Si la escena cargada es el Menú Principal (índice 0)
        if (scene.buildIndex == 0)
        {
            if (musicaMenu != null && audioSource.clip != musicaMenu)
            {
                audioSource.clip = musicaMenu;
                audioSource.Play();
            }
        }
        // Si la escena cargada es el Juego / Salón (índice 1 o superior)
        else
        {
            if (musicaJuego != null && audioSource.clip != musicaJuego)
            {
                audioSource.clip = musicaJuego;
                audioSource.Play();
            }
        }
    }
}