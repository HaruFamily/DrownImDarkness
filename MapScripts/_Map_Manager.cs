using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEngine.Rendering.DebugUI;

public class _Map_Manager : MonoBehaviour
{
    #region FollowerBox
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //----------------------------------------------------------------------------------------------------
    public _Map_MoveManager _Map_MoveManager;

    public _Map_FieldCreator _Map_FieldCreator;
    public _Map_BattleCreator _Map_BattleCreator;

    public _Map_BattleRound _Map_BattleRound;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public TextAsset _Data_RegionInput_TextAsset;
    public TextAsset _Data_MapInput_TextAsset;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public Dictionary<string, RegionDataClass> _Data_Region_Dictionary = new Dictionary<string, RegionDataClass>();
    public Dictionary<string, MapDataClass> _Data_Map_Dictionary = new Dictionary<string, MapDataClass>();
    public Dictionary<string, LanguageClass> _Language_Map_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_Weather_Dictionary = new Dictionary<string, LanguageClass>();
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public GameObject _Map_SelectUnit_GameObject;
    //�쳥���a��m
    public Transform _Map_SelectStore_Transform;
    public Transform _Map_FieldMap_Transform;
    public Transform _Map_BattleMap_Transform;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public Queue<_Map_SelectUnit> _Map_SelectPool_ScriptsQueue = new Queue<_Map_SelectUnit>();
    //�d�򤺪����
    public List<_Map_SelectUnit> _Card_GroundUnitInRange_ScriptsList = new List<_Map_SelectUnit>();
    public List<_Map_SelectUnit> _Card_GroundUnitInRangePath_ScriptsList = new List<_Map_SelectUnit>();
    public List<_Map_SelectUnit> _Card_GroundUnitInRangeExtend_ScriptsList = new List<_Map_SelectUnit>();

    public List<_Map_SelectUnit> _Card_GroundUnitInPath_ScriptsList = new List<_Map_SelectUnit>();
    public List<_Map_SelectUnit> _Card_GroundUnitInSelect_ScriptsList = new List<_Map_SelectUnit>();
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //Class�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�]�w������O----------------------------------------------------------------------------------------------------
    //�a�ϸ��
    public class MapDataClass///�a��(�@�Ӧa�ϥi��]�t�Ƽ�Region)
    {
        public Vector2Int Size;
        //�����ܲ�
        public List<string> Syndrome = new List<string>();
        //�a��
        public string[,] Map;
        //�ӲŸ��N���ذϰ�
        public Dictionary<string, List<string>> RegionCode = new Dictionary<string, List<string>>();
        //�T�w�o�ͨƥ�
        public Dictionary<Vector2Int, string> TargetEvent = new Dictionary<Vector2Int, string>();
        //����ɶ��ƥ�(�q�`�Ѯ����)
        public Dictionary<string, List<string>> TimeEvent = new Dictionary<string, List<string>>();
    }
    //----------------------------------------------------------------------------------------------------

    //�]�w������O----------------------------------------------------------------------------------------------------
    //�a�ϸ��
    public class RegionDataClass//����Region
    {
        public List<string> Tags = new List<string>();
        //�ܲ�
        public List<string> Syndrome = new List<string>();
        //�ɶ��ƥ�(�u�b�Ӱϰ�o��)
        public List<KeyValuePair<string, int>> Branch = new List<KeyValuePair<string, int>>();
        //�ɶ��ƥ�(�u�b�Ӱϰ�o��)
        public Dictionary<string, List<string>> TimeEvent = new Dictionary<string, List<string>>();
        //�ƶq�ƥ�/���q�C��<�C���H��(�@�μƶq)>
        public List<Dictionary<string, int>> QuantityEvent = new List<Dictionary<string, int>>();
        //�H���ƥ�
        public List<Dictionary<string, int>> RandomEvent = new List<Dictionary<string, int>>();
    }
    //----------------------------------------------------------------------------------------------------

    //�O������T----------------------------------------------------------------------------------------------------
    public class GroundDataClass//�a����T
    {
        public string Region;
        public List<string> Tags = new List<string>();//����(�v�T��V/������)
        public string Event;//�ƥ�
        public List<_Map_FieldObjectUnit> Objects = new List<_Map_FieldObjectUnit>();//����������
    }
    //----------------------------------------------------------------------------------------------------

    //�O������T----------------------------------------------------------------------------------------------------
    public class FieldDataClass//�a����T
    {
        public Vector2Int Size;
        public Dictionary<Vector2Int, GroundDataClass> Data;//�a�����
        //�ܲ�(All(����),Region(�ϰ�))
        public Dictionary<string, List<string>> Syndrome = new Dictionary<string, List<string>>();
        //�ɶ��ƥ�(All(����),Region(�ϰ�))
        public Dictionary<string, Dictionary<string, List<string>>> TimeEvent = 
            new Dictionary<string, Dictionary<string, List<string>>>();
        //State
        public Dictionary<Vector2Int, bool> CanPass;//��_�g�L
        public Dictionary<Vector2Int, bool> CanView;//��_�z��
    }
    //----------------------------------------------------------------------------------------------------

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion FollowerBox

    #region FirstBuild
    //�Ĥ@��ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    void Awake()
    {
        //�]�w�ܼ�----------------------------------------------------------------------------------------------------
        _World_Manager._Map_Manager = this;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        DataSet();
        LanguageSet();
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion FirstBuild

    #region DataBaseSet
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    private void DataSet()
    {
        #region - Map -
        //�a��----------------------------------------------------------------------------------------------------
        string[] QuickSave_MapSourceSplit = _Data_MapInput_TextAsset.text.Split("�X"[0]);
        for (int t = 0; t < QuickSave_MapSourceSplit.Length; t++)
        {
            //�إ߸��----------------------------------------------------------------------------------------------------
            MapDataClass QuickSave_Map_Class = new MapDataClass();
            //���Ψ�L�P�Ʀr
            string[] QuickSave_Split_StringArray = QuickSave_MapSourceSplit[t].Split("�V"[0]);
            //----------------------------------------------------------------------------------------------------

            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            string[] QuickSave_DataSourceSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            string[] QuickSave_ElementSplit_StringArray = QuickSave_DataSourceSplit[2].Substring(6).Split(","[0]);
            QuickSave_Map_Class.Size =
                new Vector2Int(int.Parse(QuickSave_ElementSplit_StringArray[0]), int.Parse(QuickSave_ElementSplit_StringArray[1]));
            string QuickSave_Syndrome_String = QuickSave_DataSourceSplit[3].Substring(10);
            QuickSave_Map_Class.Syndrome = new List<string>();
            if (QuickSave_Syndrome_String != "")
            {
                QuickSave_Map_Class.Syndrome.AddRange(
                    new List<string>(QuickSave_Syndrome_String.Split(","[0])));
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_ElementDataSourceSplit = QuickSave_Split_StringArray[1].Split("\r"[0]);
                QuickSave_Map_Class.Map = new string[QuickSave_Map_Class.Size.x, QuickSave_Map_Class.Size.y];
                for (int y = 1; y < QuickSave_ElementDataSourceSplit.Length - 1; y++)
                {
                    string[] QuickSave_ExtraSplit = QuickSave_ElementDataSourceSplit[y].Split(","[0]);
                    for (int x = 0; x < QuickSave_ExtraSplit.Length; x++)
                    {
                        QuickSave_Map_Class.Map[x, y - 1] = QuickSave_ExtraSplit[x];
                    }
                }
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[2].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(","[0]);
                    List<string> QuickSave_Region_StringList = new List<string>();
                    for (int r = 1; r < QuickSave_TextSplit.Length; r++)
                    {
                        string[] QuickSave_InnerSplit_StringArray = QuickSave_TextSplit[r].Split(":"[0]);
                        int QuickSave_Count_Int = int.Parse(QuickSave_InnerSplit_StringArray[1]);
                        for (int a = 0; a < QuickSave_Count_Int; a++)
                        {
                            QuickSave_Region_StringList.Add(QuickSave_InnerSplit_StringArray[0]);
                        }
                    }
                    QuickSave_Map_Class.RegionCode.Add(QuickSave_TextSplit[0], QuickSave_Region_StringList);
                }
            }
            //----------------------------------------------------------------------------------------------------   
            
            //��L����----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_ElementDataSourceSplit = QuickSave_Split_StringArray[3].Split("\r"[0]);
                for (int s = 1; s < QuickSave_ElementDataSourceSplit.Length - 1; s++)
                {
                    string[] QuickSave_TextSplit = QuickSave_ElementDataSourceSplit[s].Substring(1).Split(":"[0]);
                    string[] QuickSave_VectorSplit = QuickSave_TextSplit[0].Split(","[0]);
                    Vector2Int QuickSave_Vector_Vector =
                        new Vector2Int(int.Parse(QuickSave_VectorSplit[0]), int.Parse(QuickSave_VectorSplit[1]));
                    //�m�J����
                    if (QuickSave_TextSplit[1] == "")
                    {
                        continue;
                    }
                    QuickSave_Map_Class.TargetEvent.Add(QuickSave_Vector_Vector, QuickSave_TextSplit[1]);
                }
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[4].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(":"[0]);
                    QuickSave_Map_Class.TimeEvent.Add(
                        QuickSave_TextSplit[0], new List<string>(QuickSave_TextSplit[1].Split(","[0])));
                }
            }
            //----------------------------------------------------------------------------------------------------


            //�]�w----------------------------------------------------------------------------------------------------
            //�m�J����
            _Data_Map_Dictionary.Add(QuickSave_DataSourceSplit[1].Substring(5), QuickSave_Map_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //����O����
        _Data_MapInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Region -
        //�ͪ�----------------------------------------------------------------------------------------------------
        //AI
        string[] QuickSave_RegionSourceSplit = _Data_RegionInput_TextAsset.text.Split("�X"[0]);
        for (int t = 0; t < QuickSave_RegionSourceSplit.Length; t++)
        {
            //�إ߸��----------------------------------------------------------------------------------------------------
            RegionDataClass QuickSave_Region_Class = new RegionDataClass();
            //���Ψ�L�P�Ʀr
            string[] QuickSave_Split_StringArray = QuickSave_RegionSourceSplit[t].Split("�V"[0]);
            //----------------------------------------------------------------------------------------------------

            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            string[] QuickSave_DataSourceSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            QuickSave_Region_Class.Tags =
                new List<string>(QuickSave_DataSourceSplit[2].Substring(6).Split(","[0]));
            QuickSave_Region_Class.Syndrome =
                new List<string>(QuickSave_DataSourceSplit[3].Substring(10).Split(","[0]));
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[1].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(":"[0]);
                    QuickSave_Region_Class.Branch.Add(
                        new KeyValuePair<string, int>(QuickSave_TextSplit[0], int.Parse(QuickSave_TextSplit[1])));
                }
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[2].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(":"[0]);
                    QuickSave_Region_Class.TimeEvent.Add(
                        QuickSave_TextSplit[0], new List<string>(QuickSave_TextSplit[1].Split(","[0])));
                }
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[3].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(","[0]);
                    Dictionary<string, int> QuickSave_Events_Dictionary = new Dictionary<string, int>();
                    int QuickSave_Count_Int = int.Parse(QuickSave_TextSplit[0]);
                    for (int r = 1; r < QuickSave_TextSplit.Length; r++)
                    {
                        string[] QuickSave_InnerSplit_StringArray = QuickSave_TextSplit[r].Split(":"[0]);
                        QuickSave_Events_Dictionary.Add(
                            QuickSave_InnerSplit_StringArray[0], int.Parse(QuickSave_InnerSplit_StringArray[1]));
                    }
                    for (int c = 0; c < QuickSave_Count_Int; c++)
                    {
                        QuickSave_Region_Class.QuantityEvent.Add(QuickSave_Events_Dictionary);
                    }
                }
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[4].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(","[0]);
                    Dictionary<string, int> QuickSave_Events_Dictionary = new Dictionary<string, int>();
                    int QuickSave_Count_Int = int.Parse(QuickSave_TextSplit[0]);
                    for (int r = 1; r < QuickSave_TextSplit.Length; r++)
                    {
                        string[] QuickSave_InnerSplit_StringArray = QuickSave_TextSplit[r].Split(":"[0]);
                        QuickSave_Events_Dictionary.Add(
                            QuickSave_InnerSplit_StringArray[0], int.Parse(QuickSave_InnerSplit_StringArray[1]));
                    }
                    for (int c = 0; c < QuickSave_Count_Int; c++)
                    {
                        QuickSave_Region_Class.RandomEvent.Add(QuickSave_Events_Dictionary);
                    }
                }
            }
            //----------------------------------------------------------------------------------------------------

            //�]�w----------------------------------------------------------------------------------------------------
            //�m�J����
            _Data_Region_Dictionary.Add(QuickSave_DataSourceSplit[1].Substring(5), QuickSave_Region_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //����O����
        _Data_RegionInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //�y���]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void LanguageSet()
    {
        #region - Map -
        //���o�_�l�奻----------------------------------------------------------------------------------------------------
        //�a�O���ƥ�
        string QuickSave_MapTextSource_String = "";
        string QuickSave_MapTextAssetCheck_String = "";
        QuickSave_MapTextAssetCheck_String = Application.streamingAssetsPath + "/Map/_" + _World_Manager._Config_Language_String + "_Map.txt";
        if (File.Exists(QuickSave_MapTextAssetCheck_String))
        {
            QuickSave_MapTextSource_String = File.ReadAllText(QuickSave_MapTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Map/_" + _World_Manager._Config_Language_String + "_Map.txt");
            QuickSave_MapTextAssetCheck_String = Application.streamingAssetsPath + "/Map/_TraditionalChinese_Map.txt";
            QuickSave_MapTextSource_String = File.ReadAllText(QuickSave_MapTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //�奻���λP�m�J����----------------------------------------------------------------------------------------------------
        //�a�O���ƥ�
        //���ά��涵
        string[] QuickSave_MapSourceSplit = QuickSave_MapTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_MapSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_MapSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_MapSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_Map_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Weather -
        //���o�_�l�奻----------------------------------------------------------------------------------------------------
        //�a�O���ƥ�
        string QuickSave_WeatherTextSource_String = "";
        string QuickSave_WeatherTextAssetCheck_String = "";
        QuickSave_WeatherTextAssetCheck_String = Application.streamingAssetsPath + "/Map/_" + _World_Manager._Config_Language_String + "_Weather.txt";
        if (File.Exists(QuickSave_WeatherTextAssetCheck_String))
        {
            QuickSave_WeatherTextSource_String = File.ReadAllText(QuickSave_WeatherTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Map/_" + _World_Manager._Config_Language_String + "_Weather.txt");
            QuickSave_WeatherTextAssetCheck_String = Application.streamingAssetsPath + "/Map/_TraditionalChinese_Weather.txt";
            QuickSave_WeatherTextSource_String = File.ReadAllText(QuickSave_WeatherTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //�奻���λP�m�J����----------------------------------------------------------------------------------------------------
        //�a�O���ƥ�
        //���ά��涵
        string[] QuickSave_WeatherSourceSplit = QuickSave_WeatherTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_WeatherSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_WeatherSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_WeatherSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_Weather_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion DataBaseSet

    #region Start
    public bool _Map_BattleComplete_Bool = false;
    //�_�l�I�s�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void ManagerStart(int TimePass = 0)
    {
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                {
                    string QuickSave_Map_String = 
                        _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
                    _Map_FieldMap_Transform.gameObject.SetActive(true);
                    _Map_BattleMap_Transform.gameObject.SetActive(false);
                    if (!_Map_FieldCreator._Map_Data_Dictionary.ContainsKey(QuickSave_Map_String))
                    {
                        //�إ��v��
                        FieldStateSet("Locking", "��l�]�w");
                        //�a�ϻs�@
                        _Map_FieldCreator.SystemStart(QuickSave_Map_String);
                    }
                    else//�q��L�a��^��(Field�w�ͦ�)
                    {
                        _UI_EventManager _UI_EventManager = _World_Manager._UI_Manager._UI_EventManager;
                        MapGroundUnitComplete();
                        /*
                        if (_UI_EventManager._Event_NowEventKey_String == "")
                        {
                            _World_Manager._Object_Manager._Object_Player_Script.BuildFront();
                        }
                        else
                        {
                            _UI_EventManager.EventFinding(_UI_EventManager._Event_NowVector_Class, TimePass, false);
                        }*/
                    }
                }
                break;
            case "Battle":
                {
                    _Map_BattleMap_Transform.gameObject.SetActive(true);
                    _Map_FieldMap_Transform.gameObject.SetActive(false);
                    if (!_Map_BattleComplete_Bool)
                    {
                        //�إ��v��
                        BattleStateSet("Locking", "��l�]�w");
                        //�a�ϻs�@
                        _Map_BattleCreator.SystemStart();
                        _Map_BattleComplete_Bool = true;
                    }
                }
                break;
        }
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Start


    #region MapComplete
    //�a�ϧ����X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void MapGroundUnitComplete()
    {
        //----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        _UI_EventManager _UI_EventManager = _World_Manager._UI_Manager._UI_EventManager;
        _Object_CreatureUnit QuickSave_Creature_Script = _Object_Manager._Object_Player_Script;
        //----------------------------------------------------------------------------------------------------

        //����}�l----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                {
                    //���a�C�}�l
                    Vector QuickSave_StartCoor_Class =
                        QuickSave_Creature_Script._Creature_FieldObjectt_Script._Map_Coordinate_Class;
                    QuickSave_Creature_Script._Player_Script.FieldStart(QuickSave_StartCoor_Class);
                }
                break;
            case "Battle":
                {
                    QuickSave_Creature_Script._Player_Script.BattleStart();
                    //�ͪ��s�y
                    _Object_Manager.NPCSet(_UI_EventManager._Battle_NPCCreateKey_String);
                    _Map_BattleRound.CreatureDelaySet();
                    //�_�l�I�s
                    BattleStateSet("RoundTimes", "�a�ϧ���");
                    //�^�X�i��
                    StartCoroutine(_Map_BattleRound.RoundCall());
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void ClearMap()
    {
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Battle":
                for (int x = 0; x < _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(0); x++)
                {
                    for (int y = 0; y < _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(1); y++)
                    {
                        if ((x + y) % 2 == 0)
                        {
                            Destroy(_Map_BattleCreator._Map_GroundBoard_ScriptsArray[x, y].gameObject);
                        }
                    }
                }
                _Map_BattleCreator._Map_PreCreate_BoolArray = null;
                _Map_BattleCreator._Map_GroundBoard_ScriptsArray = null;
                _Map_BattleMap_Transform.gameObject.SetActive(false);
                break;
        }
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion MapComplete

    #region FieldState
    //�]�w���A�����X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //��e���A��
    static public string _State_FieldState_String;
    //���A��----------------------------------------------------------------------------------------------------
    //Locking-��w
    //BuildFront-�e�m

    //SelectExplore-��ܱ���
    //SelectRange-��ܽd��

    //AnimeFront-�ʵe�e�m
    //AnimeMiddle-�ʵe���m
    //AnimeBack-�ʵe��m

    //BuildBack-�ͪ���m

    //EventFront-�ƥ�e�m
    //EventMiddle-�ƥ󤤸m
    //EventSelect-�ƥ���
    //EventFrame-�ƥ󻡩�
    //EventBack-�ƥ��m

    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //���A�վ�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void FieldStateSet(string Set, string Hint = null)
    {
        //���վ�----------------------------------------------------------------------------------------------------
        if (Hint != null && _World_Manager._Test_Hint_Bool)
        {
            print(Set + "�G" + Hint);
        }
        //----------------------------------------------------------------------------------------------------

        //�v���ܧ�----------------------------------------------------------------------------------------------------
        switch (Set)
        {
            case "Locking":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "BuildFront":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "SelectExplore":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "SelectRange":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "AnimeFront":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "AnimeMiddle":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "AnimeBack":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "BuildBack":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "EventFront":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "EventMiddle":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "EventSelect":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "EventFrame":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "EventBack":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;

            default:
                print("StateName_String�G��" + Set + "��is Wrong String");
                return;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //�]�w��e���A
        _State_FieldState_String = Set;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion FieldState


    #region BattleState
    //�]�w���A�����X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //��e���A��
    static public string _State_BattleState_String;
    //Ĳ�o�o�ʤ�
    public bool _State_Reacting_Bool;
    public List<string> _State_ReactTag_StringList;

    //���A��----------------------------------------------------------------------------------------------------
    //Locking-��w
    //RoundTimes-�ɶ��b�Ƨ�
    //RoundCall-�欰�I�s

    //BuildFront-�ͪ��e�m

    //PlayerBehavior-��ܦ欰
    //PlayerEnchance-��ܪ��]�P�d��
    //EnemySelect-�ĤH�欰

    //BuildMiddle-��ܤ��m

    //AnimeFront-�ʵe�e�m
    //AnimeMiddle-�ʵe���m
    //AnimeBack-�ʵe��m

    //BuildBack-�ͪ���m
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //���A�վ�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void BattleStateSet(string Set, string Hint = null)
    {
        //���վ�----------------------------------------------------------------------------------------------------
        if (Hint != null && _World_Manager._Test_Hint_Bool)
        {
            print("Times�G" + _Map_BattleRound._Round_Time_Int + "_" + Set + "�G" + Hint);
        }
        //----------------------------------------------------------------------------------------------------

        //�v���ܧ�----------------------------------------------------------------------------------------------------
        switch (Set)
        {
            case "Locking":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "RoundTimes":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "RoundCall":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "BuildFront":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "PlayerBehavior":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "PlayerEnchance":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "EnemySelect":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "BuildMiddle":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "AnimeFront":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "AnimeMiddle":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "AnimeBack":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "BuildBack":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            default:
                print("StateName_String�G��" + Set + "��is Wrong String");
                return;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //�]�w��e���A
        _State_BattleState_String = Set;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion BattleState


    #region ViewSet
    #region - SelectQueueSet -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public _Map_SelectUnit TakeSelectDeQueue(Vector Coordinate)//���X
    {
        //----------------------------------------------------------------------------------------------------
        _Map_SelectUnit Answer_Return_Script = null;
        Dictionary<Vector2Int, _Map_SelectUnit> QuickSave_Board_Dictionary = 
            new Dictionary<Vector2Int, _Map_SelectUnit>();
        Vector3 QuickSave_Pos_Vector = new Vector3();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                QuickSave_Board_Dictionary = _Map_FieldCreator._Map_SelectBoard_Dictionary;
                QuickSave_Pos_Vector = _Map_FieldCreator._Math_CooridnateTransform_Vector2(Coordinate);
                break;
            case "Battle":
                QuickSave_Board_Dictionary = _Map_BattleCreator._Map_SelectBoard_Dictionary;
                QuickSave_Pos_Vector = _Map_BattleCreator._Math_CooridnateTransform_Vector2(Coordinate);
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Board_Dictionary.TryGetValue(Coordinate.Vector2Int, out _Map_SelectUnit Value))
        {
            Answer_Return_Script = Value;
        }
        else
        {
            if (_Map_SelectPool_ScriptsQueue.Count > 0)
            {
                Answer_Return_Script = _Map_SelectPool_ScriptsQueue.Dequeue();
            }
            else
            {
                Answer_Return_Script =
                    Instantiate(_Map_SelectUnit_GameObject, _Map_SelectStore_Transform).GetComponent<_Map_SelectUnit>();
            }
            QuickSave_Board_Dictionary.Add(Coordinate.Vector2Int, Answer_Return_Script);
        }
        _UI_Card_Unit QuickSave_Card_Script =
            _World_Manager._UI_Manager._UI_CardManager._Card_UsingCard_Script;
        Answer_Return_Script.SystemStart(Coordinate, QuickSave_Pos_Vector, QuickSave_Card_Script);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Script;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void TakeSelectEnQueue(_Map_SelectUnit SelectUnit)//���^
    {
        //----------------------------------------------------------------------------------------------------
        Dictionary<Vector2Int, _Map_SelectUnit> QuickSave_Board_Dictionary = new Dictionary<Vector2Int, _Map_SelectUnit>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                QuickSave_Board_Dictionary = _Map_FieldCreator._Map_SelectBoard_Dictionary;
                break;
            case "Battle":
                QuickSave_Board_Dictionary = _Map_BattleCreator._Map_SelectBoard_Dictionary;
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        SelectUnit.transform.localPosition = Vector3.zero;
        SelectUnit.transform.localScale = Vector3.zero;
        _Map_SelectPool_ScriptsQueue.Enqueue(SelectUnit);
        QuickSave_Board_Dictionary.Remove(SelectUnit._Basic_Coordinate_Class.Vector2Int);
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion ViewSet

    #region RangeSet
    #region - ViewOn -
    //��ܦa���d��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void ViewOn(string Type, Vector Center/*Select�ɿ���I*/,
        List<Vector> Target,
        List<Vector> Range,
        List<Vector> Path,
        List<Vector> Select)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�Ӹ`����
        string[] QuickSave_Type_StringArray = Type.Split("_"[0]);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (QuickSave_Type_StringArray[0])
        {
            #region - Range -
            case "Range":
                {
                    //----------------------------------------------------------------------------------------------------
                    if (Target != null)
                    {
                        if (Range == null || Range.Count == 0)
                        {
                            Range = Target;
                        }
                    }
                    foreach (Vector Vector in Range)
                    {
                        //�ܼ�----------------------------------------------------------------------------------------------------
                        //�a��
                        _Map_SelectUnit _Map_RangeSelectUnit = TakeSelectDeQueue(Vector);
                        //----------------------------------------------------------------------------------------------------

                        //�]�w----------------------------------------------------------------------------------------------------
                        if (!_Map_RangeSelectUnit._State_InRange_Bool)
                        {
                            //�]�w���A
                            _Map_RangeSelectUnit._State_InRange_Bool = true;
                            //�]�w��ı
                            _Map_RangeSelectUnit.ColorSet("Range");
                        }
                        //----------------------------------------------------------------------------------------------------

                        //�[�J����----------------------------------------------------------------------------------------------------
                        if (!_Card_GroundUnitInRange_ScriptsList.Contains(_Map_RangeSelectUnit))
                        {
                            _Map_RangeSelectUnit._Map_MouseSencer_Collider.enabled = true;
                            _Card_GroundUnitInRange_ScriptsList.Add(_Map_RangeSelectUnit);
                        }
                        //----------------------------------------------------------------------------------------------------
                    }

                    //�B�~�d��----------------------------------------------------------------------------------------------------
                    //Path
                    foreach (Vector Vector in Path)
                    {
                        //���o�a��
                        _Map_SelectUnit _Map_PathSelectUnit = TakeSelectDeQueue(Vector);
                        //�w�g��Range
                        if (_Map_PathSelectUnit._State_InRange_Bool)
                        {
                            continue;
                        }

                        if (!_Map_PathSelectUnit._State_InRangePath_Bool)
                        {
                            //�]�w���A
                            _Map_PathSelectUnit._State_InRangePath_Bool = true;
                            //�]�w��ı
                            _Map_PathSelectUnit.ColorSet("RangePath");
                        }

                        if (!_Card_GroundUnitInRangePath_ScriptsList.Contains(_Map_PathSelectUnit))
                        {
                            _Map_PathSelectUnit._Map_MouseSencer_Collider.enabled = true;
                            _Card_GroundUnitInRangePath_ScriptsList.Add(_Map_PathSelectUnit);
                        }
                    }


                    //Select
                    foreach (Vector Vector in Select)
                    {
                        //�ܼ�----------------------------------------------------------------------------------------------------
                        //���o�a��
                        _Map_SelectUnit _Map_SelectSelectUnit = TakeSelectDeQueue(Vector);
                        //----------------------------------------------------------------------------------------------------

                        //�P�w----------------------------------------------------------------------------------------------------
                        //�w�g��Range/Path
                        if (_Map_SelectSelectUnit._State_InRange_Bool)
                        {
                            continue;
                        }
                        if (_Map_SelectSelectUnit._State_InRangePath_Bool)
                        {
                            continue;
                        }
                        //----------------------------------------------------------------------------------------------------

                        //�]�w----------------------------------------------------------------------------------------------------
                        if (!_Map_SelectSelectUnit._State_InRangeExtend_Bool)
                        {
                            //�]�w���A
                            _Map_SelectSelectUnit._State_InRangeExtend_Bool = true;
                            //�]�w��ı
                            _Map_SelectSelectUnit.ColorSet("RangeExtend");
                        }
                        //----------------------------------------------------------------------------------------------------

                        //�[�J����----------------------------------------------------------------------------------------------------
                        if (!_Card_GroundUnitInRangeExtend_ScriptsList.Contains(_Map_SelectSelectUnit))
                        {
                            _Card_GroundUnitInRangeExtend_ScriptsList.Add(_Map_SelectSelectUnit);
                        }
                        //----------------------------------------------------------------------------------------------------
                    }
                    //----------------------------------------------------------------------------------------------------
                }
                break;
            #endregion

            #region - Select -
            case "Select":
                {
                    //�B�~�d��----------------------------------------------------------------------------------------------------
                    //Path
                    foreach (Vector Vector in Path)
                    {
                        //�ܼ�----------------------------------------------------------------------------------------------------
                        //�a��
                        _Map_SelectUnit _Map_PathSelectUnit = TakeSelectDeQueue(Vector);
                        //----------------------------------------------------------------------------------------------------

                        //�]�w----------------------------------------------------------------------------------------------------
                        //�i����ܿ�ܽd�򤺥ؼд���'
                        //�]�w��ı
                        _Map_PathSelectUnit.ColorSet("Path");
                        //----------------------------------------------------------------------------------------------------

                        //�[�J����----------------------------------------------------------------------------------------------------
                        if (!_Card_GroundUnitInPath_ScriptsList.Contains(_Map_PathSelectUnit))
                        {
                            _Card_GroundUnitInPath_ScriptsList.Add(_Map_PathSelectUnit);
                        }
                        //----------------------------------------------------------------------------------------------------
                    }


                    //Select
                    foreach (Vector Vector in Select)
                    {
                        //�ܼ�----------------------------------------------------------------------------------------------------
                        //�a��
                        _Map_SelectUnit _Map_RangeSelectUnit = TakeSelectDeQueue(Vector);
                        //----------------------------------------------------------------------------------------------------

                        //�]�w----------------------------------------------------------------------------------------------------
                        //�i����ܿ�ܽd�򤺥ؼд���'
                        //�]�w��ı
                        _Map_RangeSelectUnit.ColorSet("Select");
                        //----------------------------------------------------------------------------------------------------

                        //�[�J����----------------------------------------------------------------------------------------------------
                        if (!_Card_GroundUnitInSelect_ScriptsList.Contains(_Map_RangeSelectUnit))
                        {
                            _Card_GroundUnitInSelect_ScriptsList.Add(_Map_RangeSelectUnit);
                        }
                        //----------------------------------------------------------------------------------------------------
                    }

                    _Map_SelectUnit _Map_CenterSelectUnit = TakeSelectDeQueue(Center);
                    //�ϥ��I�L�Į�
                    if (!_Card_GroundUnitInSelect_ScriptsList.Contains(_Map_CenterSelectUnit))
                    {
                        _Map_CenterSelectUnit.ColorSet("SelectNone");
                        _Card_GroundUnitInSelect_ScriptsList.Add(_Map_CenterSelectUnit);
                    }
                    //----------------------------------------------------------------------------------------------------

                }
                break;
            #endregion
            default:
                break;
        }
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - ViewOff -
    //��ܦa���d��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void ViewOff(string Type)
    {
        //�]�m----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            #region - Range -
            case "Range":
                //�������----------------------------------------------------------------------------------------------------
                //�d��
                for (int a = 0; a < _Card_GroundUnitInRange_ScriptsList.Count; a++)
                {
                    _Map_SelectUnit QuickSave_SelectUnit_Script =
                        _Card_GroundUnitInRange_ScriptsList[a];
                    QuickSave_SelectUnit_Script._Map_MouseSencer_Collider.enabled = false;
                    QuickSave_SelectUnit_Script._State_InRange_Bool = false;
                    TakeSelectEnQueue(QuickSave_SelectUnit_Script);
                    QuickSave_SelectUnit_Script.ColorSet("Clear");
                }
                _Card_GroundUnitInRange_ScriptsList.Clear();
                //���|
                for (int a = 0; a < _Card_GroundUnitInRangePath_ScriptsList.Count; a++)
                {
                    _Map_SelectUnit QuickSave_SelectUnit_Script =
                        _Card_GroundUnitInRangePath_ScriptsList[a];
                    QuickSave_SelectUnit_Script._Map_MouseSencer_Collider.enabled = false;
                    QuickSave_SelectUnit_Script._State_InRangePath_Bool = false;
                    TakeSelectEnQueue(QuickSave_SelectUnit_Script);
                    QuickSave_SelectUnit_Script.ColorSet("Clear");
                }
                _Card_GroundUnitInRangePath_ScriptsList.Clear();
                //�d�򩵦�
                for (int a = 0; a < _Card_GroundUnitInRangeExtend_ScriptsList.Count; a++)
                {
                    _Map_SelectUnit QuickSave_SelectUnit_Script =
                        _Card_GroundUnitInRangeExtend_ScriptsList[a];
                    QuickSave_SelectUnit_Script._State_InRangeExtend_Bool = false;
                    TakeSelectEnQueue(QuickSave_SelectUnit_Script);
                    QuickSave_SelectUnit_Script.ColorSet("Clear");
                }
                _Card_GroundUnitInRangeExtend_ScriptsList.Clear();
                //----------------------------------------------------------------------------------------------------
                break;
            #endregion

            #region - Select -
            case "Select":
                //�]�w----------------------------------------------------------------------------------------------------
                //���|
                for (int a = 0; a < _Card_GroundUnitInPath_ScriptsList.Count; a++)
                {
                    _Card_GroundUnitInPath_ScriptsList[a].ColorSet("Clear");
                }
                _Card_GroundUnitInPath_ScriptsList.Clear();
                //���
                for (int a = 0; a < _Card_GroundUnitInSelect_ScriptsList.Count; a++)
                {
                    _Card_GroundUnitInSelect_ScriptsList[a].ColorSet("Clear");
                }
                _Card_GroundUnitInSelect_ScriptsList.Clear();
                //----------------------------------------------------------------------------------------------------
                break;
            #endregion
            default:
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - PathSelect -
    //�̷�Vector(���w)�PDirection��z(�u��Key���ۦP)�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<PathSelectPairClass> PathSelectPair(PathCollectClass Path, SelectCollectClass Select, Vector Coordinate)
    {
        //----------------------------------------------------------------------------------------------------
        List<PathSelectPairClass> Answer_Return_ClassList = new List<PathSelectPairClass>();
        Dictionary<Vector, Dictionary<int, PathSelectPairClass>> QuickSave_Return_Dictionary =
            new Dictionary<Vector, Dictionary<int, PathSelectPairClass>>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (Path.Data.Count == 0)
        {
            //�LPath
            foreach (SelectUnitClass SelectUnit in Select.Data)
            {
                if (Coordinate != SelectUnit.Vector)
                {
                    continue;
                }
                if (QuickSave_Return_Dictionary.TryGetValue(SelectUnit.Vector, out Dictionary<int, PathSelectPairClass> VectorDic))
                {
                    if (VectorDic.TryGetValue(SelectUnit.Direction, out PathSelectPairClass DirectionDic))
                    {
                        DirectionDic.Select.Add(SelectUnit);
                    }
                    else
                    {
                        PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                        {
                            Path = new List<PathUnitClass>(),
                            Select = new List<SelectUnitClass> { SelectUnit }
                        };
                        QuickSave_Return_Dictionary[SelectUnit.Vector].Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                    }
                }
                else
                {
                    Dictionary<int, PathSelectPairClass> QuickSave_PathSelect_Dictionary =
                        new Dictionary<int, PathSelectPairClass>();
                    PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                    {
                        Path = new List<PathUnitClass> (),
                        Select = new List<SelectUnitClass> { SelectUnit }
                    };
                    QuickSave_PathSelect_Dictionary.Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                    QuickSave_Return_Dictionary.Add(SelectUnit.Vector, QuickSave_PathSelect_Dictionary);
                }
            }
        }
        else
        {
            foreach (PathUnitClass PathUnit in Path.Data)
            {
                foreach (SelectUnitClass SelectUnit in Select.Data)
                {
                    if (Coordinate != PathUnit.Vector ||
                        Coordinate != SelectUnit.Vector)
                    {
                        continue;
                    }
                    if (PathUnit.Direction == SelectUnit.Direction)
                    {
                        if (QuickSave_Return_Dictionary.TryGetValue(SelectUnit.Vector, out Dictionary<int, PathSelectPairClass> VectorDic))
                        {
                            if (VectorDic.TryGetValue(SelectUnit.Direction, out PathSelectPairClass DirectionDic))
                            {
                                DirectionDic.Path.Add(PathUnit);
                                DirectionDic.Select.Add(SelectUnit);
                            }
                            else
                            {
                                PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                                {
                                    Path = new List<PathUnitClass> { PathUnit },
                                    Select = new List<SelectUnitClass> { SelectUnit }
                                };
                                QuickSave_Return_Dictionary[SelectUnit.Vector].Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                            }
                        }
                        else
                        {
                            Dictionary<int, PathSelectPairClass> QuickSave_PathSelect_Dictionary =
                                new Dictionary<int, PathSelectPairClass>();
                            PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                            {
                                Path = new List<PathUnitClass> { PathUnit },
                                Select = new List<SelectUnitClass> { SelectUnit }
                            };
                            QuickSave_PathSelect_Dictionary.Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                            QuickSave_Return_Dictionary.Add(SelectUnit.Vector, QuickSave_PathSelect_Dictionary);
                        }
                    }
                }
            }
        }

        foreach (Dictionary<int, PathSelectPairClass> FDic in QuickSave_Return_Dictionary.Values)
        {
            foreach (PathSelectPairClass SDic in FDic.Values)
            {
                Answer_Return_ClassList.Add(SDic);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�̷�Vector�PDirection��z(�u��Key���ۦP)�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<PathSelectPairClass> PathSelectPair(PathCollectClass Path, SelectCollectClass Select)
    {
        //----------------------------------------------------------------------------------------------------
        List<PathSelectPairClass> Answer_Return_ClassList = new List<PathSelectPairClass>();
        Dictionary<Vector, Dictionary<int, PathSelectPairClass>> QuickSave_Return_Dictionary = 
            new Dictionary<Vector, Dictionary<int, PathSelectPairClass>>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (Path.Data.Count == 0)
        {
            foreach (SelectUnitClass SelectUnit in Select.Data)
            {
                if (QuickSave_Return_Dictionary.TryGetValue(SelectUnit.Vector, out Dictionary<int, PathSelectPairClass> VectorDic))
                {
                    if (VectorDic.TryGetValue(SelectUnit.Direction, out PathSelectPairClass DirectionDic))
                    {
                        DirectionDic.Select.Add(SelectUnit);
                    }
                    else
                    {
                        PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                        {
                            Path = new List<PathUnitClass>(),
                            Select = new List<SelectUnitClass> { SelectUnit }
                        };
                        QuickSave_Return_Dictionary[SelectUnit.Vector].Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                    }
                }
                else
                {
                    Dictionary<int, PathSelectPairClass> QuickSave_PathSelect_Dictionary =
                        new Dictionary<int, PathSelectPairClass>();
                    PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                    {
                        Path = new List<PathUnitClass>(),
                        Select = new List<SelectUnitClass> { SelectUnit }
                    };
                    QuickSave_PathSelect_Dictionary.Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                    QuickSave_Return_Dictionary.Add(SelectUnit.Vector, QuickSave_PathSelect_Dictionary);
                }
            }
        }
        else
        {
            foreach (PathUnitClass PathUnit in Path.Data)
            {
                foreach (SelectUnitClass SelectUnit in Select.Data)
                {
                    if (PathUnit.Vector == SelectUnit.Vector &&
                        PathUnit.Direction == SelectUnit.Direction)
                    {
                        if (QuickSave_Return_Dictionary.TryGetValue(SelectUnit.Vector, out Dictionary<int, PathSelectPairClass> VectorDic))
                        {
                            if (VectorDic.TryGetValue(SelectUnit.Direction, out PathSelectPairClass DirectionDic))
                            {
                                DirectionDic.Path.Add(PathUnit);
                                DirectionDic.Select.Add(SelectUnit);
                            }
                            else
                            {
                                PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                                {
                                    Path = new List<PathUnitClass> { PathUnit },
                                    Select = new List<SelectUnitClass> { SelectUnit }
                                };
                                QuickSave_Return_Dictionary[SelectUnit.Vector].Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                            }
                        }
                        else
                        {
                            Dictionary<int, PathSelectPairClass> QuickSave_PathSelect_Dictionary =
                                new Dictionary<int, PathSelectPairClass>();
                            PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                            {
                                Path = new List<PathUnitClass> { PathUnit },
                                Select = new List<SelectUnitClass> { SelectUnit }
                            };
                            QuickSave_PathSelect_Dictionary.Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                            QuickSave_Return_Dictionary.Add(SelectUnit.Vector, QuickSave_PathSelect_Dictionary);
                        }
                    }
                }
            }
        }

        foreach (Dictionary<int, PathSelectPairClass> FDic in QuickSave_Return_Dictionary.Values)
        {
            foreach (PathSelectPairClass SDic in FDic.Values)
            {
                Answer_Return_ClassList.Add(SDic);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - ClassToVector -
    //DirectionRangeClass��Ķ��VectorList�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<Vector> Range_ClassToVector(Vector Center, DirectionRangeClass RangeClass)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        List<Vector> Answer_Return_ClassList = new List<Vector>();
        //----------------------------------------------------------------------------------------------------

        //�إ߲M��----------------------------------------------------------------------------------------------------
        int QuickSave_DirectionX_Int = RangeClass.DirectionX;
        int QuickSave_DirectionY_Int = RangeClass.DirectionY;
        BoolRangeClass QuickSave_Range_Class = RangeClass.Range;
        for (int y = 0; y < QuickSave_Range_Class.Coordinate.GetLength(1); y++)
        {
            for (int x = 0; x < QuickSave_Range_Class.Coordinate.GetLength(0); x++)
            {
                //�]�w�ܼ�----------------------------------------------------------------------------------------------------
                int QuickSave_XCoordinate_Int = Center.X;
                int QuickSave_YCoordinate_Int = Center.Y;
                if (QuickSave_DirectionX_Int * QuickSave_DirectionY_Int == 1)
                {
                    QuickSave_XCoordinate_Int = Center.X -
                        QuickSave_DirectionX_Int * ((x - QuickSave_Range_Class.Center.x) + (y - QuickSave_Range_Class.Center.y));
                    QuickSave_YCoordinate_Int = Center.Y +
                        QuickSave_DirectionY_Int * ((x - QuickSave_Range_Class.Center.x) - (y - QuickSave_Range_Class.Center.y));
                }
                else
                {
                    QuickSave_XCoordinate_Int = Center.X -
                        -QuickSave_DirectionX_Int * ((x - QuickSave_Range_Class.Center.x) - (y - QuickSave_Range_Class.Center.y));
                    QuickSave_YCoordinate_Int = Center.Y +
                        -QuickSave_DirectionY_Int * ((x - QuickSave_Range_Class.Center.x) + (y - QuickSave_Range_Class.Center.y));
                }
                Vector QuickSave_Coordinate_Class = new Vector(QuickSave_XCoordinate_Int, QuickSave_YCoordinate_Int);
                //----------------------------------------------------------------------------------------------------

                //�ҥ~�P�w----------------------------------------------------------------------------------------------------
                //�d��s�b�P�w
                if (!RangeClass.Range.Coordinate[x, y])
                {
                    continue;
                }
                //�a�ϻ�����X�P�w
                if ((QuickSave_XCoordinate_Int + QuickSave_YCoordinate_Int)%2 != 0)
                {
                    continue;
                }
                //----------------------------------------------------------------------------------------------------

                //����----------------------------------------------------------------------------------------------------
                //�s�W�ܦ^�ǭ�
                Answer_Return_ClassList.Add(QuickSave_Coordinate_Class);
                //----------------------------------------------------------------------------------------------------
            }
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - ScriptToClass
    //GroundUnit��Ķ��BoolRangeClass�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public BoolRangeClass Range_ScriptToClass(Vector2Int Center, List<Vector2Int> Grounds)//new bool[1,1] = {X}
    {
        #region - GetArraySize -
        //�ܼ�----------------------------------------------------------------------------------------------------
        //��Ķ��a�O�y��
        List<Vector2Int> QuickSave_GroundsInArray_Vector2List = new List<Vector2Int>();
        //�̤j�ؤo
        int QuickSave_ArrayXMax_Int = 0;
        int QuickSave_ArrayYMax_Int = 0;
        //�̤p�ؤo
        int QuickSave_ArrayXMin_Int = 0;
        int QuickSave_ArrayYMin_Int = 0;
        //----------------------------------------------------------------------------------------------------

        //���o�}�C�j�p----------------------------------------------------------------------------------------------------
        for (int a = 0; a < Grounds.Count; a++)
        {
            //�������I�h����
            if (Grounds[a] == Center)
            {
                QuickSave_GroundsInArray_Vector2List.Add(new Vector2Int(0, 0));
                continue;
            }

            //�T�{��e�˯��O���y��
            Vector2Int QuickSave_Grounds_Vector3 = Grounds[a];
            //X
            int QuickSave_X_Int =
                ((QuickSave_Grounds_Vector3.x - Center.x) - (QuickSave_Grounds_Vector3.y - Center.y)) / 2;
            //Y
            int QuickSave_Y_Int =
                (-((QuickSave_Grounds_Vector3.x - Center.x) + (QuickSave_Grounds_Vector3.y - Center.y))) / 2;
            //�[�J��Ķ��y��
            QuickSave_GroundsInArray_Vector2List.Add(new Vector2Int(QuickSave_X_Int, QuickSave_Y_Int));

            //���o�̤j��X
            if (QuickSave_X_Int > QuickSave_ArrayXMax_Int)
            {
                QuickSave_ArrayXMax_Int = QuickSave_X_Int;
            }
            //���o�̤p��X
            else if (QuickSave_X_Int < QuickSave_ArrayXMin_Int)
            {
                QuickSave_ArrayXMin_Int = QuickSave_X_Int; ;
            }
            //���o�̤j��Y
            if (QuickSave_Y_Int > QuickSave_ArrayYMax_Int)
            {
                QuickSave_ArrayYMax_Int = QuickSave_Y_Int;
            }
            //���o�̤p��Y
            else if (QuickSave_Y_Int < QuickSave_ArrayYMin_Int)
            {
                QuickSave_ArrayYMin_Int = QuickSave_Y_Int; ;
            }
        }
        //----------------------------------------------------------------------------------------------------
        #endregion

        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǰ}�C
        BoolRangeClass Answer_Range_Class =
            new BoolRangeClass { Coordinate = new bool[(QuickSave_ArrayXMax_Int - QuickSave_ArrayXMin_Int) + 1, (QuickSave_ArrayYMax_Int - QuickSave_ArrayYMin_Int) + 1] };
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        //�����I�]�m
        Answer_Range_Class.Center = new Vector2Int(-QuickSave_ArrayXMin_Int, -QuickSave_ArrayYMin_Int);
        //�d��]�m
        for (int a = 0; a < QuickSave_GroundsInArray_Vector2List.Count; a++)
        {
            Answer_Range_Class.Coordinate
                [QuickSave_GroundsInArray_Vector2List[a].x - QuickSave_ArrayXMin_Int,
                QuickSave_GroundsInArray_Vector2List[a].y - QuickSave_ArrayYMin_Int] = true;
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return Answer_Range_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - RangeCheck -
    //�P�_�O�_���d�򤺡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public bool _Map_MapCheck_Bool(Vector Coordinate, int Xsize,int Ysize)
    {
        if ((Coordinate.X + Coordinate.Y) % 2 != 0)
        {
            return false;
        }
        if (Xsize <= Coordinate.x || Coordinate.x < 0 ||
            Ysize <= Coordinate.y || Coordinate.y < 0)
        {
            return false;
        }

        return true;
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion RangeSet
}
