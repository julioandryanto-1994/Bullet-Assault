using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SOSkill : ScriptableObject
{
    public int SkillID;
    public string SkillName;
    public Sprite SkillIcon;

    [MenuItem("ScriptableObjects/Skill")]
    public static void QuickCreate()
    {
        SOSkill asset = CreateInstance<SOSkill>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/Data/Skill.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

}
