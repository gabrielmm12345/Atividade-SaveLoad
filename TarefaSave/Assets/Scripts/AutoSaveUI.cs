using UnityEngine;
using TMPro;
using System.Collections;

public class AutoSaveUI : MonoBehaviour
{
    public TextMeshProUGUI autoSaveText;
    public float displayTime = 2f;

    private void Start()
    {
        autoSaveText.text = "";
    }

    public void ShowMessage()
    {
        StopAllCoroutines();
        StartCoroutine(DisplayAutoSave());
    }

    IEnumerator DisplayAutoSave()
    {
        autoSaveText.text = "Salvamento automático...";
        yield return new WaitForSeconds(displayTime);
        autoSaveText.text = "";
    }
}

