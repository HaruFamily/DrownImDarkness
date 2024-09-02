using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _View_Battle : MonoBehaviour
{
    //——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    public Text _View_Name_Text;
    public Text _View_SubName_Text;
    public Text _View_State_Text;
    public Image _View_Image_Image;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    //生物
    public _UI_Manager.UIBarClass[] View_Points_ClassArray;
    public Text _View_ConsciousnessPoint_Text;
    public Transform _View_ConsciousnessPointStore_Transform;
    [HideInInspector] public List<_Object_ViewConsumeUnit> _View_ConsciousnessPoint_Scripts = new List<_Object_ViewConsumeUnit>();

    public Transform _View_CardsStore_Transform;

    public Transform _View_Sequences_Transform;
    //----------------------------------------------------------------------------------------------------

    //效果----------------------------------------------------------------------------------------------------
    public Transform _View_EffectStore_Transform;
    //----------------------------------------------------------------------------------------------------

    //戰鬥顯示----------------------------------------------------------------------------------------------------
    public _Object_CreatureUnit UI_FocusCreature_Script;
    public _Map_BattleObjectUnit UI_FocusObject_Script;

    public int _View_UsingObject_Int;//記錄非Player的地塊Using查看
    //----------------------------------------------------------------------------------------------------

    //卡牌----------------------------------------------------------------------------------------------------
    public Text _View_CardsDeck_Text;
    public Text _View_CardsCemetery_Text;

    public Transform _View_StandbyStore_Transform;
    //----------------------------------------------------------------------------------------------------

    //時程表----------------------------------------------------------------------------------------------------
    //增值物件
    public GameObject _View_SequenceUnit_GameObject;
    //增值物件
    public Transform _View_SequenceStore_Transform;
    //物件表
    public List<_Map_BattleSequenceUnit> _View_Sequences_ScriptsList = new List<_Map_BattleSequenceUnit>();

    //名稱
    public Text _View_SequenceName_Text;
    //時程持有者
    public Image _View_SequenceImage_Image;
    //時間
    public Text _View_SequenceTime_Text;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————



    #region Using 
    //生物目標——————————————————————————————————————————————————————————————————————
    public void CreatureFocus(_Object_CreatureUnit Creature)
    {
        //----------------------------------------------------------------------------------------------------
        if (UI_FocusCreature_Script != Creature)
        {
            if (UI_FocusCreature_Script != null)
            {
                Transform QuickSave_PastCreature_Transform = UI_FocusCreature_Script._Object_Inventory_Script._Skill_CardsStore_Transform;
                QuickSave_PastCreature_Transform.SetParent(UI_FocusCreature_Script._UI_TotalStore_Transform);
                QuickSave_PastCreature_Transform.localPosition = Vector3.zero;
                QuickSave_PastCreature_Transform.localScale = Vector3.zero;
            }

            Transform QuickSave_NewCreature_Transform = Creature._Object_Inventory_Script._Skill_CardsStore_Transform;
            QuickSave_NewCreature_Transform.SetParent(_View_CardsStore_Transform);
            QuickSave_NewCreature_Transform.localPosition = Vector3.zero;
            QuickSave_NewCreature_Transform.localScale = Vector3.one;

            UI_FocusCreature_Script = Creature;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void UsingObjectSet(string Type,int TargetKey = 65535, _Map_BattleObjectUnit Target = null)
    {
        //----------------------------------------------------------------------------------------------------
        #region - Creature -
        if (UI_FocusObject_Script != UI_FocusCreature_Script._Card_UsingObject_Script)
        {
            return;
        }
        //變更當前生物使用
        List<_Map_BattleObjectUnit> QuickSave_ReachQueue_ScriptsList = 
            UI_FocusCreature_Script._Object_Inventory_Script._Item_ReachObject_ScriptsList;
        switch (Type)
        {
            case "Target":
                if (TargetKey != 65535)
                {
                    UI_FocusCreature_Script._Card_UsingObject_Int = TargetKey;
                    UI_FocusCreature_Script._Card_UsingObject_Script = QuickSave_ReachQueue_ScriptsList[TargetKey];
                }
                if (Target != null)
                {
                    UI_FocusCreature_Script._Card_UsingObject_Int = QuickSave_ReachQueue_ScriptsList.IndexOf(Target);
                    UI_FocusCreature_Script._Card_UsingObject_Script = Target;
                }
                break;

            case "Previous":
                if (UI_FocusCreature_Script._Card_UsingObject_Int == 0)
                {
                    UI_FocusCreature_Script._Card_UsingObject_Int = QuickSave_ReachQueue_ScriptsList.Count - 1;
                }
                else
                {
                    UI_FocusCreature_Script._Card_UsingObject_Int -= 1;
                }
                UI_FocusCreature_Script._Card_UsingObject_Script = QuickSave_ReachQueue_ScriptsList[UI_FocusCreature_Script._Card_UsingObject_Int];
                break;

            case "Next":
                if (UI_FocusCreature_Script._Card_UsingObject_Int == QuickSave_ReachQueue_ScriptsList.Count - 1)
                {
                    UI_FocusCreature_Script._Card_UsingObject_Int = 0;
                }
                else
                {
                    UI_FocusCreature_Script._Card_UsingObject_Int += 1;
                }
                UI_FocusCreature_Script._Card_UsingObject_Script = QuickSave_ReachQueue_ScriptsList[UI_FocusCreature_Script._Card_UsingObject_Int];
                break;
            default:
                break;
        }
        _World_Manager._UI_Manager._UI_CardManager.BoardRefresh(UI_FocusCreature_Script);
        FocusSet(UI_FocusCreature_Script._Card_UsingObject_Script);
        #endregion
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //滾輪切換觀察物——————————————————————————————————————————————————————————————————————
    public void UsingObjectScroll(int Pinch)
    {
        _Map_BattleObjectUnit QuickSave_FocusTarget_Script = UI_FocusObject_Script;
        if (UI_FocusObject_Script == UI_FocusCreature_Script._Card_UsingObject_Script)
        {
            //玩家
            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                UI_FocusCreature_Script._Object_Inventory_Script._Item_ReachObject_ScriptsList;
            UI_FocusCreature_Script._Card_UsingObject_Int =
                QuickSave_Objects_ScriptsList.IndexOf(UI_FocusObject_Script);
            int QuickSave_UsingAnswer_Int =
                (((UI_FocusCreature_Script._Card_UsingObject_Int - Pinch) % QuickSave_Objects_ScriptsList.Count) +
                QuickSave_Objects_ScriptsList.Count) % QuickSave_Objects_ScriptsList.Count;
            UI_FocusCreature_Script._Card_UsingObject_Int = 
                QuickSave_UsingAnswer_Int;
            QuickSave_FocusTarget_Script = 
                QuickSave_Objects_ScriptsList[QuickSave_UsingAnswer_Int];
            UI_FocusCreature_Script._Card_UsingObject_Script =
                QuickSave_FocusTarget_Script;
            _World_Manager._UI_Manager._UI_CardManager.BoardRefresh(UI_FocusCreature_Script);
        }
        else
        {
            //其他(NPC/Objects)-觀察該位置當前所有物件
            Vector QuickSave_FocuObject_Class = UI_FocusObject_Script.
                TimePosition(_Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                _World_Manager._Object_Manager.
                TimeObjects("All", null,
                _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, QuickSave_FocuObject_Class);
            if (QuickSave_Objects_ScriptsList.Count == 0)
            {
                return;
            }
            _View_UsingObject_Int =
                QuickSave_Objects_ScriptsList.IndexOf(UI_FocusObject_Script);
            int QuickSave_UsingAnswer_Int =
                (((_View_UsingObject_Int - Pinch) % QuickSave_Objects_ScriptsList.Count) +
                QuickSave_Objects_ScriptsList.Count) % QuickSave_Objects_ScriptsList.Count;
            _View_UsingObject_Int =
                QuickSave_UsingAnswer_Int;
            QuickSave_FocusTarget_Script = 
                QuickSave_Objects_ScriptsList[QuickSave_UsingAnswer_Int];
        }
        FocusSet(QuickSave_FocusTarget_Script);
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    //指定目標——————————————————————————————————————————————————————————————————————
    public void FocusSet(_Map_BattleObjectUnit Object)
    {
        //----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        Transform QuickSave_FocusOne_Transform = null;//依照驅動狀態改攝影位置
        Dictionary<string, string> QuickSave_UIName_Dictionary =
            _World_Manager._World_GeneralManager._World_TextManager._Language_UIName_Dictionary;
        //----------------------------------------------------------------------------------------------------

        //設置----------------------------------------------------------------------------------------------------
        if (UI_FocusObject_Script != null)
        {
            switch (UI_FocusObject_Script._Basic_Source_Class.SourceType)
            {
                default:
                    {
                        UI_FocusObject_Script._View_EffectStore_Transform.SetParent(UI_FocusObject_Script._View_Store_Transform);
                    }
                    break;
            }
        }
        if (UI_FocusObject_Script == this)
        {
            return;
        }
        switch (Object._Basic_Source_Class.SourceType)
        {
            case "Concept":
                {
                    _Object_CreatureUnit QuickSave_Creature_Script =
                        Object._Basic_Source_Class.Source_Creature;
                    _Item_ConceptUnit QuickSave_Concept_Script =
                        QuickSave_Creature_Script._Object_Inventory_Script._Item_EquipConcepts_Script;
                    QuickSave_FocusOne_Transform = QuickSave_Creature_Script.transform;

                    //設置名稱
                    _View_Name_Text.text =
                        QuickSave_Creature_Script._Basic_Language_Class.Name;
                    _View_SubName_Text.text = "";
                    _View_State_Text.text = "";
                    //設置圖片
                    _View_Image_Image.sprite = 
                        _World_Manager._View_Manager.GetSprite("Creature", "SideFiguar", QuickSave_Creature_Script._Basic_Key_String);


                    UI_FocusObject_Script = Object;
                    Object.MediumPointView();
                    Object.CatalystPointView();
                    Object.ConsciousnessPointView();
                    //效果
                    Object._View_EffectStore_Transform.SetParent(_View_EffectStore_Transform);
                    Object._View_EffectStore_Transform.localPosition = Vector3.zero;
                    Object._View_EffectStore_Transform.localScale = Vector3.one;
                }
                break;
            case "Weapon":
                {
                    _Item_WeaponUnit QuickSave_Weapon_Script =
                        Object._Basic_Source_Class.Source_Weapon;
                    QuickSave_FocusOne_Transform = Object._Basic_Source_Class.Source_Weapon._Basic_Object_Script.transform;
                    //QuickSave_FocusOne_Transform = Object._Basic_Source_Class.Source_Weapon._Basic_Owner_Script.transform;

                    //設置名稱
                    _View_Name_Text.text =
                        Object._Basic_Language_Class.Name;
                    if (QuickSave_UIName_Dictionary.TryGetValue(Object._Basic_SubNameSave_String, out string SubNameValue))
                    {
                        _View_SubName_Text.text = SubNameValue;
                    }
                    else
                    {
                        _View_SubName_Text.text = "";
                    }
                    if (QuickSave_UIName_Dictionary.TryGetValue(Object._Map_ObjectState_String, out string StateValue))
                    {
                        _View_State_Text.text = StateValue;
                    }
                    else
                    {
                        _View_State_Text.text = "";
                    }
                    //設置圖片
                    _View_Image_Image.sprite = 
                        _World_Manager._View_Manager.GetSprite("Weapon", "SideFiguar", Object._Basic_Key_String);

                    UI_FocusObject_Script = Object;
                    Object.MediumPointView();
                    Object.CatalystPointView();
                    Object.ConsciousnessPointView();
                    //效果
                    Object._View_EffectStore_Transform.SetParent(_View_EffectStore_Transform);
                    Object._View_EffectStore_Transform.localPosition = Vector3.zero;
                    Object._View_EffectStore_Transform.localScale = Vector3.one;
                }
                break;
            case "Item":
                {
                    _Item_ItemUnit QuickSave_Item_Script =
                        Object._Basic_Source_Class.Source_Item;
                    QuickSave_FocusOne_Transform = Object._Basic_Source_Class.Source_Item._Basic_Object_Script.transform;
                    //QuickSave_FocusOne_Transform = Object._Basic_Source_Class.Source_Item._Basic_Owner_Script.transform;

                    _View_Name_Text.text =
                        Object._Basic_Language_Class.Name;
                    if (QuickSave_UIName_Dictionary.TryGetValue(Object._Basic_SubNameSave_String, out string SubNameValue))
                    {
                        _View_SubName_Text.text = SubNameValue;
                    }
                    else
                    {
                        _View_SubName_Text.text = "";
                    }
                    if (QuickSave_UIName_Dictionary.TryGetValue(Object._Map_ObjectState_String, out string StateValue))
                    {
                        _View_State_Text.text = StateValue;
                    }
                    else
                    {
                        _View_State_Text.text = "";
                    }
                    //設置圖片
                    _View_Image_Image.sprite = 
                        _World_Manager._View_Manager.GetSprite("Item", "SideFiguar", Object._Basic_Key_String);

                    UI_FocusObject_Script = Object;
                    Object.MediumPointView();
                    Object.CatalystPointView();
                    Object.ConsciousnessPointView();
                    //效果
                    Object._View_EffectStore_Transform.SetParent(_View_EffectStore_Transform);
                    Object._View_EffectStore_Transform.localPosition = Vector3.zero;
                    Object._View_EffectStore_Transform.localScale = Vector3.one;
                }
                break;
            case "Object":
                {
                    _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
                    QuickSave_FocusOne_Transform = Object.transform;

                    _View_Name_Text.text =
                        _Object_Manager._Language_Object_Dictionary[Object._Basic_Key_String].Name;
                    if (QuickSave_UIName_Dictionary.TryGetValue(Object._Basic_SubNameSave_String, out string SubNameValue))
                    {
                        _View_SubName_Text.text = SubNameValue;
                    }
                    else
                    {
                        _View_SubName_Text.text = "";
                    }
                    if (QuickSave_UIName_Dictionary.TryGetValue(Object._Map_ObjectState_String, out string StateValue))
                    {
                        _View_State_Text.text = StateValue;
                    }
                    else
                    {
                        _View_State_Text.text = "";
                    }
                    //設置圖片
                    _View_Image_Image.sprite =
                        _World_Manager._View_Manager.GetSprite("Object", "SideFiguar", Object._Basic_Key_String);

                    UI_FocusObject_Script = Object;
                    Object.MediumPointView();
                    Object.CatalystPointView();
                    Object.ConsciousnessPointView();
                    //效果
                    Object._View_EffectStore_Transform.SetParent(_View_EffectStore_Transform);
                    Object._View_EffectStore_Transform.localPosition = Vector3.zero;
                    Object._View_EffectStore_Transform.localScale = Vector3.one;
                }
                break;
        }
        //設置目標
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (_World_Manager._Authority_Scene_String == "Battle")
        {
            _World_Manager._UI_Manager.
                ChangeTraceTarget(QuickSave_FocusOne_Transform);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
}
