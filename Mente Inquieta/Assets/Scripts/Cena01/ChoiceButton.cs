using UnityEngine;
using TMPro;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtBotao;
    private DialogueData proximoNodo;
    private VisualNovelManager manager;

    public void ConfigurarBotao(string texto, DialogueData nodoDestino, VisualNovelManager vnManager)
    {
        txtBotao.text = texto;
        proximoNodo = nodoDestino;
        manager = vnManager;
    }

    // Vincule esta função no evento OnClick() do componente Button na Unity
    public void AoClicar()
    {
        manager.CarregarDialogo(proximoNodo);
    }
}