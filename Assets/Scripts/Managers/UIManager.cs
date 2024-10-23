using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject PnlSkillSelection;

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateUI()
    { 
        
    }
}
