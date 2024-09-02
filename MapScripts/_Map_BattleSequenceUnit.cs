using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Map_BattleSequenceUnit : MonoBehaviour
{
    //物件集——————————————————————————————————————————————————————————————————————
    //UI----------------------------------------------------------------------------------------------------
    public Text _View_Name_Text;
    public Image _View_Image_Image;
    public Text _View_Time_Text;
    //資訊欄
    public Transform _UI_BubbleStore_Transform;
    //----------------------------------------------------------------------------------------------------

    //變數----------------------------------------------------------------------------------------------------
    //時間
    public int _Battle_Time_Int;
    //順序
    public int _Battle_Order_Int;
    //對應持有卡牌
    public _UI_Card_Unit _Battle_Card_Script;
    //對應生物
    public _Object_CreatureUnit _Battle_Creature_Script;

    //地塊顯示存取
    private List<Vector> _View_GroundPath_ClassList = new List<Vector>();
    private List<Vector> _View_GroundSelect_ClassList = new List<Vector>();
    //----------------------------------------------------------------------------------------------------

    //資訊----------------------------------------------------------------------------------------------------
    private GameObject _UI_Infos_GameObject;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    #region View
    //——————————————————————————————————————————————————————————————————————
    public void ViewSet(_UI_Card_Unit Card, _Object_CreatureUnit Creature, int Time,int Order)
    {
        _Battle_Card_Script = Card;
        _Battle_Creature_Script = Creature;
        _Battle_Time_Int = Time;
        _Battle_Order_Int = Order;
    }
    //——————————————————————————————————————————————————————————————————————

    //滑鼠滑入——————————————————————————————————————————————————————————————————————
    bool State_MouseIn_Bool = false;
    public void MouseIn()
    {
        //----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;

        if (_Map_Manager._State_Reacting_Bool &&
            _Battle_Time_Int == _Map_BattleRound._Round_Time_Int &&
            _Battle_Order_Int == _Map_BattleRound._Round_Order_Int)
        {
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //卡片類----------------------------------------------------------------------------------------------------
        State_MouseIn_Bool = true;
        if (_Battle_Creature_Script != null)
        {
            _Map_BattleObjectUnit QuickSave_ConceptObject_Script = _Battle_Creature_Script._Object_Inventory_Script._Item_EquipConcepts_Script._Basic_Object_Script;
            _World_Manager._UI_Manager._View_Battle.FocusSet(QuickSave_ConceptObject_Script);
        }
        if (_Battle_Card_Script != null)
        {
            _Skill_FactionUnit QuickSave_Faction_Script = 
                _Battle_Card_Script._Card_BehaviorUnit_Script._Owner_Faction_Script;

            //武器顯示----------------------------------------------------------------------------------------------------
            //設定武器圖示
            switch (QuickSave_Faction_Script._Basic_Source_Class.SourceType)
            {
                case "Concept":
                    break;
                case "Weapon":
                    break;
                case "Item":
                    break;
                case "Object":
                    break;
            }
            //----------------------------------------------------------------------------------------------------

            //地板顯示----------------------------------------------------------------------------------------------------
            //板塊新增
            if (_Battle_Card_Script != null)
            {
                Vector QuickSave_Center_Class = _Battle_Card_Script._Card_UseCenter_Class;
                PathSelectPairClass QuickSave_PathSelect_Class = _Battle_Card_Script._Range_UseData_Class;
                //Path
                foreach (PathUnitClass PathUnit in QuickSave_PathSelect_Class.Path)
                {
                    _View_GroundPath_ClassList.AddRange(PathUnit.Path.Path);
                }
                //Select
                foreach (SelectUnitClass SelectUnit in QuickSave_PathSelect_Class.Select)
                {
                    _View_GroundSelect_ClassList.AddRange(SelectUnit.AllVectors());
                }

                //顏色設置
                foreach (Vector Coordinate in _View_GroundSelect_ClassList)
                {
                    if (Coordinate.Vector3Int == QuickSave_Center_Class.Vector3Int)
                    {
                        _Map_Manager.TakeSelectDeQueue(Coordinate).ColorSet("SelectTarget");
                    }
                    else
                    {
                        _Map_Manager.TakeSelectDeQueue(Coordinate).ColorSet("Select");
                    }
                }
                foreach (Vector Coordinate in _View_GroundPath_ClassList)
                {
                    if (!_View_GroundSelect_ClassList.Contains(Coordinate))
                    {
                        _Map_Manager.TakeSelectDeQueue(Coordinate).ColorSet("Path");
                    }
                }
            }
            _View_Name_Text.material = _World_Manager._World_GeneralManager._UI_HDRText_Material;
            //----------------------------------------------------------------------------------------------------

            //卡片資訊顯示----------------------------------------------------------------------------------------------------
            GameObject QuickSave_Behavior_GameObject = _Battle_Card_Script._Basic_View_Script._View_BehaviorInfo_GameObject;
            QuickSave_Behavior_GameObject.transform.SetParent(_UI_BubbleStore_Transform);
            QuickSave_Behavior_GameObject.SetActive(true);
            QuickSave_Behavior_GameObject.transform.localScale = new Vector3(-1, 1, 1);
            _UI_Infos_GameObject =_Battle_Card_Script._Basic_View_Script._View_BehaviorInfo_GameObject;
            _Battle_Card_Script._Basic_View_Script.DetailSet("Behavior", _Battle_Time_Int, _Battle_Order_Int);
            //----------------------------------------------------------------------------------------------------

            //造物顯示----------------------------------------------------------------------------------------------------
            _Map_BattleObjectUnit QuickSave_Object_Script =
                _Battle_Card_Script._Basic_SaveData_Class.ObjectDataGet("TimePos");
            if (QuickSave_Object_Script != null)
            {
                QuickSave_Object_Script._View_Icon_SpriteRenderer.transform.localPosition = Vector3.zero;
            }
            //----------------------------------------------------------------------------------------------------
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //滑鼠滑出——————————————————————————————————————————————————————————————————————
    public void MouseOut()
    {
        //----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        if (!State_MouseIn_Bool)
        {
            return;
        }
        if (_Map_Manager._State_Reacting_Bool &&
            _Battle_Time_Int == _Map_BattleRound._Round_Time_Int &&
            _Battle_Order_Int == _Map_BattleRound._Round_Order_Int)
        {
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //卡片類----------------------------------------------------------------------------------------------------
        State_MouseIn_Bool = false;
        if (_Battle_Card_Script != null)
        {
            _Skill_FactionUnit QuickSave_Faction_Script = _Battle_Card_Script._Card_BehaviorUnit_Script._Owner_Faction_Script;

            //武器顯示----------------------------------------------------------------------------------------------------
            //設定武器圖示
            switch (QuickSave_Faction_Script._Basic_Source_Class.SourceType)
            {
                case "Concept":
                    break;
                case "Weapon":
                    break;
                case "Item":
                    break;
                case "Object":
                    break;
            }
            //----------------------------------------------------------------------------------------------------

            //地板顯示----------------------------------------------------------------------------------------------------
            foreach (Vector Key in _View_GroundPath_ClassList)
            {
                _Map_Manager.TakeSelectDeQueue(Key).ColorSet("Clear");
            }
            foreach (Vector Key in _View_GroundSelect_ClassList)
            {
                _Map_Manager.TakeSelectDeQueue(Key).ColorSet("Clear");
            }
            _View_Name_Text.material = null;
            _View_GroundPath_ClassList.Clear();
            _View_GroundSelect_ClassList.Clear();
            //----------------------------------------------------------------------------------------------------

            //卡片顯示----------------------------------------------------------------------------------------------------
            _UI_Infos_GameObject.transform.SetParent(_Battle_Card_Script._Basic_View_Script._View_InfoStore_Transform);
            _UI_Infos_GameObject.transform.SetSiblingIndex(0);
            _UI_Infos_GameObject.transform.localScale = new Vector3(1, 1, 1);
            _UI_Infos_GameObject.SetActive(false);
            _UI_Infos_GameObject = null;
            //----------------------------------------------------------------------------------------------------

            //造物顯示----------------------------------------------------------------------------------------------------
            _Map_BattleObjectUnit QuickSave_Object_Script =
                _Battle_Card_Script._Basic_SaveData_Class.ObjectDataGet("TimePos");
            if (QuickSave_Object_Script != null)
            {
                QuickSave_Object_Script._View_Icon_SpriteRenderer.gameObject.SetActive(false);
            }
            //----------------------------------------------------------------------------------------------------
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
}
