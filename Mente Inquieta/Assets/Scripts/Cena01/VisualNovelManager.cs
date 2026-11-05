using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualNovelManager : MonoBehaviour
{
    [Header("Referências de UI")]
    public TextMeshProUGUI txtNome;
    public TypewriterEffect efeitoTexto; // Arraste o objeto que tem o texto da fala e o script Typewriter
    public Image imgPersonagem;
    public Image imgCenario;

    [Header("Sistema de Escolhas")]
    public GameObject painelEscolhas;    // Um painel vazio na UI para organizar os botões
    public GameObject prefabBotaoEscolha; // O Prefab do botão criado no passo 4

    [Header("História Inicial")]
    public DialogueData nodoInicial;
    private DialogueData nodoAtual;

    void Start()
    {
        painelEscolhas.SetActive(false);
        if (nodoInicial != null) CarregarDialogo(nodoInicial);
    }

    void Update()
    {
        // Só avança no clique se NÃO houver escolhas ativas na tela
        if (nodoAtual != null && nodoAtual.escolhas.Length == 0)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (efeitoTexto.IsWriting)
                {
                    efeitoTexto.ForçarTextoCompleto();
                }
                else
                {
                    Avançar();
                }
            }
        }
    }

    public void CarregarDialogo(DialogueData novoNodo)
    {
        if (novoNodo == null)
        {
            Debug.Log("Fim da história ou ramificação vazia.");
            return;
        }

        nodoAtual = novoNodo;
        LimparBotoesEscolha();

        // 1. Textos
        txtNome.text = nodoAtual.nomeDoPersonagem;
        efeitoTexto.IniciarTexto(nodoAtual.textoDoDialogo);

        // 2. Visuais
        if (nodoAtual.spritePersonagem != null)
        {
            imgPersonagem.gameObject.SetActive(true);
            imgPersonagem.sprite = nodoAtual.spritePersonagem;
        }
        else
        {
            imgPersonagem.gameObject.SetActive(false);
        }

        if (nodoAtual.cenarioFundo != null)
        {
            imgCenario.sprite = nodoAtual.cenarioFundo;
        }

        // 3. Áudio
        if (nodoAtual.musicaDeFundo != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.TrocarMusica(nodoAtual.musicaDeFundo);
        }

        // 4. Se houver escolhas, gera os botões após carregar os dados
        if (nodoAtual.escolhas.Length > 0)
        {
            GerarEscolhas();
        }
    }

    void Avançar()
    {
        if (nodoAtual.proximoDialogoDireto != null)
        {
            CarregarDialogo(nodoAtual.proximoDialogoDireto);
        }
    }

    void GerarEscolhas()
    {
        painelEscolhas.SetActive(true);

        foreach (Choice escolha in nodoAtual.escolhas)
        {
            GameObject novoBotao = Instantiate(prefabBotaoEscolha, painelEscolhas.transform);
            ChoiceButton scriptBotao = novoBotao.GetComponent<ChoiceButton>();
            
            scriptBotao.ConfigurarBotao(escolha.textoDaEscolha, escolha.proximoDialogo, this);
        }
    }

    void LimparBotoesEscolha()
    {
        painelEscolhas.SetActive(false);
        foreach (Transform filho in painelEscolhas.transform)
        {
            Destroy(filho.gameObject);
        }
    }
}
