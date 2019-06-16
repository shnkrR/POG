using UnityEngine;


[CreateAssetMenu(fileName = "CharacterInfoData", menuName = "POG/Character", order = 1)]
public class CharacterInfoData : ScriptableObject
{
    [System.Serializable]
    public class CharacterInfo
    {
        public CharacterMeta _Meta;
        public CharacterAttributes _Attributes;

        public CharacterInfo()
        {
            _Meta = new CharacterMeta();
            _Meta._PrefabName = "Prefab Name";
            _Meta._SoulScore = 0;

            _Attributes = new CharacterAttributes();
            _Attributes._Speed = 0.5f;
            _Attributes._Work = 0.5f;
            _Attributes._Health = 0.5f;
            _Attributes._Fertility = 0.5f;
        }
    }

    [System.Serializable]
    public class CharacterAttributes
    {
        public float _Speed;
        public float _Work;
        public float _Health;
        public float _Fertility;
    }

    [System.Serializable]
    public class CharacterMeta
    {
        public string _PrefabName;

        public int _SoulScore;
    }


    public System.Collections.Generic.List<CharacterInfo> _CharacterInfo;
}