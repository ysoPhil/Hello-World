

using UnityEngine;

namespace CardGameUIEffect
{
    /// <summary>
    /// The CardInfo class.
    /// You can add your card logical properties in this class.
    /// </summary>
    public enum CardFunction { ATTACK, HEAL, GOAD}
    public enum CardType { ABILITY,POWER,SKILL}
    
    public enum CardRarity { COMMON, UNCOMMON, RARE, LEGENDARY}
    [CreateAssetMenu (fileName = "meta_data", menuName = "New Card")]
    public class CardData : ScriptableObject
    {
        [Tooltip("Card's name")]
        [SerializeField]
        string cardName;

        [Tooltip("Card's Artwork (Sprite)")]
        [SerializeField]
        Sprite artwork;

        [Tooltip ("The valid Target of the Card")]
        [SerializeField]
        UnitType target;

        [Tooltip("What the card can do")]
        [SerializeField]
        CardFunction cardFunction;

        [Tooltip("What the card's stat mult is")]
        [SerializeField]
        CardType cardType;

        [Tooltip("The card's rarity")]
        [SerializeField]
        CardRarity rarity;


        #region Getters
        public string GetCardName(){return this.cardName;}
        public Sprite GetArtWork(){return this.artwork;}
        public CardRarity GetRarity() { return this.rarity; }
        public CardType GetCardType() { return this.cardType; }
        public CardFunction GetCardFunction() { return this.cardFunction; }
        public UnitType GetCardValidTarget() { return this.target; }
        #endregion


    }
}