using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class _World_Manager : MonoBehaviour
{

    #region ManagerCaller
    //設定為全域變數——————————————————————————————————————————————————————————————————————
    static public _World_Manager _World_GeneralManager;
    static public _Map_Manager _Map_Manager;
    static public _Skill_Manager _Skill_Manager;
    static public _UI_Manager _UI_Manager;
    static public _Item_Manager _Item_Manager;
    static public _Effect_Manager _Effect_Manager;
    static public _Object_Manager _Object_Manager;
    static public _View_Manager _View_Manager;
    //——————————————————————————————————————————————————————————————————————
    #endregion ManagerCaller


    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————
    //底下資料集----------------------------------------------------------------------------------------------------
    //攝影機管理
    //public _World_CameraControl _World_CameraControl;
    //場景管理
    public _World_ScenesManager _World_ScenesManager;
    //音效管理
    public _World_SoundManager _World_SoundManager;
    //文本管理
    public _World_TextManager _World_TextManager;
    //----------------------------------------------------------------------------------------------------

    //Config設定集----------------------------------------------------------------------------------------------------
    //效果音大小
    static public int _Config_SoundEffectVolumn_Int;
    //背景音大小
    static public int _Config_SoundBackGroundVolumn_Int;
    //人聲大小
    static public int _Config_SoundVocalVolumn_Int;
    //動畫速度
    static public float _Config_AnimationSpeed_Float = 150;
    //滑鼠縮放速度
    static public int _Config_CameraScaleSpeed_Int = 50;
    //滑鼠縮放速度
    static public int _Config_CameraPositionSpeed_Int = 50;
    //拖曳反轉
    static public short _Config_PullReverse_Short = 1;
    //語言﹝TraditionalChinese﹞
    static public string _Config_Language_String = "TraditionalChinese";
    //----------------------------------------------------------------------------------------------------

    //Test測試顯示----------------------------------------------------------------------------------------------------
    //提示顯示
    static public bool _Test_Hint_Bool = false;
    //----------------------------------------------------------------------------------------------------

    //世界權限變數----------------------------------------------------------------------------------------------------
    //當前場景
    static public string _Authority_Scene_String;
    //當前位置
    static public string _Authority_Map_String;
    static public string _Authority_Weather_String;
    //操作權限﹝滑過UI許可﹞//true = 可以滑過
    static public bool _Authority_UICover_Bool = false;
    //操作權限﹝點擊卡片許可﹞//true = 可以點擊
    static public bool _Authority_CardClick_Bool = false;
    //操作權限﹝調整攝影機許可﹞//true = 可以調整
    static public bool _Authority_CameraSet_Bool = false;

    //操作權限﹝對話點擊使用許可權﹞//true = 可以點擊對話
    static public bool _Authority_DialogueClick_Bool = false;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    //防止無限迴圈
    static public List<string>
        System_SituationHashCode_StringList = null;
    //----------------------------------------------------------------------------------------------------

    //全球狀態----------------------------------------------------------------------------------------------------
    //權限
    static public bool _Privilege_Immo_Bool = false;
    static public bool _Privilege_Miio_Bool = false;
    static public bool _Privilege_Tobana_Bool = false;
    static public bool _Privilege_Lide_Bool = false;
    static public bool _Privilege_Limu_Bool = false;
    static public bool _Privilege_Choco_Bool = false;
    static public bool _Privilege_Rotetis_Bool = false;
    static public bool _Privilege_Nomus_Bool = false;
    //----------------------------------------------------------------------------------------------------

    //慣用指引----------------------------------------------------------------------------------------------------
    public Material _UI_HDRSprite_Material;
    public Material _UI_HDRText_Material;
    //----------------------------------------------------------------------------------------------------

    //Speaker----------------------------------------------------------------------------------------------------
    public bool _Speaker_NumberUnitIsReturn_Bool = false;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion ElementBox


    #region FirstBuild
    //第一行動——————————————————————————————————————————————————————————————————————
    void Awake()
    {
        //設定為全域變數----------------------------------------------------------------------------------------------------
        _World_GeneralManager = this;

        _Authority_Map_String = "Map_Gap";
        _Authority_Weather_String = "Normal";
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion FirstBuild


    #region Start
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void Start()
    {
        //----------------------------------------------------------------------------------------------------
        if (_World_ScenesManager != null)
        {
            _World_ScenesManager.SystemStart();
        }
        if (_World_TextManager != null)
        {
            _World_TextManager.SystemStart();
        }
        //----------------------------------------------------------------------------------------------------

        //共用呼叫----------------------------------------------------------------------------------------------------
        //攝影機行動
        /*
        if (_World_CameraControl != null)
        {
            _World_CameraControl.SystemStart();
        }*/

        //UI呼叫
        if (_UI_Manager != null)
        {
            _UI_Manager.ManagerStart();
        }
        if(_Object_Manager != null)
        {
            _Object_Manager.ManagerStart();
        }
        //
        switch (_Authority_Scene_String)
        {
            case "Camp":
                {
                    print("開始時直接到Field");
                    _World_ScenesManager.SwitchScenes("Field");
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start


    #region Update
    //連續行動——————————————————————————————————————————————————————————————————————
    public void Update()
    {
        //滑鼠屬性取得----------------------------------------------------------------------------------------------------
        _Mouse_PositionOnCamera_Vector = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if(_Mouse_PositionOnCamera_Vector.y > 0.5f)
        {
            if(_Mouse_PositionOnCamera_Vector.x > 0.5f)
            {
                _Mouse_DirectionOnCamera_Short = 1;//右下
            }
            else
            {
                _Mouse_DirectionOnCamera_Short = 2;//左下
            }
        }
        else
        {
            if (_Mouse_PositionOnCamera_Vector.x < 0.5f)
            {
                _Mouse_DirectionOnCamera_Short = 3;//左上
            }
            else
            {
                _Mouse_DirectionOnCamera_Short = 4;//右上
            }
        }
        //----------------------------------------------------------------------------------------------------

        //測試機----------------------------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _World_ScenesManager.SwitchScenes("Camp");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _World_ScenesManager.SwitchScenes("Field");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            switch (_Authority_Scene_String)
            {
                case "Camp":
                    print("Now:" + _UI_Manager._UI_Camp_Class._UI_CampState_String);
                    break;
                case "Field":
                    print("Region：" + _Authority_Map_String + "\n" +
                        _Map_Manager._State_FieldState_String + "\n" +
                        "Event：" + _UI_Manager._UI_EventManager._Event_NowEventKey_String);
                    break;
                case "Battle":
                    print("Times：" + _Map_BattleRound._Round_Time_Int + "\n" +
                        _Map_Manager._State_BattleState_String + "\n" +
                        "_UICover：" + _Authority_UICover_Bool + "\n" +
                        "_CardClick：" + _Authority_CardClick_Bool);
                    break;
                default:
                    print(_Authority_Scene_String);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            switch (_Authority_Scene_String)
            {
                case "Field":
                case "Battle":
                    {
                        _Map_BattleObjectUnit QuickSave_Object_Script = 
                            _Object_Manager._Object_Player_Script._Basic_Object_Script;
                        _UI_Manager._UI_CardManager.
                            CardDeal("Normal", 1, null,
                            QuickSave_Object_Script._Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class, null,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                    break;
                default:
                    print(_Authority_Scene_String);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            _Test_Hint_Bool = !_Test_Hint_Bool;
            print("_Test_Hint_Bool is " + _Test_Hint_Bool);
        }        
        if (Input.GetKeyDown(KeyCode.O))
        {
            string QuickSave_Save_String = System.DateTime.Now.ToString().Replace("上午","Am").Replace("下午", "Pm").Replace("/", "").Replace(":", "_");
            print("ScreenShot_" + QuickSave_Save_String + ".png");
            ScreenCapture.CaptureScreenshot("ScreenShot_" + QuickSave_Save_String + ".png");
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Update

    #region Customary variables
    //物件類——————————————————————————————————————————————————————————————————————
    //空白
    public Sprite _Image_Alpha_Sprite;
    //全白
    public Sprite _Image_White_Sprite;
    //——————————————————————————————————————————————————————————————————————

    //物件變數類——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    //視線移出距離
    public static Vector3 Infinity = new Vector3(65535, 65535, 65535);
    //滑鼠螢幕位置﹝螢幕位置_左下為0.0，右上為1.1﹞
    public Vector2 _Mouse_PositionOnCamera_Vector;
    //滑鼠螢幕方位﹝依照象限﹞
    public short _Mouse_DirectionOnCamera_Short;
    public static _Map_SelectUnit _Mouse_PositionOnSelect_Script;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    #region - Math -
    //運算——————————————————————————————————————————————————————————————————————
    //位數(十進位)----------------------------------------------------------------------------------------------------
    public int DecimalCount(float Number)
    {
        int Answer_Return_Int = 0;
        float QuickSave_NowNumber = Number;
        while (QuickSave_NowNumber > 1)
        {
            Answer_Return_Int++;
            QuickSave_NowNumber /= 10;
        }
        return Answer_Return_Int;
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key -
    //調整變數內容
    //如：
    //AttackValue_SlashDamage｜Concept_Status_Vitality_0(造成依據動力的劈斬傷害) 變為
    //HealValue_MediumPoint｜Concept_Status_Vitality_0(造成依據動力的介質回復)
    //——————————————————————————————————————————————————————————————————————
    public string Key_KeysUnit(string Situation, string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        //回傳值
        string Answer_Return_String = Key;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            case "Data"://資料/如：介質80% 等
                //見上方跳出
                break;
            case "Default"://預設計算/沒有數值變化
                {
                    if (UserSource != null &&
                        UserSource.Source_BattleObject != null)
                    {
                        //Situation//比照被動/效果/詞綴
                        Answer_Return_String =
                            UserSource.Source_BattleObject.SituationCaller(
                                "KeyChange", new List<string> { Key },
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order)["Key"][0];
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_String;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public float Key_NumbersUnit(string Situation, string Key, float Value, 
        SourceClass UserSource, SourceClass TargetSource,_Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //快速跳出----------------------------------------------------------------------------------------------------
        _Speaker_NumberUnitIsReturn_Bool = false;
        if (Situation == "Data")
        {
            _Speaker_NumberUnitIsReturn_Bool = true;
            return Value;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;

        string[] QuickSave_ValueSplit_StringArray = Key.Split("｜"[0]);
        string[] QuickSave_Type_StringArray = QuickSave_ValueSplit_StringArray[0].Split("_"[0]);
        string[] QuickSave_Reference_StringArray = QuickSave_ValueSplit_StringArray[1].Split("_"[0]);


        float QuickSave_EnchanceAdd_Float = 0;//額外增加
        float QuickSave_EnchanceMultiply_Float = 1;
        float QuickSave_AdvanceAdd_Float = 0;//額外增加
        float QuickSave_AdvanceMultiply_Float = 1;
        //使用者捷徑
        _Object_CreatureUnit QuickSave_UserCreature_Script = UserSource.Source_Creature;
        _Map_BattleObjectUnit QuickSave_UsingObject_Script = UserSource.Source_BattleObject;
        if (QuickSave_UsingObject_Script == null && 
            QuickSave_UserCreature_Script != null)
        {
            QuickSave_UsingObject_Script = QuickSave_UserCreature_Script._Basic_Object_Script;
        }
        _UI_Card_Unit QuickSave_UsingCard_Script = UserSource.Source_Card;
        //目標捷徑
        List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList = new List<_Map_BattleObjectUnit>();
        //----------------------------------------------------------------------------------------------------

        //特殊項目----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            case "Data"://資料/如：介質80% 等
                //見上方跳出
                break;
            case "Default"://預設計算/沒有數值變化
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //參考設定----------------------------------------------------------------------------------------------------
        #region - Target -
        {
            SourceClass QuickSave_Source_Class = null;
            //參考對象類型
            switch (QuickSave_Reference_StringArray[0])
            {
                case "Value":
                case "User":
                    {
                        if (UserSource == null)
                        {
                            //資料不完整/回報詳細(EX:目標最大介質值10%)
                            _Speaker_NumberUnitIsReturn_Bool = true;
                            return Value;
                        }
                        QuickSave_Source_Class = UserSource;
                    }
                    break;
                case "Target":
                    {
                        if (TargetSource == null)
                        {
                            //資料不完整/回報詳細(EX:目標最大介質值10%)
                            _Speaker_NumberUnitIsReturn_Bool = true;
                            return Value;
                        }
                        QuickSave_Source_Class = TargetSource;
                    }
                    break;
                case "Using":
                    {
                        if (UsingObject == null)
                        {
                            //資料不完整/回報詳細(EX:目標最大介質值10%)
                            _Speaker_NumberUnitIsReturn_Bool = true;
                            return Value;
                        }
                        QuickSave_Source_Class = UsingObject._Basic_Source_Class;
                    }
                    break;
                case "Card":
                    break;
            }
            //參考對象目標
            switch (QuickSave_Reference_StringArray[1])
            {
                case "Default":
                    {
                        if (QuickSave_Source_Class.Source_BattleObject == null)
                        {
                            //資料不完整/回報詳細(EX:目標最大介質值10%)
                            _Speaker_NumberUnitIsReturn_Bool = true;
                            return Value;
                        }
                        QuickSave_Objects_ScriptsList.Add(QuickSave_Source_Class.Source_BattleObject);
                    }
                    break;
                case "Concept"://通常配合User或Target
                    {
                        if (QuickSave_Source_Class.Source_Creature == null)
                        {
                            //資料不完整/回報詳細(EX:目標最大介質值10%)
                            _Speaker_NumberUnitIsReturn_Bool = true;
                            return Value;
                        }
                        QuickSave_Objects_ScriptsList.Add(QuickSave_Source_Class.Source_Creature._Basic_Object_Script);
                    }
                    break;
                case "Affiliation":
                case "Driving":
                    {
                        if (QuickSave_Source_Class.Source_Creature == null)
                        {
                            //資料不完整/回報詳細(EX:目標最大介質值10%)
                            _Speaker_NumberUnitIsReturn_Bool = true;
                            return Value;
                        }
                        QuickSave_Objects_ScriptsList =
                            QuickSave_Source_Class.Source_Creature.TagObject(
                                QuickSave_Reference_StringArray[1], QuickSave_Reference_StringArray[2],
                                UserSource, TargetSource);
                    }
                    break;
            }
        }
        #endregion
        //----------------------------------------------------------------------------------------------------

        //數值設定----------------------------------------------------------------------------------------------------
        #region - Value -
        //分類
        switch (QuickSave_Reference_StringArray[3])
        {
            #region - Value -
            case "Value":
                {
                    //無參考
                    Answer_Return_Float = Value;
                }
                break;
            #endregion

            #region - Material -
            case "Material":
                {
                    string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //材料值(尺寸等：概念/武器/道具/物體)
                    //基礎值
                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        Answer_Return_Float += 
                            Object.Key_Material(QS_ReferenceTarget_String, UserSource, TargetSource);
                    }
                    //效果加算
                    //Key中已計算
                    //係數計算
                    Answer_Return_Float *= Value;
                }
                break;
            #endregion

            #region - Status -
            case "Status":
                //確認能力值對象
                {
                    string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //能力值(介質等：概念 武器/道具/物體)
                    //基礎值
                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        Answer_Return_Float += 
                            Object.Key_Status(QS_ReferenceTarget_String, UserSource, TargetSource, UsingObject);
                    }
                    //效果加算
                    //Key中已計算
                    //係數計算
                    Answer_Return_Float *= Value;
                }
                break;
            #endregion

            #region - Point -
            case "Point":
                //確認計量值對象
                {
                    string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //能力值(介質值等：概念 武器/道具/物體)
                    //基礎值
                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        Answer_Return_Float += 
                            Object.Key_Point(QS_ReferenceTarget_String, QuickSave_Reference_StringArray[5], UserSource, TargetSource);
                    }
                    //效果加算
                    //Key中已計算
                    //係數計算
                    Answer_Return_Float *= Value;
                }
                break;
            #endregion

            #region - Stack -
            case "Stack":
                //符合標籤量
                {
                    //能力值(介質值等：概念 武器/道具/物體)
                    //Key
                    string QuickSave_Key_String = "";
                    for (int a = 6; a < QuickSave_Reference_StringArray.Length; a++)
                    {
                        QuickSave_Key_String += QuickSave_Reference_StringArray[a] + "_";
                    }
                    QuickSave_Key_String = QuickSave_Key_String.Remove(QuickSave_Key_String.Length - 1);
                    //基礎值
                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        Answer_Return_Float += Object.Key_Stack(
                            QuickSave_Reference_StringArray[5], QuickSave_Key_String,
                            UserSource, TargetSource, UsingObject);
                    }
                    //係數計算
                    Answer_Return_Float = (Answer_Return_Float * Value);
                }
                break;
            #endregion

            #region - Card -
            case "DelayBefore":
                //確認計量值對象
                {
                    //string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //能力值(介質值等：概念 武器/道具/物體)
                    //基礎值
                    Answer_Return_Float =
                        QuickSave_UsingCard_Script._Card_BehaviorUnit_Script.
                        Key_DelayBefore(TargetSource, UsingObject,
                        ContainEnchance: true, ContainTimeOffset: true);
                    //效果加算
                    //Key中已計算
                    //係數計算
                    Answer_Return_Float *= Value;
                }
                break;
            case "DelayAfter":
                //確認計量值對象
                {
                    //string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //能力值(介質值等：概念 武器/道具/物體)
                    //基礎值
                    Answer_Return_Float =
                        QuickSave_UsingCard_Script._Card_BehaviorUnit_Script.
                        Key_DelayAfter(TargetSource, UsingObject,
                        ContainEnchance: true, ContainTimeOffset: true);
                    //效果加算
                    //Key中已計算
                    //係數計算
                    Answer_Return_Float *= Value;
                }
                break;
            #endregion

            #region - Data -
            case "Data":
                //確認資料
                {
                    string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //資料(Combo等)
                    switch (QS_ReferenceTarget_String)
                    {
                        case "RandomNumber":
                            {
                                //係數計算
                                Answer_Return_Float *= Value;
                            }
                            break;
                        case "Combo":
                        case "React":
                            {
                                //基礎值
                                Answer_Return_Float =
                                    QuickSave_UserCreature_Script._Basic_Object_Script._Basic_SaveData_Class.ValueDataGet("Combo", 1);
                                //效果加算
                                List<string> QuickSave_Data_StringList =
                                    new List<string> { QS_ReferenceTarget_String };
                                Answer_Return_Float += float.Parse(
                                    QuickSave_UsingObject_Script.SituationCaller(
                                        "GetStatusValueAdd", QuickSave_Data_StringList,
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, 
                                        Time, Order)["ValueAdd"][0]);
                                Answer_Return_Float *= float.Parse(
                                    QuickSave_UsingObject_Script.SituationCaller(
                                        "GetStatusValueMultiply", QuickSave_Data_StringList,
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action,
                                        Time, Order)["ValueMultiply"][0]);
                                //係數計算
                                Answer_Return_Float *= Value;
                            }
                            break;
                        case "CardsCount":
                            {
                                //基礎值
                                switch (QuickSave_Reference_StringArray[5])
                                {
                                    case "Deck":
                                        {
                                            Answer_Return_Float = 
                                                QuickSave_UserCreature_Script._Card_CardsDeck_ScriptList.Count;
                                        }
                                        break;
                                    case "Board":
                                        {
                                            Answer_Return_Float =
                                                QuickSave_UserCreature_Script._Card_CardsBoard_ScriptList.Count;
                                        }
                                        break;
                                    case "Delay":
                                        {
                                            Answer_Return_Float =
                                                QuickSave_UserCreature_Script._Card_CardsDelay_ScriptList.Count;
                                        }
                                        break;
                                    case "Cemetery":
                                        {
                                            Answer_Return_Float =
                                                QuickSave_UserCreature_Script._Card_CardsCemetery_ScriptList.Count;
                                        }
                                        break;
                                    case "Exiled":
                                        {
                                            Answer_Return_Float =
                                                QuickSave_UserCreature_Script._Card_CardsExiled_ScriptList.Count;
                                        }
                                        break;
                                }
                                //效果加算
                                List<string> QuickSave_Data_StringList =
                                    new List<string> { QS_ReferenceTarget_String };
                                Answer_Return_Float += float.Parse(
                                    QuickSave_UsingObject_Script.SituationCaller(
                                        "GetStatusValueAdd", QuickSave_Data_StringList,
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action,
                                        Time, Order)["ValueAdd"][0]);
                                Answer_Return_Float *= float.Parse(
                                    QuickSave_UsingObject_Script.SituationCaller(
                                        "GetStatusValueMultiply", QuickSave_Data_StringList,
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action,
                                        Time, Order)["ValueMultiply"][0]);
                                //係數計算
                                Answer_Return_Float *= Value;
                            }
                            break;
                    }
                }
                break;
            #endregion

            default:
                Answer_Return_Float = Value;
                break;
        }
        #endregion
        //----------------------------------------------------------------------------------------------------

        //用途調整----------------------------------------------------------------------------------------------------
        #region - Type -
        switch (QuickSave_Type_StringArray[0])
        {
            #region - Value -
            case "Value":
                {
                    //無參考/上方已經設置了
                    //Answer_Return_Float = Value;
                }
                break;
            #endregion

            #region - AttackNumber -
            case "AttackNumber":
                {
                    float QuickSave_Value_Float = 0;
                    if (UsingObject != null)
                    {
                        switch (Situation)
                        {
                            case "Enchance"://附魔效果
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("EnchanceValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                            default://正常
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("AttackValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                        }
                    }
                    //基礎數值(已加算附魔)
                    Answer_Return_Float = Mathf.RoundToInt(
                        (Answer_Return_Float + QuickSave_Value_Float) +
                        ((QuickSave_EnchanceAdd_Float) * QuickSave_EnchanceMultiply_Float));

                    //數值計算
                    List<string> QuickSave_Data_StringList =
                        new List<string> ( QuickSave_Type_StringArray );
                    QuickSave_AdvanceAdd_Float += float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueAdd", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                                        HateTarget, Action,
                                        Time, Order)["ValueAdd"][0]);
                    QuickSave_AdvanceMultiply_Float *= float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueMultiply", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                                        HateTarget, Action,
                                        Time, Order)["ValueMultiply"][0]);
                    //總結
                    Answer_Return_Float =
                        Mathf.RoundToInt((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float);
                }
                break;
            #endregion

            #region - HealNumber -
            case "HealNumber":
                {
                    float QuickSave_Value_Float = 0;
                    if (UsingObject != null)
                    {
                        switch (Situation)
                        {
                            case "Enchance"://附魔效果
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("EnchanceValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                            default://正常
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("HealValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                        }
                    }
                    //基礎數值(已加算附魔)
                    Answer_Return_Float = Mathf.RoundToInt(
                        (Answer_Return_Float + QuickSave_Value_Float) +
                        ((QuickSave_EnchanceAdd_Float) * QuickSave_EnchanceMultiply_Float));

                    //數值計算
                    List<string> QuickSave_Data_StringList =
                        new List<string>(QuickSave_Type_StringArray);
                    QuickSave_AdvanceAdd_Float += float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueAdd", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueAdd"][0]);
                    QuickSave_AdvanceMultiply_Float *= float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueMultiply", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueMultiply"][0]);
                    //總結
                    Answer_Return_Float =
                        Mathf.RoundToInt((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float);
                }
                break;
            #endregion

            #region - ConstructNumber -
            case "ConstructNumber":
                {
                    float QuickSave_Value_Float = 0;
                    if (UsingObject != null)
                    {
                        switch (Situation)
                        {
                            case "Enchance"://附魔效果
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("EnchanceValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                            default://正常
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("ConstructValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                        }
                    }
                    //基礎數值(已加算附魔)
                    Answer_Return_Float = Mathf.RoundToInt(
                        (Answer_Return_Float + (QuickSave_Value_Float* Answer_Return_Float * 0.1f)) +
                        ((QuickSave_EnchanceAdd_Float) * QuickSave_EnchanceMultiply_Float));

                    //數值計算
                    List<string> QuickSave_Data_StringList =
                        new List<string>(QuickSave_Type_StringArray);
                    QuickSave_AdvanceAdd_Float += float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueAdd", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueAdd"][0]);
                    QuickSave_AdvanceMultiply_Float *= float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueMultiply", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueMultiply"][0]);
                    //總結
                    Answer_Return_Float =
                        Mathf.RoundToInt((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float);
                    
                }
                break;
            #endregion

            #region - Probability -
            case "Probability"://機率
                {
                    //數值計算
                    List<string> QuickSave_Data_StringList =
                        new List<string>(QuickSave_Type_StringArray);
                    QuickSave_AdvanceAdd_Float += float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueAdd", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueAdd"][0]);
                    QuickSave_AdvanceMultiply_Float *= float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueMultiply", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueMultiply"][0]);
                    //總結
                    Answer_Return_Float =
                        (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
                }
                break;
            #endregion

            #region - Percentage -
            case "Percentage"://百分比
                {
                    //數值計算
                    List<string> QuickSave_Data_StringList =
                        new List<string>(QuickSave_Type_StringArray);
                    QuickSave_AdvanceAdd_Float += float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueAdd", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueAdd"][0]);
                    QuickSave_AdvanceMultiply_Float *= float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueMultiply", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueMultiply"][0]);
                    //總結
                    Answer_Return_Float =
                        (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
                }
                break;
            #endregion

            #region - Once -
            case "AttackTimes"://主要攻擊數
            case "HealTimes"://主要攻擊數
            case "PursuitTimes"://追擊傷害數

            case "Pursuit"://追擊傷害
            case "Deal"://抽卡
            case "Throw"://丟卡

            case "Path"://範圍
            case "Shift"://位移

            case "Random"://隨機數

            case "MoveHeight"://移動高度(有製作但無用)
            case "ShiftHeight"://位移高度(有製作但無用)
            case "AttackHeight"://攻擊高度(有製作但無用)
            case "EffectHeight"://效果高度(有製作但無用)
                {
                    //數值計算
                    List<string> QuickSave_Data_StringList =
                        new List<string>(QuickSave_Type_StringArray);
                    QuickSave_AdvanceAdd_Float += float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueAdd", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueAdd"][0]);
                    QuickSave_AdvanceMultiply_Float *= float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueMultiply", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueMultiply"][0]);
                    //總結
                    Answer_Return_Float =
                        Mathf.RoundToInt((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float);
                }
                break;
                //消耗/移除(紀錄對象與數值)
            case "Consume"://使用消耗
            case "Remove"://狀態移除
                {
                    //數值計算
                    List<string> QuickSave_Data_StringList =
                        new List<string>(QuickSave_Type_StringArray);
                    QuickSave_AdvanceAdd_Float += float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueAdd", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueAdd"][0]);
                    QuickSave_AdvanceMultiply_Float *= float.Parse(
                        QuickSave_UsingObject_Script.SituationCaller(
                            "GetStatusValueMultiply", QuickSave_Data_StringList,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action,
                            Time, Order)["ValueMultiply"][0]);
                    //總結
                    Answer_Return_Float =
                        Mathf.RoundToInt((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float);
                }
                break;
        #endregion
    }
        #endregion
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (QuickSave_Type_StringArray[0])
        {
            case "AttackTimes"://主要攻擊數
            case "HealTimes"://主要攻擊數
            case "PursuitTimes"://追擊傷害數
                Answer_Return_Float = Mathf.Clamp(Answer_Return_Float, 1, 65535);
                break;
            default:
                {
                    Answer_Return_Float = Mathf.Clamp(Answer_Return_Float, 0, 65535);
                }
                break;
        }
        return Answer_Return_Float;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Direction -
    public sbyte Direction(int x,int y)
    {
        if (y == 1)
        {
            if (x == 1)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        else if(y == -1)
        {
            if (x == 1)
            {
                return 4;
            }
            else
            {
                return 3;
            }
        }
        return 0;
    }
    #endregion

    #region - Random -
    //——————————————————————————————————————————————————————————————————————
    //骰子隨機----------------------------------------------------------------------------------------------------
    public float DiceRandom(int D, float RandomMin, float RandomMax)
    {
        float Answer_DiceValue_Float = 0;
        for (int a = 0; a < D; a++)
        {
            Answer_DiceValue_Float += UnityEngine.Random.Range(RandomMin, RandomMax);
        }
        return Answer_DiceValue_Float;
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - SituationCallerMath -
    //——————————————————————————————————————————————————————————————————————
    public List<string> SituationCaller_TransToStringList(Dictionary<string,List<string>> TransTarget)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();

        foreach (string Key in TransTarget.Keys)
        {
            foreach (string Value in TransTarget[Key])
            {
                Answer_Return_StringList.Add(Key + "｜" + Value);
            }
        }
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion Customary variables
}

#region PublicClass
#region - Variaty -
[System.Serializable]
public class Tuple
{
    public float Min;
    public float Max;

    public void InputSet(string[] Input)
    {
        Min = float.Parse(Input[0]);
        Max = float.Parse(Input[1]);
    }

    public bool Between(float Value)
    {
        if (Min < Value && Value <= Max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

[System.Serializable]
public class Vector
{
    public float x;
    public float y;
    public float z;

    #region - GetInt -
    public int X
    {
        get
        {
            return Mathf.RoundToInt(x);
        }
    }
    public int Y
    {
        get
        {
            return Mathf.RoundToInt(y);
        }
    }
    public int Z
    {
        get
        {
            return Mathf.RoundToInt(z);
        }
    }
    #endregion

    #region - SetValue -
    public Vector()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
    }
    public Vector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public Vector(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public Vector(Vector obj)
    {
        this.x = obj.x;
        this.y = obj.y;
    }
    public Vector(Vector2 obj)
    {
        this.x = obj.x;
        this.y = obj.y;
    }
    public Vector(Vector2Int obj)
    {
        this.x = obj.x;
        this.y = obj.y;
    }
    public Vector(Vector3 obj)
    {
        this.x = obj.x;
        this.y = obj.y;
        this.z = obj.z;
    }
    public Vector(Vector3Int obj)
    {
        this.x = obj.x;
        this.y = obj.y;
        this.z = obj.z;
    }
    #endregion

    #region - GetValue -
    public Vector2 Vector2
    {
        get
        {
            return new Vector2(x, y);
        }
    }
    public Vector2Int Vector2Int
    {
        get
        {
            return new Vector2Int(X, Y);
        }
    }
    public Vector3 Vector3
    {
        get
        {
            return new Vector3(x, y, z);
        }
    }
    public Vector3Int Vector3Int
    {
        get
        {
            return new Vector3Int(X, Y, Z);
        }
    }
    #endregion

    #region - Compare -
    //相當於InstanceID
    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 31 + x.GetHashCode();
        hash = hash * 31 + y.GetHashCode();
        hash = hash * 31 + z.GetHashCode();
        return hash;
    }
    public override bool Equals(object obj)
    {
        if (obj is Vector)
        {
            Vector Turnobj = (Vector)obj;
            return x == Turnobj.x && y == Turnobj.y && z == Turnobj.z;
        }
        else if(obj is Vector2)
        {
            Vector2 Turnobj = (Vector2)obj;
            return x == Turnobj.x && y == Turnobj.y;
        }
        else if (obj is Vector2Int)
        {
            Vector2Int Turnobj = (Vector2Int)obj;
            return x == Turnobj.x && y == Turnobj.y;
        }
        else if (obj is Vector3)
        {
            Vector3 Turnobj = (Vector3)obj;
            return x == Turnobj.x && y == Turnobj.y && z == Turnobj.z;
        }
        else if (obj is Vector3Int)
        {
            Vector3 Turnobj = (Vector3)obj;
            return x == Turnobj.x && y == Turnobj.y && z == Turnobj.z;
        }
        return false;
    }
    // 重載 "==" 運算子
    public static bool operator ==(Vector v1, object v2)
    {
        if (ReferenceEquals(v1, null))
        {
            return ReferenceEquals(v2, null);
        }
        return v1.Equals(v2);
    }
    public static bool operator != (Vector v1, object v2)
    {
        if (ReferenceEquals(v1, null))
        {
            return !ReferenceEquals(v2, null);
        }
        return !v1.Equals(v2);
    }
    #endregion

    #region - Function -
    public float Distance(Vector Target)
    {
        float Dx = Mathf.Abs((x - y) - (Target.x - Target.y));//2/0
        float Dy = Mathf.Abs((x + y) - (Target.x + Target.y));//-2/-2
        return (Dx + Dy) * 0.5f;//0/1
    }
    #endregion
}

public class PathPreviewClass
{
    public string Key;
    public _Map_BattleObjectUnit UseObject;//使用
    public Vector FinalCoor;//最終位置(已被計算穿越與停留後的位置)
    public DirectionPathClass FinalPath;//最終路徑(已被計算穿越與停留後的位置)
    public List<string> ScoreList = new List<string>();//分數表(用於AI判定)
    public List<_Map_BattleObjectUnit> PassObjects = new List<_Map_BattleObjectUnit>();
    public _Map_BattleObjectUnit HitObject;//命中目標(不包含在穿透中
}
#endregion

#region - Battle -
//----------------------------------------------------------------------------------------------------
//類別-時間單位
[System.Serializable]
public class RoundSequenceUnitClass
{
    //時間位置
    public int Time;//總Sequence時用
    //時間位置
    public string Type;//Priority時用
    //持有者
    public _Map_BattleObjectUnit Owner;
    //單位表
    public List<RoundElementClass> RoundUnit = new List<RoundElementClass>();

}
//類別-單位
[System.Serializable]
public class RoundElementClass
{
    //類型
    public SourceClass Source = new SourceClass();
    //延遲類型
    public string DelayType;
    //延遲時間(本次延遲增加的時間)
    public int DelayTime;
    //延遲偏移
    public int DelayOffset;
    //累積時間(從以前到現在的時間)、同為上次行動時間
    public int AccumulatedTime;
    //總時間  目標時間(因被佔用而延後) 
    public int TargetTime;
}

//----------------------------------------------------------------------------------------------------

public class DamageClass
{
    //類型
    public SourceClass Source = new SourceClass();
    public string DamageType;//傷害類型/Slash,Puncture,Impact,Energy,Chaos,Abstract,Stark
    public float Damage;//傷害值
    public int Times;//傷害次數
}

[System.Serializable]
[HideInInspector]
public class DataClass
{
    public Dictionary<string, float> Numbers = new Dictionary<string, float>();
    public Dictionary<string, string> Keys = new Dictionary<string, string>();
}

public class SourceClass
{
    public string SourceType;
    //來源類型/Creature、Behavior(Behavior、Enchance)、Weapon、EffectObject、EffectObject、System
    //特殊類型/Event

    public _UI_EventManager.EventDataClass Source_Event = null;

    public _Object_CreatureUnit Source_Creature = null;

    public Vector Source_Coordinate = null;//System

    public _Map_FieldObjectUnit Source_FieldObject = null;
    public _Map_BattleObjectUnit Source_BattleObject = null;
    public _Map_FieldGroundUnit Source_FieldGround = null;
    public _Map_BattleGroundUnit Source_BattleGround = null;

    public _UI_Card_Unit Source_Card = null;

    public _Effect_EffectCardUnit Source_EffectCard = null;
    public _Effect_EffectObjectUnit Source_EffectObject = null;

    public _Item_WeaponUnit Source_Weapon = null;
    public _Item_ItemUnit Source_Item = null;
    public _Item_ConceptUnit Source_Concept = null;
    public _Item_MaterialUnit Source_Material = null;
    public _Item_Manager.MaterialDataClass Source_MaterialData = null;

    public _Item_SyndromeUnit Source_Syndrome = null;

    public Dictionary<string, float> Source_NumbersData = null;
    public Dictionary<string, string> Source_KeysData = null;
}
#endregion

#region - Map -
//範圍類別
[System.Serializable]
[HideInInspector]
public class BoolRangeClass
{
    int CenterX;
    int CenterY;

    public Vector2Int Center
    {
        get
        {
            return new Vector2Int(CenterX, CenterY);
        }
        set
        {
            CenterX = value.x;
            CenterY = value.y;
        }
    }
    public bool[,] Coordinate;
}

//面相範圍類別/用於指示技能方向
[System.Serializable]
public class DirectionRangeClass
{
    //第三象限(-1,-1)/第二象限(+1,-1)
    //第四象限(-1,+1)/第一象限(+1,+1)
    public sbyte DirectionX;
    public sbyte DirectionY;
    public Vector2Int Direction
    {
        get
        {
            return new Vector2Int(DirectionX, DirectionY);
        }
        set
        {
            DirectionX = (sbyte)value.x;
            DirectionY = (sbyte)value.y;
        }
    }
    public BoolRangeClass Range;
}

//面相範圍類別/用於指示技能方向
[System.Serializable]
public class DirectionPathClass
{
    //第三象限(-1,-1)/第二象限(+1,-1)
    //第四象限(-1,+1)/第一象限(+1,+1)
    public List<sbyte> Direction = new List<sbyte>();
    public List<Vector> Path = new List<Vector>();
}
public class PathSelectPairClass
{
    public List<PathUnitClass> Path = new List<PathUnitClass>();
    public List<SelectUnitClass> Select = new List<SelectUnitClass>();
}
public class PathUnitClass
{
    public string Key;//標籤
    public Vector Vector;//座標
    public int Direction;//方向
    public DirectionPathClass Path;//可能有複數條(滾輪選擇)
}
public class PathCollectClass
{
    public List<PathUnitClass> Data = new List<PathUnitClass>();
    #region - AllVectors -
    public List<Vector> AllVectors(List<PathUnitClass> Input)
    {
        HashSet<Vector> QuickSave_Vector_HashSetList = new HashSet<Vector>();
        List<PathUnitClass> QuickSave_Data_ClassList = Data;
        if (Input != null)
        {
            QuickSave_Data_ClassList = Input;
        }
        foreach (PathUnitClass Data in QuickSave_Data_ClassList)
        {
            QuickSave_Vector_HashSetList.UnionWith(Data.Path.Path);
        }
        return new List<Vector>(QuickSave_Vector_HashSetList);
    }
    #endregion
    #region - PathUnits -
    public List<PathUnitClass> PathUnits(string Key)
    {
        List<PathUnitClass> QuickSave_Data_ClassList = new List<PathUnitClass>();
        foreach (PathUnitClass DataUnit in Data)
        {
            if (DataUnit.Key == Key)
            {
                QuickSave_Data_ClassList.Add(DataUnit);
            }
        }
        return QuickSave_Data_ClassList;
    }
    public List<PathUnitClass> PathUnits(Vector Vector, int Direction)
    {
        List<PathUnitClass> QuickSave_Data_ClassList = new List<PathUnitClass>();
        foreach (PathUnitClass DataUnit in Data)
        {
            if (DataUnit.Vector == Vector && DataUnit.Direction == Direction)
            {
                QuickSave_Data_ClassList.Add(DataUnit);
            }
        }
        return QuickSave_Data_ClassList;
    }
    public List<PathUnitClass> PathUnits(string Key, Vector Vector)
    {
        List<PathUnitClass> QuickSave_Data_ClassList = new List<PathUnitClass>();
        foreach (PathUnitClass DataUnit in Data)
        {
            if (DataUnit.Key == Key && DataUnit.Vector == Vector)
            {
                QuickSave_Data_ClassList.Add(DataUnit);
            }
        }
        return QuickSave_Data_ClassList;
    }
    public List<PathUnitClass> PathUnits(string Key, Vector Vector, int Direction)
    {
        List<PathUnitClass> QuickSave_Data_ClassList = new List<PathUnitClass>();
        foreach (PathUnitClass DataUnit in Data)
        {
            if (DataUnit.Key == Key && DataUnit.Vector == Vector && DataUnit.Direction == Direction)
            {
                QuickSave_Data_ClassList.Add(DataUnit);
            }
        }
        return QuickSave_Data_ClassList;
    }
    #endregion
    #region - Path -
    public List<DirectionPathClass> Path(string Key)
    {
        List<DirectionPathClass> QuickSave_Data_ClassList = new List<DirectionPathClass>();
        foreach (PathUnitClass DataUnit in Data)
        {
            if (DataUnit.Key == Key)
            {
                QuickSave_Data_ClassList.Add(DataUnit.Path);
            }
        }
        return QuickSave_Data_ClassList;
    }
    public List<DirectionPathClass> Path(Vector Vector, int Direction)
    {
        List<DirectionPathClass> QuickSave_Data_ClassList = new List<DirectionPathClass>();
        foreach (PathUnitClass DataUnit in Data)
        {
            if (DataUnit.Vector == Vector && DataUnit.Direction == Direction)
            {
                QuickSave_Data_ClassList.Add(DataUnit.Path);
            }
        }
        return QuickSave_Data_ClassList;
    }
    public List<DirectionPathClass> Path(string Key, Vector Vector, int Direction)
    {
        List<DirectionPathClass> QuickSave_Data_ClassList = new List<DirectionPathClass>();
        foreach (PathUnitClass DataUnit in Data)
        {
            if (DataUnit.Key == Key && DataUnit.Vector == Vector && DataUnit.Direction == Direction)
            {
                QuickSave_Data_ClassList.Add(DataUnit.Path);
            }
        }
        return QuickSave_Data_ClassList;
    }
    #endregion
}

public class SelectUnitClass
{
    public string Key;//標籤
    public Vector Vector;//座標
    public int Direction;//方向
    public DirectionRangeClass Select;//可能有複數條(滾輪選擇)

    public List<Vector> AllVectors()
    {
        HashSet<Vector> QuickSave_Vector_HashSetList = new HashSet<Vector>();
        QuickSave_Vector_HashSetList.UnionWith(
            _World_Manager._Map_Manager.Range_ClassToVector(Vector, Select));
        return new List<Vector>(QuickSave_Vector_HashSetList);
    }
}
public class SelectCollectClass
{
    public List<SelectUnitClass> Data = new List<SelectUnitClass>();
    #region - AllVectors -
    public List<Vector> Vector()
    {
        HashSet<Vector> QuickSave_Vector_HashSetList = new HashSet<Vector>();
        List<SelectUnitClass> QuickSave_Data_ClassList = Data;
        foreach (SelectUnitClass Data in QuickSave_Data_ClassList)
        {
            QuickSave_Vector_HashSetList.Add(Data.Vector);
        }
        return new List<Vector>(QuickSave_Vector_HashSetList);
    }
    public List<Vector> AllVectors(List<SelectUnitClass> Input)
    {
        HashSet<Vector> QuickSave_Vector_HashSetList = new HashSet<Vector>();
        List<SelectUnitClass> QuickSave_Data_ClassList = Data;
        if (Input != null)
        {
            QuickSave_Data_ClassList = Input;
        }
        foreach (SelectUnitClass Data in QuickSave_Data_ClassList)
        {
            QuickSave_Vector_HashSetList.UnionWith(Data.AllVectors());
        }
        return new List<Vector>(QuickSave_Vector_HashSetList);
    }
    #endregion
    #region - SelectUnits -
    public List<SelectUnitClass> SelectUnits(string Key)
    {
        List<SelectUnitClass> QuickSave_Data_ClassList = new List<SelectUnitClass>();
        foreach (SelectUnitClass DataUnit in Data)
        {
            if (DataUnit.Key == Key)
            {
                QuickSave_Data_ClassList.Add(DataUnit);
            }
        }
        return QuickSave_Data_ClassList;
    }
    public List<SelectUnitClass> SelectUnits(Vector Vector, int Direction)
    {
        List<SelectUnitClass> QuickSave_Data_ClassList = new List<SelectUnitClass>();
        foreach (SelectUnitClass DataUnit in Data)
        {
            if (DataUnit.Vector == Vector && DataUnit.Direction == Direction)
            {
                QuickSave_Data_ClassList.Add(DataUnit);
            }
        }
        return QuickSave_Data_ClassList;
    }
    public List<SelectUnitClass> SelectUnits(string Key, Vector Vector)
    {
        List<SelectUnitClass> QuickSave_Data_ClassList = new List<SelectUnitClass>();
        foreach (SelectUnitClass DataUnit in Data)
        {
            if (DataUnit.Key == Key && DataUnit.Vector == Vector)
            {
                QuickSave_Data_ClassList.Add(DataUnit);
            }
        }
        return QuickSave_Data_ClassList;
    }
    public List<SelectUnitClass> SelectUnits(string Key, Vector Vector, int Direction)
    {
        List<SelectUnitClass> QuickSave_Data_ClassList = new List<SelectUnitClass>();
        foreach (SelectUnitClass DataUnit in Data)
        {
            if (DataUnit.Key == Key && DataUnit.Vector == Vector && DataUnit.Direction == Direction)
            {
                QuickSave_Data_ClassList.Add(DataUnit);
            }
        }
        return QuickSave_Data_ClassList;
    }
    #endregion
    #region - Select -
    public List<DirectionRangeClass> Select(string Key)
    {
        List<DirectionRangeClass> QuickSave_Data_ClassList = new List<DirectionRangeClass>();
        foreach (SelectUnitClass DataUnit in Data)
        {
            if (DataUnit.Key == Key)
            {
                QuickSave_Data_ClassList.Add(DataUnit.Select);
            }
        }
        return QuickSave_Data_ClassList;
    }
    public List<DirectionRangeClass> Select(Vector Vector, int Direction)
    {
        List<DirectionRangeClass> QuickSave_Data_ClassList = new List<DirectionRangeClass>();
        foreach (SelectUnitClass DataUnit in Data)
        {
            if (DataUnit.Vector == Vector && DataUnit.Direction == Direction)
            {
                QuickSave_Data_ClassList.Add(DataUnit.Select);
            }
        }
        return QuickSave_Data_ClassList;
    }
    public List<DirectionRangeClass> Select(string Key, Vector Vector, int Direction)
    {
        List<DirectionRangeClass> QuickSave_Data_ClassList = new List<DirectionRangeClass>();
        foreach (SelectUnitClass DataUnit in Data)
        {
            if (DataUnit.Key == Key && DataUnit.Vector == Vector && DataUnit.Direction == Direction)
            {
                QuickSave_Data_ClassList.Add(DataUnit.Select);
            }
        }
        return QuickSave_Data_ClassList;
    }
    #endregion
}
#endregion

#region - Data -
//語言類別
public class LanguageClass
{
    public string Name;
    public string Summary;
    public string Description;
    public string Select = null;//只有在特殊情況才會有(Event)
}

[System.Serializable]
[HideInInspector]
public class NumbericalStsringClass
{
    //基礎職
    public List<string> Default = new List<string>();
    //額外增加目錄
    public List<string> AddKeys = new List<string>();//新增順序
    public Dictionary<string, List<string>> ExtraDictionary = new Dictionary<string, List<string>>();

    //額外數值取得/數字取代
    public void ExtraSet(string Key, List<string> Value)
    {
        if (ExtraDictionary.TryGetValue(Key, out List<string> DicValue))
        {
            if (Value == null)
            {
                AddKeys.Remove(Key);
                ExtraDictionary.Remove(Key);
                return;
            }
            ExtraDictionary[Key] = Value;
        }
        else
        {
            if (Value != null)
            {
                AddKeys.Add(Key);
                ExtraDictionary.Add(Key, Value);
            }
        }
    }
    //重製(針對或全體)
    public void ExtraClear(string Type)
    {
        switch (Type)
        {
            case "all":
                AddKeys.Clear();
                ExtraDictionary.Clear();
                break;
            default:
                //包含指定
                foreach (string Key in ExtraDictionary.Keys)
                {
                    if (Key.Contains(Type))
                    {
                        ExtraSet(Key, null);
                    }
                }
                break;
        }
    }

    //總和
    public List<string> Total()
    {
        HashSet<string> AnswerString = new HashSet<string>(Default);
        for (int a = 0; a < AddKeys.Count; a++)
        {
            List<string> DicKeys = ExtraDictionary[AddKeys[a]];
            foreach (string Key in DicKeys)
            {
                #region TagCheck
                switch (Key)
                {
                    #region - Attack -
                    case "LightAttack":
                    case "HeavyAttack":
                    case "FinishAttack":
                        {
                            AnswerString.Remove("LightAttack");
                            AnswerString.Remove("HeavyAttack");
                            AnswerString.Remove("FinishAttack");
                        }
                        break;
                    #endregion

                    #region - Move -
                    case "ProgressiveMove":
                    case "UniqueMove":
                        {
                            AnswerString.Remove("ProgressiveMove");
                            AnswerString.Remove("UniqueMove");
                        }
                        break;
                    #endregion

                    #region - Push/Pull -
                    case "PushDisplace":
                    case "PullDisplace":
                        {
                            AnswerString.Remove("PushDisplace");
                            AnswerString.Remove("PullDisplace");
                        }
                        break;
                    #endregion

                    #region - Range -
                    case "DirectionRange":
                    case "OverallRange":
                        {
                            AnswerString.Remove("DirectionRange");
                            AnswerString.Remove("OverallRange");
                        }
                        break;
                    #endregion

                    #region - Damage -
                    case "SlashDamage":
                    case "PunctureDamage":
                    case "ImpactDamage":
                    case "EnergyDamage":
                    case "ChaosDamage":
                    case "AbstractDamage":
                    case "StarkDamage":
                        {
                            AnswerString.Remove("SlashDamage");
                            AnswerString.Remove("PunctureDamage");
                            AnswerString.Remove("ImpactDamage");
                            AnswerString.Remove("EnergyDamage");
                            AnswerString.Remove("ChaosDamage");
                            AnswerString.Remove("AbstractDamage");
                            AnswerString.Remove("StarkDamage");
                        }
                        break;
                    #endregion

                    #region - Action -
                    case "Soar":
                    case "Drop":
                        {
                            AnswerString.Remove("Soar");
                            AnswerString.Remove("Drop");
                        }
                        break;
                    #endregion

                    #region - Speed -
                    case "Preparatory":
                    case "Burst":
                    case "Instantaneous":
                        {
                            AnswerString.Remove("Preparatory");
                            AnswerString.Remove("Burst");
                            AnswerString.Remove("Instantaneous");
                        }
                        break;
                    #endregion

                    #region - 流勢 -
                    case "TrendForward":
                    case "TrendOpposite":
                        {
                            AnswerString.Remove("TrendForward");
                            AnswerString.Remove("TrendOpposite");
                        }
                        break;
                        #endregion
                }
                #endregion
                AnswerString.Add(Key);
            }
        }
        return new List<string>(AnswerString);
    }

    //標籤確認
    public bool TagContains(List<string> Keys,bool NeedAllMatch)
    {
        bool OneMatch = false;
        bool AllMatch = true;
        List<string> QuickSave_Total_Class = Total();
        foreach (string CheckKey in Keys)
        {
            switch (CheckKey)
            {
                case "All":
                    return true;
                default:
                    if (CheckKey.Contains("Select"))
                    {
                        List<string> TagsSplit =
                            new List<string>(CheckKey.Substring(6, CheckKey.Length - 7).
                            Split(":"[0]));
                        bool TagHave = false;
                        for (int t = 0; t < TagsSplit.Count; t++)
                        {
                            if (QuickSave_Total_Class.Contains(TagsSplit[t]))
                            {
                                TagHave = true;
                                break;
                            }
                        }
                        if (!TagHave)
                        {
                            AllMatch = false;
                        }
                        else
                        {
                            OneMatch = true;
                        }
                    }
                    else if (CheckKey.Contains("Un"))
                    {
                        List<string> TagsSplit =
                            new List<string>(CheckKey.Substring(2, CheckKey.Length - 3).
                            Split(":"[0]));
                        bool TagHave = false;
                        for (int t = 0; t < TagsSplit.Count; t++)
                        {
                            if (QuickSave_Total_Class.Contains(TagsSplit[t]))
                            {
                                TagHave = true;
                                break;
                            }
                        }
                        if (TagHave)
                        {
                            AllMatch = false;
                        }
                        else
                        {
                            OneMatch = true;
                        }
                    }
                    else if (!QuickSave_Total_Class.Contains(CheckKey))
                    {
                        AllMatch = false;
                    }
                    else
                    {
                        OneMatch = true;
                    }
                    break;
            }
        }
        if (NeedAllMatch)
        {
            return AllMatch;
        }
        else
        {
            return OneMatch;
        }
    }
}

//數值類別
[System.Serializable]
[HideInInspector]
public class NumbericalValueClass
{
    //數值
    public float Point;

    //基礎職
    public float Default;
    //額外值
    public float Extra = 0;
    //額外增加目錄
    public Dictionary<string, float> ExtraDictionary = new Dictionary<string, float>();

    //額外數值取得/數字取代
    public void ExtraSet(string Key, float Value)
    {
        if (ExtraDictionary.TryGetValue(Key, out float DicValue))
        {
            Extra += Value - DicValue;
            ExtraDictionary[Key] = Value;
        }
        else
        {
            Extra += Value;
            ExtraDictionary.Add(Key, Value);
        }
        if (ExtraDictionary[Key] == 0)
        {
            ExtraDictionary.Remove(Key);
        }
    }
    //重製(針對或全體)
    public void ExtraClear(string Type)
    {
        switch (Type)
        {
            case "all":
                ExtraDictionary.Clear();
                Extra = 0;
                break;
            default:
                foreach(string Key in ExtraDictionary.Keys)
                {
                    if (Key.Contains(Type))
                    {
                        ExtraSet(Key, 0);
                    }
                }
                break;
        }
    }

    //總和
    public float Total()
    {
        return Default + Extra;
    }
    public float Gap()
    {
        return Total() - Point;
    }
}

//深層複製
public static class Extensions
{
    public static T DeepClone<T>(this T item)
    {
        if (item != null)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, item);
                stream.Seek(0, SeekOrigin.Begin);
                var result = (T)formatter.Deserialize(stream);
                return result;
            }
        }

        return default(T);
    }
}
#endregion

#region - Compare -
public class SourceTypeComparer : IComparer<_Map_BattleObjectUnit>
{
    private string sortPriority;

    public SourceTypeComparer(string priority)
    {
        this.sortPriority = priority;
    }

    public int Compare(_Map_BattleObjectUnit x, _Map_BattleObjectUnit y)
    {
        int xScore = TypeScore(x._Basic_Source_Class.SourceType);
        int yScore = TypeScore(y._Basic_Source_Class.SourceType);

        return xScore.CompareTo(yScore);
    }

    int TypeScore(string input)
    {
        int baseScore;
        switch (input)
        {
            case "Creature":
                baseScore = 5;
                break;
            case "Concept":
                baseScore = 65535;
                break;
            case "Weapon":
                baseScore = 3;
                break;
            case "Item":
                baseScore = 2;
                break;
            case "Object":
                baseScore = 1;
                break;
            default:
                baseScore = 0;
                break;
        }

        switch (sortPriority)
        {
            case "Reverse":
                baseScore *= -1;
                break;
        }

        return baseScore;
    }
}
#endregion
#region - TimesLimit -
public class TimesLimitClass
{
    //只執行X次——————————————————————————————————————————————————————————————————————
    private Dictionary<string, Dictionary<string, int>> ActionTimes = 
        new Dictionary<string, Dictionary<string, int>>()//類型、編號、次數統計
    {
        {"Round",new Dictionary<string, int>(){ {"Default",0 } } }/*回合*/,
        {"Standby",new Dictionary<string, int>(){ {"Default",0 } } }/*回合待命*/,
        {"Times",new Dictionary<string, int>(){ {"Default",0 } } }/*每次生命週期(卡牌:抽卡重製、效果:消除時重製)*/
    };
    private Dictionary<string, Dictionary<string, int>> ActionTimesSave = 
        new Dictionary<string, Dictionary<string, int>>()//次數暫存
    {
        {"Round",new Dictionary<string, int>(){ {"Default",0 } } }/*回合*/,
        {"Standby",new Dictionary<string, int>(){ {"Default",0 } } }/*回合待命*/,
        {"Times",new Dictionary<string, int>(){ {"Default",0 } } }/*每次生命週期(卡牌:抽卡重製、效果:消除時重製)*/
    };
    public void TimesLimit_Reset(string Type, string Key = "Default")
    {
        if (ActionTimes[Type].ContainsKey(Key))
        {
            ActionTimes[Type][Key] = 0;
        }
        else
        {
            ActionTimes[Type]["Default"] = 0;
        }
    }
    public void TimesLimit_Save()
    {
        foreach (string Type in ActionTimes.Keys)
        {
            foreach (string Key in ActionTimes[Type].Keys)
            {
                if (ActionTimesSave[Type].ContainsKey(Key))
                {
                    ActionTimesSave[Type][Key] = ActionTimes[Type][Key];
                }
                else
                {
                    ActionTimesSave[Type].Add(Key, ActionTimes[Type][Key]);
                }
            }
        }
    }
    public void TimesLimit_Load()
    {
        foreach (string Type in ActionTimesSave.Keys)
        {
            foreach (string Key in ActionTimesSave[Type].Keys)
            {
                if (ActionTimes[Type].ContainsKey(Key))
                {
                    ActionTimes[Type][Key] = ActionTimesSave[Type][Key];
                }
                else
                {
                    ActionTimes[Type].Add(Key, ActionTimesSave[Type][Key]);
                }
            }
        }
    }
    public bool TimesLimit(string Type, int Times, string Key = "Default")
    {
        //判定----------------------------------------------------------------------------------------------------
        if (ActionTimes[Type].ContainsKey(Key))
        {
            if (ActionTimes[Type][Key] < Times)
            {
                ActionTimes[Type][Key]++;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            ActionTimes[Type].Add(Key, 1);
            return true;
        }
        //----------------------------------------------------------------------------------------------------
    }
    public Dictionary<string, Dictionary<string, int>> TimesLimit_Check()
    {
        return ActionTimes;
    }
    //——————————————————————————————————————————————————————————————————————
}
#endregion

#region - SaveData -
public class SaveDataClass
{
    #region - DataDictionary -
    private Dictionary<string, bool> _Basic_BoolData_Dictionary = new Dictionary<string, bool>();
    private Dictionary<string, float> _Basic_ValueData_Dictionary = new Dictionary<string, float>();
    private Dictionary<string, List<float>> _Basic_ValueListData_Dictionary = new Dictionary<string, List<float>>();
    private Dictionary<string, string> _Basic_StringData_Dictionary = new Dictionary<string, string>();
    private Dictionary<string, _Map_BattleObjectUnit> _Basic_ObjectData_Dictionary = new Dictionary<string, _Map_BattleObjectUnit>();
    private Dictionary<string, List<_Map_BattleObjectUnit>> _Basic_ObjectListData_Dictionary = new Dictionary<string, List<_Map_BattleObjectUnit>>();
    private Dictionary<string, List<SourceClass>> _Basic_SourceListData_Dictionary = new Dictionary<string, List<SourceClass>>();
    #endregion
    #region - Check -
    public bool ValueDataCheck(string Key)
    {
        //----------------------------------------------------------------------------------------------------
        return _Basic_ValueData_Dictionary.ContainsKey(Key);
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Set -
    public void BoolDataSet(string Key, bool Value)
    {
        //----------------------------------------------------------------------------------------------------
        if (Value == false)
        {
            _Basic_BoolData_Dictionary.Remove(Key);
            return;
        }

        if (_Basic_BoolData_Dictionary.TryGetValue(Key, out bool DicValue))
        {
            _Basic_BoolData_Dictionary[Key] = Value;
        }
        else
        {
            _Basic_BoolData_Dictionary.Add(Key, Value);
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void ValueDataSet(string Key, float Value, bool Operation = false)
    {
        //----------------------------------------------------------------------------------------------------
        if (Value == 0)
        {
            _Basic_ValueData_Dictionary.Remove(Key);
            return;
        }

        if (_Basic_ValueData_Dictionary.TryGetValue(Key, out float DicValue))
        {
            if (Operation)
            {
                _Basic_ValueData_Dictionary[Key] += Value;
            }
            else
            {
                _Basic_ValueData_Dictionary[Key] = Value;
            }
        }
        else
        {
            _Basic_ValueData_Dictionary.Add(Key, Value);
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void ValueListDataAdd(string Key, float Value)
    {
        //----------------------------------------------------------------------------------------------------
        if (_Basic_ValueListData_Dictionary.TryGetValue(Key, out List<float> DicValue))
        {
            _Basic_ValueListData_Dictionary[Key].Add(Value);
        }
        else
        {
            _Basic_ValueListData_Dictionary.Add(Key, new List<float> { Value });
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void ValueListDataSet(string Key, List<float> Value)
    {
        //----------------------------------------------------------------------------------------------------
        if (Value == null)
        {
            _Basic_ValueListData_Dictionary.Remove(Key);
            return;
        }

        if (_Basic_ValueListData_Dictionary.TryGetValue(Key, out List<float> DicValue))
        {
            _Basic_ValueListData_Dictionary[Key] = Value;
        }
        else
        {
            _Basic_ValueListData_Dictionary.Add(Key, Value);
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void StringDataSet(string Key, string Value)
    {
        //----------------------------------------------------------------------------------------------------
        if (Value == null)
        {
            _Basic_StringData_Dictionary.Remove(Key);
            return;
        }

        if (_Basic_StringData_Dictionary.TryGetValue(Key, out string DicValue))
        {
            _Basic_StringData_Dictionary[Key] = Value;
        }
        else
        {
            _Basic_StringData_Dictionary.Add(Key, Value);
        }
        //----------------------------------------------------------------------------------------------------
    }



    public void ObjectDataSet(string Key, _Map_BattleObjectUnit Value)
    {
        //----------------------------------------------------------------------------------------------------
        if (Value == null)
        {
            _Basic_ObjectData_Dictionary.Remove(Key);
            return;
        }

        if (_Basic_ObjectData_Dictionary.TryGetValue(Key, out _Map_BattleObjectUnit DicValue))
        {
            _Basic_ObjectData_Dictionary[Key] = Value;
        }
        else
        {
            _Basic_ObjectData_Dictionary.Add(Key, Value);
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void ObjectListDataAdd(string Key, _Map_BattleObjectUnit Value)
    {
        //----------------------------------------------------------------------------------------------------
        if (_Basic_ObjectListData_Dictionary.TryGetValue(Key, out List<_Map_BattleObjectUnit> DicValue))
        {
            _Basic_ObjectListData_Dictionary[Key].Add(Value);
        }
        else
        {
            _Basic_ObjectListData_Dictionary.Add(Key, new List<_Map_BattleObjectUnit> { Value });
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void ObjectListDataRemove(string Key, _Map_BattleObjectUnit Value)
    {
        //----------------------------------------------------------------------------------------------------
        if (_Basic_ObjectListData_Dictionary.TryGetValue(Key, out List<_Map_BattleObjectUnit> DicValue))
        {
            _Basic_ObjectListData_Dictionary[Key].Remove(Value);
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void ObjectListDataSet(string Key, List<_Map_BattleObjectUnit> Value)
    {
        //----------------------------------------------------------------------------------------------------
        if (Value == null)
        {
            _Basic_ObjectListData_Dictionary.Remove(Key);
            return;
        }

        if (_Basic_ObjectListData_Dictionary.TryGetValue(Key, out List<_Map_BattleObjectUnit> DicValue))
        {
            _Basic_ObjectListData_Dictionary[Key] = Value;
        }
        else
        {
            _Basic_ObjectListData_Dictionary.Add(Key, Value);
        }
        //----------------------------------------------------------------------------------------------------
    }


    public List<SourceClass> SourceListDataGet(string Key)
    {
        //----------------------------------------------------------------------------------------------------
        return _Basic_SourceListData_Dictionary.TryGetValue(Key, out List<SourceClass> Value) ? Value : new List<SourceClass>();
        //----------------------------------------------------------------------------------------------------
    }
    public bool SourceListDataContain(string Key, SourceClass Target)
    {
        //----------------------------------------------------------------------------------------------------
        if (_Basic_SourceListData_Dictionary.TryGetValue(Key, out List<SourceClass> Value))
        {
            return Value.Contains(Target);
        }
        else
        {
            return false;
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void SourceListDataAdd(string Key, SourceClass Value)
    {
        //----------------------------------------------------------------------------------------------------
        if (_Basic_SourceListData_Dictionary.TryGetValue(Key, out List<SourceClass> DicValue))
        {
            _Basic_SourceListData_Dictionary[Key].Add(Value);
        }
        else
        {
            _Basic_SourceListData_Dictionary.Add(Key, new List<SourceClass> { Value });
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void SourceListDataRemove(string Key, SourceClass Value)
    {
        //----------------------------------------------------------------------------------------------------
        if (_Basic_SourceListData_Dictionary.TryGetValue(Key, out List<SourceClass> DicValue))
        {
            _Basic_SourceListData_Dictionary[Key].Remove(Value);
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void SourceListDataSet(string Key, List<SourceClass> Value)
    {
        //----------------------------------------------------------------------------------------------------
        if (Value == null)
        {
            _Basic_SourceListData_Dictionary.Remove(Key);
            return;
        }

        if (_Basic_SourceListData_Dictionary.TryGetValue(Key, out List<SourceClass> DicValue))
        {
            _Basic_SourceListData_Dictionary[Key] = Value;
        }
        else
        {
            _Basic_SourceListData_Dictionary.Add(Key, Value);
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion

    #region - Get -
    public bool BoolDataGet(string Key)
    {
        //----------------------------------------------------------------------------------------------------
        return _Basic_BoolData_Dictionary.TryGetValue(Key, out bool Value) ? Value : false;
        //----------------------------------------------------------------------------------------------------
    }
    public float ValueDataGet(string Key, float percentage = 1)
    {
        //----------------------------------------------------------------------------------------------------
        float QuickSave_Value_Float = 0;
        if (_Basic_ValueData_Dictionary.TryGetValue(Key, out float Value))
        {
            QuickSave_Value_Float = Value;
        }
        return QuickSave_Value_Float * percentage;
        //----------------------------------------------------------------------------------------------------
    }
    public List<float> ValueListDataGet(string Key)
    {
        //----------------------------------------------------------------------------------------------------
        return _Basic_ValueListData_Dictionary.TryGetValue(Key, out List<float> Value) ? Value : null;
        //----------------------------------------------------------------------------------------------------
    }
    public string StringDataGet(string Key)
    {
        //----------------------------------------------------------------------------------------------------
        return _Basic_StringData_Dictionary.TryGetValue(Key, out string Value) ? Value : null;
        //----------------------------------------------------------------------------------------------------
    }
    public _Map_BattleObjectUnit ObjectDataGet(string Key)
    {
        //----------------------------------------------------------------------------------------------------
        return _Basic_ObjectData_Dictionary.TryGetValue(Key, out _Map_BattleObjectUnit Value) ? Value : null;
        //----------------------------------------------------------------------------------------------------
    }
    public List<_Map_BattleObjectUnit> ObjectListDataGet(string Key)
    {
        //----------------------------------------------------------------------------------------------------
        return _Basic_ObjectListData_Dictionary.TryGetValue(Key, out List<_Map_BattleObjectUnit> Value) ? Value : new List<_Map_BattleObjectUnit>();
        //----------------------------------------------------------------------------------------------------
    }


    #endregion
}
#endregion
#endregion PublicClass