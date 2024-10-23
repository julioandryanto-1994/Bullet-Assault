using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public GameObject PnlGameOver;

    public void ShowGameOver()
    {
        if (PnlGameOver != null)
        {
            PnlGameOver.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("panel game over belum ada");
        }
    }
    
    public void RestartGame()
    {
        PnlGameOver.SetActive(false);
        Time.timeScale = 1f;
    }
}

    
