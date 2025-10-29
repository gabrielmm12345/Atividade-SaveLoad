using UnityEngine;

public class ColetarItem : MonoBehaviour
{
    public int quantidade = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().AdicionarItem(quantidade);
            FindObjectOfType<AutoSaveUI>().ShowMessage();
            Destroy(gameObject);
        }
    }
}

