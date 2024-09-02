using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;

public class _UI_Manager : MonoBehaviour
{
    #region FollowerBox
    //底下資料集——————————————————————————————————————————————————————————————————————
    //卡片管理
    public _UI_CardManager _UI_CardManager;
    //事件/對話管理
    public _UI_EventManager _UI_EventManager;
    private _World_Manager _World_Manager;
    //——————————————————————————————————————————————————————————————————————
    #endregion FollowerBox


    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————

    //攝影機----------------------------------------------------------------------------------------------------
    //攝影機本體
    public Transform _Camera_Camera_Transform;
    //追蹤目標
    public Transform _Camera_TraceTarget_Transform;
    public Vector3 _Camera_TraceTarget_Vector;
    //平移位置
    private Vector2 _Camera_MousePositionOffset_Vector;
    private Vector2 _Camera_MousePositionCenter_Vector;
    private bool _State_CameraOperating_Bool = false;
    private bool _State_CameraPanning_Bool = false;
    //縮放
    private float _Camera_ScaleDefault_Float;
    private float _Camera_ScaleOffst_Float = 0;
    //----------------------------------------------------------------------------------------------------

    //掃描器----------------------------------------------------------------------------------------------------
    //追蹤機
    GraphicRaycaster m_GraphicRaycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    public Transform _Tracer_MousePos_Transform;
    public Text _Tracer_Pos_Text;
    //滑鼠接觸的UI
    public GameObject _MouseTarget_GameObject;
    public GameObject _MouseTargeted_GameObject;
    //按下物件
    private GameObject _MouseClickDownTarget_GameObject;

    //Camp拖曳起始位置紀錄
    private Vector2 _Camp_MousePositionSave_Vector2;
    private float _Mouse_LeftClickTime_Float;
    private float _Mouse_CenterClickTime_Float;
    private float _Mouse_RightClickTime_Float;
    //----------------------------------------------------------------------------------------------------

    //狀態值----------------------------------------------------------------------------------------------------
    //Camp背景拖曳
    private bool _Camp_BackGroundPull_Bool;

    //TextEffectOning
    public Dictionary<string, _UI_TextEffect> _UI_TextEffectLocking_Dictionary = new Dictionary<string, _UI_TextEffect>();
    //New標記紀錄
    public Dictionary<string, Dictionary<string, List<_UI_HintEffect>>>  _UI_Hint_Dictionary = new Dictionary<string, Dictionary<string, List<_UI_HintEffect>>>();

    //物件
    public GameObject _UI_QuarterUnit_GameObject;
    public UIBarClass _UI_Time_Class;
    //檢查間隔
    private float _View_CheckInterval_Float = 0.1f;
    private float _View_NextCheckTime_Float = 0f;
    //----------------------------------------------------------------------------------------------------

    //場景物建類別表----------------------------------------------------------------------------------------------------
    //Camp全物件
    public Transform _UI_Camp_Transform;
    //FieldBattle全物件
    public Transform _UI_FieldBattle_Transform;

    //Camp
    public CampClass _UI_Camp_Class;

    //戰鬥UI
    public _View_Battle _View_Battle;
    //設定
    public Transform _UI_Setting_Transform;
    //對話UI
    public Transform _UI_Dialogue_Transform;
    //事件
    public Transform _UI_Event_Transform;
    //效果
    public ParticleSystem _UI_ReactEffect_Effect;

    //語言介面更新清單
    public UILanguageClass _Language_UI_Class;
    //----------------------------------------------------------------------------------------------------
    //子物件集——————————————————————————————————————————————————————————————————————
    
    //場景物件集——————————————————————————————————————————————————————————————————————
    //CampClass----------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class CampClass
    {
        //----------------------------------------------------------------------------------------------------
        //過去UI編號
        public string _UI_CampState_String;
        public string _UI_CampStateLast_String;
        //----------------------------------------------------------------------------------------------------

        //場景物建----------------------------------------------------------------------------------------------------
        //背景圖
        public Image BackImage;
        public _View_QuickView _View_QuickView;
        public _View_TinyMenu _View_TinyMenu;
        //----------------------------------------------------------------------------------------------------

        //UI清單----------------------------------------------------------------------------------------------------
        public Transform[] SummaryTransforms;
        public _UI_BTN[] BTNs;
        public _UI_TextEffect[] BTNsEffect;
        /*
         * 00：Adventure
         * 01：Equipment
         * 02：Inventory
         * 03：Alchemy
         * 04：Book
         * 05：Miio
         * 06：Tabana
         * 07：Lide
         * 08：Limu
         * 09：Choco
         * 10：Rotetis
         * 11：Nomus
         * 12：ItemInfo
         * 13：SkillInfo
         * 14：TinyMenu
        */
        //----------------------------------------------------------------------------------------------------

        #region - UISave -
        //UISave----------------------------------------------------------------------------------------------------
        public _UI_Adventure _UI_Adventure;
        public _UI_Skill _UI_Skill;
        public _UI_Equipment _UI_Equipment;
        public _View_Inventory _View_Inventory;
        public _View_Alchemy _View_Alchemy;
        public TextMeshProUGUI Miio_Summary;
        public _View_ItemInfo _View_ItemInfo;
        public _UI_SkillView _UI_SkillView;
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //----------------------------------------------------------------------------------------------------

    //IndexClass----------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class UILanguageClass
    {
        public Text[] Texts;

        public void LanguageReset()
        {
            Dictionary<string, string> Language = _World_Manager._World_GeneralManager._World_TextManager._Language_UIName_Dictionary;
            for (int a  =0; a < Texts.Length; a++)
            {
                if (Texts[a] != null)
                {
                    if (Language.TryGetValue(Texts[a].gameObject.name, out string Value))
                    {
                        Texts[a].text = Value;
                    }
                    else
                    {
                        Texts[a].text = Texts[a].name + "：NoFound";
                    }
                }
            }
        }
    }
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class UIBarClass
    {
        public Transform MainTransform;
        public Text BarKey;
        public Text BarValue;
        public Transform BarTransform;
    }
    //----------------------------------------------------------------------------------------------------
    
    //----------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class IconsClass
    {
        public Transform Store;
        public GameObject[] UnitObject;
        public Image[] UnitImage;
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion ElementBox


    #region Start
    //第一行動——————————————————————————————————————————————————————————————————————
    void Awake()
    {
        //設定變數----------------------------------------------------------------------------------------------------
        _World_Manager._UI_Manager = this;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (_World_Manager._Authority_Scene_String == "Field")
        {
            Effect("React", false);
        }
        //----------------------------------------------------------------------------------------------------

        //攝影機設置----------------------------------------------------------------------------------------------------
        //設定基本縮放
        _Camera_Camera_Transform = Camera.main.transform;
        _Camera_ScaleDefault_Float = Camera.main.orthographicSize;
        //----------------------------------------------------------------------------------------------------

        //設定變數----------------------------------------------------------------------------------------------------
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        m_GraphicRaycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = EventSystem.current;
        _MouseTargeted_GameObject = _MouseTarget_GameObject;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void ManagerStart()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager = _World_Manager._World_GeneralManager;
        if (_UI_EventManager != null)
        {
            _UI_EventManager.SystemStart();
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Dictionary<string, List<_UI_HintEffect>> QuickSave_New_Dictionary = new Dictionary<string, List<_UI_HintEffect>>();
        QuickSave_New_Dictionary.Add("Faction", new List<_UI_HintEffect>());
        QuickSave_New_Dictionary.Add("Weapon", new List<_UI_HintEffect>());
        QuickSave_New_Dictionary.Add("Item", new List<_UI_HintEffect>());
        QuickSave_New_Dictionary.Add("Concept", new List<_UI_HintEffect>());
        QuickSave_New_Dictionary.Add("Material", new List<_UI_HintEffect>());
        QuickSave_New_Dictionary.Add("Recipe", new List<_UI_HintEffect>());
        QuickSave_New_Dictionary.Add("Menu", new List<_UI_HintEffect>());
        _UI_Hint_Dictionary.Add("New", QuickSave_New_Dictionary);

        Dictionary<string, List<_UI_HintEffect>> QuickSave_Using_Dictionary = new Dictionary<string, List<_UI_HintEffect>>();
        QuickSave_Using_Dictionary.Add("Faction", new List<_UI_HintEffect>());
        QuickSave_Using_Dictionary.Add("Weapon", new List<_UI_HintEffect>());
        QuickSave_Using_Dictionary.Add("Item", new List<_UI_HintEffect>());
        QuickSave_Using_Dictionary.Add("Concept", new List<_UI_HintEffect>());
        QuickSave_Using_Dictionary.Add("Material", new List<_UI_HintEffect>());
        QuickSave_Using_Dictionary.Add("Recipe", new List<_UI_HintEffect>());
        QuickSave_Using_Dictionary.Add("Menu", new List<_UI_HintEffect>());
        _UI_Hint_Dictionary.Add("Using", QuickSave_Using_Dictionary);
        //----------------------------------------------------------------------------------------------------

        //基礎設定----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Camp":
                _Language_UI_Class.LanguageReset();
                CampAwake();
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start


    #region Update
    //總呼叫——————————————————————————————————————————————————————————————————————
    void Update()
    {
        //左鍵
        if (Input.GetMouseButtonDown(0))
        {
            MouseLeftClickDown(_MouseTarget_GameObject);
        }
        if (Input.GetMouseButton(0))
        {
            MouseLeftClick(_MouseTarget_GameObject);
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseLeftClickUp(_MouseTarget_GameObject);
        }
        //右鍵
        if (Input.GetMouseButtonDown(1))
        {
            MouseRightClickDown(_MouseTarget_GameObject);
        }
        if (Input.GetMouseButton(1))
        {
            MouseRightClick(_MouseTarget_GameObject);
        }
        if (Input.GetMouseButtonUp(1))
        {
            MouseRightClickUp(_MouseTarget_GameObject);
        }
        //中鍵
        if (Input.GetMouseButtonDown(2))
        {
            MouseCenterClickDown(_MouseTarget_GameObject);
        }
        if (Input.GetMouseButton(2))
        {
            MouseCenterClick(_MouseTarget_GameObject);
        }
        if (Input.GetMouseButtonUp(2))
        {
            MouseCenterClickUp(_MouseTarget_GameObject);
        }
        //慣例行為
        SensorWorking();
        UpadteAction();
    }
    //——————————————————————————————————————————————————————————————————————

    //連續執行事項——————————————————————————————————————————————————————————————————————
    public int _State_Wheel_Int;
    public int _State_WheelSave_Int = 0;
    private void UpadteAction()
    {
        //攝影機----------------------------------------------------------------------------------------------------
        if (_Camera_TraceTarget_Transform != null)
        {
            if (!_State_CameraPanning_Bool)
            {
                if (_State_CameraOperating_Bool ||
                    Vector2.Distance(_Camera_Camera_Transform.transform.position, _Camera_TraceTarget_Vector) > 0.1f)
                {
                    CameraTraceTargetPosSet();
                    CameraTraceSet();
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //Tracer----------------------------------------------------------------------------------------------------
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        Vector3 QuickSave_MousePos_Vector3 = _World_Manager._World_GeneralManager._Mouse_PositionOnCamera_Vector;
        if (_Tracer_MousePos_Transform != null)
        {
            float QuickSave_WindowX_Float = (QuickSave_MousePos_Vector3.x - 0.5f) * Screen.width;
            float QuickSave_WindowY_Float = (QuickSave_MousePos_Vector3.y - 0.5f) * Screen.height;
            _Tracer_MousePos_Transform.localPosition = new Vector3(QuickSave_WindowX_Float, QuickSave_WindowY_Float, 0);
            if (_Tracer_Pos_Text != null)
            {
                RectTransform QuickSave_Store_Transform = _View_Manager._Bubble_TracerStore_Transform;
                float QuickSave_BolderMinX_Float = -(Screen.width * 0.5f) - QuickSave_WindowX_Float + (QuickSave_Store_Transform.sizeDelta.x * 0.5f);
                float QuickSave_BolderMaxX_Float = (Screen.width * 0.5f) - QuickSave_WindowX_Float - (QuickSave_Store_Transform.sizeDelta.x * 0.5f);
                float QuickSave_BolderMinY_Float = (Screen.height * 0.5f) + QuickSave_WindowY_Float - (QuickSave_Store_Transform.sizeDelta.y * 0.5f);
                float QuickSave_BolderMaxY_Float = -(Screen.height * 0.5f) + QuickSave_WindowY_Float + (QuickSave_Store_Transform.sizeDelta.y * 0.5f);
                float QuickSave_StoreX = Mathf.Clamp(QuickSave_Store_Transform.sizeDelta.x * 0.5f, QuickSave_BolderMinX_Float, QuickSave_BolderMaxX_Float);
                float QuickSave_StoreY = Mathf.Clamp(QuickSave_Store_Transform.sizeDelta.y * 0.5f, QuickSave_BolderMaxY_Float, QuickSave_BolderMinY_Float);
                QuickSave_Store_Transform.localPosition = new Vector3(QuickSave_StoreX, -QuickSave_StoreY, 0);
                /*
                _Tracer_Pos_Text.text = 
                    "Change:" + ChangeTimes + 
                    "\nSetSame:" + SetSameTimes + 
                    "\nSame:" + (_MouseTarget_GameObject != _MouseTargeted_GameObject) + 
                    "\nTarget:" + _MouseTarget_GameObject + 
                    "\nTargeted:" + _MouseTargeted_GameObject;*/
            }
        }
        //----------------------------------------------------------------------------------------------------

        if (!_World_Manager._Authority_UICover_Bool)
        {
            return;
        }

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            #region Camp
            case "Camp":
                if ((!_UI_Camp_Class._View_QuickView._View_Lock_Bool && QuickSave_MousePos_Vector3.x < 0.003f)|| 
                    (_UI_Camp_Class._View_QuickView._View_Lock_Bool && QuickSave_MousePos_Vector3.x > 0.14f))
                {
                    _UI_Camp_Class._View_QuickView.QuickViewSet();
                }
                break;
            case "Field":
            case "Battle":
                if ((!_UI_Camp_Class._View_QuickView._View_Lock_Bool && QuickSave_MousePos_Vector3.x < 0.003f) ||
                    (_UI_Camp_Class._View_QuickView._View_Lock_Bool && QuickSave_MousePos_Vector3.x > 0.009f))
                {
                    _UI_Camp_Class._View_QuickView.QuickViewSet();
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //ScrollWheel----------------------------------------------------------------------------------------------------
        int QuickSave_Pinch_Int;
        _State_Wheel_Int += Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 10);
        if (_State_Wheel_Int != _State_WheelSave_Int)
        {
            QuickSave_Pinch_Int = _State_Wheel_Int - _State_WheelSave_Int;
            _State_WheelSave_Int = _State_Wheel_Int;

            if (_State_CameraPanning_Bool)//攝影機平移中
            {
                _Camera_ScaleOffst_Float += QuickSave_Pinch_Int;
            }
            else//其他
            {
                switch (_World_Manager._Authority_Scene_String)
                {
                    case "Field":
                        MouseRightClickUp(null);
                        if (_World_Manager._Mouse_PositionOnSelect_Script == null)
                        {
                            _View_Battle.UsingObjectScroll(QuickSave_Pinch_Int);
                        }
                        break;
                    case "Battle":
                        {
                            if (_World_Manager._Authority_Scene_String == "Battle")
                            {
                                if (_World_Manager._Mouse_PositionOnSelect_Script == null)
                                {
                                    switch (_Map_Manager._State_BattleState_String)
                                    {
                                        case "PlayerBehavior":
                                        case "PlayerEnchance":
                                            {
                                                _View_Battle.UsingObjectScroll(QuickSave_Pinch_Int);
                                            }
                                            return;
                                    }
                                }
                            }
                            MouseRightClickUp(null);
                        }
                        break;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Update

    #region - Camera -
    //攝影機——————————————————————————————————————————————————————————————————————
    public void ChangeTraceTarget(Transform NewTarget)
    {
        //----------------------------------------------------------------------------------------------------
        _State_CameraOperating_Bool = true;
        _Camera_TraceTarget_Transform = NewTarget;
        _Camera_MousePositionOffset_Vector = Vector2.zero;
        _Camera_ScaleOffst_Float = 0;
        //----------------------------------------------------------------------------------------------------
    }
    public void CameraTraceTargetPosSet()
    {
        _Camera_TraceTarget_Vector = new Vector3(
            _Camera_TraceTarget_Transform.position.x + _Camera_MousePositionOffset_Vector.x,
            _Camera_TraceTarget_Transform.position.y + (-1.8f) + _Camera_MousePositionOffset_Vector.y,
            _Camera_Camera_Transform.transform.position.z);
    }
    private void CameraTraceSet()
    {
        //目標追蹤----------------------------------------------------------------------------------------------------
        //平移
        _Camera_Camera_Transform.transform.position =
            Vector3.Lerp(_Camera_Camera_Transform.transform.position, 
            _Camera_TraceTarget_Vector, 
            Time.deltaTime * 5f);

        if (Vector2.Distance(_Camera_Camera_Transform.transform.position, _Camera_TraceTarget_Vector) < 0.1f)
        {
            _Camera_Camera_Transform.transform.position = _Camera_TraceTarget_Vector;
            _State_CameraOperating_Bool = false;
        }
        //縮放
        Camera.main.orthographicSize = Mathf.Clamp(_Camera_ScaleDefault_Float + _Camera_ScaleOffst_Float, 1, 20);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region SensorState
    //感測器運行與狀態機——————————————————————————————————————————————————————————————————————
    private void SensorWorking()
    {
        //運行----------------------------------------------------------------------------------------------------
        //減少頻率
        if (Time.time < _View_NextCheckTime_Float)
        {
            return;
        }
        _View_NextCheckTime_Float = Time.time + _View_CheckInterval_Float;
        //Raycast
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_GraphicRaycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0)
        {
            _MouseTarget_GameObject = results[0].gameObject;
        }
        else
        {
            _MouseTarget_GameObject = null;
        }
        //----------------------------------------------------------------------------------------------------

        //狀態呼叫----------------------------------------------------------------------------------------------------
        if (_MouseTarget_GameObject != _MouseTargeted_GameObject)
        {
            GameObject QuickSave_PastObject_GameObject = _MouseTargeted_GameObject;
            _MouseTargeted_GameObject = _MouseTarget_GameObject;
            if (_MouseClickDownTarget_GameObject == null)
            {
                UIExit(QuickSave_PastObject_GameObject);
                UIEnter(_MouseTarget_GameObject);
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion SensorState


    #region SensorBehavior
    #region LeftClick
    //左鍵按下——————————————————————————————————————————————————————————————————————
    private void MouseLeftClickDown(GameObject CheckObject)
    {
        //設定----------------------------------------------------------------------------------------------------
        _MouseClickDownTarget_GameObject = CheckObject;
        _Mouse_LeftClickTime_Float = 0;
        //----------------------------------------------------------------------------------------------------

        //執行動作----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String) 
        {
            #region Camp
            case "Camp":
                if (CheckObject != null)
                {
                    switch (CheckObject.name)
                    {
                        #region - Alchemy -
                        case "Alchemy_Processing":
                            {
                                if (_UI_Camp_Class._UI_CampState_String == "Alchemy_ProcessingRest")
                                {
                                    return;
                                }
                                _UI_Camp_Class._UI_CampState_String = "Alchemy_Processing";
                                _View_AlchemyUpdating QuickSave_Updating_Script =
                                    _UI_Camp_Class._View_Alchemy.gameObject.AddComponent<_View_AlchemyUpdating>();
                                QuickSave_Updating_Script._View_ProcessType_String =
                                    _UI_Camp_Class._View_Alchemy.
                                    Alchemy_OnRecipeProcesss_ScriptsList[_UI_Camp_Class._View_Alchemy._Alchemy_NowProcess_Int].Type;
                                QuickSave_Updating_Script._View_Bar00_Transform =
                                    _UI_Camp_Class._View_Alchemy._View_ProcessBar_ImageList[0].rectTransform;
                                QuickSave_Updating_Script._View_Bar01_Transform =
                                    _UI_Camp_Class._View_Alchemy._View_ProcessBar_ImageList[1].rectTransform;
                                _UI_Camp_Class._View_Alchemy.Alchemy_OnProcess_Script = QuickSave_Updating_Script;
                            }
                            break;
                            #endregion
                    }
                }
                break;
            #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //左鍵按住——————————————————————————————————————————————————————————————————————
    private void MouseLeftClick(GameObject CheckObject)
    {
        //----------------------------------------------------------------------------------------------------
        _Mouse_LeftClickTime_Float += Time.deltaTime;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Camp":
                {
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //左鍵抬起——————————————————————————————————————————————————————————————————————
    private void MouseLeftClickUp(GameObject CheckObject)
    {
        //必定結束----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Camp":
                { 
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //非同位跳出----------------------------------------------------------------------------------------------------
        if (_MouseClickDownTarget_GameObject != null)
        {
            switch (_MouseClickDownTarget_GameObject.name)
            {
                #region - Alchemy -
                case "Alchemy_Processing":
                    {
                        if (_UI_Camp_Class._UI_CampState_String == "Alchemy_Processing")
                        {
                            if (_UI_Camp_Class._View_Alchemy.Alchemy_OnProcess_Script != null)
                            {
                                _UI_Camp_Class._View_Alchemy.Alchemy_OnProcess_Script.EndUpdate();
                            }
                        }
                    }
                    _MouseClickDownTarget_GameObject = null;
                    return;
                #endregion
            }

            if (_MouseClickDownTarget_GameObject != CheckObject)
            {
                UIExit(_MouseClickDownTarget_GameObject);
                UIEnter(_MouseTarget_GameObject);
                _MouseClickDownTarget_GameObject = null;
                return;
            }
        }
        _MouseClickDownTarget_GameObject = null;
        //----------------------------------------------------------------------------------------------------

        //執行動作----------------------------------------------------------------------------------------------------
        
        if (CheckObject == null)
        {
            if (_World_Manager._Authority_DialogueClick_Bool)
            {
                switch (_World_Manager._Authority_Scene_String)
                {
                    #region - Field -
                    case "Camp":
                        _UI_EventManager.DialogueNext();
                        break;
                    case "Field":
                        switch (_Map_Manager._State_FieldState_String)
                        {
                            case "EventMiddle":
                                _UI_EventManager.DialogueNext();
                                break;
                        }
                        break;
                        #endregion
                }
            }
            return;
        }
        else
        {
            if (_World_Manager._Authority_DialogueClick_Bool)
            {
                if (CheckObject.name.Contains("BTNBack_"))
                {
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        #region - Field -
                        case "Camp":
                            _UI_EventManager.DialogueNext();
                            break;
                        case "Field":
                            switch (_Map_Manager._State_FieldState_String)
                            {
                                case "EventMiddle":
                                    _UI_EventManager.DialogueNext();
                                    break;
                            }
                            break;
                            #endregion
                    }
                }
                return;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        CheckObject.TryGetComponent(out _UI_TextEffect QuickSave_TextEffect_Script);
        switch (CheckObject.name)
        {
            #region - Setting - 
            case "BTN_Setting":
                //UISet("Setting");
                break;
            case "BTN_Operate":
                break;
            case "BTN_Display":
                break;
            case "BTN_Sound":
                break;
            case "BTN_Return":
            case "SettingFrame":
                UISet("Last");
                break;
            case "BTN_Exit":
                print("QuitGame");
                Application.Quit();
                break;
            #endregion

            #region - Event -
            case "EventSure":
                switch (_Map_Manager._State_FieldState_String)
                {
                    case "EventFrame":
                        if (_UI_EventManager._Event_Selected_Bool)
                        {
                            _UI_EventManager.EndEvent();
                        }
                        break;
                }
                break;
            #endregion

            #region - Index - 
            case "BTN_Start":
            case "BTN_Continue":
                if (_World_Manager._Authority_UICover_Bool)
                {
                    _World_Manager._World_GeneralManager._World_ScenesManager.SwitchScenes("Main");
                }
                break;
            #endregion

            #region - Camp - 
            #region - TinyMenu -
            case "_View_ButtonUnit":
                {
                    CheckObject.TryGetComponent(out _View_ButtonUnit QuickSave_ButtonUnit_Script);
                    QuickSave_ButtonUnit_Script._OnLeftClickButton();
                }
                break;
            #endregion

            #region - MenuBTN -
            case "BTN_Adventure":
            case "BTN_Equipment":
            case "BTN_Skill":
            case "BTN_Inventory":
            case "BTN_Alchemy":
            case "BTN_Book":
            case "BTN_Miio":
            case "BTN_Tabana":
            case "BTN_Lide":
            case "BTN_Limu":
            case "BTN_Choco":
            case "BTN_Rotetis":
            case "BTN_Nomus":
                _UI_Camp_Class._View_QuickView.QuickViewSet();
                TextEffectDictionarySet("CampMenu", QuickSave_TextEffect_Script);
                UISet(CheckObject.name.Replace("BTN_",""));
                break;

            case "BTNBack_Adventure":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[0]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            case "BTNBack_Equipment":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[1]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            case "BTNBack_Skill":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[2]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            case "BTNBack_Inventory":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[3]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            case "BTNBack_Alchemy":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[4]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            case "BTNBack_Book":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[5]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            case "BTNBack_Miio":
                {
                    TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[6]);
                    if (_UI_Camp_Class._UI_CampState_String == "Null")
                    {
                        UISet(CheckObject.name.Replace("BTNBack_", ""));
                    }
                }
                break;
            case "BTNBack_Tabana":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[7]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            case "BTNBack_Lide":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[8]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            case "BTNBack_Limu":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[9]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            case "BTNBack_Choco":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[10]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            case "BTNBack_Rotetis":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[11]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            case "BTNBack_Nomus":
                TextEffectDictionarySet("CampMenu", _UI_Camp_Class.BTNsEffect[12]);
                if (_UI_Camp_Class._UI_CampState_String == "Null")
                {
                    UISet(CheckObject.name.Replace("BTNBack_", ""));
                }
                break;
            #endregion

            #region - ItemInfo -
            case "Item_ConceptUnit(Clone)":
                {
                    CheckObject.TryGetComponent(out _Item_ConceptUnit QuickSave_ConceptUnit_Script);
                    TextEffectDictionarySet("ItemUnitInfo", QuickSave_TextEffect_Script);
                    _UI_Camp_Class._View_TinyMenu.TinyMenuSet(
                        _UI_Camp_Class._UI_CampState_String, QuickSave_ConceptUnit_Script._Basic_Object_Script._Basic_Source_Class);

                }
                break;
            case "Item_WeaponUnit(Clone)":
                {
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Field":
                        case "Battle":
                            break;
                        default:
                            CheckObject.TryGetComponent(out _Item_WeaponUnit QuickSave_WeaponUnit_Script);
                            TextEffectDictionarySet("ItemUnitInfo", QuickSave_TextEffect_Script);
                            _UI_Camp_Class._View_TinyMenu.TinyMenuSet(
                                _UI_Camp_Class._UI_CampState_String, QuickSave_WeaponUnit_Script._Basic_Object_Script._Basic_Source_Class);
                            break;
                    }
                }
                break;
            case "Item_ItemUnit(Clone)":
                {
                    CheckObject.TryGetComponent(out _Item_ItemUnit QuickSave_ItemUnit_Script);
                    TextEffectDictionarySet("ItemUnitInfo", QuickSave_TextEffect_Script);
                    _UI_Camp_Class._View_TinyMenu.TinyMenuSet(
                        _UI_Camp_Class._UI_CampState_String, QuickSave_ItemUnit_Script._Basic_Object_Script._Basic_Source_Class);
                }
                break;
            case "Item_MaterialUnit(Clone)":
                {
                    CheckObject.TryGetComponent(out _Item_MaterialUnit QuickSave_MaterialUnit_Script);
                    TextEffectDictionarySet("ItemUnitInfo", QuickSave_TextEffect_Script);
                    _UI_Camp_Class._View_TinyMenu.TinyMenuSet(
                        _UI_Camp_Class._UI_CampState_String, QuickSave_MaterialUnit_Script._Basic_Object_Script._Basic_Source_Class);
                }
                break;

            case "Item_RecipeUnit(Clone)":
                {
                    CheckObject.TryGetComponent(out _Item_RecipeUnit QuickSave_RecipeUnit_Script);
                    if (QuickSave_RecipeUnit_Script != _UI_Camp_Class._View_Alchemy.Alchemy_OnRecipe_Script)
                    {
                        TextEffectDictionarySet("CampAlchemyInfo", QuickSave_TextEffect_Script);
                        _UI_Camp_Class._View_Alchemy.Alchemy_RecipeStartSet(QuickSave_RecipeUnit_Script);
                    }
                }
                break;
            #endregion

            #region - Adventure -
            case "World_MapUnit(Clone)":
                CheckObject.TryGetComponent(out _UI_MapUnit QuickSave_RegionUnit_Script);
                UISet("Null");
                QuickSave_RegionUnit_Script.SwitchScenes();
                break;
            #endregion

            #region - Inventory -
            case "BTN_Weapons":
            case "BTN_Items":
            case "BTN_Concepts":
            case "BTN_Materials":
                TextEffectDictionarySet("InventoryMenu", QuickSave_TextEffect_Script);
                UISet(CheckObject.name.Replace("BTN_", "Inventory_"));
                break;
            #endregion

            #region - Alchemy -
            case "Alchemy_Material_00":
            case "Alchemy_Material_01":
            case "Alchemy_Material_02":
            case "Alchemy_Material_03":
            //case "Alchemy_Material_04":(催化劑?)
                int QuickSave_Number_Int = int.Parse(CheckObject.name.Replace("Alchemy_Material_", ""));
                if (QuickSave_Number_Int >= _UI_Camp_Class._View_Alchemy.Alchemy_OnRecipeMaterials_ScriptsList.Count)
                {
                    return;
                }
                if (QuickSave_Number_Int != _UI_Camp_Class._View_Alchemy.Alchemy_MaterialSelect_Int)
                {
                    TextEffectDictionarySet("CampAlchemyMaterial", QuickSave_TextEffect_Script);
                    _UI_Camp_Class._View_Alchemy.Alchemy_MaterialInventorySet(QuickSave_Number_Int);
                }
                break;
            case "Alchemy_ProcessStart":
                if (_UI_Camp_Class._View_Alchemy.MaterialsCheck())
                {
                    UISet("Alchemy_Process");
                }
                break;
            #endregion
            #endregion

            #region - Using -
                /*
            case "_UI_PreviousUsing":
                {
                    if (!(_World_Manager._Authority_Scene_String == "Battle"&&
                        _Map_Manager._State_BattleState_String == "PlayerBehavior"))
                    {
                        MouseRightClickUp(null);
                    }
                    _View_Battle.UsingObjectSet("Previous");
                }
                break;
            case "_UI_NextUsing":
                {
                    if (!(_World_Manager._Authority_Scene_String == "Battle" &&
                        _Map_Manager._State_BattleState_String == "PlayerBehavior"))
                    {
                        MouseRightClickUp(null);
                    }
                    _View_Battle.UsingObjectSet("Next");
                }
                break;*/
            #endregion

            #region - Card -
            case "Card_Unit":
                if (!_World_Manager._Authority_CardClick_Bool)
                {
                    return;
                }

                if (CheckObject.TryGetComponent(out _View_Card_Unit QuickSave_CardView_Script))
                {
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Field":
                            switch (_Map_Manager._State_FieldState_String)
                            {
                                case "SelectExplore":
                                    if (!QuickSave_CardView_Script._Basic_Owner_Script._State_ExploreCanUse_Bool)
                                    {
                                        return;
                                    }
                                    QuickSave_CardView_Script.MouseClickOn("Explore");
                                    break;
                                case "SelectRange":
                                    if (!QuickSave_CardView_Script._Basic_Owner_Script._State_ExploreCanUse_Bool)
                                    {
                                        return;
                                    }
                                    if (QuickSave_CardView_Script._Basic_Owner_Script != 
                                        _UI_CardManager._Card_UsingCard_Script)
                                    {
                                        _UI_CardManager._Card_UsingCard_Script._Basic_View_Script.MouseClickOff("Explore", MouseCover: false);
                                        QuickSave_CardView_Script.MouseClickOn("Explore");
                                    }
                                    else
                                    {
                                        QuickSave_CardView_Script.MouseClickOff("Explore", MouseCover: true);
                                    }
                                    break;
                                case "EventSelect":
                                case "EventFrame":
                                    {
                                        if (_UI_CardManager._Card_UsingCard_Script == null)
                                        {
                                            if (!QuickSave_CardView_Script._Basic_Owner_Script._State_ExploreCanUse_Bool)
                                            {
                                                return;
                                            }
                                            //設定為使用招式(Plot)
                                            QuickSave_CardView_Script.MouseClickOn("Explore");
                                        }
                                        else
                                        {
                                            //判定是否點擊使用中的卡片
                                            if (QuickSave_CardView_Script._Basic_Owner_Script != 
                                                _UI_CardManager._Card_UsingCard_Script)
                                            {
                                                if (_UI_CardManager._Card_EventingCard_ScriptsList.
                                                    Contains(QuickSave_CardView_Script._Basic_Owner_Script))
                                                {
                                                    //取消附魔
                                                    QuickSave_CardView_Script.MouseClickOff("Explore", MouseCover: true);
                                                }
                                                else if (QuickSave_CardView_Script._Basic_Owner_Script._State_ExploreCanUse_Bool)
                                                {
                                                    //新增附魔
                                                    QuickSave_CardView_Script.MouseClickOn("Explore");
                                                }
                                            }
                                            else
                                            {
                                                //取消附魔
                                                List<_UI_Card_Unit> QuickSave_CardBoard_ScriptList =
                                                    new List<_UI_Card_Unit>(_UI_CardManager._Card_EventingCard_ScriptsList);
                                                for (int a = 0; a < QuickSave_CardBoard_ScriptList.Count; a++)
                                                {
                                                    QuickSave_CardBoard_ScriptList[a]._Basic_View_Script.MouseClickOff("Explore", MouseCover: false);
                                                }
                                                _UI_CardManager._Card_UsingCard_Script._Basic_View_Script.MouseClickOff("Explore", MouseCover: true);
                                                _UI_CardManager.BoardRefresh(_World_Manager._Object_Manager._Object_Player_Script);
                                            }
                                        }
                                    }
                                    break;
                            }
                            break;
                        case "Battle":
                            switch (_Map_Manager._State_BattleState_String)
                            {
                                case "PlayerBehavior":
                                    if (!QuickSave_CardView_Script._Basic_Owner_Script._State_BehaviorCanUse_Bool)
                                    {
                                        return;
                                    }
                                    QuickSave_CardView_Script.MouseClickOn("Behavior");
                                    break;
                                case "PlayerEnchance":
                                    //判定是否點擊使用中的卡片
                                    if (QuickSave_CardView_Script._Basic_Owner_Script != _UI_CardManager._Card_UsingCard_Script)
                                    {
                                        if (_UI_CardManager._Card_UsingCard_Script._State_EnchanceStore_ScriptsList.
                                            Contains(QuickSave_CardView_Script._Basic_Owner_Script))
                                        {
                                            //取消附魔
                                            QuickSave_CardView_Script.MouseClickOff("Enchance", MouseCover: true);
                                        }
                                        else if(QuickSave_CardView_Script._Basic_Owner_Script._State_EnchanceCanUse_Bool)
                                        {
                                            //新增附魔
                                            QuickSave_CardView_Script.MouseClickOn("Enchance");
                                        }
                                    }
                                    else
                                    {
                                        //取消附魔
                                        List<_UI_Card_Unit> QuickSave_CardBoard_ScriptList = 
                                            _UI_CardManager._Card_UsingCard_Script._State_EnchanceStore_ScriptsList;

                                        for (int a = 0; a < QuickSave_CardBoard_ScriptList.Count; a++)
                                        {
                                            if (QuickSave_CardBoard_ScriptList[a] != _UI_CardManager._Card_UsingCard_Script)
                                            {
                                                QuickSave_CardBoard_ScriptList[a]._Basic_View_Script.MouseClickOff("Enchance", MouseCover: false);
                                            }
                                        }
                                        _UI_CardManager._Card_UsingCard_Script._Basic_View_Script.MouseClickOff("Behavior", MouseCover: true);
                                        _UI_CardManager.BoardRefresh(_World_Manager._Object_Manager._Object_Player_Script);
                                    }
                                    break;
                            }
                            break;
                    }
                }
                break;

            case "UI_Standby":
                {
                    _Object_CreatureUnit QuickSave_Player_Script =
                        _World_Manager._Object_Manager._Object_Player_Script;
                    _Item_ConceptUnit QuickSave_Concept_Script =
                        QuickSave_Player_Script._Object_Inventory_Script._Item_EquipConcepts_Script;

                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Field":
                            {
                                switch (_Map_Manager._State_FieldState_String)
                                {
                                    case "SelectExplore":
                                        {
                                            _UI_TextEffect QuickSave_EffectText_Script = CheckObject.GetComponent<_UI_TextEffect>();
                                            QuickSave_EffectText_Script.CoverOff();
                                            int QuickSave_StandbyTime_Int =
                                                QuickSave_Concept_Script.Key_StandbyTime();
                                            QuickSave_Player_Script.BuildBack(FieldTime: QuickSave_StandbyTime_Int, Standby:true);
                                        }
                                        break;
                                    case "EventSelect":
                                        {
                                            int QuickSave_Deal_Int = QuickSave_Concept_Script.Key_Deal();
                                            _UI_TextEffect QuickSave_EffectText_Script = CheckObject.GetComponent<_UI_TextEffect>();
                                            QuickSave_EffectText_Script.CoverOff();
                                            _UI_CardManager.
                                                CardDeal("Normal", QuickSave_Deal_Int, null,
                                                null, QuickSave_Player_Script._Basic_Object_Script._Basic_Source_Class, null,
                                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                            //print("增加消耗時間");
                                        }
                                        break;
                                }
                            }
                            break;
                        case "Battle":
                            if (_Map_Manager._State_BattleState_String == "PlayerBehavior")
                            {
                                _UI_TextEffect QuickSave_EffectText_Script = CheckObject.GetComponent<_UI_TextEffect>();
                                QuickSave_EffectText_Script.CoverOff();
                                QuickSave_Player_Script.BuildBack(Standby:true);
                                _View_Battle._View_StandbyStore_Transform.localPosition = new Vector3(0, 0, 0);
                            }
                            break;
                    }
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion LeftClick


    #region RightClick
    //右鍵按下——————————————————————————————————————————————————————————————————————
    private void MouseRightClickDown(GameObject CheckObject)
    {
        //設定----------------------------------------------------------------------------------------------------
        _MouseClickDownTarget_GameObject = CheckObject;
        _Mouse_RightClickTime_Float = 0;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Camp":
                {
                    if (_UI_Camp_Class._UI_CampState_String == "Null")
                    {
                        _Camp_MousePositionSave_Vector2 = Input.mousePosition;
                        _Camp_BackGroundPull_Bool = true;
                    }
                }
                break;

            case "Field":
            case "Battle":
                {
                    //攝影機
                    if (!_State_CameraOperating_Bool &&
                        _World_Manager._Authority_CameraSet_Bool)
                    {
                        _State_CameraPanning_Bool = true;
                        _Camera_MousePositionCenter_Vector =
                            _World_Manager._Mouse_PositionOnCamera_Vector;//按下位置
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //右鍵按住——————————————————————————————————————————————————————————————————————
    private void MouseRightClick(GameObject CheckObject)
    {
        //----------------------------------------------------------------------------------------------------
        _Mouse_RightClickTime_Float += Time.deltaTime;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Camp":
                {
                    if (_Camp_BackGroundPull_Bool)
                    {
                        //背景拖曳
                        RectTransform QuickSave_Rect_Transform = _UI_Camp_Class.BackImage.rectTransform;
                        float QuickSave_Speed_Float = 0.008f;
                        Vector2 QuickSave_ClampRange_Vector2 = new Vector2((QuickSave_Rect_Transform.sizeDelta.x - 1920) * 0.5f, (QuickSave_Rect_Transform.sizeDelta.y - 1080) * 0.5f);
                        Vector2 QuickSave_MousePos_Vector2 = Input.mousePosition;
                        QuickSave_Rect_Transform.localPosition = new Vector3
                            (Mathf.Clamp(QuickSave_Rect_Transform.localPosition.x + (_Camp_MousePositionSave_Vector2.x - QuickSave_MousePos_Vector2.x) * QuickSave_Speed_Float * _World_Manager._Config_PullReverse_Short, -QuickSave_ClampRange_Vector2.x, QuickSave_ClampRange_Vector2.x),
                            Mathf.Clamp(QuickSave_Rect_Transform.localPosition.y + (_Camp_MousePositionSave_Vector2.y - QuickSave_MousePos_Vector2.y) * QuickSave_Speed_Float * _World_Manager._Config_PullReverse_Short, -QuickSave_ClampRange_Vector2.y, QuickSave_ClampRange_Vector2.y),
                            0);
                    }
                }
                break;

            case "Field":
            case "Battle":
                {
                    //攝影機
                    if (_State_CameraPanning_Bool)
                    {
                        //平移
                        _Camera_MousePositionOffset_Vector +=
                            _World_Manager._Config_CameraPositionSpeed_Int *
                            _World_Manager._Config_PullReverse_Short *
                            Camera.main.orthographicSize *
                            0.001f *
                            (_World_Manager._Mouse_PositionOnCamera_Vector - _Camera_MousePositionCenter_Vector);
                        CameraTraceTargetPosSet();
                        CameraTraceSet();
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //右鍵抬起——————————————————————————————————————————————————————————————————————
    private void MouseRightClickUp(GameObject CheckObject)
    {
        //攝影機----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Camp":
                if (_Camp_BackGroundPull_Bool)
                {
                    _Camp_BackGroundPull_Bool = false;
                }
                break;

            case "Field":
            case "Battle":
                {
                    //攝影機
                    if (_State_CameraPanning_Bool)
                    {
                        _State_CameraPanning_Bool = false;
                        if (_Mouse_RightClickTime_Float < 0.2f)
                        {
                            _Camera_MousePositionOffset_Vector = Vector2.zero;
                            _Camera_ScaleOffst_Float = 0;
                        }
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //非同位跳出----------------------------------------------------------------------------------------------------
        if (_MouseClickDownTarget_GameObject != null && _MouseClickDownTarget_GameObject != CheckObject)
        {
            switch (_MouseClickDownTarget_GameObject.name)
            {
                default:
                    UIExit(_MouseClickDownTarget_GameObject);
                    UIEnter(_MouseTarget_GameObject);
                    break;
            }
            _MouseClickDownTarget_GameObject = null;
            return;
        }
        _MouseClickDownTarget_GameObject = null;
        //----------------------------------------------------------------------------------------------------

        //執行動作----------------------------------------------------------------------------------------------------
        if (CheckObject == null)
        {
            switch (_World_Manager._Authority_Scene_String)
            {
            }
            return;
        }
        switch (CheckObject.name)
        {
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion RightClick


    #region CenterClick

    //中鍵按下——————————————————————————————————————————————————————————————————————
    private void MouseCenterClickDown(GameObject CheckObject)
    {
        //設定----------------------------------------------------------------------------------------------------
        _MouseClickDownTarget_GameObject = CheckObject;
        _Mouse_CenterClickTime_Float = 0;
        //----------------------------------------------------------------------------------------------------

    }
    //——————————————————————————————————————————————————————————————————————

    //中鍵按住——————————————————————————————————————————————————————————————————————
    private void MouseCenterClick(GameObject CheckObject)
    {
        _Mouse_CenterClickTime_Float += Time.deltaTime;
    }
    //——————————————————————————————————————————————————————————————————————

    //中鍵抬起——————————————————————————————————————————————————————————————————————
    private void MouseCenterClickUp(GameObject CheckObject)
    {
        //非同位跳出----------------------------------------------------------------------------------------------------
        if (_MouseClickDownTarget_GameObject != null && _MouseClickDownTarget_GameObject != CheckObject)
        {
            switch (_MouseClickDownTarget_GameObject.name)
            {
                default:
                    UIExit(_MouseClickDownTarget_GameObject);
                    UIEnter(_MouseTarget_GameObject);
                    break;
            }
            _MouseClickDownTarget_GameObject = null;
            return;
        }
        _MouseClickDownTarget_GameObject = null;
        //----------------------------------------------------------------------------------------------------

        //執行動作----------------------------------------------------------------------------------------------------
        if (CheckObject == null)
        {
            switch (_World_Manager._Authority_Scene_String)
            {
                #region - Camp -
                case "Camp":
                    if (_World_Manager._Authority_DialogueClick_Bool)
                    {
                        _UI_EventManager.DialogueNext(true);
                        return;
                    }
                    if (_UI_Camp_Class._View_TinyMenu._View_OnMenu_String != "null")
                    {
                        _UI_Camp_Class._View_TinyMenu.CancelSet();
                        return;
                    }
                    switch (_UI_Camp_Class._UI_CampState_String)
                    {
                        case "Adventure":
                        case "Adventure_Map":
                        case "Skill":
                        case "Skill_Faction":
                        case "Equipment":
                        case "Equipment_Main":
                        case "Iventory":
                        case "Inventory_Weapons":
                        case "Inventory_Items":
                        case "Inventory_Concepts":
                        case "Inventory_Materials":
                        case "Alchemy":
                        case "Alchemy_Recipe":
                        case "Book":
                        case "Miio":
                        case "Tabana":
                        case "Lide":
                        case "Limu":
                        case "Choco":
                        case "Rotetis":
                        case "Nomus":
                            TextEffectDictionarySet("CampMenu", null);
                            UISet("Null");
                            break;

                        case "Skill_Skills":
                            TextEffectDictionarySet("FactionUnitInfo", null);
                            UISet("Skill_Faction");
                            break;

                        case "Equipment_CreatureFactions":
                        case "Equipment_WeaponFactions":
                        case "Equipment_Weapons":
                        case "Equipment_Items":
                            TextEffectDictionarySet("EquipmentSelect", null);
                            UISet("Equipment_Main");
                            break;

                        case "Alchemy_Alchemy":
                            UISet("Alchemy_Recipe");
                            break;
                        case "Alchemy_Material":
                            UISet("Alchemy_Alchemy");
                            break;
                    }
                    break;
                #endregion

                #region - Field -
                case "Field":
                    switch (_Map_Manager._State_FieldState_String)
                    {
                        case "SelectRange":
                            _UI_CardManager._Card_UsingCard_Script._Basic_View_Script.
                                MouseClickOff("Explore", MouseCover: false);
                            break;
                        case "EventMiddle":
                            if (_World_Manager._Authority_DialogueClick_Bool)
                            {
                                _UI_EventManager.DialogueNext(true);
                                return;
                            }
                            break;
                        case "EventFrame":
                            {
                                if (_UI_CardManager._Card_UsingCard_Script != null)
                                {
                                    _UI_Card_Unit QuickSave_Card_Script = _UI_CardManager._Card_UsingCard_Script;
                                    List<_UI_Card_Unit> QuickSave_CardBoard_ScriptList =
                                        new List<_UI_Card_Unit>(_UI_CardManager._Card_EventingCard_ScriptsList);

                                    for (int a = 0; a < QuickSave_CardBoard_ScriptList.Count; a++)
                                    {
                                        QuickSave_CardBoard_ScriptList[a]._Basic_View_Script.MouseClickOff("Explore", MouseCover: false);
                                    }
                                    QuickSave_Card_Script._Basic_View_Script.MouseClickOff("Explore", MouseCover: false);
                                    _UI_CardManager.BoardRefresh(_World_Manager._Object_Manager._Object_Player_Script);
                                }
                            }
                            break;
                    }
                    break;
                #endregion

                #region - Battle -
                case "Battle":
                    switch (_Map_Manager._State_BattleState_String)
                    {
                        case "PlayerBehavior":
                            _View_Battle.FocusSet(_World_Manager._Object_Manager._Object_Player_Script._Card_UsingObject_Script);
                            break;
                        case "PlayerEnchance":
                            {
                                _Object_CreatureUnit QuickSave_Player_Script = _World_Manager._Object_Manager._Object_Player_Script;
                                _UI_Card_Unit QuickSave_Card_Script = _UI_CardManager._Card_UsingCard_Script;
                                List<_UI_Card_Unit> QuickSave_CardBoard_ScriptList = QuickSave_Card_Script._State_EnchanceStore_ScriptsList;

                                for (int a = 0; a < QuickSave_CardBoard_ScriptList.Count; a++)
                                {
                                    if (QuickSave_CardBoard_ScriptList[a] != QuickSave_Card_Script)
                                    {
                                        QuickSave_CardBoard_ScriptList[a]._Basic_View_Script.MouseClickOff("Enchance", MouseCover: false);
                                    }
                                }
                                QuickSave_Card_Script._Basic_View_Script.MouseClickOff("Behavior", MouseCover: false);
                                _UI_CardManager.BoardRefresh(QuickSave_Player_Script);
                            }
                            //----------------------------------------------------------------------------------------------------
                            break;
                    }
                    break;
                    #endregion
            }
            return;
        }

        if (CheckObject.name.Contains("BTNBack_"))
        {

            switch (_World_Manager._Authority_Scene_String)
            {
                case "Camp":
                    {
                        if (_World_Manager._Authority_DialogueClick_Bool)
                        {
                            _UI_EventManager.DialogueNext(true);
                            return;
                        }
                        if (_UI_Camp_Class._View_TinyMenu._View_OnMenu_String != "null")
                        {
                            _UI_Camp_Class._View_TinyMenu.CancelSet();
                            return;
                        }
                        switch (_UI_Camp_Class._UI_CampState_String)
                        {
                            case "Adventure":
                            case "Adventure_Map":
                            case "Skill":
                            case "Skill_Faction":
                            case "Equipment":
                            case "Equipment_Main":
                            case "Iventory":
                            case "Inventory_Weapons":
                            case "Inventory_Items":
                            case "Inventory_Concepts":
                            case "Inventory_Materials":
                            case "Alchemy":
                            case "Alchemy_Recipe":
                            case "Book":
                            case "Miio":
                            case "Tabana":
                            case "Lide":
                            case "Limu":
                            case "Choco":
                            case "Rotetis":
                            case "Nomus":
                                TextEffectDictionarySet("CampMenu", null);
                                UISet("Null");
                                break;

                            case "Skill_Skills":
                                TextEffectDictionarySet("FactionUnitInfo", null);
                                UISet("Skill_Faction");
                                break;

                            case "Equipment_CreatureFactions":
                            case "Equipment_WeaponFactions":
                            case "Equipment_Weapons":
                            case "Equipment_Items":
                                TextEffectDictionarySet("EquipmentSelect", null);
                                UISet("Equipment_Main");
                                break;

                            case "Alchemy_Alchemy":
                                UISet("Alchemy_Recipe");
                                break;
                            case "Alchemy_Material":
                                UISet("Alchemy_Alchemy");
                                break;
                        }
                    }
                    break;
                case "Field":
                    {
                        switch (_Map_Manager._State_FieldState_String)
                        {
                            case "EventMiddle":
                                if (_World_Manager._Authority_DialogueClick_Bool)
                                {
                                    _UI_EventManager.DialogueNext(true);
                                }
                                break;
                        }
                    }
                    break;
            }
        }

        switch (CheckObject.name)
        {
            #region - Setting -
            case "SettingFrame":
                UISet("Last");
                break;
            #endregion

            #region - Event -
            case "EventSure":
                switch (_Map_Manager._State_FieldState_String)
                {
                    case "EventFrame":
                        if (_UI_EventManager._Event_Selected_Bool)
                        {
                            _UI_EventManager.EndEvent();
                        }
                        break;
                }
                break;
            #endregion

            #region - Card -
            case "Card_Unit":
                if (CheckObject.TryGetComponent(out _View_Card_Unit QuickSave_CardView_Script))
                {
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Field":
                            switch (_Map_Manager._State_FieldState_String)
                            {
                                case "SelectRange":
                                    if (QuickSave_CardView_Script._Basic_Owner_Script !=
                                        _UI_CardManager._Card_UsingCard_Script)
                                    {
                                        _UI_CardManager._Card_UsingCard_Script._Basic_View_Script.
                                            MouseClickOff("Explore", MouseCover: false);
                                    }
                                    else
                                    {
                                        QuickSave_CardView_Script.MouseClickOff("Explore", MouseCover: true);
                                    }
                                    break;
                                case "EventFrame":
                                    {
                                        _UI_Card_Unit QuickSave_Card_Script = _UI_CardManager._Card_UsingCard_Script;
                                        List<_UI_Card_Unit> QuickSave_CardBoard_ScriptList =
                                            new List<_UI_Card_Unit>(_UI_CardManager._Card_EventingCard_ScriptsList);
                                        QuickSave_CardBoard_ScriptList.Add(QuickSave_Card_Script);
                                        for (int a = 0; a < QuickSave_CardBoard_ScriptList.Count; a++)
                                        {
                                            bool IsCover = false;
                                            if (QuickSave_CardBoard_ScriptList[a] == QuickSave_CardView_Script._Basic_Owner_Script)
                                            {
                                                IsCover = true;
                                            }
                                            QuickSave_CardBoard_ScriptList[a]._Basic_View_Script.MouseClickOff("Explore", MouseCover: IsCover);
                                        }
                                        //QuickSave_Card_Script._Basic_View_Script.MouseOverOut("Explore");
                                        _UI_CardManager.BoardRefresh(_World_Manager._Object_Manager._Object_Player_Script);
                                    }
                                    break;
                            }
                            break;
                        case "Battle":
                            switch (_Map_Manager._State_BattleState_String)
                            {
                                case "PlayerEnchance":
                                    {
                                        _Object_CreatureUnit QuickSave_Player_Script = _World_Manager._Object_Manager._Object_Player_Script;
                                        _UI_Card_Unit QuickSave_Card_Script = _UI_CardManager._Card_UsingCard_Script;
                                        List<_UI_Card_Unit> QuickSave_CardBoard_ScriptList =
                                            QuickSave_Card_Script._State_EnchanceStore_ScriptsList;
                                        QuickSave_CardBoard_ScriptList.Add(QuickSave_Card_Script);
                                        for (int a = 0; a < QuickSave_CardBoard_ScriptList.Count; a++)
                                        {
                                            bool IsCover = false;
                                            if (QuickSave_CardBoard_ScriptList[a] == QuickSave_CardView_Script._Basic_Owner_Script)
                                            {
                                                IsCover = true;
                                            }
                                            if (QuickSave_CardBoard_ScriptList[a] != QuickSave_Card_Script)
                                            {
                                                QuickSave_CardBoard_ScriptList[a]._Basic_View_Script.MouseClickOff("Enchance", MouseCover: IsCover);
                                            }
                                        }
                                        if (QuickSave_Card_Script == QuickSave_CardView_Script._Basic_Owner_Script)
                                        {
                                            QuickSave_Card_Script._Basic_View_Script.MouseClickOff("Behavior", MouseCover: true);
                                        }
                                        if (CheckObject != QuickSave_Card_Script.gameObject)
                                        {
                                            QuickSave_Card_Script._Basic_View_Script.MouseOverOut("Enchance");
                                        }
                                        _UI_CardManager.BoardRefresh(QuickSave_Player_Script);
                                    }
                                    break;
                            }
                            break;
                    }
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion CenterClick

    #region - SomthingClick -
    #region SomethingLeftClick
    //動作機：地板左鍵點擊——————————————————————————————————————————————————————————————————————
    public void SelectLeftClick(_Map_SelectUnit Select)
    {
        //----------------------------------------------------------------------------------------------------
        if (_MouseTarget_GameObject != null)
        {
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                {
                    switch (_Map_Manager._State_FieldState_String)
                    {
                        case "SelectRange":
                            if (Select._State_InRange_Bool || Select._State_InRangePath_Bool)
                            {
                                //使用卡片
                                _UI_CardManager._Card_UsingCard_Script.UseCardStart("Field", Select._Basic_Coordinate_Class);
                            }
                            break;
                    }
                }
                break;
            case "Battle":
                {
                    switch (_Map_Manager._State_BattleState_String)
                    {
                        case "PlayerEnchance":
                            if (Select._State_InRange_Bool || Select._State_InRangePath_Bool)
                            {
                                //使用卡片
                                _UI_CardManager._Card_UsingCard_Script.UseCardStart("Battle", Select._Basic_Coordinate_Class);
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //生物點擊——————————————————————————————————————————————————————————————————————
    public void ObjectLeftClick(_Map_BattleObjectUnit BattleObject = null)
    {
        //----------------------------------------------------------------------------------------------------
        if (_MouseTarget_GameObject != null)
        {
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Map_Manager._State_BattleState_String)
        {
            case "PlayerBehavior":
                _View_Battle.FocusSet(BattleObject);
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion SomethingLeftClick
    
    #region SomethingRightClick
    //動作機：地板右鍵點擊——————————————————————————————————————————————————————————————————————
    public void SelectRightClick(_Map_SelectUnit Select)
    {
        //----------------------------------------------------------------------------------------------------
        if (_MouseTarget_GameObject != null)
        {
            return;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //生物點擊——————————————————————————————————————————————————————————————————————
    public void ObjectRightClick(_Map_BattleObjectUnit BattleObject = null)
    {
        //----------------------------------------------------------------------------------------------------
        if (_MouseTarget_GameObject != null)
        {
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Map_Manager._State_BattleState_String)
        {
            case "PlayerBehavior":
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion SomethingRightClick
       
    #region SomethingMiddleClick
    //動作機：地板中鍵點擊——————————————————————————————————————————————————————————————————————
    public void SelectCenterClick(_Map_SelectUnit Select)
    {
        if (_MouseTarget_GameObject != null)
        {
            return;
        }
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion SomethingMiddleClick
    #endregion

    //動作機：進入——————————————————————————————————————————————————————————————————————
    #region MouseEnter
    private void UIEnter(GameObject CheckObject)
    {
        //跳出----------------------------------------------------------------------------------------------------
        if (CheckObject == null)
        {
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (CheckObject.TryGetComponent(out _UI_TextEffect QuickSave_Text_Script))
        {
            QuickSave_Text_Script.CoverOn();
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (CheckObject.name)
        {
            #region - UI -
            case "Effect_ObjectUnit(Clone)":
                {
                    //顯示說明
                    CheckObject.TryGetComponent(out _Effect_EffectObjectUnit QuickSave_EffectObjectUnit_Script);
                    _World_Manager._View_Manager.TracerBubbleOn(
                        QuickSave_EffectObjectUnit_Script._Basic_Source_Class, 
                        QuickSave_EffectObjectUnit_Script._Basic_Key_String, 
                        QuickSave_EffectObjectUnit_Script._Basic_Language_Class);
                }
                break;

            case "Item_SyndromeUnit(Clone)":
                {
                    //顯示說明
                    CheckObject.TryGetComponent(out _Item_SyndromeUnit QuickSave_SyndromeUnit_Script);
                    _World_Manager._View_Manager.TracerBubbleOn(
                        QuickSave_SyndromeUnit_Script._Basic_Source_Class, 
                        QuickSave_SyndromeUnit_Script._Basic_Key_String,
                        QuickSave_SyndromeUnit_Script._Basic_Language_ClassList[QuickSave_SyndromeUnit_Script._Syndrome_Rank_Int]);
                }
                break;
            #endregion

            #region - TinyMenu -
            case "_View_ButtonUnit":
                {
                    CheckObject.TryGetComponent(out _View_ButtonUnit QuickSave_ButtonUnit_Script);
                    QuickSave_ButtonUnit_Script._OnMouseEnter();
                }
                break;
            #endregion

            #region - ItemInfo -
            case "Item_WeaponUnit(Clone)":
                {
                    CheckObject.TryGetComponent(out _Item_WeaponUnit QuickSave_WeaponUnit_Script);
                    _UI_Camp_Class._View_ItemInfo.ItemInfoSet(QuickSave_WeaponUnit_Script._Basic_Object_Script);
                }
                break;
            case "Item_ItemUnit(Clone)":
                {
                    CheckObject.TryGetComponent(out _Item_ItemUnit QuickSave_ItemUnit_Script);
                    _UI_Camp_Class._View_ItemInfo.ItemInfoSet(QuickSave_ItemUnit_Script._Basic_Object_Script);
                }
                break;
            case "Item_ConceptUnit(Clone)":
                {
                    CheckObject.TryGetComponent(out _Item_ConceptUnit QuickSave_ConceptUnit_Script);
                    _UI_Camp_Class._View_ItemInfo.ItemInfoSet(QuickSave_ConceptUnit_Script._Basic_Object_Script);
                }
                break;
            case "Item_MaterialUnit(Clone)":
                {
                    CheckObject.TryGetComponent(out _Item_MaterialUnit QuickSave_MaterialUnit_Script);
                    _UI_Camp_Class._View_ItemInfo.ItemInfoSet(QuickSave_MaterialUnit_Script._Basic_Object_Script);
                }
                break;

            case "Bubble_UISummary_Medium":
            case "Bubble_UISummary_Catalyst":
            case "Bubble_UISummary_Consciousness":
            case "Bubble_UISummary_Vitality":
            case "Bubble_UISummary_Strength":
            case "Bubble_UISummary_Precision":
            case "Bubble_UISummary_Speed":
            case "Bubble_UISummary_Luck":
                _UI_Camp_Class._View_TinyMenu.
                    TracerBubbleOn("UISummary", CheckObject.name.Replace("Bubble_UISummary_", ""));
                break;

            case "Bubble_AlchemyTag_0":
            case "Bubble_AlchemyTag_1":
            case "Bubble_AlchemyTag_2":
            case "Bubble_AlchemyTag_3":
            case "Bubble_AlchemyTag_4":
            case "Bubble_AlchemyTag_5":
            case "Bubble_AlchemyTag_6":
            case "Bubble_AlchemyTag_7":
            case "Bubble_AlchemyTag_8":
            case "Bubble_AlchemyTag_9":
            case "Bubble_AlchemyTag_10":
            case "Bubble_AlchemyTag_11":
                _UI_Camp_Class._View_TinyMenu.
                    TracerBubbleOn("AlchemyTag", CheckObject.name.Replace("Bubble_AlchemyTag_", ""));
                break;
            case "Bubble_AlchemySpecialAffix_0":
            case "Bubble_AlchemySpecialAffix_1":
            case "Bubble_AlchemySpecialAffix_2":
            case "Bubble_AlchemySpecialAffix_3":
                _UI_Camp_Class._View_TinyMenu.
                    TracerBubbleOn("AlchemySpecialAffix", CheckObject.name.Replace("Bubble_AlchemySpecialAffix_", ""));
                break;

            case "Bubble_Tag_0":
            case "Bubble_Tag_1":
            case "Bubble_Tag_2":
            case "Bubble_Tag_3":
            case "Bubble_Tag_4":
            case "Bubble_Tag_5":
            case "Bubble_Tag_6":
            case "Bubble_Tag_7":
            case "Bubble_Tag_8":
            case "Bubble_Tag_9":
            case "Bubble_Tag_10":
            case "Bubble_Tag_11":
                _UI_Camp_Class._View_TinyMenu.
                    TracerBubbleOn("Tag", CheckObject.name.Replace("Bubble_Tag_", ""));
                break;
            case "Bubble_SpecialAffix_0":
            case "Bubble_SpecialAffix_1":
            case "Bubble_SpecialAffix_2":
            case "Bubble_SpecialAffix_3":
                _UI_Camp_Class._View_TinyMenu.
                    TracerBubbleOn("SpecialAffix", CheckObject.name.Replace("Bubble_SpecialAffix_",""));
                break;

            #endregion

            #region - SkillInfo -
            case "Skill_PassiveUnit(Clone)":
                //CheckObject.TryGetComponent(out _Skill_PassiveUnit QuickSave_PassiveUnit_Script);
                //_UI_Camp_Class._UI_SkillView.SkillInfoSet(QuickSave_PassiveUnit_Script);
                break;
            case "Skill_ExploreUnit(Clone)":
                CheckObject.TryGetComponent(out _Skill_ExploreUnit QuickSave_ExploreUnit_Script);
                _UI_Camp_Class._UI_SkillView.SkillInfoSet(QuickSave_ExploreUnit_Script);
                break;
            case "Skill_BehaviorUnit(Clone)":
                CheckObject.TryGetComponent(out _Skill_BehaviorUnit QuickSave_BehaviorUnit_Script);
                _UI_Camp_Class._UI_SkillView.SkillInfoSet(QuickSave_BehaviorUnit_Script);
                break;
            case "Skill_EnchanceUnit(Clone)":
                CheckObject.TryGetComponent(out _Skill_EnchanceUnit QuickSave_EnchanceUnit_Script);
                _UI_Camp_Class._UI_SkillView.SkillInfoSet(QuickSave_EnchanceUnit_Script);
                break;
            #endregion

            #region - Card -
            case "Card_Unit":
                if (!_World_Manager._Authority_CardClick_Bool)
                {
                    return;
                }

                if (CheckObject.TryGetComponent(out _View_Card_Unit QuickSave_CardView_Script))
                {
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Field":
                            switch (_Map_Manager._State_FieldState_String)
                            {
                                case "SelectExplore":
                                case "EventSelect":
                                case "EventFrame":
                                    QuickSave_CardView_Script.MouseOverIn("Explore");
                                    break;
                                case "SelectRange":
                                    if (QuickSave_CardView_Script._Basic_Owner_Script != _UI_CardManager._Card_UsingCard_Script)
                                    {
                                        QuickSave_CardView_Script.MouseOverIn("Explore");
                                        _UI_CardManager._Card_UsingCard_Script._Basic_View_Script.MouseOverOut("Explore");
                                    }
                                    break;
                            }
                            break;
                        case "Battle":
                            switch (_Map_Manager._State_BattleState_String)
                            {
                                case "PlayerBehavior":
                                    QuickSave_CardView_Script.MouseOverIn("Behavior");
                                    break;
                                case "PlayerEnchance":
                                    if (QuickSave_CardView_Script._Basic_Owner_Script != _UI_CardManager._Card_UsingCard_Script)//滑入以選擇無效
                                    {
                                        QuickSave_CardView_Script.MouseOverIn("Enchance");
                                    }
                                    break;
                            }
                            break;
                    }
                }
                break;

            case "UI_Sequence":
                if (!_World_Manager._Authority_CardClick_Bool)
                {
                    return;
                }
                if (_Map_Manager._State_BattleState_String == "PlayerBehavior")
                {
                    _Map_BattleSequenceUnit QuickSave_SequenceUnit_Script =
                        CheckObject.GetComponentInParent<_Map_BattleSequenceUnit>();
                    QuickSave_SequenceUnit_Script.MouseIn();
                }
                break;

            case "UI_Standby":
                switch (_World_Manager._Authority_Scene_String)
                {
                    case "Field":
                        break;
                    case "Battle":
                        if (_Map_Manager._State_BattleState_String == "PlayerBehavior" &&
                            !_World_Manager._Map_Manager._State_Reacting_Bool)
                        {
                            _Map_BattleRound _Map_BattleRound = _World_Manager._Map_Manager._Map_BattleRound;

                            _Object_CreatureUnit QuickSave_Player_Script = 
                                _World_Manager._Object_Manager._Object_Player_Script;
                            _Item_ConceptUnit QuickSave_Concept_Script = 
                                QuickSave_Player_Script._Object_Inventory_Script._Item_EquipConcepts_Script;
                            _Map_BattleObjectUnit QuickSave_Object_Script = QuickSave_Concept_Script._Basic_Object_Script;
                            //視覺設置
                            _View_Battle._View_StandbyStore_Transform.localPosition = new Vector3(0, 60, 0);
                            //回合設置
                            int QuickSave_DelayStandby_Int =
                                QuickSave_Concept_Script.Key_DelayStandby(ContainTimeOffset: false);
                            RoundElementClass QuickSave_RoundUnit_Class = QuickSave_Object_Script._Round_Unit_Class;
                            RoundSequenceUnitClass QuickSave_RoundSequence_Class =
                                new RoundSequenceUnitClass
                                {
                                    Type = "Preview",
                                    Owner = QuickSave_Object_Script,
                                    RoundUnit = new List<RoundElementClass> { QuickSave_RoundUnit_Class }
                                };
                            QuickSave_RoundUnit_Class.DelayTime = QuickSave_DelayStandby_Int;
                            QuickSave_Object_Script._Round_GroupUnit_Class = QuickSave_RoundSequence_Class;
                            _Map_BattleRound.RoundSequenceSet(
                                QuickSave_Object_Script._Round_GroupUnit_Class, null);
                            //視覺
                            _Map_BattleRound.SequenceView();
                        }
                        break;
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion MouseEnter
    //——————————————————————————————————————————————————————————————————————

    //動作機：離開——————————————————————————————————————————————————————————————————————
    #region MouseExit
    public void UIExit(GameObject CheckObject)
    {
        //跳出----------------------------------------------------------------------------------------------------
        if (CheckObject == null)
        {
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (CheckObject.TryGetComponent(out _UI_TextEffect QuickSave_Text_Script))
        {
            QuickSave_Text_Script.CoverOff();
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (CheckObject.name)
        {
            #region - UI -
            case "Effect_ObjectUnit(Clone)":
            case "Item_SyndromeUnit(Clone)":
                //關閉說明
                _World_Manager._View_Manager.TracerBubbleOff();
                break;
            #endregion

            #region - TinyMenu -
            case "_View_ButtonUnit":
                {
                    CheckObject.TryGetComponent(out _View_ButtonUnit QuickSave_ButtonUnit_Script);
                    QuickSave_ButtonUnit_Script._OnMouseExit();
                }
                break;
            #endregion

            #region - ItemInfo -
            case "Item_WeaponUnit(Clone)":
            case "Item_ItemUnit(Clone)":
            case "Item_ConceptUnit(Clone)":
            case "Item_MaterialUnit(Clone)":
                _UI_Camp_Class._View_ItemInfo.ItemInfoOut();
                break;

            case "Bubble_UISummary_Medium":
            case "Bubble_UISummary_Catalyst":
            case "Bubble_UISummary_Consciousness":
            case "Bubble_UISummary_Vitality":
            case "Bubble_UISummary_Strength":
            case "Bubble_UISummary_Precision":
            case "Bubble_UISummary_Speed":
            case "Bubble_UISummary_Luck":
            case "Bubble_Tag_0":
            case "Bubble_Tag_1":
            case "Bubble_Tag_2":
            case "Bubble_Tag_3":
            case "Bubble_Tag_4":
            case "Bubble_Tag_5":
            case "Bubble_Tag_6":
            case "Bubble_Tag_7":
            case "Bubble_Tag_8":
            case "Bubble_Tag_9":
            case "Bubble_Tag_10":
            case "Bubble_Tag_11":
            case "Bubble_AlchemyTag_0":
            case "Bubble_AlchemyTag_1":
            case "Bubble_AlchemyTag_2":
            case "Bubble_AlchemyTag_3":
            case "Bubble_AlchemyTag_4":
            case "Bubble_AlchemyTag_5":
            case "Bubble_AlchemyTag_6":
            case "Bubble_AlchemyTag_7":
            case "Bubble_AlchemyTag_8":
            case "Bubble_AlchemyTag_9":
            case "Bubble_AlchemyTag_10":
            case "Bubble_AlchemyTag_11":
            case "Bubble_SpecialAffix_0":
            case "Bubble_SpecialAffix_1":
            case "Bubble_SpecialAffix_2":
            case "Bubble_SpecialAffix_3":
            case "Bubble_AlchemySpecialAffix_0":
            case "Bubble_AlchemySpecialAffix_1":
            case "Bubble_AlchemySpecialAffix_2":
            case "Bubble_AlchemySpecialAffix_3":
                _World_Manager._View_Manager.TracerBubbleOff();
                break;
            #endregion

            #region - SkillInfo -
            case "Skill_PassiveUnit(Clone)":
            case "Skill_ExploreUnit(Clone)":
            case "Skill_BehaviorUnit(Clone)":
            case "Skill_EnchanceUnit(Clone)":
                _UI_Camp_Class._UI_SkillView.SkillInfoOut();
                break;
            #endregion

            #region - Card -
            case "Card_Unit":
                {
                    if (!_World_Manager._Authority_CardClick_Bool)
                    {
                        return;
                    }

                    if (CheckObject.TryGetComponent(out _View_Card_Unit QuickSave_CardView_Script))
                    {
                        switch (_World_Manager._Authority_Scene_String)
                        {
                            case "Field":
                                switch (_Map_Manager._State_FieldState_String)
                                {
                                    case "SelectExplore":
                                    case "EventSelect":
                                    case "EventFrame":
                                        QuickSave_CardView_Script.MouseOverOut("Explore");
                                        break;
                                    case "SelectRange":
                                        if (QuickSave_CardView_Script._Basic_Owner_Script != _UI_CardManager._Card_UsingCard_Script)
                                        {
                                            QuickSave_CardView_Script.MouseOverOut("Explore");
                                            _UI_CardManager._Card_UsingCard_Script._Basic_View_Script.MouseOverIn("Explore");
                                        }
                                        break;
                                }
                                break;
                            case "Battle":
                                switch (_Map_Manager._State_BattleState_String)
                                {
                                    case "PlayerBehavior":
                                        if (QuickSave_CardView_Script._Basic_Owner_Script != _UI_CardManager._Card_UsingCard_Script)
                                        {
                                            QuickSave_CardView_Script.MouseOverOut("Behavior");
                                        }
                                        break;
                                    case "PlayerEnchance":
                                        if (QuickSave_CardView_Script._Basic_Owner_Script != _UI_CardManager._Card_UsingCard_Script)
                                        {
                                            QuickSave_CardView_Script.MouseOverOut("Enchance");
                                        }
                                        break;
                                }
                                break;
                        }
                    }
                }
                break;

            case "UI_Sequence":
                if (!_World_Manager._Authority_CardClick_Bool)
                {
                    return;
                }

                if (_Map_Manager._State_BattleState_String == "PlayerBehavior")
                {
                    _Map_BattleSequenceUnit QuickSave_SequenceUnit_Script = 
                        CheckObject.GetComponentInParent<_Map_BattleSequenceUnit>();
                    QuickSave_SequenceUnit_Script.MouseOut();
                }
                break;

            case "UI_Standby":
                switch (_World_Manager._Authority_Scene_String)
                {
                    case "Field":
                        break;
                    case "Battle":
                        if (
                            _Map_Manager._State_BattleState_String == "PlayerBehavior" &&
                            !_World_Manager._Map_Manager._State_Reacting_Bool)
                        {
                            _Map_BattleRound _Map_BattleRound = _World_Manager._Map_Manager._Map_BattleRound;

                            _Object_CreatureUnit QuickSave_Player_Script =
                                _World_Manager._Object_Manager._Object_Player_Script;
                            _Item_ConceptUnit QuickSave_Concept_Script =
                                QuickSave_Player_Script._Object_Inventory_Script._Item_EquipConcepts_Script;
                            _Map_BattleObjectUnit QuickSave_Object_Script = QuickSave_Concept_Script._Basic_Object_Script;
                            //視覺設置
                            _View_Battle._View_StandbyStore_Transform.localPosition = new Vector3(0, 0, 0);
                            //延遲時間
                            QuickSave_Object_Script._Round_Unit_Class.DelayTime = 0;
                            _Map_BattleRound.RoundSequenceSet(
                                null, QuickSave_Object_Script._Round_GroupUnit_Class);
                            //視覺
                            _Map_BattleRound.SequenceView();
                        }
                        break;
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion MouseExit
    //——————————————————————————————————————————————————————————————————————
    #endregion SensorBehavior

    #region UIVoid
    //——————————————————————————————————————————————————————————————————————
    //CampMenu：Camp左方欄位
    public void TextEffectDictionarySet(string Type,  _UI_TextEffect Target)
    {
        //----------------------------------------------------------------------------------------------------
        if (_UI_TextEffectLocking_Dictionary.TryGetValue(Type, out _UI_TextEffect Value))
        {
            //跳出
            if (Target != null)
            {
                switch (Type)
                {
                    case "InventoryMenu":
                        if (Target == Value)
                        {
                            return;
                        }
                        break;
                }
            }
            //設置
            Value._Effect_LockText_Bool = false;
            if (Target == Value )
            {
                _UI_TextEffectLocking_Dictionary.Remove(Type);
                return;
            }
            if (Target == null)
            {
                _UI_TextEffectLocking_Dictionary.Remove(Type);
                Value.CoverOff();
                return;
            }

            Value.CoverOff();
            _UI_TextEffectLocking_Dictionary[Type] = Target;
            Target.CoverOn();
            Target._Effect_LockText_Bool = true;
        }
        else
        {
            if (Target != null)
            {
                Target.CoverOn();
                Target._Effect_LockText_Bool = true;
                _UI_TextEffectLocking_Dictionary.Add(Type, Target);
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //Camp——————————————————————————————————————————————————————————————————————

    public void UISet(string UICode)
    {
        _View_Manager _View_Manager = _World_Manager._View_Manager;

        //離開視窗行為----------------------------------------------------------------------------------------------------
        switch (_UI_Camp_Class._UI_CampState_String)
        {
            case "Null":
                break;

            #region - Adventure -
            case "Adventure":
                {
                    if (!UICode.Contains("Adventure_"))
                    {
                        _UI_Camp_Class.SummaryTransforms[0].gameObject.SetActive(false);
                    }
                }
                break;
            case "Adventure_Map":
                if (!UICode.Contains("Adventure_"))
                {
                    _UI_Camp_Class._UI_Adventure.Adventure_AdventureSave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[0].gameObject.SetActive(false);
                }
                if (UICode == "Adventure")
                {
                    UICode = "Null";
                }
                break;
            #endregion

            #region - Equipment -
            case "Equipment":
                if (!UICode.Contains("Equipment_"))
                {
                    _UI_Camp_Class.SummaryTransforms[1].gameObject.SetActive(false);
                }
                break;
            case "Equipment_Main":
                if (!UICode.Contains("Equipment_"))
                {
                    _UI_Camp_Class._UI_Equipment.Equipment_EquipmentSave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[1].gameObject.SetActive(false);
                }
                if (UICode == "Equipment")
                {
                    UICode = "Null";
                }
                break;
            case "Equipment_CreatureFactions":
            case "Equipment_WeaponFactions":
                if (!UICode.Contains("Equipment_"))
                {
                    _UI_Camp_Class._UI_Equipment.Equipment_EquipmentSave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[1].gameObject.SetActive(false);
                }
                if (UICode == "Equipment_Main")
                {
                    _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[1].gameObject.SetActive(false);
                }
                if (UICode == "Equipment")
                {
                    UICode = "Null";
                }
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[2].gameObject.SetActive(false);
                break;
            case "Equipment_Weapons":
                if (!UICode.Contains("Equipment_"))
                {
                    _UI_Camp_Class._UI_Equipment.Equipment_EquipmentSave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[1].gameObject.SetActive(false);
                }
                if (UICode == "Equipment_Main")
                {
                    _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[1].gameObject.SetActive(false);
                }
                if (UICode == "Equipment")
                {
                    UICode = "Null";
                }
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[3].gameObject.SetActive(false);
                break;
            case "Equipment_Items":
                if (!UICode.Contains("Equipment_"))
                {
                    _UI_Camp_Class._UI_Equipment.Equipment_EquipmentSave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[1].gameObject.SetActive(false);
                }
                if (UICode == "Equipment_Main")
                {
                    _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[1].gameObject.SetActive(false);
                }
                if (UICode == "Equipment")
                {
                    UICode = "Null";
                }
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[4].gameObject.SetActive(false);
                break;
            #endregion

            #region - Skill -
            case "Skill":
                if (!UICode.Contains("Skill_"))
                {
                    _UI_Camp_Class.SummaryTransforms[2].gameObject.SetActive(false);
                }
                break;
            case "Skill_Faction":
                if (!UICode.Contains("Skill_"))
                {
                    _UI_Camp_Class._UI_Skill.Skill_SkillSave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[2].gameObject.SetActive(false);

                }
                if (UICode == "Skill")
                {
                    UICode = "Null";
                }
                _UI_Camp_Class._UI_Skill.Skill_SummaryTransforms[0].gameObject.SetActive(false);
                break;
            case "Skill_Skills":
                if (!UICode.Contains("Skill_"))
                {
                    _UI_Camp_Class._UI_Skill.Skill_SkillSave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[2].gameObject.SetActive(false);

                }
                if (UICode == "Skill")
                {
                    UICode = "Null";
                }
                if (_UI_Camp_Class._View_TinyMenu.Select_Faction_Script != null)
                {
                    _Skill_FactionUnit QuickSave_Faction_Script = _UI_Camp_Class._View_TinyMenu.Select_Faction_Script;
                    //視覺設計
                }

                _UI_Camp_Class._UI_Skill.Skill_SummaryTransforms[1].gameObject.SetActive(false);
                break;
            #endregion

            #region - Inventory -
            case "Inventory":
                if (!UICode.Contains("Inventory_"))
                {
                    _UI_Camp_Class.SummaryTransforms[3].gameObject.SetActive(false);
                }
                break;
            case "Inventory_Weapons":
                if (!UICode.Contains("Inventory_"))
                {
                    _UI_Camp_Class._View_TinyMenu.CancelSet();
                    _UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[3].gameObject.SetActive(false);

                }
                if (UICode == "Inventory")
                {
                    UICode = "Null";
                }
                _UI_Camp_Class._View_Inventory.Inventory_SummaryTransforms[0].gameObject.SetActive(false);
                break;
            case "Inventory_Items":
                if (!UICode.Contains("Inventory_"))
                {
                    _UI_Camp_Class._View_TinyMenu.CancelSet();
                    _UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[3].gameObject.SetActive(false);
                }
                if (UICode == "Inventory")
                {
                    UICode = "Null";
                }
                _UI_Camp_Class._View_Inventory.Inventory_SummaryTransforms[1].gameObject.SetActive(false);
                break;
            case "Inventory_Concepts":
                if (!UICode.Contains("Inventory_"))
                {
                    _UI_Camp_Class._View_TinyMenu.CancelSet();
                    _UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[3].gameObject.SetActive(false);
                }
                if (UICode == "Inventory")
                {
                    UICode = "Null";
                }
                _UI_Camp_Class._View_Inventory.Inventory_SummaryTransforms[2].gameObject.SetActive(false);
                break;
            case "Inventory_Materials":
                if (!UICode.Contains("Inventory_"))
                {
                    _UI_Camp_Class._View_TinyMenu.CancelSet();
                    _UI_Camp_Class._View_Inventory.Inventory_InventorySave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[3].gameObject.SetActive(false);
                }
                if (UICode == "Inventory")
                {
                    UICode = "Null";
                }
                _UI_Camp_Class._View_Inventory.Inventory_SummaryTransforms[3].gameObject.SetActive(false);
                break;
            #endregion

            #region - Alchemy -
            case "Alchemy":
                if (!UICode.Contains("Alchemy_"))
                {
                    _UI_Camp_Class.SummaryTransforms[4].gameObject.SetActive(false);
                }
                break;
            case "Alchemy_Recipe":
                if (!UICode.Contains("Alchemy_"))
                {
                    _UI_Camp_Class._View_TinyMenu.CancelSet();
                    _UI_Camp_Class._View_Alchemy.Alchemy_AlchemySave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[4].gameObject.SetActive(false);
                }
                if (UICode == "Alchemy_Alchemy")
                {
                    break;
                }
                if (UICode == "Alchemy")
                {
                    UICode = "Null";
                }
                _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[0].gameObject.SetActive(false);
                break;
            case "Alchemy_Alchemy":
                _View_Alchemy _View_Alchemy = _UI_Camp_Class._View_Alchemy;
                if (!UICode.Contains("Alchemy_"))
                {
                    _UI_Camp_Class._View_Alchemy.Alchemy_AlchemySave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[0].gameObject.SetActive(false);
                    _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[1].gameObject.SetActive(false);
                    _UI_Camp_Class.SummaryTransforms[4].gameObject.SetActive(false);
                }
                switch (UICode)
                {
                    case "Alchemy":
                        UICode = "Null";
                        break;
                    case "Alchemy_Recipe":
                        TextEffectDictionarySet("CampAlchemyInfo", null);
                        if (_View_Alchemy.Alchemy_OnRecipe_Script != null)
                        {
                            _View_Alchemy.Alchemy_OnRecipe_Script._View_Hint_Script.HintSet("UnUsing", "Recipe");
                            for (int a = 0; a < _View_Alchemy._Alchemy_Material_ScriptsArray.Length; a++)
                            {
                                if (_View_Alchemy._Alchemy_Material_ScriptsArray[a] != null)
                                {
                                    _View_Alchemy._Alchemy_Material_ScriptsArray[a]._View_Hint_Script.HintSet("UnUsing", "Material");
                                    _View_Alchemy._Alchemy_Material_ScriptsArray[a] = null;
                                }
                            }
                            _View_Alchemy.Alchemy_OnRecipe_Script = null;
                        }
                        _View_Alchemy.Alchemy_OnRecipeMaterials_ScriptsList = null;
                        _View_Alchemy.Alchemy_SummaryTransforms[1].gameObject.SetActive(false);
                        break;
                    case "Alchemy_Material":
                        _View_Alchemy.Alchemy_SummaryTransforms[0].gameObject.SetActive(false);
                        break;
                }
                break;
            case "Alchemy_Material":
                if (!UICode.Contains("Alchemy_"))
                {
                    _UI_Camp_Class._View_Alchemy.Alchemy_AlchemySave_String = _UI_Camp_Class._UI_CampState_String;
                    _UI_Camp_Class.SummaryTransforms[4].gameObject.SetActive(false);
                }
                else
                {
                    if (UICode == "Alchemy_Alchemy")
                    {
                        TextEffectDictionarySet("CampAlchemyMaterial", null);
                        _UI_Camp_Class._View_Alchemy.Alchemy_MaterialSelect_Int = 65535;
                        _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[2].gameObject.SetActive(false);
                        _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[3].gameObject.SetActive(false);
                        _UI_Camp_Class._UI_CampState_String = "Alchemy_Alchemy";
                        UISet("Alchemy_Recipe");
                        return;
                    }
                }
                if (UICode == "Alchemy")
                {
                    UICode = "Null";
                }
                break;
            #endregion

            #region MainMenu
            case "Book":
                _UI_Camp_Class.SummaryTransforms[5].gameObject.SetActive(false);
                break;
            case "Miio":
                break;
            case "Tabana":
                _UI_Camp_Class.SummaryTransforms[7].gameObject.SetActive(false);
                break;
            case "Lide":
                _UI_Camp_Class.SummaryTransforms[8].gameObject.SetActive(false);
                break;
            case "Limu":
                _UI_Camp_Class.SummaryTransforms[9].gameObject.SetActive(false);
                break;
            case "Choco":
                _UI_Camp_Class.SummaryTransforms[10].gameObject.SetActive(false);
                break;
            case "Rotetis":
                _UI_Camp_Class.SummaryTransforms[11].gameObject.SetActive(false);
                break;
            case "Nomus":
                _UI_Camp_Class.SummaryTransforms[12].gameObject.SetActive(false);
                break;

            case "Setting":
                _UI_Setting_Transform.gameObject.SetActive(false);
                break;
            #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //設定中立變數----------------------------------------------------------------------------------------------------
        if (UICode == "Last")
        {
            UICode = _UI_Camp_Class._UI_CampStateLast_String;
            _UI_Camp_Class._UI_CampStateLast_String = _UI_Camp_Class._UI_CampState_String;
            _UI_Camp_Class._UI_CampState_String = UICode;
            return;
        }
        _UI_Camp_Class._UI_CampStateLast_String = _UI_Camp_Class._UI_CampState_String;
        _UI_Camp_Class._UI_CampState_String = UICode;
        //----------------------------------------------------------------------------------------------------

        //進入視窗行為----------------------------------------------------------------------------------------------------
        RectTransform QuickSave_Rect_Transform = _UI_Camp_Class.BackImage.rectTransform;
        _Item_Object_Inventory QuickSave_Inventory_Script = _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script;
        string QuickSave_Map_String = 
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        switch (UICode)
        {
            #region Camp
            case "Null":
                break;

            #region - Adventure -
            case "Adventure":
                {
                    QuickSave_Rect_Transform.localPosition =
                        -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String, UICode);
                    if (_UI_EventManager._Dialogue_DialogueCamp_Dictionary.TryGetValue(UICode, out List<string> Values))
                    {
                        if (Values.Count > 0)
                        {
                            _UI_EventManager.DialogueCamp(UICode);
                            return;
                        }
                    }
                    _UI_Camp_Class.SummaryTransforms[0].gameObject.SetActive(true);
                    UISet(_UI_Camp_Class._UI_Adventure.Adventure_AdventureSave_String);
                }
                break;
            case "Adventure_Map":
                _UI_Camp_Class._UI_Adventure.Adventure_SummaryTransforms[1].gameObject.SetActive(true);
                _UI_Camp_Class._UI_Adventure.Adventure_SummaryTransforms[2].gameObject.SetActive(true);
                break;
            #endregion

            #region - Equipment -
                /*
            case "Equipment":
                QuickSave_Rect_Transform.localPosition = 
                    -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String, UICode);
                _UI_Camp_Class.SummaryTransforms[1].gameObject.SetActive(true);
                UISet(_UI_Camp_Class._UI_Equipment.Equipment_EquipmentSave_String);
                break;
            case "Equipment_Main":
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[0].gameObject.SetActive(true);
                _UI_Camp_Class._UI_Equipment.EquipmentStartSet();
                break;
            case "Equipment_CreatureFactions":
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[1].gameObject.SetActive(true);
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[2].gameObject.SetActive(true);
                print("SkillFaction Creature");
                //QuickSave_Inventory_Script.SkillFilterSet("Faction", "Type_Creature");
                break;
            case "Equipment_Weapons":
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[1].gameObject.SetActive(true);
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[3].gameObject.SetActive(true);
                QuickSave_Inventory_Script.ItemFilterSet("Weapons","Equipment");
                break;
            case "Equipment_WeaponFactions":
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[1].gameObject.SetActive(true);
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[2].gameObject.SetActive(true);
                print("SkillFaction Weapon");
                //QuickSave_Inventory_Script.SkillFilterSet("Faction", "Allow_Weapon", Weapon:QuickSave_Inventory_Script._Items_EquipWeapons_ScriptsArray[_UI_Camp_Class._UI_Equipment.Equipment_OnSelect_Int]);
                break;
            case "Equipment_Items":
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[1].gameObject.SetActive(true);
                _UI_Camp_Class._UI_Equipment.Equipment_SummaryTransforms[4].gameObject.SetActive(true);
                QuickSave_Inventory_Script.ItemFilterSet("Items", "Equipment");
                break;*/
            #endregion

            #region - Skill -
                /*
            case "Skill":
                QuickSave_Rect_Transform.localPosition = 
                    -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String,UICode);
                _UI_Camp_Class.SummaryTransforms[2].gameObject.SetActive(false);
                UISet(_UI_Camp_Class._UI_Skill.Skill_SkillSave_String);
                break;
            case "Skill_Faction":
                _UI_Camp_Class._UI_Skill.Skill_SummaryTransforms[0].gameObject.SetActive(true);
                break;
            case "Skill_Skills":
                _UI_Camp_Class._UI_Skill.Skill_SummaryTransforms[1].gameObject.SetActive(true);
                break;*/
            #endregion

            #region - Collection -
            case "Inventory":
                {
                    QuickSave_Rect_Transform.localPosition =
                        -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String, UICode);
                    if (_UI_EventManager._Dialogue_DialogueCamp_Dictionary.TryGetValue(UICode, out List<string> Values))
                    {
                        if (Values.Count > 0)
                        {
                            _UI_EventManager.DialogueCamp(UICode);
                            return;
                        }
                    }
                    _UI_Camp_Class.SummaryTransforms[3].gameObject.SetActive(true);
                    UISet(_UI_Camp_Class._View_Inventory.Inventory_InventorySave_String);
                }
                break;
            case "Inventory_Weapons":
                QuickSave_Inventory_Script.ItemFilterSet("Weapons");
                _UI_Camp_Class._View_Inventory.Inventory_SummaryTransforms[0].gameObject.SetActive(true);
                break;
            case "Inventory_Items":
                QuickSave_Inventory_Script.ItemFilterSet("Items");
                _UI_Camp_Class._View_Inventory.Inventory_SummaryTransforms[1].gameObject.SetActive(true);
                break;
            case "Inventory_Concepts":
                QuickSave_Inventory_Script.ItemFilterSet("Concepts");
                _UI_Camp_Class._View_Inventory.Inventory_SummaryTransforms[2].gameObject.SetActive(true);
                break;
            case "Inventory_Materials":
                QuickSave_Inventory_Script.ItemFilterSet("Materials");
                _UI_Camp_Class._View_Inventory.Inventory_SummaryTransforms[3].gameObject.SetActive(true);
                break;
            #endregion

            #region - Alchemy -
            case "Alchemy":
                {
                    QuickSave_Rect_Transform.localPosition =
                        -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String, UICode);
                    if (_UI_EventManager._Dialogue_DialogueCamp_Dictionary.TryGetValue(UICode, out List<string> Values))
                    {
                        if (Values.Count > 0)
                        {
                            _UI_EventManager.DialogueCamp(UICode);
                            return;
                        }
                    }

                    _UI_Camp_Class.SummaryTransforms[4].gameObject.SetActive(true);
                    if (_UI_Camp_Class._View_Alchemy.Alchemy_AlchemySave_String != "Alchemy")
                    {
                        _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[4].gameObject.SetActive(true);
                        UISet(_UI_Camp_Class._View_Alchemy.Alchemy_AlchemySave_String);
                    }
                }
                break;
            case "Alchemy_Recipe":         
                _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[0].gameObject.SetActive(true);
                break;
            case "Alchemy_Alchemy":
                _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[1].gameObject.SetActive(true);
                break;
            case "Alchemy_Material":
                _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[2].gameObject.SetActive(true);
                if (_UI_Camp_Class._View_Alchemy.Alchemy_MaterialSelect_Int != 65535)
                {
                    QuickSave_Inventory_Script.
                        ItemFilterSet("Materials", "Recipe", RecipeFilter: 
                        _UI_Camp_Class._View_Alchemy.Alchemy_OnRecipeMaterials_ScriptsList[_UI_Camp_Class._View_Alchemy.Alchemy_MaterialSelect_Int]);
                }
                break;
            case "Alchemy_Process":
                _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[0].gameObject.SetActive(false);
                _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[1].gameObject.SetActive(false);
                _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[2].gameObject.SetActive(false);
                _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[3].gameObject.SetActive(true);
                _UI_Camp_Class._View_Alchemy.Alchemy_SummaryTransforms[4].gameObject.SetActive(false);
                _UI_Camp_Class._View_Alchemy.Alchemy_ProcessStartSet(0);
                break;
            #endregion

            case "Book":
                QuickSave_Rect_Transform.localPosition = 
                    -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String,UICode);
                _UI_Camp_Class.SummaryTransforms[5].gameObject.SetActive(true);
                break;
            case "Miio":
                QuickSave_Rect_Transform.localPosition = 
                    -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String,UICode);
                //_UI_Camp_Class._View_QuickView.QuickViewSet();
                TextEffectDictionarySet("CampMenu", null);
                _UI_EventManager.DialogueCamp(UICode);
                break;
            case "Tabana":
                QuickSave_Rect_Transform.localPosition =
                    -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String,UICode);
                _UI_Camp_Class.SummaryTransforms[7].gameObject.SetActive(true);
                break;
            case "Lide":
                QuickSave_Rect_Transform.localPosition =
                    -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String,UICode);
                _UI_Camp_Class.SummaryTransforms[8].gameObject.SetActive(true);
                break;
            case "Limu":
                QuickSave_Rect_Transform.localPosition =
                    -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String,UICode);
                _UI_Camp_Class.SummaryTransforms[9].gameObject.SetActive(true);
                break;
            case "Choco":
                QuickSave_Rect_Transform.localPosition =
                    -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String,UICode);
                _UI_Camp_Class.SummaryTransforms[10].gameObject.SetActive(true);
                break;
            case "Rotetis":
                QuickSave_Rect_Transform.localPosition =
                    -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String,UICode);
                _UI_Camp_Class.SummaryTransforms[11].gameObject.SetActive(true);
                break;
            case "Nomus":
                QuickSave_Rect_Transform.localPosition =
                    -_View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String,UICode);
                _UI_Camp_Class.SummaryTransforms[12].gameObject.SetActive(true);
                break;
            #endregion

            #region UI
            case "Setting":
                _UI_Setting_Transform.gameObject.SetActive(true);
                break;
                #endregion
        }
        Vector2 QuickSave_ClampRange_Vector2 = 
            new Vector2((QuickSave_Rect_Transform.sizeDelta.x - 1920) * 0.5f, (QuickSave_Rect_Transform.sizeDelta.y - 1080) * 0.5f);
        QuickSave_Rect_Transform.localPosition = new Vector3
            (Mathf.Clamp(QuickSave_Rect_Transform.localPosition.x , -QuickSave_ClampRange_Vector2.x, QuickSave_ClampRange_Vector2.x),
            Mathf.Clamp(QuickSave_Rect_Transform.localPosition.y, -QuickSave_ClampRange_Vector2.y, QuickSave_ClampRange_Vector2.y),
            0);
        //print("GetOut：" + _UI_Camp_Class._UI_CampState_String + "←" + UICode);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion UIVoid

    #region CampVoid
    #region - Camp -
    //Camp初始化——————————————————————————————————————————————————————————————————————
    public void CampAwake()
    {
        //UI初始化----------------------------------------------------------------------------------------------------
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        UISet("Null");
        _UI_Camp_Transform.gameObject.SetActive(true);
        _UI_FieldBattle_Transform.gameObject.SetActive(false);
        _UI_Setting_Transform.gameObject.SetActive(false);
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        //----------------------------------------------------------------------------------------------------

        //UI設定----------------------------------------------------------------------------------------------------
        _UI_Camp_Class.BackImage.sprite = _View_Manager.GetSprite("CampBackGround", QuickSave_Map_String);
        _UI_Camp_Class.BackImage.SetNativeSize();
        //設定按鈕顯示與位置
        List<string> QuickSave_BTNs_StringList = new List<string> 
        { 
            "Adventure" ,"Equipment","Skill","Inventory","Alchemy","Book",
            "Miio","Tabana","Lide","Limu","Choco","Rotetis","Nomus"
        };
        for (int a = 0; a < QuickSave_BTNs_StringList.Count; a++)
        {
            string QuickSave_Key_String = QuickSave_BTNs_StringList[a];
            _UI_Camp_Class.BTNs[a].ImageSet(
            _View_Manager.GetSprite("CampMenuSprite", QuickSave_Map_String, QuickSave_Key_String),
             _View_Manager.GetVector3("CampMenuPosition", QuickSave_Map_String, QuickSave_Key_String));
        }
        //----------------------------------------------------------------------------------------------------

        //介面初始化----------------------------------------------------------------------------------------------------
        for (int a = 0; a < _UI_Camp_Class.SummaryTransforms.Length; a++)
        {
            if (_UI_Camp_Class.SummaryTransforms[a] != null)
            {
                _UI_Camp_Class.SummaryTransforms[a].gameObject.SetActive(false);
            }
        }

        #region - StartSet -
        _UI_Camp_Class._View_TinyMenu.StartSet();
        _UI_Camp_Class._UI_Adventure.StartSet();
        _UI_Camp_Class._UI_Skill.StartSet();
        _UI_Camp_Class._UI_Equipment.StartSet();
        TextEffectDictionarySet("InventoryMenu", _UI_Camp_Class._View_Inventory.Inventory_DefaultMenu);
        _UI_Camp_Class._View_Inventory.StartSet();
        _UI_Camp_Class._View_Alchemy.StartSet();
        #endregion

        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #endregion CampVoid

    #region - Effect -
    public void Effect(string Key,bool TurnOn/*相反為Off*/)
    {
        switch (Key)
        {
            case "React":
                if (TurnOn)
                {
                    _UI_ReactEffect_Effect.gameObject.SetActive(true);
                    _UI_ReactEffect_Effect.Play();
                }
                else
                {
                    _UI_ReactEffect_Effect.gameObject.SetActive(false);
                    _UI_ReactEffect_Effect.Stop();
                }
                
                break;
        }
    }
    #endregion
}