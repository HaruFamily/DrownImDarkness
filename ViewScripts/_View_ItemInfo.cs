using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using TMPro;

public class _View_ItemInfo : MonoBehaviour
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
    public Image _View_StatusPolygonImage_Image;

    public List<Text> _View_MaterialStatusValue_TextList = new List<Text>();

    public List<Image> _View_TagImage_ImageList = new List<Image>();


    public Transform _View_SpecialAffixStore_Transform;
    public List<Text> _View_SpecialAffix_TextList = new List<Text>();

    private List<SourceClass> _View_ItemInfoSortSource_ClassList = new List<SourceClass>();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————


    #region - Void -
    //獲得時顯示(紀錄複數獲得)//一次輸入一個
    public void ItemInfoSortAdd(SourceClass Source)
    {
        //----------------------------------------------------------------------------------------------------
        _View_ItemInfoSortSource_ClassList.Add(Source);
        //----------------------------------------------------------------------------------------------------
    }
    public void ItemInfoSortReset()
    {
        //----------------------------------------------------------------------------------------------------
        _View_ItemInfoSortSource_ClassList.Clear();
        //----------------------------------------------------------------------------------------------------
    }
    public void ItemInfoShow()
    {
        //----------------------------------------------------------------------------------------------------
        if (_View_ItemInfoSortSource_ClassList.Count == 0)
        {
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _View_TinyMenu _View_TinyMenu = _World_Manager._UI_Manager._UI_Camp_Class._View_TinyMenu;
        _View_TinyMenu.
            TinyMenuSet("Get_Inventory", _View_ItemInfoSortSource_ClassList);
        //----------------------------------------------------------------------------------------------------
    }


    #region - Object -
    //——————————————————————————————————————————————————————————————————————
    public void ItemInfoSet(_Map_BattleObjectUnit Target)
    {
        //變數----------------------------------------------------------------------------------------------------
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        string QuickSave_Type_String = Target._Basic_Source_Class.SourceType;
        //----------------------------------------------------------------------------------------------------

        //設置----------------------------------------------------------------------------------------------------
        //提示
        _UI_HintEffect QuickSave_Hint_Script = null;
        Color QuickSave_Color_Color = Color.white;
        switch (QuickSave_Type_String)
        {
            case "Concept":
                QuickSave_Hint_Script = Target._Basic_Source_Class.Source_Concept._View_Hint_Script;
                break;
            case "Weapon":
                QuickSave_Hint_Script = Target._Basic_Source_Class.Source_Weapon._View_Hint_Script;
                QuickSave_Color_Color = Target._Basic_Source_Class.Source_Weapon._Basic_Color_Color;
                break;
            case "Item":
                QuickSave_Hint_Script = Target._Basic_Source_Class.Source_Item._View_Hint_Script;
                QuickSave_Color_Color = Target._Basic_Source_Class.Source_Item._Basic_Color_Color;
                break;
            case "Material":
                QuickSave_Hint_Script = Target._Basic_Source_Class.Source_Material._View_Hint_Script;
                break;
        }
        if (_UI_Manager._UI_Hint_Dictionary["New"][QuickSave_Type_String].Contains(QuickSave_Hint_Script) &&
            !_UI_Manager._UI_Hint_Dictionary["Using"][QuickSave_Type_String].Contains(QuickSave_Hint_Script))
        {
            QuickSave_Hint_Script.HintSet("null", QuickSave_Type_String);
            QuickSave_Hint_Script.HintSet("UnNew", QuickSave_Type_String);
        }
        //設置
        _UI_Manager._UI_Camp_Class.SummaryTransforms[13].gameObject.SetActive(true);

        _View_Name_Text.text = Target._Basic_Language_Class.Name;
        _View_Image_Image.sprite = 
            _View_Manager.GetSprite(QuickSave_Type_String, "Info", Target._Basic_Key_String);
        int QuickSave_Size_Int = 
            Mathf.RoundToInt(Target.Key_Material("Size", Target._Basic_Source_Class, null));
        int QuickSave_Form_Int = 
            Mathf.RoundToInt(Target.Key_Material("Form", Target._Basic_Source_Class, null));
        int QuickSave_Weight_Int = 
            Mathf.RoundToInt(Target.Key_Material("Weight", Target._Basic_Source_Class, null));
        int QuickSave_Purity_Int = 
            Mathf.RoundToInt(Target.Key_Material("Purity", Target._Basic_Source_Class, null));
        switch (QuickSave_Type_String)
        {
            case "Concept":
                {
                    _View_HardBack_Image.sprite = _View_BackSprite_SpriteArray[1];
                    _View_TopBack_Image.sprite = _View_BackSprite_SpriteArray[3];
                    _View_InfoType_Transfome[0].gameObject.SetActive(false);
                    _View_InfoType_Transfome[1].gameObject.SetActive(true);

                    _View_ConceptMaterialStatusValue_TextList[0].text = 
                        QuickSave_Size_Int.ToString("0");
                    _View_ConceptMaterialStatusValue_TextList[1].text = 
                        QuickSave_Form_Int.ToString("0");
                    _View_ConceptMaterialStatusValue_TextList[2].text = 
                        QuickSave_Weight_Int.ToString("0");
                    _View_ConceptMaterialStatusValue_TextList[3].text = 
                        QuickSave_Purity_Int.ToString("0");

                    _View_ConceptStatus_TextList[0].text = 
                        Target.Key_Status("Medium", Target._Basic_Source_Class, null, null).ToString("0");
                    _View_ConceptStatus_TextList[1].text = 
                        Target.Key_Status("Catalyst", Target._Basic_Source_Class, null, null).ToString("0");
                    _View_ConceptStatus_TextList[2].text = 
                        Target.Key_Status("Consciousness", Target._Basic_Source_Class, null, null).ToString("0");
                    _View_ConceptStatus_TextList[3].text = 
                        Target.Key_Status("Vitality", Target._Basic_Source_Class, null, null).ToString("0");
                    _View_ConceptStatus_TextList[4].text = 
                        Target.Key_Status("Strength", Target._Basic_Source_Class, null, null).ToString("0");
                    _View_ConceptStatus_TextList[5].text = 
                        Target.Key_Status("Precision", Target._Basic_Source_Class, null, null).ToString("0");
                    _View_ConceptStatus_TextList[6].text = 
                        Target.Key_Status("Speed", Target._Basic_Source_Class, null, null).ToString("0");
                    _View_ConceptStatus_TextList[7].text = 
                        Target.Key_Status("Luck", Target._Basic_Source_Class, null, null).ToString("0");

                    for (int a = 0; a < 8; a++)
                    {
                        _View_ConceptStatusImage_ImageList[a].sprite = _View_ConceptStatusSprite_SpriteList[a];
                        _View_ConceptStatusImage_ImageList[a].raycastTarget = true;
                    }
                    _View_ConceptStatusImage_ImageList[0].gameObject.name = 
                        "Bubble_UISummary_Medium";
                    _View_ConceptStatusImage_ImageList[1].gameObject.name = 
                        "Bubble_UISummary_Catalyst";
                    _View_ConceptStatusImage_ImageList[2].gameObject.name = 
                        "Bubble_UISummary_Consciousness";
                    _View_ConceptStatusImage_ImageList[3].gameObject.name = 
                        "Bubble_UISummary_Vitality";
                    _View_ConceptStatusImage_ImageList[4].gameObject.name = 
                        "Bubble_UISummary_Strength";
                    _View_ConceptStatusImage_ImageList[5].gameObject.name = 
                        "Bubble_UISummary_Precision";
                    _View_ConceptStatusImage_ImageList[6].gameObject.name = 
                        "Bubble_UISummary_Speed";
                    _View_ConceptStatusImage_ImageList[7].gameObject.name = 
                        "Bubble_UISummary_Luck";
                }
                break;
            case "Weapon":
            case "Item":
            case "Material":
                {
                    _View_HardBack_Image.sprite = _View_BackSprite_SpriteArray[0];
                    _View_TopBack_Image.sprite = _View_BackSprite_SpriteArray[2];
                    _View_InfoType_Transfome[0].gameObject.SetActive(true);
                    _View_InfoType_Transfome[1].gameObject.SetActive(false);

                    _View_StatusPolygon_Scrtips.VerticesDistances[3] = 
                        Mathf.Clamp((float)QuickSave_Size_Int / 25, 0, 1);
                    _View_StatusPolygon_Scrtips.VerticesDistances[0] = 
                        Mathf.Clamp((float)QuickSave_Form_Int / 25, 0, 1);
                    _View_StatusPolygon_Scrtips.VerticesDistances[2] = 
                        Mathf.Clamp((float)QuickSave_Weight_Int / 25, 0, 1);
                    _View_StatusPolygon_Scrtips.VerticesDistances[1] = 
                        Mathf.Clamp((float)QuickSave_Purity_Int / 25, 0, 1);
                    _View_StatusPolygonImage_Image.color = QuickSave_Color_Color;
                    StartCoroutine(_View_Manager.EquipStatusPolygonAnimate(_View_StatusPolygon_Scrtips));

                    _View_MaterialStatusValue_TextList[0].text = 
                        QuickSave_Size_Int.ToString("0");
                    _View_MaterialStatusValue_TextList[1].text = 
                        QuickSave_Form_Int.ToString("0");
                    _View_MaterialStatusValue_TextList[2].text = 
                        QuickSave_Weight_Int.ToString("0");
                    _View_MaterialStatusValue_TextList[3].text = 
                        QuickSave_Purity_Int.ToString("0");

                    List<string> QuickSave_Tags_StringList = 
                        Target.Key_Tag(Target._Basic_Source_Class, null);
                    for (int a = 0; a < _View_TagImage_ImageList.Count; a++)
                    {
                        if (a < QuickSave_Tags_StringList.Count)
                        {
                            _View_TagImage_ImageList[a].raycastTarget = true;
                            _View_TagImage_ImageList[a].sprite = 
                                _View_Manager.GetSprite("Icon", "Tag", QuickSave_Tags_StringList[a]);
                        }
                        else
                        {
                            _View_TagImage_ImageList[a].raycastTarget = false;
                            _View_TagImage_ImageList[a].sprite = 
                                _View_Manager.GetSprite("null", "null");
                        }
                    }
                }
                break;
        }
        for (int a = 0; a < _View_SpecialAffix_TextList.Count; a++)
        {
            string QuickSave_SpecialAffixKey_String = 
                Target._Basic_Material_Class.SpecialAffix[a];
            if (QuickSave_SpecialAffixKey_String == null || 
                QuickSave_SpecialAffixKey_String == "")
            {
                _View_SpecialAffix_TextList[a].text = "";
            }
            else
            {
                _View_SpecialAffix_TextList[a].text = 
                    _Item_Manager._Language_SpecialAffix_Dictionary[QuickSave_SpecialAffixKey_String].Name;
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region Out    
     public void ItemInfoOut()
     {
         //----------------------------------------------------------------------------------------------------
         _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
         _View_TinyMenu _View_TinyMenu = _UI_Manager._UI_Camp_Class._View_TinyMenu;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (!_View_TinyMenu.Select_OnSelect_Bool)
        {
            _View_TinyMenu.TinyMenuOut();
            _UI_Manager._UI_Camp_Class.SummaryTransforms[13].gameObject.SetActive(false);
        }
        else
        {
            ItemInfoSet(_View_TinyMenu.Select_Source_Class.Source_BattleObject);
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #endregion
}
