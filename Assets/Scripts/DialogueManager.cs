using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private float letterDelay = 0.05f;

    [SerializeField] private GameManager gameManager;

    private Coroutine typingCoroutine;

    public void MostrarDialogo(string[] dialogos)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeDialogue(dialogos));
    }

    private IEnumerator TypeDialogue(string[] dialogos)
    {
        foreach (string dialogo in dialogos)
        {
            textMeshPro.text = ""; 
            foreach (char letter in dialogo)
            {
                textMeshPro.text += letter;
                yield return new WaitForSeconds(letterDelay);
            }
        }

        if (gameManager != null)
        {
            gameManager.ResetearBotones();
        }
    }
}