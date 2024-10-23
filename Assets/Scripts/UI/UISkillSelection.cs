using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillSelection : MonoBehaviour
{
    public List<SOSkill> skillList = new List<SOSkill>();
    public List<SOSkill> skills = new List<SOSkill>();

    public List<Button> btnSkillSelection = new List<Button>();
    public List<TextMeshProUGUI> txtSkillNames = new List<TextMeshProUGUI>();
    public List<Image> imgSkill = new List<Image>();

    private void OnEnable()
    {
        GetRandomSkill();
    }

    private void GetRandomSkill()
    {
        skills = new List<SOSkill>();

        for (int i = 0; i < skillList.Count; i++)
        {
            skills.Add(skillList[i]);
        }

        for (int i = 0; i < 3; i++)
        { 
            int selectedSkillIndex = Random.Range(0, skills.Count);

            txtSkillNames[i].text = skills[selectedSkillIndex].SkillName;
            imgSkill[i].sprite = skills[selectedSkillIndex].SkillIcon;

            btnSkillSelection[i].onClick.RemoveAllListeners();
            int index = skills[selectedSkillIndex].SkillID;
            btnSkillSelection[i].onClick.AddListener(() => Player.instance.PowerUp(index));

            skills.RemoveAt(selectedSkillIndex);
        }
    }
}
