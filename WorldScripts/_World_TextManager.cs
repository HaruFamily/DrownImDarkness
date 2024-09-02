using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Xml.Linq;

public class _World_TextManager : MonoBehaviour
{
    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————
    //索引----------------------------------------------------------------------------------------------------
    //UI介面名稱索引
    public Dictionary<string, string> _Language_UIName_Dictionary = new Dictionary<string, string>();
    public Dictionary<string, string> _Language_UISummary_Dictionary = new Dictionary<string, string>();
    public Dictionary<string, string> _Language_Adden_Dictionary = new Dictionary<string, string>();

    public Dictionary<string, LanguageClass> _Language_Tag_Dictionary = new Dictionary<string, LanguageClass>();

    public TMP_SpriteAsset _Language_SpriteAsset_Data;
    private List<string> _Language_SpriteAssetKeyRing_StringList = new List<string>();
    //----------------------------------------------------------------------------------------------------
    //子物件集——————————————————————————————————————————————————————————————————————
    #endregion ElementBox


    #region DictionarySet
    //各類圖片匯入區——————————————————————————————————————————————————————————————————————
    //設定語言類別----------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion DictionarySet


    #region FirstBuild
    //第一行動——————————————————————————————————————————————————————————————————————
    public void SystemStart()
    {
        //建立資料----------------------------------------------------------------------------------------------------
        //設置語言
        LanguageSet();
        //----------------------------------------------------------------------------------------------------

        foreach (TMP_SpriteCharacter Sprite in _Language_SpriteAsset_Data.spriteCharacterTable)
        {
            _Language_SpriteAssetKeyRing_StringList.Add(Sprite.name);
        }
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion FirstBuild


    #region DataBaseSet
    //語言設置——————————————————————————————————————————————————————————————————————
    public void LanguageSet()
    {
        //UI介面名稱----------------------------------------------------------------------------------------------------
        //文本分割與置入索引
        string QuickSave_UINameTextSource_String = "";
        string QuickSave_UINameTextAssetCheck_String = "";
        QuickSave_UINameTextAssetCheck_String = Application.streamingAssetsPath + "/Text/_" + _World_Manager._Config_Language_String + "_UIName.txt";
        if (File.Exists(QuickSave_UINameTextAssetCheck_String))
        {
            QuickSave_UINameTextSource_String = File.ReadAllText(QuickSave_UINameTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Text/_" + _World_Manager._Config_Language_String + "_UIName.txt");
            QuickSave_UINameTextAssetCheck_String = Application.streamingAssetsPath + "/Text/_TraditionalChinese_UIName.txt";
            QuickSave_UINameTextSource_String = File.ReadAllText(QuickSave_UINameTextAssetCheck_String);
        }
        //分割為單項
        string[] QuickSave_UINameSourceSplit = QuickSave_UINameTextSource_String.Split("\r"[0]);
        for (int t = 0; t < QuickSave_UINameSourceSplit.Length; t++)
        {
            if (QuickSave_UINameSourceSplit[t] == "" || QuickSave_UINameSourceSplit[t] == "\n")
            {
                continue;
            }
            string[] QuickSave_TextSplit = QuickSave_UINameSourceSplit[t].Substring(1).Split(":"[0]);
            //置入索引
            _Language_UIName_Dictionary.Add(QuickSave_TextSplit[0], QuickSave_TextSplit[1]);
        }
        //----------------------------------------------------------------------------------------------------

        //UI介面內容----------------------------------------------------------------------------------------------------
        //文本分割與置入索引
        string QuickSave_UISummaryTextSource_String = "";
        string QuickSave_UISummaryTextAssetCheck_String = "";
        QuickSave_UISummaryTextAssetCheck_String = Application.streamingAssetsPath + "/Text/_" + _World_Manager._Config_Language_String + "_UISummary.txt";
        if (File.Exists(QuickSave_UISummaryTextAssetCheck_String))
        {
            QuickSave_UISummaryTextSource_String = File.ReadAllText(QuickSave_UISummaryTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Text/_" + _World_Manager._Config_Language_String + "_UISummary.txt");
            QuickSave_UISummaryTextAssetCheck_String = Application.streamingAssetsPath + "/Text/_TraditionalChinese_UISummary.txt";
            QuickSave_UISummaryTextSource_String = File.ReadAllText(QuickSave_UISummaryTextAssetCheck_String);
        }
        //分割為單項
        string[] QuickSave_UISummarySourceSplit = QuickSave_UISummaryTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_UISummarySourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_UISummarySourceSplit[t] == "" || QuickSave_UISummarySourceSplit[t] == "\n")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit_StringArray = QuickSave_UISummarySourceSplit[t].Split("｜"[0]);
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Language_UISummary_Dictionary.Add(QuickSave_TextSplit_StringArray[0].Substring(2), QuickSave_TextSplit_StringArray[1].Substring(2));
            //----------------------------------------------------------------------------------------------------
        }
        //----------------------------------------------------------------------------------------------------

        //新增內容----------------------------------------------------------------------------------------------------
        //文本分割與置入索引
        string QuickSave_AddenTextSource_String = "";
        string QuickSave_AddenTextAssetCheck_String = "";
        QuickSave_AddenTextAssetCheck_String = Application.streamingAssetsPath + "/Text/_" + _World_Manager._Config_Language_String + "_Adden.txt";
        if (File.Exists(QuickSave_AddenTextAssetCheck_String))
        {
            QuickSave_AddenTextSource_String = File.ReadAllText(QuickSave_AddenTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Text/_" + _World_Manager._Config_Language_String + "_Adden.txt");
            QuickSave_AddenTextAssetCheck_String = Application.streamingAssetsPath + "/Text/_TraditionalChinese_Adden.txt";
            QuickSave_AddenTextSource_String = File.ReadAllText(QuickSave_AddenTextAssetCheck_String);
        }
        //分割為單項
        string[] QuickSave_AddenSourceSplit = QuickSave_AddenTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_AddenSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_AddenSourceSplit[t] == "" || QuickSave_AddenSourceSplit[t] == "\n")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit_StringArray = QuickSave_AddenSourceSplit[t].Split("｜"[0]);
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            string QuickSave_Input_String =
                QuickSave_TextSplit_StringArray[1].Substring(2);
            QuickSave_Input_String =
                QuickSave_Input_String.Substring(0, QuickSave_Input_String.Length - 2);
            _Language_Adden_Dictionary.Add(QuickSave_TextSplit_StringArray[0].Substring(2), QuickSave_Input_String);
            //----------------------------------------------------------------------------------------------------
        }
        //----------------------------------------------------------------------------------------------------

        #region - Tag -
        //行為——————————————————————————————————————————————————————————————————————
        //變數----------------------------------------------------------------------------------------------------
        string QuickSave_TagTextSource_String = "";
        string QuickSave_TagTextAssetCheck_String = "";
        //----------------------------------------------------------------------------------------------------

        //取得文檔----------------------------------------------------------------------------------------------------
        QuickSave_TagTextAssetCheck_String = Application.streamingAssetsPath + "/Text/_" + _World_Manager._Config_Language_String + "_Tag.txt";
        if (File.Exists(QuickSave_TagTextAssetCheck_String))
        {
            QuickSave_TagTextSource_String = File.ReadAllText(QuickSave_TagTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Skill/_" + _World_Manager._Config_Language_String + "_Tag.txt");
            QuickSave_TagTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_TraditionalChinese_Tag.txt";
            QuickSave_TagTextSource_String = File.ReadAllText(QuickSave_TagTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //讀取文檔----------------------------------------------------------------------------------------------------
        string[] QuickSave_TagSourceSplit = QuickSave_TagTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_TagSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_TagSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //執行----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_TagSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_Tag_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //----------------------------------------------------------------------------------------------------
        //——————————————————————————————————————————————————————————————————————
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion DataBaseSet


    #region TextmeshProTranslate

    //密碼確認——————————————————————————————————————————————————————————————————————
    public string TextmeshProTranslater(string Situation, string TextInput, int AutoSegmentation,
        SourceClass UserSource, SourceClass TargetSource,_Map_BattleObjectUnit UsingObject,
        int Time, int Order)
    {
        //變數----------------------------------------------------------------------------------------------------
        //輸出答案與儲存
        string Answer_TranslatedText_String = "";

        //擷取器開關
        bool QuickSave_CodeSwitch_Bool = false;
        //擷取密碼
        string QuickSave_Code_String = "";
        //自動段落位置
        bool QuickSave_AutoSegmentation_Bool = true;
        if (AutoSegmentation == 0)
        {
            QuickSave_AutoSegmentation_Bool = false;
        }
        int QuickSave_AutoSegmentation_Int = AutoSegmentation;
        int QuickSave_AutoSegmentationSave_Int = QuickSave_AutoSegmentation_Int;
        //----------------------------------------------------------------------------------------------------

        //掃描字串----------------------------------------------------------------------------------------------------
        int QuickSave_Count_Int =0;//正常文字計數器;
        for (int a = 0; a < TextInput.Length; a++)
        {
            switch (TextInput[a].ToString())
            {
                case "﹝":
                    //開啟開關
                    QuickSave_CodeSwitch_Bool = true;
                    break;
                case "﹞":
                    //關閉開關
                    QuickSave_CodeSwitch_Bool = false;
                    //內容調整
                    string[] QuickSave_CodeSplit_StringArray = QuickSave_Code_String.Split(":"[0]);
                    //紀錄編譯後文字-主要類型與類型細節---">"將在翻譯後新增
                    string QuickSave_CodeAnswer_String = 
                        TranslaterDivert(Situation, QuickSave_CodeSplit_StringArray,
                        UserSource, TargetSource, UsingObject,
                        Time, Order);
                    Answer_TranslatedText_String += QuickSave_CodeAnswer_String;
                    //初始化
                    QuickSave_Code_String = "";

                    //自動段落
                    if (QuickSave_AutoSegmentation_Bool)
                    {
                        int QuickSave_StartIndex_Int = QuickSave_CodeAnswer_String.IndexOf("<");

                        // 當還有起始字符存在時
                        while (QuickSave_StartIndex_Int != -1)
                        {
                            int QuickSave_EndIndex_Int = 
                                QuickSave_CodeAnswer_String.IndexOf(">", QuickSave_StartIndex_Int + 1);

                            // 當還有結束字符存在且結束字符在起始字符之後時
                            if (QuickSave_EndIndex_Int != -1 && QuickSave_EndIndex_Int > QuickSave_StartIndex_Int)
                            {
                                // 移除起始字符和結束字符之間的字元
                                QuickSave_CodeAnswer_String = QuickSave_CodeAnswer_String.Remove(
                                    QuickSave_StartIndex_Int, 
                                    QuickSave_EndIndex_Int - QuickSave_StartIndex_Int + 1);
                            }
                            else
                            {
                                // 如果找不到對應的結束字符，跳出迴圈
                                break;
                            }

                            // 尋找下一對起始字符和結束字符
                            QuickSave_StartIndex_Int = QuickSave_CodeAnswer_String.IndexOf("<", QuickSave_StartIndex_Int);
                        }
                        QuickSave_Count_Int += QuickSave_CodeAnswer_String.Length;
                    }
                    break;
                default:
                    if (QuickSave_CodeSwitch_Bool)
                    {
                        //解碼器開啟，紀錄密碼
                        QuickSave_Code_String += TextInput[a];
                    }
                    else
                    {
                        //正常狀態紀錄文字
                        Answer_TranslatedText_String += TextInput[a];
                        //自動段落
                        if (QuickSave_AutoSegmentation_Bool)
                        {
                            QuickSave_Count_Int += 1;
                            if (QuickSave_Count_Int > QuickSave_AutoSegmentationSave_Int)
                            {
                                Answer_TranslatedText_String += "\n";
                                QuickSave_AutoSegmentationSave_Int += QuickSave_AutoSegmentation_Int;
                            }
                        }
                    }
                    break;
            }
        }
        //段落
        //----------------------------------------------------------------------------------------------------

        //輸出----------------------------------------------------------------------------------------------------
        return Answer_TranslatedText_String;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //密碼分流——————————————————————————————————————————————————————————————————————
    private string TranslaterDivert(string Situation /*Data=資料，Default=基本，CountEnchance=計算附魔,VarietyTest=測試變數*/,
        string[] Text, 
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        string Answer_Return_String = "";
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        
        //----------------------------------------------------------------------------------------------------

        //各類呼叫----------------------------------------------------------------------------------------------------
        switch (Text[0])
        {
            case "Common":
                #region - Common -
                {
                    //設定----------------------------------------------------------------------------------------------------
                    switch (Text[1])
                    {
                        //無數字
                        case "LineFeed":
                            return "\n";

                        #region - Cover -
                        //引用
                        case "Start":
                            switch (Text[2])
                            {
                                case "Code":
                                    return "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>";
                                default:
                                    return " <" + Text[1] + ":" + Text[2] + ">";
                            }

                        case "End":
                            switch (Text[2])
                            {
                                case "Code":
                                    return "</b></color>";
                                default:
                                    return "<" + Text[1] + ":" + Text[2] + "> ";
                            }
                            break;
                        #endregion

                        #region - UIName -
                        case "UIName":
                            {
                                string QuickSave_Text_String = Text[2];
                                if (_Language_UIName_Dictionary.TryGetValue(Text[2],out string DicValue))
                                {
                                    QuickSave_Text_String = DicValue;
                                }
                                string QuickSave_SpriteText_String = "";
                                if (_Language_SpriteAssetKeyRing_StringList.Contains(Text[2]))
                                {
                                    QuickSave_SpriteText_String = ("<sprite name=\"" + Text[2] + "\"" +
                                            "color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + ">");
                                }
                                return QuickSave_SpriteText_String +
                                            "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>" +
                                            QuickSave_Text_String + "</b></color>";
                            }
                            break;
                        #endregion

                        #region - Skill -
                        case "Passive":
                            {
                                if (_World_Manager._Skill_Manager._Language_Passive_Dictionary.TryGetValue(Text[2], out LanguageClass DicValue))
                                {
                                    return "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>" +
                                                DicValue.Name + "</b></color>";

                                }
                            }
                            break;
                        case "Explore":
                            {
                                if (_World_Manager._Skill_Manager._Language_Explore_Dictionary.TryGetValue(Text[2], out LanguageClass DicValue))
                                {
                                    return "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>" +
                                                DicValue.Name + "</b></color>";

                                }
                            }
                            break;
                        case "Behavior":
                            {
                                if (_World_Manager._Skill_Manager._Language_Behavior_Dictionary.TryGetValue(Text[2], out LanguageClass DicValue))
                                {
                                    return "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>" +
                                                DicValue.Name + "</b></color>";

                                }
                            }
                            break;
                        case "Enchance":
                            {
                                if (_World_Manager._Skill_Manager._Language_Enchance_Dictionary.TryGetValue(Text[2], out LanguageClass DicValue))
                                {
                                    return "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>" +
                                                DicValue.Name + "</b></color>";

                                }
                            }
                            break;
                        #endregion

                        #region - Effect -
                        case "EffectObject":
                            {
                                if (_World_Manager._Effect_Manager._Language_EffectObject_Dictionary.TryGetValue(Text[2], out LanguageClass DicValue))
                                {
                                    return "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>" +
                                                DicValue.Name + "</b></color>";

                                }
                            }
                            break;
                        case "EffectCard":
                            {
                                if (_World_Manager._Effect_Manager._Language_EffectCard_Dictionary.TryGetValue(Text[2], out LanguageClass DicValue))
                                {
                                    return "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>" +
                                                DicValue.Name + "</b></color>";

                                }
                            }
                            break;
                        #endregion

                        #region - Object -
                        case "Object":
                            {
                                if (_World_Manager._Object_Manager._Language_Object_Dictionary.TryGetValue(Text[2], out LanguageClass DicValue))
                                {
                                    return "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>" +
                                                DicValue.Name + "</b></color>";

                                }
                            }
                            break;
                        #endregion

                        #region - Card -
                        case "Card":
                            {
                                if (_World_Manager._Skill_Manager._Language_SkillLeaves_Dictionary.TryGetValue(Text[2], out LanguageClass DicValue))
                                {
                                    return "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>" +
                                                DicValue.Name + "</b></color>";

                                }
                            }
                            break;
                        #endregion

                        #region - Tag -
                        case "Tag":
                            {
                                //顯示標籤
                                return "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>" +
                                            TagTextTranslate(Text[2]) + "</b></color>";
                            }
                            break;
                        #endregion
                        default:
                            return "<" + Text[1] + ":" + Text[2] + "> ";
                    }
                    //----------------------------------------------------------------------------------------------------

                }
                #endregion
                break;
            case "Numbers":
                {
                    #region - Numbers -
                    if (!UserSource.Source_NumbersData.ContainsKey(Text[1]))
                    {
                        print("Lost:" + Text[1]);
                        foreach (string Key in UserSource.Source_NumbersData.Keys)
                        {
                            print(Key);
                        }
                        return "";
                    }
                    if (Situation == "VarietyTest")
                    {
                        return "";
                    }
                    float QuickSave_NumbersValue =
                        UserSource.Source_NumbersData[Text[1]];
                    string QuickSave_KeyUnit_String =
                        _World_Manager.Key_KeysUnit(
                            Situation, Text[1], 
                            UserSource, TargetSource, UsingObject,
                            null, false, Time, Order);
                    float QuickSave_NumberUnit_Float =
                        _World_Manager.Key_NumbersUnit(
                            Situation, QuickSave_KeyUnit_String, QuickSave_NumbersValue,
                            UserSource, TargetSource, UsingObject,
                            null , false, Time, Order);

                    string[] QuickSave_ValueSplit_StringArray = QuickSave_KeyUnit_String.Split("｜"[0]);
                    string[] QuickSave_Type_StringArray = QuickSave_ValueSplit_StringArray[0].Split("_"[0]);
                    string QuickSave_SubType_String = QuickSave_ValueSplit_StringArray[0].Replace(QuickSave_Type_StringArray[0] + "_", "");
                    string[] QuickSave_Reference_StringArray = QuickSave_ValueSplit_StringArray[1].Split("_"[0]);
                    //開頭新增
                    #region - Type -
                    switch (QuickSave_Type_StringArray[0])
                    {
                        #region - ValueAndTimes -
                        case "AttackNumber":
                        case "HealNumber":
                        case "PursuitNumber":
                        case "DamageNumber":
                            {
                                string QuickSave_Start_String =
                                    ("<sprite name=\"" + QuickSave_Type_StringArray[1] + "\"" +
                                    "color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", QuickSave_Type_StringArray[1])) + ">") +
                                    "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", QuickSave_Type_StringArray[1])) + "><b>";
                                string QuickSave_End_String = "</b></color>";

                                //文字輸出
                                Answer_Return_String +=
                                    _Language_Adden_Dictionary[QuickSave_Type_StringArray[0]].
                                    Replace("﹝Value_01﹞", QuickSave_Start_String +
                                    DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float,
                                    UserSource, TargetSource)).
                                    Replace("﹝Value_02﹞", QuickSave_End_String);


                            }
                            break;
                        case "AttackTimes":
                        case "HealTimes":
                        case "PursuitTimes":
                        case "DamageTimes":
                            {
                                //通常於Value進行/如在Language中才顯示(前通常為特殊項)
                                string QuickSave_Start_String =
                                    "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", QuickSave_Type_StringArray[1])) + "><b>";
                                string QuickSave_End_String =
                                        _Language_UIName_Dictionary[QuickSave_Type_StringArray[1]] + "</b></color>";
                                //顯示判定
                                bool QuickSave_ContainNumber_Bool = false;
                                string QuickSave_CheckKey_String = QuickSave_ValueSplit_StringArray[0].Replace("Times", "Number");
                                foreach (string Key in UserSource.Source_NumbersData.Keys)
                                {
                                    string QuickSave_DicKeyUnit_String =
                                        _World_Manager.Key_KeysUnit(
                                            Situation, Key,
                                            UserSource, TargetSource, UsingObject,
                                            null, false, Time, Order);
                                    if (QuickSave_DicKeyUnit_String.Split("｜"[0])[0] == QuickSave_CheckKey_String)
                                    {
                                        QuickSave_ContainNumber_Bool = true;
                                        break;
                                    }
                                }
                                //輸出
                                if (QuickSave_ContainNumber_Bool)
                                {
                                    if (QuickSave_NumberUnit_Float != 1)
                                    {
                                        Answer_Return_String +=
                                            (QuickSave_Start_String + "*" +
                                            DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource) +
                                            QuickSave_End_String);
                                    }
                                    else
                                    {
                                        Answer_Return_String +=
                                            (QuickSave_Start_String +
                                            QuickSave_End_String);
                                    }
                                }
                                else
                                {
                                    Answer_Return_String +=
                                        (QuickSave_Start_String + "*" +
                                        DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource) +
                                        QuickSave_End_String);
                                }
                            }
                            break;
                        #endregion

                        #region - ConstructNumber -
                        case "ConstructNumber":
                            {
                                _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
                                //獲得效果
                                string QuickSave_Type_String = QuickSave_SubType_String.Split("_"[0])[0];
                                string QuickSave_Start_String =
                                    "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>";
                                string QuickSave_End_String =
                                    "</b></color>";
                                string QuickSave_Name_String = "";
                                switch (QuickSave_Type_String)
                                {
                                    case "EffectObject":
                                        {
                                            if (_Effect_Manager._Language_EffectObject_Dictionary.TryGetValue(
                                                QuickSave_SubType_String, out LanguageClass Language))
                                            {
                                                QuickSave_Name_String = Language.Name;
                                            }
                                            else
                                            {
                                                QuickSave_Name_String = QuickSave_SubType_String;
                                            }
                                        }
                                        break;
                                    case "EffectCard":
                                        {
                                            if (_Effect_Manager._Language_EffectCard_Dictionary.TryGetValue(
                                                QuickSave_SubType_String, out LanguageClass Language))
                                            {
                                                QuickSave_Name_String = Language.Name;
                                            }
                                            else
                                            {
                                                QuickSave_Name_String = QuickSave_SubType_String;
                                            }
                                        }
                                        break;
                                }
                                Answer_Return_String +=
                                    _Language_Adden_Dictionary[QuickSave_Type_StringArray[0]].
                                    Replace("﹝Value_01﹞",
                                    QuickSave_Start_String + QuickSave_Name_String + "*" +
                                    DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource) + QuickSave_End_String);
                            }
                            break;
                        #endregion

                        #region - Consume/Remove -
                        case "Consume":
                            {
                                //消耗數值
                                string QuickSave_Start_String =
                                    ("<sprite name=\"" + QuickSave_Type_StringArray[1] + "\"" +
                                    "color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", QuickSave_Type_StringArray[1])) + ">") +
                                    "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", QuickSave_Type_StringArray[1])) + "><b>";
                                string QuickSave_End_String =
                                    _Language_UIName_Dictionary[QuickSave_Type_StringArray[1]] + "</b></color>";
                                Answer_Return_String +=
                                    _Language_Adden_Dictionary[QuickSave_Type_StringArray[0]].
                                    Replace("﹝Value_01﹞", QuickSave_Start_String +
                                    DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource) + QuickSave_End_String);
                            }
                            break;
                        case "Remove":
                            {
                                //移除效果
                                string QuickSave_Start_String =
                                    "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>";
                                string QuickSave_End_String =
                                    "</b></color>";
                                string QuickSave_TagText_String = TagTextTranslate(QuickSave_SubType_String);//視需求調整

                                Answer_Return_String +=
                                    _Language_Adden_Dictionary[QuickSave_Type_StringArray[0]].
                                    Replace("﹝Value_01﹞",
                                    QuickSave_Start_String + QuickSave_TagText_String + "*" +
                                    DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource) + QuickSave_End_String);
                            }
                            break;
                        #endregion

                        #region - Deal -
                        case "Deal":
                        case "Throw":
                            {
                                //抽/丟卡
                                string QuickSave_Start_String =
                                    "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>";
                                string QuickSave_End_String =
                                    "</b></color>";
                                Answer_Return_String +=
                                    _Language_Adden_Dictionary[QuickSave_Type_StringArray[0]].
                                    Replace("﹝Value_01﹞",
                                    QuickSave_Start_String +
                                    DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource) + QuickSave_End_String);
                            }
                            break;
                        #endregion

                        #region - Probability -
                        case "Probability":
                            {
                                //機率
                                string QuickSave_Start_String =
                                    "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>";
                                string QuickSave_End_String =
                                    "</b></color>";
                                Answer_Return_String +=
                                    _Language_Adden_Dictionary[QuickSave_Type_StringArray[0]].
                                    Replace("﹝Value_01﹞",
                                    QuickSave_Start_String +
                                    DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource) + QuickSave_End_String);
                            }
                            break;
                        #endregion

                        #region - Percentage -
                        case "Percentage":
                            {
                                //獲得效果
                                string QuickSave_Start_String =
                                    "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>";
                                string QuickSave_End_String =
                                    "</b></color>";
                                Answer_Return_String += _Language_Adden_Dictionary[QuickSave_Type_StringArray[0]].
                                    Replace("﹝Value_01﹞",
                                    QuickSave_Start_String +
                                    DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource) +
                                    QuickSave_End_String);
                            }
                            break;
                        #endregion

                        #region - Create -
                        case "Create":
                            {
                                //獲得效果
                                string QuickSave_Start_String =
                                    "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>";
                                string QuickSave_End_String =
                                    "</b></color>";
                                string QuickSave_Name_String =
                                    _World_Manager._Object_Manager._Language_Object_Dictionary[QuickSave_SubType_String].Name;//視需求調整
                                Answer_Return_String +=
                                    _Language_Adden_Dictionary[QuickSave_Type_StringArray[0]].
                                    Replace("﹝Value_01﹞",
                                    QuickSave_Start_String + QuickSave_Name_String + "*" +
                                    DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource) + QuickSave_End_String);
                            }
                            break;
                        #endregion

                        #region - Shift -
                        case "Shift":
                            {
                                //位移/ShiftPush/ShiftPull
                                string QuickSave_Start_String =
                                    "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>";
                                string QuickSave_End_String =
                                    "</b></color>";
                                Answer_Return_String +=
                                    _Language_Adden_Dictionary[QuickSave_Type_StringArray[0] + QuickSave_Type_StringArray[1]].
                                    Replace("﹝Value_01﹞",
                                    QuickSave_Start_String +
                                    DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource) + QuickSave_End_String);
                            }
                            break;
                        #endregion

                        #region - MinMaxRange -
                        case "Path":
                        case "Random":
                            {
                                switch (QuickSave_Type_StringArray[1])
                                {
                                    case "Min"://判斷最低值是否相等於最高值/相等時不顯示/不確認最高值
                                        {
                                            string QuickSave_Start_String =
                                                "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>";
                                            string QuickSave_End_String =
                                                "</b></color>";

                                            string QuickSave_CheckKey_String = QuickSave_ValueSplit_StringArray[0].Replace("Max", "Min");
                                            foreach (string Key in UserSource.Source_NumbersData.Keys)
                                            {
                                                string QuickSave_DicKeyUnit_String =
                                                    _World_Manager.Key_KeysUnit(
                                                        Situation, Key, 
                                                        UserSource, TargetSource, UsingObject,
                                                        null, false, Time, Order);
                                                if (QuickSave_DicKeyUnit_String.Split("｜"[0])[0] == QuickSave_CheckKey_String)
                                                {
                                                    float QuickSave_CheckNumbersValue =
                                                        UserSource.Source_NumbersData[Key];
                                                    QuickSave_CheckKey_String = QuickSave_DicKeyUnit_String;
                                                    float QuickSave_CheckNumberUnit_Float =
                                                        _World_Manager.Key_NumbersUnit(
                                                            Situation, QuickSave_CheckKey_String, QuickSave_CheckNumbersValue,
                                                            UserSource, TargetSource, UsingObject, 
                                                            null, false, Time, Order);
                                                    if (QuickSave_CheckNumberUnit_Float != QuickSave_NumberUnit_Float)
                                                    {
                                                        Answer_Return_String +=
                                                            _Language_Adden_Dictionary[QuickSave_Type_StringArray[0]].
                                                            Replace("﹝Value_01﹞", QuickSave_Start_String +
                                                            DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource)).
                                                            Replace("﹝Value_02﹞", "~" +
                                                            DataTextTranslate(Situation, QuickSave_CheckKey_String, QuickSave_CheckNumberUnit_Float, UserSource, TargetSource) + QuickSave_End_String);
                                                    }
                                                    else
                                                    {
                                                        Answer_Return_String +=
                                                            _Language_Adden_Dictionary[QuickSave_Type_StringArray[0]].
                                                            Replace("﹝Value_01﹞", QuickSave_Start_String +
                                                            DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource)).
                                                            Replace("﹝Value_02﹞", QuickSave_End_String);
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                        #endregion

                        default:
                            {
                                string QuickSave_Start_String =
                                    "<color=#" + ColorUtility.ToHtmlStringRGB(_View_Manager.GetColor("Code", "Code")) + "><b>";
                                string QuickSave_End_String =
                                    "</b></color>";
                                //數值新增
                                Answer_Return_String +=
                                    (QuickSave_Start_String +
                                    DataTextTranslate(Situation, QuickSave_KeyUnit_String, QuickSave_NumberUnit_Float, UserSource, TargetSource) +
                                    QuickSave_End_String);

                            }
                            break;
                    }
                    #endregion
                    #endregion
                }
                break;
            /*
        case "Adden"://取得﹝Adden﹞內容/通常為 傳入 一變數
            {
                switch (Situation)
                {
                    case "CountEnchance":
                        {
                            List<_UI_Card_Unit> QuickSave_EnchanceCards_ScriptsList = 
                                UserSource.Source_Card._State_EnchanceStore_ScriptsList;
                            for (int a = 0; a < QuickSave_EnchanceCards_ScriptsList.Count; a++)
                            {
                                _Skill_EnchanceUnit QuickSave_Enchance_Script =
                                    QuickSave_EnchanceCards_ScriptsList[a]._Card_EnchanceUnit_Script;
                                return QuickSave_Enchance_Script.Key_Adden(Text[1]);
                            }
                        }
                        break;
                }
            }
            break;*/
            default:
                print("Wrong:" + Text[0]);
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_String;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //資料說明類 翻譯/——————————————————————————————————————————————————————————————————————
    private string DataTextTranslate(string Type, string TextInput,float Value,
        SourceClass UserSource, SourceClass TargetSource)
    {
        //設定----------------------------------------------------------------------------------------------------
        string Answer_Return_String = _Language_Adden_Dictionary["DataText"];
        string[] QuickSave_Split_StringArray = TextInput.Split("｜"[0]);
        string[] QuickSave_Type_StringArray = QuickSave_Split_StringArray[0].Split("_"[0]);
        string[] QuickSave_Value_StringArray = QuickSave_Split_StringArray[1].Split("_"[0]);//ref
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        bool QuickSave_DataMode_Bool = false;
        if (_World_Manager._World_GeneralManager._Speaker_NumberUnitIsReturn_Bool)
        {
            QuickSave_DataMode_Bool = true;
        }
        switch (Type)
        {
            case "Data":
                {
                    QuickSave_DataMode_Bool = true;
                }
                break;
            default:
                {
                    switch (QuickSave_Value_StringArray[0])
                    {
                        case "User"://目標為自身
                            {
                                if (UserSource == null)
                                {
                                    QuickSave_DataMode_Bool = true;
                                }
                                else
                                {
                                    if (UserSource.Source_BattleObject == null)
                                    {
                                        QuickSave_DataMode_Bool = true;
                                    }
                                }
                            }
                            break;
                        case "Target"://目標為對象
                            {
                                if (TargetSource == null)
                                {
                                    QuickSave_DataMode_Bool = true;
                                }
                                else
                                {
                                    if (TargetSource.Source_BattleObject == null)
                                    {
                                        QuickSave_DataMode_Bool = true;
                                    }
                                }
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //﹝Target_01﹞﹝TargetTag_01﹞﹝SubTarget_01﹞﹝KeyTag_01﹞﹝SubKey_01﹞﹝Key_01﹞﹝Value_01﹞
        //HealNumber_CatalystPoint_Type0｜User_Concept_Default_Status_Catalyst_Default_Default｜0
        string QuickSave_DicValue_String = "";
        //Target
        if (QuickSave_DataMode_Bool &&
            _Language_UIName_Dictionary.TryGetValue(QuickSave_Value_StringArray[0], out QuickSave_DicValue_String))
        {
            Answer_Return_String = Answer_Return_String.Replace("﹝Target_01﹞", QuickSave_DicValue_String + "");
        }
        else
        {
            Answer_Return_String = Answer_Return_String.Replace("﹝Target_01﹞", "");
        }
        //SubTarget
        if (QuickSave_DataMode_Bool &&
            _Language_UIName_Dictionary.TryGetValue(QuickSave_Value_StringArray[1], out QuickSave_DicValue_String))
        {
            Answer_Return_String = Answer_Return_String.Replace("﹝SubTarget_01﹞", QuickSave_DicValue_String + "");
        }
        else
        {
            Answer_Return_String = Answer_Return_String.Replace("﹝SubTarget_01﹞", "");
        }
        //TargetTag
        if (QuickSave_DataMode_Bool &&
            QuickSave_Value_StringArray[2] != "Default")
        {
            string QuickSave_TagTrans_String = TagTextTranslate(QuickSave_Value_StringArray[2]);
            Answer_Return_String = Answer_Return_String.Replace("﹝TargetTag_01﹞", QuickSave_TagTrans_String + "");
        }
        else
        {
            Answer_Return_String = Answer_Return_String.Replace("﹝TargetTag_01﹞", "");
        }
        //3為類型
        //SubKey
        if (QuickSave_DataMode_Bool &&
            _Language_UIName_Dictionary.TryGetValue(QuickSave_Value_StringArray[4], out QuickSave_DicValue_String))
        {
            Answer_Return_String = Answer_Return_String.Replace("﹝SubKey_01﹞", QuickSave_DicValue_String + "");
        }
        else
        {
            Answer_Return_String = Answer_Return_String.Replace("﹝SubKey_01﹞", "");
        }
        //Key
        if (QuickSave_DataMode_Bool &&
            _Language_UIName_Dictionary.TryGetValue(QuickSave_Value_StringArray[5], out QuickSave_DicValue_String))
        {
            Answer_Return_String = Answer_Return_String.Replace("﹝Key_01﹞", QuickSave_DicValue_String + "");
        }
        else
        {
            Answer_Return_String = Answer_Return_String.Replace("﹝Key_01﹞", "");
        }
        //KeyTag
        if (QuickSave_DataMode_Bool &&
            QuickSave_Value_StringArray[6] != "Default")
        {
            string QuickSave_TagTrans_String = TagTextTranslate(QuickSave_Value_StringArray[6]);
            Answer_Return_String = Answer_Return_String.Replace("﹝KeyTag_01﹞", QuickSave_TagTrans_String + "");
        }
        else
        {
            Answer_Return_String = Answer_Return_String.Replace("﹝KeyTag_01﹞", "");
        }
        //Value
        switch (QuickSave_Type_StringArray[0])
        {
            case "Percentage":
            case "Probability":
                {
                    //百分比顯示
                    Answer_Return_String = Answer_Return_String.Replace("﹝Value_01﹞", (Value * 100) + "%");
                }
                break;
            default:
                {
                    if (QuickSave_DataMode_Bool &&
                        QuickSave_Value_StringArray[3] != "Default")
                    {
                        //百分比顯示
                        Answer_Return_String = Answer_Return_String.Replace("﹝Value_01﹞", (Value * 100) + "%");
                    }
                    else
                    {
                        //基本顯示
                        Answer_Return_String = Answer_Return_String.Replace("﹝Value_01﹞", Mathf.RoundToInt(Value).ToString());
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

    //複數標籤類 翻譯/——————————————————————————————————————————————————————————————————————
    private string TagTextTranslate(string TextInput)
    {
        //設定----------------------------------------------------------------------------------------------------
        string[] QuickSave_Split_StringArray = TextInput.Split(","[0]);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        string QuickSave_Replace_String = "";
        foreach (string CheckKey in QuickSave_Split_StringArray)
        {
            switch (CheckKey)
            {
                case "All":
                case "X":
                    QuickSave_Replace_String += _Language_Tag_Dictionary[CheckKey].Name + ",";
                    break;
                default:
                    {
                        if (CheckKey.Contains("("))
                        {
                            List<string> TagsSplit = new List<string>(CheckKey.Replace(")", "").Split("("[0]));
                            List<string> Tags = new List<string>(TagsSplit[1].Split("|"[0]));

                            string QuickSave_IncludeReplace_String = "";
                            foreach (string Tag in Tags)
                            {
                                if (_Language_Tag_Dictionary.TryGetValue(Tag, out LanguageClass Language))
                                {
                                    QuickSave_IncludeReplace_String += Language.Name + ",";
                                }
                                else
                                {
                                    QuickSave_IncludeReplace_String += Tag + ",";
                                }
                            }
                            QuickSave_IncludeReplace_String = 
                                QuickSave_IncludeReplace_String.Substring(0, QuickSave_IncludeReplace_String.Length - 1);
                            QuickSave_Replace_String += _Language_Adden_Dictionary[TagsSplit[0] + "Explanation"].
                                Replace("﹝Value_01﹞", QuickSave_IncludeReplace_String) + ",";
                            QuickSave_Replace_String =
                                QuickSave_Replace_String.Substring(0, QuickSave_Replace_String.Length - 1);
                        }
                        else
                        {
                            if (_Language_Tag_Dictionary.TryGetValue(CheckKey, out LanguageClass Language))
                            {
                                QuickSave_Replace_String = _Language_Tag_Dictionary[CheckKey].Name;
                            }
                            else
                            {
                                QuickSave_Replace_String = CheckKey;
                            }
                        }
                    }
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        QuickSave_Replace_String = _Language_Adden_Dictionary["TagExplanation"].Replace("﹝Value_01﹞", QuickSave_Replace_String);
        return QuickSave_Replace_String;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion TextmeshProTranslate
}
