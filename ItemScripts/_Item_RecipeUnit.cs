using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Item_RecipeUnit : MonoBehaviour
{
    //——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    public string _Basic_Key_String;
    public string _Basic_Type_String;
    //語言資料
    [HideInInspector] public LanguageClass _Basic_Language_Class;
    //檔案資料
    [HideInInspector] public _Item_Manager.ObjectRecipeDataClass _Basic_ObjectData_Class;
    [HideInInspector] public _Item_Manager.ConceptRecipeDataClass _Basic_ConceptData_Class;
    [HideInInspector] public _Item_Manager.RecipeShareDataClass _Basic_MaterialData_Class;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public Text _View_Name_Text;
    public Text _View_Type_Text;
    public Image _View_Image_Image;
    public _UI_HintEffect _View_Hint_Script;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void MaterialSet(_Item_MaterialUnit[] Materials ,_Item_Manager.MaterialDataClass MaterialData, Dictionary<string, float> StatusData)
    {
        int QuickSave_MaterialCount_Int = 0;
        int QuickSave_MaterialSize_Int = 0;
        int QuickSave_MaterialForm_Int = 0;
        int QuickSave_MaterialWeight_Int = 0;
        int QuickSave_MaterialPurity_Int = 0;
        for (int a = 0; a < Materials.Length; a++)
        {
            if (Materials[a] != null)
            {
                _Map_BattleObjectUnit QS_Object_Script = Materials[a]._Basic_Object_Script;
                QuickSave_MaterialCount_Int++;
                QuickSave_MaterialSize_Int += Mathf.RoundToInt(QS_Object_Script.Key_Material("Size", QS_Object_Script._Basic_Source_Class, null));
                QuickSave_MaterialForm_Int += Mathf.RoundToInt(QS_Object_Script.Key_Material("Form", QS_Object_Script._Basic_Source_Class, null));
                QuickSave_MaterialWeight_Int += Mathf.RoundToInt(QS_Object_Script.Key_Material("Weight", QS_Object_Script._Basic_Source_Class, null));
                QuickSave_MaterialPurity_Int += Mathf.RoundToInt(QS_Object_Script.Key_Material("Purity", QS_Object_Script._Basic_Source_Class, null));
            }
        }
        if (QuickSave_MaterialCount_Int != 0)
        {
            MaterialData.Status["Size"] = QuickSave_MaterialSize_Int / QuickSave_MaterialCount_Int;
            MaterialData.Status["Form"] = QuickSave_MaterialForm_Int / QuickSave_MaterialCount_Int;
            MaterialData.Status["Weight"] = QuickSave_MaterialWeight_Int / QuickSave_MaterialCount_Int;
            MaterialData.Status["Purity"] = QuickSave_MaterialPurity_Int / QuickSave_MaterialCount_Int;
        }

        switch (_Basic_Type_String)
        {
            case "Weapon":
                MaterialData.Tag = new List<string> { "Weapon" };
                break;
            case "Item":
                MaterialData.Tag = new List<string> { "Item" };
                break;
            case "Concept":
                {
                    List<string> QuickSave_Keys_StringList = new List<string>
                    {
                        "Medium",
                        "Catalyst",
                        "Consciousness",
                        "Vitality",
                        "Strength",
                        "Precision",
                        "Speed",
                        "Luck"
                    };
                    for (int a = 0; a < QuickSave_Keys_StringList.Count; a++)
                    {
                        StatusData[QuickSave_Keys_StringList[a]] =
                            _Basic_ConceptData_Class.Status[a, 0] + (
                            _Basic_ConceptData_Class.Status[a, 1] * MaterialData.Status["Size"] +
                            _Basic_ConceptData_Class.Status[a, 2] * MaterialData.Status["Form"] +
                            _Basic_ConceptData_Class.Status[a, 3] * MaterialData.Status["Weight"] +
                            _Basic_ConceptData_Class.Status[a, 4] * MaterialData.Status["Purity"]);
                    }
                    MaterialData.Tag = new List<string> { "Concept" };
                }
                break;
            case "Material":
                break;
        }
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public bool ProcessesCheck(KeyValuePair<string, int>[] Process)/*確認是否都使用Process了*/
    {
        int QuickSave_Count_Int = 0;
        for (int a = 0; a < Process.Length; a++)
        {
            if (Process[a].Key != null)
            {
                QuickSave_Count_Int++;
            }
        }
        switch (_Basic_Type_String)
        {

            case "Weapon":
            case "Item":
                if (QuickSave_Count_Int != _Basic_ObjectData_Class.Process.Count)
                {
                    return false;
                }
                break;
            case "Concept":
                if (QuickSave_Count_Int != _Basic_ConceptData_Class.Process.Count)
                {
                    return false;
                }
                break;
            case "Material":
                if (QuickSave_Count_Int != _Basic_MaterialData_Class.Process.Count)
                {
                    return false;
                }
                break;
        }
        return true;
    }
    //——————————————————————————————————————————————————————————————————————
}
