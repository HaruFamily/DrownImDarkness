using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _View_TinyMenu : MonoBehaviour
{
    //——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    public Text _View_Title_Text;
    public Image _View_BackGround_Image;
    public GameObject _View_ButtonUnit_GameObject;
    public Transform _View_ScrollStore_Transform;

    public List<Sprite> _View_BackGroundSprite_SpriteList = new List<Sprite>();
    private List<_View_ButtonUnit> _View_Button_ScriptsList = new List<_View_ButtonUnit>();
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    //選取物
    [HideInInspector] public bool Select_OnSelect_Bool;
    [HideInInspector] public _Item_RecipeUnit Select_Recipe_Script;
    [HideInInspector] public _View_ButtonUnit Select_Button_Script;
    [HideInInspector] public SourceClass Select_Source_Class;
    [HideInInspector] public List<SourceClass> Select_Source_ClassList;

    [HideInInspector] public _Skill_FactionUnit Select_Faction_Script;
    [HideInInspector] public _Skill_ExploreUnit Select_Explore_Script;
    [HideInInspector] public _Skill_BehaviorUnit Select_Behavior_Script;
    [HideInInspector] public _Skill_EnchanceUnit Select_Enchance_Script;

    //[HideInInspector] public _Item_SyndromeUnit Select_Syndrome_Script;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public string _View_OnMenu_String;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    #region MenuSet

    #region - Start -
    public void StartSet()
    {
        _View_OnMenu_String = "null";
    }
    #endregion

    #region - Other -
    public void TinyMenuSet(string Type)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _UI_Manager.CampClass _UI_Camp_Class = _UI_Manager._UI_Camp_Class;
        int QuickSave_Check_Int = 0;
        //----------------------------------------------------------------------------------------------------

        //選取設置----------------------------------------------------------------------------------------------------
        Select_OnSelect_Bool = true;
        _UI_Camp_Class.SummaryTransforms[15].gameObject.SetActive(true);
        _View_OnMenu_String = Type;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)//Normal、Equipment、Alchemy
        {
            #region - Get - 
            case "Get_Inventory_Select_Remove":
                {
                    _View_Title_Text.text = _World_TextManager._Language_UIName_Dictionary["Remove"] + "?";
                    ActiveSet(2);
                    _View_Button_ScriptsList[0]._View_BTNName_String = "Remove";
                    _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Sure"];
                    _View_Button_ScriptsList[1]._View_BTNName_String = "Cancel";
                    _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                }
                break;
            case "Get_Inventory_Remove":
                {
                    _View_Title_Text.text = _World_TextManager._Language_UIName_Dictionary["RemoveAll"] + "?";
                    ActiveSet(2);
                    _View_Button_ScriptsList[0]._View_BTNName_String =
                        "Remove";
                    _View_Button_ScriptsList[0]._View_Text_Text.text =
                        _World_TextManager._Language_UIName_Dictionary["Sure"];
                    _View_Button_ScriptsList[1]._View_BTNName_String =
                        "Cancel";
                    _View_Button_ScriptsList[1]._View_Text_Text.text =
                        _World_TextManager._Language_UIName_Dictionary["Cancel"];
                }
                break;
            #endregion

            #region - Other -
            case "Equipment_Main":
                {
                    _UI_Equipment QuickSave_Equipment_Script = _UI_Manager._UI_Camp_Class._UI_Equipment;
                    _Item_Object_Inventory QuickSave_Inventroy_Script = _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script;
                    int QuickSave_NumberSave_Int = _UI_Camp_Class._UI_Equipment.Equipment_OnSelect_Int;

                    switch (QuickSave_Equipment_Script.Equipment_OnSelect_String)
                    {
                        case "Concept":
                            _View_Title_Text.text = 
                                QuickSave_Inventroy_Script._Item_EquipConcepts_Script._Basic_Object_Script._Skill_Faction_Script._Basic_Language_Class.Name;
                            ActiveSet(2);
                            _View_Button_ScriptsList[0]._View_BTNName_String = 
                                "ChangeFaction";
                            _View_Button_ScriptsList[0]._View_Text_Text.text = 
                                _World_TextManager._Language_UIName_Dictionary["ChangeFaction"];
                            _View_Button_ScriptsList[1]._View_BTNName_String = 
                                "Cancel";
                            _View_Button_ScriptsList[1]._View_Text_Text.text = 
                                _World_TextManager._Language_UIName_Dictionary["Cancel"];
                            break;
                        case "Weapon":
                            for (int a = 0; a < QuickSave_Inventroy_Script._Item_EquipWeapons_ScriptsList.Count; a++)
                            {
                                if (QuickSave_Inventroy_Script._Item_EquipWeapons_ScriptsList[a] != null)
                                {
                                    QuickSave_Check_Int++;
                                }
                            }

                            if (QuickSave_Inventroy_Script._Item_EquipWeapons_ScriptsList[QuickSave_NumberSave_Int] != null)
                            {
                                _Item_WeaponUnit QuickSave_Weapon_Script = QuickSave_Inventroy_Script._Item_EquipWeapons_ScriptsList[QuickSave_NumberSave_Int];
                                _View_Title_Text.text = QuickSave_Weapon_Script._Basic_Object_Script._Basic_Language_Class.Name;
                                ActiveSet(4);
                                _View_Button_ScriptsList[0]._View_BTNName_String = "ChangeWeapon";
                                _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["ChangeWeapon"];
                                _View_Button_ScriptsList[1]._View_BTNName_String = "ChangeFaction";
                                _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["ChangeFaction"];
                                if (QuickSave_Check_Int > 1)
                                {
                                    _View_Button_ScriptsList[2]._View_BTNName_String = "Remove";
                                    _View_Button_ScriptsList[2]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["RemoveEquip"];
                                }
                                else
                                {
                                    _View_Button_ScriptsList[2]._View_BTNName_String = "Noact";
                                    _View_Button_ScriptsList[2]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["NoactByLastEquip"];
                                }
                                _View_Button_ScriptsList[3]._View_BTNName_String = "Cancel";
                                _View_Button_ScriptsList[3]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                            }
                            else
                            {
                                _View_Title_Text.text = _World_TextManager._Language_UIName_Dictionary["Empty"];
                                ActiveSet(2);
                                _View_Button_ScriptsList[0]._View_BTNName_String = "ChangeWeapon";
                                _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["ChangeWeapon"];
                                _View_Button_ScriptsList[1]._View_BTNName_String = "Cancel";
                                _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                            }
                            break;
                        case "Item":
                            if (QuickSave_Inventroy_Script._Item_EquipItems_ScriptsList[QuickSave_NumberSave_Int] != null)
                            {
                                _Item_ItemUnit QuickSave_Item_Script = QuickSave_Inventroy_Script._Item_EquipItems_ScriptsList[QuickSave_NumberSave_Int];
                                _View_Title_Text.text = QuickSave_Item_Script._Basic_Object_Script._Basic_Language_Class.Name;
                                ActiveSet(3);
                                _View_Button_ScriptsList[0]._View_BTNName_String = "ChangeItem";
                                _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["ChangeItem"];
                                _View_Button_ScriptsList[1]._View_BTNName_String = "Remove";
                                _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["RemoveEquip"];
                                _View_Button_ScriptsList[2]._View_BTNName_String = "Cancel";
                                _View_Button_ScriptsList[2]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                            }
                            else
                            {
                                _View_Title_Text.text = _World_TextManager._Language_UIName_Dictionary["Empty"];
                                ActiveSet(2);
                                _View_Button_ScriptsList[0]._View_BTNName_String = "ChangeItem";
                                _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["ChangeItem"];
                                _View_Button_ScriptsList[1]._View_BTNName_String = "Cancel";
                                _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                            }
                            break;
                    }
                }
                break;

            case "Inventory_Weapons_Remove":
            case "Inventory_Items_Remove":
            case "Inventory_Collections_Remove":
            case "Inventory_Materials_Remove":
                {
                    _View_Title_Text.text = _World_TextManager._Language_UIName_Dictionary["Remove"] + "?";
                    ActiveSet(2);
                    _View_Button_ScriptsList[0]._View_BTNName_String = "Sure";
                    _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Sure"];
                    _View_Button_ScriptsList[1]._View_BTNName_String = "Cancel";
                    _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                }
                break;
            #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void TinyMenuSet(string Type, List<string> Data)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _UI_Manager.CampClass _UI_Camp_Class = _UI_Manager._UI_Camp_Class;
        //----------------------------------------------------------------------------------------------------

        //選取設置----------------------------------------------------------------------------------------------------
        Select_OnSelect_Bool = true;
        _UI_Camp_Class.SummaryTransforms[15].gameObject.SetActive(true);
        _View_OnMenu_String = Type;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)//Normal、Equipment、Alchemy
        {
            #region -Explore -
            //變異池新增/選項
            case "SyndromePool_Add":
            case "SyndromePool_Remove":
                {
                    ActiveSet(Data.Count);//選擇大小                
                    _View_Title_Text.text =
                        _World_TextManager._Language_UIName_Dictionary[Type];
                    for (int a = 0; a < Data.Count; a++)
                    {
                        //按鈕設置
                        if (_World_Manager._Item_Manager.
                            _Language_Syndrome_Dictionary.TryGetValue(Data[a], out LanguageClass Language))
                        {
                            _View_Button_ScriptsList[a]._View_Text_Text.text = Language.Name;
                        }
                        else
                        {
                            print(Data[a]);
                            _View_Button_ScriptsList[a]._View_Text_Text.text = Data[a];
                        }
                        _View_Button_ScriptsList[a]._View_BTNName_String = Type;
                        _View_Button_ScriptsList[a]._View_BTNCode_String = Data[a];
                        _View_Button_ScriptsList[a]._View_BTNCode_Int = a;
                    }
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void TinyMenuSet(string Type, SourceClass SourceData)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _Item_Object_Inventory QuickSave_Inventory_Script = _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script;
        //----------------------------------------------------------------------------------------------------

        //選取設置----------------------------------------------------------------------------------------------------
        Select_OnSelect_Bool = true;
        _UI_Manager._UI_Camp_Class.SummaryTransforms[15].gameObject.SetActive(true);
        Select_Source_Class = SourceData;
        _View_OnMenu_String = Type;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        bool QuickSave_Equip_Bool = true;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _View_Title_Text.text = 
            SourceData.Source_BattleObject._Basic_Language_Class.Name;
        switch (Type)
        {

            //獲得物品檢查
            case "Get_Inventory_Select":
                {
                    //顯示資訊----------------------------------------------------------------------------------------------------
                    //變數
                    LanguageClass QuickSave_Language_Class =
                        Select_Source_Class.Source_BattleObject._Basic_Language_Class;
                    //設定
                    _View_ItemInfo _View_ItemInfo = _World_Manager._UI_Manager._UI_Camp_Class._View_ItemInfo;
                    _View_ItemInfo.ItemInfoSet(Select_Source_Class.Source_BattleObject);
                    //----------------------------------------------------------------------------------------------------

                    //按鈕設置----------------------------------------------------------------------------------------------------
                    _View_Title_Text.text = QuickSave_Language_Class.Name;
                    ActiveSet(2);
                    _View_Button_ScriptsList[0]._View_BTNName_String = "Cancel";
                    _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                    _View_Button_ScriptsList[1]._View_BTNName_String = "Remove";
                    _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Remove"];
                    //----------------------------------------------------------------------------------------------------
                }
                break;

            #region - Inventory -
            case "Inventory_Concepts":
                {
                    if (QuickSave_Inventory_Script._Item_EquipConcepts_Script == SourceData.Source_Concept)
                    {
                        QuickSave_Equip_Bool = false;
                    }
                    ActiveSet(3);
                    if (QuickSave_Equip_Bool)
                    {
                        _View_Button_ScriptsList[0]._View_BTNName_String = "Equip";
                        _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Equip"];
                        _View_Button_ScriptsList[1]._View_Text_Text.color = Color.white;

                        _View_Button_ScriptsList[1]._View_BTNName_String = "Remove";
                        _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Remove"];
                        _View_Button_ScriptsList[1]._View_Text_Text.color = Color.white;
                    }
                    else
                    {
                        _View_Button_ScriptsList[0]._View_BTNName_String = "NoAct";
                        _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["CannotRemove"];
                        _View_Button_ScriptsList[0]._View_Text_Text.color = _World_Manager._View_Manager.GetColor("Code", "Empty");

                        _View_Button_ScriptsList[1]._View_BTNName_String = "NoAct";
                        _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Remove"];
                        _View_Button_ScriptsList[1]._View_Text_Text.color = _World_Manager._View_Manager.GetColor("Code", "Empty");
                    }
                    _View_Button_ScriptsList[2]._View_BTNName_String = "Cancel";
                    _View_Button_ScriptsList[2]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                }
                break;
            case "Inventory_Weapons":
                {
                    foreach (_Item_WeaponUnit Weapon in QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList)
                    {
                        if (Weapon == SourceData.Source_Weapon)
                        {
                            QuickSave_Equip_Bool = false;
                            break;
                        }
                    }
                    ActiveSet(3);
                    if (QuickSave_Equip_Bool)
                    {
                        _View_Button_ScriptsList[0]._View_BTNName_String = "Equip";
                        _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Equip"];

                        _View_Button_ScriptsList[1]._View_BTNName_String = "Remove";
                        _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Remove"];
                        _View_Button_ScriptsList[1]._View_Text_Text.color = Color.white;
                    }
                    else
                    {
                        _View_Button_ScriptsList[0]._View_BTNName_String = "RemoveEquip";
                        _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["RemoveEquip"];

                        _View_Button_ScriptsList[1]._View_BTNName_String = "NoAct";
                        _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Remove"];
                        _View_Button_ScriptsList[1]._View_Text_Text.color = _World_Manager._View_Manager.GetColor("Code", "Empty");
                    }
                    _View_Button_ScriptsList[2]._View_BTNName_String = "Cancel";
                    _View_Button_ScriptsList[2]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                }
                break;
            case "Inventory_Items":
                {
                    foreach (_Item_ItemUnit Item in QuickSave_Inventory_Script._Item_EquipItems_ScriptsList)
                    {
                        if (Item == SourceData.Source_Item)
                        {
                            QuickSave_Equip_Bool = false;
                            break;
                        }
                    }
                    ActiveSet(3);
                    if (QuickSave_Equip_Bool)
                    {
                        _View_Button_ScriptsList[0]._View_BTNName_String = "Equip";
                        _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Equip"];

                        _View_Button_ScriptsList[1]._View_BTNName_String = "Remove";
                        _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Remove"];
                        _View_Button_ScriptsList[1]._View_Text_Text.color = Color.white;
                    }
                    else
                    {
                        _View_Button_ScriptsList[0]._View_BTNName_String = "RemoveEquip";
                        _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["RemoveEquip"];

                        _View_Button_ScriptsList[1]._View_BTNName_String = "NoAct";
                        _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Remove"];
                        _View_Button_ScriptsList[1]._View_Text_Text.color = _World_Manager._View_Manager.GetColor("Code", "Empty");
                    }
                    _View_Button_ScriptsList[2]._View_BTNName_String = "Cancel";
                    _View_Button_ScriptsList[2]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                }
                break;
            case "Inventory_Materials":
                {
                    ActiveSet(2);
                    _View_Button_ScriptsList[0]._View_BTNName_String = "Remove";
                    _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Remove"];
                    _View_Button_ScriptsList[1]._View_BTNName_String = "Cancel";
                    _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                }
                break;
            #endregion

            #region - Alchemy -
            case "Alchemy_Material":
                {
                    ActiveSet(2);
                    bool QuickSave_MaterialCheck_Bool = false;
                    _View_Alchemy _View_Alchemy = _World_Manager._UI_Manager._UI_Camp_Class._View_Alchemy;
                    for (int a = 0; a < _View_Alchemy._Alchemy_Material_ScriptsArray.Length; a++)
                    {
                        if (_View_Alchemy._Alchemy_Material_ScriptsArray[a] == SourceData.Source_Material)
                        {
                            QuickSave_MaterialCheck_Bool = true;
                        }
                    }
                    if (QuickSave_MaterialCheck_Bool)
                    {
                        _View_Button_ScriptsList[0]._View_BTNName_String = "NoAct";
                        _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Selecting"];
                        _View_Button_ScriptsList[0]._View_Text_Text.color = _World_Manager._View_Manager.GetColor("Code", "Empty");
                    }
                    else
                    {
                        _View_Button_ScriptsList[0]._View_BTNName_String = "Sure";
                        _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Select"];
                        _View_Button_ScriptsList[0]._View_Text_Text.color = Color.white;
                    }
                    _View_Button_ScriptsList[1]._View_BTNName_String = "Cancel";
                    _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void TinyMenuSet(string Type, List<SourceClass> SourceData)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _UI_Manager.CampClass _UI_Camp_Class = _UI_Manager._UI_Camp_Class;
        //----------------------------------------------------------------------------------------------------

        //選取設置----------------------------------------------------------------------------------------------------
        Select_OnSelect_Bool = true;
        _UI_Camp_Class.SummaryTransforms[15].gameObject.SetActive(true);
        Select_Source_ClassList = SourceData;
        _View_OnMenu_String = Type;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)//Normal、Equipment、Alchemy
        {
            #region -ItemGet -
            //獲得
            case "Get_Inventory":
                {
                    _Item_Object_Inventory QuickSave_Inventory_Script =
                        _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script;
                    ActiveSet(SourceData.Count + 2);//選擇大小
                    _View_Title_Text.text =
                        _World_TextManager._Language_UIName_Dictionary[Type];
                    for (int a = 0; a < SourceData.Count; a++)
                    {
                        //按鈕設置
                        LanguageClass QuickSave_Language_Class = 
                            SourceData[a].Source_BattleObject._Basic_Language_Class;
                        _View_Button_ScriptsList[a]._View_BTNName_String = Type;
                        _View_Button_ScriptsList[a]._View_BTNCode_String = SourceData[a].Source_BattleObject._Basic_Key_String;
                        _View_Button_ScriptsList[a]._View_BTNCode_Int = a;
                        _View_Button_ScriptsList[a]._View_BTNCode_Class = SourceData[a];
                        _View_Button_ScriptsList[a]._View_Text_Text.text = QuickSave_Language_Class.Name;
                    }
                    bool QuickSave_CanTake_Bool = true;
                    foreach (NumbericalValueClass Status in 
                        QuickSave_Inventory_Script._Item_EquipStatus_ClassArray)
                    {
                        if (Status.Point > Status.Total())
                        {
                            QuickSave_CanTake_Bool = false;
                            break;
                        }
                    }
                    if (!QuickSave_CanTake_Bool)
                    {
                        _View_Button_ScriptsList[SourceData.Count]._View_BTNName_String = "NoAct";
                        _View_Button_ScriptsList[SourceData.Count]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Get_Inventory_Full"];
                        _View_Button_ScriptsList[SourceData.Count]._View_Text_Text.color = _World_Manager._View_Manager.GetColor("Code", "Empty");
                    }
                    else
                    {
                        _View_Button_ScriptsList[SourceData.Count]._View_BTNName_String = "Cancel";
                        _View_Button_ScriptsList[SourceData.Count]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Sure"];
                        _View_Button_ScriptsList[SourceData.Count]._View_Text_Text.color = Color.white;
                    }
                    _View_Button_ScriptsList[SourceData.Count + 1]._View_BTNName_String = "Remove";
                    _View_Button_ScriptsList[SourceData.Count + 1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["RemoveAll"];
                }
                break;
            #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void TinyMenuSet(string Type, _View_ButtonUnit Button)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _UI_Manager.CampClass _UI_Camp_Class = _UI_Manager._UI_Camp_Class;
        //----------------------------------------------------------------------------------------------------

        //選取設置----------------------------------------------------------------------------------------------------
        Select_OnSelect_Bool = true;
        _UI_Camp_Class.SummaryTransforms[15].gameObject.SetActive(true);
        _View_OnMenu_String = Type;
        Select_Button_Script = Button;
        Select_Source_Class = Button._View_BTNCode_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)//Normal、Equipment、Alchemy
        {
            #region -Explore -
            //變異池新增/確定選擇
            case "SyndromePool_Add_Select":
                {
                    //顯示資訊----------------------------------------------------------------------------------------------------
                    //變數
                    LanguageClass QuickSave_Language_Class =
                        _World_Manager._Item_Manager.
                        _Language_Syndrome_Dictionary[Button._View_BTNCode_String];
                    //設定
                    print("Syndrome尚未顯示");
                    //----------------------------------------------------------------------------------------------------

                    //按鈕設置----------------------------------------------------------------------------------------------------
                    _View_Title_Text.text = QuickSave_Language_Class.Name;
                    ActiveSet(2);
                    _View_Button_ScriptsList[0]._View_BTNName_String = "Sure";
                    _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Sure"];
                    _View_Button_ScriptsList[1]._View_BTNName_String = "Cancel";
                    _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                    //----------------------------------------------------------------------------------------------------
                }
                break;
            case "SyndromePool_Remove_Select":
                {
                    //顯示資訊----------------------------------------------------------------------------------------------------
                    //變數
                    LanguageClass QuickSave_Language_Class =
                        _World_Manager._Item_Manager.
                        _Language_Syndrome_Dictionary[Button._View_BTNCode_String];
                    //設定
                    print("Syndrome尚未顯示");
                    //----------------------------------------------------------------------------------------------------

                    //按鈕設置----------------------------------------------------------------------------------------------------
                    _View_Title_Text.text = QuickSave_Language_Class.Name;
                    ActiveSet(2);
                    _View_Button_ScriptsList[0]._View_BTNName_String = "Sure";
                    _View_Button_ScriptsList[0]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Sure"];
                    _View_Button_ScriptsList[1]._View_BTNName_String = "Cancel";
                    _View_Button_ScriptsList[1]._View_Text_Text.text = _World_TextManager._Language_UIName_Dictionary["Cancel"];
                    //----------------------------------------------------------------------------------------------------
                }
                break;
            #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion

    #region - Other -
    //——————————————————————————————————————————————————————————————————————
    public void TinyMenuOut()
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_View_OnMenu_String)
        {
            case "Inventory_Weapons":
            case "Inventory_Items":
            case "Inventory_Concepts":
            case "Inventory_Materials":
            case "Alchemy_Material":
                Select_Source_Class = null;
                break;

            case "Get_Inventory_Select":
            case "Get_Inventory_Select_Remove":
                Select_Source_Class = null;
                break;

            case "Get_Inventory":
            case "Get_Inventory_Remove":
                {
                    Select_Source_Class = null;
                    Select_Source_ClassList = null;
                    _World_Manager._UI_Manager._UI_Camp_Class._View_ItemInfo.
                        ItemInfoSortReset();
                }
                break;

            case "Alchemy_Alchemy":
                Select_Recipe_Script = null;
                break;

            //變異池增減
            case "SyndromePool_Add_Select":
            case "SyndromePool_Remove_Select":
                Select_Button_Script = null;
                break;
        }
        _UI_Manager._UI_Camp_Class.SummaryTransforms[15].gameObject.SetActive(false);
        Select_OnSelect_Bool = false;
        _View_OnMenu_String = "null";
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    private void ActiveSet(int Count)
    {
        _View_BackGround_Image.sprite = _View_BackGroundSprite_SpriteList[Mathf.Clamp(Count - 1, 0, 5)];
        while (_View_Button_ScriptsList.Count < Count)
        {
            _View_ButtonUnit QuickSave_ButtonUnit_Script = 
                Instantiate(_View_ButtonUnit_GameObject, _View_ScrollStore_Transform).GetComponent<_View_ButtonUnit>();
            _View_Button_ScriptsList.Add(QuickSave_ButtonUnit_Script);
            QuickSave_ButtonUnit_Script.name = "_View_ButtonUnit";
        }
        for (int a = 0; a < _View_Button_ScriptsList.Count; a++)
        {
            if (a < Count)
            {
                _View_Button_ScriptsList[a].gameObject.SetActive(true);
                _View_Button_ScriptsList[a]._View_Text_Text.color = Color.white;
            }
            else
            {
                _View_Button_ScriptsList[a].gameObject.SetActive(false);
            }
        }
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion MenuSet 

    #region BTN
    #region - Sure -
    //——————————————————————————————————————————————————————————————————————
    public void SureSet()
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_View_OnMenu_String)
        {
            case "Inventory_Weapons_Remove":
            case "Inventory_Items_Remove":
            case "Inventory_Concepts_Remove":
            case "Inventory_Materials_Remove":
                RemoveSet();
                break;

            case "Alchemy_Material":
                _UI_Manager._UI_Camp_Class._View_Alchemy.Alchemy_MaterialInput();
                CancelSet();
                break;

            //變異池增減/確定選擇
            case "SyndromePool_Add_Select":
                {
                    _UI_Manager._UI_EventManager.
                        SyndromePoolSet("Add", Select_Button_Script._View_BTNCode_String, null);
                    TinyMenuOut();
                    _UI_Manager._UI_EventManager.EventContinue();
                }
                break;
            case "SyndromePool_Remove_Select":
                {
                    _UI_Manager._UI_EventManager.
                        SyndromePoolSet("Remove", Select_Button_Script._View_BTNCode_String, null);
                    TinyMenuOut();
                    _UI_Manager._UI_EventManager.EventContinue();
                }
                break;

        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Remove -
    //——————————————————————————————————————————————————————————————————————
    public void RemoveSet()
    {
        //----------------------------------------------------------------------------------------------------
        _Item_Object_Inventory QuickSave_Inventory_Script = _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_View_OnMenu_String)
        {
            #region - Get -
            //刪除詢問
            case "Get_Inventory_Select":
                TinyMenuSet(_View_OnMenu_String + "_Remove");
                break;
            case "Get_Inventory":
                TinyMenuSet(_View_OnMenu_String + "_Remove");
                break;
            //確定刪除
            case "Get_Inventory_Select_Remove":
                {
                    switch (Select_Source_Class.SourceType)
                    {
                        case "Concept":
                            QuickSave_Inventory_Script.InventorySet("Destroy", Select_Source_Class.Source_Concept);
                            break;
                        case "Weapon":
                            QuickSave_Inventory_Script.InventorySet("Destroy", Select_Source_Class.Source_Weapon);
                            break;
                        case "Item":
                            QuickSave_Inventory_Script.InventorySet("Destroy", Select_Source_Class.Source_Item);
                            break;
                        case "Material":
                            QuickSave_Inventory_Script.InventorySet("Destroy", Select_Source_Class.Source_Material);
                            break;
                    }
                    Select_Source_ClassList.Remove(Select_Source_Class);
                    Select_OnSelect_Bool = false;
                    _World_Manager._UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoOut();
                    TinyMenuSet("Get_Inventory", Select_Source_ClassList);
                }
                break;
            case "Get_Inventory_Remove":
                {
                    _View_ItemInfo _View_ItemInfo = _World_Manager._UI_Manager._UI_Camp_Class._View_ItemInfo;
                    //全部刪除
                    foreach (SourceClass Source in Select_Source_ClassList)
                    {
                        switch (Source.SourceType)
                        {
                            case "Concept":
                                QuickSave_Inventory_Script.InventorySet("Destroy", Source.Source_Concept);
                                break;
                            case "Weapon":
                                QuickSave_Inventory_Script.InventorySet("Destroy", Source.Source_Weapon);
                                break;
                            case "Item":
                                QuickSave_Inventory_Script.InventorySet("Destroy", Source.Source_Item);
                                break;
                            case "Material":
                                QuickSave_Inventory_Script.InventorySet("Destroy", Source.Source_Material);
                                break;
                        }
                    }
                    TinyMenuOut();
                    _View_ItemInfo.ItemInfoOut();
                }
                break;
            #endregion

            #region - Inventory -
            //刪除詢問
            case "Inventory_Weapons":
            case "Inventory_Items":
            case "Inventory_Concepts":
            case "Inventory_Materials":
                TinyMenuSet(_View_OnMenu_String + "_Remove");
                break;
            //確定刪除(由SureSet而來)
            case "Inventory_Weapons_Remove":
                QuickSave_Inventory_Script.InventorySet("Destroy", Select_Source_Class.Source_Weapon);
                _View_OnMenu_String = _View_OnMenu_String.Replace("_Remove","");
                CancelSet();
                break;
            case "Inventory_Items_Remove":
                QuickSave_Inventory_Script.InventorySet("Destroy", Select_Source_Class.Source_Item);
                _View_OnMenu_String = _View_OnMenu_String.Replace("_Remove", "");
                CancelSet();
                break;
            case "Inventory_Concepts_Remove":
                QuickSave_Inventory_Script.InventorySet("Destroy", Select_Source_Class.Source_Concept);
                _View_OnMenu_String = _View_OnMenu_String.Replace("_Remove", "");
                CancelSet();
                break;
            case "Inventory_Materials_Remove":
                QuickSave_Inventory_Script.InventorySet("Destroy", Select_Source_Class.Source_Material);
                _View_OnMenu_String = _View_OnMenu_String.Replace("_Remove", "");
                CancelSet();
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion - Remove -

    #region - Cancel -
    //——————————————————————————————————————————————————————————————————————
    public void CancelSet()
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_View_OnMenu_String)
        {
            case "Get_Inventory_Select":
                {
                    Select_OnSelect_Bool = false;
                    _World_Manager._UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoOut();
                    TinyMenuSet("Get_Inventory", Select_Source_ClassList);
                }
                break;
            case "Get_Inventory_Select_Remove":
                {
                    TinyMenuSet("Get_Inventory_Select", Select_Source_Class);
                }
                break;
            case "Get_Inventory":
                {
                    TinyMenuOut();
                }
                break;
            case "Get_Inventory_Remove":
                {
                    TinyMenuSet("Get_Inventory", Select_Source_ClassList);
                }
                break;

            case "Inventory_Weapons":
            case "Inventory_Items":
            case "Inventory_Concepts":
            case "Inventory_Materials":
            case "Alchemy_Material":
                switch (Select_Source_Class.SourceType)
                {
                    case "Weapon":
                    case "Item":
                    case "Concept":
                    case "Material":
                        TinyMenuOut();
                        _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoOut();
                        _UI_Manager.TextEffectDictionarySet("ItemUnitInfo", null);
                        break;
                    default:
                        break;
                }
                break;
            //回到道具欄
            case "Inventory_Weapons_Remove":
            case "Inventory_Items_Remove":
            case "Inventory_Concepts_Remove":
            case "Inventory_Materials_Remove":
                TinyMenuSet(_View_OnMenu_String.Replace("_Remove", ""), Select_Source_Class);
                break;

            case "Alchemy_Materials":
                //TinyMenuSet(Select_Material_Script, "");
                //_UI_Manager.UISet("Alchemy_Alchemy");
                //_UI_Manager.TextEffectDictionarySet("ItemUnitInfo", null);
                break;

            //回到變異池選擇
            case "SyndromePool_Add_Select":
                TinyMenuSet("SyndromePool_Add");
                break;
            case "SyndromePool_Remove_Select":
                TinyMenuSet("SyndromePool_Remove");
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion - Cancel -

    #region - Equip -
    //——————————————————————————————————————————————————————————————————————
    public void EquipSet()
    {
        //----------------------------------------------------------------------------------------------------
        _Object_CreatureUnit QuickSave_Player_Script = _World_Manager._Object_Manager._Object_Player_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_View_OnMenu_String)
        {
            case "Inventory_Concepts":
                QuickSave_Player_Script._Object_Inventory_Script.InventorySet("Equip", Select_Source_Class.Source_Concept);
                break;
            case "Inventory_Weapons":
                QuickSave_Player_Script._Object_Inventory_Script.InventorySet("Equip", Select_Source_Class.Source_Weapon);
                break;
            case "Inventory_Items":
                QuickSave_Player_Script._Object_Inventory_Script.InventorySet("Equip", Select_Source_Class.Source_Item);
                break;
            default:
                print(_View_OnMenu_String);
                break;
        }
        CancelSet();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void RemoveEquipSet()
    {
        //----------------------------------------------------------------------------------------------------
        _Object_CreatureUnit QuickSave_Player_Script = _World_Manager._Object_Manager._Object_Player_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_View_OnMenu_String)
        {
            case "Inventory_Weapons":
                QuickSave_Player_Script._Object_Inventory_Script.InventorySet("RemoveEquip", Select_Source_Class.Source_Weapon);
                break;
            case "Inventory_Items":
                QuickSave_Player_Script._Object_Inventory_Script.InventorySet("RemoveEquip", Select_Source_Class.Source_Item);
                break;
            default:
                print(_View_OnMenu_String);
                break;
        }
        CancelSet();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion BTN

    #region View
    //——————————————————————————————————————————————————————————————————————
    public void TracerBubbleOn(string Type,string Key)
    {
        //----------------------------------------------------------------------------------------------------
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        SourceClass QuickSave_Source_Class = null;
        string QuickSave_Key_String = "";
        LanguageClass QuickSave_Language_Class = null;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "UIName":
            case "UISummary":
                QuickSave_Source_Class = new SourceClass { SourceType = Type };
                QuickSave_Key_String = Key;
                break;

            case "AlchemyTag":
                {
                    int QuickSave_Key_Int = int.Parse(Key);
                    _Item_RecipeUnit QuickSave_Alchemy_Script =
                        _World_Manager._UI_Manager._UI_Camp_Class._View_Alchemy.Alchemy_OnRecipe_Script;
                    switch (QuickSave_Alchemy_Script._Basic_Type_String)
                    {
                        case "Concept":
                        case "Weapon":
                        case "Item":
                            QuickSave_Key_String =
                                QuickSave_Alchemy_Script._Basic_ObjectData_Class.Tag[QuickSave_Key_Int];
                            break;
                        case "Material":
                            List<string> QuickSave_Tag = _World_Manager._Item_Manager.
                                _Data_Material_Dictionary[QuickSave_Alchemy_Script._Basic_MaterialData_Class.Target].Tag;
                            QuickSave_Key_String = QuickSave_Tag[QuickSave_Key_Int];
                            break;
                    }
                    QuickSave_Source_Class = new SourceClass
                    {
                        SourceType = "Tag",
                    };
                }
                break;
            case "Tag":
                {
                    int QuickSave_Key_Int = int.Parse(Key);
                    QuickSave_Key_String =
                        Select_Source_Class.Source_BattleObject.
                            Key_Tag(Select_Source_Class, null)[QuickSave_Key_Int];
                    QuickSave_Source_Class = new SourceClass
                    {
                        SourceType = "Tag",
                    };
                }
                break;

            case "AlchemySpecialAffix":
                {
                    int QuickSave_Key_Int = int.Parse(Key);
                    _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
                    _View_Alchemy _View_Alchemy = _World_Manager._UI_Manager._UI_Camp_Class._View_Alchemy;
                    _Item_RecipeUnit QuickSave_Alchemy_Script =
                        _View_Alchemy.Alchemy_OnRecipe_Script;
                    int QuickSave_TargetMaterial_Int =
                        _View_Alchemy._Alchemy_InheritSave_IntArray[QuickSave_Key_Int];
                    if (QuickSave_TargetMaterial_Int > _View_Alchemy._Alchemy_Material_ScriptsArray.Length)
                    {
                        return;
                    }
                    _Item_MaterialUnit QuickSave_Material_Script =
                        _View_Alchemy._Alchemy_Material_ScriptsArray[QuickSave_TargetMaterial_Int];
                    if (QuickSave_Material_Script == null)
                    {
                        return;
                    }
                    QuickSave_Key_String =
                        QuickSave_Material_Script._Basic_Object_Script._Basic_Material_Class.SpecialAffix[QuickSave_Key_Int];
                    SourceClass QuickSave_MaterialSource_Class = 
                        QuickSave_Material_Script._Basic_Object_Script._Basic_Source_Class;
                    if (QuickSave_Key_String == "" || QuickSave_Key_String == null)
                    {
                        return;
                    }
                    QuickSave_Source_Class = new SourceClass
                    {
                        SourceType = "SpecialAffix",
                        Source_Concept = QuickSave_MaterialSource_Class.Source_Concept,
                        Source_Weapon = QuickSave_MaterialSource_Class.Source_Weapon,
                        Source_Item = QuickSave_MaterialSource_Class.Source_Item,
                        Source_Material = QuickSave_MaterialSource_Class.Source_Material,
                        Source_BattleObject = QuickSave_MaterialSource_Class.Source_BattleObject,
                        Source_NumbersData = _Item_Manager._Data_SpecialAffix_Dictionary[QuickSave_Key_String].Numbers,
                        Source_KeysData = _Item_Manager._Data_SpecialAffix_Dictionary[QuickSave_Key_String].Keys
                    };
                    if (Select_Source_Class.SourceType == "Material")
                    {
                        QuickSave_Source_Class.Source_BattleObject = null;
                    }
                    QuickSave_Language_Class = _Item_Manager._Language_SpecialAffix_Dictionary[QuickSave_Key_String];
                }
                break;
            case "SpecialAffix":
                {
                    int QuickSave_Key_Int = int.Parse(Key);
                    _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
                    QuickSave_Key_String =
                        Select_Source_Class.Source_BattleObject._Basic_Material_Class.SpecialAffix[QuickSave_Key_Int];
                    if (QuickSave_Key_String == "" || QuickSave_Key_String == null)
                    {
                        return;
                    }
                    QuickSave_Source_Class = new SourceClass
                    {
                        SourceType = "SpecialAffix",
                        Source_Concept = Select_Source_Class.Source_Concept,
                        Source_Weapon = Select_Source_Class.Source_Weapon,
                        Source_Item = Select_Source_Class.Source_Item,
                        Source_Material = Select_Source_Class.Source_Material,
                        Source_BattleObject = Select_Source_Class.Source_BattleObject,
                        Source_NumbersData = _Item_Manager._Data_SpecialAffix_Dictionary[QuickSave_Key_String].Numbers,
                        Source_KeysData = _Item_Manager._Data_SpecialAffix_Dictionary[QuickSave_Key_String].Keys
                    };
                    if (Select_Source_Class.SourceType == "Material")
                    {
                        QuickSave_Source_Class.Source_BattleObject = null;
                    }
                    QuickSave_Language_Class = _Item_Manager._Language_SpecialAffix_Dictionary[QuickSave_Key_String];
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _View_Manager.TracerBubbleOn(
            QuickSave_Source_Class,
            QuickSave_Key_String,
            QuickSave_Language_Class);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
}
