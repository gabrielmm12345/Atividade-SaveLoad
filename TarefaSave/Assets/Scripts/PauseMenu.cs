using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuUI;
    public TextMeshProUGUI vidaText;
    public TextMeshProUGUI itensText;
    public TextMeshProUGUI posicaoText;

    private bool isPaused = false;
    private PlayerStats player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        menuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isPaused = !isPaused;
        menuUI.SetActive(isPaused);

        if (isPaused)
        {
            AtualizarInformacoes();
            Time.timeScale = 0f;  // pausa jogo
        }
        else
        {
            Time.timeScale = 1f; // retoma jogo
        }
    }

    public void AtualizarInformacoes()
    {
        vidaText.text = "Vida: " + player.vida;
        itensText.text = "Itens: " + player.itens;

        Vector3 pos = player.transform.position;
        posicaoText.text = $"Posição: ({pos.x:F1}, {pos.y:F1}, {pos.z:F1})";
    }

    // Novo método para o botão Recarregar
    public void RecarregarJogo()
    {
        // Resetar dados do player
        player.vida = 100;
        player.itens = 0;
        player.transform.position = Vector3.zero;

        // Salvar os dados resetados na API
        player.ForceSaveToAPI();

        // Retomar o tempo da cena
        Time.timeScale = 1f;

        // Recarregar a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


