using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _World_SoundManager : MonoBehaviour
{
    #region Element
    //�ܼƻP�ѼơX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //��m��----------------------------------------------------------------------------------------------------
    //�������
    public GameObject _Sound_Unit_GameObject;
    //�I����
    public AudioSource _Sound_BackGround_AudioSource;
    //���֮w��m��
    public List<SoundStore> _Sound_AudioStore_ClassList;
    //----------------------------------------------------------------------------------------------------

    //�ܼƶ�----------------------------------------------------------------------------------------------------
    //������j�p
    public int _Sound_UnitSize_Int;
    //�����񪫥��
    public Stack<AudioSource> _Sound_IdleUnitPool_Stack = new Stack<AudioSource>();
    //���񤤪����
    public Stack<AudioSource> _Sound_PlayingUnitPool_Stack = new Stack<AudioSource>();
    //�a���w����
    public Dictionary<string, Dictionary<string, AudioClip>> _Sound_AudioStore_Dictionary = new Dictionary<string, Dictionary<string, AudioClip>>();
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Element


    #region PicDictionaryInput
    //�U���n���פJ�ϡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�]�w�n������----------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class SoundElement
    {
        public string _Sound_AudioName_String;
        public AudioClip _Sound_AudioClip_AudioClip;
    }
    //----------------------------------------------------------------------------------------------------

    //�]�w�n�����O----------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class SoundStore
    {
        public string _Sound_AudioTypeName_String;
        public List<SoundElement> _Sound_AudioType_ClassList;
    }
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion PicDictionaryInput


    #region FirstBuild
    //�Ĥ@��ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Awake()
    {
        //�Ϥ��m�J�ܯ���----------------------------------------------------------------------------------------------------
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion FirstBuild


    #region Start
    //�_�l�I�s�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void SystemStart()
    {
        //�a�O������s�y----------------------------------------------------------------------------------------------------
        for(int a = 0; a < _Sound_UnitSize_Int; a++)
        {
            AudioSource QuickSave_Unit_AudioSource = Instantiate(_Sound_Unit_GameObject).GetComponent<AudioSource>();
            QuickSave_Unit_AudioSource.clip = null;
            QuickSave_Unit_AudioSource.Stop();
            _Sound_IdleUnitPool_Stack.Push(QuickSave_Unit_AudioSource);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Start


    #region MusicPlayer
    //����I�����֡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void PlayBackGroundAudio(string Element)
    {
        if(Element != "null")
        {
            //�ˬd����----------------------------------------------------------------------------------------------------
            if (!_Sound_AudioStore_Dictionary["BackGround"].ContainsKey(Element))
            {
                print("WrongWithKeys_SoundType�GBackGround_SoundElement�G" + Element);
                return;
            }
            //----------------------------------------------------------------------------------------------------

            //�]�w����----------------------------------------------------------------------------------------------------
            _Sound_BackGround_AudioSource.volume = _World_Manager._Config_SoundBackGroundVolumn_Int * 0.01f;
            _Sound_BackGround_AudioSource.clip = _Sound_AudioStore_Dictionary["BackGround"][Element];
            _Sound_BackGround_AudioSource.Play();
            //----------------------------------------------------------------------------------------------------
        }
        else
        {
            //�]�w����----------------------------------------------------------------------------------------------------
            _Sound_BackGround_AudioSource.volume = 0;
            _Sound_BackGround_AudioSource.clip = null;
            _Sound_BackGround_AudioSource.Stop();
            //----------------------------------------------------------------------------------------------------
        }
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�����쭵�֡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void PlayUnitAudio(string Type, string Element)
    {
        //�ˬd����----------------------------------------------------------------------------------------------------
        if (!_Sound_AudioStore_Dictionary.ContainsKey(Type))
        {
            print("WrongWithKeys_SoundType�G" + Type);
            return;
        }
        if (!_Sound_AudioStore_Dictionary["BackGround"].ContainsKey(Element))
        {
            print("WrongWithKeys_SoundType�GBackGround_SoundElement�G" + Element);
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //�]�w����----------------------------------------------------------------------------------------------------
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

        //�I�s�p������----------------------------------------------------------------------------------------------------
        StartCoroutine(WaitUnitAudio(QuickSave_SoundUnit_AudioSource));
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //�����������֡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public IEnumerator WaitUnitAudio(AudioSource PlayingAudioSource)
    {
        //�p��----------------------------------------------------------------------------------------------------
        yield return new WaitForSeconds(PlayingAudioSource.clip.length);
        //----------------------------------------------------------------------------------------------------

        //��������----------------------------------------------------------------------------------------------------
        PlayingAudioSource.volume = 0;
        PlayingAudioSource.clip = null;
        PlayingAudioSource.Stop();
        _Sound_IdleUnitPool_Stack.Push(PlayingAudioSource);
        _Sound_PlayingUnitPool_Stack.Pop();
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //�ߧY�����Ҧ����֡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void StopUnitAudio( )
    {
        //��������----------------------------------------------------------------------------------------------------
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion MusicPlayer

}
