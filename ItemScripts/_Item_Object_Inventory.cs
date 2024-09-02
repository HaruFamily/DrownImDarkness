using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class _Item_Object_Inventory : MonoBehaviour
{
    #region Element
    #region - DataElement -
    //變數集——————————————————————————————————————————————————————————————————————
    //變數欄----------------------------------------------------------------------------------------------------
    //持有者
    public _Object_CreatureUnit _Basic_Owner_Script;
    //武器
    public List<_Item_WeaponUnit> _Item_Weapons_ScriptsList = new List<_Item_WeaponUnit>();
    //道具
    public List<_Item_ItemUnit> _Item_Items_ScriptsList = new List<_Item_ItemUnit>();
    //概念
    public List<_Item_ConceptUnit> _Item_Concepts_ScriptsList = new List<_Item_ConceptUnit>();
    //持有素材
    public List<_Item_MaterialUnit> _Item_Materials_ScriptsList = new List<_Item_MaterialUnit>();//平常持有素材
    //塵晶(金錢)
    public int _Item_Dust_Int;
    //----------------------------------------------------------------------------------------------------

    //存在區----------------------------------------------------------------------------------------------------
    public Transform _Item_WeaponStore_Transform;
    public Transform _Item_ItemStore_Transform;
    public Transform _Item_ConceptStore_Transform;
    public Transform _Item_MaterialStore_Transform;
    //
    public Dictionary<string, List<Transform>> _Inventory_FilterHideSave_Dictionary = new Dictionary<string, List<Transform>>()
    {
        {"Weapons", new List<Transform>() },
        {"Items", new List<Transform>() },
        {"Collections", new List<Transform>() },
        {"Materials", new List<Transform>() },
        {"WeaponRecipe", new List<Transform>() },
        {"ItemRecipe", new List<Transform>() },
        {"Faction", new List<Transform>() }
    };
    //全體暫存區
    public Transform _Inventory_FilterStore_Transform;

    //招式
    //招式庫//檢視用
    public Transform _Skill_CardsStore_Transform;
    public Transform _Skill_EmptyStore_Transform;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    //裝備流派
    public NumbericalValueClass[] _Item_EquipStatus_ClassArray = new NumbericalValueClass[4];//Status["Size"]/Form/Weight/Purity
    public List<_Map_BattleObjectUnit> _Item_EquipQueue_ScriptsList = new List<_Map_BattleObjectUnit>();//裝備物

    public List<_Item_WeaponUnit> _Item_EquipWeapons_ScriptsList = new List<_Item_WeaponUnit>();
    public List<_Item_ItemUnit> _Item_EquipItems_ScriptsList = new List<_Item_ItemUnit>();
    public _Item_ConceptUnit _Item_EquipConcepts_Script;

    public List<_Map_BattleObjectUnit> _Item_Loots_ScriptsList = 
        new List<_Map_BattleObjectUnit>();//探索戰利品

    public List<_Map_BattleObjectUnit> _Item_CarryObject_ScriptsList =
        new List<_Map_BattleObjectUnit>();//攜帶物-會隨著一起移動-被丟出/移動時移除
    public List<_Map_BattleObjectUnit> _Item_ReachObject_ScriptsList = 
        new List<_Map_BattleObjectUnit>();//可觸及物體
    public List<_Map_BattleObjectUnit> _Item_DrivingObject_ScriptsList = 
        new List<_Map_BattleObjectUnit>();//驅動中物體
    public List<_Map_BattleObjectUnit> _Item_DrivingMapObject_ScriptsList =
        new List<_Map_BattleObjectUnit>();//驅動中的地圖物體-在離開戰鬥時清除
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion Element

    #region Start
    //——————————————————————————————————————————————————————————————————————
    public void Awake()
    {
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region MaterialSet

    #region - Concept -
    //取得材料——————————————————————————————————————————————————————————————————————
    public void InventorySet(string Situation, _Item_ConceptUnit Target)
    {
        //----------------------------------------------------------------------------------------------------
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            #region - Equip -
            case "Equip":
                {
                    if (_Item_EquipConcepts_Script != null)
                    {
                        SourceClass _Basic_EquipSource_Class = _Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class;
                        _Item_EquipQueue_ScriptsList.Remove(_Item_EquipConcepts_Script._Basic_Object_Script);
                        _Item_EquipConcepts_Script._View_Hint_Script.HintSet("UnUsing", "Concept");
                        _Item_EquipConcepts_Script._Basic_Object_Script.transform.SetParent(_Item_EquipConcepts_Script.transform);
                        _Item_EquipConcepts_Script._Basic_Object_Script.transform.localPosition = Vector3.zero;
                        _Item_EquipConcepts_Script._Basic_Object_Script.transform.localScale = Vector3.zero;
                        //裝備佔有
                        if (_Basic_Owner_Script._Player_Script != null)
                        {
                            _Item_EquipStatus_ClassArray[0].Point -=
                            _Item_EquipConcepts_Script._Basic_Object_Script.Key_Material("Size", _Basic_EquipSource_Class, _Basic_EquipSource_Class);
                            _Item_EquipStatus_ClassArray[1].Point -=
                                _Item_EquipConcepts_Script._Basic_Object_Script.Key_Material("Form", _Basic_EquipSource_Class, _Basic_EquipSource_Class);
                            _Item_EquipStatus_ClassArray[2].Point -=
                                _Item_EquipConcepts_Script._Basic_Object_Script.Key_Material("Weight", _Basic_EquipSource_Class, _Basic_EquipSource_Class);
                            _Item_EquipStatus_ClassArray[3].Point -=
                                _Item_EquipConcepts_Script._Basic_Object_Script.Key_Material("Purity", _Basic_EquipSource_Class, _Basic_EquipSource_Class);
                        }
                    }
                    SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                    _Item_EquipConcepts_Script = Target;
                    _Item_EquipQueue_ScriptsList.Add(Target._Basic_Object_Script);
                    _Item_EquipQueue_ScriptsList.Sort(new SourceTypeComparer("Reverse"));
                    Target._Basic_Object_Script.
                        StateSet("Affiliation",_Basic_Source_Class, 
                        null ,true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    //裝備佔有
                    if (_Basic_Owner_Script._Player_Script != null)
                    {
                        _Item_EquipStatus_ClassArray[0].Point +=
                            Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[1].Point +=
                            Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[2].Point +=
                            Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[3].Point +=
                            Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class);
                        Target._View_Hint_Script.HintSet("Using", "Concept");
                    }

                    Transform QuickSave_Store_Transform = _Object_Manager._Object_ObjectStore_Transform;
                    if (_Basic_Owner_Script._Player_Script != null)
                    {
                        QuickSave_Store_Transform = _Object_Manager._Object_PlayerObjectStore_Transform;
                    }
                    Target._Basic_Object_Script.transform.SetParent(QuickSave_Store_Transform);
                    Target._Basic_Object_Script.transform.localPosition = Vector3.zero;
                    Target._Basic_Object_Script.transform.localScale = Vector3.one;

                    List<string> QuickSave_Avdance_StringList =
                        new List<string> { "Medium", "Catalyst", "Consciousness" };
                    foreach (_Item_WeaponUnit Weapon in _Item_EquipWeapons_ScriptsList)
                    {
                        _Item_Manager.ObjectStatusAdvanceSet(Weapon._Basic_Object_Script, QuickSave_Avdance_StringList);
                    }
                    foreach (_Item_ItemUnit Item in _Item_EquipItems_ScriptsList)
                    {
                        _Item_Manager.ObjectStatusAdvanceSet(Item._Basic_Object_Script, QuickSave_Avdance_StringList);
                    }
                    _Object_Manager.CreatureStatusAdvanceSet(_Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class,
                        new List<string> { "Medium", "Catalyst", "Consciousness", 
                            "Vitality", "Strength", "Precision", "Speed", "Luck" });
                }
                break;
            #endregion

            #region - Add -
            case "Add":
                if (_Basic_Owner_Script._Player_Script != null)
                {
                    Target._View_Hint_Script.HintSet("New", "Concept");
                }
                switch (_World_Manager._Authority_Scene_String)
                {
                    case "Camp":
                        _Item_Concepts_ScriptsList.Add(Target);
                        break;
                    default:
                        {
                            SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                            _Item_ConceptUnit QuickSave_Concept_Script =
                                _Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_EquipConcepts_Script;
                            //裝備佔有
                            if (_Basic_Owner_Script._Player_Script != null)
                            {
                                _Item_EquipStatus_ClassArray[0].Point +=
                                    Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Size");
                                _Item_EquipStatus_ClassArray[1].Point +=
                                    Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Form");
                                _Item_EquipStatus_ClassArray[2].Point +=
                                    Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Weight");
                                _Item_EquipStatus_ClassArray[3].Point +=
                                    Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Purity");
                            }
                            _Item_Loots_ScriptsList.Add(Target._Basic_Object_Script);
                        }
                        break;
                }
                break;
            #endregion

            #region - Remove -
            case "Remove":
                if (_World_Manager._UI_Manager._UI_Hint_Dictionary["New"]["Concept"].Contains(Target._View_Hint_Script))
                {
                    Target._View_Hint_Script.HintSet("UnNew", "Concept");
                }
                _Item_Concepts_ScriptsList.Remove(Target);
                break;
            #endregion

            #region - Destroy -
            case "Destroy":
                {
                    if (_World_Manager._UI_Manager._UI_Hint_Dictionary["New"]["Concept"].Contains(Target._View_Hint_Script))
                    {
                        Target._View_Hint_Script.HintSet("UnNew", "Concept");
                    }
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Camp":
                            _Item_Concepts_ScriptsList.Remove(Target);
                            break;
                        default:
                            {
                                SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                                _Item_ConceptUnit QuickSave_Concept_Script =
                                    _Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_EquipConcepts_Script;
                                //裝備佔有
                                if (_Basic_Owner_Script._Player_Script != null)
                                {
                                    _Item_EquipStatus_ClassArray[0].Point -=
                                    Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Size");
                                    _Item_EquipStatus_ClassArray[1].Point -=
                                        Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Form");
                                    _Item_EquipStatus_ClassArray[2].Point -=
                                        Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Weight");
                                    _Item_EquipStatus_ClassArray[3].Point -=
                                        Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Purity");
                                }
                                _Item_Loots_ScriptsList.Remove(Target._Basic_Object_Script);
                            }
                            break;
                    }
                    foreach (_Effect_EffectObjectUnit Effect in
                        Target._Basic_Object_Script._Effect_Effect_Dictionary.Values)
                    {
                        Effect.Destroy();
                    }
                    Destroy(Target.gameObject);
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Weapon -
    //取得材料——————————————————————————————————————————————————————————————————————
    public void InventorySet(string Situation, _Item_WeaponUnit Target)
    {
        //----------------------------------------------------------------------------------------------------
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            #region - Equip -
            case "Equip":
                {
                    SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                    _Item_EquipWeapons_ScriptsList.Add(Target);
                    _Item_EquipQueue_ScriptsList.Add(Target._Basic_Object_Script);
                    _Item_EquipQueue_ScriptsList.Sort(new SourceTypeComparer("Reverse"));
                    Target._Basic_Object_Script.
                        StateSet("Affiliation",_Basic_Source_Class, 
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    //裝備佔有
                    if (_Basic_Owner_Script._Player_Script != null)
                    {
                        _Item_EquipStatus_ClassArray[0].Point +=
                            Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[1].Point +=
                            Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[2].Point +=
                            Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[3].Point +=
                            Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class);
                        Target._View_Hint_Script.HintSet("Using", "Weapon");
                    }
                    Transform QuickSave_Store_Transform = _Object_Manager._Object_ObjectStore_Transform;
                    if (_Basic_Owner_Script._Player_Script != null)
                    {
                        QuickSave_Store_Transform = _Object_Manager._Object_PlayerObjectStore_Transform;
                    }
                    Target._Basic_Object_Script.transform.SetParent(QuickSave_Store_Transform);
                    Target._Basic_Object_Script.transform.localPosition = Vector3.zero;
                    Target._Basic_Object_Script.transform.localScale = Vector3.one;

                    List<string> QuickSave_Avdance_StringList =
                        new List<string> { "Medium", "Catalyst", "Consciousness" };
                    foreach (_Item_WeaponUnit Weapon in _Item_EquipWeapons_ScriptsList)
                    {
                        _Item_Manager.ObjectStatusAdvanceSet(Weapon._Basic_Object_Script, QuickSave_Avdance_StringList);
                    }
                    foreach (_Item_ItemUnit Item in _Item_EquipItems_ScriptsList)
                    {
                        _Item_Manager.ObjectStatusAdvanceSet(Item._Basic_Object_Script, QuickSave_Avdance_StringList);
                    }
                    _Object_Manager.CreatureStatusAdvanceSet(_Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class,
                        new List<string> { "Medium", "Catalyst", "Consciousness", 
                            "Vitality", "Strength", "Precision", "Speed", "Luck" });
                }
                break;
            case "RemoveEquip":
                {
                    SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                    _Item_EquipWeapons_ScriptsList.Remove(Target);
                    _Item_EquipQueue_ScriptsList.Remove(Target._Basic_Object_Script);
                    _Item_EquipQueue_ScriptsList.Sort(new SourceTypeComparer("Reverse"));
                    //裝備佔有
                    if (_Basic_Owner_Script._Player_Script != null)
                    {
                        _Item_EquipStatus_ClassArray[0].Point -=
                            Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[1].Point -=
                            Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[2].Point -=
                            Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[3].Point -=
                            Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class);
                        Target._View_Hint_Script.HintSet("UnUsing", "Weapon");
                    }
                    Target._Basic_Object_Script.transform.SetParent(Target.transform);
                    Target._Basic_Object_Script.transform.localPosition = Vector3.zero;
                    Target._Basic_Object_Script.transform.localScale = Vector3.zero;

                    List<string> QuickSave_Avdance_StringList =
                        new List<string> { "Medium", "Catalyst", "Consciousness" };
                    for (int a = 0; a < _Item_EquipWeapons_ScriptsList.Count; a++)
                    {
                        _Item_Manager.ObjectStatusAdvanceSet(_Item_EquipWeapons_ScriptsList[a]._Basic_Object_Script, QuickSave_Avdance_StringList);
                    }
                    for (int a = 0; a < _Item_EquipItems_ScriptsList.Count; a++)
                    {
                        _Item_Manager.ObjectStatusAdvanceSet(_Item_EquipItems_ScriptsList[a]._Basic_Object_Script, QuickSave_Avdance_StringList);
                    }
                    _Object_Manager.CreatureStatusAdvanceSet(_Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class,
                        new List<string> { "Medium", "Catalyst", "Consciousness", 
                            "Vitality", "Strength", "Precision", "Speed", "Luck" });
                }
                break;
            #endregion

            #region - Add -
            case "Add":
                if (_Basic_Owner_Script._Player_Script != null)
                {
                    Target._View_Hint_Script.HintSet("New", "Weapon");
                }
                switch (_World_Manager._Authority_Scene_String)
                {
                    case "Camp":
                        _Item_Weapons_ScriptsList.Add(Target);
                        break;
                    default:
                        {
                            SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                            _Item_ConceptUnit QuickSave_Concept_Script =
                                _Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_EquipConcepts_Script;
                            //裝備佔有
                            if (_Basic_Owner_Script._Player_Script != null)
                            {
                                _Item_EquipStatus_ClassArray[0].Point +=
                                    Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Size");
                                _Item_EquipStatus_ClassArray[1].Point +=
                                    Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Form");
                                _Item_EquipStatus_ClassArray[2].Point +=
                                    Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Weight");
                                _Item_EquipStatus_ClassArray[3].Point +=
                                    Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Purity");
                            }
                            _Item_Loots_ScriptsList.Add(Target._Basic_Object_Script);
                        }
                        break;
                }
                break;
            #endregion

            #region - Remove -
            case "Remove":
                if (_World_Manager._UI_Manager._UI_Hint_Dictionary["New"]["Weapon"].Contains(Target._View_Hint_Script))
                {
                    Target._View_Hint_Script.HintSet("UnNew","Weapon");
                }
                _Item_Weapons_ScriptsList.Remove(Target);
                break;
            #endregion

            #region - Destroy -
            case "Destroy":
                {
                    if (_World_Manager._UI_Manager._UI_Hint_Dictionary["New"]["Weapon"].Contains(Target._View_Hint_Script))
                    {
                        Target._View_Hint_Script.HintSet("UnNew", "Weapon");
                    }
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Camp":
                            _Item_Weapons_ScriptsList.Remove(Target);
                            break;
                        default:
                            {
                                SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                                _Item_ConceptUnit QuickSave_Concept_Script =
                                    _Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_EquipConcepts_Script;
                                //裝備佔有
                                if (_Basic_Owner_Script._Player_Script != null)
                                {
                                    _Item_EquipStatus_ClassArray[0].Point -=
                                    Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Size");
                                    _Item_EquipStatus_ClassArray[1].Point -=
                                        Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Form");
                                    _Item_EquipStatus_ClassArray[2].Point -=
                                        Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Weight");
                                    _Item_EquipStatus_ClassArray[3].Point -=
                                        Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Purity");
                                }
                                _Item_Loots_ScriptsList.Remove(Target._Basic_Object_Script);
                            }
                            break;
                    }
                    foreach (_Effect_EffectObjectUnit Effect in
                        Target._Basic_Object_Script._Effect_Effect_Dictionary.Values)
                    {
                        Effect.Destroy();
                    }
                    Destroy(Target.gameObject);
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Item -
    //取得材料——————————————————————————————————————————————————————————————————————
    public void InventorySet(string Situation, _Item_ItemUnit Target)
    {
        //----------------------------------------------------------------------------------------------------
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            #region - Equip -
            case "Equip":
                {
                    SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                    _Item_EquipItems_ScriptsList.Add(Target);
                    _Item_EquipQueue_ScriptsList.Add(Target._Basic_Object_Script);
                    _Item_EquipQueue_ScriptsList.Sort(new SourceTypeComparer("Reverse"));
                    Target._Basic_Object_Script.
                        StateSet("Affiliation",_Basic_Source_Class, 
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    //裝備佔有
                    if (_Basic_Owner_Script._Player_Script != null)
                    {
                        _Item_EquipStatus_ClassArray[0].Point +=
                            Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[1].Point +=
                            Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[2].Point +=
                            Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[3].Point +=
                            Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class);
                        Target._View_Hint_Script.HintSet("Using", "Item");
                    }
                    Transform QuickSave_Store_Transform = _Object_Manager._Object_ObjectStore_Transform;
                    if (_Basic_Owner_Script._Player_Script != null)
                    {
                        QuickSave_Store_Transform = _Object_Manager._Object_PlayerObjectStore_Transform;
                    }
                    Target._Basic_Object_Script.transform.SetParent(QuickSave_Store_Transform);
                    Target._Basic_Object_Script.transform.localPosition = Vector3.zero;
                    Target._Basic_Object_Script.transform.localScale = Vector3.one;

                    List<string> QuickSave_Avdance_StringList =
                        new List<string> { "Medium", "Catalyst", "Consciousness" };
                    foreach (_Item_WeaponUnit Weapon in _Item_EquipWeapons_ScriptsList)
                    {
                        _Item_Manager.ObjectStatusAdvanceSet(Weapon._Basic_Object_Script, QuickSave_Avdance_StringList);
                    }
                    foreach (_Item_ItemUnit Item in _Item_EquipItems_ScriptsList)
                    {
                        _Item_Manager.ObjectStatusAdvanceSet(Item._Basic_Object_Script, QuickSave_Avdance_StringList);
                    }
                    _Object_Manager.CreatureStatusAdvanceSet(_Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class,
                        new List<string> { "Medium", "Catalyst", "Consciousness", 
                            "Vitality", "Strength", "Precision", "Speed", "Luck" });
                }
                break;
            case "RemoveEquip":
                {
                    SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                    _Item_EquipItems_ScriptsList.Remove(Target);
                    _Item_EquipQueue_ScriptsList.Remove(Target._Basic_Object_Script);
                    _Item_EquipQueue_ScriptsList.Sort(new SourceTypeComparer("Reverse"));
                    //裝備佔有
                    if (_Basic_Owner_Script._Player_Script != null)
                    {
                        _Item_EquipStatus_ClassArray[0].Point -=
                            Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[1].Point -=
                            Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[2].Point -=
                            Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class);
                        _Item_EquipStatus_ClassArray[3].Point -=
                            Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class);
                        Target._View_Hint_Script.HintSet("UnUsing", "Item");
                    }
                    Target._Basic_Object_Script.transform.SetParent(Target.transform);
                    Target._Basic_Object_Script.transform.localPosition = Vector3.zero;
                    Target._Basic_Object_Script.transform.localScale = Vector3.zero;

                    List<string> QuickSave_Avdance_StringList =
                        new List<string> { "Medium", "Catalyst", "Consciousness" };
                    for (int a = 0; a < _Item_EquipWeapons_ScriptsList.Count; a++)
                    {
                        _Item_Manager.ObjectStatusAdvanceSet(_Item_EquipWeapons_ScriptsList[a]._Basic_Object_Script, QuickSave_Avdance_StringList);
                    }
                    for (int a = 0; a < _Item_EquipItems_ScriptsList.Count; a++)
                    {
                        _Item_Manager.ObjectStatusAdvanceSet(_Item_EquipItems_ScriptsList[a]._Basic_Object_Script, QuickSave_Avdance_StringList);
                    }
                    _Object_Manager.CreatureStatusAdvanceSet(_Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class,
                        new List<string> { "Medium", "Catalyst", "Consciousness", 
                            "Vitality", "Strength", "Precision", "Speed", "Luck" });
                }
                break;
            #endregion

            #region - Add -
            case "Add":
                if (_Basic_Owner_Script._Player_Script != null)
                {
                    Target._View_Hint_Script.HintSet("New", "Item");
                }
                switch (_World_Manager._Authority_Scene_String)
                {
                    case "Camp":
                        _Item_Items_ScriptsList.Add(Target);
                        break;
                    default:
                        {
                            SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                            _Item_ConceptUnit QuickSave_Concept_Script =
                                _Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_EquipConcepts_Script;
                            //裝備佔有
                            if (_Basic_Owner_Script._Player_Script != null)
                            {
                                _Item_EquipStatus_ClassArray[0].Point +=
                                    Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Size");
                                _Item_EquipStatus_ClassArray[1].Point +=
                                    Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Form");
                                _Item_EquipStatus_ClassArray[2].Point +=
                                    Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Weight");
                                _Item_EquipStatus_ClassArray[3].Point +=
                                    Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Purity");
                            }
                            _Item_Loots_ScriptsList.Add(Target._Basic_Object_Script);
                        }
                        break;
                }
                break;
            #endregion

            #region - Remove -
            case "Remove":
                if (_World_Manager._UI_Manager._UI_Hint_Dictionary["New"]["Item"].Contains(Target._View_Hint_Script))
                {
                    Target._View_Hint_Script.HintSet("UnNew", "Item");
                }
                _Item_Items_ScriptsList.Remove(Target);
                break;
            #endregion

            #region - Destroy -
            case "Destroy":
                {
                    if (_World_Manager._UI_Manager._UI_Hint_Dictionary["New"]["Item"].Contains(Target._View_Hint_Script))
                    {
                        Target._View_Hint_Script.HintSet("UnNew", "Material");
                    }
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Camp":
                            _Item_Items_ScriptsList.Remove(Target);
                            break;
                        default:
                            {
                                SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                                _Item_ConceptUnit QuickSave_Concept_Script =
                                    _Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_EquipConcepts_Script;
                                //裝備佔有
                                if (_Basic_Owner_Script._Player_Script != null)
                                {
                                    _Item_EquipStatus_ClassArray[0].Point -=
                                    Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Size");
                                    _Item_EquipStatus_ClassArray[1].Point -=
                                        Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Form");
                                    _Item_EquipStatus_ClassArray[2].Point -=
                                        Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Weight");
                                    _Item_EquipStatus_ClassArray[3].Point -=
                                        Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Purity");
                                }
                                _Item_Loots_ScriptsList.Remove(Target._Basic_Object_Script);
                            }
                            break;
                    }
                    foreach (_Effect_EffectObjectUnit Effect in
                        Target._Basic_Object_Script._Effect_Effect_Dictionary.Values)
                    {
                        Effect.Destroy();
                    }
                    Destroy(Target.gameObject);
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Material -
    //取得材料——————————————————————————————————————————————————————————————————————
    public void InventorySet(string Situation, _Item_MaterialUnit Target)
    {
        //----------------------------------------------------------------------------------------------------
        string QuickSave_Key_String = Target._Basic_Object_Script._Basic_Key_String;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            #region - Add -
            case "Add":
                {
                    if (_Basic_Owner_Script._Player_Script != null)
                    {
                        Target._View_Hint_Script.HintSet("New", "Material");
                    }
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Camp":
                            _Item_Materials_ScriptsList.Add(Target);
                            break;
                        default:
                            {
                                SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                                _Item_ConceptUnit QuickSave_Concept_Script =
                                _Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_EquipConcepts_Script;
                                //裝備佔有
                                if (_Basic_Owner_Script._Player_Script != null)
                                {
                                    _Item_EquipStatus_ClassArray[0].Point +=
                                        Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Size");
                                    _Item_EquipStatus_ClassArray[1].Point +=
                                        Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Form");
                                    _Item_EquipStatus_ClassArray[2].Point +=
                                        Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Weight");
                                    _Item_EquipStatus_ClassArray[3].Point +=
                                        Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Purity");
                                }
                                _Item_Loots_ScriptsList.Add(Target._Basic_Object_Script);
                            }
                            break;
                    }
                }
                break;
            #endregion

            #region - Remove -
            case "Remove":
                {
                    if (_World_Manager._UI_Manager._UI_Hint_Dictionary["New"]["Material"].Contains(Target._View_Hint_Script))
                    {
                        Target._View_Hint_Script.HintSet("UnNew", "Material");
                    }
                    _Item_Materials_ScriptsList.Remove(Target);
                }
                break;
            #endregion

            #region - Destroy -
            case "Destroy":
                {
                    if (_World_Manager._UI_Manager._UI_Hint_Dictionary["New"]["Material"].Contains(Target._View_Hint_Script))
                    {
                        Target._View_Hint_Script.HintSet("UnNew", "Material");
                    }
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Camp":
                            _Item_Materials_ScriptsList.Remove(Target);
                            break;
                        default:
                            {
                                SourceClass _Basic_Source_Class = Target._Basic_Object_Script._Basic_Source_Class;
                                _Item_ConceptUnit QuickSave_Concept_Script =
                                    _Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_EquipConcepts_Script;
                                //裝備佔有
                                if (_Basic_Owner_Script._Player_Script != null)
                                {
                                    _Item_EquipStatus_ClassArray[0].Point -=
                                    Target._Basic_Object_Script.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class) *
                                    QuickSave_Concept_Script.Key_Occupancy("Size");
                                    _Item_EquipStatus_ClassArray[1].Point -=
                                        Target._Basic_Object_Script.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Form");
                                    _Item_EquipStatus_ClassArray[2].Point -=
                                        Target._Basic_Object_Script.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Weight");
                                    _Item_EquipStatus_ClassArray[3].Point -=
                                        Target._Basic_Object_Script.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class) *
                                        QuickSave_Concept_Script.Key_Occupancy("Purity");
                                }
                                _Item_Loots_ScriptsList.Remove(Target._Basic_Object_Script);
                            }
                            break;
                    }
                    foreach (_Effect_EffectObjectUnit Effect in
                        Target._Basic_Object_Script._Effect_Effect_Dictionary.Values)
                    {
                        Effect.Destroy();
                    }
                    Destroy(Target.gameObject);
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion MaterialSet

    #region Item
    #region Filter
    //——————————————————————————————————————————————————————————————————————
    public void ItemFilterSet(string Type, string ActionType = "",
        _Item_Manager.RecipeDataClass RecipeFilter = null)
    {
        //變數----------------------------------------------------------------------------------------------------
        List<Transform> QuickSave_Transform_TransformList = new List<Transform>();
        //----------------------------------------------------------------------------------------------------

        //選拔----------------------------------------------------------------------------------------------------
        //釋放過去
        ItemInventorySort(Type, "", false);
        if (ActionType == "")
        {
            return;
        }
        switch (Type)
        {
            case "Weapons":
                foreach (_Item_WeaponUnit Weapon in _Item_Weapons_ScriptsList)
                {
                    if (!ItemFilterMatch(Type, ActionType, Wepaon: Weapon))
                    {
                        QuickSave_Transform_TransformList.Add(Weapon.transform);
                    }
                }
                break;
            case "Items":
                foreach (_Item_ItemUnit Item in _Item_Items_ScriptsList)
                {
                    if (!ItemFilterMatch(Type, ActionType, Item: Item))
                    {
                        QuickSave_Transform_TransformList.Add(Item.transform);
                    }
                }
                break;
            case "Concepts":
                foreach (_Item_ConceptUnit Concept in _Item_Concepts_ScriptsList)
                {
                    if (!ItemFilterMatch(Type, ActionType, Concept: Concept))
                    {
                        QuickSave_Transform_TransformList.Add(Concept.transform);
                    }
                }
                break;
            case "Materials":
                foreach (_Item_MaterialUnit Material in _Item_Materials_ScriptsList)
                {
                    if (!ItemFilterMatch(Type, ActionType, Material: Material, RecipeFilter: RecipeFilter))
                    {
                        QuickSave_Transform_TransformList.Add(Material.transform);
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //設置----------------------------------------------------------------------------------------------------
        if (QuickSave_Transform_TransformList.Count > 0)
        {
            for (int a = 0; a < QuickSave_Transform_TransformList.Count; a++)
            {
                QuickSave_Transform_TransformList[a].SetParent(_Inventory_FilterStore_Transform);
                _Inventory_FilterHideSave_Dictionary[Type].Add(QuickSave_Transform_TransformList[a]);
            }
        }
        //----------------------------------------------------------------------------------------------------
    }

    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public bool ItemFilterMatch(string Type, string ActionType, 
        _Item_WeaponUnit Wepaon = null,_Item_ItemUnit Item = null, _Item_ConceptUnit Concept = null,_Item_MaterialUnit Material = null, 
        _Item_Manager.RecipeDataClass RecipeFilter = null)
    {
        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Weapons":
                switch (ActionType)
                {
                    case "Equipment":
                        for (int a  =0; a < _Item_EquipWeapons_ScriptsList.Count; a++)
                        {
                            if (_Item_EquipWeapons_ScriptsList[a] == Wepaon)
                            {
                                return false;
                            }
                        }
                        break;
                }
                break;
            case "Items":
                switch (ActionType)
                {
                    case "Equipment":
                        for (int a = 0; a < _Item_EquipItems_ScriptsList.Count; a++)
                        {
                            if (_Item_EquipItems_ScriptsList[a] == Item)
                            {
                                return false;
                            }
                        }
                        break;
                }
                break;
            case "Concepts":
                switch (ActionType)
                {
                    case "Equipment":
                        if (_Item_EquipConcepts_Script == Concept)
                        {
                            return false;
                        }
                        break;
                }
                break;

            case "Materials":
                switch (ActionType)
                {
                    case "Recipe":
                        {
                            _Map_BattleObjectUnit QuickSave_Object_Script =
                                Material._Basic_Object_Script;
                            SourceClass QuickSave_Source_Class = 
                                QuickSave_Object_Script._Basic_Source_Class;
                            switch (RecipeFilter.Type)
                            {
                                case "Class":
                                    //類型判定
                                    if (!QuickSave_Object_Script._Basic_Material_Class.Tag.Contains(RecipeFilter.Key))
                                    {
                                        return false;
                                    }
                                    break;
                                case "Target":
                                    //編號判定
                                    if (QuickSave_Object_Script._Basic_Key_String != RecipeFilter.Key)
                                    {
                                        return false;
                                    }
                                    break;
                            }
                            //數值判定
                            int QuickSave_Size_Int =
                                QuickSave_Object_Script.Key_Material("Size", QuickSave_Source_Class, null);
                            int QuickSave_Form_Int =
                                QuickSave_Object_Script.Key_Material("Form", QuickSave_Source_Class, null);
                            int QuickSave_Weight_Int =
                                QuickSave_Object_Script.Key_Material("Weight", QuickSave_Source_Class, null);
                            int QuickSave_Purity_Int =
                                QuickSave_Object_Script.Key_Material("Purity", QuickSave_Source_Class, null);
                            if (QuickSave_Size_Int < RecipeFilter.Size.Min ||
                                QuickSave_Size_Int > RecipeFilter.Size.Max)
                            {
                                return false;
                            }
                            if (QuickSave_Form_Int < RecipeFilter.Form.Min ||
                                QuickSave_Form_Int > RecipeFilter.Form.Max)
                            {
                                return false;
                            }
                            if (QuickSave_Weight_Int < RecipeFilter.Weight.Min ||
                                QuickSave_Weight_Int > RecipeFilter.Weight.Max)
                            {
                                return false;
                            }
                            if (QuickSave_Purity_Int < RecipeFilter.Purity.Min ||
                                QuickSave_Purity_Int > RecipeFilter.Purity.Max)
                            {
                                return false;
                            }
                        }
                        break;
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return true;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region Sort
    //回歸Store——————————————————————————————————————————————————————————————————————
    public void ItemInventorySort(string Type, string Compare, bool IgnoreHide = true)
    {
        //----------------------------------------------------------------------------------------------------
        List<GameObject> QuickSave_Objects_GameObjectsList = new List<GameObject>();
        //----------------------------------------------------------------------------------------------------

        //分類----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Concept":
                foreach (_Item_ConceptUnit Concept in _Item_Concepts_ScriptsList)
                {
                    QuickSave_Objects_GameObjectsList.Add(Concept.gameObject);
                }

                switch (Compare)
                {
                    default:
                        for (int a = 0; a < QuickSave_Objects_GameObjectsList.Count; a++)
                        {
                            QuickSave_Objects_GameObjectsList[a].transform.SetParent(_Item_ConceptStore_Transform);
                            QuickSave_Objects_GameObjectsList[a].transform.localPosition =
                                new Vector3(QuickSave_Objects_GameObjectsList[a].transform.localPosition.x, QuickSave_Objects_GameObjectsList[a].transform.localPosition.y, 0);
                        }
                        break;
                }
                break;
            case "Weapons":
                foreach (_Item_WeaponUnit Weapon in _Item_Weapons_ScriptsList)
                {
                    QuickSave_Objects_GameObjectsList.Add(Weapon.gameObject);
                }
                switch (Compare)
                {
                    default:
                        for (int a = 0; a < QuickSave_Objects_GameObjectsList.Count; a++)
                        {
                            QuickSave_Objects_GameObjectsList[a].transform.SetParent(_Item_WeaponStore_Transform);
                            QuickSave_Objects_GameObjectsList[a].transform.localPosition =
                                new Vector3(QuickSave_Objects_GameObjectsList[a].transform.localPosition.x, QuickSave_Objects_GameObjectsList[a].transform.localPosition.y, 0);
                        }
                        break;
                }
                break;
            case "Items":
                foreach (_Item_ItemUnit Item in _Item_Items_ScriptsList)
                {
                    QuickSave_Objects_GameObjectsList.Add(Item.gameObject);
                }

                switch (Compare)
                {
                    default:
                        for (int a = 0; a < QuickSave_Objects_GameObjectsList.Count; a++)
                        {
                            QuickSave_Objects_GameObjectsList[a].transform.SetParent(_Item_ItemStore_Transform);
                            QuickSave_Objects_GameObjectsList[a].transform.localPosition =
                                new Vector3(QuickSave_Objects_GameObjectsList[a].transform.localPosition.x, QuickSave_Objects_GameObjectsList[a].transform.localPosition.y, 0);
                        }
                        break;
                }
                break;
            case "Materials":
                foreach (_Item_MaterialUnit Material in _Item_Materials_ScriptsList)
                {
                    QuickSave_Objects_GameObjectsList.Add(Material.gameObject);
                }

                switch (Compare)
                {
                    default:
                        for (int a = 0; a< QuickSave_Objects_GameObjectsList.Count; a++)
                        {
                            QuickSave_Objects_GameObjectsList[a].transform.SetParent(_Item_MaterialStore_Transform);
                            QuickSave_Objects_GameObjectsList[a].transform.localPosition =
                                new Vector3(QuickSave_Objects_GameObjectsList[a].transform.localPosition.x, QuickSave_Objects_GameObjectsList[a].transform.localPosition.y, 0);
                        }
                        break;
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Sort
    #endregion Item
    /*
    #region Skill
    #region Filter
    //——————————————————————————————————————————————————————————————————————
    public void SkillFilterSet(string Type, string ActionType = "",
        _Item_WeaponUnit Weapon = null)
    {
        //變數----------------------------------------------------------------------------------------------------
        List<Transform> QuickSave_Transform_TransformList = new List<Transform>();
        //----------------------------------------------------------------------------------------------------

        //選拔----------------------------------------------------------------------------------------------------
        //釋放過去
        SkillInventorySort(Type, "", false);
        if (ActionType == "")
        {
            return;
        }
        switch (Type)
        {
            case "Faction":
                foreach (string Key in _Basic_Owner_Script._Skill_LearnSkill_Dictionary.Keys)
                {
                    switch (ActionType)
                    {
                        case "Type_Creature":
                            if (!SkillFilterMatch(Type, ActionType, Key))
                            {
                                QuickSave_Transform_TransformList.Add(_Basic_Owner_Script._Skill_LearnSkill_Dictionary[Key].transform);
                            }
                            break;
                        case "Allow_Weapon":
                            if (!SkillFilterMatch(Type, ActionType, Key, Weapon:Weapon))
                            {
                                QuickSave_Transform_TransformList.Add(_Basic_Owner_Script._Skill_LearnSkill_Dictionary[Key].transform);
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //設置----------------------------------------------------------------------------------------------------
        if (QuickSave_Transform_TransformList.Count > 0)
        {
            for (int a = 0; a < QuickSave_Transform_TransformList.Count; a++)
            {
                QuickSave_Transform_TransformList[a].SetParent(_Inventory_FilterStore_Transform);
                _Inventory_FilterHideSave_Dictionary[Type].Add(QuickSave_Transform_TransformList[a]);
            }
        }
        //----------------------------------------------------------------------------------------------------
    }

    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public bool SkillFilterMatch(string Type, string ActionType,string Key, _Item_WeaponUnit Weapon = null)
    {
        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Faction":
                switch (ActionType)
                {
                    case "Type_Creature":
                        if (_Basic_Owner_Script._Skill_LearnSkill_Dictionary[Key]._Basic_Data_Class.Type != "Creature")
                        {
                            return false;
                        }
                        break;
                    case "Allow_Weapon":
                        if (!Weapon._Skill_AllowFaction_ScriptsList.Contains(_Basic_Owner_Script._Skill_LearnSkill_Dictionary[Key]._Basic_Key_String))
                        {
                            return false;
                        }
                        break;
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return true;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region Sort
    //回歸Store——————————————————————————————————————————————————————————————————————
    public void SkillInventorySort(string Type, string Compare, bool IgnoreHide = true)
    {
        //----------------------------------------------------------------------------------------------------
        List<GameObject> QuickSave_Objects_GameObjectsList = new List<GameObject>();
        //----------------------------------------------------------------------------------------------------

        //分類----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Faction":
                foreach (string Key in _Basic_Owner_Script._Skill_LearnSkill_Dictionary.Keys)
                {
                    QuickSave_Objects_GameObjectsList.Add(_Basic_Owner_Script._Skill_LearnSkill_Dictionary[Key].gameObject);
                }
                switch (Compare)
                {
                    default:
                        for (int a = 0; a < QuickSave_Objects_GameObjectsList.Count; a++)
                        {
                            QuickSave_Objects_GameObjectsList[a].transform.SetParent(_Skill_LearnFactionStore_Transform);
                            QuickSave_Objects_GameObjectsList[a].transform.localPosition =
                                new Vector3(QuickSave_Objects_GameObjectsList[a].transform.localPosition.x, QuickSave_Objects_GameObjectsList[a].transform.localPosition.y, 0);
                        }
                        break;
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Sort
    #endregion Item*/
}
