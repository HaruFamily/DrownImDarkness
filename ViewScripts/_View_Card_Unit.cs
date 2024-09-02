using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _View_Card_Unit : MonoBehaviour
{
    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————
    //子父層----------------------------------------------------------------------------------------------------
    public _UI_Card_Unit _Basic_Owner_Script;

    //位置
    public Transform _View_Offset_Transform;
    public Transform _View_InfoStore_Transform;
    public GameObject _View_ExploreInfo_GameObject;
    public GameObject _View_BehaviorInfo_GameObject;
    public GameObject _View_EnchanceInfo_GameObject;
    public GameObject _View_Explain_GameObject;
    //----------------------------------------------------------------------------------------------------

    //卡牌簡視----------------------------------------------------------------------------------------------------
    public Text _View_Name_Text;
    public Image _View_Image_Image;

    //探索
    public Text _View_ExploreName_Text;
    public Image _View_ExploreImage_Image;
    public TextMeshProUGUI _View_ExploreSummary_Text;

    public Text _View_BehaviorName_Text;
    public Image _View_BehaviorImage_Image;
    public Text _View_BehaviorDelayBefore_Text;
    public Text _View_BehaviorDelayAfter_Text;
    public List<Image> _View_BehaviorEnchances_ImageList = new List<Image>();
    public TextMeshProUGUI _View_BehaviorSummary_Text;

    public Text _View_EnchanceName_Text;
    public Image _View_EnchanceImage_Image;
    public Text _View_EnchanceDelayBefore_Text;
    public Text _View_EnchanceDelayAfter_Text;
    public List<Image> _View_EnchanceEnchances_ImageList = new List<Image>();
    public TextMeshProUGUI _View_EnchanceSummary_Text;

    public Text _View_Explain_Text;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion ElementBox

    #region MouseState
    #region - SimpleSet -
    //簡易(小卡片)設置——————————————————————————————————————————————————————————————————————
    public void SimpleSet(string Type)
    {
        //----------------------------------------------------------------------------------------------------
        _Map_BattleObjectUnit QuickSave_Using_Script = _Basic_Owner_Script._Card_UseObject_Script;
        if (QuickSave_Using_Script == null)
        {
            QuickSave_Using_Script = 
                _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Card_UsingObject_Script;
        }
        switch (Type)
        {
            #region - Explore -
            case "Explore":
                {
                    _Skill_ExploreUnit QuickSave_Explore_Script = _Basic_Owner_Script._Card_ExploreUnit_Script;
                    _Basic_Owner_Script.UseLicense(
                        "Explore", QuickSave_Using_Script, null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    _View_Name_Text.text = QuickSave_Explore_Script._Basic_Language_Class.Name;
                    _View_Image_Image.sprite = _World_Manager._View_Manager.GetSprite("Explore", "Card", QuickSave_Explore_Script._Basic_Key_String);
                }
                break;
            #endregion

            #region - Behavior -
            case "Behavior":
                {
                    _Skill_BehaviorUnit QuickSave_Behavior_Script = _Basic_Owner_Script._Card_BehaviorUnit_Script;
                    _Basic_Owner_Script.UseLicense(
                        "Behavior", QuickSave_Using_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int
                        , ReactTag: _World_Manager._Map_Manager._State_ReactTag_StringList);
                    _View_Name_Text.text = QuickSave_Behavior_Script._Basic_Language_Class.Name;
                    _View_Image_Image.sprite = _World_Manager._View_Manager.GetSprite("Behavior", "Card", QuickSave_Behavior_Script._Basic_Key_String);
                }
                break;
            #endregion

            #region - Enchance -
            case "Enchance":
                {
                    //無顯示判定
                    _UI_Card_Unit QuickSave_UsedCard_Script =
                        _World_Manager._UI_Manager._UI_CardManager._Card_UsingCard_Script;
                    _Skill_EnchanceUnit QuickSave_Enchance_Script = 
                        _Basic_Owner_Script._Card_EnchanceUnit_Script;
                    string QuickSave_EnchanceType_String = 
                        QuickSave_UsedCard_Script._Card_BehaviorUnit_Script.Key_Enchance();
                    if (_Basic_Owner_Script._Card_SpecialUnit_Dictionary.
                        TryGetValue(QuickSave_EnchanceType_String, out _Skill_EnchanceUnit DicValue))
                    {
                        QuickSave_Enchance_Script = DicValue;
                    }

                    _Basic_Owner_Script.UseLicense(
                        QuickSave_EnchanceType_String, QuickSave_Using_Script, 
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int,
                        QuickSave_UsedCard_Script);
                    _View_Name_Text.text = QuickSave_Enchance_Script._Basic_Language_Class.Name;
                    _View_Image_Image.sprite = 
                        _World_Manager._View_Manager.GetSprite(QuickSave_EnchanceType_String, "Card", 
                        QuickSave_Enchance_Script._Basic_Key_String);
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - CanUseSet -
    //能否使用差異——————————————————————————————————————————————————————————————————————
    public void CanUseSet(bool CanUse, bool Out)
    {
        if (CanUse)
        {
            if (Out)
            {
                _View_Name_Text.color = new Color(1, 1, 1, 0.4f);
                _View_Image_Image.color = Color.white;
            }
            else
            {
                _View_Name_Text.color = Color.white;
                _View_Image_Image.color = new Color(1, 1, 1, 0.4f);
            }
        }
        else
        {
            if (Out)
            {
                _View_Name_Text.color = new Color(0.3f, 0.3f, 0.3f, 0.4f);
                _View_Image_Image.color = new Color(0.3f, 0.3f, 0.3f, 1);
            }
            else
            {
                _View_Name_Text.color = new Color(0.3f, 0.3f, 0.3f, 1f);
                _View_Image_Image.color = new Color(0.3f, 0.3f, 0.3f, 0.4f);
            }
        }
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - DetailSet - 
    //詳細(資訊)設置——————————————————————————————————————————————————————————————————————
    public void DetailSet(string Type, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        _Object_CreatureUnit QuickSave_Creature_Script =
            _Basic_Owner_Script._Basic_Source_Class.Source_Creature;
        _Map_BattleObjectUnit QuickSave_UsingObject_Script =
            QuickSave_Creature_Script._Card_UsingObject_Script;
        if (_Basic_Owner_Script._Card_UseObject_Script != null)
        {
            QuickSave_UsingObject_Script = 
                _Basic_Owner_Script._Card_UseObject_Script;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            #region - Explore -
            case "Explore":
                {
                    _Skill_ExploreUnit QuickSave_Explore_Script = _Basic_Owner_Script._Card_ExploreUnit_Script;

                    _View_ExploreInfo_GameObject.SetActive(true);
                    if (!_Basic_Owner_Script._State_ExploreCanUse_Bool)
                    {
                        _View_Explain_GameObject.SetActive(true);
                        _View_Explain_Text.text = _World_Manager._World_GeneralManager._World_TextManager.
                            _Language_UIName_Dictionary[_Basic_Owner_Script._State_CannotUseCause_String];
                    }
                    //圖示設定
                    if (QuickSave_Explore_Script._Basic_Key_String != null)//非Event
                    {
                        _View_ExploreImage_Image.sprite = _View_Manager.GetSprite("Explore", "CardInfo", QuickSave_Explore_Script._Basic_Key_String);
                    }
                    else//Event
                    {
                        _View_ExploreImage_Image.sprite = _View_Manager.GetSprite("Event", "CardInfo", QuickSave_Explore_Script._Basic_Key_String);
                    }
                    //文字設定
                    string QuickSave_Name_String = QuickSave_Explore_Script._Basic_Language_Class.Name;
                    _View_ExploreName_Text.text = QuickSave_Name_String;
                    _View_ExploreName_Text.fontSize = 100;
                    _View_ExploreName_Text.fontSize =
                        Mathf.RoundToInt((2250 -
                        Mathf.Clamp(_View_ExploreName_Text.preferredWidth, 750, 1500)) * 0.06f);
                    _View_ExploreSummary_Text.text =
                            _World_TextManager.TextmeshProTranslater
                            ("Default", QuickSave_Explore_Script._Basic_Language_Class.Summary, 0,
                            _Basic_Owner_Script._Card_ExploreUnit_Script._Basic_Source_Class, null, QuickSave_UsingObject_Script,
                            Time, Order);
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_View_ExploreInfo_GameObject.transform);
                }
                break;
            #endregion

            #region - Behavior -
            case "Behavior":
                {
                    _Skill_BehaviorUnit QuickSave_Behavior_Script = _Basic_Owner_Script._Card_BehaviorUnit_Script;
                    string QuickSave_EnchanceType_String =
                        QuickSave_Behavior_Script.Key_Enchance();

                    _View_BehaviorInfo_GameObject.SetActive(true);
                    if (!_Basic_Owner_Script._State_BehaviorCanUse_Bool)
                    {
                        _View_Explain_GameObject.SetActive(true);
                        _View_Explain_Text.text = _World_Manager._World_GeneralManager._World_TextManager.
                            _Language_UIName_Dictionary[_Basic_Owner_Script._State_CannotUseCause_String];
                    }

                    string QuickSave_Name_String = QuickSave_Behavior_Script._Basic_Language_Class.Name;
                    _View_BehaviorName_Text.text = QuickSave_Name_String;
                    _View_BehaviorName_Text.fontSize = 100;
                    _View_BehaviorName_Text.fontSize =
                        Mathf.RoundToInt((2250 -
                        Mathf.Clamp(_View_BehaviorName_Text.preferredWidth, 750, 1500)) * 0.06f);

                    _View_BehaviorImage_Image.sprite = 
                        _View_Manager.GetSprite("Behavior", "CardInfo", QuickSave_Behavior_Script._Basic_Key_String);

                    _View_BehaviorDelayBefore_Text.text = 
                        QuickSave_Behavior_Script.Key_DelayBefore(null, QuickSave_UsingObject_Script, 
                        ContainEnchance: true, ContainTimeOffset: false).ToString();
                    _View_BehaviorDelayAfter_Text.text = 
                        QuickSave_Behavior_Script.Key_DelayAfter(null, QuickSave_UsingObject_Script,
                        ContainEnchance: true, ContainTimeOffset: false).ToString();

                    int QuickSave_EnchanceEnchant_Int = 0;
                    foreach (_UI_Card_Unit Enchance in _Basic_Owner_Script._State_EnchanceStore_ScriptsList)
                    {
                        _Skill_EnchanceUnit EnchanceUnit = Enchance._Card_EnchanceUnit_Script;
                        if (Enchance._Card_SpecialUnit_Dictionary.
                            TryGetValue(QuickSave_EnchanceType_String, out _Skill_EnchanceUnit SpecialUnit))
                        {
                            EnchanceUnit = SpecialUnit;
                        }
                        QuickSave_EnchanceEnchant_Int += Mathf.RoundToInt(
                            EnchanceUnit._Basic_Data_Class.Enchant);
                    }
                    for (int a = 0; a < _View_BehaviorEnchances_ImageList.Count; a++)
                    {
                        if (a < QuickSave_Behavior_Script._Basic_Data_Class.Enchant)
                        {
                            if ( a < QuickSave_EnchanceEnchant_Int)
                            {
                                _View_BehaviorEnchances_ImageList[a].color =
                                    _World_Manager._View_Manager.GetColor("Code", "Code");
                            }
                            else
                            {
                                _View_BehaviorEnchances_ImageList[a].color = Color.white;
                            }
                        }
                        else
                        {
                            _View_BehaviorEnchances_ImageList[a].color = Color.clear;
                        }
                    }
                    _View_BehaviorSummary_Text.text =
                        _World_TextManager.TextmeshProTranslater
                        ("Default", QuickSave_Behavior_Script._Basic_Language_Class.Summary, 0,
                            _Basic_Owner_Script._Card_BehaviorUnit_Script._Basic_Source_Class, null, QuickSave_UsingObject_Script,
                            Time, Order);

                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_View_BehaviorInfo_GameObject.transform);
                }
                break;
            #endregion

            #region - Enchance -
            case "Enchance":
                {
                    //無顯示判定
                    _UI_Card_Unit QuickSave_UsedCard_Script =
                        _World_Manager._UI_Manager._UI_CardManager._Card_UsingCard_Script;
                    _View_Card_Unit QuickSave_UsedCardView_Script =
                        QuickSave_UsedCard_Script._Basic_View_Script;
                    _Skill_EnchanceUnit QuickSave_Enchance_Script =
                        _Basic_Owner_Script._Card_EnchanceUnit_Script;
                    string QuickSave_EnchanceType_String =
                        QuickSave_UsedCard_Script._Card_BehaviorUnit_Script.Key_Enchance();
                    if (_Basic_Owner_Script._Card_SpecialUnit_Dictionary.
                        TryGetValue(QuickSave_EnchanceType_String, out _Skill_EnchanceUnit DicValue))
                    {
                        QuickSave_Enchance_Script = DicValue;
                    }

                    QuickSave_UsedCardView_Script._View_EnchanceInfo_GameObject.SetActive(true);
                    if (!_Basic_Owner_Script._State_EnchanceCanUse_Bool)
                    {
                        QuickSave_UsedCardView_Script._View_Explain_GameObject.SetActive(true);
                        QuickSave_UsedCardView_Script._View_Explain_Text.text = 
                            _World_Manager._World_GeneralManager._World_TextManager.
                            _Language_UIName_Dictionary[_Basic_Owner_Script._State_CannotUseCause_String];
                    }

                    string QuickSave_Name_String = QuickSave_Enchance_Script._Basic_Language_Class.Name;
                    QuickSave_UsedCardView_Script._View_EnchanceName_Text.text = QuickSave_Name_String;
                    QuickSave_UsedCardView_Script._View_EnchanceName_Text.fontSize = 100;
                    QuickSave_UsedCardView_Script._View_EnchanceName_Text.fontSize =
                        Mathf.RoundToInt((2250 -
                        Mathf.Clamp(QuickSave_UsedCardView_Script._View_EnchanceName_Text.preferredWidth, 750, 1500)) * 0.06f);
                    QuickSave_UsedCardView_Script._View_EnchanceImage_Image.sprite = 
                        _View_Manager.GetSprite(QuickSave_EnchanceType_String, "CardInfo", QuickSave_Enchance_Script._Basic_Key_String);

                    QuickSave_UsedCardView_Script._View_EnchanceDelayBefore_Text.text = 
                        QuickSave_Enchance_Script.Key_DelayBefore(null, QuickSave_UsingObject_Script).ToString();
                    QuickSave_UsedCardView_Script._View_EnchanceDelayAfter_Text.text = 
                        QuickSave_Enchance_Script.Key_DelayAfter(null, QuickSave_UsingObject_Script).ToString();
                    for (int a = 0; a < QuickSave_UsedCardView_Script._View_EnchanceEnchances_ImageList.Count; a++)
                    {
                        if (a < QuickSave_Enchance_Script._Basic_Data_Class.Enchant)
                        {
                            QuickSave_UsedCardView_Script._View_EnchanceEnchances_ImageList[a].color =
                                _World_Manager._View_Manager.GetColor("Code", "Code");
                        }
                        else
                        {
                            QuickSave_UsedCardView_Script._View_EnchanceEnchances_ImageList[a].color = Color.clear;
                        }
                    }
                    QuickSave_UsedCardView_Script._View_EnchanceSummary_Text.text =
                        _World_TextManager.TextmeshProTranslater
                        ("Default", QuickSave_Enchance_Script._Basic_Language_Class.Summary, 0,
                            QuickSave_Enchance_Script._Basic_Source_Class, null, QuickSave_UsingObject_Script,
                            Time, Order);
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)QuickSave_UsedCardView_Script._View_EnchanceInfo_GameObject.transform);
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - MouseOver -
    //滑鼠滑入設置——————————————————————————————————————————————————————————————————————
    public void MouseOverIn(string Type)
    {
        //----------------------------------------------------------------------------------------------------
        _View_Offset_Transform.localPosition =
            new Vector3(0, Mathf.Clamp(_View_Name_Text.preferredHeight * 0.12f, 20, 135), 0);
        switch (Type)
        {
            case "Explore":
                CanUseSet(_Basic_Owner_Script._State_ExploreCanUse_Bool, false);
                break;
            case "Behavior":
                CanUseSet(_Basic_Owner_Script._State_BehaviorCanUse_Bool, false);
                break;
            case "Enchance":
                CanUseSet(_Basic_Owner_Script._State_EnchanceCanUse_Bool, false);
                break;
        }
        DetailSet(Type, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //滑鼠滑出設置——————————————————————————————————————————————————————————————————————
    public void MouseOverOut(string Type, bool ForceReset = false)
    {
        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            #region - Explore -
            case "Explore":
                {
                    _View_Explain_GameObject.SetActive(false);
                    CanUseSet(_Basic_Owner_Script._State_ExploreCanUse_Bool, true);
                    _View_Offset_Transform.localPosition = Vector3.zero;
                    _View_ExploreInfo_GameObject.SetActive(false);
                }
                break;
            #endregion

            #region - Behavior -
            case "Behavior":
                {
                    _View_Explain_GameObject.SetActive(false);
                    CanUseSet(_Basic_Owner_Script._State_BehaviorCanUse_Bool, true);
                    _View_Offset_Transform.localPosition = Vector3.zero;
                    _View_BehaviorInfo_GameObject.SetActive(false);
                }
                break;
            #endregion

            #region - Enchance -
            case "Enchance":
                {
                    _UI_Card_Unit QuickSave_UsingCard_Script =
                        _World_Manager._UI_Manager._UI_CardManager._Card_UsingCard_Script;
                    QuickSave_UsingCard_Script._Basic_View_Script._View_Explain_GameObject.SetActive(false);
                    CanUseSet(_Basic_Owner_Script._State_EnchanceCanUse_Bool, true);
                    if (_Basic_Owner_Script._Card_EnchanceUnit_Script._Owner_EnchanceTarget_Script !=
                        QuickSave_UsingCard_Script || ForceReset)
                    {
                        _View_Offset_Transform.localPosition = Vector3.zero;
                    }
                    QuickSave_UsingCard_Script._Basic_View_Script._View_EnchanceInfo_GameObject.SetActive(false);
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - MouseClick -
    //滑鼠點擊入設置——————————————————————————————————————————————————————————————————————
    public void MouseClickOn(string Type)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_BattleRound _Map_BattleRound = _Map_Manager._Map_BattleRound;
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        _UI_CardManager _UI_CardManager = _World_Manager._UI_Manager._UI_CardManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _Map_BattleObjectUnit QuickSave_UsingObject_Script = _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Card_UsingObject_Script;
        _Basic_Owner_Script._Card_UseObject_Script = QuickSave_UsingObject_Script;

        switch (Type)
        {
            #region - Explore -
            case "Explore":
                {
                    Vector QuickSave_StartCoordinate_Class =
                        _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Creature_FieldObjectt_Script._Map_Coordinate_Class;
                    _View_Name_Text.material = _World_Manager._World_GeneralManager._UI_HDRText_Material;

                    switch (_Map_Manager._State_FieldState_String)
                    {
                        case "EventSelect":
                        case "EventFrame":
                            {
                                _UI_EventManager _UI_EventManager = _World_Manager._UI_Manager._UI_EventManager;
                                //事件中使用Token
                                if (_UI_CardManager._Card_UsingCard_Script == null)
                                {
                                    _UI_CardManager._Card_UsingCard_Script = _Basic_Owner_Script;
                                    MouseOverIn("Explore");
                                    _UI_EventManager.EventSelect(_Basic_Owner_Script);
                                }
                                else
                                {
                                    _UI_CardManager._Card_EventingCard_ScriptsList.Add(_Basic_Owner_Script);
                                }
                            }
                            break;
                        default:
                            {
                                //正常
                                _Map_Manager.FieldStateSet("SelectRange", "點擊行為卡牌，顯示範圍");
                                _UI_CardManager._Card_UsingCard_Script = _Basic_Owner_Script;

                                //顯示範圍
                                {
                                    Dictionary<string, List<Vector>> QuickSave_Range_Dictionary =
                                        _Basic_Owner_Script._Card_ExploreUnit_Script.
                                        Key_Range(QuickSave_StartCoordinate_Class, QuickSave_UsingObject_Script);

                                    List<Vector> QuickSave_Target_ClassList = new List<Vector>();
                                    List<Vector> QuickSave_Range_ClassList = new List<Vector>();
                                    List<Vector> QuickSave_Path_ClassList = new List<Vector>();
                                    List<Vector> QuickSave_Select_ClassList = new List<Vector>();

                                    foreach (string Key in QuickSave_Range_Dictionary.Keys)
                                    {
                                        QuickSave_Target_ClassList.AddRange(_Basic_Owner_Script._Range_Select_Class.Vector());
                                        QuickSave_Range_ClassList.AddRange(QuickSave_Range_Dictionary[Key]);
                                        QuickSave_Path_ClassList.AddRange(
                                            _Basic_Owner_Script._Range_Path_Class.AllVectors(
                                                _Basic_Owner_Script._Range_Path_Class.PathUnits(Key)));
                                        QuickSave_Select_ClassList.AddRange(
                                            _Basic_Owner_Script._Range_Select_Class.AllVectors(
                                                _Basic_Owner_Script._Range_Select_Class.SelectUnits(Key)));
                                    }
                                    _Map_Manager.ViewOn
                                        ("Range", null,
                                        QuickSave_Target_ClassList,
                                        QuickSave_Range_ClassList,
                                        QuickSave_Path_ClassList,
                                        QuickSave_Select_ClassList);
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion

            #region - Behavior -
            case "Behavior":
                {
                    //回合演進
                    _Map_Manager.BattleStateSet("PlayerEnchance", "點擊行為卡牌，顯示附魔");
                    //冠必生物資訊顯示
                    _Object_Manager.ColliderTurnOff();

                    _UI_CardManager._Card_UsingCard_Script = _Basic_Owner_Script;

                    //回合設置
                    _Map_BattleObjectUnit QuickSave_Object_Script =
                        _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Basic_Object_Script;
                    RoundElementClass QuickSave_CardRound_Class = 
                        _Basic_Owner_Script._Round_Unit_Class;
                    RoundElementClass QuickSave_CreatureRound_Class =
                        QuickSave_Object_Script._Round_Unit_Class;

                    int QuickSave_DelayBeforeTime_Int =
                        _Basic_Owner_Script._Card_BehaviorUnit_Script.
                        Key_DelayBefore(null, _Basic_Owner_Script._Card_UseObject_Script,
                        ContainEnchance: true, ContainTimeOffset: false);
                    int QuickSave_DelayAfterTime_Int =
                        QuickSave_DelayBeforeTime_Int + 
                        _Basic_Owner_Script._Card_BehaviorUnit_Script.
                        Key_DelayAfter(null, _Basic_Owner_Script._Card_UseObject_Script,
                        ContainEnchance: true, ContainTimeOffset: false);

                    Vector QuickSave_Coordinate_Class =
                        QuickSave_UsingObject_Script.
                        TimePosition(_Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    _Basic_Owner_Script._Round_DelayBefore_Int = _Map_BattleRound._Round_Time_Int + QuickSave_DelayBeforeTime_Int;
                    _Basic_Owner_Script._Round_DelayAfter_Int = _Map_BattleRound._Round_Time_Int + QuickSave_DelayAfterTime_Int;

                    //顯示範圍
                    {
                        Dictionary<string, List<Vector>> QuickSave_Range_Dictionary =
                            _Basic_Owner_Script._Card_BehaviorUnit_Script.
                            Key_Range(QuickSave_Coordinate_Class, QuickSave_UsingObject_Script,
                            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        List<Vector> QuickSave_Target_ClassList = new List<Vector>();
                        List<Vector> QuickSave_Range_ClassList = new List<Vector>();
                        List<Vector> QuickSave_Path_ClassList = new List<Vector>();
                        List<Vector> QuickSave_Select_ClassList = new List<Vector>();
                        foreach (string Key in QuickSave_Range_Dictionary.Keys)
                        {
                            QuickSave_Target_ClassList.AddRange(_Basic_Owner_Script._Range_Select_Class.Vector());
                            QuickSave_Range_ClassList.AddRange(QuickSave_Range_Dictionary[Key]);
                            QuickSave_Path_ClassList.AddRange(
                                _Basic_Owner_Script._Range_Path_Class.AllVectors(
                                    _Basic_Owner_Script._Range_Path_Class.PathUnits(Key)));
                            QuickSave_Select_ClassList.AddRange(
                                _Basic_Owner_Script._Range_Select_Class.AllVectors(
                                    _Basic_Owner_Script._Range_Select_Class.SelectUnits(Key)));
                        }
                        _Map_Manager.ViewOn
                            ("Range", null,
                            QuickSave_Target_ClassList,
                            QuickSave_Range_ClassList,
                            QuickSave_Path_ClassList,
                            QuickSave_Select_ClassList);
                    }

                    //回合設置
                    RoundSequenceUnitClass QuickSave_RoundSequence_Class =
                        new RoundSequenceUnitClass
                        {
                            Type = "Preview",
                            Owner = QuickSave_Object_Script,
                            RoundUnit = new List<RoundElementClass> { QuickSave_CardRound_Class, QuickSave_CreatureRound_Class }
                        };
                    QuickSave_CardRound_Class.AccumulatedTime = _Map_BattleRound._Round_Time_Int;
                    QuickSave_CardRound_Class.DelayTime = QuickSave_DelayBeforeTime_Int;
                    QuickSave_CreatureRound_Class.DelayTime = QuickSave_DelayAfterTime_Int;
                    _Basic_Owner_Script._Round_GroupUnit_Class = QuickSave_RoundSequence_Class;
                    //新增回合
                    if (!_Map_Manager._State_Reacting_Bool)
                    {
                        _Map_BattleRound.RoundSequenceSet(
                            _Basic_Owner_Script._Round_GroupUnit_Class, null);
                    }
                    else
                    {
                        //觸發
                        _UI_Card_Unit QuickSave_Card_Script =
                            _World_Manager._UI_Manager._UI_CardManager._React_CallerCard_Script;
                        _Map_BattleRound.RoundSequenceSet(
                            _Basic_Owner_Script._Round_GroupUnit_Class, QuickSave_Card_Script._Round_GroupUnit_Class);
                    }

                    //注視設定
                    _View_Name_Text.material = _World_Manager._World_GeneralManager._UI_HDRText_Material;
                    _World_Manager._UI_Manager._View_Battle.FocusSet(QuickSave_UsingObject_Script);

                    //卡片替換
                    List<_UI_Card_Unit> QuickSave_BattleBoard_ScriptList = 
                        _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Card_CardsBoard_ScriptList;
                    //其他卡牌替換為附魔
                    for (int a = 0; a < QuickSave_BattleBoard_ScriptList.Count; a++)
                    {
                        if (QuickSave_BattleBoard_ScriptList[a] != _Basic_Owner_Script)
                        {
                            QuickSave_BattleBoard_ScriptList[a]._Basic_View_Script.SimpleSet("Enchance");
                        }
                    }
                    _Map_BattleRound.SequenceView();
                }
                break;
            #endregion

            #region - Enchance -
            case "Enchance":
                {
                    _Object_CreatureUnit QuickSave_SelfCreature_Script =
                        _Basic_Owner_Script._Basic_Source_Class.Source_Creature;
                    _UI_Card_Unit QuickSave_UsedCard_Script = 
                        _UI_CardManager._Card_UsingCard_Script;
                    string QuickSave_EnchanceType_String =
                        QuickSave_UsedCard_Script._Card_BehaviorUnit_Script.Key_Enchance();
                    _View_Name_Text.material = _World_Manager._World_GeneralManager._UI_HDRText_Material;
                    //加入清單                    
                    _Basic_Owner_Script.EnchanceSet("Add", QuickSave_EnchanceType_String, QuickSave_UsedCard_Script, QuickSave_UsingObject_Script);
                    //更新附魔卡片
                    List<_UI_Card_Unit> QuickSave_Cards_ScriptsList = 
                        QuickSave_SelfCreature_Script._Card_CardsBoard_ScriptList;
                    for (int a = 0; a < QuickSave_Cards_ScriptsList.Count; a++)
                    {
                        if (QuickSave_UsedCard_Script != QuickSave_Cards_ScriptsList[a])
                        {
                            QuickSave_Cards_ScriptsList[a].
                                _Basic_View_Script.SimpleSet(QuickSave_EnchanceType_String);
                        }
                    }
                    //注視設定
                    _World_Manager._UI_Manager._View_Battle.FocusSet(QuickSave_UsingObject_Script);
                    //重新設置時間軸
                    QuickSave_UsedCard_Script._Basic_View_Script.SequenceReSet();
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //滑鼠點擊入設置——————————————————————————————————————————————————————————————————————
    public void MouseClickOff(string Type,bool MouseCover)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_FieldCreator _Map_FieldCreator = _Map_Manager._Map_FieldCreator;
        _Map_BattleCreator _Map_BattleCreator = _Map_Manager._Map_BattleCreator;
        _Map_BattleRound _Map_BattleRound = _Map_Manager._Map_BattleRound;
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        _UI_CardManager _UI_CardManager = _World_Manager._UI_Manager._UI_CardManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _Map_BattleObjectUnit QuickSave_UsingObject_Script =
            _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Card_UsingObject_Script;

        switch (Type)
        {
            #region - Explore -
            case "Explore":
                {
                    _View_Name_Text.material = null;
                    if (!MouseCover)
                    {
                        MouseOverOut("Explore");
                    }

                    switch (_Map_Manager._State_FieldState_String)
                    {
                        case "EventSelect":
                        case "EventFrame":
                            //事件中使用Token
                            if (_UI_CardManager._Card_UsingCard_Script == _Basic_Owner_Script)
                            {
                                _UI_CardManager._Card_UsingCard_Script = null;
                                MouseOverOut("Explore");
                                _World_Manager._UI_Manager._UI_EventManager.EventSelect(_Basic_Owner_Script);
                            }
                            else
                            {
                                _UI_CardManager._Card_EventingCard_ScriptsList.Remove(_Basic_Owner_Script);
                            }
                            break;
                        default:
                            {
                                _UI_CardManager._Card_UsingCard_Script = null;

                                _Map_Manager.ViewOff("Select");
                                _Map_Manager.ViewOff("Path");
                                _Map_Manager.ViewOff("Range");
                                _Map_Manager.FieldStateSet("SelectExplore", "取消選擇卡片，回到選擇模式");
                            }
                            break;
                    }
                }
                break;
            #endregion

            #region - Behavior -
            case "Behavior":
                {
                    //開起生物資訊顯示
                    _Object_Manager.ColliderTurnOn();

                    //排定
                    RoundElementClass QuickSave_CardRound_Class =
                        _Basic_Owner_Script._Round_Unit_Class;
                    RoundElementClass QuickSave_CreatureRound_Class = 
                        _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Basic_Object_Script._Round_Unit_Class;
                    QuickSave_CardRound_Class.DelayTime = 0;
                    QuickSave_CreatureRound_Class.DelayTime = 0;
                    //移除回合
                    if (!_Map_Manager._State_Reacting_Bool)
                    {
                        _Map_BattleRound.RoundSequenceSet(
                            null, _Basic_Owner_Script._Round_GroupUnit_Class);
                    }
                    else
                    {
                        //觸發
                        _UI_Card_Unit QuickSave_Card_Script =
                            _World_Manager._UI_Manager._UI_CardManager._React_CallerCard_Script;
                        int QuickSave_ReactOriginalTime_Int =
                            _Map_BattleRound._Round_Time_Int +
                            QuickSave_Card_Script._Card_BehaviorUnit_Script.
                            Key_DelayAfter(null, QuickSave_UsingObject_Script, 
                            ContainEnchance: true, ContainTimeOffset: false);
                        QuickSave_CreatureRound_Class.DelayTime = QuickSave_ReactOriginalTime_Int;
                        _Map_BattleRound.RoundSequenceSet(
                            QuickSave_Card_Script._Round_GroupUnit_Class, _Basic_Owner_Script._Round_GroupUnit_Class);
                    }

                    //移除時間標記
                    int QuickSave_Time_Int = _Basic_Owner_Script._Round_DelayBefore_Int;
                    int QuickSave_Order_Int = _Map_BattleRound.RoundUnit(_Basic_Owner_Script._Round_DelayBefore_Int).Count;
                    //時間移除
                    _Basic_Owner_Script._Card_UseObject_Script.
                        TimePositionRemove(QuickSave_Time_Int, QuickSave_Order_Int);

                    //設定名稱
                    _View_Name_Text.material = null;

                    //更新行為卡片
                    if (!MouseCover)
                    {
                        MouseOverOut("Behavior");
                    }

                    //更新附魔卡片
                    List<_UI_Card_Unit> QuickSave_BattleBoard_ScriptList =
                        _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Card_CardsBoard_ScriptList;
                    for (int a = 0; a < QuickSave_BattleBoard_ScriptList.Count; a++)
                    {
                        QuickSave_BattleBoard_ScriptList[a]._Basic_View_Script.SimpleSet("Behavior");
                    }

                    _UI_CardManager._Card_UsingCard_Script = null;

                    _Map_Manager.ViewOff("Select");
                    _Map_Manager.ViewOff("Path");
                    _Map_Manager.ViewOff("Range");

                    _Map_BattleRound.SequenceView();
                    _Map_Manager.BattleStateSet("PlayerBehavior", "取消選擇卡片，回到行為選擇模式");
                }
                break;
            #endregion

            #region - Enchance -
            case "Enchance":
                {
                    _UI_Card_Unit QuickSave_UsedCard_Script =
                        _UI_CardManager._Card_UsingCard_Script;
                    string QuickSave_EnchanceType_String =
                        QuickSave_UsedCard_Script._Card_BehaviorUnit_Script.Key_Enchance();
                    _View_Name_Text.material = null;
                    //移出清單
                    _Basic_Owner_Script.EnchanceSet("Remove", QuickSave_EnchanceType_String, QuickSave_UsedCard_Script, null);
                    //離開當前資訊
                    if (!MouseCover)
                    {
                        MouseOverOut(Type);
                    }
                    //更新附魔卡片
                    if (MouseCover)
                    {
                        //更新附魔卡片
                        List<_UI_Card_Unit> QuickSave_BattleBoard_ScriptList =
                            _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Card_CardsBoard_ScriptList;
                        for (int a = 0; a < QuickSave_BattleBoard_ScriptList.Count; a++)
                        {
                            QuickSave_BattleBoard_ScriptList[a]._Basic_View_Script.SimpleSet(QuickSave_EnchanceType_String);
                        }
                    }
                    //重新設置時間軸
                    QuickSave_UsedCard_Script._Basic_View_Script.SequenceReSet();
                }
                break;
                #endregion
        }
        _Basic_Owner_Script._Card_UseObject_Script = null;
        _Basic_Owner_Script._Range_UseData_Class.Path = new List<PathUnitClass>();
        _Basic_Owner_Script._Range_UseData_Class.Select = new List<SelectUnitClass>();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion MouseState

    #region 
    public void SequenceReSet()//作為Behavior使用
    {
        //----------------------------------------------------------------------------------------------------
        _Map_BattleRound _Map_BattleRound = _World_Manager._Map_Manager._Map_BattleRound;
        RoundElementClass QuickSave_CreatureRound_Class =
            _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Basic_Object_Script._Round_Unit_Class;
        RoundElementClass QuickSave_CardRound_Class =
            _Basic_Owner_Script._Round_Unit_Class;
        int QuickSave_DelayBeforeTime_Int =
            _Basic_Owner_Script._Card_BehaviorUnit_Script.
            Key_DelayBefore(null, _Basic_Owner_Script._Card_UseObject_Script,
            ContainEnchance: true, ContainTimeOffset: false);
        int QuickSave_DelayAfterTime_Int =
            QuickSave_DelayBeforeTime_Int +
            _Basic_Owner_Script._Card_BehaviorUnit_Script.
            Key_DelayAfter(null, _Basic_Owner_Script._Card_UseObject_Script,
            ContainEnchance: true, ContainTimeOffset: false);

        //生物回合
        QuickSave_CardRound_Class.DelayTime = QuickSave_DelayBeforeTime_Int;
        QuickSave_CreatureRound_Class.DelayTime = QuickSave_DelayAfterTime_Int;
        //檔案設置
        _Basic_Owner_Script._Round_DelayBefore_Int = _Map_BattleRound._Round_Time_Int + QuickSave_DelayBeforeTime_Int;
        _Basic_Owner_Script._Round_DelayAfter_Int = _Map_BattleRound._Round_Time_Int + QuickSave_DelayAfterTime_Int;
        //更新行為卡片
        _Basic_Owner_Script._Basic_View_Script.SimpleSet("Behavior");
        _Basic_Owner_Script._Basic_View_Script.DetailSet("Behavior", _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        //視覺更新
        _Map_BattleRound.SequenceSort();

        //----------------------------------------------------------------------------------------------------
    }
    #endregion
}
