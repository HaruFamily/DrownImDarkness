using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _UI_SkillView : MonoBehaviour
{
    //
    //----------------------------------------------------------------------------------------------------
    public Text _SkillView_Name_Text;

    public Image _SkillView_Image_Image;

    public List<Text> _SkillView_VariableKey_TextList = new List<Text>();
    public List<Text> _SkillView_VariableValue_TextList = new List<Text>();

    public TextMeshProUGUI _SkillView_Summary_Text;

    public _UI_Manager.UIBarClass _SkillView_Expertise_Class;

    public Text _SkillView_LeftTagTitle_Text;
    public List<Text> _SkillView_LeftTag_TextList = new List<Text>();

    public Text _SkillView_RightTagTitle_Text;
    public List<Text> _SkillView_RightTag_TextList = new List<Text>();
    //----------------------------------------------------------------------------------------------------
    //


    #region - Set -
    /*
    //
    public void SkillInfoSet(_Skill_PassiveUnit Target)
    {
        //跑计----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        //----------------------------------------------------------------------------------------------------

        //砞竚----------------------------------------------------------------------------------------------------
        _UI_Manager._UI_Camp_Class.SummaryTransforms[14].gameObject.SetActive(true);

        _SkillView_Name_Text.text = Target._View_Name_Text.text;

        _SkillView_Image_Image.sprite = _World_Manager._View_Manager.GetSprite("Passive","Info", Target._Basic_Key_String);

        for (int a = 0; a < 5; a++)
        {
            _SkillView_VariableKey_TextList[a].text = "";
            _SkillView_VariableValue_TextList[a].text = "";
        }

        _SkillView_Summary_Text.text =
        _World_TextManager.
            TextmeshProTranslater(
            "Data", Target._Basic_Language_Class.Summary,
            new SourceClass
            {
                SourceType = "NumbersData",
                Source_NumbersData = Target._Basic_Data_Class.Numbers,
                Source_KeysData = Target._Basic_Data_Class.Keys
            }, null ,null);

        _SkillView_Expertise_Class.MainTransform.gameObject.SetActive(false);

        _SkillView_LeftTagTitle_Text.text = "";
        for (int a = 0; a < 10; a++)
        {
            _SkillView_LeftTag_TextList[a].text = "";
        }

        _SkillView_RightTagTitle_Text.text = "";
        for (int a = 0; a < 10; a++)
        {
            _SkillView_RightTag_TextList[a].text = "";
        }
        //----------------------------------------------------------------------------------------------------
    }*/
    //

    //
    public void SkillInfoSet(_Skill_ExploreUnit Target)
    {
        //跑计----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        //----------------------------------------------------------------------------------------------------

        //砞竚----------------------------------------------------------------------------------------------------
        _UI_Manager._UI_Camp_Class.SummaryTransforms[14].gameObject.SetActive(true);

        _SkillView_Image_Image.sprite = _World_Manager._View_Manager.GetSprite("Explore", "Info", Target._Basic_Key_String);

        _SkillView_VariableKey_TextList[0].text = _World_TextManager._Language_UIName_Dictionary["Depletion"];
        /*
        switch (Target._Owner_Faction_Script._Basic_Data_Class.Type)
        {
            case "Creature":
                _SkillView_VariableValue_TextList[0].text = Target._Basic_Data_Class.Depletion.ToString("0");
                break;
            case "Weapon":
                _SkillView_VariableValue_TextList[0].text = (Target._Basic_Data_Class.Depletion * 100).ToString("0") + "%";
                break;
        }*/
        for (int a = 0; a < 5; a++)
        {
            _SkillView_VariableKey_TextList[a].text = "";
            _SkillView_VariableValue_TextList[a].text = "";
        }

        _SkillView_Summary_Text.text =
        _World_TextManager.
            TextmeshProTranslater(
            "Data", Target._Basic_Language_Class.Summary, 0,
            new SourceClass
            {
                SourceType = "NumbersData",
                Source_NumbersData = Target._Basic_Data_Class.Numbers,
                Source_KeysData = Target._Basic_Data_Class.Keys
            }, null, null, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        /*
        _SkillView_Expertise_Class.MainTransform.gameObject.SetActive(true);
        NumbericalValueClass QuickSave_Number_Class = Target._Explore_Expertise_Class;
        float QuickSave_Expertise_Float = QuickSave_Number_Class.Point / QuickSave_Number_Class.Total();
        _SkillView_Expertise_Class.BarValue.text = QuickSave_Number_Class.Point.ToString("0") + "<size=80>" + "/" + QuickSave_Number_Class.Total().ToString("0") + "</size>";
        _SkillView_Expertise_Class.BarTransform.localScale = new Vector3(QuickSave_Expertise_Float, 1, 1);
        */
        _SkillView_LeftTagTitle_Text.text = "";
        for (int a = 0; a < 10; a++)
        {
            _SkillView_LeftTag_TextList[a].text = "";
        }

        _SkillView_RightTagTitle_Text.text = "";
        for (int a = 0; a < 10; a++)
        {
            _SkillView_RightTag_TextList[a].text = "";
        }
        //----------------------------------------------------------------------------------------------------
    }
    //

    //
    public void SkillInfoSet(_Skill_BehaviorUnit Target)
    {
        //跑计----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        //----------------------------------------------------------------------------------------------------

        //砞竚----------------------------------------------------------------------------------------------------
        _UI_Manager._UI_Camp_Class.SummaryTransforms[14].gameObject.SetActive(true);

        _SkillView_Image_Image.sprite = _World_Manager._View_Manager.GetSprite("Behavior", "Info", Target._Basic_Key_String);

        _SkillView_VariableKey_TextList[0].text = _World_TextManager._Language_UIName_Dictionary["Depletion"];
        /*
        switch (Target._Owner_Faction_Script._Basic_Data_Class.Type)
        {
            case "Creature":
                _SkillView_VariableValue_TextList[0].text = Target._Basic_Data_Class.Depletion.ToString("0");
                break;
            case "Weapon":
                _SkillView_VariableValue_TextList[0].text = (Target._Basic_Data_Class.Depletion * 100).ToString("0") + "%";
                break;
        }*/
        _SkillView_VariableKey_TextList[1].text = _World_TextManager._Language_UIName_Dictionary["Delay"];
        string QuickSave_Variable01_String = "";
        for (int a = 0; a < Target._Basic_Data_Class.DelayBefore; a++)
        {
            QuickSave_Variable01_String += "』";
        }
        QuickSave_Variable01_String += ",";
        for (int a = 0; a < Target._Basic_Data_Class.DelayAfter; a++)
        {
            QuickSave_Variable01_String += "『";
        }
        _SkillView_VariableValue_TextList[1].text = QuickSave_Variable01_String;
        _SkillView_VariableKey_TextList[2].text = _World_TextManager._Language_UIName_Dictionary["EnchantGap"];
        string QuickSave_Variable02_String = "";
        for (int a = 0; a < Target._Basic_Data_Class.Enchant; a++)
        {
            QuickSave_Variable02_String += "』";
        }
        _SkillView_VariableValue_TextList[2].text = QuickSave_Variable02_String;

        for (int a = 3; a < 5; a++)
        {
            _SkillView_VariableKey_TextList[a].text = "";
            _SkillView_VariableValue_TextList[a].text = "";
        }

        _SkillView_Summary_Text.text =
        _World_TextManager.
            TextmeshProTranslater(
            "Data", Target._Basic_Language_Class.Summary, 0,
            new SourceClass
            {
                SourceType = "NumbersData",
                Source_NumbersData = Target._Basic_Data_Class.Numbers,
                Source_KeysData = Target._Basic_Data_Class.Keys
            }, null, null, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        /*
        _SkillView_Expertise_Class.MainTransform.gameObject.SetActive(true);
        NumbericalValueClass QuickSave_Number_Class = Target._Behavior_Expertise_Class;
        float QuickSave_Expertise_Float = QuickSave_Number_Class.Point / QuickSave_Number_Class.Total();
        _SkillView_Expertise_Class.BarValue.text = QuickSave_Number_Class.Point.ToString("0") + "<size=80>" + "/" + QuickSave_Number_Class.Total().ToString("0") + "</size>";
        _SkillView_Expertise_Class.BarTransform.localScale = new Vector3(QuickSave_Expertise_Float, 1, 1);
        */
        _SkillView_LeftTagTitle_Text.text = _World_TextManager._Language_UIName_Dictionary["OwnTag"];
        /*
        List<string> QuickSave_OwnTag_StringList = Target._Basic_Data_Class.OwnTag.Total();
        for (int a = 0; a < 10; a++)
        {
            if (a < QuickSave_OwnTag_StringList.Count)
            {
                //_SkillView_LeftTag_TextList[a].text = _Skill_Manager._Language_Tag_Dictionary[QuickSave_OwnTag_StringList[a]].Name;
            }
            else
            {
                _SkillView_LeftTag_TextList[a].text = "";
            }
        }*/

        _SkillView_RightTagTitle_Text.text = _World_TextManager._Language_UIName_Dictionary["ReactTag"];
        //List<string> QuickSave_ReactTag_StringList = Target._Basic_Data_Class.ReactTag.Total();
        /*for (int a = 0; a < 10; a++)
        {
            if (a < QuickSave_ReactTag_StringList.Count)
            {
                //_SkillView_RightTag_TextList[a].text = _Skill_Manager._Language_Tag_Dictionary[QuickSave_ReactTag_StringList[a]].Name;
            }
            else
            {
                _SkillView_RightTag_TextList[a].text = "";
            }
        }*/
        //----------------------------------------------------------------------------------------------------
    }
    //

    //
    public void SkillInfoSet(_Skill_EnchanceUnit Target)
    {
        //跑计----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        //----------------------------------------------------------------------------------------------------

        //砞竚----------------------------------------------------------------------------------------------------
        _UI_Manager._UI_Camp_Class.SummaryTransforms[14].gameObject.SetActive(true);

        _SkillView_Image_Image.sprite = _World_Manager._View_Manager.GetSprite("Enchance","Info", Target._Basic_Key_String);

        _SkillView_VariableKey_TextList[0].text = _World_TextManager._Language_UIName_Dictionary["Depletion"];
        /*
        switch (Target._Owner_Faction_Script._Basic_Data_Class.Type)
        {
            case "Creature":
                _SkillView_VariableValue_TextList[0].text = Target._Basic_Data_Class.Depletion.ToString("0");
                break;
            case "Weapon":
                _SkillView_VariableValue_TextList[0].text = (Target._Basic_Data_Class.Depletion * 100).ToString("0") + "%";
                break;
        }*/
        _SkillView_VariableKey_TextList[1].text = _World_TextManager._Language_UIName_Dictionary["EnchantSize"];
        string QuickSave_Variable02_String = "";
        for (int a = 0; a < Target._Basic_Data_Class.Enchant; a++)
        {
            QuickSave_Variable02_String += "』";
        }
        _SkillView_VariableValue_TextList[1].text = QuickSave_Variable02_String;

        for (int a = 2; a < 5; a++)
        {
            _SkillView_VariableKey_TextList[a].text = "";
            _SkillView_VariableValue_TextList[a].text = "";
        }

        _SkillView_Summary_Text.text =
        _World_TextManager.
            TextmeshProTranslater(
            "Data", Target._Basic_Language_Class.Summary, 0,
            new SourceClass
            {
                SourceType = "NumbersData",
                Source_NumbersData = Target._Basic_Data_Class.Numbers,
                Source_KeysData = Target._Basic_Data_Class.Keys
            }, null, null, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        /*
        _SkillView_Expertise_Class.MainTransform.gameObject.SetActive(true);
        NumbericalValueClass QuickSave_Number_Class = Target._Enchance_Expertise_Class;
        float QuickSave_Expertise_Float = QuickSave_Number_Class.Point / QuickSave_Number_Class.Total();
        _SkillView_Expertise_Class.BarValue.text = QuickSave_Number_Class.Point.ToString("0") + "<size=80>" + "/" + QuickSave_Number_Class.Total().ToString("0") + "</size>";
        _SkillView_Expertise_Class.BarTransform.localScale = new Vector3(QuickSave_Expertise_Float, 1, 1);
        */
        _SkillView_LeftTagTitle_Text.text = _World_TextManager._Language_UIName_Dictionary["AddenTag"];
        /*List<string> QuickSave_AddenTag_StringList = Target._Basic_Data_Class.AddenTag.Total();
        for (int a = 0; a < 10; a++)
        {
            if (a < QuickSave_AddenTag_StringList.Count)
            {
                //_SkillView_LeftTag_TextList[a].text = _Skill_Manager._Language_Tag_Dictionary[QuickSave_AddenTag_StringList[a]].Name;
            }
            else
            {
                _SkillView_LeftTag_TextList[a].text = "";
            }
        }*/

        _SkillView_RightTagTitle_Text.text = _World_TextManager._Language_UIName_Dictionary["RequiredTag"];
        /*List<string> QuickSave_RequiredTag_StringList = Target._Basic_Data_Class.RequiredTag.Total();
        for (int a = 0; a < 10; a++)
        {
            if (a < QuickSave_RequiredTag_StringList.Count)
            {
                //_SkillView_RightTag_TextList[a].text = _Skill_Manager._Language_Tag_Dictionary[QuickSave_RequiredTag_StringList[a]].Name;
            }
            else
            {
                _SkillView_RightTag_TextList[a].text = "";
            }
        }*/
        //----------------------------------------------------------------------------------------------------
    }
    //

    //
    public void SkillInfoOut()
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _View_TinyMenu _View_TinyMenu = _UI_Manager._UI_Camp_Class._View_TinyMenu;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        /*
        switch (_View_TinyMenu.Select_OnSelect_String)
        {
            case "Passive":
                //SkillInfoSet(_View_TinyMenu.Select_Passive_Script);
                break;
            case "Explore":
                SkillInfoSet(_View_TinyMenu.Select_Explore_Script);
                break;
            case "Behavior":
                SkillInfoSet(_View_TinyMenu.Select_Behavior_Script);
                break;
            case "Enchance":
                SkillInfoSet(_View_TinyMenu.Select_Enchance_Script);
                break;
            default:
                _UI_Manager._UI_Camp_Class.SummaryTransforms[14].gameObject.SetActive(false);
                break;
        }
        //----------------------------------------------------------------------------------------------------*/
    }
    //
    #endregion
}
