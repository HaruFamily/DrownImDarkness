using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _UI_Equipment : MonoBehaviour
{
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    //----------------------------------------------------------------------------------------------------
    public Text Equipment_Character_Text;
    public Text Equipment_CreatureFaction_Text;
    public Image Equipment_CreatureImage_Image;

    public List<Image> Equipment_WeaponImage_ImageList;
    public List<Text> Equipment_WeaponName_TextList;
    public List<Text> Equipment_WeaponFaction_TextList;
    public List<_UI_Manager.UIBarClass> Equipment_WeaponExpertise_ClassList;

    public List<Image> Equipment_ItemImage_ImageList;
    public List<Text> Equipment_ItemName_TextList;
    public List<Text> Equipment_ItemFaction_TextList;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public string Equipment_EquipmentSave_String;
    public int Equipment_OnSelect_Int;
    public string Equipment_OnSelect_String;
    public Transform[] Equipment_SummaryTransforms;
    /*
     * 0¡GMainFrame
     * 1¡GLeftMenu
     * 2¡GFaction
     * 3¡GWeapon
     * 4¡GItem
     */
    //----------------------------------------------------------------------------------------------------
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X

    #region Start
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void StartSet()
    {
        Equipment_EquipmentSave_String = "Equipment_Main";
        Equipment_SummaryTransforms[0].gameObject.SetActive(true);
        for (int a = 1; a < Equipment_SummaryTransforms.Length; a++)
        {
            Equipment_SummaryTransforms[a].gameObject.SetActive(false);
        }
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    #endregion Start

    #region StartSet
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void EquipmentStartSet()
    {
        //ÅÜ¼Æ----------------------------------------------------------------------------------------------------
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        _Item_Object_Inventory QuickSave_Inventory_Script = _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script;
        //----------------------------------------------------------------------------------------------------

        //³]¸m----------------------------------------------------------------------------------------------------
        Equipment_Character_Text.text = _World_Manager._World_GeneralManager._World_TextManager._Language_UIName_Dictionary["Immo/Miio"];
        Equipment_CreatureFaction_Text.text = QuickSave_Inventory_Script._Item_EquipConcepts_Script._Basic_Object_Script._Skill_Faction_Script._Basic_Language_Class.Name;
        //Equipment_CreatureImage_Image.sprite = _World_Manager._Object_Manager.CreatureImage("CG", "Immo");
                
        for (int a =0; a < 8; a++)
        {
            if (a < QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList.Count)
            {
                _Item_WeaponUnit QuickSave_Weapon_Script = QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList[a];
                _Map_BattleObjectUnit QuickSave_Object_Script = QuickSave_Weapon_Script._Basic_Object_Script;
                Equipment_WeaponImage_ImageList[a].sprite = _View_Manager.GetSprite("Weapon","Icon", QuickSave_Object_Script._Basic_Key_String);
                Equipment_WeaponName_TextList[a].text = QuickSave_Object_Script._Basic_Language_Class.Name;
                Equipment_WeaponFaction_TextList[a].text = QuickSave_Object_Script._Skill_Faction_Script._Basic_Language_Class.Name;

                /*NumbericalValueClass QuickSave_Expertise_Class = QuickSave_Weapon_Script._Basic_Point_Dictionary["Expertise"];
                float QuickSave_Expertise_Float = QuickSave_Expertise_Class.Point / QuickSave_Expertise_Class.Total();
                Equipment_WeaponExpertise_ClassList[a].BarValue.text = QuickSave_Expertise_Class.Point.ToString("0") + "<size=80>" + "/" + QuickSave_Expertise_Class.Total().ToString("0") + "</size>";
                Equipment_WeaponExpertise_ClassList[a].BarTransform.localScale = new Vector3(QuickSave_Expertise_Float, 1, 1);*/
            }
            else
            {
                Equipment_WeaponImage_ImageList[a].sprite = _View_Manager.GetSprite("Weapon", "Icon","null");
                Equipment_WeaponName_TextList[a].text = "";
                Equipment_WeaponFaction_TextList[a].text = "";

                Equipment_WeaponExpertise_ClassList[a].BarValue.text = "";
                Equipment_WeaponExpertise_ClassList[a].BarTransform.localScale = Vector3.zero;
            }
        }

        for (int a = 0; a < 10; a++)
        {
            if (a < QuickSave_Inventory_Script._Item_EquipItems_ScriptsList.Count)
            {
                _Item_ItemUnit QuickSave_Item_Script = QuickSave_Inventory_Script._Item_EquipItems_ScriptsList[a];
                _Map_BattleObjectUnit QuickSave_Object_Script = QuickSave_Item_Script._Basic_Object_Script;
                Equipment_ItemImage_ImageList[a].sprite = _View_Manager.GetSprite("Item", "Icon", QuickSave_Object_Script._Basic_Key_String);
                Equipment_ItemName_TextList[a].text = QuickSave_Object_Script._Basic_Language_Class.Name;
                Equipment_ItemFaction_TextList[a].text = QuickSave_Object_Script._Skill_Faction_Script._Basic_Language_Class.Name;
            }
            else
            {
                Equipment_ItemImage_ImageList[a].sprite = _View_Manager.GetSprite("Item", "Icon", "null");
                Equipment_ItemName_TextList[a].text = "";
                Equipment_ItemFaction_TextList[a].text = "";
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    #endregion StartSet

    #region Equipment
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void EquipmentSet(_Skill_FactionUnit Target)
    {
        //----------------------------------------------------------------------------------------------------
        _Item_ConceptUnit QuickSave_CreatureConcept_Script = 
            _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script._Item_EquipConcepts_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_CreatureConcept_Script._Basic_Object_Script._Skill_Faction_Script != null)
        {
            QuickSave_CreatureConcept_Script._Basic_Object_Script._Skill_Faction_Script._View_Hint_Script.HintSet("UnUsing","Faction");
        }
        Target._View_Hint_Script.HintSet("Using", "Faction");
        QuickSave_CreatureConcept_Script._Basic_Object_Script._Skill_Faction_Script = Target;
        UnSelectSet();
        //----------------------------------------------------------------------------------------------------
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X

    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void EquipmentSet(_Item_WeaponUnit Target)
    {
        //----------------------------------------------------------------------------------------------------
        _Object_CreatureUnit QuickSave_Creature_Script = _World_Manager._Object_Manager._Object_Player_Script;
        _Item_Object_Inventory QuickSave_Inventory_Script = QuickSave_Creature_Script._Object_Inventory_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList[Equipment_OnSelect_Int] != null)
        {
            QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList[Equipment_OnSelect_Int]._View_Hint_Script.HintSet("UnUsing", "Weapon");
        }
        Target._View_Hint_Script.HintSet("Using", "Weapon");
        QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList[Equipment_OnSelect_Int] = Target;
        //----------------------------------------------------------------------------------------------------
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X

    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void EquipmentWeaponFactionSet(_Skill_FactionUnit Target)
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _Object_CreatureUnit QuickSave_Creature_Script = _World_Manager._Object_Manager._Object_Player_Script;
        _Item_Object_Inventory QuickSave_Inventory_Script = QuickSave_Creature_Script._Object_Inventory_Script;
        _Skill_FactionUnit QuickSave_Faction_Script =
            QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList[Equipment_OnSelect_Int]._Basic_Object_Script._Skill_Faction_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Faction_Script != null)
        {
            QuickSave_Faction_Script._View_Hint_Script.HintSet("UnUsing", "Faction");
        }
        Target._View_Hint_Script.HintSet("Using", "Faction");
        QuickSave_Faction_Script = Target;
        //----------------------------------------------------------------------------------------------------
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X

    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void EquipmentSet(_Item_ItemUnit Target)
    {
        //----------------------------------------------------------------------------------------------------
        _Object_CreatureUnit QuickSave_Creature_Script = _World_Manager._Object_Manager._Object_Player_Script;
        _Item_Object_Inventory QuickSave_Inventory_Script = QuickSave_Creature_Script._Object_Inventory_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        print(QuickSave_Inventory_Script._Item_EquipItems_ScriptsList[Equipment_OnSelect_Int] != null);
        if (QuickSave_Inventory_Script._Item_EquipItems_ScriptsList[Equipment_OnSelect_Int] != null)
        {
            QuickSave_Inventory_Script._Item_EquipItems_ScriptsList[Equipment_OnSelect_Int]._View_Hint_Script.HintSet("UnUsing", "Item");
        }
        Target._View_Hint_Script.HintSet("Using", "Item");
        QuickSave_Inventory_Script._Item_EquipItems_ScriptsList[Equipment_OnSelect_Int] = Target;
        UnSelectSet();
        //----------------------------------------------------------------------------------------------------
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    #endregion Equipment

    #region Remove
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void RemoveSet()
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _Object_CreatureUnit QuickSave_Creature_Script = _World_Manager._Object_Manager._Object_Player_Script;
        _Item_Object_Inventory QuickSave_Inventory_Script = QuickSave_Creature_Script._Object_Inventory_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Equipment_OnSelect_String)
        {
            case "Creature":
                print("It's Wrong");
                break;
            case "Weapon":
                _Item_WeaponUnit QuickSave_Weapon_Script = QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList[Equipment_OnSelect_Int];
                if (QuickSave_Weapon_Script != null)
                {
                    QuickSave_Weapon_Script._Basic_Object_Script._Skill_Faction_Script._View_Hint_Script.HintSet("UnUsing", "Weapon");
                    QuickSave_Weapon_Script._View_Hint_Script.HintSet("UnUsing", "Faction");
                }
                QuickSave_Weapon_Script._Basic_Object_Script._Skill_Faction_Script = null;
                QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList[Equipment_OnSelect_Int] = null;
                break;
            case "Item":
                _Item_ItemUnit QuickSave_Item_Script = QuickSave_Inventory_Script._Item_EquipItems_ScriptsList[Equipment_OnSelect_Int];
                if (QuickSave_Item_Script != null)
                {
                    QuickSave_Item_Script._View_Hint_Script.HintSet("UnUsing", "Item");
                }
                QuickSave_Inventory_Script._Item_EquipItems_ScriptsList[Equipment_OnSelect_Int] = null;
                break;
        }
        //_UI_Manager._UI_Camp_Class._View_TinyMenu.TinyMenuOut();
        _UI_Manager.TextEffectDictionarySet("EquipmentSelect", null);
        EquipmentStartSet();
        UnSelectSet();
        //----------------------------------------------------------------------------------------------------
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    #endregion Remove

    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void UnSelectSet()
    {
        Equipment_OnSelect_String = "";
        Equipment_OnSelect_Int = 0;
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
}
