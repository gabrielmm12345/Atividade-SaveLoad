using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("Atributos do Player")]
    public int vida = 100;
    public int itens = 0;

    [Header("UI")]
    public Text mensagemSalvamento;

    [Header("API")]
    public string apiUrl = "https://68ffac48e02b16d1753eea76.mockapi.io/player/1"; 

    void Start()
    {
        if (mensagemSalvamento != null)
            mensagemSalvamento.enabled = false;

        StartCoroutine(CarregarDadosDaAPI());
    }

    public void AdicionarItem(int quantidade)
    {
        itens += quantidade;
        ShowSaveMessage();
    }

    public void PerderVida(int dano)
    {
        vida -= dano;
        ShowSaveMessage();
    }


    void ShowSaveMessage()
    {
        if (mensagemSalvamento != null)
            StartCoroutine(ShowTemporaryMessage());

        StartCoroutine(SalvarDadosNaAPI());
    }

    IEnumerator ShowTemporaryMessage()
    {
        mensagemSalvamento.enabled = true;
        yield return new WaitForSeconds(2f);
        mensagemSalvamento.enabled = false;
    }

    IEnumerator SalvarDadosNaAPI()
    {
        PlayerData data = new PlayerData
        {
            Vida = vida,
            QuantidadedeItens = itens,
            PosicaoX = transform.position.x,
            PosicaoY = transform.position.y,
            PosicaoZ = transform.position.z
        };

        string json = JsonUtility.ToJson(data);
        UnityWebRequest request = new UnityWebRequest(apiUrl, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao salvar dados: " + request.error);
        }
        else
        {
            Debug.Log("Dados salvos no MockAPI!");
        }
    }

    IEnumerator CarregarDadosDaAPI()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao carregar dados: " + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            vida = data.Vida;
            itens = data.QuantidadedeItens;
            transform.position = new Vector3(data.PosicaoX, data.PosicaoY, data.PosicaoZ);

            Debug.Log("Dados carregados do MockAPI!");
        }
    }
    // Permite salvar na API diretamente, usado pelo botão Recarregar
    public void ForceSaveToAPI()
    {
        StartCoroutine(SalvarDadosNaAPI());
    }

}

[System.Serializable]
public class PlayerData
{
    public int Vida;
    public int QuantidadedeItens;
    public float PosicaoX;
    public float PosicaoY;
    public float PosicaoZ;
}



