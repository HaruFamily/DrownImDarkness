using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _World_SoundManager : MonoBehaviour
{
    #region Element
    //變數與參數——————————————————————————————————————————————————————————————————————
    //放置區----------------------------------------------------------------------------------------------------
    //音源單位
    public GameObject _Sound_Unit_GameObject;
    //背景音
    public AudioSource _Sound_BackGround_AudioSource;
    //音樂庫放置區
    public List<SoundStore> _Sound_AudioStore_ClassList;
    //----------------------------------------------------------------------------------------------------

    //變數集----------------------------------------------------------------------------------------------------
    //物件池大小
    public int _Sound_UnitSize_Int;
    //未播放物件池
    public Stack<AudioSource> _Sound_IdleUnitPool_Stack = new Stack<AudioSource>();
    //撥放中物件池
    public Stack<AudioSource> _Sound_PlayingUnitPool_Stack = new Stack<AudioSource>();
    //地版庫索引
    public Dictionary<string, Dictionary<string, AudioClip>> _Sound_AudioStore_Dictionary = new Dictionary<string, Dictionary<string, AudioClip>>();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion Element


    #region PicDictionaryInput
    //各類聲音匯入區——————————————————————————————————————————————————————————————————————
    //設定聲音項目----------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class SoundElement
    {
        public string _Sound_AudioName_String;
        public AudioClip _Sound_AudioClip_AudioClip;
    }
    //----------------------------------------------------------------------------------------------------

    //設定聲音類別----------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class SoundStore
    {
        public string _Sound_AudioTypeName_String;
        public List<SoundElement> _Sound_AudioType_ClassList;
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion PicDictionaryInput


    #region FirstBuild
    //第一行動——————————————————————————————————————————————————————————————————————
    public void Awake()
    {
        //圖片置入至索引----------------------------------------------------------------------------------------------------
        foreach (SoundStore AudioStore in _Sound_AudioStore_ClassList)
        {
            Dictionary<string, AudioClip> _QuickSave_AudioElement_Dictionary = new Dictionary<string, AudioClip>();
            foreach (SoundElement AudioElement in AudioStore._Sound_AudioType_ClassList)
            {
                _QuickSave_AudioElement_Dictionary.Add(AudioElement._Sound_AudioName_String, AudioElement._Sound_AudioClip_AudioClip);
            }
            _Sound_AudioStore_Dictionary.Add(AudioStore._Sound_AudioTypeName_String, _QuickSave_AudioElement_Dictionary);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion FirstBuild


    #region Start
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void SystemStart()
    {
        //地板物件池製造----------------------------------------------------------------------------------------------------
        for(int a = 0; a < _Sound_UnitSize_Int; a++)
        {
            AudioSource QuickSave_Unit_AudioSource = Instantiate(_Sound_Unit_GameObject).GetComponent<AudioSource>();
            QuickSave_Unit_AudioSource.clip = null;
            QuickSave_Unit_AudioSource.Stop();
            _Sound_IdleUnitPool_Stack.Push(QuickSave_Unit_AudioSource);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start


    #region MusicPlayer
    //播放背景音樂——————————————————————————————————————————————————————————————————————
    public void PlayBackGroundAudio(string Element)
    {
        if(Element != "null")
        {
            //檢查索引----------------------------------------------------------------------------------------------------
            if (!_Sound_AudioStore_Dictionary["BackGround"].ContainsKey(Element))
            {
                print("WrongWithKeys_SoundType：BackGround_SoundElement：" + Element);
                return;
            }
            //----------------------------------------------------------------------------------------------------

            //設定撥放器----------------------------------------------------------------------------------------------------
            _Sound_BackGround_AudioSource.volume = _World_Manager._Config_SoundBackGroundVolumn_Int * 0.01f;
            _Sound_BackGround_AudioSource.clip = _Sound_AudioStore_Dictionary["BackGround"][Element];
            _Sound_BackGround_AudioSource.Play();
            //----------------------------------------------------------------------------------------------------
        }
        else
        {
            //設定撥放器----------------------------------------------------------------------------------------------------
            _Sound_BackGround_AudioSource.volume = 0;
            _Sound_BackGround_AudioSource.clip = null;
            _Sound_BackGround_AudioSource.Stop();
            //----------------------------------------------------------------------------------------------------
        }
    }
    //——————————————————————————————————————————————————————————————————————

    //播放單位音樂——————————————————————————————————————————————————————————————————————
    public void PlayUnitAudio(string Type, string Element)
    {
        //檢查索引----------------------------------------------------------------------------------------------------
        if (!_Sound_AudioStore_Dictionary.ContainsKey(Type))
        {
            print("WrongWithKeys_SoundType：" + Type);
            return;
        }
        if (!_Sound_AudioStore_Dictionary["BackGround"].ContainsKey(Element))
        {
            print("WrongWithKeys_SoundType：BackGround_SoundElement：" + Element);
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //設定撥放器----------------------------------------------------------------------------------------------------
        AudioSource QuickSave_SoundUnit_AudioSource = _Sound_IdleUnitPool_Stack.Pop();
        _Sound_PlayingUnitPool_Stack.Push(QuickSave_SoundUnit_AudioSource);
        switch (Type)
        {
            case "BackGround":
                QuickSave_SoundUnit_AudioSource.volume = _World_Manager._Config_SoundBackGroundVolumn_Int * 0.01f;
                break;
            case "EffectObject":
                QuickSave_SoundUnit_AudioSource.volume = _World_Manager._Config_SoundEffectVolumn_Int * 0.01f;
                break;
            case "Vocal":
                QuickSave_SoundUnit_AudioSource.volume = _World_Manager._Config_SoundVocalVolumn_Int * 0.01f;
                break;
        }
        QuickSave_SoundUnit_AudioSource.clip = _Sound_AudioStore_Dictionary[Type][Element];
        _Sound_BackGround_AudioSource.Play();
        //----------------------------------------------------------------------------------------------------

        //呼叫計時關閉----------------------------------------------------------------------------------------------------
        StartCoroutine(WaitUnitAudio(QuickSave_SoundUnit_AudioSource));
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————


    //等待關閉音樂——————————————————————————————————————————————————————————————————————
    public IEnumerator WaitUnitAudio(AudioSource PlayingAudioSource)
    {
        //計時----------------------------------------------------------------------------------------------------
        yield return new WaitForSeconds(PlayingAudioSource.clip.length);
        //----------------------------------------------------------------------------------------------------

        //關閉撥放器----------------------------------------------------------------------------------------------------
        PlayingAudioSource.volume = 0;
        PlayingAudioSource.clip = null;
        PlayingAudioSource.Stop();
        _Sound_IdleUnitPool_Stack.Push(PlayingAudioSource);
        _Sound_PlayingUnitPool_Stack.Pop();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————


    //立即關閉所有音樂——————————————————————————————————————————————————————————————————————
    public void StopUnitAudio( )
    {
        //關閉撥放器----------------------------------------------------------------------------------------------------
        for(int a = 0; a < _Sound_PlayingUnitPool_Stack.Count; a++)
        {
            AudioSource PlayingAudioSource = _Sound_PlayingUnitPool_Stack.Pop();
            PlayingAudioSource.volume = 0;
            PlayingAudioSource.clip = null;
            PlayingAudioSource.Stop();
            _Sound_IdleUnitPool_Stack.Push(PlayingAudioSource);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion MusicPlayer

}
