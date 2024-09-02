using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Map_BattleSequenceUnit : MonoBehaviour
{
    //���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //UI----------------------------------------------------------------------------------------------------
    public Text _View_Name_Text;
    public Image _View_Image_Image;
    public Text _View_Time_Text;
    //��T��
    public Transform _UI_BubbleStore_Transform;
    //----------------------------------------------------------------------------------------------------

    //�ܼ�----------------------------------------------------------------------------------------------------
    //�ɶ�
    public int _Battle_Time_Int;
    //����
    public int _Battle_Order_Int;
    //���������d�P
    public _UI_Card_Unit _Battle_Card_Script;
    //�����ͪ�
    public _Object_CreatureUnit _Battle_Creature_Script;

    //�a����ܦs��
    private List<Vector> _View_GroundPath_ClassList = new List<Vector>();
    private List<Vector> _View_GroundSelect_ClassList = new List<Vector>();
    //----------------------------------------------------------------------------------------------------

    //��T----------------------------------------------------------------------------------------------------
    private GameObject _UI_Infos_GameObject;
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    #region View
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void ViewSet(_UI_Card_Unit Card, _Object_CreatureUnit Creature, int Time,int Order)
    {
        _Battle_Card_Script = Card;
        _Battle_Creature_Script = Creature;
        _Battle_Time_Int = Time;
        _Battle_Order_Int = Order;
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�ƹ��ƤJ�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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

        //�d����----------------------------------------------------------------------------------------------------
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

            //�Z�����----------------------------------------------------------------------------------------------------
            //�]�w�Z���ϥ�
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

            //�a�O���----------------------------------------------------------------------------------------------------
            //�O���s�W
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

                //�C��]�m
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

            //�d����T���----------------------------------------------------------------------------------------------------
            GameObject QuickSave_Behavior_GameObject = _Battle_Card_Script._Basic_View_Script._View_BehaviorInfo_GameObject;
            QuickSave_Behavior_GameObject.transform.SetParent(_UI_BubbleStore_Transform);
            QuickSave_Behavior_GameObject.SetActive(true);
            QuickSave_Behavior_GameObject.transform.localScale = new Vector3(-1, 1, 1);
            _UI_Infos_GameObject =_Battle_Card_Script._Basic_View_Script._View_BehaviorInfo_GameObject;
            _Battle_Card_Script._Basic_View_Script.DetailSet("Behavior", _Battle_Time_Int, _Battle_Order_Int);
            //----------------------------------------------------------------------------------------------------

            //�y�����----------------------------------------------------------------------------------------------------
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�ƹ��ƥX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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

        //�d����----------------------------------------------------------------------------------------------------
        State_MouseIn_Bool = false;
        if (_Battle_Card_Script != null)
        {
            _Skill_FactionUnit QuickSave_Faction_Script = _Battle_Card_Script._Card_BehaviorUnit_Script._Owner_Faction_Script;

            //�Z�����----------------------------------------------------------------------------------------------------
            //�]�w�Z���ϥ�
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

            //�a�O���----------------------------------------------------------------------------------------------------
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

            //�d�����----------------------------------------------------------------------------------------------------
            _UI_Infos_GameObject.transform.SetParent(_Battle_Card_Script._Basic_View_Script._View_InfoStore_Transform);
            _UI_Infos_GameObject.transform.SetSiblingIndex(0);
            _UI_Infos_GameObject.transform.localScale = new Vector3(1, 1, 1);
            _UI_Infos_GameObject.SetActive(false);
            _UI_Infos_GameObject = null;
            //----------------------------------------------------------------------------------------------------

            //�y�����----------------------------------------------------------------------------------------------------
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
}
