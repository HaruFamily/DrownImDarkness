using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class _View_QuickView : MonoBehaviour
{
    //——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------

    //QuickView----------------------------------------------------------------------------------------------------
    public Image _View_Character_Image;
    public Text _View_CharacterName_Text;

    public _UI_Manager.UIBarClass[] View_Points_ClassArray;
    public Text _View_ConsciousnessPoint_Text;
    public Transform _View_ConsciousnessPointStore_Transform;
    [HideInInspector] public List<_Object_ViewConsumeUnit> _View_ConsciousnessPoint_Scripts = new List<_Object_ViewConsumeUnit>();

    public List<Text> _View_StatusValue_TextList = new List<Text>();
    public List<Text> _View_EquipStatusValue_TextList = new List<Text>();

    public Transform _View_EquipStatusPolygonStore_Transform;
    public GameObject _View_EquipStatusPolygon_GameObject;
    public List<UIPolygon> _View_EquipStatusPolygon_ScrtipsList = new List<UIPolygon>();

    public List<_View_Equipment_StateBar> _View_EquipBar_ScriptsList = new List<_View_Equipment_StateBar>();

    public List<_UI_HintEffect> QuickSave_NewHint_ScriptsList = new List<_UI_HintEffect>();
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public bool _View_Lock_Bool = false;
    private float QuickSave_ConsciousnessPointLimitSave_Float = 0;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void QuickViewSet()
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        //----------------------------------------------------------------------------------------------------

        _View_Lock_Bool = !_View_Lock_Bool;

        if (_View_Lock_Bool)
        {
            transform.gameObject.SetActive(true);
            //FirstSet
            _Object_CreatureUnit _Basic_Owner_Script = _World_Manager._Object_Manager._Object_Player_Script;
            _Item_Object_Inventory QuickSave_Inventory_Script = _Basic_Owner_Script._Object_Inventory_Script;
            _Item_ConceptUnit QuickSave_CreatureConcept_Script = QuickSave_Inventory_Script._Item_EquipConcepts_Script;
            _Map_BattleObjectUnit QuickSave_ConceptObject_Script = QuickSave_CreatureConcept_Script._Basic_Object_Script;
            SourceClass QuickSave_ConceptSource_Class = QuickSave_ConceptObject_Script._Basic_Source_Class;

            switch (_World_Manager._Authority_Scene_String)
            {
                case "Camp":
                    transform.localPosition = Vector3.zero;
                    break;
                case "Field":
                case "Battle":
                    transform.localPosition = new Vector3(-260, 0, 0);
                    break;
            }

            _View_Character_Image.sprite = 
                _View_Manager.GetSprite("Creature", "CG", "Creature_Immo_0");
            _View_CharacterName_Text.text = _World_Manager._World_GeneralManager._World_TextManager._Language_UIName_Dictionary["Immo/Miio"];
            //QuickView_CreatureFaction_Text.text = QuickSave_CreatureConcept_Script._Skill_Faction_Script._View_Name_Text.text;

            NumbericalValueClass QuickSave_MediumPoint_Class = QuickSave_ConceptObject_Script._Basic_Point_Dictionary["MediumPoint"];
            NumbericalValueClass QuickSave_CatalystPoint_Class = QuickSave_ConceptObject_Script._Basic_Point_Dictionary["CatalystPoint"];
            float QuickSave_MediumPoint_Float = QuickSave_MediumPoint_Class.Point / QuickSave_CatalystPoint_Class.Point;
            float QuickSave_CatalystPoint_Float = QuickSave_CatalystPoint_Class.Point / QuickSave_CatalystPoint_Class.Total();
            View_Points_ClassArray[0].BarValue.text = 
                "MP:" + QuickSave_MediumPoint_Class.Point.ToString("0");
            View_Points_ClassArray[0].BarTransform.localScale = new Vector3(QuickSave_MediumPoint_Float, 1, 1);
            View_Points_ClassArray[1].BarValue.text =
                "CP:" + QuickSave_CatalystPoint_Class.Point.ToString("0");
            View_Points_ClassArray[1].BarTransform.localScale = new Vector3(QuickSave_CatalystPoint_Float, 1, 1);

            NumbericalValueClass QuickSave_Status_Class = QuickSave_ConceptObject_Script._Basic_Point_Dictionary["ConsciousnessPoint"];
            float QuickSave_PointLimit = QuickSave_Status_Class.Total();
            int QuickSave_NewSize_Int = Mathf.CeilToInt(QuickSave_PointLimit / 4);
            Color QuickSave_Consciousness_Color = _World_Manager._View_Manager.GetColor("Status", "Consciousness");
            Color QuickSave_Dustal_Color = _World_Manager._View_Manager.GetColor("Code", "Empty");
            //上限重新設定

            //上限重新設定
            if (QuickSave_ConsciousnessPointLimitSave_Float != QuickSave_PointLimit)
            {
                if (_View_ConsciousnessPoint_Scripts.Count < QuickSave_NewSize_Int)
                {
                    for (int a = _View_ConsciousnessPoint_Scripts.Count; a < QuickSave_NewSize_Int; a++)
                    {
                        _View_ConsciousnessPoint_Scripts.Add(
                            Instantiate(_UI_Manager._UI_QuarterUnit_GameObject, _View_ConsciousnessPointStore_Transform).
                            GetComponent<_Object_ViewConsumeUnit>());
                    }
                }
                QuickSave_ConsciousnessPointLimitSave_Float = QuickSave_PointLimit;
            }

            //列表設定
            List<Image> QuickSave_BattleConsumeQuarter_ImageList = new List<Image>();
            for (int a = 0; a < _View_ConsciousnessPoint_Scripts.Count; a++)
            {
                QuickSave_BattleConsumeQuarter_ImageList.AddRange(_View_ConsciousnessPoint_Scripts[a]._View_ConsumeQuarter_ImageArray);
                if (a < Mathf.CeilToInt(QuickSave_PointLimit / 4))
                {
                    _View_ConsciousnessPoint_Scripts[a]._View_ConsumeHardBack_Image.color = Color.white;
                    _View_ConsciousnessPoint_Scripts[a]._View_ConsumeTopBack_Image.color = Color.white;
                    _View_ConsciousnessPoint_Scripts[a]._View_ConsumeLightBack_Image.color = Color.white;
                }
                else
                {
                    _View_ConsciousnessPoint_Scripts[a]._View_ConsumeHardBack_Image.color = Color.clear;
                    _View_ConsciousnessPoint_Scripts[a]._View_ConsumeTopBack_Image.color = Color.clear;
                    _View_ConsciousnessPoint_Scripts[a]._View_ConsumeLightBack_Image.color = Color.clear;
                }
            }
            //單位設定
            for (int a = 0; a < QuickSave_BattleConsumeQuarter_ImageList.Count; a++)
            {
                if (a < QuickSave_Status_Class.Point)
                {
                    QuickSave_BattleConsumeQuarter_ImageList[a].color = QuickSave_Consciousness_Color;
                }
                else if (a < QuickSave_PointLimit)
                {
                    QuickSave_BattleConsumeQuarter_ImageList[a].color = QuickSave_Dustal_Color;
                }
                else
                {
                    QuickSave_BattleConsumeQuarter_ImageList[a].color = new Color32(0, 0, 0, 0);
                }
            }
            //文字設定
            _View_ConsciousnessPoint_Text.text =
                QuickSave_Status_Class.Point.ToString("0");


            _View_StatusValue_TextList[0].text = QuickSave_ConceptObject_Script.Key_Status("Medium", QuickSave_ConceptSource_Class, null, null).ToString("0");
            _View_StatusValue_TextList[1].text = QuickSave_ConceptObject_Script.Key_Status("Catalyst", QuickSave_ConceptSource_Class, null, null).ToString("0");
            _View_StatusValue_TextList[2].text = QuickSave_ConceptObject_Script.Key_Status("Consciousness", QuickSave_ConceptSource_Class, null, null).ToString("0");
            _View_StatusValue_TextList[3].text = QuickSave_ConceptObject_Script.Key_Status("Vitality", QuickSave_ConceptSource_Class, null, null).ToString("0");
            _View_StatusValue_TextList[4].text = QuickSave_ConceptObject_Script.Key_Status("Strength", QuickSave_ConceptSource_Class, null  , null).ToString("0");
            _View_StatusValue_TextList[5].text = QuickSave_ConceptObject_Script.Key_Status("Precision", QuickSave_ConceptSource_Class, null, null).ToString("0");
            _View_StatusValue_TextList[6].text = QuickSave_ConceptObject_Script.Key_Status("Speed", QuickSave_ConceptSource_Class, null, null).ToString("0");
            _View_StatusValue_TextList[7].text = QuickSave_ConceptObject_Script.Key_Status("Luck", QuickSave_ConceptSource_Class, null, null).ToString("0");

            for (int a= 0; a < 4; a++)
            {
                _View_EquipStatusValue_TextList[a].text = 
                    QuickSave_Inventory_Script._Item_EquipStatus_ClassArray[a].Total().ToString("0");
            }

            for (int a = 0; a < 9; a++)
            {
                if (QuickSave_Inventory_Script._Item_EquipQueue_ScriptsList.Count > a)
                {
                    _View_EquipBar_ScriptsList[a].
                        SetView(QuickSave_Inventory_Script._Item_EquipQueue_ScriptsList[a]);
                }
                else
                {
                    _View_EquipBar_ScriptsList[a].
                        SetView(null);
                }
            }



            for (int a = 0; a < _View_EquipStatusPolygon_ScrtipsList.Count; a++)
            {
                Destroy(_View_EquipStatusPolygon_ScrtipsList[a].gameObject);
            }
            _View_EquipStatusPolygon_ScrtipsList.Clear();

            float QuickSave_TotalSize_Float = 0;
            float QuickSave_TotalForm_Float = 0;
            float QuickSave_TotalWeight_Float = 0;
            float QuickSave_TotalPurity_Float = 0;
            foreach (_Map_BattleObjectUnit Object in QuickSave_Inventory_Script._Item_EquipQueue_ScriptsList)
            {
                _Item_Manager.MaterialDataClass QuickSave_Material_Class = null;
                Color32 QuickSave_Color_Color = Color.clear;
                SourceClass QuickSave_Source_Class = Object._Basic_Source_Class;
                switch (QuickSave_Source_Class.SourceType)
                {
                    case "Concept":
                        QuickSave_Material_Class =
                            QuickSave_Source_Class.Source_Concept._Basic_Object_Script._Basic_Material_Class;
                        QuickSave_Color_Color =
                            QuickSave_Source_Class.Source_Concept._Basic_Color_Color;
                        break;
                    case "Weapon":
                        QuickSave_Material_Class = 
                            QuickSave_Source_Class.Source_Weapon._Basic_Object_Script._Basic_Material_Class;
                        QuickSave_Color_Color = 
                            QuickSave_Source_Class.Source_Weapon._Basic_Color_Color;
                        break;
                    case "Item":
                        QuickSave_Material_Class = 
                            QuickSave_Source_Class.Source_Item._Basic_Object_Script._Basic_Material_Class;
                        QuickSave_Color_Color = 
                            QuickSave_Source_Class.Source_Item._Basic_Color_Color;
                        break;
                        default:
                            break;
                }
                QuickSave_TotalSize_Float +=
                    Object.Key_Material("Size", QuickSave_Source_Class, null);
                QuickSave_TotalForm_Float += 
                    Object.Key_Material("Form", QuickSave_Source_Class, null);
                QuickSave_TotalWeight_Float += 
                    Object.Key_Material("Weight", QuickSave_Source_Class, null);
                QuickSave_TotalPurity_Float += 
                    Object.Key_Material("Purity", QuickSave_Source_Class, null);
                if (QuickSave_Source_Class.SourceType != "Concept")
                {
                    UIPolygon QuickSave_Polygon_Script =
                        Instantiate(_View_EquipStatusPolygon_GameObject, _View_EquipStatusPolygonStore_Transform).GetComponent<UIPolygon>();
                    QuickSave_Polygon_Script.VerticesDistances[3] =
                        Mathf.Clamp(QuickSave_TotalSize_Float / 200, 0, 1);
                    QuickSave_Polygon_Script.VerticesDistances[0] =
                        Mathf.Clamp(QuickSave_TotalForm_Float / 200, 0, 1);
                    QuickSave_Polygon_Script.VerticesDistances[2] =
                        Mathf.Clamp(QuickSave_TotalWeight_Float / 200, 0, 1);
                    QuickSave_Polygon_Script.VerticesDistances[1] =
                        Mathf.Clamp(QuickSave_TotalPurity_Float / 200, 0, 1);
                    QuickSave_Polygon_Script.GetComponentInChildren<Image>().color = QuickSave_Color_Color;
                    StartCoroutine(_View_Manager.EquipStatusPolygonAnimate(QuickSave_Polygon_Script));
                    QuickSave_Polygon_Script.transform.SetAsFirstSibling();
                    _View_EquipStatusPolygon_ScrtipsList.Add(QuickSave_Polygon_Script);
                }
            }
            //當前獲得的素材
            {
                _View_EquipBar_ScriptsList[9]._View_State_TextList[0].text =
                    (QuickSave_Inventory_Script._Item_EquipStatus_ClassArray[0].Point - QuickSave_TotalSize_Float).ToString("0");
                _View_EquipBar_ScriptsList[9]._View_State_TextList[1].text =
                    (QuickSave_Inventory_Script._Item_EquipStatus_ClassArray[1].Point - QuickSave_TotalForm_Float).ToString("0");
                _View_EquipBar_ScriptsList[9]._View_State_TextList[2].text =
                    (QuickSave_Inventory_Script._Item_EquipStatus_ClassArray[2].Point - QuickSave_TotalWeight_Float).ToString("0");
                _View_EquipBar_ScriptsList[9]._View_State_TextList[3].text =
                    (QuickSave_Inventory_Script._Item_EquipStatus_ClassArray[3].Point - QuickSave_TotalPurity_Float).ToString("0");
                _View_EquipBar_ScriptsList[9]._View_Color_Image.color =
                    QuickSave_CreatureConcept_Script._Basic_Color_Color;
            }

            //剩餘空間
            {
                UIPolygon QuickSave_CreaturePolygon_Script =
                    Instantiate(_View_EquipStatusPolygon_GameObject, _View_EquipStatusPolygonStore_Transform).GetComponent<UIPolygon>();
                for (int a = 0; a < QuickSave_Inventory_Script._Item_EquipStatus_ClassArray.Length; a++)
                {
                    QuickSave_CreaturePolygon_Script.VerticesDistances[a] =
                        Mathf.Clamp(QuickSave_Inventory_Script._Item_EquipStatus_ClassArray[a].Total() / 200, 0, 1);
                }
                QuickSave_CreaturePolygon_Script.GetComponentInChildren<Image>().color =
                    QuickSave_Inventory_Script._Item_EquipConcepts_Script._Basic_Color_Color;
                StartCoroutine(_View_Manager.EquipStatusPolygonAnimate(QuickSave_CreaturePolygon_Script));
                QuickSave_CreaturePolygon_Script.transform.SetAsFirstSibling();
                _View_EquipStatusPolygon_ScrtipsList.Add(QuickSave_CreaturePolygon_Script);
                for (int a = 0; a < 4; a++)
                {
                    _View_EquipBar_ScriptsList[10]._View_State_TextList[a].text =
                        QuickSave_Inventory_Script._Item_EquipStatus_ClassArray[a].Gap().ToString("0");
                }
                _View_EquipBar_ScriptsList[10]._View_Color_Image.color =
                    QuickSave_CreatureConcept_Script._Basic_Color_Color;
            }

            if (_World_Manager._Authority_Scene_String != "Camp")
            {
                return;
            }
            //Skill
            if (_UI_Manager._UI_Hint_Dictionary["New"]["Faction"].Count > 0)
            {
                QuickSave_NewHint_ScriptsList[2].HintSet("New", "Menu");
            }
            else
            {
                QuickSave_NewHint_ScriptsList[2].HintSet("UnNew", "Menu");
            }
            //Inventory
            if (_UI_Manager._UI_Hint_Dictionary["New"]["Weapon"].Count > 0 ||
                _UI_Manager._UI_Hint_Dictionary["New"]["Item"].Count > 0 ||
                _UI_Manager._UI_Hint_Dictionary["New"]["Collection"].Count > 0 ||
                _UI_Manager._UI_Hint_Dictionary["New"]["Material"].Count > 0 ||
                _UI_Manager._UI_Hint_Dictionary["New"]["Recipe"].Count > 0)
            {
                QuickSave_NewHint_ScriptsList[3].HintSet("New", "Menu");
            }
            else
            {
                QuickSave_NewHint_ScriptsList[3].HintSet("UnNew", "Menu");
            }
            //Alchemy
            if (_UI_Manager._UI_Hint_Dictionary["New"]["Recipe"].Count > 0)
            {
                QuickSave_NewHint_ScriptsList[4].HintSet("New", "Menu");
            }
            else
            {
                QuickSave_NewHint_ScriptsList[4].HintSet("UnNew", "Menu");
            }

            //Miio
            {
                if (_UI_Manager._UI_EventManager._Dialogue_DialogueCamp_Dictionary.TryGetValue("Miio", out List<string> Values))
                {
                    if (Values.Count > 0)
                    {
                        QuickSave_NewHint_ScriptsList[5].HintSet("New", "Menu");
                    }
                }
                else
                {
                    QuickSave_NewHint_ScriptsList[5].HintSet("UnNew", "Menu");
                }
            }
        }
        else
        {
            //close
            this.gameObject.SetActive(false);
        }        
    }
    //——————————————————————————————————————————————————————————————————————
}
