using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _World_ScenesManager : MonoBehaviour
{
    #region ElementBox
    //場景存取——————————————————————————————————————————————————————————————————————//----------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------
    //轉場位置
    public Transform _Fade_Fade_Transform;
    //轉場圖片
    public Image _Fade_Image_Transform;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion ElementBox


    #region SystemStart
    //起始設定——————————————————————————————————————————————————————————————————————
    public void SystemStart()
    {
        //變數----------------------------------------------------------------------------------------------------
        string QuickSave_SceneName_String = SceneManager.GetActiveScene().name;
        switch (QuickSave_SceneName_String)
        {
            case "Main":
                {
                    QuickSave_SceneName_String = "Camp";
                    //開啟顯示
                    _Fade_Fade_Transform.gameObject.SetActive(true);
                    //設定變數
                    _World_Manager._Authority_Scene_String = QuickSave_SceneName_String;
                }
                break;
            default:
                {
                    //開啟顯示
                    _Fade_Fade_Transform.gameObject.SetActive(true);
                    //設定變數
                    _World_Manager._Authority_Scene_String = QuickSave_SceneName_String;
                    //執行起始行為
                    SwitchScenes(QuickSave_SceneName_String);
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion SystemStart


    #region ScenesSet
    //——————————————————————————————————————————————————————————————————————
    //讀取場景----------------------------------------------------------------------------------------------------
    public void SwitchScenes(string ScenesName)
    {
        _World_Manager._Authority_UICover_Bool = false;
        if (_World_Manager._Authority_Scene_String == ScenesName)
        {
            StartCoroutine(FadeIn(ScenesName));
        }
        else
        {
            StartCoroutine(FadeOut(ScenesName));
        }
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion ScenesSet


    #region Fade
    private int QuickSave_TimePass_Int = 0;
    //淡入——————————————————————————————————————————————————————————————————————
    public IEnumerator FadeIn(string ScenesName)
    {
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;

        //切換後場景事件----------------------------------------------------------------------------------------------------
        _World_Manager._Authority_Scene_String = ScenesName;
        switch (ScenesName)
        {
            case "Index":
                _World_Manager._Authority_UICover_Bool = true;
                break;
            case "Main":
                break;
            case "Camp":
                _World_Manager._UI_Manager.
                        ChangeTraceTarget(null);
                _UI_Manager._UI_Camp_Transform.gameObject.SetActive(true);
                _UI_Manager._UI_FieldBattle_Transform.gameObject.SetActive(false);
                _World_Manager._Authority_UICover_Bool = true;
                break;
            case "Field":
                _World_Manager._UI_Manager.
                        ChangeTraceTarget(_Object_Manager._Object_Player_Script._Creature_FieldObjectt_Script.transform);

                _World_Manager._Map_Manager.ManagerStart(QuickSave_TimePass_Int);
                _World_Manager._UI_Manager._View_Battle.
                    UsingObjectSet("Target", Target: _Object_Manager._Object_Player_Script._Card_UsingObject_Script);

                //加入智總生物列表
                _UI_Manager._View_Battle.gameObject.SetActive(true);
                _UI_Manager._View_Battle._View_Sequences_Transform.gameObject.SetActive(false);
                _UI_Manager._UI_Camp_Transform.gameObject.SetActive(false);
                _UI_Manager._UI_FieldBattle_Transform.gameObject.SetActive(true);
                break;
            case "Battle":
                _World_Manager._UI_Manager.
                        ChangeTraceTarget(_Object_Manager._Object_Player_Script._Basic_Object_Script.transform);

                _World_Manager._Map_Manager.ManagerStart();

                _UI_Manager._View_Battle.gameObject.SetActive(true);
                _UI_Manager._View_Battle._View_Sequences_Transform.gameObject.SetActive(true);
                break;
            default:
                print("ScenesName_String：﹝" + ScenesName + "﹞ is Wrong String");
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //動畫----------------------------------------------------------------------------------------------------
        _Fade_Fade_Transform.gameObject.SetActive(true);
        Color QuickSave_Alpha_Color = Color.clear;
        for(int a = 0; a < 25 / (_World_Manager._Config_AnimationSpeed_Float / 100); a++)
        {
            _Fade_Image_Transform.color = 
                Color.Lerp(_Fade_Image_Transform.color, 
                QuickSave_Alpha_Color, a * 0.04f * (_World_Manager._Config_AnimationSpeed_Float / 100));
            yield return new WaitForSeconds(0.03f);
        }
        _Fade_Fade_Transform.gameObject.SetActive(false);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //淡出——————————————————————————————————————————————————————————————————————
    public IEnumerator FadeOut(string ScenesName)
    {
        //----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //動畫前設置----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Battle":
                {
                    switch (ScenesName)
                    {
                        case "Field":
                            {
                                //慢動作特效
                                yield return new WaitForSeconds(0.35f);
                                for (int a = 0; a < 11; a ++)
                                {
                                    Time.timeScale =
                                        Mathf.Lerp(0.2f,
                                        1, a * 0.1f);
                                    yield return new WaitForSeconds(0.1f);
                                }
                                yield return new WaitForSeconds(0.5f);
                            }
                            break;
                    }    
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
        
        //動畫----------------------------------------------------------------------------------------------------        
        _Fade_Fade_Transform.gameObject.SetActive(true);
        Color QuickSave_Black_Color = Color.black;
        for (int a = 0; a < 25 / (_World_Manager._Config_AnimationSpeed_Float / 100); a++)
        {
            _Fade_Image_Transform.color = 
                Color.Lerp(_Fade_Image_Transform.color, 
                QuickSave_Black_Color, a * 0.04f * (_World_Manager._Config_AnimationSpeed_Float / 100));
            yield return new WaitForSeconds(0.04f);
        }
        _Fade_Fade_Transform.gameObject.SetActive(false);
        //----------------------------------------------------------------------------------------------------

        //切換前場景事件----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Index":
                break;
            case "Main":
                break;
            case "Camp":
                {
                    List<string> QuickSave_Keys_StringList = new List<string>();
                    foreach (string Key in _UI_Manager._UI_TextEffectLocking_Dictionary.Keys)
                    {
                        QuickSave_Keys_StringList.Add(Key);
                    }
                    for (int a = 0; a < QuickSave_Keys_StringList.Count; a++)
                    {
                        _UI_Manager.TextEffectDictionarySet(QuickSave_Keys_StringList[a], null);
                    }
                }
                break;
            case "Field":
                {
                    switch (ScenesName)
                    {
                        case "Camp":
                            {
                                _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
                                _Object_CreatureUnit QuickSave_Creature_Script = _Object_Manager._Object_Player_Script;
                                _Map_Manager.ClearMap();
                                //玩家回復
                                QuickSave_Creature_Script._Basic_Object_Script._Round_Unit_Class.TargetTime = 0;
                                _Object_Manager.FieldToHold(QuickSave_Creature_Script);
                                //加入智總生物列表
                                _UI_Manager._View_Battle.gameObject.SetActive(false);
                                _UI_Manager._View_Battle._View_Sequences_Transform.gameObject.SetActive(false);
                                _UI_Manager._UI_FieldBattle_Transform.gameObject.SetActive(false);
                            }
                            break;
                        case "Battle":
                            {
                                _UI_Manager.UIExit(_UI_Manager._MouseTarget_GameObject);
                            }
                            break;
                    }
                }
                break;
            case "Battle":
                {
                    switch (ScenesName)
                    {
                        case "Field":
                        case "Camp":
                            {
                                _Map_BattleRound _Map_BattleRound = _World_Manager._Map_Manager._Map_BattleRound;
                                _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
                                _Object_CreatureUnit QuickSave_Creature_Script = _Object_Manager._Object_Player_Script;
                                //地圖清理
                                _Map_Manager.ClearMap();
                                QuickSave_TimePass_Int =//時間經過
                                    Mathf.RoundToInt(_Map_BattleRound._Round_Time_Int * 0.05f);
                                _Map_BattleRound._Round_Time_Int = 0;
                                _Map_BattleRound._Round_Order_Int = 0;
                                _Map_BattleRound._Round_RoundCreatures_ClassList.Clear();
                                _Map_BattleRound._Round_RoundSequencePriority_ClassList.Clear();
                                _Map_BattleRound._Round_RoundSequence_ClassList.Clear();
                                _World_Manager._Map_Manager._Map_BattleComplete_Bool = false;
                                //玩家回復
                                QuickSave_Creature_Script._Basic_Object_Script._Round_Unit_Class.TargetTime = 0;
                                _Object_Manager.BattleToField(QuickSave_Creature_Script);
                                //清理物件
                                List<_Map_BattleObjectUnit> QuickSave_Delete_ScriptsList =
                                    new List<_Map_BattleObjectUnit>();
                                List<_Object_CreatureUnit> QuickSave_NPCDelete_ScriptsList =
                                    new List<_Object_CreatureUnit>();
                                List<string> QuickSave_Keys_StringList =
                                    new List<string> { "Concept", "Weapon", "Item", "Object", "Ground", "Project", "TimePos" };
                                foreach (string Key in QuickSave_Keys_StringList)
                                {
                                    List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                        _Object_Manager._Basic_SaveData_Class.ObjectListDataGet(Key);
                                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                                    {
                                        if (Object._Basic_BornScene_String != _World_Manager._Authority_Scene_String)
                                        {
                                            continue;
                                        }
                                        QuickSave_Delete_ScriptsList.Add(Object);
                                        if (Key == "Concept")
                                        {
                                            QuickSave_NPCDelete_ScriptsList.Add(Object._Map_AffiliationOwner_Script);
                                        }
                                    }
                                    _Object_Manager._Basic_SaveData_Class.ObjectListDataSet(Key, null);
                                }
                                foreach (_Map_BattleObjectUnit Object in QuickSave_Delete_ScriptsList)
                                {
                                    Object.DeleteSet();
                                }
                                foreach (_Object_CreatureUnit Creature in QuickSave_NPCDelete_ScriptsList)
                                {
                                    Destroy(Creature.gameObject);
                                }
                                _UI_Manager._View_Battle.FocusSet(QuickSave_Creature_Script._Card_UsingObject_Script);
                                //直通Camp
                                if (ScenesName == "Camp")
                                {
                                    //玩家回復
                                    QuickSave_Creature_Script._Basic_Object_Script._Round_Unit_Class.TargetTime = 0;
                                    _Object_Manager.FieldToHold(QuickSave_Creature_Script);
                                    //加入智總生物列表
                                    _UI_Manager._View_Battle.gameObject.SetActive(false);
                                    _UI_Manager._View_Battle._View_Sequences_Transform.gameObject.SetActive(false);
                                    _UI_Manager._UI_FieldBattle_Transform.gameObject.SetActive(false);
                                }
                            }
                            break;
                    }
                }
                break;
            default:
                print("_Authority_Scene_String：﹝" + _World_Manager._Authority_Scene_String + "﹞ is Wrong String");
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _World_Manager._Authority_Scene_String = ScenesName;
        //----------------------------------------------------------------------------------------------------

        //切換場景事件----------------------------------------------------------------------------------------------------
        switch (ScenesName)
        {
            case "Index":
                SceneManager.LoadScene(ScenesName, LoadSceneMode.Single);
                break;
            case "Main":
                SceneManager.LoadScene(ScenesName, LoadSceneMode.Single);
                StartCoroutine(FadeIn(ScenesName));
                break;
            case "Camp":
            case "Field":
            case "Battle":
                StartCoroutine(FadeIn(ScenesName));
                break;
            default:
                print("ScenesName_String：﹝" + ScenesName + "﹞ is Wrong String");
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Fade
}
