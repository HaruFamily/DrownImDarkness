using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class _World_Manager : MonoBehaviour
{

    #region ManagerCaller
    //�]�w�������ܼơX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    static public _World_Manager _World_GeneralManager;
    static public _Map_Manager _Map_Manager;
    static public _Skill_Manager _Skill_Manager;
    static public _UI_Manager _UI_Manager;
    static public _Item_Manager _Item_Manager;
    static public _Effect_Manager _Effect_Manager;
    static public _Object_Manager _Object_Manager;
    static public _View_Manager _View_Manager;
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ManagerCaller


    #region ElementBox
    //�l���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //���U��ƶ�----------------------------------------------------------------------------------------------------
    //��v���޲z
    //public _World_CameraControl _World_CameraControl;
    //�����޲z
    public _World_ScenesManager _World_ScenesManager;
    //���ĺ޲z
    public _World_SoundManager _World_SoundManager;
    //�奻�޲z
    public _World_TextManager _World_TextManager;
    //----------------------------------------------------------------------------------------------------

    //Config�]�w��----------------------------------------------------------------------------------------------------
    //�ĪG���j�p
    static public int _Config_SoundEffectVolumn_Int;
    //�I�����j�p
    static public int _Config_SoundBackGroundVolumn_Int;
    //�H�n�j�p
    static public int _Config_SoundVocalVolumn_Int;
    //�ʵe�t��
    static public float _Config_AnimationSpeed_Float = 150;
    //�ƹ��Y��t��
    static public int _Config_CameraScaleSpeed_Int = 50;
    //�ƹ��Y��t��
    static public int _Config_CameraPositionSpeed_Int = 50;
    //�즲����
    static public short _Config_PullReverse_Short = 1;
    //�y����TraditionalChinese��
    static public string _Config_Language_String = "TraditionalChinese";
    //----------------------------------------------------------------------------------------------------

    //Test�������----------------------------------------------------------------------------------------------------
    //�������
    static public bool _Test_Hint_Bool = false;
    //----------------------------------------------------------------------------------------------------

    //�@���v���ܼ�----------------------------------------------------------------------------------------------------
    //��e����
    static public string _Authority_Scene_String;
    //��e��m
    static public string _Authority_Map_String;
    static public string _Authority_Weather_String;
    //�ާ@�v�����ƹLUI�\�i��//true = �i�H�ƹL
    static public bool _Authority_UICover_Bool = false;
    //�ާ@�v�����I���d���\�i��//true = �i�H�I��
    static public bool _Authority_CardClick_Bool = false;
    //�ާ@�v�����վ���v���\�i��//true = �i�H�վ�
    static public bool _Authority_CameraSet_Bool = false;

    //�ާ@�v��������I���ϥγ\�i�v��//true = �i�H�I�����
    static public bool _Authority_DialogueClick_Bool = false;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    //����L���j��
    static public List<string>
        System_SituationHashCode_StringList = null;
    //----------------------------------------------------------------------------------------------------

    //���y���A----------------------------------------------------------------------------------------------------
    //�v��
    static public bool _Privilege_Immo_Bool = false;
    static public bool _Privilege_Miio_Bool = false;
    static public bool _Privilege_Tobana_Bool = false;
    static public bool _Privilege_Lide_Bool = false;
    static public bool _Privilege_Limu_Bool = false;
    static public bool _Privilege_Choco_Bool = false;
    static public bool _Privilege_Rotetis_Bool = false;
    static public bool _Privilege_Nomus_Bool = false;
    //----------------------------------------------------------------------------------------------------

    //�D�Ϋ���----------------------------------------------------------------------------------------------------
    public Material _UI_HDRSprite_Material;
    public Material _UI_HDRText_Material;
    //----------------------------------------------------------------------------------------------------

    //Speaker----------------------------------------------------------------------------------------------------
    public bool _Speaker_NumberUnitIsReturn_Bool = false;
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ElementBox


    #region FirstBuild
    //�Ĥ@��ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    void Awake()
    {
        //�]�w�������ܼ�----------------------------------------------------------------------------------------------------
        _World_GeneralManager = this;

        _Authority_Map_String = "Map_Gap";
        _Authority_Weather_String = "Normal";
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion FirstBuild


    #region Start
    //�_�l�I�s�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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

        //�@�ΩI�s----------------------------------------------------------------------------------------------------
        //��v�����
        /*
        if (_World_CameraControl != null)
        {
            _World_CameraControl.SystemStart();
        }*/

        //UI�I�s
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
                    print("�}�l�ɪ�����Field");
                    _World_ScenesManager.SwitchScenes("Field");
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Start


    #region Update
    //�s���ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Update()
    {
        //�ƹ��ݩʨ��o----------------------------------------------------------------------------------------------------
        _Mouse_PositionOnCamera_Vector = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if(_Mouse_PositionOnCamera_Vector.y > 0.5f)
        {
            if(_Mouse_PositionOnCamera_Vector.x > 0.5f)
            {
                _Mouse_DirectionOnCamera_Short = 1;//�k�U
            }
            else
            {
                _Mouse_DirectionOnCamera_Short = 2;//���U
            }
        }
        else
        {
            if (_Mouse_PositionOnCamera_Vector.x < 0.5f)
            {
                _Mouse_DirectionOnCamera_Short = 3;//���W
            }
            else
            {
                _Mouse_DirectionOnCamera_Short = 4;//�k�W
            }
        }
        //----------------------------------------------------------------------------------------------------

        //���վ�----------------------------------------------------------------------------------------------------
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
                    print("Region�G" + _Authority_Map_String + "\n" +
                        _Map_Manager._State_FieldState_String + "\n" +
                        "Event�G" + _UI_Manager._UI_EventManager._Event_NowEventKey_String);
                    break;
                case "Battle":
                    print("Times�G" + _Map_BattleRound._Round_Time_Int + "\n" +
                        _Map_Manager._State_BattleState_String + "\n" +
                        "_UICover�G" + _Authority_UICover_Bool + "\n" +
                        "_CardClick�G" + _Authority_CardClick_Bool);
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
            string QuickSave_Save_String = System.DateTime.Now.ToString().Replace("�W��","Am").Replace("�U��", "Pm").Replace("/", "").Replace(":", "_");
            print("ScreenShot_" + QuickSave_Save_String + ".png");
            ScreenCapture.CaptureScreenshot("ScreenShot_" + QuickSave_Save_String + ".png");
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Update

    #region Customary variables
    //�������X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�ť�
    public Sprite _Image_Alpha_Sprite;
    //����
    public Sprite _Image_White_Sprite;
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�����ܼ����X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //----------------------------------------------------------------------------------------------------
    //���u���X�Z��
    public static Vector3 Infinity = new Vector3(65535, 65535, 65535);
    //�ƹ��ù���m���ù���m_���U��0.0�A�k�W��1.1��
    public Vector2 _Mouse_PositionOnCamera_Vector;
    //�ƹ��ù���졣�̷ӶH����
    public short _Mouse_DirectionOnCamera_Short;
    public static _Map_SelectUnit _Mouse_PositionOnSelect_Script;
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    #region - Math -
    //�B��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //���(�Q�i��)----------------------------------------------------------------------------------------------------
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Key -
    //�վ��ܼƤ��e
    //�p�G
    //AttackValue_SlashDamage�UConcept_Status_Vitality_0(�y���̾ڰʤO���A�ٶˮ`) �ܬ�
    //HealValue_MediumPoint�UConcept_Status_Vitality_0(�y���̾ڰʤO������^�_)
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public string Key_KeysUnit(string Situation, string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        string Answer_Return_String = Key;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            case "Data"://���/�p�G����80% ��
                //���W����X
                break;
            case "Default"://�w�]�p��/�S���ƭ��ܤ�
                {
                    if (UserSource != null &&
                        UserSource.Source_BattleObject != null)
                    {
                        //Situation//��ӳQ��/�ĪG/����
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public float Key_NumbersUnit(string Situation, string Key, float Value, 
        SourceClass UserSource, SourceClass TargetSource,_Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //�ֳt���X----------------------------------------------------------------------------------------------------
        _Speaker_NumberUnitIsReturn_Bool = false;
        if (Situation == "Data")
        {
            _Speaker_NumberUnitIsReturn_Bool = true;
            return Value;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        float Answer_Return_Float = 0;

        string[] QuickSave_ValueSplit_StringArray = Key.Split("�U"[0]);
        string[] QuickSave_Type_StringArray = QuickSave_ValueSplit_StringArray[0].Split("_"[0]);
        string[] QuickSave_Reference_StringArray = QuickSave_ValueSplit_StringArray[1].Split("_"[0]);


        float QuickSave_EnchanceAdd_Float = 0;//�B�~�W�[
        float QuickSave_EnchanceMultiply_Float = 1;
        float QuickSave_AdvanceAdd_Float = 0;//�B�~�W�[
        float QuickSave_AdvanceMultiply_Float = 1;
        //�ϥΪ̱��|
        _Object_CreatureUnit QuickSave_UserCreature_Script = UserSource.Source_Creature;
        _Map_BattleObjectUnit QuickSave_UsingObject_Script = UserSource.Source_BattleObject;
        if (QuickSave_UsingObject_Script == null && 
            QuickSave_UserCreature_Script != null)
        {
            QuickSave_UsingObject_Script = QuickSave_UserCreature_Script._Basic_Object_Script;
        }
        _UI_Card_Unit QuickSave_UsingCard_Script = UserSource.Source_Card;
        //�ؼб��|
        List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList = new List<_Map_BattleObjectUnit>();
        //----------------------------------------------------------------------------------------------------

        //�S����----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            case "Data"://���/�p�G����80% ��
                //���W����X
                break;
            case "Default"://�w�]�p��/�S���ƭ��ܤ�
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //�Ѧҳ]�w----------------------------------------------------------------------------------------------------
        #region - Target -
        {
            SourceClass QuickSave_Source_Class = null;
            //�Ѧҹ�H����
            switch (QuickSave_Reference_StringArray[0])
            {
                case "Value":
                case "User":
                    {
                        if (UserSource == null)
                        {
                            //��Ƥ�����/�^���Բ�(EX:�ؼг̤j�����10%)
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
                            //��Ƥ�����/�^���Բ�(EX:�ؼг̤j�����10%)
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
                            //��Ƥ�����/�^���Բ�(EX:�ؼг̤j�����10%)
                            _Speaker_NumberUnitIsReturn_Bool = true;
                            return Value;
                        }
                        QuickSave_Source_Class = UsingObject._Basic_Source_Class;
                    }
                    break;
                case "Card":
                    break;
            }
            //�Ѧҹ�H�ؼ�
            switch (QuickSave_Reference_StringArray[1])
            {
                case "Default":
                    {
                        if (QuickSave_Source_Class.Source_BattleObject == null)
                        {
                            //��Ƥ�����/�^���Բ�(EX:�ؼг̤j�����10%)
                            _Speaker_NumberUnitIsReturn_Bool = true;
                            return Value;
                        }
                        QuickSave_Objects_ScriptsList.Add(QuickSave_Source_Class.Source_BattleObject);
                    }
                    break;
                case "Concept"://�q�`�t�XUser��Target
                    {
                        if (QuickSave_Source_Class.Source_Creature == null)
                        {
                            //��Ƥ�����/�^���Բ�(EX:�ؼг̤j�����10%)
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
                            //��Ƥ�����/�^���Բ�(EX:�ؼг̤j�����10%)
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

        //�ƭȳ]�w----------------------------------------------------------------------------------------------------
        #region - Value -
        //����
        switch (QuickSave_Reference_StringArray[3])
        {
            #region - Value -
            case "Value":
                {
                    //�L�Ѧ�
                    Answer_Return_Float = Value;
                }
                break;
            #endregion

            #region - Material -
            case "Material":
                {
                    string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //���ƭ�(�ؤo���G����/�Z��/�D��/����)
                    //��¦��
                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        Answer_Return_Float += 
                            Object.Key_Material(QS_ReferenceTarget_String, UserSource, TargetSource);
                    }
                    //�ĪG�[��
                    //Key���w�p��
                    //�Y�ƭp��
                    Answer_Return_Float *= Value;
                }
                break;
            #endregion

            #region - Status -
            case "Status":
                //�T�{��O�ȹ�H
                {
                    string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //��O��(���赥�G���� �Z��/�D��/����)
                    //��¦��
                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        Answer_Return_Float += 
                            Object.Key_Status(QS_ReferenceTarget_String, UserSource, TargetSource, UsingObject);
                    }
                    //�ĪG�[��
                    //Key���w�p��
                    //�Y�ƭp��
                    Answer_Return_Float *= Value;
                }
                break;
            #endregion

            #region - Point -
            case "Point":
                //�T�{�p�q�ȹ�H
                {
                    string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //��O��(����ȵ��G���� �Z��/�D��/����)
                    //��¦��
                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        Answer_Return_Float += 
                            Object.Key_Point(QS_ReferenceTarget_String, QuickSave_Reference_StringArray[5], UserSource, TargetSource);
                    }
                    //�ĪG�[��
                    //Key���w�p��
                    //�Y�ƭp��
                    Answer_Return_Float *= Value;
                }
                break;
            #endregion

            #region - Stack -
            case "Stack":
                //�ŦX���Ҷq
                {
                    //��O��(����ȵ��G���� �Z��/�D��/����)
                    //Key
                    string QuickSave_Key_String = "";
                    for (int a = 6; a < QuickSave_Reference_StringArray.Length; a++)
                    {
                        QuickSave_Key_String += QuickSave_Reference_StringArray[a] + "_";
                    }
                    QuickSave_Key_String = QuickSave_Key_String.Remove(QuickSave_Key_String.Length - 1);
                    //��¦��
                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        Answer_Return_Float += Object.Key_Stack(
                            QuickSave_Reference_StringArray[5], QuickSave_Key_String,
                            UserSource, TargetSource, UsingObject);
                    }
                    //�Y�ƭp��
                    Answer_Return_Float = (Answer_Return_Float * Value);
                }
                break;
            #endregion

            #region - Card -
            case "DelayBefore":
                //�T�{�p�q�ȹ�H
                {
                    //string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //��O��(����ȵ��G���� �Z��/�D��/����)
                    //��¦��
                    Answer_Return_Float =
                        QuickSave_UsingCard_Script._Card_BehaviorUnit_Script.
                        Key_DelayBefore(TargetSource, UsingObject,
                        ContainEnchance: true, ContainTimeOffset: true);
                    //�ĪG�[��
                    //Key���w�p��
                    //�Y�ƭp��
                    Answer_Return_Float *= Value;
                }
                break;
            case "DelayAfter":
                //�T�{�p�q�ȹ�H
                {
                    //string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //��O��(����ȵ��G���� �Z��/�D��/����)
                    //��¦��
                    Answer_Return_Float =
                        QuickSave_UsingCard_Script._Card_BehaviorUnit_Script.
                        Key_DelayAfter(TargetSource, UsingObject,
                        ContainEnchance: true, ContainTimeOffset: true);
                    //�ĪG�[��
                    //Key���w�p��
                    //�Y�ƭp��
                    Answer_Return_Float *= Value;
                }
                break;
            #endregion

            #region - Data -
            case "Data":
                //�T�{���
                {
                    string QS_ReferenceTarget_String = QuickSave_Reference_StringArray[4];
                    //���(Combo��)
                    switch (QS_ReferenceTarget_String)
                    {
                        case "RandomNumber":
                            {
                                //�Y�ƭp��
                                Answer_Return_Float *= Value;
                            }
                            break;
                        case "Combo":
                        case "React":
                            {
                                //��¦��
                                Answer_Return_Float =
                                    QuickSave_UserCreature_Script._Basic_Object_Script._Basic_SaveData_Class.ValueDataGet("Combo", 1);
                                //�ĪG�[��
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
                                //�Y�ƭp��
                                Answer_Return_Float *= Value;
                            }
                            break;
                        case "CardsCount":
                            {
                                //��¦��
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
                                //�ĪG�[��
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
                                //�Y�ƭp��
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

        //�γ~�վ�----------------------------------------------------------------------------------------------------
        #region - Type -
        switch (QuickSave_Type_StringArray[0])
        {
            #region - Value -
            case "Value":
                {
                    //�L�Ѧ�/�W��w�g�]�m�F
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
                            case "Enchance"://���]�ĪG
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("EnchanceValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                            default://���`
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("AttackValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                        }
                    }
                    //��¦�ƭ�(�w�[����])
                    Answer_Return_Float = Mathf.RoundToInt(
                        (Answer_Return_Float + QuickSave_Value_Float) +
                        ((QuickSave_EnchanceAdd_Float) * QuickSave_EnchanceMultiply_Float));

                    //�ƭȭp��
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
                    //�`��
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
                            case "Enchance"://���]�ĪG
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("EnchanceValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                            default://���`
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("HealValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                        }
                    }
                    //��¦�ƭ�(�w�[����])
                    Answer_Return_Float = Mathf.RoundToInt(
                        (Answer_Return_Float + QuickSave_Value_Float) +
                        ((QuickSave_EnchanceAdd_Float) * QuickSave_EnchanceMultiply_Float));

                    //�ƭȭp��
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
                    //�`��
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
                            case "Enchance"://���]�ĪG
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("EnchanceValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                            default://���`
                                {
                                    QuickSave_Value_Float +=
                                        UsingObject.Key_Status("ConstructValue", UserSource, TargetSource, UsingObject);
                                }
                                break;
                        }
                    }
                    //��¦�ƭ�(�w�[����])
                    Answer_Return_Float = Mathf.RoundToInt(
                        (Answer_Return_Float + (QuickSave_Value_Float* Answer_Return_Float * 0.1f)) +
                        ((QuickSave_EnchanceAdd_Float) * QuickSave_EnchanceMultiply_Float));

                    //�ƭȭp��
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
                    //�`��
                    Answer_Return_Float =
                        Mathf.RoundToInt((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float);
                    
                }
                break;
            #endregion

            #region - Probability -
            case "Probability"://���v
                {
                    //�ƭȭp��
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
                    //�`��
                    Answer_Return_Float =
                        (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
                }
                break;
            #endregion

            #region - Percentage -
            case "Percentage"://�ʤ���
                {
                    //�ƭȭp��
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
                    //�`��
                    Answer_Return_Float =
                        (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
                }
                break;
            #endregion

            #region - Once -
            case "AttackTimes"://�D�n������
            case "HealTimes"://�D�n������
            case "PursuitTimes"://�l���ˮ`��

            case "Pursuit"://�l���ˮ`
            case "Deal"://��d
            case "Throw"://��d

            case "Path"://�d��
            case "Shift"://�첾

            case "Random"://�H����

            case "MoveHeight"://���ʰ���(���s�@���L��)
            case "ShiftHeight"://�첾����(���s�@���L��)
            case "AttackHeight"://��������(���s�@���L��)
            case "EffectHeight"://�ĪG����(���s�@���L��)
                {
                    //�ƭȭp��
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
                    //�`��
                    Answer_Return_Float =
                        Mathf.RoundToInt((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float);
                }
                break;
                //����/����(������H�P�ƭ�)
            case "Consume"://�ϥή���
            case "Remove"://���A����
                {
                    //�ƭȭp��
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
                    //�`��
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
            case "AttackTimes"://�D�n������
            case "HealTimes"://�D�n������
            case "PursuitTimes"://�l���ˮ`��
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //��l�H��----------------------------------------------------------------------------------------------------
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - SituationCallerMath -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> SituationCaller_TransToStringList(Dictionary<string,List<string>> TransTarget)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();

        foreach (string Key in TransTarget.Keys)
        {
            foreach (string Value in TransTarget[Key])
            {
                Answer_Return_StringList.Add(Key + "�U" + Value);
            }
        }
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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
    //�۷��InstanceID
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
    // ���� "==" �B��l
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
    public _Map_BattleObjectUnit UseObject;//�ϥ�
    public Vector FinalCoor;//�̲צ�m(�w�Q�p���V�P���d�᪺��m)
    public DirectionPathClass FinalPath;//�̲׸��|(�w�Q�p���V�P���d�᪺��m)
    public List<string> ScoreList = new List<string>();//���ƪ�(�Ω�AI�P�w)
    public List<_Map_BattleObjectUnit> PassObjects = new List<_Map_BattleObjectUnit>();
    public _Map_BattleObjectUnit HitObject;//�R���ؼ�(���]�t�b��z��
}
#endregion

#region - Battle -
//----------------------------------------------------------------------------------------------------
//���O-�ɶ����
[System.Serializable]
public class RoundSequenceUnitClass
{
    //�ɶ���m
    public int Time;//�`Sequence�ɥ�
    //�ɶ���m
    public string Type;//Priority�ɥ�
    //������
    public _Map_BattleObjectUnit Owner;
    //����
    public List<RoundElementClass> RoundUnit = new List<RoundElementClass>();

}
//���O-���
[System.Serializable]
public class RoundElementClass
{
    //����
    public SourceClass Source = new SourceClass();
    //��������
    public string DelayType;
    //����ɶ�(��������W�[���ɶ�)
    public int DelayTime;
    //���𰾲�
    public int DelayOffset;
    //�ֿn�ɶ�(�q�H�e��{�b���ɶ�)�B�P���W����ʮɶ�
    public int AccumulatedTime;
    //�`�ɶ�  �ؼЮɶ�(�]�Q���Φө���) 
    public int TargetTime;
}

//----------------------------------------------------------------------------------------------------

public class DamageClass
{
    //����
    public SourceClass Source = new SourceClass();
    public string DamageType;//�ˮ`����/Slash,Puncture,Impact,Energy,Chaos,Abstract,Stark
    public float Damage;//�ˮ`��
    public int Times;//�ˮ`����
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
    //�ӷ�����/Creature�BBehavior(Behavior�BEnchance)�BWeapon�BEffectObject�BEffectObject�BSystem
    //�S������/Event

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
//�d�����O
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

//���۽d�����O/�Ω���ܧޯ��V
[System.Serializable]
public class DirectionRangeClass
{
    //�ĤT�H��(-1,-1)/�ĤG�H��(+1,-1)
    //�ĥ|�H��(-1,+1)/�Ĥ@�H��(+1,+1)
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

//���۽d�����O/�Ω���ܧޯ��V
[System.Serializable]
public class DirectionPathClass
{
    //�ĤT�H��(-1,-1)/�ĤG�H��(+1,-1)
    //�ĥ|�H��(-1,+1)/�Ĥ@�H��(+1,+1)
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
    public string Key;//����
    public Vector Vector;//�y��
    public int Direction;//��V
    public DirectionPathClass Path;//�i�঳�ƼƱ�(�u�����)
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
    public string Key;//����
    public Vector Vector;//�y��
    public int Direction;//��V
    public DirectionRangeClass Select;//�i�঳�ƼƱ�(�u�����)

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
//�y�����O
public class LanguageClass
{
    public string Name;
    public string Summary;
    public string Description;
    public string Select = null;//�u���b�S���p�~�|��(Event)
}

[System.Serializable]
[HideInInspector]
public class NumbericalStsringClass
{
    //��¦¾
    public List<string> Default = new List<string>();
    //�B�~�W�[�ؿ�
    public List<string> AddKeys = new List<string>();//�s�W����
    public Dictionary<string, List<string>> ExtraDictionary = new Dictionary<string, List<string>>();

    //�B�~�ƭȨ��o/�Ʀr���N
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
    //���s(�w��Υ���)
    public void ExtraClear(string Type)
    {
        switch (Type)
        {
            case "all":
                AddKeys.Clear();
                ExtraDictionary.Clear();
                break;
            default:
                //�]�t���w
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

    //�`�M
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

                    #region - �y�� -
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

    //���ҽT�{
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

//�ƭ����O
[System.Serializable]
[HideInInspector]
public class NumbericalValueClass
{
    //�ƭ�
    public float Point;

    //��¦¾
    public float Default;
    //�B�~��
    public float Extra = 0;
    //�B�~�W�[�ؿ�
    public Dictionary<string, float> ExtraDictionary = new Dictionary<string, float>();

    //�B�~�ƭȨ��o/�Ʀr���N
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
    //���s(�w��Υ���)
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

    //�`�M
    public float Total()
    {
        return Default + Extra;
    }
    public float Gap()
    {
        return Total() - Point;
    }
}

//�`�h�ƻs
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
    //�u����X���X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    private Dictionary<string, Dictionary<string, int>> ActionTimes = 
        new Dictionary<string, Dictionary<string, int>>()//�����B�s���B���Ʋέp
    {
        {"Round",new Dictionary<string, int>(){ {"Default",0 } } }/*�^�X*/,
        {"Standby",new Dictionary<string, int>(){ {"Default",0 } } }/*�^�X�ݩR*/,
        {"Times",new Dictionary<string, int>(){ {"Default",0 } } }/*�C���ͩR�g��(�d�P:��d���s�B�ĪG:�����ɭ��s)*/
    };
    private Dictionary<string, Dictionary<string, int>> ActionTimesSave = 
        new Dictionary<string, Dictionary<string, int>>()//���ƼȦs
    {
        {"Round",new Dictionary<string, int>(){ {"Default",0 } } }/*�^�X*/,
        {"Standby",new Dictionary<string, int>(){ {"Default",0 } } }/*�^�X�ݩR*/,
        {"Times",new Dictionary<string, int>(){ {"Default",0 } } }/*�C���ͩR�g��(�d�P:��d���s�B�ĪG:�����ɭ��s)*/
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
        //�P�w----------------------------------------------------------------------------------------------------
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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