using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _View_Card_Unit : MonoBehaviour
{
    #region ElementBox
    //�l���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�l���h----------------------------------------------------------------------------------------------------
    public _UI_Card_Unit _Basic_Owner_Script;

    //��m
    public Transform _View_Offset_Transform;
    public Transform _View_InfoStore_Transform;
    public GameObject _View_ExploreInfo_GameObject;
    public GameObject _View_BehaviorInfo_GameObject;
    public GameObject _View_EnchanceInfo_GameObject;
    public GameObject _View_Explain_GameObject;
    //----------------------------------------------------------------------------------------------------

    //�d�P²��----------------------------------------------------------------------------------------------------
    public Text _View_Name_Text;
    public Image _View_Image_Image;

    //����
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ElementBox

    #region MouseState
    #region - SimpleSet -
    //²��(�p�d��)�]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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
                    //�L��ܧP�w
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - CanUseSet -
    //��_�ϥήt���X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - DetailSet - 
    //�Բ�(��T)�]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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
                    //�ϥܳ]�w
                    if (QuickSave_Explore_Script._Basic_Key_String != null)//�DEvent
                    {
                        _View_ExploreImage_Image.sprite = _View_Manager.GetSprite("Explore", "CardInfo", QuickSave_Explore_Script._Basic_Key_String);
                    }
                    else//Event
                    {
                        _View_ExploreImage_Image.sprite = _View_Manager.GetSprite("Event", "CardInfo", QuickSave_Explore_Script._Basic_Key_String);
                    }
                    //��r�]�w
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
                    //�L��ܧP�w
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - MouseOver -
    //�ƹ��ƤJ�]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�ƹ��ƥX�]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - MouseClick -
    //�ƹ��I���J�]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void MouseClickOn(string Type)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
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
                                //�ƥ󤤨ϥ�Token
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
                                //���`
                                _Map_Manager.FieldStateSet("SelectRange", "�I���欰�d�P�A��ܽd��");
                                _UI_CardManager._Card_UsingCard_Script = _Basic_Owner_Script;

                                //��ܽd��
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
                    //�^�X�t�i
                    _Map_Manager.BattleStateSet("PlayerEnchance", "�I���欰�d�P�A��ܪ��]");
                    //�a���ͪ���T���
                    _Object_Manager.ColliderTurnOff();

                    _UI_CardManager._Card_UsingCard_Script = _Basic_Owner_Script;

                    //�^�X�]�m
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

                    //��ܽd��
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

                    //�^�X�]�m
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
                    //�s�W�^�X
                    if (!_Map_Manager._State_Reacting_Bool)
                    {
                        _Map_BattleRound.RoundSequenceSet(
                            _Basic_Owner_Script._Round_GroupUnit_Class, null);
                    }
                    else
                    {
                        //Ĳ�o
                        _UI_Card_Unit QuickSave_Card_Script =
                            _World_Manager._UI_Manager._UI_CardManager._React_CallerCard_Script;
                        _Map_BattleRound.RoundSequenceSet(
                            _Basic_Owner_Script._Round_GroupUnit_Class, QuickSave_Card_Script._Round_GroupUnit_Class);
                    }

                    //�`���]�w
                    _View_Name_Text.material = _World_Manager._World_GeneralManager._UI_HDRText_Material;
                    _World_Manager._UI_Manager._View_Battle.FocusSet(QuickSave_UsingObject_Script);

                    //�d������
                    List<_UI_Card_Unit> QuickSave_BattleBoard_ScriptList = 
                        _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Card_CardsBoard_ScriptList;
                    //��L�d�P���������]
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
                    //�[�J�M��                    
                    _Basic_Owner_Script.EnchanceSet("Add", QuickSave_EnchanceType_String, QuickSave_UsedCard_Script, QuickSave_UsingObject_Script);
                    //��s���]�d��
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
                    //�`���]�w
                    _World_Manager._UI_Manager._View_Battle.FocusSet(QuickSave_UsingObject_Script);
                    //���s�]�m�ɶ��b
                    QuickSave_UsedCard_Script._Basic_View_Script.SequenceReSet();
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�ƹ��I���J�]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void MouseClickOff(string Type,bool MouseCover)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
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
                            //�ƥ󤤨ϥ�Token
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
                                _Map_Manager.FieldStateSet("SelectExplore", "������ܥd���A�^���ܼҦ�");
                            }
                            break;
                    }
                }
                break;
            #endregion

            #region - Behavior -
            case "Behavior":
                {
                    //�}�_�ͪ���T���
                    _Object_Manager.ColliderTurnOn();

                    //�Ʃw
                    RoundElementClass QuickSave_CardRound_Class =
                        _Basic_Owner_Script._Round_Unit_Class;
                    RoundElementClass QuickSave_CreatureRound_Class = 
                        _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Basic_Object_Script._Round_Unit_Class;
                    QuickSave_CardRound_Class.DelayTime = 0;
                    QuickSave_CreatureRound_Class.DelayTime = 0;
                    //�����^�X
                    if (!_Map_Manager._State_Reacting_Bool)
                    {
                        _Map_BattleRound.RoundSequenceSet(
                            null, _Basic_Owner_Script._Round_GroupUnit_Class);
                    }
                    else
                    {
                        //Ĳ�o
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

                    //�����ɶ��аO
                    int QuickSave_Time_Int = _Basic_Owner_Script._Round_DelayBefore_Int;
                    int QuickSave_Order_Int = _Map_BattleRound.RoundUnit(_Basic_Owner_Script._Round_DelayBefore_Int).Count;
                    //�ɶ�����
                    _Basic_Owner_Script._Card_UseObject_Script.
                        TimePositionRemove(QuickSave_Time_Int, QuickSave_Order_Int);

                    //�]�w�W��
                    _View_Name_Text.material = null;

                    //��s�欰�d��
                    if (!MouseCover)
                    {
                        MouseOverOut("Behavior");
                    }

                    //��s���]�d��
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
                    _Map_Manager.BattleStateSet("PlayerBehavior", "������ܥd���A�^��欰��ܼҦ�");
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
                    //���X�M��
                    _Basic_Owner_Script.EnchanceSet("Remove", QuickSave_EnchanceType_String, QuickSave_UsedCard_Script, null);
                    //���}��e��T
                    if (!MouseCover)
                    {
                        MouseOverOut(Type);
                    }
                    //��s���]�d��
                    if (MouseCover)
                    {
                        //��s���]�d��
                        List<_UI_Card_Unit> QuickSave_BattleBoard_ScriptList =
                            _Basic_Owner_Script._Basic_Source_Class.Source_Creature._Card_CardsBoard_ScriptList;
                        for (int a = 0; a < QuickSave_BattleBoard_ScriptList.Count; a++)
                        {
                            QuickSave_BattleBoard_ScriptList[a]._Basic_View_Script.SimpleSet(QuickSave_EnchanceType_String);
                        }
                    }
                    //���s�]�m�ɶ��b
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion MouseState

    #region 
    public void SequenceReSet()//�@��Behavior�ϥ�
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

        //�ͪ��^�X
        QuickSave_CardRound_Class.DelayTime = QuickSave_DelayBeforeTime_Int;
        QuickSave_CreatureRound_Class.DelayTime = QuickSave_DelayAfterTime_Int;
        //�ɮ׳]�m
        _Basic_Owner_Script._Round_DelayBefore_Int = _Map_BattleRound._Round_Time_Int + QuickSave_DelayBeforeTime_Int;
        _Basic_Owner_Script._Round_DelayAfter_Int = _Map_BattleRound._Round_Time_Int + QuickSave_DelayAfterTime_Int;
        //��s�欰�d��
        _Basic_Owner_Script._Basic_View_Script.SimpleSet("Behavior");
        _Basic_Owner_Script._Basic_View_Script.DetailSet("Behavior", _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        //��ı��s
        _Map_BattleRound.SequenceSort();

        //----------------------------------------------------------------------------------------------------
    }
    #endregion
}
