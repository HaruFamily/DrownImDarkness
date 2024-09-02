using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _View_BubbleUnit : MonoBehaviour
{
    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————
    //放置區----------------------------------------------------------------------------------------------------
    //名稱
    public Text _Bubble_Name_Text;
    //圖片
    public Image _Bubble_MainImage_Image;
    //內文
    public TextMeshProUGUI _Bubble_Summary_Text;
    //類型
    public Text _Bubble_Type_Text;
    //註解
    public Text _Bubble_Commont_Text;

    //邊框目標
    public RectTransform _Bubble_WidthTarget_Transform;
    //整理邊框
    public List<RectTransform> _Bubble_WidthSet_TransformList;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion ElementBox

    #region ViewSet
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void BubbleSet(SourceClass Source, string Key, LanguageClass LanguageClass)
    {
        //----------------------------------------------------------------------------------------------------
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        string QuickSave_Name_String = "";
        string QuickSave_Type_String = "";
        string QuickSave_Summary_String = "";
        //----------------------------------------------------------------------------------------------------

        //設置----------------------------------------------------------------------------------------------------
        switch (Source.SourceType)
        {
            case "UIName":
                {
                    QuickSave_Name_String = _World_TextManager._Language_UIName_Dictionary[Key];
                }
                break;

            case "UISummary":
                {
                    QuickSave_Name_String = _World_TextManager._Language_UIName_Dictionary[Key];
                    QuickSave_Summary_String = _World_TextManager.
                        TextmeshProTranslater("Default",
                        _World_TextManager._Language_UISummary_Dictionary[Key], 0,
                        Source, null, null, 
                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                }
                break;

            case "Tag":
                {
                    if (_World_TextManager._Language_Tag_Dictionary.TryGetValue(Key, out LanguageClass Value))
                    {
                        QuickSave_Name_String = Value.Name;
                        QuickSave_Summary_String = _World_TextManager.
                            TextmeshProTranslater("Default", Value.Summary, 0,
                            Source, null, null,
                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                    else if(_World_Manager._Skill_Manager._Language_Faction_Dictionary.TryGetValue(
                        "Faction_" + Key + "_0", out LanguageClass Language))
                    {
                        QuickSave_Name_String = Language.Name;
                        QuickSave_Summary_String = _World_TextManager.
                            TextmeshProTranslater(
                            "Default", Language.Summary, 0,
                            Source, null, null,
                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int); 
                    }
                    else
                    {
                        QuickSave_Name_String = Key;
                    }
                }
                break;
            case "SpecialAffix":
                {
                    QuickSave_Name_String = Key;
                    QuickSave_Summary_String = "";
                    if (LanguageClass == null)
                    {
                        if (_World_Manager._Item_Manager._Language_SpecialAffix_Dictionary.TryGetValue(Key, out LanguageClass Language))
                        {
                            QuickSave_Name_String = Language.Name;
                            QuickSave_Summary_String = Language.Summary;
                        }
                    }
                    else
                    {
                        QuickSave_Name_String = LanguageClass.Name;
                        QuickSave_Summary_String = LanguageClass.Summary;
                    }
                    QuickSave_Summary_String = _World_TextManager.
                        TextmeshProTranslater(
                        "Default", QuickSave_Summary_String, 20,
                            Source, null, null,
                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                }
                break;

            #region - Language有輸入↓ -
            case "Passive":
                {
                    QuickSave_Name_String = (LanguageClass.Name);
                    QuickSave_Summary_String = _World_TextManager.
                        TextmeshProTranslater(
                        "Default", LanguageClass.Summary, 20,
                            Source, null, null,
                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                }
                break;

            case "EffectObject":
                {
                    _Effect_EffectObjectUnit QuickSave_EffectObject_Script = Source.Source_EffectObject;
                    //設置內容
                    int QuickSave_StackLimit_Int = QuickSave_EffectObject_Script.Key_StackLimit();
                    string QuickSave_StackView_String = "";
                    if (QuickSave_StackLimit_Int != 65535)
                    {
                        QuickSave_StackView_String =
                            QuickSave_EffectObject_Script.Key_Stack("Default", QuickSave_EffectObject_Script._Basic_Key_String, null, null, null) +
                            "<size=90>/" + QuickSave_StackLimit_Int.ToString() + "</size>";
                    }
                    else
                    {
                        QuickSave_StackView_String =
                            QuickSave_EffectObject_Script.Key_Stack("Default", QuickSave_EffectObject_Script._Basic_Key_String, null, null, null).ToString();
                    }
                    string QuickSave_DecayView_String =  "";
                    switch (QuickSave_EffectObject_Script._Basic_Data_Class.Decay)
                    {
                        case "Sequence":
                        case "Round":
                            {
                                QuickSave_DecayView_String =
                                    QuickSave_EffectObject_Script._Effect_Decay_Int +
                                    "<size=60>/" + (QuickSave_EffectObject_Script._Basic_Data_Class.DecayTimes) + "</size>";
                            }
                            break;
                    }

                    QuickSave_Name_String = LanguageClass.Name + "*" + QuickSave_StackView_String;
                    QuickSave_Type_String =
                        _World_TextManager._Language_UIName_Dictionary[
                            "Decay_" + QuickSave_EffectObject_Script._Basic_Data_Class.Decay] + QuickSave_DecayView_String;
                    QuickSave_Summary_String = _World_TextManager.
                        TextmeshProTranslater(
                        "Default", LanguageClass.Summary, 20,
                            Source, null, null,
                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                }
                break;

            case "EffectCard":
                {
                    _Effect_EffectCardUnit QuickSave_EffectCard_Script = Source.Source_EffectCard;
                    //設置名稱
                    //設置內容
                    int QuickSave_StackLimit_Int = QuickSave_EffectCard_Script._Basic_Data_Class.StackLimit;
                    string QuickSave_StackView_String = "";
                    if (QuickSave_StackLimit_Int != 65535)
                    {
                        QuickSave_StackView_String = QuickSave_EffectCard_Script._Effect_Stack_Int +
                            "<size=90>/" + QuickSave_StackLimit_Int.ToString() + "</size>";
                    }
                    else
                    {
                        QuickSave_StackView_String = QuickSave_EffectCard_Script._Effect_Stack_Int.ToString();
                    }
                    QuickSave_Name_String = LanguageClass.Name + "*" + QuickSave_StackView_String;
                    QuickSave_Summary_String = _World_TextManager.
                        TextmeshProTranslater(
                        "Default", LanguageClass.Summary, 20,
                            Source, null, null,
                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                }
                break;
            case "Syndrome":
                if (Source.Source_Syndrome != null)
                {
                    if (LanguageClass == null)
                    {
                        print("Wrong");
                        break;
                    }
                    _View_Manager _View_Manager = _World_Manager._View_Manager;
                    _Item_SyndromeUnit QuickSave_Syndrome_Script = Source.Source_Syndrome;
                    Color QuickSave_Color_Color;
                    List<sbyte> QuickSave_Rank_intList = QuickSave_Syndrome_Script._Basic_Data_Class.Rank;
                    switch (QuickSave_Syndrome_Script._Syndrome_Rank_Int)
                    {
                        case 0:
                            QuickSave_Name_String = LanguageClass.Name;
                            break;
                        case 1:
                            QuickSave_Color_Color = _View_Manager.GetColor("Rare", "★");
                            QuickSave_Name_String =
                                "<color=#" + ColorUtility.ToHtmlStringRGB(QuickSave_Color_Color) + "><b>" + "★" + "</b></color>" +
                                LanguageClass.Name;
                            break;
                        case 2:
                            QuickSave_Color_Color = _View_Manager.GetColor("Rare", "★★");
                            QuickSave_Name_String =
                                "<color=#" + ColorUtility.ToHtmlStringRGB(QuickSave_Color_Color) + "><b>" + "★★" + "</b></color>" +
                                LanguageClass.Name;
                            break;
                        case 3:
                            QuickSave_Color_Color = _View_Manager.GetColor("Rare", "★★★");
                            QuickSave_Name_String =
                                "<color=#" + ColorUtility.ToHtmlStringRGB(QuickSave_Color_Color) + "><b>" + "★★★" + "</b></color>" +
                                LanguageClass.Name;
                            break;
                        case 4:
                            QuickSave_Color_Color = _View_Manager.GetColor("Rare", "★★★★");
                            QuickSave_Name_String +=
                                "<color=#" + ColorUtility.ToHtmlStringRGB(QuickSave_Color_Color) + "><b>" + "★" + "</b></color>";
                            QuickSave_Color_Color = _View_Manager.GetColor("Rare", "★★★");
                            QuickSave_Name_String +=
                                "<color=#" + ColorUtility.ToHtmlStringRGB(QuickSave_Color_Color) + "><b>" + "★★★" + "</b></color>" +
                                LanguageClass.Name;
                            break;
                    }
                    QuickSave_Summary_String = _World_TextManager.
                        TextmeshProTranslater(
                        "Default", LanguageClass.Summary, 20,
                            Source, null, null,
                            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    QuickSave_Type_String = "";
                    for (int a = 0; a < QuickSave_Rank_intList[QuickSave_Rank_intList.Count - 1]; a++)
                    {
                        if (a < Source.Source_Syndrome._Syndrome_Stack_Int)
                        {
                            QuickSave_Type_String += "◆";
                        }
                        else
                        {
                            QuickSave_Type_String += "◇";
                        }
                    }
                }
                break;
                #endregion
        }
        //設置
        BubbleName(QuickSave_Name_String);
        BubbleType(QuickSave_Type_String);
        BubbleSummary(QuickSave_Summary_String);
        //寬度變更
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_Bubble_Summary_Text.transform);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)this.transform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_Bubble_WidthTarget_Transform);
        for (int a = 0; a < _Bubble_WidthSet_TransformList.Count; a++)
        {
            _Bubble_WidthSet_TransformList[a].sizeDelta = new Vector2(_Bubble_WidthTarget_Transform.rect.width-10, _Bubble_WidthSet_TransformList[a].rect.height);
        }

        //重整狀態
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)this.transform);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    private void BubbleName(string Key)
    {
        if (Key != "")
        {
            _Bubble_Name_Text.text = Key;
            _Bubble_Name_Text.gameObject.SetActive(true);
        }
        else
        {
            _Bubble_Name_Text.text = "";
            _Bubble_Name_Text.gameObject.SetActive(false);
        }
    }
    private void BubbleSummary(string Key)
    {
        if (Key != "")
        {
            _Bubble_Summary_Text.text = Key;
            _Bubble_Summary_Text.gameObject.SetActive(true);
        }
        else
        {
            _Bubble_Summary_Text.text = "";
            _Bubble_Summary_Text.gameObject.SetActive(false);
        }
    }
    private void BubbleType(string Key)
    {
        if (Key != "")
        {
            _Bubble_Type_Text.text = Key;
            _Bubble_Type_Text.gameObject.SetActive(true);
        }
        else
        {
            _Bubble_Type_Text.text = "";
            _Bubble_Type_Text.gameObject.SetActive(false);
        }
    }

    //——————————————————————————————————————————————————————————————————————
    #endregion ViewSet
}
