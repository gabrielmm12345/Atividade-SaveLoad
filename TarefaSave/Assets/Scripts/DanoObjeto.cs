using UnityEngine;

public class DanoObjeto : MonoBehaviour
{
    public int dano = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().PerderVida(dano);
            FindObjectOfType<AutoSaveUI>().ShowMessage();
            Destroy(gameObject);
        }
    }
}

