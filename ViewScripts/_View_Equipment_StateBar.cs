using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _View_Equipment_StateBar : MonoBehaviour
{
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    //----------------------------------------------------------------------------------------------------
    //¾¹ª«ÃC¦â
    public Image _View_Color_Image;
    //
    public Text _View_Faction_Text;
    public List<Text> _View_State_TextList;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public _Map_BattleObjectUnit _Data_Owner_Script;
    //----------------------------------------------------------------------------------------------------
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X

    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void SetView(_Map_BattleObjectUnit Owner)
    {
        _View_Manager _View_Manager = _World_Manager._View_Manager;

        _Data_Owner_Script = Owner;
        if (Owner == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
            switch (_Data_Owner_Script._Basic_Source_Class.SourceType)
            {
                case "Concept":
                    {
                        _Object_CreatureUnit QuickSave_Creature_Script =
                            _Data_Owner_Script._Basic_Source_Class.Source_Creature;
                        _Item_ConceptUnit QuickSave_Concept_Script = 
                            _Data_Owner_Script._Basic_Source_Class.Source_Concept;
                        _Map_BattleObjectUnit QuickSave_Object_Script = 
                            QuickSave_Concept_Script._Basic_Object_Script;
                        SourceClass QuickSave_Source_Class =
                            QuickSave_Object_Script._Basic_Source_Class;

                        _View_Color_Image.color = 
                            QuickSave_Concept_Script._Basic_Color_Color;
                        _View_Faction_Text.text =
                            QuickSave_Object_Script._Skill_Faction_Script._Basic_Language_Class.Name;
                        _View_State_TextList[0].text =
                            QuickSave_Object_Script.Key_Material("Size", QuickSave_Source_Class, null).ToString("0");
                        _View_State_TextList[1].text =
                            QuickSave_Object_Script.Key_Material("Form", QuickSave_Source_Class, null).ToString("0"); 
                        _View_State_TextList[2].text =
                            QuickSave_Object_Script.Key_Material("Weight", QuickSave_Source_Class, null).ToString("0"); 
                        _View_State_TextList[3].text =
                            QuickSave_Object_Script.Key_Material("Purity", QuickSave_Source_Class, null).ToString("0"); 
                    }
                    break;
                case "Weapon":
                    {
                        _Item_WeaponUnit QuickSave_Weapon_Script =
                            _Data_Owner_Script._Basic_Source_Class.Source_Weapon;
                        _Map_BattleObjectUnit QuickSave_Object_Script =
                            QuickSave_Weapon_Script._Basic_Object_Script;
                        SourceClass QuickSave_Source_Class =
                            QuickSave_Object_Script._Basic_Source_Class;

                        _View_Color_Image.color =
                            QuickSave_Weapon_Script._Basic_Color_Color;
                        _View_Faction_Text.text =
                            QuickSave_Object_Script._Skill_Faction_Script._Basic_Language_Class.Name;
                        _View_State_TextList[0].text =
                            QuickSave_Object_Script.Key_Material("Size", QuickSave_Source_Class, null).ToString("0");
                        _View_State_TextList[1].text =
                            QuickSave_Object_Script.Key_Material("Form", QuickSave_Source_Class, null).ToString("0");
                        _View_State_TextList[2].text =
                            QuickSave_Object_Script.Key_Material("Weight", QuickSave_Source_Class, null).ToString("0");
                        _View_State_TextList[3].text =
                            QuickSave_Object_Script.Key_Material("Purity", QuickSave_Source_Class, null).ToString("0");
                    }
                    break;
                case "Item":
                    {
                        _Item_ItemUnit QuickSave_Item_Script =
                            _Data_Owner_Script._Basic_Source_Class.Source_Item;
                        _Map_BattleObjectUnit QuickSave_Object_Script =
                            QuickSave_Item_Script._Basic_Object_Script;
                        SourceClass QuickSave_Source_Class =
                            QuickSave_Object_Script._Basic_Source_Class;

                        _View_Color_Image.color = QuickSave_Item_Script._Basic_Color_Color;
                        _View_Faction_Text.text = QuickSave_Object_Script._Skill_Faction_Script._Basic_Language_Class.Name;
                        _View_State_TextList[0].text =
                            QuickSave_Object_Script.Key_Material("Size", QuickSave_Source_Class, null).ToString("0");
                        _View_State_TextList[1].text =
                            QuickSave_Object_Script.Key_Material("Form", QuickSave_Source_Class, null).ToString("0");
                        _View_State_TextList[2].text =
                            QuickSave_Object_Script.Key_Material("Weight", QuickSave_Source_Class, null).ToString("0");
                        _View_State_TextList[3].text =
                            QuickSave_Object_Script.Key_Material("Purity", QuickSave_Source_Class, null).ToString("0");
                    }
                    break;
            }
        }
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
}
