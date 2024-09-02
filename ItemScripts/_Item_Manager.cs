using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class _Item_Manager : MonoBehaviour
{
    #region ElementBox
    //�l���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //��Ʒ�----------------------------------------------------------------------------------------------------
    //������Ʈw
    public TextAsset _Data_MaterialInput_TextAsset;
    public TextAsset _Data_ConceptRecipeInput_TextAsset;
    public TextAsset _Data_WeaponRecipeInput_TextAsset;
    public TextAsset _Data_ItemRecipeInput_TextAsset;
    public TextAsset _Data_MaterialRecipeInput_TextAsset;
    public TextAsset _Data_SpecialAffixInput_TextAsset;

    //�������Ʈw
    public TextAsset _Data_SyndromeInput_TextAsset;

    //�ͦ�����
    public GameObject _Item_ConceptUnit_GameObject;
    public GameObject _Item_WeaponUnit_GameObject;
    public GameObject _Item_ItemUnit_GameObject;
    public GameObject _Item_MaterialUnit_GameObject;
    public GameObject _Item_RecipeUnit_GameObject;

    public GameObject _Item_SyndromeUnit_GameObject;

    public Transform _Item_ConceptRecipeStore_Transform;
    public Transform _Item_WeaponRecipeStore_Transform;
    public Transform _Item_ItemRecipeStore_Transform;
    public Transform _Item_MaterialRecipeStore_Transform;
    //----------------------------------------------------------------------------------------------------

    //����----------------------------------------------------------------------------------------------------
    //��O�ȯ���
    public List<string> _Data_StatusMaterial_StringList =
        new List<string> { "Size", "Form", "Weight", "Purity" };

    public List<string> _Data_StatusConcept_StringList =
        new List<string> { "Medium", "Catalyst","Consciousness",
            "Vitality","Strength","Precision","Speed","Luck" };
    public List<string> _Data_PointConcept_StringList =
        new List<string> { "MediumPoint", "CatalystPoint", "ConsciousnessPoint" };

    public List<string> _Data_StatusObject_StringList =
        new List<string> { "Medium","Catalyst","Consciousness",
            "AttackValue","HealValue","ConstructValue","EnchanceValue",
            "DelayBeforeWeight","DelayAfterWeight","ComplexPoint","Proficiency" };
    public List<string> _Data_PointObject_StringList =
        new List<string> { "MediumPoint", "CatalystPoint", "ConsciousnessPoint" };

    //���
    public Dictionary<string, MaterialDataDicClass> _Data_Material_Dictionary = new Dictionary<string, MaterialDataDicClass>();
    public Dictionary<string, ConceptRecipeDataClass> _Data_ConceptRecipe_Dictionary = new Dictionary<string, ConceptRecipeDataClass>();
    public Dictionary<string, ObjectRecipeDataClass> _Data_WeaponRecipe_Dictionary = new Dictionary<string, ObjectRecipeDataClass>();
    public Dictionary<string, ObjectRecipeDataClass> _Data_ItemRecipe_Dictionary = new Dictionary<string, ObjectRecipeDataClass>();
    public Dictionary<string, RecipeShareDataClass> _Data_MaterialRecipe_Dictionary = new Dictionary<string, RecipeShareDataClass>();
    public Dictionary<string, SpecialAffixDataClass> _Data_SpecialAffix_Dictionary = new Dictionary<string, SpecialAffixDataClass>();
    public Dictionary<string, SyndromeDataClass> _Data_Syndrome_Dictionary = new Dictionary<string, SyndromeDataClass>();

    //�y��
    public Dictionary<string, LanguageClass> _Language_Concept_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_Weapon_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_Item_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_Material_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_ConceptRecipe_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_WeaponRecipe_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_ItemRecipe_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_MaterialRecipe_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_SpecialAffix_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_Syndrome_Dictionary = new Dictionary<string, LanguageClass>();
    //----------------------------------------------------------------------------------------------------

    //�ܼƶ�----------------------------------------------------------------------------------------------------
    //�D���涰
    [HideInInspector] public List<_Item_Object_Inventory> _Item_Inventory_ScriptList = new List<_Item_Object_Inventory>();
    //----------------------------------------------------------------------------------------------------

    //��������----------------------------------------------------------------------------------------------------
    //�w���t��
    public List<_Item_RecipeUnit> _Recipe_Concept_ClassList = new List<_Item_RecipeUnit>();
    public List<_Item_RecipeUnit> _Recipe_Weapon_ClassList = new List<_Item_RecipeUnit>();
    public List<_Item_RecipeUnit> _Recipe_Item_ClassList = new List<_Item_RecipeUnit>();
    public List<_Item_RecipeUnit> _Recipe_Material_ClassList = new List<_Item_RecipeUnit>();
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ElementBox


    #region DictionarySet
    //�U���Ϥ��פJ�ϡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�H�����----------------------------------------------------------------------------------------------------
    //�Z�����
    public class MaterialDataDicClass
    {
        //����
        public List<string> Tag = new List<string>();

        //����
        public Tuple Size = new Tuple();
        public Tuple Form = new Tuple();
        public Tuple Weight = new Tuple();
        public Tuple Purity = new Tuple();

        public Dictionary<string,List<KeyValuePair<string, int>>>[] SpecialAffix = new Dictionary<string, List<KeyValuePair<string, int>>>[4];


    }
    //----------------------------------------------------------------------------------------------------

    //�T�w���----------------------------------------------------------------------------------------------------
    //�������
    public class MaterialDataClass
    {
        //����//�]SA����
        public List<string> Tag;

        //��L���
        public Dictionary<string, int> Status = new Dictionary<string, int>();

        public string[] SpecialAffix = new string[4];
    }
    //----------------------------------------------------------------------------------------------------

    //�T�w���----------------------------------------------------------------------------------------------------
    public class RecipeShareDataClass
    {
        public string RecipeType;
        public string Target;
        public string FailedTarget;
        public int Quantity;
        public List<string> Color = new List<string>();
        public List<string> Faction;
        public List<string> Tag;

        public List<RecipeDataClass> Recipe = new List<RecipeDataClass>();
        public List<ProcessDataClass> Process = new List<ProcessDataClass>();
    }

    //�����t����
    public class ConceptRecipeDataClass : RecipeShareDataClass
    {
        public float[,] Status = new float[8, 5];
        /* 
         *  Medium
            Catalyst
            Consciousness
            Vitality
            Strength
            Precision
            Speed
            Luck
        */

        public float[,] Point = new float[0, 0];
    }
    //����t����
    public class ObjectRecipeDataClass: RecipeShareDataClass
    {
        public float[,] Status = new float[10, 5];
        /*  
         *  Medium
            Catalyst
            Consciousness
            Attack
            Heal
            Construct
            Enchance
            DelayBeforeWeight
            DelayAfterWeight
            ComplexPoint
        */

        public float[,] Point = new float[0, 0];
    }
    //----------------------------------------------------------------------------------------------------

    //�T�w���----------------------------------------------------------------------------------------------------
    //������
    public class SpecialAffixDataClass : DataClass
    {
        public Dictionary<string, List<KeyValuePair<Tuple, string>>> ProcessChange = new Dictionary<string, List<KeyValuePair<Tuple, string>>>();
    }
    //----------------------------------------------------------------------------------------------------

    //�T�w���----------------------------------------------------------------------------------------------------
    //��������
    public class SyndromeDataClass : DataClass
    {
        public List<sbyte> Rank = new List<sbyte>();
    }
    //----------------------------------------------------------------------------------------------------

    //�X���t��----------------------------------------------------------------------------------------------------
    public class RecipeDataClass
    {
        public string Type;//Class or Target
        public string Key;
        public List<int> Inherit = new List<int>();

        public Tuple Size = new Tuple();
        public Tuple Form = new Tuple();
        public Tuple Weight = new Tuple();
        public Tuple Purity = new Tuple();
    }
    //----------------------------------------------------------------------------------------------------

    //�[�u����----------------------------------------------------------------------------------------------------
    public class ProcessDataClass
    {
        public string Type;
        public List<Tuple> Range = new List<Tuple>();
    }
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion DictionarySet


    #region FirstBuild
    //�Ĥ@��ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    void Awake()
    {
        //�]�w�ܼ�----------------------------------------------------------------------------------------------------
        _World_Manager._Item_Manager = this;
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
        #region - Material -
        //�Z��----------------------------------------------------------------------------------------------------
        string[] QuickSave_MaterialSourceSplit = _Data_MaterialInput_TextAsset.text.Split("�X"[0]);
        for (int t = 0; t < QuickSave_MaterialSourceSplit.Length; t++)
        {
            //�إ߸��----------------------------------------------------------------------------------------------------
            //�����O
            MaterialDataDicClass QuickSave_Data_Class = new MaterialDataDicClass();
            //���Ψ�L�P�Ʀr
            string[] QuickSave_Split_StringArray = QuickSave_MaterialSourceSplit[t].Split("�V"[0]);
            //----------------------------------------------------------------------------------------------------

            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            string[] QuickSave_TextSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            QuickSave_Data_Class.Tag = new List<string>(QuickSave_TextSplit[2].Substring(5).Split(","[0]));
            QuickSave_Data_Class.Size.InputSet(QuickSave_TextSplit[3].Substring(6).Split("~"[0]));
            QuickSave_Data_Class.Form.InputSet(QuickSave_TextSplit[4].Substring(6).Split("~"[0]));
            QuickSave_Data_Class.Weight.InputSet(QuickSave_TextSplit[5].Substring(8).Split("~"[0]));
            QuickSave_Data_Class.Purity.InputSet(QuickSave_TextSplit[6].Substring(8).Split("~"[0]));
            //----------------------------------------------------------------------------------------------------

            //�Ʀr----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            for (int a = 1; a < 5; a++)
            {
                string[] QuickSave_NumberSplit_StringArray = QuickSave_Split_StringArray[a].Split("\r"[0]);
                //�إ߸��
                Dictionary<string, List<KeyValuePair<string, int>>> QuickSave_SpecialAffix_PairList =
                    new Dictionary<string, List<KeyValuePair<string, int>>>();

                for (int b = 1; b < QuickSave_NumberSplit_StringArray.Length - 1; b++)
                {
                    string[] QuickSave_KeyValue = QuickSave_NumberSplit_StringArray[b].Split(":"[0]);
                    string QuickSave_Weather_String = QuickSave_KeyValue[0].Substring(1);
                    KeyValuePair<string, int> QuickSave_SpecialAffix =
                        new KeyValuePair<string, int>(QuickSave_KeyValue[1], int.Parse(QuickSave_KeyValue[2]));

                    if (QuickSave_SpecialAffix_PairList.TryGetValue(QuickSave_Weather_String, out List<KeyValuePair<string, int>> Value))
                    {
                        Value.Add(QuickSave_SpecialAffix);
                    }
                    else
                    {
                        QuickSave_SpecialAffix_PairList.Add
                            (QuickSave_Weather_String, new List<KeyValuePair<string, int>> { QuickSave_SpecialAffix });
                    }
                }
                QuickSave_Data_Class.SpecialAffix[a - 1] = QuickSave_SpecialAffix_PairList;
            }
            //----------------------------------------------------------------------------------------------------

            //�]�w----------------------------------------------------------------------------------------------------
            //�m�J����
            _Data_Material_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //����O����
        _Data_MaterialInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - ConceptRecipe -
        //�Z���ج[----------------------------------------------------------------------------------------------------
        string[] QuickSave_ConceptRecipeSourceSplit = _Data_ConceptRecipeInput_TextAsset.text.Split("�X"[0]);
        for (int t = 0; t < QuickSave_ConceptRecipeSourceSplit.Length; t++)
        {
            //�إ߸��----------------------------------------------------------------------------------------------------
            //�����O
            ConceptRecipeDataClass QuickSave_Data_Class = new ConceptRecipeDataClass();
            //���Ψ�L�P�Ʀr
            string[] QuickSave_Split_StringArray = QuickSave_ConceptRecipeSourceSplit[t].Split("�V"[0]);
            //----------------------------------------------------------------------------------------------------

            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            string[] QuickSave_TextSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            string[] QuickSave_SubSplite_StringArray = null;

            QuickSave_Data_Class.RecipeType = "Concept";
            QuickSave_Data_Class.Target = QuickSave_TextSplit[2].Substring(8);
            QuickSave_Data_Class.FailedTarget = QuickSave_TextSplit[3].Substring(14);
            QuickSave_Data_Class.Quantity = int.Parse(QuickSave_TextSplit[4].Substring(10));
            QuickSave_Data_Class.Color = new List<string>(QuickSave_TextSplit[5].Substring(7).Split(","[0]));
            QuickSave_Data_Class.Faction = new List<string>(QuickSave_TextSplit[6].Substring(9).Split(","[0]));
            QuickSave_Data_Class.Tag = new List<string>(QuickSave_TextSplit[7].Substring(5).Split(","[0]));

            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[8].Substring(8).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[0, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[9].Substring(10).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[1, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[10].Substring(15).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[2, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[11].Substring(10).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[3, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[12].Substring(10).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[4, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[13].Substring(11).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[5, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[14].Substring(7).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[6, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[15].Substring(6).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[7, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            //----------------------------------------------------------------------------------------------------

            //����----------------------------------------------------------------------------------------------------
            string[] QuickSave_RecipeTextSplit = QuickSave_Split_StringArray[1].Split("-"[0]);
            for (int r = 1; r < QuickSave_RecipeTextSplit.Length; r++)
            {
                RecipeDataClass QuickSave_Recipe_Class = new RecipeDataClass();
                string[] QuickSave_MaterialTextSplit = QuickSave_RecipeTextSplit[r].Split("\r"[0]);
                QuickSave_Recipe_Class.Type = QuickSave_MaterialTextSplit[1].Substring(6);
                QuickSave_Recipe_Class.Key = QuickSave_MaterialTextSplit[2].Substring(5);
                string[] QuickSave_Inherit_StringArray = QuickSave_MaterialTextSplit[3].Substring(9).Split(","[0]);
                for (int a = 0; a < QuickSave_Inherit_StringArray.Length; a++)
                {
                    QuickSave_Recipe_Class.Inherit.Add(int.Parse(QuickSave_Inherit_StringArray[a]));
                }

                QuickSave_Recipe_Class.Size.InputSet(QuickSave_MaterialTextSplit[4].Substring(6).Split("~"[0]));
                QuickSave_Recipe_Class.Form.InputSet(QuickSave_MaterialTextSplit[5].Substring(6).Split("~"[0]));
                QuickSave_Recipe_Class.Weight.InputSet(QuickSave_MaterialTextSplit[6].Substring(8).Split("~"[0]));
                QuickSave_Recipe_Class.Purity.InputSet(QuickSave_MaterialTextSplit[7].Substring(8).Split("~"[0]));
                QuickSave_Data_Class.Recipe.Add(QuickSave_Recipe_Class);
            }
            //----------------------------------------------------------------------------------------------------

            //�[�u----------------------------------------------------------------------------------------------------
            string[] QuickSave_ProcessTextSplit = QuickSave_Split_StringArray[2].Split("-"[0]);
            for (int r = 1; r < QuickSave_ProcessTextSplit.Length; r++)
            {
                ProcessDataClass QuickSave_Process_Class = new ProcessDataClass();
                string[] QuickSave_MaterialTextSplit = QuickSave_ProcessTextSplit[r].Split("\r"[0]);
                QuickSave_Process_Class.Type = QuickSave_MaterialTextSplit[1].Substring(6);
                string[] QuickSave_Range_StringArray = QuickSave_MaterialTextSplit[2].Substring(7).Split(","[0]);
                for (int ra = 0; ra < QuickSave_Range_StringArray.Length; ra++)
                {
                    Tuple QuickSave_Range_Tuple = new Tuple();
                    QuickSave_Range_Tuple.InputSet(QuickSave_Range_StringArray[ra].Split("~"[0]));
                    QuickSave_Process_Class.Range.Add(QuickSave_Range_Tuple);
                }
                QuickSave_Data_Class.Process.Add(QuickSave_Process_Class);
            }
            //----------------------------------------------------------------------------------------------------

            //�]�w----------------------------------------------------------------------------------------------------
            //�m�J����
            _Data_ConceptRecipe_Dictionary.Add(QuickSave_Split_StringArray[0].Split("\r"[0])[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //����O����
        _Data_ConceptRecipeInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - WeaponRecipe -
        //�Z���ج[----------------------------------------------------------------------------------------------------
        string[] QuickSave_WeaponRecipeSourceSplit = _Data_WeaponRecipeInput_TextAsset.text.Split("�X"[0]);
        for (int t = 0; t < QuickSave_WeaponRecipeSourceSplit.Length; t++)
        {
            //�إ߸��----------------------------------------------------------------------------------------------------
            //�����O
            ObjectRecipeDataClass QuickSave_Data_Class = new ObjectRecipeDataClass();
            //���Ψ�L�P�Ʀr
            string[] QuickSave_Split_StringArray = QuickSave_WeaponRecipeSourceSplit[t].Split("�V"[0]);
            //----------------------------------------------------------------------------------------------------

            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            string[] QuickSave_TextSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            string[] QuickSave_SubSplite_StringArray = null;

            QuickSave_Data_Class.RecipeType = "Weapon";
            QuickSave_Data_Class.Target = QuickSave_TextSplit[2].Substring(8);
            QuickSave_Data_Class.FailedTarget = QuickSave_TextSplit[3].Substring(14);
            QuickSave_Data_Class.Quantity = int.Parse(QuickSave_TextSplit[4].Substring(10));
            QuickSave_Data_Class.Color = new List<string>(QuickSave_TextSplit[5].Substring(7).Split(","[0]));
            QuickSave_Data_Class.Faction = new List<string>(QuickSave_TextSplit[6].Substring(9).Split(","[0]));
            QuickSave_Data_Class.Tag = new List<string>(QuickSave_TextSplit[7].Substring(5).Split(","[0]));

            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[8].Substring(8).Split(","[0]);
            for (int a=0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[0, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[9].Substring(10).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[1, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[10].Substring(15).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[2, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[11].Substring(13).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[3, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[12].Substring(11).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[4, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[13].Substring(16).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[5, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[14].Substring(15).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[6, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[15].Substring(19).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[7, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[16].Substring(18).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[8, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[17].Substring(14).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[9, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            //----------------------------------------------------------------------------------------------------

            //����----------------------------------------------------------------------------------------------------
            string[] QuickSave_RecipeTextSplit = QuickSave_Split_StringArray[1].Split("-"[0]);
            for (int r = 1; r < QuickSave_RecipeTextSplit.Length; r++)
            {
                RecipeDataClass QuickSave_Recipe_Class = new RecipeDataClass();
                string[] QuickSave_MaterialTextSplit = QuickSave_RecipeTextSplit[r].Split("\r"[0]);
                QuickSave_Recipe_Class.Type = QuickSave_MaterialTextSplit[1].Substring(6);
                QuickSave_Recipe_Class.Key = QuickSave_MaterialTextSplit[2].Substring(5);
                string[] QuickSave_Inherit_StringArray = QuickSave_MaterialTextSplit[3].Substring(9).Split(","[0]);
                for (int a = 0; a < QuickSave_Inherit_StringArray.Length; a++)
                {
                    QuickSave_Recipe_Class.Inherit.Add(int.Parse(QuickSave_Inherit_StringArray[a]));
                }

                QuickSave_Recipe_Class.Size.InputSet(QuickSave_MaterialTextSplit[4].Substring(6).Split("~"[0]));
                QuickSave_Recipe_Class.Form.InputSet(QuickSave_MaterialTextSplit[5].Substring(6).Split("~"[0]));
                QuickSave_Recipe_Class.Weight.InputSet(QuickSave_MaterialTextSplit[6].Substring(8).Split("~"[0]));
                QuickSave_Recipe_Class.Purity.InputSet(QuickSave_MaterialTextSplit[7].Substring(8).Split("~"[0]));
                QuickSave_Data_Class.Recipe.Add(QuickSave_Recipe_Class);
            }
            //----------------------------------------------------------------------------------------------------

            //�[�u----------------------------------------------------------------------------------------------------
            string[] QuickSave_ProcessTextSplit = QuickSave_Split_StringArray[2].Split("-"[0]);
            for (int r = 1; r < QuickSave_ProcessTextSplit.Length; r++)
            {
                ProcessDataClass QuickSave_Process_Class = new ProcessDataClass();
                string[] QuickSave_MaterialTextSplit = QuickSave_ProcessTextSplit[r].Split("\r"[0]);
                QuickSave_Process_Class.Type = QuickSave_MaterialTextSplit[1].Substring(6);
                string[] QuickSave_Range_StringArray = QuickSave_MaterialTextSplit[2].Substring(7).Split(","[0]);
                for (int ra = 0; ra < QuickSave_Range_StringArray.Length; ra++)
                {
                    Tuple QuickSave_Range_Tuple = new Tuple();
                    QuickSave_Range_Tuple.InputSet(QuickSave_Range_StringArray[ra].Split("~"[0]));
                    QuickSave_Process_Class.Range.Add(QuickSave_Range_Tuple);
                }
                QuickSave_Data_Class.Process.Add(QuickSave_Process_Class);
            }
            //----------------------------------------------------------------------------------------------------

            //�]�w----------------------------------------------------------------------------------------------------
            //�m�J����
            _Data_WeaponRecipe_Dictionary.Add(QuickSave_Split_StringArray[0].Split("\r"[0])[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //����O����
        _Data_WeaponRecipeInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - ItemRecipe -
        //�Z���ج[----------------------------------------------------------------------------------------------------
        string[] QuickSave_ItemRecipeSourceSplit = _Data_ItemRecipeInput_TextAsset.text.Split("�X"[0]);
        for (int t = 0; t < QuickSave_ItemRecipeSourceSplit.Length; t++)
        {
            //�إ߸��----------------------------------------------------------------------------------------------------
            //�����O
            ObjectRecipeDataClass QuickSave_Data_Class = new ObjectRecipeDataClass();
            //���Ψ�L�P�Ʀr
            string[] QuickSave_Split_StringArray = QuickSave_ItemRecipeSourceSplit[t].Split("�V"[0]);
            //----------------------------------------------------------------------------------------------------

            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //��L����----------------------------------------------------------------------------------------------------
            string[] QuickSave_TextSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            string[] QuickSave_SubSplite_StringArray = null;

            QuickSave_Data_Class.RecipeType = "Item";
            QuickSave_Data_Class.Target = QuickSave_TextSplit[2].Substring(8);
            QuickSave_Data_Class.FailedTarget = QuickSave_TextSplit[3].Substring(14);
            QuickSave_Data_Class.Quantity = int.Parse(QuickSave_TextSplit[4].Substring(10));
            QuickSave_Data_Class.Color = new List<string>(QuickSave_TextSplit[5].Substring(7).Split(","[0]));
            QuickSave_Data_Class.Faction = new List<string>(QuickSave_TextSplit[6].Substring(9).Split(","[0]));
            QuickSave_Data_Class.Tag = new List<string>(QuickSave_TextSplit[7].Substring(5).Split(","[0]));

            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[8].Substring(8).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[0, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[9].Substring(10).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[1, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[10].Substring(15).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[2, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[11].Substring(13).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[3, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[12].Substring(11).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[4, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[13].Substring(16).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[5, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[14].Substring(15).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[6, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[15].Substring(19).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[7, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[16].Substring(18).Split(","[0]);
            for (int a = 0; a < QuickSave_SubSplite_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Status[8, a] = float.Parse(QuickSave_SubSplite_StringArray[a]);
            }
            //----------------------------------------------------------------------------------------------------

            //����----------------------------------------------------------------------------------------------------
            string[] QuickSave_RecipeTextSplit = QuickSave_Split_StringArray[1].Split("-"[0]);
            for (int r = 1; r < QuickSave_RecipeTextSplit.Length; r++)
            {
                RecipeDataClass QuickSave_Recipe_Class = new RecipeDataClass();
                string[] QuickSave_MaterialTextSplit = QuickSave_RecipeTextSplit[r].Split("\r"[0]);
                QuickSave_Recipe_Class.Type = QuickSave_MaterialTextSplit[1].Substring(6);
                QuickSave_Recipe_Class.Key = QuickSave_MaterialTextSplit[2].Substring(5);
                string[] QuickSave_Inherit_StringArray = QuickSave_MaterialTextSplit[3].Substring(9).Split(","[0]);
                for (int a = 0; a < QuickSave_Inherit_StringArray.Length; a++)
                {
                    QuickSave_Recipe_Class.Inherit.Add(int.Parse(QuickSave_Inherit_StringArray[a]));
                }

                QuickSave_Recipe_Class.Size.InputSet(QuickSave_MaterialTextSplit[4].Substring(6).Split("~"[0]));
                QuickSave_Recipe_Class.Form.InputSet(QuickSave_MaterialTextSplit[5].Substring(6).Split("~"[0]));
                QuickSave_Recipe_Class.Weight.InputSet(QuickSave_MaterialTextSplit[6].Substring(8).Split("~"[0]));
                QuickSave_Recipe_Class.Purity.InputSet(QuickSave_MaterialTextSplit[7].Substring(8).Split("~"[0]));
                QuickSave_Data_Class.Recipe.Add(QuickSave_Recipe_Class);
            }
            //----------------------------------------------------------------------------------------------------

            //�[�u----------------------------------------------------------------------------------------------------
            string[] QuickSave_ProcessTextSplit = QuickSave_Split_StringArray[2].Split("-"[0]);
            for (int r = 1; r < QuickSave_ProcessTextSplit.Length; r++)
            {
                ProcessDataClass QuickSave_Process_Class = new ProcessDataClass();
                string[] QuickSave_MaterialTextSplit = QuickSave_ProcessTextSplit[r].Split("\r"[0]);
                QuickSave_Process_Class.Type = QuickSave_MaterialTextSplit[1].Substring(6);
                string[] QuickSave_Range_StringArray = QuickSave_MaterialTextSplit[2].Substring(7).Split(","[0]);
                for (int ra = 0; ra < QuickSave_Range_StringArray.Length; ra++)
                {
                    Tuple QuickSave_Range_Tuple = new Tuple();
                    QuickSave_Range_Tuple.InputSet(QuickSave_Range_StringArray[ra].Split("~"[0]));
                    QuickSave_Process_Class.Range.Add(QuickSave_Range_Tuple);
                }
                QuickSave_Data_Class.Process.Add(QuickSave_Process_Class);
            }
            //----------------------------------------------------------------------------------------------------

            //�]�w----------------------------------------------------------------------------------------------------
            //�m�J����
            _Data_ItemRecipe_Dictionary.Add(QuickSave_Split_StringArray[0].Split("\r"[0])[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //����O����
        _Data_ItemRecipeInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - MaterialRecipe -
        //�Z���ج[----------------------------------------------------------------------------------------------------
        string[] QuickSave_MaterialRecipeSourceSplit = _Data_MaterialRecipeInput_TextAsset.text.Split("�X"[0]);
        for (int t = 0; t < QuickSave_MaterialRecipeSourceSplit.Length; t++)
        {
            //�إ߸��----------------------------------------------------------------------------------------------------
            //�����O
            RecipeShareDataClass QuickSave_Data_Class = new RecipeShareDataClass();
            //���Ψ�L�P�Ʀr
            string[] QuickSave_Split_StringArray = QuickSave_MaterialRecipeSourceSplit[t].Split("�V"[0]);
            //----------------------------------------------------------------------------------------------------

            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            string[] QuickSave_TextSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);

            QuickSave_Data_Class.RecipeType = "Material";
            QuickSave_Data_Class.Target = QuickSave_TextSplit[2].Substring(8);
            QuickSave_Data_Class.FailedTarget = QuickSave_TextSplit[3].Substring(14);
            QuickSave_Data_Class.Quantity = int.Parse(QuickSave_TextSplit[4].Substring(10));
            //----------------------------------------------------------------------------------------------------

            //����----------------------------------------------------------------------------------------------------
            string[] QuickSave_RecipeTextSplit = QuickSave_Split_StringArray[1].Split("-"[0]);
            for (int r = 1; r < QuickSave_RecipeTextSplit.Length; r++)
            {
                RecipeDataClass QuickSave_Recipe_Class = new RecipeDataClass();
                string[] QuickSave_MaterialTextSplit = QuickSave_RecipeTextSplit[r].Split("\r"[0]);
                QuickSave_Recipe_Class.Type = QuickSave_MaterialTextSplit[1].Substring(6);
                QuickSave_Recipe_Class.Key = QuickSave_MaterialTextSplit[2].Substring(5);
                string[] QuickSave_Inherit_StringArray = QuickSave_MaterialTextSplit[3].Substring(9).Split(","[0]);
                for (int a = 0; a < QuickSave_Inherit_StringArray.Length; a++)
                {
                    if (QuickSave_Inherit_StringArray[a] != "")
                    {
                        QuickSave_Recipe_Class.Inherit.Add(int.Parse(QuickSave_Inherit_StringArray[a]));
                    }
                }

                QuickSave_Recipe_Class.Size.InputSet(QuickSave_MaterialTextSplit[4].Substring(6).Split("~"[0]));
                QuickSave_Recipe_Class.Form.InputSet(QuickSave_MaterialTextSplit[5].Substring(6).Split("~"[0]));
                QuickSave_Recipe_Class.Weight.InputSet(QuickSave_MaterialTextSplit[6].Substring(8).Split("~"[0]));
                QuickSave_Recipe_Class.Purity.InputSet(QuickSave_MaterialTextSplit[7].Substring(8).Split("~"[0]));
                QuickSave_Data_Class.Recipe.Add(QuickSave_Recipe_Class);
            }
            //----------------------------------------------------------------------------------------------------

            //�[�u----------------------------------------------------------------------------------------------------
            string[] QuickSave_ProcessTextSplit = QuickSave_Split_StringArray[2].Split("-"[0]);
            for (int r = 1; r < QuickSave_ProcessTextSplit.Length; r++)
            {
                ProcessDataClass QuickSave_Process_Class = new ProcessDataClass();
                string[] QuickSave_MaterialTextSplit = QuickSave_ProcessTextSplit[r].Split("\r"[0]);
                QuickSave_Process_Class.Type = QuickSave_MaterialTextSplit[1].Substring(6);
                string[] QuickSave_Range_StringArray = QuickSave_MaterialTextSplit[2].Substring(7).Split(","[0]);
                for (int ra = 0; ra < QuickSave_Range_StringArray.Length; ra++)
                {
                    Tuple QuickSave_Range_Tuple = new Tuple();
                    QuickSave_Range_Tuple.InputSet(QuickSave_Range_StringArray[ra].Split("~"[0]));
                    QuickSave_Process_Class.Range.Add(QuickSave_Range_Tuple);
                }
                QuickSave_Data_Class.Process.Add(QuickSave_Process_Class);
            }
            //----------------------------------------------------------------------------------------------------

            //�]�w----------------------------------------------------------------------------------------------------
            //�m�J����
            _Data_MaterialRecipe_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //����O����
        _Data_MaterialRecipeInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - SpecialAffix -
        //���]----------------------------------------------------------------------------------------------------
        //���ά��涵
        string[] QuickSave_SpecialAffixSourceSplit_StringArray = _Data_SpecialAffixInput_TextAsset.text.Split("�X"[0]);
        for (int t = 0; t < QuickSave_SpecialAffixSourceSplit_StringArray.Length; t++)
        {
            //�إ߸��----------------------------------------------------------------------------------------------------
            //�����O
            SpecialAffixDataClass QuickSave_Data_Class = new SpecialAffixDataClass();
            QuickSave_Data_Class.ProcessChange.Add("Slash", new List<KeyValuePair<Tuple, string>>());
            QuickSave_Data_Class.ProcessChange.Add("Puncture", new List<KeyValuePair<Tuple, string>>());
            QuickSave_Data_Class.ProcessChange.Add("Impact", new List<KeyValuePair<Tuple, string>>());
            QuickSave_Data_Class.ProcessChange.Add("Energy", new List<KeyValuePair<Tuple, string>>());
            QuickSave_Data_Class.ProcessChange.Add("Chaos", new List<KeyValuePair<Tuple, string>>());
            QuickSave_Data_Class.ProcessChange.Add("Abstract", new List<KeyValuePair<Tuple, string>>());
            QuickSave_Data_Class.ProcessChange.Add("Stark", new List<KeyValuePair<Tuple, string>>());
            //���Ψ�L�P�Ʀr
            string[] QuickSave_Split_StringArray = QuickSave_SpecialAffixSourceSplit_StringArray[t].Split("�V"[0]);
            //----------------------------------------------------------------------------------------------------

            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            string[] QuickSave_TextSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            string[] QuickSave_SlashProcess_StringArray = QuickSave_TextSplit[2].Substring(7).Split(","[0]);
            for (int b = 0; b < QuickSave_SlashProcess_StringArray.Length; b++)
            {
                string[] QuickSave_Process_StringArray = QuickSave_SlashProcess_StringArray[b].Split(":"[0]);
                Tuple QuickSave_Range_Tuple = new Tuple();
                QuickSave_Range_Tuple.InputSet(QuickSave_Process_StringArray[0].Split("~"[0]));
                QuickSave_Data_Class.ProcessChange["Slash"].Add(new KeyValuePair<Tuple, string>( QuickSave_Range_Tuple, QuickSave_Process_StringArray[1]));
            }

            string[] QuickSave_PunctureProcess_StringArray = QuickSave_TextSplit[3].Substring(10).Split(","[0]);
            for (int b = 0; b < QuickSave_PunctureProcess_StringArray.Length; b++)
            {
                string[] QuickSave_Process_StringArray = QuickSave_PunctureProcess_StringArray[b].Split(":"[0]);
                Tuple QuickSave_Range_Tuple = new Tuple();
                QuickSave_Range_Tuple.InputSet(QuickSave_Process_StringArray[0].Split("~"[0]));
                QuickSave_Data_Class.ProcessChange["Puncture"].Add(new KeyValuePair<Tuple, string>(QuickSave_Range_Tuple, QuickSave_Process_StringArray[1]));
            }

            string[] QuickSave_ImpactProcess_StringArray = QuickSave_TextSplit[4].Substring(8).Split(","[0]);
            for (int b = 0; b < QuickSave_ImpactProcess_StringArray.Length; b++)
            {
                string[] QuickSave_Process_StringArray = QuickSave_ImpactProcess_StringArray[b].Split(":"[0]);
                Tuple QuickSave_Range_Tuple = new Tuple();
                QuickSave_Range_Tuple.InputSet(QuickSave_Process_StringArray[0].Split("~"[0]));
                QuickSave_Data_Class.ProcessChange["Impact"].Add(new KeyValuePair<Tuple, string>(QuickSave_Range_Tuple, QuickSave_Process_StringArray[1]));
            }

            string[] QuickSave_EnergyProcess_StringArray = QuickSave_TextSplit[5].Substring(8).Split(","[0]);
            for (int b = 0; b < QuickSave_EnergyProcess_StringArray.Length; b++)
            {
                string[] QuickSave_Process_StringArray = QuickSave_EnergyProcess_StringArray[b].Split(":"[0]);
                Tuple QuickSave_Range_Tuple = new Tuple();
                QuickSave_Range_Tuple.InputSet(QuickSave_Process_StringArray[0].Split("~"[0]));
                QuickSave_Data_Class.ProcessChange["Energy"].Add(new KeyValuePair<Tuple, string>(QuickSave_Range_Tuple, QuickSave_Process_StringArray[1]));
            }

            string[] QuickSave_ChaosProcess_StringArray = QuickSave_TextSplit[6].Substring(7).Split(","[0]);
            for (int b = 0; b < QuickSave_ChaosProcess_StringArray.Length; b++)
            {
                string[] QuickSave_Process_StringArray = QuickSave_ChaosProcess_StringArray[b].Split(":"[0]);
                Tuple QuickSave_Range_Tuple = new Tuple();
                QuickSave_Range_Tuple.InputSet(QuickSave_Process_StringArray[0].Split("~"[0]));
                QuickSave_Data_Class.ProcessChange["Chaos"].Add(new KeyValuePair<Tuple, string>(QuickSave_Range_Tuple, QuickSave_Process_StringArray[1]));
            }

            string[] QuickSave_AbstractProcess_StringArray = QuickSave_TextSplit[7].Substring(10).Split(","[0]);
            for (int b = 0; b < QuickSave_AbstractProcess_StringArray.Length; b++)
            {
                string[] QuickSave_Process_StringArray = QuickSave_AbstractProcess_StringArray[b].Split(":"[0]);
                Tuple QuickSave_Range_Tuple = new Tuple();
                QuickSave_Range_Tuple.InputSet(QuickSave_Process_StringArray[0].Split("~"[0]));
                QuickSave_Data_Class.ProcessChange["Abstract"].Add(new KeyValuePair<Tuple, string>(QuickSave_Range_Tuple, QuickSave_Process_StringArray[1]));
            }

            string[] QuickSave_StarkProcess_StringArray = QuickSave_TextSplit[8].Substring(7).Split(","[0]);
            for (int b = 0; b < QuickSave_StarkProcess_StringArray.Length; b++)
            {
                string[] QuickSave_Process_StringArray = QuickSave_StarkProcess_StringArray[b].Split(":"[0]);
                Tuple QuickSave_Range_Tuple = new Tuple();
                QuickSave_Range_Tuple.InputSet(QuickSave_Process_StringArray[0].Split("~"[0]));
                QuickSave_Data_Class.ProcessChange["Stark"].Add(new KeyValuePair<Tuple, string>(QuickSave_Range_Tuple, QuickSave_Process_StringArray[1]));
            }
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

            //�s��----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_KeySplit_StringArray = QuickSave_Split_StringArray[2].Split("\r"[0]);
            //�إ߸��
            for (int a = 1; a < QuickSave_KeySplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_KeySplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Keys.Add(QuickSave_KeyValue[0].Substring(1), QuickSave_KeyValue[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //�]�w----------------------------------------------------------------------------------------------------
            //�m�J����
            _Data_SpecialAffix_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //����O����
        _Data_SpecialAffixInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Syndrome -
        //�欰----------------------------------------------------------------------------------------------------
        //���ά��涵
        string[] QuickSave_SyndromeSourceSplit_StringArray = _Data_SyndromeInput_TextAsset.text.Split("�X"[0]);
        for (int t = 0; t < QuickSave_SyndromeSourceSplit_StringArray.Length; t++)
        {
            //�إ߸��----------------------------------------------------------------------------------------------------
            //�����O
            SyndromeDataClass QuickSave_Data_Class = new SyndromeDataClass();
            //���Ψ�L�P�Ʀr
            string[] QuickSave_Split_StringArray = QuickSave_SyndromeSourceSplit_StringArray[t].Split("�V"[0]);
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
            QuickSave_Data_Class.Rank = new List<sbyte>();
            string[] QuickSave_RankSplit_StringArray = QuickSave_TextSplit_StringArray[2].Substring(6).Split(","[0]);
            for (int a = 0; a < QuickSave_RankSplit_StringArray.Length; a++)
            {
                QuickSave_Data_Class.Rank.Add(sbyte.Parse(QuickSave_RankSplit_StringArray[a]));
            }
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

            //�s��----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_KeySplit_StringArray = QuickSave_Split_StringArray[2].Split("\r"[0]);
            //�إ߸��
            for (int a = 1; a < QuickSave_KeySplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_KeySplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Keys.Add(QuickSave_KeyValue[0].Substring(1), QuickSave_KeyValue[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //�]�w----------------------------------------------------------------------------------------------------
            //�m�J����
            _Data_Syndrome_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //����O����
        _Data_SyndromeInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�y���]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void LanguageSet()
    {
        #region - Weapon -
        //�Z��
        string QuickSave_WeaponTextSource_String = "";
        string QuickSave_WeaponTextAssetCheck_String = "";
        QuickSave_WeaponTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_" + _World_Manager._Config_Language_String + "_Weapon.txt";
        if (File.Exists(QuickSave_WeaponTextAssetCheck_String))
        {
            QuickSave_WeaponTextSource_String = File.ReadAllText(QuickSave_WeaponTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Item/_" + _World_Manager._Config_Language_String + "_Weapon.txt");
            QuickSave_WeaponTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_TraditionalChinese_Weapon.txt";
            QuickSave_WeaponTextSource_String = File.ReadAllText(QuickSave_WeaponTextAssetCheck_String);
        }
        //���ά��涵
        string[] QuickSave_WeaponSourceSplit = QuickSave_WeaponTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_WeaponSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_WeaponSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_WeaponSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_Weapon_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        #endregion

        #region - Item -
        //�D��
        string QuickSave_ItemTextSource_String = "";
        string QuickSave_ItemTextAssetCheck_String = "";
        QuickSave_ItemTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_" + _World_Manager._Config_Language_String + "_Item.txt";
        if (File.Exists(QuickSave_ItemTextAssetCheck_String))
        {
            QuickSave_ItemTextSource_String = File.ReadAllText(QuickSave_ItemTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Item/_" + _World_Manager._Config_Language_String + "_Item.txt");
            QuickSave_ItemTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_TraditionalChinese_Item.txt";
            QuickSave_ItemTextSource_String = File.ReadAllText(QuickSave_ItemTextAssetCheck_String);
        }
        //���ά��涵
        string[] QuickSave_ItemSourceSplit = QuickSave_ItemTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_ItemSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_ItemSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_ItemSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_Item_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        #endregion

        #region - Concept -
        //�`���~
        string QuickSave_ConceptTextSource_String = "";
        string QuickSave_ConceptTextAssetCheck_String = "";
        QuickSave_ConceptTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_" + _World_Manager._Config_Language_String + "_Concept.txt";
        if (File.Exists(QuickSave_ConceptTextAssetCheck_String))
        {
            QuickSave_ConceptTextSource_String = File.ReadAllText(QuickSave_ConceptTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Item/_" + _World_Manager._Config_Language_String + "_Concept.txt");
            QuickSave_ConceptTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_TraditionalChinese_Concept.txt";
            QuickSave_ConceptTextSource_String = File.ReadAllText(QuickSave_ConceptTextAssetCheck_String);
        }
        //���ά��涵
        string[] QuickSave_ConceptSourceSplit = QuickSave_ConceptTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_ConceptSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_ConceptSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_ConceptSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_Concept_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        #endregion

        #region - Material -
        //����
        string QuickSave_MaterialTextSource_String = "";
        string QuickSave_MaterialTextAssetCheck_String = "";
        QuickSave_MaterialTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_" + _World_Manager._Config_Language_String + "_Material.txt";
        if (File.Exists(QuickSave_MaterialTextAssetCheck_String))
        {
            QuickSave_MaterialTextSource_String = File.ReadAllText(QuickSave_MaterialTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Item/_" + _World_Manager._Config_Language_String + "_Material.txt");
            QuickSave_MaterialTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_TraditionalChinese_Material.txt";
            QuickSave_MaterialTextSource_String = File.ReadAllText(QuickSave_MaterialTextAssetCheck_String);
        }
        //���ά��涵
        string[] QuickSave_MaterialSourceSplit = QuickSave_MaterialTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_MaterialSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_MaterialSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_MaterialSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_Material_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        #endregion

        #region - WeaponRecipe -
        //�Z���ج[
        string QuickSave_WeaponRecipeTextSource_String = "";
        string QuickSave_WeaponRecipeTextAssetCheck_String = "";
        QuickSave_WeaponRecipeTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_" + _World_Manager._Config_Language_String + "_WeaponRecipe.txt";
        if (File.Exists(QuickSave_WeaponRecipeTextAssetCheck_String))
        {
            QuickSave_WeaponRecipeTextSource_String = File.ReadAllText(QuickSave_WeaponRecipeTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Item/_" + _World_Manager._Config_Language_String + "_WeaponRecipe.txt");
            QuickSave_WeaponRecipeTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_TraditionalChinese_WeaponRecipe.txt";
            QuickSave_WeaponRecipeTextSource_String = File.ReadAllText(QuickSave_WeaponRecipeTextAssetCheck_String);
        }
        //���ά��涵
        string[] QuickSave_WeaponRecipeSourceSplit = QuickSave_WeaponRecipeTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_WeaponRecipeSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_WeaponRecipeSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_WeaponRecipeSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_WeaponRecipe_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        #endregion

        #region - ItemRecipe -
        //�D��ج[
        string QuickSave_ItemRecipeTextSource_String = "";
        string QuickSave_ItemRecipeTextAssetCheck_String = "";
        QuickSave_ItemRecipeTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_" + _World_Manager._Config_Language_String + "_ItemRecipe.txt";
        if (File.Exists(QuickSave_ItemRecipeTextAssetCheck_String))
        {
            QuickSave_ItemRecipeTextSource_String = File.ReadAllText(QuickSave_ItemRecipeTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Item/_" + _World_Manager._Config_Language_String + "_ItemRecipe.txt");
            QuickSave_ItemRecipeTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_TraditionalChinese_ItemRecipe.txt";
            QuickSave_ItemRecipeTextSource_String = File.ReadAllText(QuickSave_ItemRecipeTextAssetCheck_String);
        }
        //���ά��涵
        string[] QuickSave_ItemRecipeSourceSplit = QuickSave_ItemRecipeTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_ItemRecipeSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_ItemRecipeSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_ItemRecipeSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_ItemRecipe_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        #endregion

        #region - ConceptRecipe -
        //�Z���ج[
        string QuickSave_ConceptRecipeTextSource_String = "";
        string QuickSave_ConceptRecipeTextAssetCheck_String = "";
        QuickSave_ConceptRecipeTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_" + _World_Manager._Config_Language_String + "_ConceptRecipe.txt";
        if (File.Exists(QuickSave_ConceptRecipeTextAssetCheck_String))
        {
            QuickSave_ConceptRecipeTextSource_String = File.ReadAllText(QuickSave_ConceptRecipeTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Item/_" + _World_Manager._Config_Language_String + "_ConceptRecipe.txt");
            QuickSave_ConceptRecipeTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_TraditionalChinese_ConceptRecipe.txt";
            QuickSave_ConceptRecipeTextSource_String = File.ReadAllText(QuickSave_ConceptRecipeTextAssetCheck_String);
        }
        //���ά��涵
        string[] QuickSave_ConceptRecipeSourceSplit = QuickSave_ConceptRecipeTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_ConceptRecipeSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_ConceptRecipeSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_ConceptRecipeSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_ConceptRecipe_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        #endregion

        #region - MaterialRecipe -
        //�D��ج[
        string QuickSave_MaterialRecipeTextSource_String = "";
        string QuickSave_MaterialRecipeTextAssetCheck_String = "";
        QuickSave_MaterialRecipeTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_" + _World_Manager._Config_Language_String + "_MaterialRecipe.txt";
        if (File.Exists(QuickSave_MaterialRecipeTextAssetCheck_String))
        {
            QuickSave_MaterialRecipeTextSource_String = File.ReadAllText(QuickSave_MaterialRecipeTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Item/_" + _World_Manager._Config_Language_String + "_MaterialRecipe.txt");
            QuickSave_MaterialRecipeTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_TraditionalChinese_MaterialRecipe.txt";
            QuickSave_MaterialRecipeTextSource_String = File.ReadAllText(QuickSave_MaterialRecipeTextAssetCheck_String);
        }
        //���ά��涵
        string[] QuickSave_MaterialRecipeSourceSplit = QuickSave_MaterialRecipeTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_MaterialRecipeSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_MaterialRecipeSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_MaterialRecipeSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_MaterialRecipe_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        #endregion

        #region - SpecialAffix -
        //����
        string QuickSave_SpecialAffixTextSource_String = "";
        string QuickSave_SpecialAffixTextAssetCheck_String = "";
        QuickSave_SpecialAffixTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_" + _World_Manager._Config_Language_String + "_SpecialAffix.txt";
        if (File.Exists(QuickSave_SpecialAffixTextAssetCheck_String))
        {
            QuickSave_SpecialAffixTextSource_String = File.ReadAllText(QuickSave_SpecialAffixTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Item/_" + _World_Manager._Config_Language_String + "_SpecialAffix.txt");
            QuickSave_SpecialAffixTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_TraditionalChinese_SpecialAffix.txt";
            QuickSave_SpecialAffixTextSource_String = File.ReadAllText(QuickSave_SpecialAffixTextAssetCheck_String);
        }
        //���ά��涵
        string[] QuickSave_SpecialAffixSourceSplit = QuickSave_SpecialAffixTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_SpecialAffixSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_SpecialAffixSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_SpecialAffixSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_SpecialAffix_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        #endregion

        #region - Syndrome -
        //����
        string QuickSave_SyndromeTextSource_String = "";
        string QuickSave_SyndromeTextAssetCheck_String = "";
        QuickSave_SyndromeTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_" + _World_Manager._Config_Language_String + "_Syndrome.txt";
        if (File.Exists(QuickSave_SyndromeTextAssetCheck_String))
        {
            QuickSave_SyndromeTextSource_String = File.ReadAllText(QuickSave_SyndromeTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset�G/Item/_" + _World_Manager._Config_Language_String + "_Syndrome.txt");
            QuickSave_SyndromeTextAssetCheck_String = Application.streamingAssetsPath + "/Item/_TraditionalChinese_Syndrome.txt";
            QuickSave_SyndromeTextSource_String = File.ReadAllText(QuickSave_SyndromeTextAssetCheck_String);
        }
        //���ά��涵
        string[] QuickSave_SyndromeSourceSplit = QuickSave_SyndromeTextSource_String.Split("�X"[0]);
        for (int t = 0; t < QuickSave_SyndromeSourceSplit.Length; t++)
        {
            //���Ѹ��X----------------------------------------------------------------------------------------------------
            if (QuickSave_SyndromeSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //���ά���档�̶}�Y�P�̵������ťա�
            string[] QuickSave_TextSplit = QuickSave_SyndromeSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //�إ߸��_Substring(X)�N���X�}�l
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //�m�J����
            _Language_Syndrome_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        #endregion
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion DataBaseSet

    #region ElementSet
    #region - Material -
    //���Ƴ]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<_Item_MaterialUnit> MaterialStartSet(_Object_CreatureUnit Owner, string Key, int Quantity, bool View,
        RecipeShareDataClass RecipeData = null, _Item_MaterialUnit[] Materials = null, KeyValuePair<string, int>[] Process = null)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<_Item_MaterialUnit> Answer_Return_ScriptsList = new List<_Item_MaterialUnit>();
        for (int Q = 0; Q < Quantity; Q++)
        {
            _Item_MaterialUnit Answer_Return_Script =
                Instantiate(_Item_MaterialUnit_GameObject, Owner._Object_Inventory_Script._Item_MaterialStore_Transform).
                GetComponent<_Item_MaterialUnit>();
            Answer_Return_ScriptsList.Add(Answer_Return_Script);
            int QuickSave_MaterialCount_Int = 0;
            int QuickSave_MaterialSize_Int = 0;
            int QuickSave_MaterialForm_Int = 0;
            int QuickSave_MaterialWeight_Int = 0;
            int QuickSave_MaterialPurity_Int = 0;
            int[] QuickSave_Inherit_IntArray = new int[4] { 5, 5, 5, 5};
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //��¦�]�w
            _Map_BattleObjectUnit QuickSave_Object_Script = Answer_Return_Script._Basic_Object_Script;
            SourceClass QuickSave_Source_Class = new SourceClass
            {
                SourceType = "Material",
                Source_Creature = Owner,
                Source_BattleObject = QuickSave_Object_Script,
                Source_Material = Answer_Return_Script,
                Source_MaterialData = QuickSave_Object_Script._Basic_Material_Class
            };
            _Object_Manager.ObjectDataClass QuickSave_Object_Class =
                new _Object_Manager.ObjectDataClass();
            //�O�_�Q�s�y
            if (RecipeData != null && Materials != null && Process != null)
            {
                List<string> QuickSave_SpecialAffixs_StringList = new List<string>();
                //��¦��ƨ��o
                for (int a = 0; a < Materials.Length; a++)
                {
                    if (Materials[a] == null)
                    {
                        continue;
                    }
                    _Map_BattleObjectUnit QS_MatObject_Script = Materials[a]._Basic_Object_Script;
                    int QuickSave_Size_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Size", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Form_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Form", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Weight_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Weight", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Purity_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Purity", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    QuickSave_MaterialCount_Int++;
                    QuickSave_MaterialSize_Int += QuickSave_Size_Int;
                    QuickSave_MaterialForm_Int += QuickSave_Form_Int;
                    QuickSave_MaterialWeight_Int += QuickSave_Weight_Int;
                    QuickSave_MaterialPurity_Int += QuickSave_Purity_Int;

                    QuickSave_SpecialAffixs_StringList.AddRange(QS_MatObject_Script._Basic_Material_Class.SpecialAffix);
                }
                //�i����Ƴ]�w
                for (int a = 0; a < Materials.Length; a++)//��J��Ʀ�
                {
                    if (Materials[a] == null)
                    {
                        continue;
                    }
                    _Map_BattleObjectUnit QS_MatObject_Script = Materials[a]._Basic_Object_Script;
                    for (int b = 0; b < RecipeData.Recipe[a].Inherit.Count; b++)//�~�Ӷ�(�q�`���@��/�i�঳�_��)
                    {
                        int QuickSave_InheritTarget_Int = RecipeData.Recipe[a].Inherit[b];//�~�Ӧ�m
                        QuickSave_Inherit_IntArray[QuickSave_InheritTarget_Int] = a;
                        for (int p = 0; p < Process.Length; p++)
                        {
                            if (Process[p].Key == null)
                            {
                                continue;
                            }
                            //��l����
                            string QuickSave_SpecialAffix_String =
                                    QS_MatObject_Script._Basic_Material_Class.SpecialAffix[QuickSave_InheritTarget_Int];
                            //���� �X���ܤ�
                            QuickSave_Object_Class.SpecialAffix[QuickSave_InheritTarget_Int] =
                                SpecialAffixProcessSet(QuickSave_SpecialAffix_String,
                                    Process[p],
                                    QuickSave_SpecialAffixs_StringList);
                        }
                    }
                }
            }
            else
            {
                MaterialDataDicClass QuickSave_DataDic_Dictionary = _Data_Material_Dictionary[Key];
                //��¦�]�w(���|���a���ܤ�)
                QuickSave_MaterialCount_Int = 1;
                QuickSave_MaterialSize_Int = Random.Range((int)QuickSave_DataDic_Dictionary.Size.Min, (int)QuickSave_DataDic_Dictionary.Size.Max);
                QuickSave_MaterialForm_Int = Random.Range((int)QuickSave_DataDic_Dictionary.Form.Min, (int)QuickSave_DataDic_Dictionary.Form.Max);
                QuickSave_MaterialWeight_Int = Random.Range((int)QuickSave_DataDic_Dictionary.Weight.Min, (int)QuickSave_DataDic_Dictionary.Weight.Max);
                QuickSave_MaterialPurity_Int = Random.Range((int)QuickSave_DataDic_Dictionary.Purity.Min, (int)QuickSave_DataDic_Dictionary.Purity.Max);
                //�a���ܤ�(����)(�p�G�a���U���yĦ�����w�����ū~)
                string QuickSave_Map_String = 
                    _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
                for (int a = 0; a < QuickSave_DataDic_Dictionary.SpecialAffix.Length; a++)
                {
                    List<KeyValuePair<string, int>> QuickSave_SpecialAffixData_PairList = null;
                    if (QuickSave_DataDic_Dictionary.SpecialAffix[a].Keys.Count > 0)
                    {
                        if (QuickSave_DataDic_Dictionary.SpecialAffix[a].
                            ContainsKey(QuickSave_Map_String))
                        {
                            QuickSave_SpecialAffixData_PairList = QuickSave_DataDic_Dictionary.SpecialAffix[a][QuickSave_Map_String];
                        }
                        else
                        {
                            QuickSave_SpecialAffixData_PairList = QuickSave_DataDic_Dictionary.SpecialAffix[a]["Normal"];
                        }
                        //�H���ͦ� ����
                        List<string> QuickSave_Random_StringList = new List<string>();
                        for (int b = 0; b < QuickSave_SpecialAffixData_PairList.Count; b++)
                        {
                            for (int r = 0; r < QuickSave_SpecialAffixData_PairList[b].Value; r++)
                            {
                                QuickSave_Random_StringList.Add(QuickSave_SpecialAffixData_PairList[b].Key);
                            }
                        }
                        string QuickSave_SpecialAffix_String = QuickSave_Random_StringList[Random.Range(0, QuickSave_Random_StringList.Count)];
                        if (QuickSave_SpecialAffix_String == "Null")
                        {
                            continue;
                        }
                        QuickSave_Object_Class.SpecialAffix[a] = QuickSave_SpecialAffix_String;
                    }
                }
            }
            //������O
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Size", (QuickSave_MaterialSize_Int / QuickSave_MaterialCount_Int), 
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Form", (QuickSave_MaterialForm_Int / QuickSave_MaterialCount_Int), 
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Weight", (QuickSave_MaterialWeight_Int / QuickSave_MaterialCount_Int), 
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Purity", (QuickSave_MaterialPurity_Int / QuickSave_MaterialCount_Int), 
                Materials, QuickSave_Inherit_IntArray));
            //����
            QuickSave_Object_Class.Tag =
                AlchemyTag(_Data_Material_Dictionary[Key].Tag, Materials, Process);
            //��O��
            for (int a = 0; a < _Data_StatusObject_StringList.Count; a++)
            {
                QuickSave_Object_Class.StatusData.Add(0);
            }
            //�m�J
            QuickSave_Object_Script.
                SystemStart(Key, QuickSave_Source_Class, QuickSave_Object_Class, _Language_Material_Dictionary[Key]);
            //----------------------------------------------------------------------------------------------------

            //���޳]�m----------------------------------------------------------------------------------------------------
            Answer_Return_Script._View_Image_Image.sprite = 
                _World_Manager._View_Manager.GetSprite("Material", "Icon", Key);
            Owner._Object_Inventory_Script.InventorySet("Add", Answer_Return_Script);
            //----------------------------------------------------------------------------------------------------
        }

        //�������----------------------------------------------------------------------------------------------------
        if (Materials != null && Process != null)
        {
            for (int a = Materials.Length - 1; a >= 0; a--)
            {
                if (Materials[a] == null)
                {
                    continue;
                }
                Owner._Object_Inventory_Script.InventorySet("Destroy", Materials[a]);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //��ı�]�w----------------------------------------------------------------------------------------------------
        if (View)
        {
            _UI_Manager.CampClass _UI_Camp_Class = _World_Manager._UI_Manager._UI_Camp_Class;
            foreach (_Item_MaterialUnit Material in Answer_Return_ScriptsList)
            {
                _UI_Camp_Class._View_ItemInfo.ItemInfoSortAdd(Material._Basic_Object_Script._Basic_Source_Class);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return Answer_Return_ScriptsList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion


    #region - Concept
    //�Z���]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<_Item_ConceptUnit> ConceptStartSet(_Object_CreatureUnit Owner, string RecipeKey, int Quantity, bool View,
        ConceptRecipeDataClass RecipeData = null, _Item_MaterialUnit[] Materials = null, KeyValuePair<string, int>[] Process = null,
        _Object_Manager.CustomRecipeMakeClass CustomMake = null)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<_Item_ConceptUnit> Answer_Return_ScriptsList = new List<_Item_ConceptUnit>();
        KeyValuePair<string, int>[] QuickSave_Process_PairList = new KeyValuePair<string, int>[3];
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        ConceptRecipeDataClass QuickSave_ConceptRecipe_Class =
            RecipeData != null ? RecipeData : _Data_ConceptRecipe_Dictionary[RecipeKey];
        for (int Q = 0; Q < Quantity; Q++)
        {
            _Item_ConceptUnit Answer_Return_Script =
            Instantiate(_Item_ConceptUnit_GameObject, Owner._Object_Inventory_Script._Item_ConceptStore_Transform).GetComponent<_Item_ConceptUnit>();
            Answer_Return_ScriptsList.Add(Answer_Return_Script);
            _Map_BattleObjectUnit QuickSave_Object_Script = Answer_Return_Script._Basic_Object_Script;
            List<string> QuickSave_SpecialAffixs_StringList = new List<string>();

            int QuickSave_MaterialCount_Int = 0;
            int QuickSave_MaterialSize_Int = 0;
            int QuickSave_MaterialForm_Int = 0;
            int QuickSave_MaterialWeight_Int = 0;
            int QuickSave_MaterialPurity_Int = 0;
            int[] QuickSave_Inherit_IntArray = new int[4] {  5, 5, 5, 5 };
            //---------------------------------------------------------------------------------------------------- 

            //----------------------------------------------------------------------------------------------------
            //�����B�s���B�奻
            RecipeKey = QuickSave_ConceptRecipe_Class.Target;
            Answer_Return_Script._Basic_Color_Color = new Color32(
                byte.Parse(QuickSave_ConceptRecipe_Class.Color[0]),
                byte.Parse(QuickSave_ConceptRecipe_Class.Color[1]),
                byte.Parse(QuickSave_ConceptRecipe_Class.Color[2]),
                byte.Parse(QuickSave_ConceptRecipe_Class.Color[3]));
            Answer_Return_Script._Basic_Owner_Script = Owner;

            SourceClass QuickSave_Source_Class = new SourceClass
            {
                SourceType = "Concept",
                Source_Creature = Owner,
                Source_Concept = Answer_Return_Script,
                Source_FieldObject = Owner._Creature_FieldObjectt_Script,
                Source_BattleObject = QuickSave_Object_Script,
                Source_MaterialData = QuickSave_Object_Script._Basic_Material_Class
            };
            _Object_Manager.ObjectDataClass QuickSave_Object_Class = 
                new _Object_Manager.ObjectDataClass();
            //�y��
            QuickSave_Object_Class.Faction = new List<string>(QuickSave_ConceptRecipe_Class.Faction);
            //���ƺc��
            if (Materials != null && Process != null)
            {
                for (int a = 0; a < Materials.Length; a++)
                {
                    if (Materials[a] == null)
                    {
                        continue;
                    }
                    _Map_BattleObjectUnit QS_MatObject_Script = Materials[a]._Basic_Object_Script;
                    QuickSave_MaterialCount_Int++;
                    int QuickSave_Size_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Size", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Form_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Form", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Weight_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Weight", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Purity_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Purity", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    QuickSave_MaterialSize_Int += QuickSave_Size_Int;
                    QuickSave_MaterialForm_Int += QuickSave_Form_Int;
                    QuickSave_MaterialWeight_Int += QuickSave_Weight_Int;
                    QuickSave_MaterialPurity_Int += QuickSave_Purity_Int;

                    QuickSave_SpecialAffixs_StringList.AddRange(QS_MatObject_Script._Basic_Material_Class.SpecialAffix);
                }
                for (int a = 0; a < Materials.Length; a++)
                {
                    if (Materials[a] == null)
                    {
                        continue;
                    }
                    _Map_BattleObjectUnit QS_MatObject_Script = Materials[a]._Basic_Object_Script;
                    for (int b = 0; b < QuickSave_ConceptRecipe_Class.Recipe[a].Inherit.Count; b++)
                    {
                        int QuickSave_InheritTarget_Int = QuickSave_ConceptRecipe_Class.Recipe[a].Inherit[b];
                        QuickSave_Inherit_IntArray[QuickSave_InheritTarget_Int] = a;
                        for (int p = 0; p < Process.Length; p++)
                        {
                            if (Process[p].Key == null)
                            {
                                continue;
                            }
                            string QuickSave_SpecialAffix_String =
                                    QS_MatObject_Script._Basic_Material_Class.SpecialAffix[QuickSave_InheritTarget_Int];
                            QuickSave_Object_Class.SpecialAffix[QuickSave_InheritTarget_Int] =
                                SpecialAffixProcessSet(
                                    QuickSave_SpecialAffix_String,
                                    Process[p],
                                    QuickSave_SpecialAffixs_StringList);
                        }
                    }
                }
                QuickSave_Process_PairList = Process;
            }
            //�Ȼs�ƺc��
            if (CustomMake != null)
            {
                for (int a = 0; a < CustomMake.MaterialStatus.Count; a++)
                {
                    string[] QuickSave_StatusSplit_StringArray = CustomMake.MaterialStatus[a].Split(","[0]);
                    QuickSave_MaterialCount_Int++;
                    QuickSave_MaterialSize_Int += int.Parse(QuickSave_StatusSplit_StringArray[0]);
                    QuickSave_MaterialForm_Int += int.Parse(QuickSave_StatusSplit_StringArray[1]);
                    QuickSave_MaterialWeight_Int += int.Parse(QuickSave_StatusSplit_StringArray[2]);
                    QuickSave_MaterialPurity_Int += int.Parse(QuickSave_StatusSplit_StringArray[3]);

                    QuickSave_SpecialAffixs_StringList.AddRange(CustomMake.MaterialSpecialAffix[a]);
                }
                for (int a = 0; a < CustomMake.MaterialStatus.Count; a++)
                {
                    for (int b = 0; b < QuickSave_ConceptRecipe_Class.Recipe[a].Inherit.Count; b++)
                    {
                        int QuickSave_InheritTarget_Int = QuickSave_ConceptRecipe_Class.Recipe[a].Inherit[b];
                        QuickSave_Inherit_IntArray[QuickSave_InheritTarget_Int] = a;
                        for (int p = 0; p < CustomMake.Process.Count; p++)
                        {
                            string QuickSave_SpecialAffix_String =
                                    CustomMake.MaterialSpecialAffix[a][QuickSave_InheritTarget_Int];
                            QuickSave_Object_Class.SpecialAffix[QuickSave_InheritTarget_Int] =
                                SpecialAffixProcessSet(
                                    QuickSave_SpecialAffix_String,
                                    CustomMake.Process[p],
                                    QuickSave_SpecialAffixs_StringList);
                        }
                    }
                }
                for (int p = 0; p < CustomMake.Process.Count; p++)
                {
                    QuickSave_Process_PairList[p] = CustomMake.Process[p];
                }
            }
            //������O
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Size", (QuickSave_MaterialSize_Int / QuickSave_MaterialCount_Int), 
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Form", (QuickSave_MaterialForm_Int / QuickSave_MaterialCount_Int),
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Weight", (QuickSave_MaterialWeight_Int / QuickSave_MaterialCount_Int),
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Purity", (QuickSave_MaterialPurity_Int / QuickSave_MaterialCount_Int),
                Materials, QuickSave_Inherit_IntArray));
            //����
            QuickSave_Object_Class.Tag =
                AlchemyTag(QuickSave_ConceptRecipe_Class.Tag, Materials, QuickSave_Process_PairList);
            //��O��
            for (int a = 0; a < _Data_StatusConcept_StringList.Count; a++)
            {
                if (a < 8)
                {
                    float QuickSave_Value_Float =
                        QuickSave_ConceptRecipe_Class.Status[a, 0] + (
                        QuickSave_ConceptRecipe_Class.Status[a, 1] * QuickSave_Object_Class.MaterialData[0] +
                        QuickSave_ConceptRecipe_Class.Status[a, 2] * QuickSave_Object_Class.MaterialData[1] +
                        QuickSave_ConceptRecipe_Class.Status[a, 3] * QuickSave_Object_Class.MaterialData[2] +
                        QuickSave_ConceptRecipe_Class.Status[a, 4] * QuickSave_Object_Class.MaterialData[3]);
                    QuickSave_Object_Class.StatusData.Add(Mathf.RoundToInt(QuickSave_Value_Float));
                }
                else
                {
                    QuickSave_Object_Class.StatusData.Add(0);
                }
            }

            //�m�J
            QuickSave_Object_Script.
                SystemStart(RecipeKey, QuickSave_Source_Class, QuickSave_Object_Class, _Language_Concept_Dictionary[RecipeKey]);
            //----------------------------------------------------------------------------------------------------

            //���޳]�m----------------------------------------------------------------------------------------------------
            Answer_Return_Script._View_Image_Image.sprite = 
                _World_Manager._View_Manager.GetSprite("Concept", "Icon", RecipeKey);
            Owner._Object_Inventory_Script.InventorySet("Add", Answer_Return_Script);
            //----------------------------------------------------------------------------------------------------
        }

        //�������----------------------------------------------------------------------------------------------------
        if (Materials != null && Process != null)
        {
            for (int a = Materials.Length - 1; a >= 0; a--)
            {
                if (Materials[a] == null)
                {
                    continue;
                }
                Owner._Object_Inventory_Script.InventorySet("Destroy", Materials[a]);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //��ı�]�w----------------------------------------------------------------------------------------------------
        if (View)
        {
            _UI_Manager.CampClass _UI_Camp_Class = _World_Manager._UI_Manager._UI_Camp_Class;
            foreach (_Item_ConceptUnit Concept in Answer_Return_ScriptsList)
            {
                _UI_Camp_Class._View_ItemInfo.ItemInfoSortAdd(Concept._Basic_Object_Script._Basic_Source_Class);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return Answer_Return_ScriptsList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Weapon
    //�Z���]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<_Item_WeaponUnit> WeaponStartSet(_Object_CreatureUnit Owner,string RecipeKey, int Quantity, bool View,
        ObjectRecipeDataClass RecipeData = null, _Item_MaterialUnit[] Materials = null, KeyValuePair<string, int>[] Process = null,
        _Object_Manager.CustomRecipeMakeClass CustomMake = null)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<_Item_WeaponUnit> Answer_Return_ScriptsList = new List<_Item_WeaponUnit>();
        KeyValuePair<string, int>[] QuickSave_Process_PairList = new KeyValuePair<string, int>[3];
        ObjectRecipeDataClass QuickSave_WeaponRecipe_Class =
            RecipeData != null ? RecipeData : _Data_WeaponRecipe_Dictionary[RecipeKey];

        for (int Q = 0; Q < Quantity; Q++)
        {
            _Item_WeaponUnit Answer_Return_Script =
            Instantiate(_Item_WeaponUnit_GameObject, Owner._Object_Inventory_Script._Item_WeaponStore_Transform).GetComponent<_Item_WeaponUnit>();
            Answer_Return_ScriptsList.Add(Answer_Return_Script);
            _Map_BattleObjectUnit QuickSave_Object_Script = Answer_Return_Script._Basic_Object_Script;
            List<string> QuickSave_SpecialAffixs_StringList = new List<string>();

            int QuickSave_MaterialCount_Int = 0;
            int QuickSave_MaterialSize_Int = 0;
            int QuickSave_MaterialForm_Int = 0;
            int QuickSave_MaterialWeight_Int = 0;
            int QuickSave_MaterialPurity_Int = 0;
            int[] QuickSave_Inherit_IntArray = new int[4] { 5, 5, 5, 5 };
            //---------------------------------------------------------------------------------------------------- 

            //----------------------------------------------------------------------------------------------------
            //�����B�s���B�奻
            RecipeKey = QuickSave_WeaponRecipe_Class.Target;
            Answer_Return_Script._Basic_Color_Color = new Color32(
                byte.Parse(QuickSave_WeaponRecipe_Class.Color[0]),
                byte.Parse(QuickSave_WeaponRecipe_Class.Color[1]),
                byte.Parse(QuickSave_WeaponRecipe_Class.Color[2]),
                byte.Parse(QuickSave_WeaponRecipe_Class.Color[3]));
            Answer_Return_Script._Basic_Owner_Script = Owner;

            SourceClass QuickSave_Source_Class = new SourceClass
            {
                SourceType = "Weapon",
                Source_Creature = Owner,
                Source_Weapon = Answer_Return_Script,
                Source_BattleObject = QuickSave_Object_Script,
                Source_MaterialData = QuickSave_Object_Script._Basic_Material_Class
            };
            _Object_Manager.ObjectDataClass QuickSave_Object_Class =
                new _Object_Manager.ObjectDataClass();
            //�y��
            QuickSave_Object_Class.Faction = new List<string>(QuickSave_WeaponRecipe_Class.Faction);
            //���ƺc��
            if (Materials != null && Process != null)
            {
                for (int a = 0; a < Materials.Length; a++)
                {
                    if (Materials[a] == null)
                    {
                        continue;
                    }
                    _Map_BattleObjectUnit QS_MatObject_Script = Materials[a]._Basic_Object_Script;
                    QuickSave_MaterialCount_Int++;
                    int QuickSave_Size_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Size", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Form_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Form", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Weight_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Weight", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Purity_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Purity", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    QuickSave_MaterialSize_Int += QuickSave_Size_Int;
                    QuickSave_MaterialForm_Int += QuickSave_Form_Int;
                    QuickSave_MaterialWeight_Int += QuickSave_Weight_Int;
                    QuickSave_MaterialPurity_Int += QuickSave_Purity_Int;

                    QuickSave_SpecialAffixs_StringList.AddRange(QS_MatObject_Script._Basic_Material_Class.SpecialAffix);
                }

                for (int a = 0; a < Materials.Length; a++)
                {
                    if (Materials[a] == null)
                    {
                        continue;
                    }
                    _Map_BattleObjectUnit QS_MatObject_Script = Materials[a]._Basic_Object_Script;
                    for (int b = 0; b < QuickSave_WeaponRecipe_Class.Recipe[a].Inherit.Count; b++)
                    {
                        int QuickSave_InheritTarget_Int = QuickSave_WeaponRecipe_Class.Recipe[a].Inherit[b];
                        QuickSave_Inherit_IntArray[QuickSave_InheritTarget_Int] = a;
                        for (int p = 0; p < Process.Length; p++)
                        {
                            if (Process[p].Key == null)
                            {
                                continue;
                            }
                            string QuickSave_SpecialAffix_String =
                                    QS_MatObject_Script._Basic_Material_Class.SpecialAffix[QuickSave_InheritTarget_Int];
                            QuickSave_Object_Class.SpecialAffix[QuickSave_InheritTarget_Int] =
                                SpecialAffixProcessSet(
                                    QuickSave_SpecialAffix_String,
                                    Process[p],
                                    QuickSave_SpecialAffixs_StringList);
                        }
                    }
                }
                QuickSave_Process_PairList = Process;
            }
            //�Ȼs�ƺc��
            if (CustomMake != null)
            {
                for (int a = 0; a < CustomMake.MaterialStatus.Count; a++)
                {
                    string[] QuickSave_StatusSplit_StringArray = CustomMake.MaterialStatus[a].Split(","[0]);
                    QuickSave_MaterialCount_Int++;
                    QuickSave_MaterialSize_Int += int.Parse(QuickSave_StatusSplit_StringArray[0]);
                    QuickSave_MaterialForm_Int += int.Parse(QuickSave_StatusSplit_StringArray[1]);
                    QuickSave_MaterialWeight_Int += int.Parse(QuickSave_StatusSplit_StringArray[2]);
                    QuickSave_MaterialPurity_Int += int.Parse(QuickSave_StatusSplit_StringArray[3]);

                    QuickSave_SpecialAffixs_StringList.AddRange(CustomMake.MaterialSpecialAffix[a]);
                }
                for (int a = 0; a < CustomMake.MaterialStatus.Count; a++)
                {
                    for (int b = 0; b < QuickSave_WeaponRecipe_Class.Recipe[a].Inherit.Count; b++)
                    {
                        int QuickSave_InheritTarget_Int = QuickSave_WeaponRecipe_Class.Recipe[a].Inherit[b];
                        QuickSave_Inherit_IntArray[QuickSave_InheritTarget_Int] = a;
                        for (int p = 0; p < CustomMake.Process.Count; p++)
                        {
                            string QuickSave_SpecialAffix_String =
                                    CustomMake.MaterialSpecialAffix[a][QuickSave_InheritTarget_Int];
                            QuickSave_Object_Class.SpecialAffix[QuickSave_InheritTarget_Int] =
                                SpecialAffixProcessSet(
                                    QuickSave_SpecialAffix_String,
                                    CustomMake.Process[p],
                                    QuickSave_SpecialAffixs_StringList);
                        }
                    }
                }
                for (int p = 0; p < CustomMake.Process.Count; p++)
                {
                    QuickSave_Process_PairList[p] = CustomMake.Process[p];
                }
            }
            //������O
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Size", (QuickSave_MaterialSize_Int / QuickSave_MaterialCount_Int),
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Form", (QuickSave_MaterialForm_Int / QuickSave_MaterialCount_Int),
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Weight", (QuickSave_MaterialWeight_Int / QuickSave_MaterialCount_Int),
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Purity", (QuickSave_MaterialPurity_Int / QuickSave_MaterialCount_Int),
                Materials, QuickSave_Inherit_IntArray));
            //����
            QuickSave_Object_Class.Tag =
                AlchemyTag(QuickSave_WeaponRecipe_Class.Tag, Materials, QuickSave_Process_PairList);
            //��O��
            for (int a = 0; a < _Data_StatusObject_StringList.Count; a++)
            {
                if (a < 10)
                {
                    float QuickSave_Value_Float =
                        QuickSave_WeaponRecipe_Class.Status[a, 0] + (
                            QuickSave_WeaponRecipe_Class.Status[a, 1] * QuickSave_Object_Class.MaterialData[0] +
                            QuickSave_WeaponRecipe_Class.Status[a, 2] * QuickSave_Object_Class.MaterialData[1] +
                            QuickSave_WeaponRecipe_Class.Status[a, 3] * QuickSave_Object_Class.MaterialData[2] +
                            QuickSave_WeaponRecipe_Class.Status[a, 4] * QuickSave_Object_Class.MaterialData[3]);
                    QuickSave_Object_Class.StatusData.Add(Mathf.RoundToInt(QuickSave_Value_Float));
                }
                else
                {
                    QuickSave_Object_Class.StatusData.Add(0);
                }
            }
            //�m�J
            QuickSave_Object_Script.
                SystemStart(RecipeKey, QuickSave_Source_Class, QuickSave_Object_Class, _Language_Weapon_Dictionary[RecipeKey]);
            //----------------------------------------------------------------------------------------------------

            //���޳]�m----------------------------------------------------------------------------------------------------
            Answer_Return_Script._View_Image_Image.sprite = 
                _World_Manager._View_Manager.GetSprite("Weapon", "Icon", RecipeKey);
            Owner._Object_Inventory_Script.InventorySet("Add", Answer_Return_Script);
            //----------------------------------------------------------------------------------------------------
        }
        //�������----------------------------------------------------------------------------------------------------
        if (Materials != null && Process != null)
        {
            for (int a = Materials.Length - 1; a >= 0; a--)
            {
                if (Materials[a] == null)
                {
                    continue;
                }
                Owner._Object_Inventory_Script.InventorySet("Destroy", Materials[a]);
            }
        }
        //----------------------------------------------------------------------------------------------------


        //��ı�]�w----------------------------------------------------------------------------------------------------
        if (View)
        {
            _UI_Manager.CampClass _UI_Camp_Class = _World_Manager._UI_Manager._UI_Camp_Class;
            foreach (_Item_WeaponUnit Weapon in Answer_Return_ScriptsList)
            {
                _UI_Camp_Class._View_ItemInfo.ItemInfoSortAdd(Weapon._Basic_Object_Script._Basic_Source_Class);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return Answer_Return_ScriptsList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Item -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<_Item_ItemUnit> ItemStartSet(_Object_CreatureUnit Owner, string RecipeKey, int Quantity, bool View,
         ObjectRecipeDataClass RecipeData = null, _Item_MaterialUnit[] Materials = null, KeyValuePair<string, int>[] Process = null,
        _Object_Manager.CustomRecipeMakeClass CustomMake = null)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<_Item_ItemUnit> Answer_Return_ScriptsList = new List<_Item_ItemUnit>();
        KeyValuePair<string, int>[] QuickSave_Process_PairList = new KeyValuePair<string, int>[3];
        ObjectRecipeDataClass QuickSave_ItemRecipe_Class =
            RecipeData != null ? RecipeData : _Data_ItemRecipe_Dictionary[RecipeKey];
        for (int Q = 0; Q < Quantity; Q++)
        {
            _Item_ItemUnit Answer_Return_Script =
            Instantiate(_Item_ItemUnit_GameObject, Owner._Object_Inventory_Script._Item_ItemStore_Transform).GetComponent<_Item_ItemUnit>();
            Answer_Return_ScriptsList.Add(Answer_Return_Script);
            _Map_BattleObjectUnit QuickSave_Object_Script = Answer_Return_Script._Basic_Object_Script;
            List<string> QuickSave_SpecialAffixs_StringList = new List<string>();

            int QuickSave_MaterialCount_Int = 0;
            int QuickSave_MaterialSize_Int = 0;
            int QuickSave_MaterialForm_Int = 0;
            int QuickSave_MaterialWeight_Int = 0;
            int QuickSave_MaterialPurity_Int = 0;
            int[] QuickSave_Inherit_IntArray = new int[4] { 5, 5, 5, 5 };
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //�����B�s���B�奻
            RecipeKey = QuickSave_ItemRecipe_Class.Target;
            Answer_Return_Script._Basic_Color_Color = new Color32(
                byte.Parse(QuickSave_ItemRecipe_Class.Color[0]),
                byte.Parse(QuickSave_ItemRecipe_Class.Color[1]),
                byte.Parse(QuickSave_ItemRecipe_Class.Color[2]),
                byte.Parse(QuickSave_ItemRecipe_Class.Color[3]));
            Answer_Return_Script._Basic_Owner_Script = Owner;

            SourceClass QuickSave_Source_Class = new SourceClass
            {
                SourceType = "Item",
                Source_Creature = Owner,
                Source_Item = Answer_Return_Script,
                Source_BattleObject = QuickSave_Object_Script,
                Source_MaterialData = QuickSave_Object_Script._Basic_Material_Class
            };
            _Object_Manager.ObjectDataClass QuickSave_Object_Class = 
                new _Object_Manager.ObjectDataClass();
            //�y��
            QuickSave_Object_Class.Faction = new List<string>(QuickSave_ItemRecipe_Class.Faction);
            //���ƺc��
            if (Materials != null && Process != null)
            {
                for (int a = 0; a < Materials.Length; a++)
                {
                    if (Materials[a] == null)
                    {
                        continue;
                    }
                    _Map_BattleObjectUnit QS_MatObject_Script = Materials[a]._Basic_Object_Script;
                    QuickSave_MaterialCount_Int++;
                    int QuickSave_Size_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Size", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Form_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Form", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Weight_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Weight", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    int QuickSave_Purity_Int =
                        Mathf.RoundToInt(QS_MatObject_Script.
                        Key_Material("Purity", QS_MatObject_Script._Basic_Source_Class, QuickSave_Source_Class));
                    QuickSave_MaterialSize_Int += QuickSave_Size_Int;
                    QuickSave_MaterialForm_Int += QuickSave_Form_Int;
                    QuickSave_MaterialWeight_Int += QuickSave_Weight_Int;
                    QuickSave_MaterialPurity_Int += QuickSave_Purity_Int;

                    QuickSave_SpecialAffixs_StringList.AddRange(QS_MatObject_Script._Basic_Material_Class.SpecialAffix);
                }

                for (int a = 0; a < Materials.Length; a++)
                {
                    if (Materials[a] == null)
                    {
                        continue;
                    }
                    _Map_BattleObjectUnit QS_MatObject_Script = Materials[a]._Basic_Object_Script;
                    for (int b = 0; b < QuickSave_ItemRecipe_Class.Recipe[a].Inherit.Count; b++)
                    {
                        int QuickSave_InheritTarget_Int = QuickSave_ItemRecipe_Class.Recipe[a].Inherit[b];
                        QuickSave_Inherit_IntArray[QuickSave_InheritTarget_Int] = a;
                        for (int p = 0; p < Process.Length; p++)
                        {
                            if (Process[p].Key == null)
                            {
                                continue;
                            }
                            string QuickSave_SpecialAffix_String =
                                    QS_MatObject_Script._Basic_Material_Class.SpecialAffix[QuickSave_InheritTarget_Int];
                            QuickSave_Object_Class.SpecialAffix[QuickSave_InheritTarget_Int] =
                                SpecialAffixProcessSet(
                                    QuickSave_SpecialAffix_String,
                                    Process[p],
                                    QuickSave_SpecialAffixs_StringList);
                        }
                    }
                }
                QuickSave_Process_PairList = Process;
            }
            //�Ȼs�ƺc��
            if (CustomMake != null)
            {
                for (int a = 0; a < CustomMake.MaterialStatus.Count; a++)
                {
                    string[] QuickSave_StatusSplit_StringArray = CustomMake.MaterialStatus[a].Split(","[0]);
                    QuickSave_MaterialCount_Int++;
                    QuickSave_MaterialSize_Int += int.Parse(QuickSave_StatusSplit_StringArray[0]);
                    QuickSave_MaterialForm_Int += int.Parse(QuickSave_StatusSplit_StringArray[1]);
                    QuickSave_MaterialWeight_Int += int.Parse(QuickSave_StatusSplit_StringArray[2]);
                    QuickSave_MaterialPurity_Int += int.Parse(QuickSave_StatusSplit_StringArray[3]);

                    QuickSave_SpecialAffixs_StringList.AddRange(CustomMake.MaterialSpecialAffix[a]);
                }

                for (int a = 0; a < CustomMake.MaterialStatus.Count; a++)
                {
                    for (int b = 0; b < QuickSave_ItemRecipe_Class.Recipe[a].Inherit.Count; b++)
                    {
                        int QuickSave_InheritTarget_Int = QuickSave_ItemRecipe_Class.Recipe[a].Inherit[b];
                        QuickSave_Inherit_IntArray[QuickSave_InheritTarget_Int] = a;
                        for (int p = 0; p < CustomMake.Process.Count; p++)
                        {
                            string QuickSave_SpecialAffix_String =
                                    CustomMake.MaterialSpecialAffix[a][QuickSave_InheritTarget_Int];
                            QuickSave_Object_Class.SpecialAffix[QuickSave_InheritTarget_Int] =
                                SpecialAffixProcessSet(
                                    QuickSave_SpecialAffix_String,
                                    CustomMake.Process[p],
                                    QuickSave_SpecialAffixs_StringList);

                        }
                    }
                }
                for (int p = 0; p < CustomMake.Process.Count; p++)
                {
                    QuickSave_Process_PairList[p] = CustomMake.Process[p];
                }
            }

            //������O
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Size", (QuickSave_MaterialSize_Int / QuickSave_MaterialCount_Int),
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Form", (QuickSave_MaterialForm_Int / QuickSave_MaterialCount_Int),
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Weight", (QuickSave_MaterialWeight_Int / QuickSave_MaterialCount_Int),
                Materials, QuickSave_Inherit_IntArray));
            QuickSave_Object_Class.MaterialData.Add(
                AlchemyStatus("Purity", (QuickSave_MaterialPurity_Int / QuickSave_MaterialCount_Int),
                Materials, QuickSave_Inherit_IntArray));
            //����
            QuickSave_Object_Class.Tag =
                AlchemyTag(QuickSave_ItemRecipe_Class.Tag, Materials, QuickSave_Process_PairList);
            //��O��
            for (int a = 0; a < _Data_StatusObject_StringList.Count; a++)
            {
                if (a < 10)
                {
                    float QuickSave_Value_Float =
                        QuickSave_ItemRecipe_Class.Status[a, 0] + (
                        QuickSave_ItemRecipe_Class.Status[a, 1] * QuickSave_Object_Class.MaterialData[0] +
                        QuickSave_ItemRecipe_Class.Status[a, 2] * QuickSave_Object_Class.MaterialData[1] +
                        QuickSave_ItemRecipe_Class.Status[a, 3] * QuickSave_Object_Class.MaterialData[2] +
                        QuickSave_ItemRecipe_Class.Status[a, 4] * QuickSave_Object_Class.MaterialData[3]);
                    QuickSave_Object_Class.StatusData.Add(Mathf.RoundToInt(QuickSave_Value_Float));
                }
                else
                {
                    QuickSave_Object_Class.StatusData.Add(0);
                }
            }

            //�m�J
            QuickSave_Object_Script.
                SystemStart(RecipeKey, QuickSave_Source_Class, QuickSave_Object_Class, _Language_Item_Dictionary[RecipeKey]);
            //----------------------------------------------------------------------------------------------------

            //���޳]�m----------------------------------------------------------------------------------------------------
            Answer_Return_Script._View_Image_Image.sprite = 
                _World_Manager._View_Manager.GetSprite("Item", "Icon", RecipeKey);
            Owner._Object_Inventory_Script.InventorySet("Add", Answer_Return_Script);
            //----------------------------------------------------------------------------------------------------
        }

        //�������----------------------------------------------------------------------------------------------------
        if (Materials != null && Process != null)
        {
            for (int a = Materials.Length - 1; a >= 0; a--)
            {
                if (Materials[a] == null)
                {
                    continue;
                }
                Owner._Object_Inventory_Script.InventorySet("Destroy", Materials[a]);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //��ı�]�w----------------------------------------------------------------------------------------------------
        if (View)
        {
            _UI_Manager.CampClass _UI_Camp_Class = _World_Manager._UI_Manager._UI_Camp_Class;
            foreach (_Item_ItemUnit Item in Answer_Return_ScriptsList)
            {
                _UI_Camp_Class._View_ItemInfo.ItemInfoSortAdd(Item._Basic_Object_Script._Basic_Source_Class);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return Answer_Return_ScriptsList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Recipe -
    //Recipe�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public _Item_RecipeUnit RecipeStartSet(string Type, string Key)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _Item_RecipeUnit Answer_Return_Script = null;
        int QuickSave_Quantity_Int = 1;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Concept":
                {
                    Answer_Return_Script =
                        Instantiate(_Item_RecipeUnit_GameObject, _Item_ConceptRecipeStore_Transform).GetComponent<_Item_RecipeUnit>();
                    Answer_Return_Script._Basic_Language_Class = _Language_ConceptRecipe_Dictionary[Key];
                    Answer_Return_Script._Basic_ConceptData_Class = _Data_ConceptRecipe_Dictionary[Key];
                    Answer_Return_Script._View_Image_Image.sprite =
                        _World_Manager._View_Manager.GetSprite("ConceptRecipe", "Bar", Key);
                    _Recipe_Concept_ClassList.Add(Answer_Return_Script);
                    QuickSave_Quantity_Int = _Data_ConceptRecipe_Dictionary[Key].Quantity;
                }
                break;
            case "Weapon":
                {
                    Answer_Return_Script =
                        Instantiate(_Item_RecipeUnit_GameObject, _Item_WeaponRecipeStore_Transform).GetComponent<_Item_RecipeUnit>();
                    Answer_Return_Script._Basic_Language_Class = _Language_WeaponRecipe_Dictionary[Key];
                    Answer_Return_Script._Basic_ObjectData_Class = _Data_WeaponRecipe_Dictionary[Key];
                    Answer_Return_Script._View_Image_Image.sprite =
                        _World_Manager._View_Manager.GetSprite("WeaponRecipe", "Bar", Key);
                    _Recipe_Weapon_ClassList.Add(Answer_Return_Script);
                    QuickSave_Quantity_Int = _Data_WeaponRecipe_Dictionary[Key].Quantity;
                }
                break;
            case "Item":
                {
                    Answer_Return_Script =
                        Instantiate(_Item_RecipeUnit_GameObject, _Item_ItemRecipeStore_Transform).GetComponent<_Item_RecipeUnit>();
                    Answer_Return_Script._Basic_Language_Class = _Language_ItemRecipe_Dictionary[Key];
                    Answer_Return_Script._Basic_ObjectData_Class = _Data_ItemRecipe_Dictionary[Key];
                    Answer_Return_Script._View_Image_Image.sprite =
                        _World_Manager._View_Manager.GetSprite("ItemRecipe", "Bar", Key);
                    _Recipe_Item_ClassList.Add(Answer_Return_Script);
                    QuickSave_Quantity_Int = _Data_ItemRecipe_Dictionary[Key].Quantity;
                }
                break;
            case "Material":
                {
                    Answer_Return_Script =
                        Instantiate(_Item_RecipeUnit_GameObject, _Item_MaterialRecipeStore_Transform).GetComponent<_Item_RecipeUnit>();
                    Answer_Return_Script._Basic_Language_Class = _Language_MaterialRecipe_Dictionary[Key];
                    Answer_Return_Script._Basic_MaterialData_Class = _Data_MaterialRecipe_Dictionary[Key];
                    Answer_Return_Script._View_Image_Image.sprite =
                        _World_Manager._View_Manager.GetSprite("MaterialRecipe", "Bar", Key);
                    _Recipe_Material_ClassList.Add(Answer_Return_Script);
                    QuickSave_Quantity_Int = _Data_MaterialRecipe_Dictionary[Key].Quantity;
                }
                break;
        }
        Answer_Return_Script._Basic_Key_String = Key;
        Answer_Return_Script._Basic_Type_String = Type;
        Answer_Return_Script._View_Name_Text.text = 
            Answer_Return_Script._Basic_Language_Class.Name;
        if (QuickSave_Quantity_Int > 1)
        {
            Answer_Return_Script._View_Name_Text.text += "*" + QuickSave_Quantity_Int;
        }
        Answer_Return_Script._View_Type_Text.text = 
            _World_Manager._World_GeneralManager._World_TextManager._Language_UIName_Dictionary[Type];
        Answer_Return_Script._View_Hint_Script.HintSet("New", "Recipe");
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return Answer_Return_Script;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region Syndrome
    //���o�ܲ��ĪG�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void GetSyndrome(string Key, int Count, 
        SourceClass UserSource,SourceClass TargetSource)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _Object_CreatureUnit QuickSave_Creature_Script = TargetSource.Source_Creature;
        _Item_ConceptUnit QuickSave_CreatureConcept = QuickSave_Creature_Script._Object_Inventory_Script._Item_EquipConcepts_Script;
        //�B�~�W�[
        int QuickSave_Value_Int = QuickSave_CreatureConcept.Key_GetSyndrome(Count);
        string QuickSave_Key_String = Key;
        if (Key == "Random")
        {
            _UI_EventManager _UI_EventManager = _World_Manager._UI_Manager._UI_EventManager;
            List<string> QuickSave_SyndromePool_StringList = _UI_EventManager._Syndrome_SyndromePool_StringList;
            QuickSave_Key_String = 
                QuickSave_SyndromePool_StringList[Random.Range(0, QuickSave_SyndromePool_StringList.Count)];
        }
        //----------------------------------------------------------------------------------------------------

        //�P�w----------------------------------------------------------------------------------------------------
        if (QuickSave_CreatureConcept._Syndrome_Syndrome_Dictionary.TryGetValue(QuickSave_Key_String, out _Item_SyndromeUnit Value))
        {
            Value.StackIncrease(QuickSave_Value_Int);
        }
        else
        {
            //�ͦ�
            Instantiate(_Item_SyndromeUnit_GameObject, QuickSave_Creature_Script._View_SyndromeStore_Transform).
                GetComponent<_Item_SyndromeUnit>().
                SystemStart(QuickSave_Creature_Script, QuickSave_Key_String, QuickSave_Value_Int);
        }
        //�P�_�O�_����
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Syndrome

    #region Dust
    //�����ܰʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> DustSet(string Type, float Value, SourceClass UserSource, SourceClass TargetSource)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        _Item_Object_Inventory QuickSave_Inventory_Script = TargetSource.Source_Creature._Object_Inventory_Script;
        //----------------------------------------------------------------------------------------------------

        //�P�w----------------------------------------------------------------------------------------------------
        switch (Type)
        {
        }
        QuickSave_Inventory_Script._Item_Dust_Int += Mathf.RoundToInt(Value);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Dust
    #endregion ElementGet

    #region Avdance
    #region - Object -
    public void ObjectStatusAdvanceSet(_Map_BattleObjectUnit Object, List<string> ChangeStatus)
    {
        //----------------------------------------------------------------------------------------------------
        Dictionary<string, NumbericalValueClass> QuickSave_Point_Dicitonary = Object._Basic_Point_Dictionary;
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        foreach (string Key in ChangeStatus)
        {
            switch (Key)
            {
                case "Medium":
                    {
                        int QuickSave_Scale_Int = 5;
                        int QuickSave_Status_Int =
                            Mathf.RoundToInt(Object.
                            Key_Status("Medium", Object._Basic_Source_Class, Object._Basic_Source_Class, null));
                        float QuickSave_Substract_Float = 
                            QuickSave_Status_Int * QuickSave_Scale_Int - 
                            QuickSave_Point_Dicitonary["MediumPoint"].Default;
                        QuickSave_Point_Dicitonary["MediumPoint"].Default = 
                            QuickSave_Status_Int * QuickSave_Scale_Int;
                        QuickSave_Point_Dicitonary["MediumPoint"].Point += 
                            QuickSave_Substract_Float;
                        Object.MediumPointView();
                    }
                    break;
                case "Catalyst"://�̤j�ͩR�ȵ�Ĳ�C�өw
                    {
                        int QuickSave_Scale_Int = 5;
                        int QuickSave_Status_Int = 
                            Mathf.RoundToInt(Object.
                            Key_Status("Catalyst", Object._Basic_Source_Class, Object._Basic_Source_Class, null));
                        float QuickSave_Substract_Float = 
                            QuickSave_Status_Int * QuickSave_Scale_Int - 
                            QuickSave_Point_Dicitonary["CatalystPoint"].Default;
                        QuickSave_Point_Dicitonary["CatalystPoint"].Default = 
                            QuickSave_Status_Int * QuickSave_Scale_Int;
                        QuickSave_Point_Dicitonary["CatalystPoint"].Point += 
                            QuickSave_Substract_Float;
                        Object.MediumPointView();
                    }
                    break;
                case "Consciousness":
                    {
                        int QuickSave_Scale_Int = 1;
                        int QuickSave_Status_Int = 
                            Mathf.RoundToInt(Object.
                            Key_Status("Consciousness", Object._Basic_Source_Class, Object._Basic_Source_Class, null));
                        QuickSave_Point_Dicitonary["ConsciousnessPoint"].Default = 
                            QuickSave_Status_Int * QuickSave_Scale_Int;
                        Object.ConsciousnessPointView();
                    }
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion

    #region - Alchemy -
    //���~�ƶq�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public int AlchemyQuantity(_Item_MaterialUnit[] Materials, _Item_RecipeUnit Recipe)
    {
        //----------------------------------------------------------------------------------------------------
        float Answer_Return_Float = 1;
        switch (Recipe._Basic_Type_String)
        {
            case "Weapon":
                Answer_Return_Float = Recipe._Basic_ObjectData_Class.Quantity;
                break;
            case "Item":
                Answer_Return_Float = Recipe._Basic_ObjectData_Class.Quantity;
                break;
            case "Concept":
                Answer_Return_Float = Recipe._Basic_ConceptData_Class.Quantity;
                break;
            case "Material":
                Answer_Return_Float = Recipe._Basic_MaterialData_Class.Quantity;
                break;
        }
        if (Recipe == null)
        {
            return Mathf.Clamp(Mathf.RoundToInt(Answer_Return_Float), 1, 9);
        }
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        SourceClass _Basic_Source_Class =
            _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script.
            _Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        List<RecipeDataClass> QuickSave_Recipt_ClassList = null;
        int[] QuickSave_Inherit_IntArray = new int[4] { 5, 5, 5, 5 };
        switch (Recipe._Basic_Type_String)
        {
            case "Weapon":
            case "Item":
                QuickSave_Recipt_ClassList = Recipe._Basic_ObjectData_Class.Recipe;
                break;
            case "Concept":
                QuickSave_Recipt_ClassList = Recipe._Basic_ConceptData_Class.Recipe;
                break;
            case "Material":
                QuickSave_Recipt_ClassList = Recipe._Basic_MaterialData_Class.Recipe;
                break;
        }
        for (int a = 0; a < QuickSave_Recipt_ClassList.Count; a++)
        {
            for (int b = 0; b < QuickSave_Recipt_ClassList[a].Inherit.Count; b++)
            {
                QuickSave_Inherit_IntArray[QuickSave_Recipt_ClassList[a].Inherit[b]] = a;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        for (int a = 0; a < Materials.Length; a++)
        {
            if (Materials[a] == null)
            {
                continue;
            }
            string[] QuickSave_SpecialAffix_StringArray =
                Materials[a]._Basic_Object_Script._Basic_Material_Class.SpecialAffix;
            for (int b = 0; b < QuickSave_SpecialAffix_StringArray.Length; b++)
            {
                string QuickSave_SpecialAffix_String = QuickSave_SpecialAffix_StringArray[b];
                switch (QuickSave_SpecialAffix_String)
                {
                    #region - OreCluster -
                    case "SpecialAffix_OreCluster_1":
                    case "SpecialAffix_OreCluster_2":
                        {
                            //�~�Ӧ�ۦP
                            if (QuickSave_Inherit_IntArray[b] != a)
                            {
                                break;
                            }
                            //����
                            List<string> QuickSave_Tag_StringList = new List<string>();
                            switch (Recipe._Basic_Type_String)
                            {
                                case "Weapon":
                                case "Item":
                                case "Concept":
                                    QuickSave_Tag_StringList = Recipe._Basic_ObjectData_Class.Tag;
                                    break;
                                case "Material":
                                    QuickSave_Tag_StringList = _Data_Material_Dictionary[Recipe._Basic_MaterialData_Class.Target].Tag;
                                    break;
                            }
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Metal" };
                            if (!_World_Manager._Skill_Manager.
                                TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                break;
                            }

                            //�ĪG
                            string QuickSave_Number_String = QuickSave_SpecialAffix_String.Split("_"[0])[2];
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U4" + QuickSave_Number_String;
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit(
                                    "Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, null, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit(
                                    "Default", QuickSave_Key_String, _Data_SpecialAffix_Dictionary[QuickSave_SpecialAffix_String].Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, null, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            Answer_Return_Float += QuickSave_Value_Float;
                        }
                        break;
                    #endregion

                    #region - GlobularMossFluff -
                    case "SpecialAffix_GlobularMossFluff_0":
                        {
                            //�~�Ӧ�ۦP
                            if (QuickSave_Inherit_IntArray[b] != a)
                            {
                                break;
                            }
                            //����
                            List<string> QuickSave_Tag_StringList = new List<string>();
                            switch (Recipe._Basic_Type_String)
                            {
                                case "Weapon":
                                case "Item":
                                case "Concept":
                                    QuickSave_Tag_StringList = Recipe._Basic_ObjectData_Class.Tag;
                                    break;
                                case "Material":
                                    QuickSave_Tag_StringList = _Data_Material_Dictionary[Recipe._Basic_MaterialData_Class.Target].Tag;
                                    break;
                            }
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Cloth" };
                            if (!_World_Manager._Skill_Manager.
                                TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                break;
                            }

                            //�ĪG
                            string QuickSave_Number_String = QuickSave_SpecialAffix_String.Split("_"[0])[2];
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U4" + QuickSave_Number_String;
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit(
                                    "Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, null, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit(
                                    "Default", QuickSave_Key_String, _Data_SpecialAffix_Dictionary[QuickSave_SpecialAffix_String].Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, null, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            Answer_Return_Float += QuickSave_Value_Float;
                        }
                        break;
                        #endregion
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Mathf.Clamp(Mathf.RoundToInt(Answer_Return_Float), 1, 9);
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //���~��O�ȡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public int AlchemyStatus(string Type, float Value, _Item_MaterialUnit[] Materials, int[] Inherit)
    {
        //----------------------------------------------------------------------------------------------------
        float Answer_Return_Float = Value;
        if (Materials == null)
        {
            return Mathf.RoundToInt(Answer_Return_Float);
        }
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        SourceClass _Basic_Source_Class =
            _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script.
            _Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        for (int a = 0; a < Materials.Length; a++)
        {
            _Item_MaterialUnit QuickSave_Material_Script = Materials[a];
            if (QuickSave_Material_Script == null)
            {
                continue;
            }
            string[] QuickSave_SpecialAffix_StringArray =
                QuickSave_Material_Script._Basic_Object_Script._Basic_Material_Class.SpecialAffix;
            for (int b = 0; b < QuickSave_SpecialAffix_StringArray.Length; b++)
            {
                string _Basic_Key_String = QuickSave_SpecialAffix_StringArray[b];
                string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
                switch (Type)
                {
                    #region - Size -
                    case "Size":
                        switch (_Basic_Key_String)
                        {
                            #region - Mutations -
                            case "SpecialAffix_Mutations_0":
                                {
                                    //�~�Ӧ�ۦP
                                    /*
                                    if (Inherit[b] != a)
                                    {
                                        break;
                                    }*/
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit(
                                            "Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, null, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit(
                                            "Default", QuickSave_Key_String, _Data_SpecialAffix_Dictionary[_Basic_Key_String].Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, null, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    Answer_Return_Float += Random.Range(-QuickSave_Value_Float, QuickSave_Value_Float);
                                }
                                break;
                            #endregion
                        }
                        break;
                    #endregion

                    #region - Form -
                    case "Form":
                        switch (_Basic_Key_String)
                        {
                            #region - Mutations -
                            case "SpecialAffix_Mutations_1":
                                {
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit(
                                            "Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, null, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit(
                                            "Default", QuickSave_Key_String, _Data_SpecialAffix_Dictionary[_Basic_Key_String].Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, null, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    Answer_Return_Float += Random.Range(-QuickSave_Value_Float, QuickSave_Value_Float);
                                }
                                break;
                                #endregion
                        }
                        break;
                    #endregion

                    #region - Weight -
                    case "Weight":
                        switch (_Basic_Key_String)
                        {
                            #region - Mutations -
                            case "SpecialAffix_Mutations_2":
                                {
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit(
                                            "Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, null, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit(
                                            "Default", QuickSave_Key_String, _Data_SpecialAffix_Dictionary[_Basic_Key_String].Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, null, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    Answer_Return_Float += Random.Range(-QuickSave_Value_Float, QuickSave_Value_Float);
                                }
                                break;
                                #endregion
                        }
                        break;
                    #endregion

                    #region - Purity -
                    case "Purity":
                        switch (_Basic_Key_String)
                        {
                            #region - Mutations -
                            case "SpecialAffix_Mutations_3":
                                {
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit(
                                            "Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, null, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit(
                                            "Default", QuickSave_Key_String, _Data_SpecialAffix_Dictionary[_Basic_Key_String].Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, null, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    Answer_Return_Float += Random.Range(-QuickSave_Value_Float, QuickSave_Value_Float);
                                }
                                break;
                                #endregion
                        }
                        break;
                        #endregion
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Mathf.RoundToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //���ҡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> AlchemyTag(List<string> Tag, _Item_MaterialUnit[] Materials, KeyValuePair<string, int>[] Process)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>(Tag);
        if (Materials == null)
        {
            return Answer_Return_StringList;
        }
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        for (int a = 0; a < Materials.Length; a++)
        {
            _Item_MaterialUnit QuickSave_Material_Script = Materials[a];
            if (QuickSave_Material_Script == null)
            {
                continue;
            }
            string[] QuickSave_SpecialAffix_StringArray =
                QuickSave_Material_Script._Basic_Object_Script._Basic_Material_Class.SpecialAffix;
            for (int b = 0; b < QuickSave_SpecialAffix_StringArray.Length; b++)
            {
                string _Basic_Key_String = QuickSave_SpecialAffix_StringArray[b];
                switch (_Basic_Key_String)
                {
                    #region - ScentedBurning -
                    case "SpecialAffix_ScentedBurning_0":
                        {
                            foreach (KeyValuePair<string,int> Pair in Process)
                            {
                                if (Pair.Key == "Energy")
                                {
                                    Answer_Return_StringList.Add("Volatile");
                                    break;
                                }
                            }
                        }
                        break;
                        #endregion
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�����\�Υ��ѡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public bool AlchemySuccessCheck(KeyValuePair<string, int> ProcessAnswer, ProcessDataClass ProcessRange, _Item_MaterialUnit[] Materials)
    {
        //----------------------------------------------------------------------------------------------------
        if (ProcessAnswer.Key == null)
        {
            return false;
        }
        if (ProcessAnswer.Value == 2)//���~���]�w�ƭ�
        {
            return false;
        }
        SourceClass _Basic_Source_Class =
            _World_Manager._Object_Manager._Object_Player_Script._Object_Inventory_Script.
            _Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //SA���ѧP�w----------------------------------------------------------------------------------------------------
        float QuickSave_RangeMaxAdden_Float = 0;
        float QuickSave_RangeMinAdden_Float = 0;
        for (int a = 0; a < Materials.Length; a++)
        {
            _Item_MaterialUnit QuickSave_Material_Script = Materials[a];
            if (QuickSave_Material_Script == null)
            {
                continue;
            }
            string[] QuickSave_SpecialAffix_StringArray = 
                QuickSave_Material_Script._Basic_Object_Script._Basic_Material_Class.SpecialAffix;
            for (int b = 0; b < QuickSave_SpecialAffix_StringArray.Length; b++)
            {
                string _Basic_Key_String = QuickSave_SpecialAffix_StringArray[b];
                switch (_Basic_Key_String)
                {
                    #region - Whital -
                    case "SpecialAffix_Whital_0":
                        {
                            if (ProcessAnswer.Key == "Impact")
                            {
                                _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
                                string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit(
                                        "Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, null, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit(
                                        "Default", QuickSave_Key_String, _Data_SpecialAffix_Dictionary[_Basic_Key_String].Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, null, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                QuickSave_RangeMaxAdden_Float += QuickSave_Value_Float;
                                QuickSave_RangeMinAdden_Float -= QuickSave_Value_Float;
                            }
                        }
                        break;
                    #endregion

                    #region - Mutations -
                    case "SpecialAffix_Mutations_4":
                        {
                            return false;
                        }
                        #endregion
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //�d��P�w----------------------------------------------------------------------------------------------------
        bool QuickSave_RangeCheck = false;
        for (int pp = 0; pp < ProcessRange.Range.Count; pp++)//�i�঳1~20,90~100
        {
            if ((ProcessRange.Range[pp].Min + QuickSave_RangeMinAdden_Float) < ProcessAnswer.Value &&
                ProcessAnswer.Value <= (ProcessRange.Range[pp].Max + QuickSave_RangeMaxAdden_Float))
            {
                QuickSave_RangeCheck = true;
            }
        }
        if (!QuickSave_RangeCheck)
        {
            return false;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return true;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�����ܧ�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public string SpecialAffixProcessSet(string Key, KeyValuePair<string, int> Process, List<string> TotalSpecialAffix)
    {
        //----------------------------------------------------------------------------------------------------
        if (Key == "" || Key == null)
        {
            return "";
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        string Answer_Return_String = Key;
        SpecialAffixDataClass QuickSave_Data_Class = _Data_SpecialAffix_Dictionary[Key];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        for (int a = 0; a < TotalSpecialAffix.Count; a++)
        {
            if (TotalSpecialAffix[a] == "")
            {
                continue;
            }

            switch (TotalSpecialAffix[a])
            {
                /*
                #region - Intertwined -
                case "SpecialAffix_Intertwined_0":
                    switch (Process.Key)
                    {
                        case "Abstract":
                            if (Process.Value <= 50)
                            {
                                goto End;
                            }
                            break;
                    }
                    break;
                    #endregion
                */
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        List<KeyValuePair<Tuple, string>> QuickSave_RangeSA_PairList = QuickSave_Data_Class.ProcessChange[Process.Key];
        foreach (KeyValuePair<Tuple, string> Pair in QuickSave_RangeSA_PairList)
        {
            if (Pair.Key.Between(Process.Value))
            {
                Answer_Return_String = Pair.Value;
                break;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        End:
        switch (Answer_Return_String)
        {
            case "Self":
                Answer_Return_String = Key;
                break;
            case "Null":
                Answer_Return_String = "";
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_String;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion AvdanceStatus

    #region ItemCreate
    #region - Duplicate - 
    private _Item_WeaponUnit DuplicateWeapon(_Item_WeaponUnit Target)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _Item_WeaponUnit QuickSave_Weapon_Script = 
            Instantiate(_Item_WeaponUnit_GameObject, Target._Basic_Owner_Script._Object_Inventory_Script._Item_WeaponStore_Transform).GetComponent<_Item_WeaponUnit>();
        //----------------------------------------------------------------------------------------------------
        
        //�]�m----------------------------------------------------------------------------------------------------
        
        QuickSave_Weapon_Script._Basic_Owner_Script = Target._Basic_Owner_Script;
        QuickSave_Weapon_Script._Basic_Object_Script._Basic_Status_Dictionary = Target._Basic_Object_Script._Basic_Status_Dictionary.DeepClone();

        if (Target._Basic_Object_Script._Skill_Faction_Script != null)
        {
            QuickSave_Weapon_Script._Basic_Object_Script._Skill_Faction_Script = 
                _World_Manager._Skill_Manager.DuplicateFaction(
                    Target._Basic_Owner_Script._Object_Inventory_Script._Skill_EmptyStore_Transform,
                    Target._Basic_Object_Script._Skill_Faction_Script);
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return QuickSave_Weapon_Script;
        //----------------------------------------------------------------------------------------------------
    }
    private _Item_ItemUnit DuplicateItem(_Item_ItemUnit Target)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        _Item_ItemUnit QuickSave_Item_Script =
            Instantiate(_Item_ItemUnit_GameObject, Target._Basic_Owner_Script._Object_Inventory_Script._Item_ItemStore_Transform).GetComponent<_Item_ItemUnit>();
        //----------------------------------------------------------------------------------------------------

        //�]�m----------------------------------------------------------------------------------------------------

        QuickSave_Item_Script._Basic_Owner_Script = Target._Basic_Owner_Script;
        QuickSave_Item_Script._Basic_Object_Script._Skill_Faction_Script = 
            _Skill_Manager.DuplicateFaction(QuickSave_Item_Script._Basic_Object_Script._View_SkillStore_Transform, 
            Target._Basic_Object_Script._Skill_Faction_Script);

        QuickSave_Item_Script._Basic_Object_Script._Basic_Status_Dictionary = Target._Basic_Object_Script._Basic_Status_Dictionary.DeepClone();
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return QuickSave_Item_Script;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #endregion ItemCreate
}
