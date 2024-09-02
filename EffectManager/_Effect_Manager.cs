using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class _Effect_Manager : MonoBehaviour
{
    #region ElementBox
    //�l���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //��Ʒ�----------------------------------------------------------------------------------------------------
    //����ĪG��Ʈw
    public TextAsset _Data_EffectObjectInput_TextAsset;
    public TextAsset _Data_EffectCardInput_TextAsset;
    //����ĪG�Ϥ���Ʈw
    public PictureDataClass[] _Sprite_EffectObjectInput_ClassArray;
    public PictureDataClass[] _Sprite_EffectCardInput_ClassArray;
    //----------------------------------------------------------------------------------------------------

    //����----------------------------------------------------------------------------------------------------
    //����ĪG����
    public Dictionary<string, EffectDataClass> _Data_EffectObject_Dictionary = new Dictionary<string, EffectDataClass>();
    public Dictionary<string, EffectDataClass> _Data_EffectCard_Dictionary = new Dictionary<string, EffectDataClass>();
    public Dictionary<string, LanguageClass> _Language_EffectObject_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_EffectCard_Dictionary = new Dictionary<string, LanguageClass>();
    //----------------------------------------------------------------------------------------------------

    //�����----------------------------------------------------------------------------------------------------
    //�ͪ��ĪG
    public GameObject _Effect_CreatureUnit_GameObject;
    //�Z���ĪG
    public GameObject _Effect_ObjectUnit_GameObject;
    public GameObject _Effect_CardUnit_GameObject;
    //----------------------------------------------------------------------------------------------------
    //�l���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ElementBox


    #region DictionarySet
    //�U���Ϥ��פJ�ϡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�]�w������O----------------------------------------------------------------------------------------------------
    //����ĪG���
    public class EffectDataClass : DataClass
    {
        public List<string> EffectTag;//
        public string Decay;//���򫬺A
        public int StackLimit;
        public int DecayTimes;
        public float Value;//���n����
    }
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    [System.Serializable]
    //�W�ٻP�Ϥ�
    public class PictureDataClass
    {
        public string Key;
        public Sprite Sprite;
    }
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion DictionarySet


    #region FirstBuild
    //�Ĥ@��ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    void Awake()
    {
        //�]�w�ܼ�----------------------------------------------------------------------------------------------------
        _World_Manager._Effect_Manager = this;
        //----------------------------------------------------------------------------------------------------

        //�إ߸��----------------------------------------------------------------------------------------------------
        //��Ʈw
        DataSet();
        //�]�m�y��
        LanguageSet();
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion FirstBuild


    #region DataBaseSet
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    private void DataSet()
    {
        #region - EffectObject -
        //����ĪG----------------------------------------------------------------------------------------------------
        //���ά��涵
        string[] QuickSave_EffectObjectSourceSplit_StringArray = _Data_EffectObjectInput_TextAsset.text.Split("�X"[0]);
        for (int t = 0; t < QuickSave_EffectObjectSourceSplit_StringArray.Length; t++)
        {
            //�إ߸��----------------------------------------------------------------------------------------------------
            //�����O
            EffectDataClass QuickSave_Data_Class = new EffectDataClass();
            //���Ψ�L�P�Ʀr
            string[] QuickSave_Split_StringArray = QuickSave_EffectObjectSourceSplit_StringArray[t].Split("�V"[0]);
            //----------------------------------------------------------------------------------------------------

            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit_StringArray = QuickSave_Split_StringArray[0].Split("\r"[0]);
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Data_Class.EffectTag = new List<string>(QuickSave_TextSplit_StringArray[2].Substring(5).Split(","[0]));
            QuickSave_Data_Class.Decay = QuickSave_TextSplit_StringArray[3].Substring(7);
            QuickSave_Data_Class.StackLimit = int.Parse(QuickSave_TextSplit_StringArray[4].Substring(12));
            QuickSave_Data_Class.DecayTimes = int.Parse(QuickSave_TextSplit_StringArray[5].Substring(12));
            QuickSave_Data_Class.Value = float.Parse(QuickSave_TextSplit_StringArray[6].Substring(7));
            //----------------------------------------------------------------------------------------------------

            //�Ʀr----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_NumberSplit_StringArray = QuickSave_Split_StringArray[1].Split("\r"[0]);
            //�إ߸��
            for (int a = 1; a < QuickSave_NumberSplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_NumberSplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Numbers.Add(QuickSave_KeyValue[0].Substring(1), float.Parse(QuickSave_KeyValue[1]));
            }
            //----------------------------------------------------------------------------------------------------

            //�Ʀr----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_KeySplit_StringArray = QuickSave_Split_StringArray[2].Split("\r"[0]);
            //�إ߸��
            for (int a = 1; a < QuickSave_KeySplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_KeySplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Keys.Add(QuickSave_KeyValue[0].Substring(1), QuickSave_KeyValue[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //�m�J����
            _Data_EffectObject_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
        }
        //����O����
        _Data_EffectObjectInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - EffectCard -
        //����ĪG----------------------------------------------------------------------------------------------------
        //���ά��涵
        string[] QuickSave_EffectCardSourceSplit_StringArray = _Data_EffectCardInput_TextAsset.text.Split("�X"[0]);
        for (int t = 0; t < QuickSave_EffectCardSourceSplit_StringArray.Length; t++)
        {
            //�إ߸��----------------------------------------------------------------------------------------------------
            //�����O
            EffectDataClass QuickSave_Data_Class = new EffectDataClass();
            //���Ψ�L�P�Ʀr
            string[] QuickSave_Split_StringArray = QuickSave_EffectCardSourceSplit_StringArray[t].Split("�V"[0]);
            //----------------------------------------------------------------------------------------------------

            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit_StringArray = QuickSave_Split_StringArray[0].Split("\r"[0]);
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Data_Class.EffectTag = new List<string>(QuickSave_TextSplit_StringArray[2].Substring(5).Split(","[0]));
            QuickSave_Data_Class.Decay = QuickSave_TextSplit_StringArray[3].Substring(7);
            QuickSave_Data_Class.StackLimit = int.Parse(QuickSave_TextSplit_StringArray[4].Substring(12));
            QuickSave_Data_Class.DecayTimes = int.Parse(QuickSave_TextSplit_StringArray[5].Substring(12));
            QuickSave_Data_Class.Value = float.Parse(QuickSave_TextSplit_StringArray[6].Substring(7));
            //----------------------------------------------------------------------------------------------------

            //�Ʀr----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_NumberSplit_StringArray = QuickSave_Split_StringArray[1].Split("\r"[0]);
            //�إ߸��
            for (int a = 1; a < QuickSave_NumberSplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_NumberSplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Numbers.Add(QuickSave_KeyValue[0].Substring(1), float.Parse(QuickSave_KeyValue[1]));
            }
            //----------------------------------------------------------------------------------------------------

            //�Ʀr----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_KeySplit_StringArray = QuickSave_Split_StringArray[2].Split("\r"[0]);
            //�إ߸��
            for (int a = 1; a < QuickSave_KeySplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_KeySplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Keys.Add(QuickSave_KeyValue[0].Substring(1), QuickSave_KeyValue[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //�m�J����
            _Data_EffectCard_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
        }
        //����O����
        _Data_EffectCardInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //�y���]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void LanguageSet()
    {
        #region - EffectObject -
        //�ͪ��ĪG�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
        //�ܼ�----------------------------------------------------------------------------------------------------
        string QuickSave_EffectObjectTextSource_String = "";
        string QuickSave_EffectObjectTextAssetCheck_String = "";
        //----------------------------------------------------------------------------------------------------

        //���o����----------------------------------------------------------------------------------------------------
        QuickSave_EffectObjectTextAssetCheck_String = Application.streamingAssetsPath + "/Effect/_" + _World_Manager._Config_Language_String + "_EffectObject.txt";
        if (File.Exists(QuickSave_EffectObjectTextAssetCheck_String))
        {
            QuickSave_EffectObjectTextSource_String = File.ReadAllText(QuickSave_EffectObjectTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Effect/_" + _World_Manager._Config_Language_String + "_EffectObject.txt");
            QuickSave_EffectObjectTextAssetCheck_String = Application.streamingAssetsPath + "/Effect/_TraditionalChinese_EffectObject.txt";
            QuickSave_EffectObjectTextSource_String = File.ReadAllText(QuickSave_EffectObjectTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //Ū������----------------------------------------------------------------------------------------------------
        string[] QuickSave_EffectObjectSourceSplit = QuickSave_EffectObjectTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_EffectObjectSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_EffectObjectSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_EffectObjectSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_EffectObject_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
        #endregion

        #region - EffectCard -
        //�ͪ��ĪG�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
        //�ܼ�----------------------------------------------------------------------------------------------------
        string QuickSave_EffectCardTextSource_String = "";
        string QuickSave_EffectCardTextAssetCheck_String = "";
        //----------------------------------------------------------------------------------------------------

        //���o����----------------------------------------------------------------------------------------------------
        QuickSave_EffectCardTextAssetCheck_String = Application.streamingAssetsPath + "/Effect/_" + _World_Manager._Config_Language_String + "_EffectCard.txt";
        if (File.Exists(QuickSave_EffectCardTextAssetCheck_String))
        {
            QuickSave_EffectCardTextSource_String = File.ReadAllText(QuickSave_EffectCardTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Effect/_" + _World_Manager._Config_Language_String + "_EffectCard.txt");
            QuickSave_EffectCardTextAssetCheck_String = Application.streamingAssetsPath + "/Effect/_TraditionalChinese_EffectCard.txt";
            QuickSave_EffectCardTextSource_String = File.ReadAllText(QuickSave_EffectCardTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //Ū������----------------------------------------------------------------------------------------------------
        string[] QuickSave_EffectCardSourceSplit = QuickSave_EffectCardTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_EffectCardSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_EffectCardSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_EffectCardSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_EffectCard_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
        #endregion
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion DataBaseSet


    #region Start
    //�_�l�I�s�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void ManagerStart()
    {
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Start


    #region GetEffect
    #region EffectObject
    //���o�Z���ĪG�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> GetEffectObject(string Key, int Count,
        SourceClass UserSource, SourceClass TargetSource,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        string QuickSave_Type_String = Key.Split("_"[0])[0];
        //������
        _Effect_EffectObjectUnit QuickSave_Effect_Script = null;
        int QuickSave_Increase_Int = 0;
        int QuickSave_BeforeStack_Int = 0;
        int QuickSave_Value_Int = Count;
        //----------------------------------------------------------------------------------------------------

        //�K�̧P�w/�Ʀr�ܰ�----------------------------------------------------------------------------------------------------
        if (QuickSave_Type_String == "EffectObject")
        {
            List<string> QuickSave_Data_StringList =
                new List<string> { "Object", Key };
            QuickSave_Value_Int += int.Parse(
                UserSource.Source_BattleObject.SituationCaller("GetEffectValueAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueAdd"][0]);
            QuickSave_Value_Int *= int.Parse(
                UserSource.Source_BattleObject.SituationCaller("GetEffectValueMultiply", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueMultiply"][0]);
            //�K�̸��X
            if (!(QuickSave_Value_Int > 0))
            {
                return new List<string> { "Null�U" };
            }
        }
        //----------------------------------------------------------------------------------------------------

        //�P�w----------------------------------------------------------------------------------------------------
        if (TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Value))
        {
            QuickSave_BeforeStack_Int = Value.Key_Stack("Default", null, null, null, null);
            int QuickSave_EffectStackLimit_Int = Value.Key_StackLimit();
            if (QuickSave_BeforeStack_Int + QuickSave_Value_Int >= QuickSave_EffectStackLimit_Int)
            {
                QuickSave_Increase_Int = QuickSave_EffectStackLimit_Int - QuickSave_BeforeStack_Int;
            }
            else
            {
                QuickSave_Increase_Int = QuickSave_Value_Int;
            }
            if (Action)
            {
                QuickSave_Effect_Script = Value;
                QuickSave_Effect_Script.StackIncrease(QuickSave_Value_Int);
            }
        }
        else
        {
            if (_Data_EffectObject_Dictionary.TryGetValue(Key, out EffectDataClass Effect))
            {
                if (QuickSave_Value_Int >= Effect.StackLimit)
                {
                    QuickSave_Increase_Int = Effect.StackLimit;
                }
                else
                {
                    QuickSave_Increase_Int = QuickSave_Value_Int;
                }
            }
            else
            {
                QuickSave_Increase_Int = 1;
            }
            if (Action)
            {
                //�ͦ�
                QuickSave_Effect_Script = Instantiate(_Effect_ObjectUnit_GameObject, 
                    TargetSource.Source_BattleObject._View_EffectStore_Transform).GetComponent<_Effect_EffectObjectUnit>();
                QuickSave_Effect_Script.SystemStart(TargetSource.Source_BattleObject, Key);
                QuickSave_Effect_Script.StackIncrease(QuickSave_Value_Int);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Type_String == "EffectObject")
        {
            //�}��]�m
            if (!Action)
            {
                if (TargetSource.Source_Creature != null)
                {
                    if (HateTarget != null && HateTarget._Basic_Source_Class.Source_Creature == TargetSource.Source_Creature)
                    {
                        Answer_Return_StringList.Add("Hater_GetEffectObject�U" + Key + ":" + QuickSave_Increase_Int);
                    }
                    else
                    {
                        if (UserSource.Source_Creature == TargetSource.Source_Creature)
                        {
                            Answer_Return_StringList.Add("Self_GetEffectObject�U" + Key + ":" + QuickSave_Increase_Int);
                        }
                        else if (UserSource.Source_Creature._Data_Sect_String == TargetSource.Source_Creature._Data_Sect_String)
                        {
                            Answer_Return_StringList.Add("Friend_GetEffectObject�U" + Key + ":" + QuickSave_Increase_Int);
                        }
                        else
                        {
                            Answer_Return_StringList.Add("Enemy_GetEffectObject�U" + Key + ":" + QuickSave_Increase_Int);
                        }
                    }
                }
                else
                {
                    Answer_Return_StringList.Add("Object_GetEffectObject�U" + Key + ":" + QuickSave_Increase_Int);
                }
            }
            //�o��ĪG�� �ĪG
            List<string> QuickSave_Data_StringList =
                new List<string> { "Object", Key, QuickSave_BeforeStack_Int.ToString(), QuickSave_Value_Int.ToString() };
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            UserSource.Source_BattleObject.SituationCaller(
                "CauseGetEffect", QuickSave_Data_StringList/*�ĪG,�L���h��,�����ɼh��*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            TargetSource.Source_BattleObject.SituationCaller(
                "GetEffect", QuickSave_Data_StringList/*�ĪG,�L���h��,�����ɼh��*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //���C�Z���ĪG�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> LostEffectObject(string Key, int Count, SourceClass UserSource, SourceClass TargetSource,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        string QuickSave_Type_String = Key.Split("_"[0])[0];
        int QuickSave_Decrease_Int = 0;
        int QuickSave_BeforeStack_Int = 0;
        int QuickSave_Value_Int = Count;
        //----------------------------------------------------------------------------------------------------

        //�K�̧P�w/�Ʀr�ܰ�----------------------------------------------------------------------------------------------------
        if (QuickSave_Type_String == "EffectObject")
        {
            List<string> QuickSave_Data_StringList =
                new List<string> { "Object", Key };
            QuickSave_Value_Int += int.Parse(
                UserSource.Source_BattleObject.SituationCaller("LostEffectValueAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueAdd"][0]);
            QuickSave_Value_Int *= int.Parse(
                UserSource.Source_BattleObject.SituationCaller("LostEffectValueMultiply", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueMultiply"][0]);
            //�K�̸��X
            if (!(QuickSave_Value_Int > 0))
            {
                return new List<string> { "Null�U" };
            }
        }
        //----------------------------------------------------------------------------------------------------

        //�P�w----------------------------------------------------------------------------------------------------
        //�P�w�O�_�����A
        if (TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Value))
        {
            QuickSave_BeforeStack_Int = Value.Key_Stack("Default", null, null, null, null);
            if (QuickSave_Value_Int > QuickSave_BeforeStack_Int)
            {
                QuickSave_Decrease_Int = QuickSave_BeforeStack_Int;
            }
            else
            {
                QuickSave_Decrease_Int = QuickSave_Value_Int;
            }

            if (Action)
            {
                //���o��m
                Value.StackDecrease("Lost", QuickSave_Value_Int);
            }

            //----------------------------------------------------------------------------------------------------
            //�}��]�m
            if (!Action)
            {
                if (TargetSource.Source_Creature != null)
                {
                    if (HateTarget != null && HateTarget._Basic_Source_Class.Source_Creature == TargetSource.Source_Creature)
                    {
                        Answer_Return_StringList.Add("Hater_LostEffectObject�U" + Key + ":" + -QuickSave_Decrease_Int);
                    }
                    else
                    {
                        if (UserSource.Source_Creature == TargetSource.Source_Creature)
                        {
                            Answer_Return_StringList.Add("Self_LostEffectObject�U" + Key + ":" + -QuickSave_Decrease_Int);
                        }
                        else if (UserSource.Source_Creature._Data_Sect_String == TargetSource.Source_Creature._Data_Sect_String)
                        {
                            Answer_Return_StringList.Add("Friend_LostEffectObject�U" + Key + ":" + -QuickSave_Decrease_Int);
                        }
                        else
                        {
                            Answer_Return_StringList.Add("Enemy_LostEffectObject�U" + Key + ":" + -QuickSave_Decrease_Int);
                        }
                    }
                }
                else
                {
                    Answer_Return_StringList.Add("Object_LostEffectObject�U" + Key + ":" + -QuickSave_Decrease_Int);
                }
            }
            //----------------------------------------------------------------------------------------------------
        }
        else
        {
            return Answer_Return_StringList;
        }
        if (QuickSave_Type_String == "EffectObject")
        {
            //�o��ĪG�� �ĪG
            List<string> QuickSave_Data_StringList =
                new List<string> { "Object", Key, QuickSave_BeforeStack_Int.ToString(), QuickSave_Value_Int.ToString() };
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            UserSource.Source_BattleObject.SituationCaller(
                "CauseLostEffect", QuickSave_Data_StringList/*�ĪG,�L���h��,����ּ�*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            TargetSource.Source_BattleObject.SituationCaller(
                "LostEffect", QuickSave_Data_StringList/*�ĪG,�L���h��,����ּ�*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion EffectObject

    #region EffectCard
    //���o�Z���ĪG�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> GetEffectCard(string Key, int Count, 
        SourceClass UserSource, SourceClass TargetSource,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order, _Skill_EnchanceUnit EnchancePlace = null)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        string QuickSave_Type_String = Key.Split("_"[0])[0];
        if (TargetSource.Source_Card == null)
        {
            return Answer_Return_StringList;
        }
        //������
        _Effect_EffectCardUnit QuickSave_Effect_Script = null;
        int QuickSave_Increase_Int = 0;
        int QuickSave_Value_Int = Count;
        //----------------------------------------------------------------------------------------------------

        //�K�̧P�w/�Ʀr�ܰ�----------------------------------------------------------------------------------------------------
        if (QuickSave_Type_String == "EffectObject")
        {
            List<string> QuickSave_Data_StringList =
                new List<string> { "Card", Key };
            QuickSave_Value_Int += int.Parse(
                UserSource.Source_BattleObject.SituationCaller("GetEffectValueAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueAdd"][0]);
            QuickSave_Value_Int *= int.Parse(
                UserSource.Source_BattleObject.SituationCaller("GetEffectValueMultiply", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueMultiply"][0]);
        }
        //----------------------------------------------------------------------------------------------------

        //�P�w----------------------------------------------------------------------------------------------------
        if (_Data_EffectCard_Dictionary.TryGetValue(Key, out EffectDataClass Effect))
        {
            if (QuickSave_Value_Int >= Effect.StackLimit)
            {
                QuickSave_Increase_Int = Effect.StackLimit;
            }
            else
            {
                QuickSave_Increase_Int = QuickSave_Value_Int;
            }
        }
        else
        {
            QuickSave_Increase_Int = 1;
        }
        if (Action)
        {
            //�ͦ�
            QuickSave_Effect_Script = Instantiate(_Effect_CardUnit_GameObject, 
                TargetSource.Source_Card._View_EffectStore_Transform).GetComponent<_Effect_EffectCardUnit>();
            QuickSave_Effect_Script.SystemStart(TargetSource.Source_Card, Key, EnchancePlace);
            QuickSave_Effect_Script.StackIncrease(QuickSave_Increase_Int);
            if (EnchancePlace != null)
            {
                EnchancePlace._Owner_EnchanceEffectCard_Script = QuickSave_Effect_Script;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Type_String == "EffectObject")
        {
            //�}��]�m
            if (Action)
            {
                if (TargetSource.Source_Creature != null)
                {
                    if (HateTarget != null && HateTarget._Basic_Source_Class.Source_Creature == TargetSource.Source_Creature)
                    {
                        Answer_Return_StringList.Add("Hater_GetEffectCard�U" + Key + ":" + QuickSave_Increase_Int);
                    }
                    else
                    {
                        if (UserSource.Source_Creature == TargetSource.Source_Creature)
                        {
                            Answer_Return_StringList.Add("Self_GetEffectCard�U" + Key + ":" + QuickSave_Increase_Int);
                        }
                        else if (UserSource.Source_Creature._Data_Sect_String == TargetSource.Source_Creature._Data_Sect_String)
                        {
                            Answer_Return_StringList.Add("Friend_GetEffectCard�U" + Key + ":" + QuickSave_Increase_Int);
                        }
                        else
                        {
                            Answer_Return_StringList.Add("Enemy_GetEffectCard�U" + Key + ":" + QuickSave_Increase_Int);
                        }
                    }
                }
                else
                {
                    Answer_Return_StringList.Add("Object_GetEffectCard�U" + Key + ":" + QuickSave_Increase_Int);
                }
            }
            //�o��ĪG�� �ĪG
            List<string> QuickSave_Data_StringList =
                new List<string> { "Card", Key, "0", QuickSave_Value_Int.ToString() };
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            UserSource.Source_BattleObject.SituationCaller(
                "CauseGetEffect", QuickSave_Data_StringList/*�ĪG,�L���h��,�����ɼh��*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            TargetSource.Source_BattleObject.SituationCaller(
                "GetEffect", QuickSave_Data_StringList/*�ĪG,�L���h��,�����ɼh��*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion EffectCard
    #endregion GetEffect


    #region EffectAnim
    public IEnumerator EffectAnim(SourceClass Source, Dictionary<string, PathPreviewClass> PathPreview, bool CallNext)
    {
        //�ʵe
        yield return new WaitForSeconds(1f / (_World_Manager._Config_AnimationSpeed_Float * 0.01f));

        if (Source.Source_Card != null && PathPreview != null && CallNext)
        {
            Source.Source_Card.UseCardEffectEnd(PathPreview);
        }
    }
    #endregion EffectAnim
}
