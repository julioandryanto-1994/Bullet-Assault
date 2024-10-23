using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenuUI; 
    public Button startButton; 
    public FadeOutUI fadeOutUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        startMenuUI.SetActive(true); 
        //startButton.onClick.AddListener(StartGame);
        startButton.onClick.AddListener(FadeOutAndStart);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startMenuUI.SetActive(false);
    }

    public void FadeOutAndStart()
    {
        fadeOutUI.StartFadeOut(1f);
    }

}
