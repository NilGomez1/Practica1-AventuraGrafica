using System;
using System.Collections;
using UnityEngine;

public class ControladorSenor : MonoBehaviour
{
    [SerializeField] private float velocidad = 2.5f;

    private Animator anim;
    private SpriteRenderer sr;
    private Coroutine corrutinaMovimiento;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void MoverHacia(Vector2 destino, Action alLlegar)
    {
        if (corrutinaMovimiento != null)
        {
            StopCoroutine(corrutinaMovimiento);
        }
        corrutinaMovimiento = StartCoroutine(RutinaMovimiento(destino, alLlegar));
    }

    private IEnumerator RutinaMovimiento(Vector2 destino, Action alLlegar)
    {
        if (anim != null) anim.SetBool("isWalking", true);

        // Mantenemos la Z original del señor para no enviarlo al fondo
        float zOriginal = transform.position.z;

        while (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), destino) > 0.05f)
        {
            Vector2 posActual = new Vector2(transform.position.x, transform.position.y);
            Vector2 direccion = (destino - posActual).normalized;

            if (anim != null)
            {
                anim.SetFloat("MoveX", direccion.x);
                anim.SetFloat("MoveY", direccion.y);
            }

            if (sr != null)
            {
                if (direccion.x < -0.1f) sr.flipX = true;
                else if (direccion.x > 0.1f) sr.flipX = false;
            }

            Vector2 nuevaPos = Vector2.MoveTowards(posActual, destino, velocidad * Time.deltaTime);
            transform.position = new Vector3(nuevaPos.x, nuevaPos.y, zOriginal);
            yield return null;
        }

        transform.position = new Vector3(destino.x, destino.y, zOriginal);
        if (anim != null) anim.SetBool("isWalking", false);
        corrutinaMovimiento = null;

        alLlegar?.Invoke();
    }
}