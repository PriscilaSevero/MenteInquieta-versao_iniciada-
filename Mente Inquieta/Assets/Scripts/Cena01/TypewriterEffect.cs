using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    [SerializeField] private float velocidadeDigitacao = 0.03f;
    private Coroutine coroutineDigitacao;
    
    public bool IsWriting { get; private set; }
    private string textoCompleto;

    void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void IniciarTexto(string texto)
    {
        textoCompleto = texto;
        
        // Se já estiver digitando um texto anterior, para ele antes de começar o novo
        if (coroutineDigitacao != null) 
            StopCoroutine(coroutineDigitacao);
            
        coroutineDigitacao = StartCoroutine(Digitar());
    }

    // CORREÇÃO AQUI: Mudado de 'Coroutine' para 'IEnumerator'
    private IEnumerator Digitar()
    {
        IsWriting = true;
        textMeshPro.text = "";
        
        foreach (char letra in textoCompleto.ToCharArray())
        {
            textMeshPro.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }
        
        IsWriting = false;
    }

    public void ForçarTextoCompleto()
    {
        if (coroutineDigitacao != null) 
            StopCoroutine(coroutineDigitacao);
            
        textMeshPro.text = textoCompleto;
        IsWriting = false;
    }
}