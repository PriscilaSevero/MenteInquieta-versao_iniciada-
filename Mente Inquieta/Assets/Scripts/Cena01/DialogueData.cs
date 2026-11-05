using UnityEngine;

[System.Serializable]
public struct Choice
{
    public string textoDaEscolha;
    public DialogueData proximoDialogo; // Para onde essa escolha leva
}

[CreateAssetMenu(fileName = "NovoDialogo", menuName = "Visual Novel/Nodo de Dialogo")]
public class DialogueData : ScriptableObject
{
    public string nomeDoPersonagem;
    [TextArea(3, 10)]
    public string textoDoDialogo;
    public Sprite spritePersonagem;
    public Sprite cenarioFundo;
    public AudioClip musicaDeFundo;

    [Header("Fluxo da História")]
    public DialogueData proximoDialogoDireto; // Se não houver escolhas, vai para este
    
    [Header("Escolhas (Deixe vazio se for linear)")]
    public Choice[] escolhas;
}
