using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using TMPro;
using System.Diagnostics;

public class _View_Alchemy : MonoBehaviour
{
    //——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    public Image _View_HardBack_Image;
    public Image _View_TopBack_Image;
    public Sprite[] _View_BackSprite_SpriteArray;

    public Text _View_Name_Text;

    public Image _View_Image_Image;

    public Transform[] _View_InfoType_Transfome;
    //生物類
    public List<Text> _View_ConceptMaterialStatusValue_TextList = new List<Text>();

    public List<Text> _View_ConceptStatus_TextList = new List<Text>();
    public List<Image> _View_ConceptStatusImage_ImageList = new List<Image>();
    public List<Sprite> _View_ConceptStatusSprite_SpriteList = new List<Sprite>();
    //物體類
    public UIPolygon _View_StatusPolygon_Scrtips;
    public List<Text> _View_MaterialStatusValue_TextList = new List<Text>();
    public List<Image> _View_TagImage_ImageList = new List<Image>();

    public List<Image> _View_Material_ImageList = new List<Image>();
    public List<Text> _View_MaterialName_TextList = new List<Text>();
    public List<Text> _View_MaterialStatusSizeText_TextList = new List<Text>();
    public List<Text> _View_MaterialStatusFormText_TextList = new List<Text>();
    public List<Text> _View_MaterialStatusWeightText_TextList = new List<Text>();
    public List<Text> _View_MaterialStatusPurityText_TextList = new List<Text>();
    public List<Text> _View_MaterialStatusSizeValue_TextList = new List<Text>();
    public List<Text> _View_MaterialStatusFormValue_TextList = new List<Text>();
    public List<Text> _View_MaterialStatusWeightValue_TextList = new List<Text>();
    public List<Text> _View_MaterialStatusPurityValue_TextList = new List<Text>();

    public List<Text> _View_MaterialInherit_List = new List<Text>();
    public Text _View_ProcessDescription_Text;

    public Text _View_ProcessName_Text;
    public Image _View_ProcessType_Image;
    public List<Image> _View_ProcessBar_ImageList = new List<Image>();
    public List<Image> _View_ProcessMaterials_ImageList = new List<Image>();
    public List<Text> _View_ProcessMaterialsInput_TextList = new List<Text>();
    public List<List<Text>> _View_ProcessMaterials_TextList = new List<List<Text>>();
    public Text _View_ProcessAnswer_Text;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    [HideInInspector] public string Alchemy_AlchemySave_String;
    [HideInInspector] public int Alchemy_MaterialSelect_Int = 65535;
    public Transform[] Alchemy_SummaryTransforms;
    /*
     * 00：Recipe
     * 01：Alchemy
     */

    [HideInInspector] public _Item_RecipeUnit Alchemy_OnRecipe_Script;
    public List<_Item_Manager.RecipeDataClass> Alchemy_OnRecipeMaterials_ScriptsList = new List<_Item_Manager.RecipeDataClass>();
    public List<_Item_Manager.ProcessDataClass> Alchemy_OnRecipeProcesss_ScriptsList = new List<_Item_Manager.ProcessDataClass>();
    public _View_AlchemyUpdating Alchemy_OnProcess_Script;
    public int _Alchemy_NowProcess_Int;
    //----------------------------------------------------------------------------------------------------

    //製作中物體素質----------------------------------------------------------------------------------------------------
    //紀錄
    public int[] _Alchemy_InheritSave_IntArray = new int[4];
    //合成材料
    public _Item_MaterialUnit[] _Alchemy_Material_ScriptsArray = new _Item_MaterialUnit[4];
    public KeyValuePair<string, int>[] Alchemy_Processes_PairArray = new KeyValuePair<string, int>[3];
    //成品資料
    public _Item_Manager.MaterialDataClass _Alchemy_MaterialsData_Class = new _Item_Manager.MaterialDataClass();
    //成品素質
    public Dictionary<string, float> _Alchemy_Status_Dictionary = new Dictionary<string, float>();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void StartSet()
    {
        //----------------------------------------------------------------------------------------------------
        Alchemy_AlchemySave_String = "Alchemy_Recipe";
        for (int a = 0; a < Alchemy_SummaryTransforms.Length; a++)
        {
            Alchemy_SummaryTransforms[a].gameObject.SetActive(false);
        }

        int QuickSave_Count_Int = 0;
        for (int a= 0; a < 4; a ++)
        {
            List<Text> QuickSave_AddenText_TextList = new List<Text>();
            for (int b = 0; b < 4; b++)
            {
                QuickSave_AddenText_TextList.Add(_View_ProcessMaterialsInput_TextList[QuickSave_Count_Int]);
                QuickSave_Count_Int++;
            }
            _View_ProcessMaterials_TextList.Add(QuickSave_AddenText_TextList);
        }
        _View_ProcessMaterialsInput_TextList = null;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    #region - Alchemy -
    //——————————————————————————————————————————————————————————————————————
    public void Alchemy_MaterialInventorySet(int RecipeTarget = 65535)
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _UI_Manager.UISet("Alchemy_Material");
        _Item_Object_Inventory QuickSave_Inventory_Script = 
            _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script;
        _Item_Manager.RecipeDataClass QuickSave_Recipe_Class = 
            Alchemy_OnRecipeMaterials_ScriptsList[RecipeTarget];
        //分類與整理
        QuickSave_Inventory_Script.ItemFilterSet("Materials", "Recipe", RecipeFilter: QuickSave_Recipe_Class);
        Alchemy_MaterialSelect_Int = RecipeTarget;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void Alchemy_MaterialInput()
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _View_TinyMenu _View_TinyMenu = _UI_Manager._UI_Camp_Class._View_TinyMenu;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //跳出
        if (Alchemy_MaterialSelect_Int == 65535)
        {
            print("Wrong with SelectInt :" + Alchemy_MaterialSelect_Int);
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _Item_MaterialUnit QuickSave_Material_Script = _View_TinyMenu.Select_Source_Class.Source_Material;
        //置入素材
        if (_Alchemy_Material_ScriptsArray[Alchemy_MaterialSelect_Int] != null)
        {
            _Alchemy_Material_ScriptsArray[Alchemy_MaterialSelect_Int]._View_Hint_Script.HintSet("UnUsing", "Material");
        }
        QuickSave_Material_Script._View_Hint_Script.HintSet("Using", "Material");
        _Alchemy_Material_ScriptsArray[Alchemy_MaterialSelect_Int] = QuickSave_Material_Script;
        Alchemy_OnRecipe_Script.MaterialSet(_Alchemy_Material_ScriptsArray, _Alchemy_MaterialsData_Class, _Alchemy_Status_Dictionary);
        Alchemy_MaterialSet();
        return;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void Alchemy_RecipeStartSet(_Item_RecipeUnit Recipe = null)
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        Dictionary<string, string> QuickSave_UIName_Dictionary = _World_TextManager._Language_UIName_Dictionary;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (Alchemy_OnRecipe_Script != null)
        {
            Alchemy_OnRecipe_Script._View_Hint_Script.HintSet("UnUsing", "Recipe");
        }
        if (Alchemy_OnRecipe_Script == Recipe)
        {
            Alchemy_OnRecipe_Script._View_Hint_Script.HintSet("UnUsing", "Recipe");
            _UI_Manager.UISet("Alchemy_Recipe");
            return;
        }

        _UI_Manager.UISet("Alchemy_Alchemy");
        Recipe._View_Hint_Script.HintSet("UnNew", "Recipe");
        Recipe._View_Hint_Script.HintSet("Using", "Recipe");

        Alchemy_OnRecipe_Script = Recipe;
        //初始化
        //繼承
        _Alchemy_InheritSave_IntArray = new int[4]
        {
            5,5,5,5
        };
        //道具
        for (int a = 0; a < _Alchemy_Material_ScriptsArray.Length; a++)
        {
            if (_Alchemy_Material_ScriptsArray[a] != null)
            {
                _Alchemy_Material_ScriptsArray[a]._View_Hint_Script.HintSet("UnUsing", "Material");
                _Alchemy_Material_ScriptsArray[a] = null;
            }
        }
        //加工
        Alchemy_Processes_PairArray = new KeyValuePair<string, int>[3];
        //素材素質
        _Alchemy_MaterialsData_Class = new _Item_Manager.MaterialDataClass();
        List<string> QuickSave_Material_StringList = _Item_Manager._Data_StatusMaterial_StringList;
        foreach (string Material in QuickSave_Material_StringList)
        {
            _Alchemy_MaterialsData_Class.Status.Add(Material, 0);
        }
        //能力素質
        _Alchemy_Status_Dictionary = new Dictionary<string, float>();
        List<string> QuickSave_Status_StringList = new List<string>();
        switch (Recipe._Basic_Type_String)
        {
            case "Concept":
                QuickSave_Status_StringList = _Item_Manager._Data_StatusConcept_StringList;
                break;
            default:
                break;
        }
        foreach (string Status in QuickSave_Status_StringList)
        {
            _Alchemy_Status_Dictionary.Add(Status, 0);
        }

        //視覺設定
        _View_Name_Text.text = Recipe._Basic_Language_Class.Name;
        _View_Image_Image.sprite = 
            _View_Manager.GetSprite(Recipe._Basic_Type_String, "Info", Recipe._Basic_Key_String);

        switch (Recipe._Basic_Type_String)
        {
            case "Weapon":
                {
                    _View_InfoType_Transfome[0].gameObject.SetActive(true);
                    _View_InfoType_Transfome[1].gameObject.SetActive(false);

                    _View_HardBack_Image.sprite = _View_BackSprite_SpriteArray[0];
                    _View_TopBack_Image.sprite = _View_BackSprite_SpriteArray[2];
                    Alchemy_OnRecipeMaterials_ScriptsList = Recipe._Basic_ObjectData_Class.Recipe;
                    Alchemy_OnRecipeProcesss_ScriptsList = Recipe._Basic_ObjectData_Class.Process;

                    List<string> QuickSave_Tag = _Item_Manager._Data_WeaponRecipe_Dictionary[Recipe._Basic_Key_String].Tag;
                    for (int a = 0; a < _View_TagImage_ImageList.Count; a++)
                    {
                        if (a < QuickSave_Tag.Count)
                        {
                            _View_TagImage_ImageList[a].raycastTarget = true;
                            _View_TagImage_ImageList[a].sprite = _View_Manager.GetSprite("Icon", "Tag", QuickSave_Tag[a]);
                        }
                        else
                        {
                            _View_TagImage_ImageList[a].raycastTarget = false;
                            _View_TagImage_ImageList[a].sprite = _View_Manager.GetSprite("null", "null");
                        }
                    }
                }
                break;
            case "Item":
                {
                    _View_InfoType_Transfome[0].gameObject.SetActive(true);
                    _View_InfoType_Transfome[1].gameObject.SetActive(false);

                    _View_HardBack_Image.sprite = _View_BackSprite_SpriteArray[0];
                    _View_TopBack_Image.sprite = _View_BackSprite_SpriteArray[2];
                    Alchemy_OnRecipeMaterials_ScriptsList = Recipe._Basic_ObjectData_Class.Recipe;
                    Alchemy_OnRecipeProcesss_ScriptsList = Recipe._Basic_ObjectData_Class.Process;

                    List<string> QuickSave_Tag = _Item_Manager._Data_ItemRecipe_Dictionary[Recipe._Basic_Key_String].Tag;
                    for (int a = 0; a < _View_TagImage_ImageList.Count; a++)
                    {
                        if (a < QuickSave_Tag.Count)
                        {
                            _View_TagImage_ImageList[a].raycastTarget = true;
                            _View_TagImage_ImageList[a].sprite = _View_Manager.GetSprite("Icon", "Tag", QuickSave_Tag[a]);
                        }
                        else
                        {
                            _View_TagImage_ImageList[a].raycastTarget = false;
                            _View_TagImage_ImageList[a].sprite = _View_Manager.GetSprite("null", "null");
                        }
                    }
                }
                break;
            case "Concept":
                _View_InfoType_Transfome[0].gameObject.SetActive(false);
                _View_InfoType_Transfome[1].gameObject.SetActive(true);

                _View_HardBack_Image.sprite = _View_BackSprite_SpriteArray[1];
                _View_TopBack_Image.sprite = _View_BackSprite_SpriteArray[3];
                Alchemy_OnRecipeMaterials_ScriptsList = Recipe._Basic_ConceptData_Class.Recipe;
                Alchemy_OnRecipeProcesss_ScriptsList = Recipe._Basic_ConceptData_Class.Process;
                for (int a = 0; a < 8; a++)
                {
                    _View_ConceptStatusImage_ImageList[a].sprite = _View_ConceptStatusSprite_SpriteList[a];
                    _View_ConceptStatusImage_ImageList[a].raycastTarget = true;
                }
                _View_ConceptStatusImage_ImageList[0].gameObject.name = "Bubble_UISummary_Medium";
                _View_ConceptStatusImage_ImageList[1].gameObject.name = "Bubble_UISummary_Catalyst";
                _View_ConceptStatusImage_ImageList[2].gameObject.name = "Bubble_UISummary_Consciousness";
                _View_ConceptStatusImage_ImageList[3].gameObject.name = "Bubble_UISummary_Vitality";
                _View_ConceptStatusImage_ImageList[4].gameObject.name = "Bubble_UISummary_Strength";
                _View_ConceptStatusImage_ImageList[5].gameObject.name = "Bubble_UISummary_Precision";
                _View_ConceptStatusImage_ImageList[6].gameObject.name = "Bubble_UISummary_Speed";
                _View_ConceptStatusImage_ImageList[7].gameObject.name = "Bubble_UISummary_Luck";
                break;
            case "Material":
                {
                    _View_InfoType_Transfome[0].gameObject.SetActive(true);
                    _View_InfoType_Transfome[1].gameObject.SetActive(false);

                    _View_HardBack_Image.sprite = _View_BackSprite_SpriteArray[0];
                    _View_TopBack_Image.sprite = _View_BackSprite_SpriteArray[2];
                    Alchemy_OnRecipeMaterials_ScriptsList = Recipe._Basic_MaterialData_Class.Recipe;
                    Alchemy_OnRecipeProcesss_ScriptsList = Recipe._Basic_MaterialData_Class.Process;

                    string QuickSave_Target_String = Recipe._Basic_MaterialData_Class.Target;

                    List<string> QuickSave_Tag = _Item_Manager._Data_Material_Dictionary[QuickSave_Target_String].Tag;
                    for (int a = 0; a < _View_TagImage_ImageList.Count; a++)
                    {
                        if (a < QuickSave_Tag.Count)
                        {
                            _View_TagImage_ImageList[a].raycastTarget = true;
                            _View_TagImage_ImageList[a].sprite = _View_Manager.GetSprite("Icon", "Tag", QuickSave_Tag[a]);
                        }
                        else
                        {
                            _View_TagImage_ImageList[a].raycastTarget = false;
                            _View_TagImage_ImageList[a].sprite = _View_Manager.GetSprite("null", "null");
                        }
                    }
                }
                break;
        }
        //詞綴繼承
        {
            for (int a = 0; a < 4; a++)
            {
                _View_MaterialInherit_List[a].text = "";
            }
            string QuickSave_Inhert_String = _World_TextManager._Language_UIName_Dictionary["Inherit"];
            for (int a = 0; a < Alchemy_OnRecipeMaterials_ScriptsList.Count; a++)
            {
                for (int b = 0; b < Alchemy_OnRecipeMaterials_ScriptsList[a].Inherit.Count; b++)
                {
                    int QuickSave_Inherit_Int = Alchemy_OnRecipeMaterials_ScriptsList[a].Inherit[b];
                    _Alchemy_InheritSave_IntArray[QuickSave_Inherit_Int] = a;
                    switch (a)
                    {
                        case 0:
                            _View_MaterialInherit_List[QuickSave_Inherit_Int].text = 
                                QuickSave_Inhert_String + _World_TextManager._Language_UIName_Dictionary["First"];
                            break;
                        case 1:
                            _View_MaterialInherit_List[QuickSave_Inherit_Int].text =
                                QuickSave_Inhert_String + _World_TextManager._Language_UIName_Dictionary["Second"];
                            break;
                        case 2:
                            _View_MaterialInherit_List[QuickSave_Inherit_Int].text =
                                QuickSave_Inhert_String + _World_TextManager._Language_UIName_Dictionary["Third"];
                            break;
                        case 3:
                            _View_MaterialInherit_List[QuickSave_Inherit_Int].text =
                                QuickSave_Inhert_String + _World_TextManager._Language_UIName_Dictionary["Fourth"];
                            break;
                    }
                }
            }
        }
        //加工
        {
            string QuickSave_ProcessDescription_String = "→";
            for (int a = 0; a < 3; a++)
            {
                if (Alchemy_OnRecipeProcesss_ScriptsList.Count > a)
                {
                    _Item_Manager.ProcessDataClass QuickSave_Process_Class = Alchemy_OnRecipeProcesss_ScriptsList[a];
                    string QuickSave_ProcessRange_String = "";
                    for (int b = 0; b < QuickSave_Process_Class.Range.Count; b++)
                    {
                        QuickSave_ProcessRange_String += 
                            QuickSave_Process_Class.Range[b].Min + "~" + QuickSave_Process_Class.Range[b].Max + ",";
                    }
                    QuickSave_ProcessRange_String = QuickSave_ProcessRange_String.Remove(QuickSave_ProcessRange_String.Length - 1);

                    QuickSave_ProcessDescription_String +=
                        "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", QuickSave_Process_Class.Type)) + ">" + 
                        _World_TextManager._Language_UIName_Dictionary[QuickSave_Process_Class.Type + "Process"] +
                        "<size=60>(" + QuickSave_ProcessRange_String + ")</size>" +
                        "</color>→";
                }
            }
            _View_ProcessDescription_Text.text = QuickSave_ProcessDescription_String;
        }

        Alchemy_MaterialSet();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void Alchemy_MaterialSet()
    {
        //----------------------------------------------------------------------------------------------------
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        //----------------------------------------------------------------------------------------------------

        //Status----------------------------------------------------------------------------------------------------
        switch (Alchemy_OnRecipe_Script._Basic_Type_String)
        {
            case "Weapon":
            case "Item":
            case "Material":
                {
                    _View_MaterialStatusValue_TextList[0].text =
                        _Alchemy_MaterialsData_Class.Status["Size"].ToString("0");
                    _View_MaterialStatusValue_TextList[1].text =
                        _Alchemy_MaterialsData_Class.Status["Form"].ToString("0");
                    _View_MaterialStatusValue_TextList[2].text =
                        _Alchemy_MaterialsData_Class.Status["Weight"].ToString("0");
                    _View_MaterialStatusValue_TextList[3].text =
                        _Alchemy_MaterialsData_Class.Status["Purity"].ToString("0");

                    _View_StatusPolygon_Scrtips.VerticesDistances[3] = 
                        Mathf.Clamp((float)_Alchemy_MaterialsData_Class.Status["Size"] / 25, 0, 1);
                    _View_StatusPolygon_Scrtips.VerticesDistances[0] = 
                        Mathf.Clamp((float)_Alchemy_MaterialsData_Class.Status["Form"] / 25, 0, 1);
                    _View_StatusPolygon_Scrtips.VerticesDistances[2] = 
                        Mathf.Clamp((float)_Alchemy_MaterialsData_Class.Status["Weight"] / 25, 0, 1);
                    _View_StatusPolygon_Scrtips.VerticesDistances[1] = 
                        Mathf.Clamp((float)_Alchemy_MaterialsData_Class.Status["Purity"] / 25, 0, 1);
                    StartCoroutine(_View_Manager.EquipStatusPolygonAnimate(_View_StatusPolygon_Scrtips));
                }
                break;                
            case "Concept":
                {
                    _View_ConceptMaterialStatusValue_TextList[0].text = 
                        _Alchemy_MaterialsData_Class.Status["Size"].ToString("0");
                    _View_ConceptMaterialStatusValue_TextList[1].text = 
                        _Alchemy_MaterialsData_Class.Status["Form"].ToString("0");
                    _View_ConceptMaterialStatusValue_TextList[2].text = 
                        _Alchemy_MaterialsData_Class.Status["Weight"].ToString("0");
                    _View_ConceptMaterialStatusValue_TextList[3].text = 
                        _Alchemy_MaterialsData_Class.Status["Purity"].ToString("0");

                    _View_ConceptStatus_TextList[0].text = 
                        Mathf.RoundToInt(_Alchemy_Status_Dictionary["Medium"]).ToString("0");
                    _View_ConceptStatus_TextList[1].text = 
                        Mathf.RoundToInt(_Alchemy_Status_Dictionary["Catalyst"]).ToString("0");
                    _View_ConceptStatus_TextList[2].text = 
                        Mathf.RoundToInt(_Alchemy_Status_Dictionary["Consciousness"]).ToString("0");
                    _View_ConceptStatus_TextList[3].text = 
                        Mathf.RoundToInt(_Alchemy_Status_Dictionary["Vitality"]).ToString("0");
                    _View_ConceptStatus_TextList[4].text = 
                        Mathf.RoundToInt(_Alchemy_Status_Dictionary["Strength"]).ToString("0");
                    _View_ConceptStatus_TextList[5].text = 
                        Mathf.RoundToInt(_Alchemy_Status_Dictionary["Precision"]).ToString("0");
                    _View_ConceptStatus_TextList[6].text = 
                        Mathf.RoundToInt(_Alchemy_Status_Dictionary["Speed"]).ToString("0");
                    _View_ConceptStatus_TextList[7].text = 
                        Mathf.RoundToInt(_Alchemy_Status_Dictionary["Luck"]).ToString("0");
                }
                break;                
        }

        for (int a = 0; a < 4; a++)
        {
            if (a < Alchemy_OnRecipeMaterials_ScriptsList.Count)
            {
                //包含素材
                if (_Alchemy_Material_ScriptsArray[a] != null)
                {
                    _Item_MaterialUnit QuickSave_Material_Script = 
                        _Alchemy_Material_ScriptsArray[a];
                    _Map_BattleObjectUnit QS_MatObject_Script = 
                        QuickSave_Material_Script._Basic_Object_Script;
                    _Item_Manager.MaterialDataClass QS_MaterialData_Class = 
                        QS_MatObject_Script._Basic_Material_Class;
                    _View_Material_ImageList[a].sprite = 
                        _View_Manager.GetSprite("Material", "Icon", 
                        QuickSave_Material_Script._Basic_Object_Script._Basic_Key_String);
                    _View_MaterialName_TextList[a].text = 
                        QuickSave_Material_Script._Basic_Object_Script._Basic_Language_Class.Name;

                    _View_MaterialStatusSizeText_TextList[a].text = 
                        _World_TextManager._Language_UIName_Dictionary["Size"];
                    _View_MaterialStatusFormText_TextList[a].text = 
                        _World_TextManager._Language_UIName_Dictionary["Form"];
                    _View_MaterialStatusWeightText_TextList[a].text = 
                        _World_TextManager._Language_UIName_Dictionary["Weight"];
                    _View_MaterialStatusPurityText_TextList[a].text = 
                        _World_TextManager._Language_UIName_Dictionary["Purity"];

                    _View_MaterialStatusSizeValue_TextList[a].text =
                        QS_MatObject_Script.Key_Material("Size", QS_MatObject_Script._Basic_Source_Class, null).ToString("0");
                    _View_MaterialStatusFormValue_TextList[a].text =
                        QS_MatObject_Script.Key_Material("Form", QS_MatObject_Script._Basic_Source_Class, null).ToString("0");
                    _View_MaterialStatusWeightValue_TextList[a].text =
                        QS_MatObject_Script.Key_Material("Weight", QS_MatObject_Script._Basic_Source_Class, null).ToString("0");
                    _View_MaterialStatusPurityValue_TextList[a].text =
                        QS_MatObject_Script.Key_Material("Purity", QS_MatObject_Script._Basic_Source_Class, null).ToString("0");

                    for (int b = 0; b < Alchemy_OnRecipeMaterials_ScriptsList[a].Inherit.Count; b++)
                    {
                        int QuickSave_Inherit_Int = Alchemy_OnRecipeMaterials_ScriptsList[a].Inherit[b];
                        string QuickSave_SpecialAffix_String =
                            QS_MaterialData_Class.SpecialAffix[QuickSave_Inherit_Int];
                        if (QuickSave_SpecialAffix_String == null || QuickSave_SpecialAffix_String == "")
                        {
                            _View_MaterialInherit_List[QuickSave_Inherit_Int].text = "";
                            continue;
                        }
                        _View_MaterialInherit_List[QuickSave_Inherit_Int].text =
                            _Item_Manager._Language_SpecialAffix_Dictionary[QuickSave_SpecialAffix_String].Name;
                    }
                }
                else
                {
                    //未有素材
                    _View_Material_ImageList[a].sprite = 
                        _View_Manager.GetSprite("Material", "null", "null");
                    _Item_Manager.RecipeDataClass QuickSave_RecipeUnit_Class = 
                        Alchemy_OnRecipeMaterials_ScriptsList[a];
                    switch (QuickSave_RecipeUnit_Class.Type)
                    {
                        case "Class":
                            {
                                string QuickSave_Name_String = QuickSave_RecipeUnit_Class.Key;
                                if (_World_TextManager._Language_Tag_Dictionary.TryGetValue(QuickSave_Name_String, out LanguageClass Language))
                                {
                                    QuickSave_Name_String = Language.Name;
                                }
                                _View_MaterialName_TextList[a].text = "(" + QuickSave_Name_String + ")";
                            }
                            break;
                        case "Target":
                            {
                                string QuickSave_Name_String = QuickSave_RecipeUnit_Class.Key;
                                if (_Item_Manager._Language_Material_Dictionary.TryGetValue(QuickSave_Name_String, out LanguageClass Language))
                                {
                                    QuickSave_Name_String = Language.Name;
                                }
                                _View_MaterialName_TextList[a].text = "(" + QuickSave_Name_String + ")";
                            }
                            break;
                    }

                    _View_MaterialStatusSizeText_TextList[a].text = 
                        _World_TextManager._Language_UIName_Dictionary["AllowSize"];
                    _View_MaterialStatusFormText_TextList[a].text = 
                        _World_TextManager._Language_UIName_Dictionary["AllowForm"];
                    _View_MaterialStatusWeightText_TextList[a].text = 
                        _World_TextManager._Language_UIName_Dictionary["AllowWeight"];
                    _View_MaterialStatusPurityText_TextList[a].text = 
                        _World_TextManager._Language_UIName_Dictionary["AllowPurity"];

                    _View_MaterialStatusSizeValue_TextList[a].text = 
                        QuickSave_RecipeUnit_Class.Size.Min + "~" + QuickSave_RecipeUnit_Class.Size.Max;
                    _View_MaterialStatusFormValue_TextList[a].text = 
                        QuickSave_RecipeUnit_Class.Form.Min + "~" + QuickSave_RecipeUnit_Class.Form.Max;
                    _View_MaterialStatusWeightValue_TextList[a].text = 
                        QuickSave_RecipeUnit_Class.Weight.Min + "~" + QuickSave_RecipeUnit_Class.Weight.Max;
                    _View_MaterialStatusPurityValue_TextList[a].text = 
                        QuickSave_RecipeUnit_Class.Purity.Min + "~" + QuickSave_RecipeUnit_Class.Purity.Max;

                }
            }
            else
            {
                _View_Material_ImageList[a].sprite = _View_Manager.GetSprite("Material", "null", "null");
                _View_MaterialName_TextList[a].text = "";

                _View_MaterialStatusSizeText_TextList[a].text = "";
                _View_MaterialStatusFormText_TextList[a].text = "";
                _View_MaterialStatusWeightText_TextList[a].text = "";
                _View_MaterialStatusPurityText_TextList[a].text = "";

                _View_MaterialStatusSizeValue_TextList[a].text = "";
                _View_MaterialStatusFormValue_TextList[a].text = "";
                _View_MaterialStatusWeightValue_TextList[a].text = "";
                _View_MaterialStatusPurityValue_TextList[a].text = "";
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void Alchemy_ProcessStartSet(int ProceStep)
    {
        //----------------------------------------------------------------------------------------------------
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        _Item_Manager.ProcessDataClass QuickSave_Process_Class = Alchemy_OnRecipeProcesss_ScriptsList[ProceStep];
        string QuickSave_Type_String = QuickSave_Process_Class.Type;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _Alchemy_NowProcess_Int = ProceStep;
        string QuickSave_ProcessRange_String = "";
        for (int b = 0; b < QuickSave_Process_Class.Range.Count; b++)
        {
            QuickSave_ProcessRange_String += QuickSave_Process_Class.Range[b].Min + "~" + QuickSave_Process_Class.Range[b].Max + ",";
        }
        QuickSave_ProcessRange_String = QuickSave_ProcessRange_String.Remove(QuickSave_ProcessRange_String.Length - 1);
        _View_ProcessName_Text.text =
            _World_TextManager._Language_UIName_Dictionary[QuickSave_Type_String + "Process"] + "(" + QuickSave_ProcessRange_String + ")";
        _View_ProcessAnswer_Text.text = _World_TextManager._Language_UIName_Dictionary["StartProcess"];
        _View_ProcessType_Image.color = _View_Manager.GetColor("Code", QuickSave_Type_String);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        for (int a = 0; a < _Alchemy_Material_ScriptsArray.Length; a++)
        {
            if (_Alchemy_Material_ScriptsArray[a] != null)
            {
                _Item_MaterialUnit QuickSave_Material_Script = _Alchemy_Material_ScriptsArray[a];
                _View_ProcessMaterials_ImageList[a].sprite =
                    _View_Manager.GetSprite("Material", "Info", QuickSave_Material_Script._Basic_Object_Script._Basic_Key_String);
                string[] QS_SpecialAffix_StringArray = 
                    QuickSave_Material_Script._Basic_Object_Script._Basic_Material_Class.SpecialAffix;
                for (int b = 0; b < 4;b ++)
                {
                    string QuickSave_SpecialAffix_String = QS_SpecialAffix_StringArray[b];
                    if (QuickSave_SpecialAffix_String == null || QuickSave_SpecialAffix_String =="")
                    {
                        _View_ProcessMaterials_TextList[a][b].text = "";
                        continue;
                    }
                    _View_ProcessMaterials_TextList[a][b].text =
                        _Item_Manager._Language_SpecialAffix_Dictionary[QuickSave_SpecialAffix_String].Name;
                }
                for (int b = 0; b < _Alchemy_InheritSave_IntArray.Length;b++)
                {
                    if (_Alchemy_InheritSave_IntArray[b] == a)
                    {
                        _View_ProcessMaterials_TextList[a][b].color = Color.white;
                    }
                    else
                    {
                        _View_ProcessMaterials_TextList[a][b].color = _View_Manager.GetColor("Code", "Empty");
                    }
                }
            }
            else
            {
                _View_ProcessMaterials_ImageList[a].sprite = _View_Manager.GetSprite("null", "null");
                for (int b = 0; b < 4; b++)
                {
                    _View_ProcessMaterials_TextList[a][b].text ="";
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _View_ProcessBar_ImageList[0].transform.localScale = Vector3.zero;
        _View_ProcessBar_ImageList[0].color = _View_Manager.GetColor("Code", QuickSave_Type_String);
        _View_ProcessBar_ImageList[1].transform.localScale = Vector3.zero;
        _View_ProcessBar_ImageList[1].color = _View_Manager.GetColor("Code", QuickSave_Type_String);
        switch (QuickSave_Type_String)
        {
            case "Slash":
                _View_ProcessBar_ImageList[2].transform.localPosition = new Vector3(-108, -312, 0);

                _View_ProcessBar_ImageList[2].transform.localScale = new Vector3(0.8f, 1, 1);
                _View_ProcessBar_ImageList[2].color = _View_Manager.GetColor("Code", QuickSave_Type_String);

                _View_ProcessBar_ImageList[3].transform.localScale = Vector3.zero;
                break;
            case "Puncture":
                _View_ProcessBar_ImageList[2].transform.localPosition = new Vector3(-317, -312, 0);

                _View_ProcessBar_ImageList[2].transform.localScale = new Vector3(0.4f, 1, 1);
                _View_ProcessBar_ImageList[2].color = _View_Manager.GetColor("Code", QuickSave_Type_String);

                _View_ProcessBar_ImageList[3].transform.localPosition = new Vector3(103, -312, 0);

                _View_ProcessBar_ImageList[3].transform.localScale = new Vector3(0.4f, 1, 1);
                _View_ProcessBar_ImageList[3].color = _View_Manager.GetColor("Code", QuickSave_Type_String);
                break;
            case "Impact":
                _View_ProcessBar_ImageList[2].transform.localPosition = new Vector3(-108, -312, 0);

                _View_ProcessBar_ImageList[2].transform.localScale = new Vector3(0.8f, 1, 1);
                _View_ProcessBar_ImageList[2].color = _View_Manager.GetColor("Code", QuickSave_Type_String);

                _View_ProcessBar_ImageList[3].transform.localScale = Vector3.zero;
                break;
            case "Energy":
            case "Chaos":
                _View_ProcessBar_ImageList[2].transform.localPosition = new Vector3(-108, -312, 0);

                _View_ProcessBar_ImageList[2].transform.localScale = Vector3.one;
                _View_ProcessBar_ImageList[2].color = _View_Manager.GetColor("Code", QuickSave_Type_String);

                _View_ProcessBar_ImageList[3].transform.localScale = Vector3.zero;
                break;
            case "Abstract":
                _View_ProcessBar_ImageList[2].transform.localPosition = new Vector3(-317, -312, 0);

                _View_ProcessBar_ImageList[2].transform.localScale = new Vector3(0.4f, 1, 1);
                _View_ProcessBar_ImageList[2].color = _View_Manager.GetColor("Code", QuickSave_Type_String);

                _View_ProcessBar_ImageList[3].transform.localPosition = new Vector3(-108, -312, 0);

                _View_ProcessBar_ImageList[3].transform.localScale = new Vector3(0.4f, 1, 1);
                _View_ProcessBar_ImageList[3].color = _View_Manager.GetColor("Code", QuickSave_Type_String);
                break;
            case "Stark":
                _View_ProcessBar_ImageList[2].transform.localPosition = new Vector3(-108, -312, 0);

                _View_ProcessBar_ImageList[2].transform.localScale = new Vector3(0.8f, 1, 1);
                _View_ProcessBar_ImageList[2].color = _View_Manager.GetColor("Code", QuickSave_Type_String);

                _View_ProcessBar_ImageList[3].transform.localScale = Vector3.zero;
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void Alchemy_ProcessSet(int Target, string Key,int Value)
    {
        Alchemy_Processes_PairArray[Target] = new KeyValuePair<string, int>(
            Key, Value);
        if (Value != 200)
        {
            _View_ProcessAnswer_Text.text = Value + "%";
        }
        else
        {
            _View_ProcessAnswer_Text.text = _World_Manager._World_GeneralManager._World_TextManager._Language_UIName_Dictionary["FailProcess"];
        }
        StartCoroutine(Alchemy_ProcessAnswer());
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public IEnumerator Alchemy_ProcessAnswer()
    {
        _UI_Manager.CampClass _UI_Camp_Class = _World_Manager._UI_Manager._UI_Camp_Class;
        _UI_Camp_Class._UI_CampState_String = "Alchemy_ProcessingRest";
        yield return new WaitForSeconds(2f);
        _UI_Camp_Class._UI_CampState_String = "Alchemy_Processing";
        if (Alchemy_OnRecipe_Script.ProcessesCheck(Alchemy_Processes_PairArray))
        {
            //製作完成
            _UI_Camp_Class._UI_CampState_String = "Alchemy_Material";
            Alchemy_AlchemySet();
        }
        else
        {
            //下個加工
            Alchemy_ProcessStartSet(_Alchemy_NowProcess_Int + 1);
        }
    }
    //——————————————————————————————————————————————————————————————————————

    //合成——————————————————————————————————————————————————————————————————————
    public void Alchemy_AlchemySet()
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        _Object_CreatureUnit QuickSave_Player_Script = _World_Manager._Object_Manager._Object_Player_Script;
        string QuickSave_Type_String = "";
        List < _Item_WeaponUnit> QuickSave_Weapons_ScriptsList = null;
        List < _Item_ItemUnit> QuickSave_Items_ScriptsList = null;
        List < _Item_ConceptUnit> QuickSave_Concepts_ScriptsList = null;
        List<_Item_MaterialUnit> QuickSave_Materials_ScriptsList = null;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        int QuickSave_Quantity_Int = 
            _Item_Manager.AlchemyQuantity(_Alchemy_Material_ScriptsArray, Alchemy_OnRecipe_Script);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Alchemy_OnRecipe_Script._Basic_Type_String)
        {
            case "Weapon":
                {
                    for (int a = 0; a < Alchemy_OnRecipe_Script._Basic_ObjectData_Class.Process.Count; a++)
                    {
                        if(!_Item_Manager.AlchemySuccessCheck(
                            Alchemy_Processes_PairArray[a],
                            Alchemy_OnRecipe_Script._Basic_ObjectData_Class.Process[a],
                            _Alchemy_Material_ScriptsArray))
                        {
                            QuickSave_Materials_ScriptsList = _Item_Manager.MaterialStartSet(
                                QuickSave_Player_Script,
                                Alchemy_OnRecipe_Script._Basic_ObjectData_Class.FailedTarget, QuickSave_Quantity_Int, true,
                                Alchemy_OnRecipe_Script._Basic_ObjectData_Class, _Alchemy_Material_ScriptsArray, Alchemy_Processes_PairArray);
                            QuickSave_Type_String = "Materials";
                            _UI_Manager._UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = "Inventory_Materials";
                            _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                            goto End;
                        }
                    }

                    QuickSave_Weapons_ScriptsList = _Item_Manager.WeaponStartSet(
                        QuickSave_Player_Script,
                        Alchemy_OnRecipe_Script._Basic_Key_String, QuickSave_Quantity_Int, true,
                        Alchemy_OnRecipe_Script._Basic_ObjectData_Class, _Alchemy_Material_ScriptsArray, Alchemy_Processes_PairArray);
                    QuickSave_Type_String = "Weapons";
                    _UI_Manager._UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = "Inventory_Weapons";
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                }
                break;
            case "Item":
                {
                    for (int a = 0; a < Alchemy_OnRecipe_Script._Basic_ObjectData_Class.Process.Count; a++)
                    {
                        if (!_Item_Manager.AlchemySuccessCheck(
                            Alchemy_Processes_PairArray[a],
                            Alchemy_OnRecipe_Script._Basic_ObjectData_Class.Process[a],
                            _Alchemy_Material_ScriptsArray))
                        {
                            QuickSave_Materials_ScriptsList = _Item_Manager.MaterialStartSet(
                                QuickSave_Player_Script,
                                Alchemy_OnRecipe_Script._Basic_ObjectData_Class.FailedTarget, QuickSave_Quantity_Int, true,
                                Alchemy_OnRecipe_Script._Basic_ObjectData_Class, _Alchemy_Material_ScriptsArray, Alchemy_Processes_PairArray);
                            QuickSave_Type_String = "Materials";
                            _UI_Manager._UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = "Inventory_Materials";
                            _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                            goto End;
                        }
                    }

                    QuickSave_Items_ScriptsList = _Item_Manager.ItemStartSet(
                        QuickSave_Player_Script,
                        Alchemy_OnRecipe_Script._Basic_Key_String, QuickSave_Quantity_Int, true,
                        Alchemy_OnRecipe_Script._Basic_ObjectData_Class, _Alchemy_Material_ScriptsArray, Alchemy_Processes_PairArray);
                    QuickSave_Type_String = "Items";
                    _UI_Manager._UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = "Inventory_Items";
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                }
                break;
            case "Concept":
                {
                    for (int a = 0; a < Alchemy_OnRecipe_Script._Basic_ConceptData_Class.Process.Count; a++)
                    {
                        if (!_Item_Manager.AlchemySuccessCheck(
                            Alchemy_Processes_PairArray[a],
                            Alchemy_OnRecipe_Script._Basic_ConceptData_Class.Process[a],
                            _Alchemy_Material_ScriptsArray))
                        {
                            QuickSave_Materials_ScriptsList = _Item_Manager.MaterialStartSet(
                                QuickSave_Player_Script,
                                Alchemy_OnRecipe_Script._Basic_ConceptData_Class.FailedTarget, QuickSave_Quantity_Int, true,
                                Alchemy_OnRecipe_Script._Basic_ConceptData_Class, _Alchemy_Material_ScriptsArray, Alchemy_Processes_PairArray);
                            QuickSave_Type_String = "Materials";
                            _UI_Manager._UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = "Inventory_Materials";
                            _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                            goto End;
                        }
                    }

                    QuickSave_Concepts_ScriptsList = _Item_Manager.ConceptStartSet(
                        QuickSave_Player_Script,
                        Alchemy_OnRecipe_Script._Basic_Key_String, QuickSave_Quantity_Int, true,
                        Alchemy_OnRecipe_Script._Basic_ConceptData_Class, _Alchemy_Material_ScriptsArray, Alchemy_Processes_PairArray);
                    QuickSave_Type_String = "Concepts";
                    _UI_Manager._UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = "Inventory_Concepts";
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                }
                break;
            case "Material":
                {
                    for (int a = 0; a < Alchemy_OnRecipe_Script._Basic_MaterialData_Class.Process.Count; a++)
                    {
                        if (!_Item_Manager.AlchemySuccessCheck(
                            Alchemy_Processes_PairArray[a],
                            Alchemy_OnRecipe_Script._Basic_MaterialData_Class.Process[a],
                            _Alchemy_Material_ScriptsArray))
                        {
                            QuickSave_Materials_ScriptsList = _Item_Manager.MaterialStartSet(
                                QuickSave_Player_Script,
                                Alchemy_OnRecipe_Script._Basic_MaterialData_Class.FailedTarget, QuickSave_Quantity_Int, true,
                                Alchemy_OnRecipe_Script._Basic_MaterialData_Class, _Alchemy_Material_ScriptsArray, Alchemy_Processes_PairArray);
                            QuickSave_Type_String = "Materials";
                            _UI_Manager._UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = "Inventory_Materials";
                            _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                            goto End;
                        }
                    }

                    QuickSave_Materials_ScriptsList = _Item_Manager.MaterialStartSet(
                        QuickSave_Player_Script,
                        Alchemy_OnRecipe_Script._Basic_Key_String.Replace("Recipe", ""), QuickSave_Quantity_Int, true,
                        Alchemy_OnRecipe_Script._Basic_MaterialData_Class, _Alchemy_Material_ScriptsArray, Alchemy_Processes_PairArray);
                    QuickSave_Type_String = "Materials";
                    _UI_Manager._UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = "Inventory_Materials";
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                }
                break;
        }
        End:
        Alchemy_OnRecipe_Script._View_Hint_Script.HintSet("UnUsing", "Recipe");
        Alchemy_OnRecipe_Script = null;

        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _UI_Manager.UISet("Alchemy_Alchemy");
        _UI_Manager.UISet("Alchemy_Recipe");
        _UI_Manager.UISet("Alchemy");
        _UI_Manager.UISet("Inventory");

        _UI_Manager.CampClass _UI_Camp_Class = _World_Manager._UI_Manager._UI_Camp_Class;
        /*
        switch (QuickSave_Type_String)
        {
            case "Weapons":
                _UI_Camp_Class._View_ItemInfo.ItemInfoSet(QuickSave_Weapons_Script);
                _UI_Camp_Class._View_TinyMenu.TinyMenuSet(QuickSave_Weapons_Script, "Get_" + QuickSave_Type_String);
                break;
            case "Items":
                _UI_Camp_Class._View_ItemInfo.ItemInfoSet(QuickSave_Items_Script);
                _UI_Camp_Class._View_TinyMenu.TinyMenuSet(QuickSave_Items_Script, "Get_" + QuickSave_Type_String);
                break;
            case "Concepts":
                _UI_Camp_Class._View_ItemInfo.ItemInfoSet(QuickSave_Concepts_Script);
                _UI_Camp_Class._View_TinyMenu.TinyMenuSet(QuickSave_Concepts_Script, "Get_" + QuickSave_Type_String);
                break;
            case "Materials":
                _UI_Camp_Class._View_ItemInfo.ItemInfoSet(QuickSave_Materials_Script);
                _UI_Camp_Class._View_TinyMenu.TinyMenuSet(QuickSave_Materials_Script, "Get_" + QuickSave_Type_String);
                break;
        }
        */

        _UI_Manager.TextEffectDictionarySet("CampMenu", _UI_Manager._UI_Camp_Class.BTNsEffect[2]);
        _UI_Manager.TextEffectDictionarySet("CampAlchemyInfo", null);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public bool MaterialsCheck()
    {
        int QuickSave_Count_Int = 0;
        for (int a = 0; a < _Alchemy_Material_ScriptsArray.Length; a++)
        {
            if (_Alchemy_Material_ScriptsArray[a] != null)
            {
                QuickSave_Count_Int++;
            }
        }
        switch (Alchemy_OnRecipe_Script._Basic_Type_String)
        {
            case "Weapon":
            case "Item":
                if (QuickSave_Count_Int != Alchemy_OnRecipe_Script._Basic_ObjectData_Class.Recipe.Count)
                {
                    print("No Match Materials");
                    return false;
                }
                break;
            case "Concept":
                if (QuickSave_Count_Int != Alchemy_OnRecipe_Script._Basic_ConceptData_Class.Recipe.Count)
                {
                    print("No Match Materials");
                    return false;
                }
                break;
            case "Material":
                if (QuickSave_Count_Int != Alchemy_OnRecipe_Script._Basic_MaterialData_Class.Recipe.Count)
                {
                    print("No Match Materials");
                    return false;
                }
                break;
        }
        return true;
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
}
