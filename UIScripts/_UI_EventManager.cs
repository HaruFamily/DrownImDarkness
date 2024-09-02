using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class _UI_EventManager : MonoBehaviour
{
    #region ElementBox
    #region - DataElement -
    //子物件集——————————————————————————————————————————————————————————————————————
    //置入區----------------------------------------------------------------------------------------------------
    public Image _View_BackImage_Image;
    public Transform _View_BackDeco_Transform;
    public DialogueTextClass _View_Text_Class;
    public Transform _View_PlayerStatus_Transform;

    //選擇欲置物
    public GameObject _Event_Select_GameObject;
    public Transform _Event_EventSureBTN_Transform;
    public Transform _Event_SelectStore_Transform;

    //選擇物件池
    private Queue<_UI_Card_Unit> _Event_SelectPools_ScriptQueue = new Queue<_UI_Card_Unit>();
    public readonly Dictionary<string, _UI_Card_Unit> _Event_Selects_Dictionary = new Dictionary<string, _UI_Card_Unit>();

    //對話文字
    public DialogueTextClass[] _Dialogue_Text_ClassArray;
    //文字Layout
    public RectTransform _Dialogue_TextLayout_Transform;

    public Image _Dialogue_BackGroundImage_Image;
    public Transform _Figure_LeftOffset_Transform;
    public Image _Figure_LeftImage_Image;
    public Transform _Figure_RightOffset_Transform;
    public Image _Figure_RightImage_Image;


    //事件資料庫
    public TextAsset _Data_EventInput_TextAsset;
    //----------------------------------------------------------------------------------------------------

    //設定變數區----------------------------------------------------------------------------------------------------
    public HashSet<string> _Event_Played_HashSet = new HashSet<string>();
    //時間點
    public int _Time_Time_Int;
    public int _Time_TimeSave_Int;
    public Dictionary<string, int> _Event_TimePoolTimes_Dictionary =
        new Dictionary<string, int>();
    public List<string> _Syndrome_EquipSyndromePool_StringList = new List<string>();
    public List<string> _Syndrome_SyndromePool_StringList = new List<string>();//當前變異池

    public string _Battle_NPCCreateKey_String;

    private _UI_Card_Unit _Event_SelectCard_Script;
    private SourceClass Basic_Source_Class;
    //事件能力值
    public Dictionary<string, float> _Event_StatusAdd_Dictionary = new Dictionary<string, float>();
    public Dictionary<string, float> _Event_StatusMultiply_Dictionary = new Dictionary<string, float>();


    public EventDataClass _Event_PlayingEvent_Class;
    //----------------------------------------------------------------------------------------------------

    //索引----------------------------------------------------------------------------------------------------
    //地板型事件索引
    public Dictionary<string, EventDataClass> _Data_Event_Dictionary = new Dictionary<string, EventDataClass>();
    public Dictionary<string, LanguageClass> _Language_Event_Dictionary = new Dictionary<string, LanguageClass>();
    //營地對話索引//米歐
    public Dictionary<string, List<string>> _Language_DialogueCamp_Dictionary = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> _Dialogue_DialogueCamp_Dictionary = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> _Dialogue_DialogueCampDefault_Dictionary = new Dictionary<string, List<string>>();
    //地板事件對話索引
    public Dictionary<string, List<string>> _Language_DialogueEvent_Dictionary = new Dictionary<string, List<string>>();
    //----------------------------------------------------------------------------------------------------
    //子物件集——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion ElementBox

    #region Introduce
    /*EventFinding(事件查找)→
     * StartEventDialogue(對話)→
     * EndDialogue(對話結束)→
     * --新增--Key_EventSet(派發獎勵)→
     * EventContinue(EventFrameSet)(設定基礎)→
     * (Select)(EventFrameSet)(選項)→
     * EndEvent(確認)→
     * --已移除--//Key_EventSet(派發獎勵)→
     * Key_EventPass(跳轉)→
     * StartEventDialogue(對話)→
     * …
     * …
     * EventEndSet(結束事件)
     */
    #endregion

    #region DictionarySet
    //各類圖片匯入區——————————————————————————————————————————————————————————————————————
    //設定資料類別----------------------------------------------------------------------------------------------------
    //地板型事件資料
    public class EventDataClass : DataClass
    {
        public List<string> Avatar = new List<string>();//旁側人物
        //類型/Normal(普通 選項群)(大部分)、Cycle(循環 保存選群(可增減))、
        //Connect(接續事件)(不允許穿插探索卡時(主線事件等))、Replace(以當前取代事件)、Compare(比較)、
        //End(事件尾(結束事件))
        public string Type;
        public string ConnectKey;//Connect/Replace /連接
        public List<string> SelectKey = new List<string>();//Normal/Cycle /選項

        public List<string> OwnTag;//事件標籤

        public List<string> UseTag;//做為選項時 使用標籤
        public int TimePass;//做為選項時 經過時間
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    //各類圖片匯入區——————————————————————————————————————————————————————————————————————
    //設定文字內容類別----------------------------------------------------------------------------------------------------
    //輸出內容容器
    [System.Serializable]
    public class DialogueTextClass
    {
        public Transform Layout;
        public TextMeshProUGUI Name;
        public RectTransform FeedBack;
        public TextMeshProUGUI Summary;
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    
    public SourceClass _Basic_Source_Class(string Key)
    {
        Basic_Source_Class.Source_Event = _Data_Event_Dictionary[Key];
        return Basic_Source_Class;
    }
    
    #endregion DictionarySet


    #region FirstBuild
    //第一行動——————————————————————————————————————————————————————————————————————
    void Awake()
    {
        //建立資料----------------------------------------------------------------------------------------------------
        //資料庫
        DataSet();
        //設置語言
        LanguageSet();
        //----------------------------------------------------------------------------------------------------

        //初始化----------------------------------------------------------------------------------------------------
        _Time_Time_Int = 0;
        _Event_TimePoolTimes_Dictionary.Add("Syndrome", 0);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion FirstBuild


    #region DataBaseSet
    //——————————————————————————————————————————————————————————————————————
    private void DataSet()
    {
        #region - Event
        //文本分割與置入索引----------------------------------------------------------------------------------------------------
        //地板型事件
        //分割為單項
        string[] QuickSave_EventSourceSplit_StringArray = _Data_EventInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_EventSourceSplit_StringArray.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            EventDataClass QuickSave_Data_Class = new EventDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_EventSourceSplit_StringArray[t].Split("–"[0]);
            //----------------------------------------------------------------------------------------------------

            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------


            //其他項目----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit_StringArray = QuickSave_Split_StringArray[0].Split("\r"[0]);
            //建立資料_Substring(X)代表由X開始
            QuickSave_Data_Class.Avatar = 
                new List<string>(QuickSave_TextSplit_StringArray[2].Substring(8).Split(","[0]));
            QuickSave_Data_Class.Type = 
                QuickSave_TextSplit_StringArray[3].Substring(6);
            QuickSave_Data_Class.ConnectKey = 
                QuickSave_TextSplit_StringArray[4].Substring(12);
            QuickSave_Data_Class.SelectKey =
                new List<string>(QuickSave_TextSplit_StringArray[5].Substring(11).Split(","[0]));
            QuickSave_Data_Class.OwnTag =
                new List<string>(QuickSave_TextSplit_StringArray[6].Substring(8).Split(","[0]));
            QuickSave_Data_Class.UseTag = 
                new List<string>(QuickSave_TextSplit_StringArray[7].Substring(8).Split(","[0]));
            QuickSave_Data_Class.TimePass = 
                int.Parse(QuickSave_TextSplit_StringArray[8].Substring(10));
            //----------------------------------------------------------------------------------------------------

            //數字----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_NumberSplit_StringArray = QuickSave_Split_StringArray[1].Split("\r"[0]);
            //建立資料
            for (int a = 1; a < QuickSave_NumberSplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_NumberSplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Numbers.Add(QuickSave_KeyValue[0].Substring(1), float.Parse(QuickSave_KeyValue[1]));
            }
            //----------------------------------------------------------------------------------------------------

            //數字----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_KeySplit_StringArray = QuickSave_Split_StringArray[2].Split("\r"[0]);
            //建立資料
            for (int a = 1; a < QuickSave_KeySplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_KeySplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Keys.Add(QuickSave_KeyValue[0].Substring(1), QuickSave_KeyValue[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //置入索引
            _Data_Event_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
        }
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————

    //語言設置——————————————————————————————————————————————————————————————————————
    public void LanguageSet()
    {
        #region - Event -
        //取得起始文本----------------------------------------------------------------------------------------------------
        //地板型事件
        string QuickSave_EventTextSource_String = "";
        string QuickSave_EventTextAssetCheck_String = "";
        QuickSave_EventTextAssetCheck_String = Application.streamingAssetsPath + "/Event/_" + _World_Manager._Config_Language_String + "_Event.txt";
        if (File.Exists(QuickSave_EventTextAssetCheck_String))
        {
            QuickSave_EventTextSource_String = File.ReadAllText(QuickSave_EventTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Event/_" + _World_Manager._Config_Language_String + "_Event.txt");
            QuickSave_EventTextAssetCheck_String = Application.streamingAssetsPath + "/Event/_TraditionalChinese_Event.txt";
            QuickSave_EventTextSource_String = File.ReadAllText(QuickSave_EventTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //文本分割與置入索引----------------------------------------------------------------------------------------------------
        //地板型事件
        //分割為單項
        string[] QuickSave_EventSourceSplit = QuickSave_EventTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_EventSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_EventSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_EventSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);
            QuickSave_Language_Class.Select = QuickSave_TextSplit[5].Substring(8);

            //置入索引
            _Language_Event_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Dialogue
        //取得起始文本----------------------------------------------------------------------------------------------------
        //地板型事件
        string QuickSave_DialogueEventTextSource_String = "";
        string QuickSave_DialogueEventTextAssetCheck_String = "";
        QuickSave_DialogueEventTextAssetCheck_String = Application.streamingAssetsPath + "/Dialogue/_" + _World_Manager._Config_Language_String + "_DialogueEvent.txt";
        if (File.Exists(QuickSave_DialogueEventTextAssetCheck_String))
        {
            QuickSave_DialogueEventTextSource_String = File.ReadAllText(QuickSave_DialogueEventTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Dialogue/_" + _World_Manager._Config_Language_String + "_DialogueEvent.txt");
            QuickSave_DialogueEventTextAssetCheck_String = Application.streamingAssetsPath + "/Dialogue/_TraditionalChinese_DialogueEvent.txt";
            QuickSave_DialogueEventTextSource_String = File.ReadAllText(QuickSave_DialogueEventTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //文本分割與置入索引----------------------------------------------------------------------------------------------------
        //地板型事件
        //分割為單項
        string[] QuickSave_DialogueEventSourceSplit = QuickSave_DialogueEventTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_DialogueEventSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_DialogueEventSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_DialogueEventSourceSplit[t].Split("\r"[0]);
            List<string> QuickSave_Contains_StringList = new List<string>();
            //建立資料_Substring(X)代表由X開始
            for (int a = 3; a < QuickSave_TextSplit.Length - 1; a++)
            {
                string QuickSave_TextSubString_String = QuickSave_TextSplit[a].Substring(1);
                if (QuickSave_TextSubString_String.Length == 0 || QuickSave_TextSubString_String.Substring(0, 2) == "//")
                {
                    continue;
                }
                QuickSave_Contains_StringList.Add(QuickSave_TextSubString_String);
            }

            //置入索引
            _Language_DialogueEvent_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Contains_StringList);
        }
        //----------------------------------------------------------------------------------------------------


        //取得起始文本----------------------------------------------------------------------------------------------------
        //地板型事件
        string QuickSave_CampTextSource_String = "";
        string QuickSave_CampTextAssetCheck_String = "";
        QuickSave_CampTextAssetCheck_String = Application.streamingAssetsPath + "/Dialogue/_" + _World_Manager._Config_Language_String + "_DialogueCamp.txt";
        if (File.Exists(QuickSave_CampTextAssetCheck_String))
        {
            QuickSave_CampTextSource_String = File.ReadAllText(QuickSave_CampTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Dialogue/_" + _World_Manager._Config_Language_String + "_DialogueCamp.txt");
            QuickSave_CampTextAssetCheck_String = Application.streamingAssetsPath + "/Dialogue/_TraditionalChinese_DialogueCamp.txt";
            QuickSave_CampTextSource_String = File.ReadAllText(QuickSave_CampTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //文本分割與置入索引----------------------------------------------------------------------------------------------------
        //地板型事件
        //分割為單項
        string[] QuickSave_CampSourceSplit = QuickSave_CampTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_CampSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_CampSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_CampSourceSplit[t].Split("\r"[0]);
            List<string> QuickSave_Contains_StringList = new List<string>();
            //建立資料_Substring(X)代表由X開始
            for (int a = 3; a < QuickSave_TextSplit.Length - 1; a++)
            {
                string QuickSave_TextSubString_String = QuickSave_TextSplit[a].Substring(1);
                if (QuickSave_TextSubString_String.Length == 0 || QuickSave_TextSubString_String.Substring(0, 2) == "//")
                {
                    continue;
                }
                QuickSave_Contains_StringList.Add(QuickSave_TextSubString_String);
            }

            //置入索引
            _Language_DialogueCamp_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Contains_StringList);
        }
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion DataBaseSet

    #region Start
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void SystemStart()
    {
        _World_Manager._UI_Manager._UI_Event_Transform.gameObject.SetActive(false);
        _View_BackDeco_Transform.gameObject.SetActive(false);

        _Object_CreatureUnit QuickSave_Creature_Script = 
            _World_Manager._Object_Manager._Object_Player_Script;
        Basic_Source_Class = new SourceClass
        {
            SourceType = "Event",
            Source_Creature = QuickSave_Creature_Script,
            Source_Concept = QuickSave_Creature_Script._Object_Inventory_Script._Item_EquipConcepts_Script,
            Source_BattleObject = QuickSave_Creature_Script._Basic_Object_Script
        };

        //回到邊際
        _World_Manager._UI_Manager._UI_Dialogue_Transform.gameObject.SetActive(false);
        _World_Manager._UI_Manager._View_Battle._View_Image_Image.gameObject.SetActive(true);
        //對話初始化
        EndDialogueInitialization();
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start



    #region Dialogue
    //暫存區——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    //執行中種類 Event = 地板行物件
    private string _Dialogue_PlayingType_String;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    //開始對話——————————————————————————————————————————————————————————————————————
    public void StartDialogue(List<string> Contain, string Type)
    {
        //取得文本
        List<string> QuickSave_Dialogue_StringList = new List<string>();

        //回到中心
        _World_Manager._UI_Manager._UI_Dialogue_Transform.gameObject.SetActive(true);
        //暫時隱藏角色
        _World_Manager._UI_Manager._View_Battle._View_Image_Image.gameObject.SetActive(false);

        //鎖定
        _World_Manager._Authority_UICover_Bool = false;
        _World_Manager._Authority_CameraSet_Bool = false;


        //判斷對話----------------------------------------------------------------------------------------------------
        _Dialogue_PlayingType_String = Type;
        Dialogue(Contain);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //文本結構——————————————————————————————————————————————————————————————————————
    public class DialogueClass
    {
        //人物名稱
        public string Name;
        //位置
        public int Direction;//Middle = 0，Left = -1，Right = 1
        //台詞
        public string Lines;

        //額外
        public string BackGround;
        public bool ChangePosition;
    }
    //當前劇本
    private List<List<DialogueClass>> _Dialogue_NowStory_ClassListList = new List<List<DialogueClass>>();
    //當前行數
    private int _Dialogue_NowStory_Int = 0;
    //——————————————————————————————————————————————————————————————————————


    //劇本翻譯——————————————————————————————————————————————————————————————————————
    //翻譯劇本
    public void Dialogue(List<string> Drama)
    {
        //總劇本庫
        List<List<DialogueClass>> QuickSave_Drama_ClassListList = new List<List<DialogueClass>>();

        //每幕檢查
        for (int a = 0; a < Drama.Count; a++)
        {
            //變數----------------------------------------------------------------------------------------------------
            //當幕劇本處存
            List<DialogueClass> QuickSave_DramaChapter_ClassList = new List<DialogueClass>();
            //掃描中文檔
            string QuickSave_Text_String = "";
            //解碼中文檔
            string QuickSave_Code_String = "";
            //解碼開關
            bool QuickSave_CodeSwitch_Bool = false;
            //----------------------------------------------------------------------------------------------------

            //發話類別輸入值----------------------------------------------------------------------------------------------------
            //名稱
            string QuickSave_ClassName_String = "";
            //位置
            int QuickSave_ClassDirection_Int = 0;
            //台詞
            string QuickSave_ClassLines_String = "";

            //額外
            //背景
            string QuickSave_ClassBackGround_String = "";
            bool QuickSave_ClassChangePosition_Bool = false;
            //----------------------------------------------------------------------------------------------------

            //掃描-分割文黨----------------------------------------------------------------------------------------------------
            for (int b = 0; b < Drama[a].Length; b++)
            {
                switch (Drama[a][b].ToString())
                {
                    case "﹝":
                        QuickSave_CodeSwitch_Bool = true;
                        break;
                    case "﹞":
                        //解碼
                        #region CodeTranslate
                        //分割密碼
                        string[] QuickSave_CodeTrans_StringArray = QuickSave_Code_String.Split(":"[0]);
                        //解碼
                        switch (QuickSave_CodeTrans_StringArray[0])
                        {
                            //文本翻譯
                            case "LineFeed":
                                //換行
                                QuickSave_Text_String += "\n";
                                break;
                            case "CharacterName":
                                //角色名稱
                                string QuickSave_CharacterName = 
                                    _World_Manager._World_GeneralManager._World_TextManager.
                                    _Language_UIName_Dictionary[QuickSave_CodeTrans_StringArray[1]];
                                QuickSave_Text_String += QuickSave_CharacterName;
                                break;
                            case "TakeOver":
                                //交換發話者與生成類別
                                //設置內容
                                QuickSave_ClassLines_String = QuickSave_Text_String;
                                //設置類別
                                DialogueClass QuickSave_DialogueCutClass_Class = new DialogueClass
                                { 
                                    Name = QuickSave_ClassName_String, 
                                    Direction = QuickSave_ClassDirection_Int, 
                                    Lines = QuickSave_ClassLines_String ,

                                    BackGround = QuickSave_ClassBackGround_String,
                                    ChangePosition = QuickSave_ClassChangePosition_Bool
                                };
                                //加入該幕劇本
                                QuickSave_DramaChapter_ClassList.Add(QuickSave_DialogueCutClass_Class);

                                //初始化
                                QuickSave_Text_String = "";
                                QuickSave_ClassName_String = "";
                                QuickSave_ClassDirection_Int = 0;
                                QuickSave_ClassLines_String = "";
                                QuickSave_ClassBackGround_String = "";
                                break;

                            case "SpeakRight":
                                //右方發話
                                QuickSave_ClassName_String = QuickSave_CodeTrans_StringArray[1];
                                QuickSave_ClassDirection_Int = 1;
                                break;
                            case "SpeakLeft":
                                //左方發話
                                QuickSave_ClassName_String = QuickSave_CodeTrans_StringArray[1];
                                QuickSave_ClassDirection_Int = -1;
                                break;
                            case "Speak":
                                //旁白發話
                                QuickSave_ClassDirection_Int = 0;
                                break;
                        }
                        //額外
                        if (QuickSave_CodeTrans_StringArray.Length > 2)
                        {
                            for (int e = 2; e < QuickSave_CodeTrans_StringArray.Length; e++)
                            {
                                string[] QuickSave_SubKey_StringArray = QuickSave_CodeTrans_StringArray[e].Split("|"[0]);
                                switch (QuickSave_SubKey_StringArray[0])
                                {
                                    case "BackGround"://背景
                                        {
                                            QuickSave_ClassBackGround_String = QuickSave_SubKey_StringArray[1];
                                        }
                                        break;
                                    case "ChangePosition"://換位
                                        {
                                            QuickSave_ClassChangePosition_Bool = true;
                                        }
                                        break;
                                }
                            }
                        }
                        #endregion CodeTranslate

                        //初始化
                        QuickSave_CodeSwitch_Bool = false;
                        QuickSave_Code_String = "";
                        break;
                    default:
                        if (QuickSave_CodeSwitch_Bool)
                        {
                            QuickSave_Code_String += Drama[a][b];
                        }
                        else
                        {
                            QuickSave_Text_String += Drama[a][b];
                        }
                        break;
                }

                if (b + 1 == Drama[a].Length)
                {
                    QuickSave_ClassLines_String = QuickSave_Text_String;
                    //設置類別
                    DialogueClass QuickSave_DialogueCutClass_Class = new DialogueClass
                    { 
                        Name = QuickSave_ClassName_String, 
                        Direction = QuickSave_ClassDirection_Int, 
                        Lines = QuickSave_ClassLines_String,

                        BackGround = QuickSave_ClassBackGround_String,
                        ChangePosition = QuickSave_ClassChangePosition_Bool
                    };
                    //加入該幕劇本
                    QuickSave_DramaChapter_ClassList.Add(QuickSave_DialogueCutClass_Class);
                }
            }
            //----------------------------------------------------------------------------------------------------

            //加入總劇本----------------------------------------------------------------------------------------------------
            //當幕劇本處存
            QuickSave_Drama_ClassListList.Add(QuickSave_DramaChapter_ClassList);
            //----------------------------------------------------------------------------------------------------
        }


        //開始
        _Dialogue_NowStory_ClassListList = QuickSave_Drama_ClassListList;
        //權限變更
        _World_Manager._Authority_DialogueClick_Bool = true;
        _Dialogue_Talking_Bool = false;

        //回合輪轉
        switch (_Dialogue_PlayingType_String)
        {
            case "Event":
                _World_Manager._Map_Manager.FieldStateSet("EventMiddle", "有事件，開始進行對話");
                break;
        }
        DialogueNext();
    }
    //——————————————————————————————————————————————————————————————————————


    //開始演出//——————————————————————————————————————————————————————————————————————
    public IEnumerator DialoguePrint()
    {
        _View_Manager _View_Manager = _World_Manager._View_Manager;

        //對話框占用
        _Dialogue_Talking_Bool = true;
        _Dialogue_PrintSwitch_Bool = false;

        for (int b = 0; b < _Dialogue_NowStory_ClassListList[_Dialogue_NowStory_Int].Count; b++)
        {
            //變數----------------------------------------------------------------------------------------------------
            DialogueClass QuickSave_Data_Class = _Dialogue_NowStory_ClassListList[_Dialogue_NowStory_Int][b];
            //檢查器
            /*print("A：" + _Dialogue_NowStory_Int + "_B：" + b + 
                "_Name：" + QuickSave_Data_Class.Name + 
                "_Direction：" + QuickSave_Data_Class.Direction + 
                "_Lines：" + QuickSave_Data_Class.Lines);*/
            //變更文字
            DialogueTextClass QuickSave_Text_Class = _Dialogue_Text_ClassArray[b];
            //消除文字與延遲
            if (b != 1)
            {
                _Dialogue_Text_ClassArray[1].Name.text = "";
                _Dialogue_Text_ClassArray[1].FeedBack.sizeDelta = new Vector2(_Dialogue_Text_ClassArray[1].FeedBack.sizeDelta.x, 0);
                _Dialogue_Text_ClassArray[1].Summary.text = "";
            }
            else
            {
                yield return new WaitForSeconds(0.15f);
            }
            //打字機佔存
            string QuickSave_Printer_String = "";
            //----------------------------------------------------------------------------------------------------

            //設定位置----------------------------------------------------------------------------------------------------
            //主圖片
            if (QuickSave_Data_Class.BackGround != "")
            {
                _Dialogue_BackGroundImage_Image.sprite =
                    _View_Manager.GetSprite("Event", "BackGround", QuickSave_Data_Class.BackGround);
            }
            Color QuickSave_Standby_Color = _View_Manager.GetColor("Code", "Empty");
            switch (QuickSave_Data_Class.Direction)
            {
                //左側
                case -1:
                    //文字位置
                    QuickSave_Text_Class.Name.alignment = TextAlignmentOptions.TopLeft;
                    QuickSave_Text_Class.FeedBack.sizeDelta = new Vector2(QuickSave_Text_Class.FeedBack.sizeDelta.x, 2);
                    QuickSave_Text_Class.Summary.alignment = TextAlignmentOptions.TopLeft;
                    //圖片位置
                    _Figure_LeftOffset_Transform.localPosition = Vector3.zero;
                    _Figure_LeftImage_Image.color = Color.white;
                    _Figure_RightOffset_Transform.localPosition = new Vector3(0, -50, 0);
                    _Figure_RightImage_Image.color = QuickSave_Standby_Color;
                    break;
                //右側
                case 1:
                    //文字位置
                    QuickSave_Text_Class.Name.alignment = TextAlignmentOptions.TopRight;
                    QuickSave_Text_Class.FeedBack.sizeDelta = new Vector2(QuickSave_Text_Class.FeedBack.sizeDelta.x, 2);
                    QuickSave_Text_Class.Summary.alignment = TextAlignmentOptions.TopLeft;
                    //圖片位置
                    _Figure_LeftOffset_Transform.localPosition = new Vector3(0, -50, 0);
                    _Figure_LeftImage_Image.color = QuickSave_Standby_Color;
                    _Figure_RightOffset_Transform.localPosition = Vector3.zero;
                    _Figure_RightImage_Image.color = Color.white;
                    break;
                //旁白
                case 0:
                    //文字位置
                    QuickSave_Text_Class.Name.alignment = TextAlignmentOptions.Top;
                    QuickSave_Text_Class.FeedBack.sizeDelta = new Vector2(QuickSave_Text_Class.FeedBack.sizeDelta.x, 2);
                    QuickSave_Text_Class.Summary.alignment = TextAlignmentOptions.Top;
                    //圖片位置
                    _Figure_LeftOffset_Transform.localPosition = new Vector3(0, -50, 0);
                    _Figure_LeftImage_Image.color = QuickSave_Standby_Color;
                    _Figure_RightOffset_Transform.localPosition = new Vector3(0, -50, 0);
                    _Figure_RightImage_Image.color = QuickSave_Standby_Color;
                    break;
            }
            //----------------------------------------------------------------------------------------------------

            //設定角色----------------------------------------------------------------------------------------------------
            string QuickSave_Name_String = QuickSave_Data_Class.Name;
            //設定圖片
            switch (QuickSave_Data_Class.Direction)
            {
                //左
                case -1:
                    {
                        _Figure_LeftImage_Image.sprite =
                            _View_Manager.GetSprite("Creature", "Fullbody", "Creature_" + QuickSave_Name_String + "_0");
                        if (QuickSave_Data_Class.ChangePosition)
                        {
                            _Figure_RightImage_Image.sprite =
                                _View_Manager.GetSprite("Creature", "null", "null");
                        }
                    }
                    break;
                //右
                case 1:
                    {
                        _Figure_RightImage_Image.sprite =
                            _View_Manager.GetSprite("Creature", "Fullbody", "Creature_" + QuickSave_Name_String + "_0");
                        if (QuickSave_Data_Class.ChangePosition)
                        {
                            _Figure_LeftImage_Image.sprite =
                                _View_Manager.GetSprite("Creature", "null", "null");
                        }
                    }
                    break;
                case 0:
                    _Figure_LeftImage_Image.sprite = 
                        _View_Manager.GetSprite("Creature", "null", "null");
                    _Figure_RightImage_Image.sprite = 
                        _View_Manager.GetSprite("Creature", "null", "null");
                    break;
            }
            //設定名稱
            if (QuickSave_Name_String != "")
            {
                //設定名稱
                QuickSave_Text_Class.Name.text = _World_Manager._World_GeneralManager._World_TextManager._Language_UIName_Dictionary[QuickSave_Name_String];
            }
            else
            {
                //設為空
                QuickSave_Text_Class.Name.text = "\r";
            }
            //----------------------------------------------------------------------------------------------------

            //打字機----------------------------------------------------------------------------------------------------
            int t = 0;
            //打字
            while (!_Dialogue_PrintSwitch_Bool)
            {
                QuickSave_Printer_String += QuickSave_Data_Class.Lines[t];
                QuickSave_Text_Class.Summary.text = QuickSave_Printer_String;
                yield return new WaitForSeconds(0.05f);
                t++;
                if (t >= QuickSave_Data_Class.Lines.Length)
                {
                    break;
                }
            }
            QuickSave_Text_Class.Summary.text = QuickSave_Data_Class.Lines;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)QuickSave_Text_Class.Name.transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)QuickSave_Text_Class.Summary.transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)QuickSave_Text_Class.Layout.transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_Dialogue_TextLayout_Transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_Dialogue_TextLayout_Transform);
            //----------------------------------------------------------------------------------------------------
            _Dialogue_PrintSwitch_Bool = false;
        }
        _Dialogue_NowStory_Int++;
        _Dialogue_Talking_Bool = false;
    }
    //——————————————————————————————————————————————————————————————————————


    //對話中判定
    public bool _Dialogue_Talking_Bool = false;
    //打字中判定
    public bool _Dialogue_PrintSwitch_Bool = false;
    //對話繼續


    //劇本翻頁——————————————————————————————————————————————————————————————————————
    public void DialogueNext(bool Skip = false)
    {
        if (_World_Manager._Authority_DialogueClick_Bool)
        {
            //對話中跳過用
            if (_Dialogue_Talking_Bool)
            {
                _Dialogue_PrintSwitch_Bool = true;
            }
            else
            {
                if (Skip)
                {
                    _Dialogue_NowStory_Int += 99;
                    _Dialogue_BackGroundImage_Image.sprite =
                        _World_Manager._View_Manager.GetSprite("Event", "null", "null");
                }
                //超過文本跳出
                if (_Dialogue_NowStory_Int >= _Dialogue_NowStory_ClassListList.Count)
                {
                    EndDialogue();
                }
                else
                {
                    //下段對話
                    StartCoroutine(DialoguePrint());
                }
            }
        }
    }
    //——————————————————————————————————————————————————————————————————————

    //結束對話——————————————————————————————————————————————————————————————————————
    public void EndDialogue()
    {
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;

        //初始化----------------------------------------------------------------------------------------------------
        //打字機開關
        _Dialogue_PrintSwitch_Bool = false;
        //故事行數
        _Dialogue_NowStory_Int = 0;
        //退出對話判斷
        _World_Manager._Authority_DialogueClick_Bool = false;
        //初始化
        EndDialogueInitialization();

        //鎖定解除
        _World_Manager._Authority_UICover_Bool = true;
        _World_Manager._Authority_CameraSet_Bool = true;

        //回到邊際
        _UI_Manager._UI_Dialogue_Transform.gameObject.SetActive(false);
        _World_Manager._UI_Manager._View_Battle._View_Image_Image.gameObject.SetActive(true);
        //----------------------------------------------------------------------------------------------------

        //呼叫----------------------------------------------------------------------------------------------------
        switch (_Dialogue_PlayingType_String)
        {
            case "Camp":
                {
                    Key_CampSet();
                }
                break;
            case "Event":
                {
                    Key_EventSet();
                }
                break;
            default:
                print("PlayingType_String：﹝" + _Dialogue_PlayingType_String + "﹞is Wrong String");
                break;
        }
        //執行種類
        _Dialogue_PlayingType_String = null;
        //----------------------------------------------------------------------------------------------------

    }
    //——————————————————————————————————————————————————————————————————————


    //結束對話畫面初始化——————————————————————————————————————————————————————————————————————
    private void EndDialogueInitialization()
    {
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        //文字初始化
        _Dialogue_Text_ClassArray[0].Name.alignment = TextAlignmentOptions.TopLeft;
        _Dialogue_Text_ClassArray[0].FeedBack.sizeDelta = new Vector2(_Dialogue_Text_ClassArray[0].FeedBack.sizeDelta.x, 0);
        _Dialogue_Text_ClassArray[0].Summary.alignment = TextAlignmentOptions.TopLeft;
        _Dialogue_Text_ClassArray[0].Name.text = "";
        _Dialogue_Text_ClassArray[0].Summary.text = "";
        _Dialogue_Text_ClassArray[1].Name.alignment = TextAlignmentOptions.TopLeft;
        _Dialogue_Text_ClassArray[1].FeedBack.sizeDelta = new Vector2(_Dialogue_Text_ClassArray[1].FeedBack.sizeDelta.x, 0);
        _Dialogue_Text_ClassArray[1].Summary.alignment = TextAlignmentOptions.TopLeft;
        _Dialogue_Text_ClassArray[1].Name.text = "";
        _Dialogue_Text_ClassArray[1].Summary.text = "";
        //圖片初始化
        _Figure_LeftOffset_Transform.localPosition = new Vector3(0, 330, 0);
        _Figure_LeftImage_Image.color = Color.white;
        _Figure_LeftImage_Image.sprite = _View_Manager.GetSprite("Creature", "null", "null");
        _Figure_RightOffset_Transform.localPosition = new Vector3(0, 330, 0);
        _Figure_RightImage_Image.color = Color.white;
        _Figure_RightImage_Image.sprite = _View_Manager.GetSprite("Creature", "null", "null");
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Dialogue

    #region CampTalk
    //——————————————————————————————————————————————————————————————————————
    private string _Dialogue_CampKey_String;
    private string _Dialogue_CampSubKey_String;
    private string _Dialogue_CampType_String;

    public void DialogueCampAdd(bool Default, string Type, string Key)
    {
        //----------------------------------------------------------------------------------------------------
        Dictionary<string, List<string>> QuickSave_Dialogue_Dictionary = _Dialogue_DialogueCamp_Dictionary;
        if (Default)
        {
            QuickSave_Dialogue_Dictionary = _Dialogue_DialogueCampDefault_Dictionary;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Dialogue_Dictionary.TryGetValue(Type, out List<string> Keys))
        {
            QuickSave_Dialogue_Dictionary[Type].Add(Key);
        }
        else
        {
            QuickSave_Dialogue_Dictionary.Add(Type, new List<string> { Key });
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void DialogueCampRemove(bool Default, string Type, string Key)
    {
        //----------------------------------------------------------------------------------------------------
        Dictionary<string, List<string>> QuickSave_Dialogue_Dictionary = _Dialogue_DialogueCamp_Dictionary;
        if (Default)
        {
            QuickSave_Dialogue_Dictionary = _Dialogue_DialogueCampDefault_Dictionary;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Key)
        {
            case "Clear":
                {
                    QuickSave_Dialogue_Dictionary.Remove(Type);
                }
                break;
            default:
                {
                    if (QuickSave_Dialogue_Dictionary.TryGetValue(Type, out List<string> Keys))
                    {
                        QuickSave_Dialogue_Dictionary[Type].Remove(Key);
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void DialogueCamp(string Type)
    {
        //----------------------------------------------------------------------------------------------------
        /*
        //地區
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;*/
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (_Dialogue_DialogueCamp_Dictionary.TryGetValue(Type, out List<string> Keys))
        {
            if (Keys.Count > 0)
            {
                //特殊向
                string QuickSave_Key_String = Keys[0];
                _Dialogue_CampSubKey_String = QuickSave_Key_String;
                switch (Keys[0])
                {
                    //預設對話
                    case "Default":
                        {
                            if (_Dialogue_DialogueCampDefault_Dictionary.TryGetValue(Type, out List<string> DefaultKeys))
                            {
                                QuickSave_Key_String =
                                    DefaultKeys[Random.Range(0, DefaultKeys.Count)];
                            }
                        }
                        break;
                }
                //開始對話
                _Dialogue_CampKey_String = QuickSave_Key_String;
                _Dialogue_CampType_String = Type;
                if (_Language_DialogueCamp_Dictionary.TryGetValue(QuickSave_Key_String, out List<string> Dialogues))
                {
                    StartDialogue(Dialogues, "Camp");
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void Key_CampSet()
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Dialogue_CampKey_String)
        {
            case "Default":
            case "Camp_Teaching_LockAdventure":
            case "Camp_Teaching_LockInventory":
            case "Camp_Teaching_LockAlchemy":
                {
                    //不移除對話
                    _UI_Manager._UI_Camp_Class._UI_CampState_String = "Null";
                }
                break;
            case "Camp_Teaching_Miio_00":
                {
                    //說明
                    _Dialogue_DialogueCamp_Dictionary[_Dialogue_CampType_String].RemoveAt(0);
                    DialogueCampAdd(false, "Miio", "Camp_Teaching_Miio_01");
                    _UI_Manager._UI_Camp_Class._UI_CampState_String = "Null";
                }
                break;
            case "Camp_Teaching_Miio_01":
                {
                    //說明
                    _Dialogue_DialogueCamp_Dictionary[_Dialogue_CampType_String].RemoveAt(0);
                    DialogueCampRemove(false, "Inventory", "Camp_Teaching_LockInventory");
                    DialogueCampAdd(false, "Inventory", "Camp_Teaching_Inventory_00");

                    DialogueCampAdd(false, "Miio", "Default");
                    DialogueCampAdd(true, "Miio", "Camp_Default_Miio_00");
                    DialogueCampAdd(true, "Miio", "Camp_Default_Miio_01");
                    DialogueCampAdd(true, "Miio", "Camp_Default_Miio_02");
                    DialogueCampAdd(true, "Miio", "Camp_Default_Miio_03");
                    DialogueCampAdd(true, "Miio", "Camp_Default_Miio_04");
                    DialogueCampAdd(true, "Miio", "Camp_Default_Miio_05");
                    DialogueCampAdd(true, "Miio", "Camp_Default_Miio_06");
                    DialogueCampAdd(true, "Miio", "Camp_Default_Miio_07");
                    _UI_Manager._UI_Camp_Class._UI_CampState_String = "Null";                    
                }
                break;
            case "Camp_Teaching_Inventory_00":
                {
                    //開啟道具欄
                    _Dialogue_DialogueCamp_Dictionary[_Dialogue_CampType_String].RemoveAt(0);
                    DialogueCampRemove(false, "Alchemy", "Camp_Teaching_LockAlchemy");
                    DialogueCampAdd(false, "Alchemy", "Camp_Teaching_Alchemy_00");
                    _UI_Manager._UI_Camp_Class.SummaryTransforms[3].gameObject.SetActive(true);
                    _UI_Manager.UISet(_UI_Manager._UI_Camp_Class._View_Inventory.Inventory_InventorySave_String);
                }
                break;
            case "Camp_Teaching_Alchemy_00":
                {
                    //開啟鍊金台
                    _Dialogue_DialogueCamp_Dictionary[_Dialogue_CampType_String].RemoveAt(0);
                    DialogueCampRemove(false, "Adventure", "Camp_Teaching_LockAdventure");
                    _UI_Manager._UI_Camp_Class.SummaryTransforms[4].gameObject.SetActive(true);
                    if (_UI_Manager._UI_Camp_Class._View_Alchemy.Alchemy_AlchemySave_String != "Alchemy")
                    {
                        _UI_Manager._UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[4].gameObject.SetActive(true);
                        _UI_Manager.UISet(_UI_Manager._UI_Camp_Class._View_Alchemy.Alchemy_AlchemySave_String);
                    }
                }
                break;
            default:
                {
                    switch (_Dialogue_CampSubKey_String)
                    {
                        case "Default":
                            {
                                //預設對話不刪除
                            }
                            break;
                        default:
                            {
                                _Dialogue_DialogueCamp_Dictionary[_Dialogue_CampType_String].RemoveAt(0);
                            }
                            break;
                    }
                    _UI_Manager._UI_Camp_Class._UI_CampState_String = "Null";
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion CampTalk

    #region Event
    #region Event
    //暫存區——————————————————————————————————————————————————————————————————————
    //事件----------------------------------------------------------------------------------------------------
    public Vector _Event_NowVector_Class;
    public bool _Event_Selected_Bool = false;

    public string _Event_NowEventKey_String;//現在事件編號
    public string _Event_SelectEventKey_String = "";//當前選擇編號

    private string _Event_Cycle_String;//選擇處存
    private List<string> _Event_CycleSelects_StringList = new List<string>();//變動選項群
    private List<string> _Event_RemovedCycleSelects_StringList = new List<string>();//已刪除選項群(防止觸發且不在內的事件再次觸發)
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    #region - Time -
    public bool TimeSet(string Type)//true = 有事件發生/false = 無事件發生
    {
        //----------------------------------------------------------------------------------------------------
        bool Answer_Return_Bool = false;
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Syndrome":
                {
                    int QuickSave_SyndromeTime_Int =
                        _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script.
                        _Item_EquipConcepts_Script.Key_SyndromeTime();
                    int QuickSave_SyndromeTimes_Int = _Event_TimePoolTimes_Dictionary[Type] + 1;
                    int QuickSave_TimePinch_Int = (_Time_Time_Int - (QuickSave_SyndromeTime_Int * QuickSave_SyndromeTimes_Int));
                    float QuickSave_SyndromeBarScale_Float = QuickSave_TimePinch_Int / QuickSave_SyndromeTime_Int;
                    if (QuickSave_SyndromeBarScale_Float >= 1)
                    {
                        Answer_Return_Bool = true;
                        QuickSave_SyndromeBarScale_Float -= 1;
                        _Event_TimePoolTimes_Dictionary[Type] += 1;
                    }
                    //_UI_Manager._UI_Time_Class.BarValue.text = QuickSave_TimePinch_Int.ToString("0");
                    //_UI_Manager._UI_Time_Class.BarTransform.localScale = new Vector3(QuickSave_SyndromeBarScale_Float, 1, 1);
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Bool;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Finding -
    //事件觸發(時間事件/地塊事件)——————————————————————————————————————————————————————————————————————
    public void EventFinding(Vector Coordinate,int Time, bool IgnoreGroundEvent)
    {
        //事件類別判定----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        _Map_FieldCreator _Map_FieldCreator = _World_Manager._Map_Manager._Map_FieldCreator;
        _Map_Manager.FieldDataClass QuickSave_FieldData_Class = 
            _Map_FieldCreator._Map_Data_Dictionary[QuickSave_Map_String];
        _Map_Manager.GroundDataClass QuickSave_GroundData_Class =
            QuickSave_FieldData_Class.Data[Coordinate.Vector2Int];
        _Object_CreatureUnit QuickSave_Creature_Script =
            _World_Manager._Object_Manager._Object_Player_Script;
        //----------------------------------------------------------------------------------------------------

        //時間事件----------------------------------------------------------------------------------------------------
        _Time_Time_Int += Time;
        if (TimeSet("Syndrome"))
        {
            //變異事件
            List<string> QuickSave_SyndromeEventList_StringList = new List<string>();
            string QuickSave_Region_String =
                QuickSave_GroundData_Class.Region;
            if (QuickSave_FieldData_Class.TimeEvent["All"].
                TryGetValue("Syndrome", out List<string> AllSyndrome))
            {
                QuickSave_SyndromeEventList_StringList.AddRange(AllSyndrome);
            }
            if (QuickSave_FieldData_Class.TimeEvent[QuickSave_Region_String].
                TryGetValue("Syndrome", out List<string> RegionSyndrome))
            {
                QuickSave_SyndromeEventList_StringList.AddRange(RegionSyndrome);
            }
            StartEventDialogue(
                QuickSave_SyndromeEventList_StringList[
                    Random.Range(0, QuickSave_SyndromeEventList_StringList.Count)], "Event");
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //地塊事件----------------------------------------------------------------------------------------------------
        _Event_NowVector_Class = Coordinate;
        if (!IgnoreGroundEvent)
        {
            string QuickSave_Event_String =
                QuickSave_GroundData_Class.Event;
            if (QuickSave_Event_String != "Null")
            {
                //有事件
                StartEventDialogue(QuickSave_Event_String, "Event");
            }
            else
            {
                //無事件
                QuickSave_Creature_Script.BuildFront();
            }
        }
        else
        {
            //無事件
            QuickSave_Creature_Script.BuildFront();
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region StartDialogue -
    //呼叫對話——————————————————————————————————————————————————————————————————————
    public void StartEventDialogue(string Key, string Type)
    {
        _Event_NowEventKey_String = Key;
        _Event_PlayingEvent_Class = _Data_Event_Dictionary[_Event_NowEventKey_String];
        _Event_SelectEventKey_String = "";
        if (_Language_DialogueEvent_Dictionary.TryGetValue(Key, out List<string> DicValue))
        {
            if (_Event_Played_HashSet.Contains(Key))
            {//已執行
                Key_EventSet();
            }
            else
            {//尚未執行過
                _Event_Played_HashSet.Add(Key);
                StartDialogue(DicValue, Type);
            }
        }
        else
        {
            Key_EventSet();
        }
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - EventContinue -
    //事件開始——————————————————————————————————————————————————————————————————————
    public void EventContinue()//Select/End
    {
        //內容設置----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _UI_CardManager _UI_CardManager = _UI_Manager._UI_CardManager;
        _Object_CreatureUnit QuickSave_Creature_Script = _World_Manager._Object_Manager._Object_Player_Script;

        //調整位置
        _UI_Manager._UI_Event_Transform.gameObject.SetActive(true);
        _View_BackDeco_Transform.gameObject.SetActive(true);
        _View_PlayerStatus_Transform.localPosition = new Vector3(0,275,0);

        switch (_Event_PlayingEvent_Class.Type)
        {
            case "Normal":
            case "Replace":
            case "Cycle":
                {
                    List<string> QuickSave_Selects_StringList = null;
                    if (_Event_NowEventKey_String == _Event_Cycle_String)
                    {
                        QuickSave_Selects_StringList = _Event_CycleSelects_StringList;
                    }
                    else
                    {
                        QuickSave_Selects_StringList = _Event_PlayingEvent_Class.SelectKey;
                        if (_Event_PlayingEvent_Class.Type == "Cycle")
                        {
                            _Event_Cycle_String = _Event_NowEventKey_String;
                            _Event_CycleSelects_StringList = new List<string>(QuickSave_Selects_StringList);
                            _Event_RemovedCycleSelects_StringList.Clear();
                        }
                    }
                    //有選項
                    //調整選項
                    List<_UI_Card_Unit> QuickSave_Selects_ScriptsList = new List<_UI_Card_Unit>();
                    for (int a = 0; a < QuickSave_Selects_StringList.Count; a++)
                    {
                        QuickSave_Selects_ScriptsList.Add(TakeSelectDeQueue(QuickSave_Selects_StringList[a]));
                    }
                    _UI_CardManager.CardsMove("Select_Add_Board", QuickSave_Creature_Script, QuickSave_Selects_ScriptsList,
                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    //回合進程
                    _World_Manager._Map_Manager.FieldStateSet("EventSelect", "對話結束，事件顯示選擇");
                    _UI_CardManager.BoardRefresh(QuickSave_Creature_Script);
                }
                break;
            default:
                {
                    //無選項
                    //回合進程
                    _World_Manager._Map_Manager.FieldStateSet("EventFrame", "對話結束，事件顯示");
                    _Event_Selected_Bool = true;
                }
                break;
        }
        EventFrameSet("Description", _Event_NowEventKey_String, _Language_Event_Dictionary[_Event_NowEventKey_String]);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - EventSelect -
    #region - Queue -
    //——————————————————————————————————————————————————————————————————————
    public _UI_Card_Unit TakeSelectDeQueue(string Key)//拿出
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Card_Unit Answer_Return_Script = null;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        _Object_CreatureUnit QuickSave_Creature_Script = _World_Manager._Object_Manager._Object_Player_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (_Event_SelectPools_ScriptQueue.Count > 0)
        {
            Answer_Return_Script = _Event_SelectPools_ScriptQueue.Dequeue();
        }
        else
        {
            Answer_Return_Script = 
                Instantiate(_Event_Select_GameObject, _Event_SelectStore_Transform).GetComponent<_UI_Card_Unit>();
            Answer_Return_Script._Card_ExploreUnit_Script = Answer_Return_Script.gameObject.AddComponent<_Skill_ExploreUnit>();
            Answer_Return_Script._Card_ExploreUnit_Script._Owner_Card_Script = Answer_Return_Script;
        }

        //程式
        Answer_Return_Script.name = "Card_Unit";
        Answer_Return_Script._Basic_Key_String = Key;
        EventDataClass QuickSave_EventData_Class = _Data_Event_Dictionary[Key];
        _Skill_Manager.ExploreDataClass QuickSave_ExploreData_Class = new _Skill_Manager.ExploreDataClass();
        QuickSave_ExploreData_Class.UseTag = QuickSave_EventData_Class.UseTag;
        QuickSave_ExploreData_Class.OwnTag = QuickSave_EventData_Class.OwnTag;
        QuickSave_ExploreData_Class.Numbers = QuickSave_EventData_Class.Numbers;
        QuickSave_ExploreData_Class.Keys = QuickSave_EventData_Class.Keys;
        SourceClass QuickSave_Source_Class = new SourceClass
        {
            SourceType = "Card",
            Source_Creature = QuickSave_Creature_Script,
            Source_BattleObject = QuickSave_Creature_Script._Basic_Object_Script,
            Source_Card = Answer_Return_Script,
            Source_NumbersData = QuickSave_ExploreData_Class.Numbers,
            Source_KeysData = QuickSave_ExploreData_Class.Keys
        };
        Answer_Return_Script._Basic_Source_Class = QuickSave_Source_Class;
        Answer_Return_Script._Card_ExploreUnit_Script._Basic_Source_Class = QuickSave_Source_Class;
        Answer_Return_Script._Card_ExploreUnit_Script._Basic_Data_Class = QuickSave_ExploreData_Class;
        Answer_Return_Script._Card_ExploreUnit_Script._Basic_Language_Class = _Language_Event_Dictionary[Key];
        Answer_Return_Script.gameObject.SetActive(true);
        _Event_Selects_Dictionary.Add(Key, Answer_Return_Script);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Script;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void TakeSelectEnQueue(_UI_Card_Unit Select)//收回
    {
        //----------------------------------------------------------------------------------------------------
        Select.transform.SetParent(_Event_SelectStore_Transform);
        Select._Basic_View_Script.MouseOverOut("Explore");
        Select._Basic_View_Script._View_Name_Text.material = null;
        Select.transform.localPosition = Vector3.zero;
        Select.transform.localScale = Vector3.zero;
        Select.gameObject.SetActive(false);
        _Event_SelectPools_ScriptQueue.Enqueue(Select);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    //事件進行﹝條件判斷區﹞——————————————————————————————————————————————————————————————————————
    //當前所選擇選項
    public void EventSelect(_UI_Card_Unit Select)
    {
        //顯示選擇內容----------------------------------------------------------------------------------------------------
        string QuickSave_Type_String = "";
        if (Select != null && 
            Select != _Event_SelectCard_Script)
        {
            QuickSave_Type_String = "Select";
            _Event_SelectCard_Script = Select;
            _Event_Selected_Bool = true;
            _Event_SelectEventKey_String = Select._Basic_Key_String;
            if (Select._Card_ExploreUnit_Script._Basic_Key_String != null)//為卡片Plot
            {
                _Event_SelectEventKey_String = 
                    Select._Card_ExploreUnit_Script.Key_Event(_Event_NowEventKey_String);
            }
            _World_Manager._Map_Manager.FieldStateSet("EventFrame", "選擇事件完成，顯示選擇說明");
        }
        else
        {
            QuickSave_Type_String = "Description";
            _Event_SelectCard_Script = null;
            _Event_Selected_Bool = false;
            //改變選擇鍵
            _Event_SelectEventKey_String = _Event_NowEventKey_String;
            //回合進程
            _World_Manager._Map_Manager.FieldStateSet("EventSelect", "取消選擇事件，顯示一般說明");
        }
        //顯示
        EventFrameSet(QuickSave_Type_String, _Event_SelectEventKey_String, _Language_Event_Dictionary[_Event_SelectEventKey_String]);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion


    #region EndEvent -
    //事件結束﹝成果回報區﹞——————————————————————————————————————————————————————————————————————
    public void EndEvent()
    {
        //回合進程----------------------------------------------------------------------------------------------------
        _World_Manager._Map_Manager.FieldStateSet("EventBack", "事件確認，進行效果實行");
        //----------------------------------------------------------------------------------------------------

        //離開動畫----------------------------------------------------------------------------------------------------
        //事件眶離開
        _World_Manager._UI_Manager._UI_Event_Transform.gameObject.SetActive(false);
        _View_BackDeco_Transform.gameObject.SetActive(false);
        _View_PlayerStatus_Transform.localPosition = Vector3.zero;
        //----------------------------------------------------------------------------------------------------

        //事件結束----------------------------------------------------------------------------------------------------
        Key_EventPass();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - EventEndSet -
    //事件完結設置——————————————————————————————————————————————————————————————————————
    public void EventEndSet()
    {
        _Event_NowEventKey_String = "";//初始化
        _Event_SelectEventKey_String = "";
        _Event_Cycle_String = "";
        _Event_CycleSelects_StringList.Clear();
        _Event_RemovedCycleSelects_StringList.Clear();
        EventFinding(_Event_NowVector_Class, 0, true);
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - EventFrameSet -
    //事件視覺內容設定——————————————————————————————————————————————————————————————————————
    public void EventFrameSet(string Type, string Key, LanguageClass Language)
    {
        _World_TextManager _World_TextManager = 
            _World_Manager._World_GeneralManager._World_TextManager;
        //調整圖片
        _View_BackImage_Image.sprite =
            _World_Manager._View_Manager.GetSprite("Event", "BackGround", Key);
        //調整內文
        Basic_Source_Class.Source_NumbersData =
            _Data_Event_Dictionary[Key].Numbers;
        Basic_Source_Class.Source_KeysData =
            _Data_Event_Dictionary[Key].Keys;
        switch (Type)
        {
            case "Description":
                _View_Text_Class.Name.text = "<size=20>" +
                    _World_TextManager._Language_UIName_Dictionary["Event"] + ":" +
                    "</size>" + Language.Name;
                _View_Text_Class.Summary.text = _World_TextManager.
                    TextmeshProTranslater("Event", Language.Description, 0, Basic_Source_Class, null, null,
                    _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                ;
                break;
            case "Select":
                _View_Text_Class.Name.text = "<size=20>" +
                    _World_TextManager._Language_UIName_Dictionary["Plot"] + ":" +
                    "</size>" + Language.Name;
                _View_Text_Class.Summary.text = _World_TextManager.
                    TextmeshProTranslater("Event", Language.Select, 0, Basic_Source_Class, null, null,
                    _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                break;
        }

        _View_Text_Class.FeedBack.sizeDelta = new Vector2(_View_Text_Class.FeedBack.sizeDelta.x, 2);

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_View_Text_Class.Layout);
        //按鈕
        _Event_EventSureBTN_Transform.gameObject.SetActive(_Event_Selected_Bool);
        //卡片
        _World_Manager._UI_Manager._UI_CardManager.BoardRefresh(_World_Manager._Object_Manager._Object_Player_Script);
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion Event

    #region KeyAction
    //——————————————————————————————————————————————————————————————————————
    private void SelectSet(List<string> AddenKey ,string RemoveKey)//如果Remove有需要複數再改
    {
        //----------------------------------------------------------------------------------------------------
        int QuickSave_Pos_Int = 0;
        //----------------------------------------------------------------------------------------------------
        if (RemoveKey != null)
        {
            QuickSave_Pos_Int = _Event_CycleSelects_StringList.IndexOf(RemoveKey);
            _Event_CycleSelects_StringList.Remove(RemoveKey);
            _Event_RemovedCycleSelects_StringList.Add(RemoveKey);
        }
        if (AddenKey != null)
        {
            for (int a = AddenKey.Count - 1; a >= 0; a--)
            {
                if (!_Event_CycleSelects_StringList.Contains(AddenKey[a]) && !_Event_RemovedCycleSelects_StringList.Contains(AddenKey[a]))
                {
                    _Event_CycleSelects_StringList.Insert(QuickSave_Pos_Int, AddenKey[a]);
                }
            }
        }
    }
    //——————————————————————————————————————————————————————————————————————

    //事件結束——————————————————————————————————————————————————————————————————————
    public void Key_EventSet()
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Object_CreatureUnit QuickSave_Creature_Script = 
            _World_Manager._Object_Manager._Object_Player_Script;
        _Map_BattleObjectUnit QuickSave_ConceptObject_Script =
            QuickSave_Creature_Script._Object_Inventory_Script._Item_EquipConcepts_Script._Basic_Object_Script;
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //事件獎勵----------------------------------------------------------------------------------------------------
        switch (_Event_NowEventKey_String)
        {
            #region - Get -
            case "Event_Gap_RebirthCenter_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MetalScrap", UnityEngine.Random.Range(1, 3), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_Rags", UnityEngine.Random.Range(0, 3), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GMFCloth", UnityEngine.Random.Range(0, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_RebirthCenter_Search_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_RottenPlanks", UnityEngine.Random.Range(1, 3), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MetalScrap", UnityEngine.Random.Range(1, 3), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_Rags", UnityEngine.Random.Range(1, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_StoneBall_Search_GreatSuccess":
                {
                    //解鎖配方
                    _Item_Manager.RecipeStartSet("Weapon", "WeaponRecipe_GatherCobble");
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GatherStone", UnityEngine.Random.Range(3, 6), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(15, 40),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_StoneBall_Search_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GatherStone", UnityEngine.Random.Range(1, 4), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_WallShale_Search_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_Shale", UnityEngine.Random.Range(1, 5), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_WhitalOreVein_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ScarletResidue", UnityEngine.Random.Range(1, 4), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GMFCloth", UnityEngine.Random.Range(0, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_WhitalOreVein_Search_WhitalOre_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_WhitalOre", UnityEngine.Random.Range(1, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_WhitalOreVein_Search_MixedSand_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MixedSand", UnityEngine.Random.Range(3, 6), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_WhitalOreVein_MineBlasting_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_WhitalOre", UnityEngine.Random.Range(6, 11), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_ComplexVein_Search_MixedSand_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MixedSand", UnityEngine.Random.Range(2, 4), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_ComplexVein_MineBlasting_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_Shale", UnityEngine.Random.Range(2, 4), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_WhitalOre", UnityEngine.Random.Range(1, 4), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_LanspidOre", UnityEngine.Random.Range(1, 4), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_BlaodOre", UnityEngine.Random.Range(0, 3), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GreceOre", UnityEngine.Random.Range(0, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_Warehouse_Search_WhitalOre_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_WhitalOre", UnityEngine.Random.Range(1, 5), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_Warehouse_Search_LanspidOre_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_WhitalOre", UnityEngine.Random.Range(1, 4), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_Warehouse_Search_BlaodOre_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_WhitalOre", UnityEngine.Random.Range(1, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_Warehouse_Search_GreceOre_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_WhitalOre", UnityEngine.Random.Range(1, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_AlgaBetweenBricks_Search_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MixedAlga", UnityEngine.Random.Range(2, 6), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_AlgaBetweenBricks_Search_Failure":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MixedAlga", UnityEngine.Random.Range(1, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_CurtainPlants_Search_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_DropleafKelp", UnityEngine.Random.Range(2, 5), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_FluorescentCluster_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_AdaptationReceptor", UnityEngine.Random.Range(1, 4), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GlosporeMossSeed", UnityEngine.Random.Range(0, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_FluorescentCluster_Search_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GlosporeMossSeed", UnityEngine.Random.Range(3, 6), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_FluorescentCluster_Search_Failure":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GlosporeMossSeed", UnityEngine.Random.Range(1, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_UndergroundFarm_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ScarletResidue", UnityEngine.Random.Range(2, 5), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_Rags", UnityEngine.Random.Range(2, 5), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GMFCloth", UnityEngine.Random.Range(0, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_UndergroundFarm_Search_DropleafKelp_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_DropleafKelp", UnityEngine.Random.Range(3, 6), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_UndergroundFarm_Search_DropleafKelp_Failure":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_DropleafKelp", UnityEngine.Random.Range(1, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_UndergroundFarm_Search_GlobularMossFluff_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GlobularMossFluff", UnityEngine.Random.Range(3, 6), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_UndergroundFarm_Search_GlobularMossFluff_Failure":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GlobularMossFluff", UnityEngine.Random.Range(1, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_UndergroundFarm_Search_GreenBerry_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GreenBerry", UnityEngine.Random.Range(3, 6), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_UndergroundFarm_Search_GreenBerry_Failure":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GreenBerry", UnityEngine.Random.Range(1, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_QuenchingPuddle_Search_MixedSand_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MixedSand", UnityEngine.Random.Range(3, 6), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_QuenchingPuddle_Search_MixedSand_Failure":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MixedSand", UnityEngine.Random.Range(1, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_QuenchingPuddle_Search_SedamicMud_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_SedamicMud", UnityEngine.Random.Range(2, 5), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_QuenchingPuddle_Search_SedamicMud_Failure":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_SedamicMud", UnityEngine.Random.Range(1, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_QuenchingPuddle_Search_ClearWater_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ClearWater", UnityEngine.Random.Range(2, 5), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_QuenchingPuddle_Search_ClearWater_Failure":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ClearWater", UnityEngine.Random.Range(1, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_AncientBones_Search_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_AlbinoCorone", UnityEngine.Random.Range(2, 4), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_AncientBones_Search_Failure":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_AlbinoCorone", UnityEngine.Random.Range(1, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_LazyGuard_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ScarletResidue", UnityEngine.Random.Range(1, 2), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MetalScrap", UnityEngine.Random.Range(0, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_PatrolJailer_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ScarletResidue", UnityEngine.Random.Range(2, 5), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MetalScrap", UnityEngine.Random.Range(1, 4), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_Rags", UnityEngine.Random.Range(1, 4), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_ActionAndroid_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ScarletResidue", UnityEngine.Random.Range(0, 3), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MetalScrap", UnityEngine.Random.Range(1, 6), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GMFCloth", UnityEngine.Random.Range(1, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_PunishmentTeam_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ScarletResidue", UnityEngine.Random.Range(2, 5), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MetalScrap", UnityEngine.Random.Range(1, 4), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_RottenPlanks", UnityEngine.Random.Range(1, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_PunishmentTeam_Plunder_Failure":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ScarletResidue", UnityEngine.Random.Range(1, 2), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_RottenPlanks", UnityEngine.Random.Range(1, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_SleagReunion_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_AdaptationReceptor", UnityEngine.Random.Range(3, 6), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GlosporeMossSeed", UnityEngine.Random.Range(0, 5), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_CornerSight_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ArthropodMucus", UnityEngine.Random.Range(1, 4), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_FrangibleCarapace", UnityEngine.Random.Range(0, 3), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_SyndromeResidue", UnityEngine.Random.Range(0, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_DiningStation_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_GMFCloth", UnityEngine.Random.Range(1, 4), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ScarletResidue", UnityEngine.Random.Range(0, 3), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MetalScrap", UnityEngine.Random.Range(0, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_HiddenFangs_InvisibleWeapon":
                {
                    //解鎖配方
                    _Item_Manager.RecipeStartSet("Weapon", "WeaponRecipe_LanspidDagger");
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_LanspidOre", UnityEngine.Random.Range(1, 5), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_RottenTachi_Search_Success":
                {
                    //解鎖配方
                    _Item_Manager.RecipeStartSet("Weapon", "WeaponRecipe_FlashingTachi");
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_RottenPlanks", UnityEngine.Random.Range(1, 2), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_EasyWeapon_Search_Success":
                {
                    //解鎖配方
                    _Item_Manager.RecipeStartSet("Weapon", "WeaponRecipe_HeavySling");
                    //獲得道具
                    _Object_Manager.CustomRecipeMakeClass QuickSave_Recipe_Class = new _Object_Manager.CustomRecipeMakeClass
                    {
                        Target = "WeaponRecipe_HeavySling",
                        MaterialKey = new List<string> { "Material_OverloadDust" },
                        MaterialStatus = new List<string> { "10,3,16,2" },
                        MaterialSpecialAffix = new List<string[]> { new string[4] },
                        Process = new List<KeyValuePair<string, int>> { new KeyValuePair<string, int>("Stark", 50) }
                    };
                    _Item_Manager.WeaponStartSet(QuickSave_Creature_Script,
                        "Weapon_HeavySling", 1, true, CustomMake: QuickSave_Recipe_Class);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_EasyWeapon_Search_Failure":
                {
                    //解鎖配方
                    _Item_Manager.RecipeStartSet("Weapon", "WeaponRecipe_HeavySling");
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_Rags", UnityEngine.Random.Range(1, 3), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(5, 20),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_FleeingInsects_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_ArthropodMucus", UnityEngine.Random.Range(2, 7), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_SyndromeResidue", UnityEngine.Random.Range(1, 5), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(10, 30),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_MinerAndroid_Plunder_Success":
                {
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_MetalScrap", UnityEngine.Random.Range(3, 13), true);
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_RefinedWhitalIngot", UnityEngine.Random.Range(3, 9), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                    //獲得塵晶
                    _Item_Manager.DustSet("Add", UnityEngine.Random.Range(30, 60),
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;

            case "Event_Gap_BlackMistDevour_Explore_Success":
                {
                    //解鎖配方
                    _Item_Manager.RecipeStartSet("Weapon", "WeaponRecipe_OrganicGauntlets");
                    //獲得道具
                    _Item_Manager.MaterialStartSet(
                        QuickSave_Creature_Script, "Material_NecroticTissue", UnityEngine.Random.Range(3, 6), true);
                    _UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            #endregion

            #region - Syndrome -
            case "Event_TimingSyndrome_Bear_Success":
                {
                    //變異池新增3選1
                    SyndromePoolSelect("Add", _Event_NowVector_Class);
                    return;//中斷
                }
                break;
            case "Event_TimingSyndrome_Bear_Failure":
            case "Event_TimingSyndrome_Eliminate_Failure":
                {
                    //變異池
                    SyndromePoolSet("Add", "Random", _Event_NowVector_Class);
                }
                break;
            case "Event_TimingSyndrome_Eliminate_Success":
                {
                    //變異池移除3選1
                    SyndromePoolSelect("Remove", _Event_NowVector_Class);
                    return;//中斷
                }
                break;
            #endregion

            #region - Recover -
            case "Event_Gap_FreshBerry_Taste_Success":
            case "Event_Gap_DiningStation_Taste_Failure":
                {
                    //治癒
                    string QuickSave_ValueKey_String =
                        "HealNumber_MediumPoint_Type0｜Target_Concept_Default_Point_MediumPoint_Total_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, QuickSave_ConceptObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Event_PlayingEvent_Class.Numbers[QuickSave_Key_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, QuickSave_ConceptObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_ValueKey02_String =
                        "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, QuickSave_ConceptObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Event_PlayingEvent_Class.Numbers[QuickSave_Key02_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, QuickSave_ConceptObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    QuickSave_ConceptObject_Script.PointSet(
                        "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                        Basic_Source_Class, QuickSave_ConceptObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            case "Event_Gap_DiningStation_Taste_Success":
                {
                    //解鎖配方
                    _Item_Manager.RecipeStartSet("Item", "ItemRecipe_GlosporeBeverage");
                    //獲得道具
                    _Object_Manager.CustomRecipeMakeClass QuickSave_Recipe_Class = new _Object_Manager.CustomRecipeMakeClass
                    {
                        Target = "ItemRecipe_GlosporeBeverage",
                        MaterialKey = new List<string> { "Material_OverloadDust" },
                        MaterialStatus = new List<string> { "5,6,3,10" },
                        MaterialSpecialAffix = new List<string[]> { new string[4] },
                        Process = new List<KeyValuePair<string, int>> { new KeyValuePair<string, int>("Stark", 50) }
                    };
                    _Item_Manager.ItemStartSet(QuickSave_Creature_Script,
                        "Item_GlosporeBeverage", 1, true, CustomMake: QuickSave_Recipe_Class);

                    //治癒
                    string QuickSave_ValueKey_String =
                        "HealNumber_MediumPoint_Type0｜Target_Concept_Default_Point_MediumPoint_Total_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, QuickSave_ConceptObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Event_PlayingEvent_Class.Numbers[QuickSave_Key_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, QuickSave_ConceptObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_ValueKey02_String =
                        "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, QuickSave_ConceptObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Event_PlayingEvent_Class.Numbers[QuickSave_Key02_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, QuickSave_ConceptObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    QuickSave_ConceptObject_Script.PointSet(
                        "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                        Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);

                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            #endregion

            #region - Pursuit -
            case "Event_Gap_FreshBerry_Taste_Failure":
            case "Event_Gap_HiddenFangs_ConcealBite":
            case "Event_Gap_BlackMistErupts_ReflexAction_Failure":
            case "Event_Gap_BlackMistDevour_Erosion":
                {
                    //傷害概念
                    QuickSave_ConceptObject_Script.Damaged(
                        Key_Pursuit(_Event_NowEventKey_String, _Event_PlayingEvent_Class), null, 
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    //獲得變異
                    _Item_Manager.GetSyndrome("Random", 1,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class);
                }
                break;
            #endregion

            #region - CycleSet -
            case "Event_Gap_Awaken_Ask_WhoYouAre":
            case "Event_Gap_Awaken_Ask_WhereIsHere":
            case "Event_Gap_Awaken_Ask_WhyIAmHere":
                {
                    SelectSet(null, _Event_NowEventKey_String);//於Cycle增加選項
                }
                break;
            #endregion

            #region - Other -
            case "Event_Gap_Exit_Success":
                {
                    //關閉遊戲
                    Application.Quit();
                }
                break;
                #endregion
        }
        EventContinue();
        //----------------------------------------------------------------------------------------------------
    }
    public void Key_EventPass() 
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Object_CreatureUnit QuickSave_Creature_Script =
            _World_Manager._Object_Manager._Object_Player_Script;
        _Map_BattleObjectUnit QuickSave_ConceptObject_Script =
            QuickSave_Creature_Script._Object_Inventory_Script._Item_EquipConcepts_Script._Basic_Object_Script;
        _UI_CardManager _UI_CardManager = _World_Manager._UI_Manager._UI_CardManager;
        bool QuickSave_SelectUsing_Bool = false;
        string QuickSave_Connect_String = "";
        string QuickSave_Replace_String = "";
        bool QuickSave_EndOut_Bool = false;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //時間增加
        int QuickSave_EventTime_Int = _Data_Event_Dictionary[_Event_NowEventKey_String].TimePass;
        _Time_Time_Int += QuickSave_EventTime_Int;
        //----------------------------------------------------------------------------------------------------

        //死亡判定----------------------------------------------------------------------------------------------------
        if (!_World_Manager._Object_Manager.ObjectRoundSet())
        {
            print("ConceptDead");
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //print(_Event_NowEventKey_String + ":" + _Event_PlayingEvent_Class.Type);
        switch (_Event_PlayingEvent_Class.Type)
        {
            //類型/Normal(普通 選項群)(大部分)、Cycle(循環 保存選群(可增減))、
            //Connect(接續事件)(不允許穿插探索卡時(主線事件等))、Replace(以當前取代事件)、Compare(比較)、
            //End(事件尾(結束事件))

            //SelectSet(new List<string> { "Event_Gap_Awaken_Ask_WhereIsBlueEye" }, _Event_NowEventKey_String);//於Cycle增加選項
            case "Normal":
            case "Cycle":
                {
                    QuickSave_Connect_String = _Event_SelectEventKey_String;
                }
                break;
            case "Connect":
                {
                    QuickSave_Connect_String = _Event_PlayingEvent_Class.ConnectKey;
                }
                break;
            case "Replace":
                {
                    QuickSave_Connect_String = _Event_SelectEventKey_String;
                    QuickSave_Replace_String = _Event_PlayingEvent_Class.ConnectKey;
                }
                break;
            case "Camp":
                {
                    switch (_Event_NowEventKey_String)
                    {
                        case "Event_Teaching_Awaken":
                            {
                                DialogueCampAdd(false, "Adventure", "Camp_Teaching_LockAdventure");
                                DialogueCampAdd(false, "Inventory", "Camp_Teaching_LockInventory");
                                DialogueCampAdd(false, "Alchemy", "Camp_Teaching_LockAlchemy");
                                DialogueCampAdd(false, "Miio", "Camp_Teaching_Miio_00");
                            }
                            break;
                    }
                    QuickSave_Replace_String = _Event_PlayingEvent_Class.SelectKey[0];
                    _World_Manager._World_GeneralManager._World_ScenesManager.SwitchScenes("Camp");
                }
                break;
            case "Battle":
                {
                    switch (_Event_NowEventKey_String)
                    {
                        case "Event_Gap_Exit":
                        case "Event_Gap_RebirthCenter_Battle":
                        case "Event_Gap_WhitalOreVein_Battle":
                        case "Event_Gap_FluorescentCluster_Battle":
                        case "Event_Gap_UndergroundFarm_Battle":
                        case "Event_Gap_LazyGuard_Battle":
                        case "Event_Gap_PatrolJailer_Battle":
                        case "Event_Gap_ActionAndroid_Battle":
                        case "Event_Gap_PunishmentTeam_Battle":
                        case "Event_Gap_SleagReunion_Battle":
                        case "Event_Gap_CornerSight_Battle":
                        case "Event_Gap_DiningStation_Battle":
                        case "Event_Gap_FleeingInsects_Battle":
                        case "Event_Gap_MinerAndroid_Battle":
                            {
                                _Battle_NPCCreateKey_String =
                                    _Event_NowEventKey_String.Replace("Event_", "NPCCreate_");
                            }
                            break;
                        case "Event_Gap_RebirthCenter_Battle_Disadvantages":
                        case "Event_Gap_WhitalOreVein_Battle_Disadvantages":
                        case "Event_Gap_FluorescentCluster_Battle_Disadvantages":
                        case "Event_Gap_UndergroundFarm_Battle_Disadvantages":
                        case "Event_Gap_PatrolJailer_Battle_Disadvantages":
                        case "Event_Gap_ActionAndroid_Battle_Disadvantages":
                        case "Event_Gap_PunishmentTeam_Battle_Disadvantages":
                        case "Event_Gap_DiningStation_Battle_Disadvantages":
                        case "Event_Gap_FleeingInsects_Battle_Disadvantages":
                        case "Event_Gap_MinerAndroid_Battle_Disadvantages":
                            {
                                _Battle_NPCCreateKey_String =
                                    _Event_NowEventKey_String.Replace("Event_", "NPCCreate_");
                            }
                            break;
                        default:
                            {
                                _Battle_NPCCreateKey_String = "NPCCreate_Gap_Test";
                            }
                            break;
                    }
                    QuickSave_Replace_String = _Event_PlayingEvent_Class.SelectKey[0];
                    _World_Manager._World_ScenesManager.SwitchScenes("Battle");
                }
                break;
            case "Compare":
                {
                    switch (_Event_NowEventKey_String)
                    {
                        //Consciousness
                        case "Event_TimingSyndrome_Bear":
                        case "Event_TimingSyndrome_Eliminate":
                            {
                                string QuickSave_ValueKey_String =
                                    "Compare_Consciousness_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Event_PlayingEvent_Class.Numbers[QuickSave_ValueKey_String],
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];
                                float QuickSave_RandomStatus_Float = RandomStatusValue(QuickSave_Type_String);

                                if (!(QuickSave_RandomStatus_Float < QuickSave_Value_Float))
                                {
                                    QuickSave_Connect_String = _Event_NowEventKey_String + "_Success";
                                }
                                else
                                {
                                    QuickSave_Connect_String = _Event_NowEventKey_String + "_Failure";
                                }
                            }
                            break;
                        //Precision
                        case "Event_Gap_CurtainPlants_Search":
                        case "Event_Gap_QuenchingPuddle_Search_ClearWater":

                        case "Event_Gap_BlackMistErupts_ReflexAction":
                            {
                                string QuickSave_ValueKey_String =
                                    "Compare_Precision_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Event_PlayingEvent_Class.Numbers[QuickSave_ValueKey_String],
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];
                                float QuickSave_RandomStatus_Float = RandomStatusValue(QuickSave_Type_String);

                                if (!(QuickSave_RandomStatus_Float < QuickSave_Value_Float))
                                {
                                    QuickSave_Connect_String = _Event_NowEventKey_String + "_Success";
                                }
                                else
                                {
                                    QuickSave_Connect_String = _Event_NowEventKey_String + "_Failure";
                                }
                            }
                            break;

                        //Vitality
                        case "Event_Gap_Awaken_Dullahan_Hide":
                            {
                                string QuickSave_ValueKey_String =
                                    "Compare_Vitality_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Event_PlayingEvent_Class.Numbers[QuickSave_ValueKey_String],
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];
                                float QuickSave_RandomStatus_Float = RandomStatusValue(QuickSave_Type_String);

                                if (!(QuickSave_RandomStatus_Float < QuickSave_Value_Float))
                                {
                                    QuickSave_Connect_String = _Event_NowEventKey_String + "_Success";
                                }
                                else
                                {
                                    QuickSave_Connect_String = _Event_NowEventKey_String + "_Failure";
                                }
                            }
                            break;
                        //Speed
                        case "Event_Gap_RebirthCenter_Escape":
                        case "Event_Gap_WhitalOreVein_Escape":
                        case "Event_Gap_FluorescentCluster_Escape":
                        case "Event_Gap_UndergroundFarm_Escape":
                        case "Event_Gap_PatrolJailer_Escape":
                        case "Event_Gap_ActionAndroid_Escape":
                        case "Event_Gap_PunishmentTeam_Escape":
                        case "Event_Gap_DiningStation_Escape":
                        case "Event_Gap_FleeingInsects_Escape":
                        case "Event_Gap_MinerAndroid_Escape":
                        case "Event_Gap_BlackMistDevour_Escape":

                        case "Event_Gap_FluorescentCluster_ReflexAction":
                            {
                                string QuickSave_ValueKey_String =
                                    "Compare_Speed_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Event_PlayingEvent_Class.Numbers[QuickSave_ValueKey_String],
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];
                                float QuickSave_RandomStatus_Float = RandomStatusValue(QuickSave_Type_String);

                                if (!(QuickSave_RandomStatus_Float < QuickSave_Value_Float))
                                {
                                    QuickSave_Connect_String = _Event_NowEventKey_String + "_Success";
                                }
                                else
                                {
                                    QuickSave_Connect_String = _Event_NowEventKey_String + "_Failure";
                                }
                            }
                            break;
                        //Luck
                        case "Event_Gap_RebirthCenter_Plunder":
                        case "Event_Gap_WhitalOreVein_Plunder":
                        case "Event_Gap_FluorescentCluster_Plunder":
                        case "Event_Gap_UndergroundFarm_Plunder":
                        case "Event_Gap_LazyGuard_Plunder":
                        case "Event_Gap_PatrolJailer_Plunder":
                        case "Event_Gap_ActionAndroid_Plunder":
                        case "Event_Gap_PunishmentTeam_Plunder":
                        case "Event_Gap_SleagReunion_Plunder":
                        case "Event_Gap_CornerSight_Plunder":
                        case "Event_Gap_DiningStation_Plunder":
                        case "Event_Gap_FleeingInsects_Plunder":
                        case "Event_Gap_MinerAndroid_Plunder":

                        case "Event_Gap_RebirthCenter_Search":
                        case "Event_Gap_WallShale_Search":
                        case "Event_Gap_WhitalOreVein_Search_WhitalOre":
                        case "Event_Gap_Warehouse_Search_WhitalOre":
                        case "Event_Gap_Warehouse_Search_LanspidOre":
                        case "Event_Gap_Warehouse_Search_BlaodOre":
                        case "Event_Gap_Warehouse_Search_GreceOre":
                        case "Event_Gap_AlgaBetweenBricks_Search":
                        case "Event_Gap_FluorescentCluster_Search":
                        case "Event_Gap_UndergroundFarm_Search_DropleafKelp":
                        case "Event_Gap_UndergroundFarm_Search_GlobularMossFluff":
                        case "Event_Gap_UndergroundFarm_Search_GreenBerry":
                        case "Event_Gap_QuenchingPuddle_Search_MixedSand":
                        case "Event_Gap_QuenchingPuddle_Search_SedamicMud":
                        case "Event_Gap_AncientBones_Search":
                        case "Event_Gap_EasyWeapon_Search":

                        case "Event_Gap_FreshBerry_Taste":
                        case "Event_Gap_DiningStation_Taste":
                        case "Event_Gap_BlackMistDevour_Explore":
                            {
                                string QuickSave_ValueKey_String =
                                    "Compare_Luck_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Event_PlayingEvent_Class.Numbers[QuickSave_ValueKey_String],
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];
                                float QuickSave_RandomStatus_Float = RandomStatusValue(QuickSave_Type_String);

                                if (!(QuickSave_RandomStatus_Float < QuickSave_Value_Float))
                                {
                                    QuickSave_Connect_String = _Event_NowEventKey_String + "_Success";
                                }
                                else
                                {
                                    QuickSave_Connect_String = _Event_NowEventKey_String + "_Failure";
                                }
                            }
                            break;
                        //包含大成功
                        case "Event_Gap_StoneBall_Search":
                            {
                                string QuickSave_ValueKey_String =
                                    "Compare_Precision_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Event_PlayingEvent_Class.Numbers[QuickSave_ValueKey_String],
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];
                                float QuickSave_RandomStatus_Float = RandomStatusValue(QuickSave_Type_String);

                                if (!(QuickSave_RandomStatus_Float < QuickSave_Value_Float))
                                {
                                    QuickSave_Connect_String = _Event_NowEventKey_String + "_GreatSuccess";
                                }
                                else
                                {
                                    QuickSave_ValueKey_String =
                                        "Compare_Precision_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                    QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Event_PlayingEvent_Class.Numbers[QuickSave_ValueKey_String],
                                        QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                    QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];
                                    QuickSave_RandomStatus_Float = RandomStatusValue(QuickSave_Type_String);

                                    if (!(QuickSave_RandomStatus_Float < QuickSave_Value_Float))
                                    {
                                        QuickSave_Connect_String = _Event_NowEventKey_String + "_Success";
                                    }
                                    else
                                    {
                                        QuickSave_Connect_String = _Event_NowEventKey_String + "_Failure";
                                    }
                                }
                            }
                            break;
                        //必定成功
                        case "Event_Gap_WhitalOreVein_Search_MixedSand":
                        case "Event_Gap_WhitalOreVein_MineBlasting":
                        case "Event_Gap_ComplexVein_Search_MixedSand":
                        case "Event_Gap_ComplexVein_MineBlasting":
                        case "Event_Gap_FluorescentCluster_HidenThreaten_Harden":
                        case "Event_Gap_RottenTachi_Search":
                        case "Event_Gap_BlackMistErupts_HidenThreaten_Harden":
                            {
                                QuickSave_Connect_String = _Event_NowEventKey_String + "_Success";
                            }
                            break;
                        //特殊走向
                        case "Event_Gap_HiddenFangs_ReachOut":
                            {
                                string QuickSave_ValueKey_String =
                                    "Compare_Precision_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Event_PlayingEvent_Class.Numbers[QuickSave_ValueKey_String],
                                    QuickSave_ConceptObject_Script._Basic_Source_Class, Basic_Source_Class, QuickSave_Creature_Script._Card_UsingObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];
                                float QuickSave_RandomStatus_Float = RandomStatusValue(QuickSave_Type_String);

                                if (!(QuickSave_RandomStatus_Float < QuickSave_Value_Float))
                                {
                                    QuickSave_Connect_String = "Event_Gap_HiddenFangs_InvisibleWeapon";
                                }
                                else
                                {
                                    QuickSave_Connect_String = "Event_Gap_HiddenFangs_ConcealBite";
                                }
                            }
                            break;
                    }
                }
                break;
            case "End":
                {
                    EventEndSet();
                    QuickSave_EndOut_Bool = true;
                }
                break;
        }
        Out:
        //----------------------------------------------------------------------------------------------------

        //初始化----------------------------------------------------------------------------------------------------
        //移除卡片
        List<_UI_Card_Unit> QuickSave_Selects_ScriptsList =
            new List<_UI_Card_Unit>(_Event_Selects_Dictionary.Values);
        List<_UI_Card_Unit> QuickSave_RemoveCards_ScriptsList =
            new List<_UI_Card_Unit>(_UI_CardManager._Card_EventingCard_ScriptsList);
        foreach (_UI_Card_Unit Key in QuickSave_Selects_ScriptsList)//回歸與剃除Select
        {
            TakeSelectEnQueue(Key);
            QuickSave_RemoveCards_ScriptsList.Remove(Key);
            if (_Event_SelectCard_Script == Key)
            {
                QuickSave_SelectUsing_Bool = true;
            }
        }
        _UI_CardManager.CardsMove("Board_Add_Select",
            _World_Manager._Object_Manager._Object_Player_Script, QuickSave_Selects_ScriptsList,
            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);//排庫清除
        _Event_Selects_Dictionary.Clear();
        //消除卡片(已無Select)
        if (_Event_SelectCard_Script != null)
        {
            if (_Event_SelectCard_Script._Card_ExploreUnit_Script._Basic_Key_String != null)//使用非Select
            {
                _Event_SelectCard_Script._Card_ExploreUnit_Script.Key_EndEvent();//使用消耗等
            }
            _Event_SelectCard_Script.
                UseCardEnd(SelectUsing: QuickSave_SelectUsing_Bool, EventCards: QuickSave_RemoveCards_ScriptsList);
        }

        _UI_CardManager._Card_EventingCard_ScriptsList.Clear();
        _Event_StatusAdd_Dictionary.Clear();
        _Event_StatusMultiply_Dictionary.Clear();
        _Event_SelectCard_Script = null;
        _Event_Selected_Bool = false;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_EndOut_Bool)
        {
            return;
        }
        //事件前往
        if (QuickSave_Replace_String != "")
        {
            ReplaceEvent(QuickSave_Replace_String);
        }
        if (QuickSave_Connect_String != "")
        {
            StartEventDialogue(QuickSave_Connect_String, "Event");
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #region - PrivateVoid -
    //——————————————————————————————————————————————————————————————————————
    private float RandomStatusValue(string StatusKey)
    {
        //----------------------------------------------------------------------------------------------------
        _UI_CardManager _UI_CardManager = _World_Manager._UI_Manager._UI_CardManager;
        _Object_CreatureUnit QuickSave_Creature_Script =
            _World_Manager._Object_Manager._Object_Player_Script;
        _Map_BattleObjectUnit QuickSave_ConceptObject_Script =
            QuickSave_Creature_Script._Object_Inventory_Script._Item_EquipConcepts_Script._Basic_Object_Script;

        string QuickSave_Status_String = StatusKey;

        float QuickSave_StatusDefault_Float = 
            QuickSave_ConceptObject_Script.Key_Status(QuickSave_Status_String,
            Basic_Source_Class, null, QuickSave_Creature_Script._Card_UsingObject_Script);
        float QuickSave_StatusAdd_Float = 0;
        float QuickSave_StatusMultiply_Float = 1;
        foreach (_UI_Card_Unit Card in _UI_CardManager._Card_EventingCard_ScriptsList)
        {
            QuickSave_StatusAdd_Float += Card._Card_ExploreUnit_Script.
                Key_EventStatusValueAdd(StatusKey, Basic_Source_Class, Card._Card_UseObject_Script);
            QuickSave_StatusMultiply_Float *= Card._Card_ExploreUnit_Script.
                Key_EventStatusValueMultiply(StatusKey, Basic_Source_Class, Card._Card_UseObject_Script);
        }        
        float QuickSave_RandomStatus_Float =
            UnityEngine.Random.Range(0, QuickSave_StatusDefault_Float);
        return (QuickSave_RandomStatus_Float + QuickSave_StatusAdd_Float) * QuickSave_StatusMultiply_Float;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    private void ReplaceEvent(string Key)
    {
        //----------------------------------------------------------------------------------------------------
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        _Map_FieldCreator _Map_FieldCreator = _World_Manager._Map_Manager._Map_FieldCreator;
        string QuickSave_Type_String = "Null";
        if (Key != "Null")
        {
            QuickSave_Type_String = _Data_Event_Dictionary[Key].OwnTag[0];
        }
        _Map_FieldCreator.
            _Map_Data_Dictionary[QuickSave_Map_String].
            Data[_Event_NowVector_Class.Vector2Int].Event = Key;
        _Map_FieldCreator._Map_GroundBoard_Dictionary[_Event_NowVector_Class.Vector2Int].EventViewSet(QuickSave_Type_String);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion



    //追加傷害(並非附加於攻擊之上(如因效果傷害自身))——————————————————————————————————————————————————————————————————————
    public List<DamageClass> Key_Pursuit(string Key, EventDataClass Event)//附魔對象類型
    {
        //變數----------------------------------------------------------------------------------------------------
        List<DamageClass> Answer_Return_ClassList = new List<DamageClass>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Object_CreatureUnit QuickSave_Creature_Script = _World_Manager._Object_Manager._Object_Player_Script;
        _Map_BattleObjectUnit QuickSave_ConceptObject_Script =
            QuickSave_Creature_Script._Object_Inventory_Script._Item_EquipConcepts_Script._Basic_Object_Script;
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        switch (Key)
        {
            //穿刺傷害
            case "Event_Gap_HiddenFangs_ConcealBite":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, Event.Numbers[QuickSave_ValueKey01_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, Event.Numbers[QuickSave_ValueKey02_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_AttackType_String =
                        QuickSave_ValueKey01_String.Split("_"[0])[1];

                    Answer_Return_ClassList.Add(new DamageClass
                    {
                        Source = Basic_Source_Class,
                        DamageType = QuickSave_AttackType_String,
                        Damage = QuickSave_Value01_Float,
                        Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                    });
                }
                break;
            //渾沌傷害
            case "Event_Gap_FreshBerry_Taste_Failure":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, Event.Numbers[QuickSave_ValueKey01_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, Event.Numbers[QuickSave_ValueKey02_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_AttackType_String =
                        QuickSave_ValueKey01_String.Split("_"[0])[1];

                    Answer_Return_ClassList.Add(new DamageClass
                    {
                        Source = Basic_Source_Class,
                        DamageType = QuickSave_AttackType_String,
                        Damage = QuickSave_Value01_Float,
                        Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                    });
                }
                break;
            //絕對傷害
            case "Event_Gap_BlackMistErupts_ReflexAction_Failure":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_StarkDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, Event.Numbers[QuickSave_ValueKey01_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_StarkDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, Event.Numbers[QuickSave_ValueKey02_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_AttackType_String =
                        QuickSave_ValueKey01_String.Split("_"[0])[1];

                    Answer_Return_ClassList.Add(new DamageClass
                    {
                        Source = Basic_Source_Class,
                        DamageType = QuickSave_AttackType_String,
                        Damage = QuickSave_Value01_Float,
                        Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                    });
                }
                break;
            case "Event_Gap_BlackMistDevour_Erosion":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_StarkDamage_Type0｜Target_Concept_Default_Point_MediumPoint_Total_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, Event.Numbers[QuickSave_ValueKey01_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_StarkDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, Event.Numbers[QuickSave_ValueKey02_String],
                        Basic_Source_Class, QuickSave_ConceptObject_Script._Basic_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_AttackType_String =
                        QuickSave_ValueKey01_String.Split("_"[0])[1];

                    Answer_Return_ClassList.Add(new DamageClass
                    {
                        Source = Basic_Source_Class,
                        DamageType = QuickSave_AttackType_String,
                        Damage = QuickSave_Value01_Float,
                        Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                    });
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //使用原本數值
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    #endregion KeyAction
    #endregion Event

    #region SyndromePool
    public List<string> TotalSyndromePool(Vector Coordinate)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (Coordinate == null)//未站於地板上/源於自身
        {
            Answer_Return_StringList.AddRange(_Syndrome_EquipSyndromePool_StringList);
        }
        else
        {
            string QuickSave_Map_String =
                _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
            _Map_Manager.FieldDataClass QuickSave_FieldData_Class =
                _World_Manager._Map_Manager._Map_FieldCreator.
                _Map_Data_Dictionary[QuickSave_Map_String];
            _Map_Manager.GroundDataClass QuickSave_GroundData_Class =
                QuickSave_FieldData_Class.Data[Coordinate.Vector2Int];
            string QuickSave_Region_String =
                QuickSave_GroundData_Class.Region;

            Answer_Return_StringList.AddRange(_Syndrome_EquipSyndromePool_StringList);
            Answer_Return_StringList.AddRange(
                QuickSave_FieldData_Class.Syndrome["All"]);
            Answer_Return_StringList.AddRange(
                QuickSave_FieldData_Class.Syndrome[QuickSave_Region_String]);
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    public void SyndromePoolSelect(string Type, Vector Coordinate)
    {
        //----------------------------------------------------------------------------------------------------
        _View_TinyMenu _View_TinyMenu = _World_Manager._UI_Manager._UI_Camp_Class._View_TinyMenu;
        string QuickSave_Key_String = "SyndromePool_" + Type;//SyndromePool_Add/SyndromePool_Remove
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //建置
        List<string> QuickSave_SyndromePool_StringList = new List<string>();
        List<string> QuickSave_AnswerSyndromePool_StringList = new List<string>();
        switch (Type)
        {
            case "Add":
                {
                    List<string> QuickSave_TotalSyndrome_StringList =
                        TotalSyndromePool(Coordinate);
                    foreach (string Syndrome in QuickSave_TotalSyndrome_StringList)
                    {
                        if (!_Syndrome_SyndromePool_StringList.Contains(Syndrome))
                        {
                            QuickSave_SyndromePool_StringList.Add(Syndrome);
                        }
                    }
                }
                break;
            case "Remove":
                {
                    QuickSave_SyndromePool_StringList.AddRange(_Syndrome_SyndromePool_StringList);
                }
                break;
        }
        int QuickSave_SelectSize_Int =
            Mathf.Clamp(3, 1, Mathf.Clamp(QuickSave_SyndromePool_StringList.Count, 1, 5));
        //抽選
        for (int a = 0; a < QuickSave_SelectSize_Int; a++)
        {
            string QuickSave_Syndrome_String =
                QuickSave_SyndromePool_StringList[UnityEngine.Random.Range(0, QuickSave_SyndromePool_StringList.Count)];            
            QuickSave_SyndromePool_StringList.Remove(QuickSave_Syndrome_String);
            QuickSave_AnswerSyndromePool_StringList.Add(QuickSave_Syndrome_String);
        }
        //開啟選單
        _View_TinyMenu.TinyMenuSet(QuickSave_Key_String, QuickSave_AnswerSyndromePool_StringList);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void SyndromePoolSet(string Type, string Key, Vector Coordinate)
    {
        //----------------------------------------------------------------------------------------------------
        string QuickSave_Key_String = "";
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Key)
        {
            case "Random":
                {
                    List<string> QuickSave_Key_StringList = new List<string>();
                    List<string> QuickSave_TotalSyndrome_StringList =
                        TotalSyndromePool(Coordinate);
                    foreach (string Syndrome in QuickSave_TotalSyndrome_StringList)
                    {
                        if (!_Syndrome_SyndromePool_StringList.Contains(Syndrome))
                        {
                            QuickSave_Key_StringList.Add(Syndrome);
                        }
                    }
                    if(QuickSave_Key_StringList.Count > 0)
                    {
                        QuickSave_Key_String =
                            QuickSave_Key_StringList[Random.Range(0, QuickSave_Key_StringList.Count)];
                    }
                }
                break;
            default:
                {
                    QuickSave_Key_String = Key;
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Key_String != "")
        {
            switch (Type)
            {
                case "Add":
                    {
                        _Syndrome_SyndromePool_StringList.Add(QuickSave_Key_String);
                    }
                    break;
                case "Remove":
                    {
                        _Syndrome_SyndromePool_StringList.Remove(QuickSave_Key_String);
                    }
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion SyndromePool
}
