namespace CardGameUIEffect
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;

    /// EventManager triggers Actions to listeners.
    /// Is meant to persist between scenes

    //Event System
    public class EventManager : MonoBehaviour
    {
        [Tooltip("The deck of cards that the player uses.")]
        [SerializeField]
        List<CardData> deckData;

        public static EventManager current;

  

        private void Awake()
        {
            current = this;
        }

        #region Actions
        public event Action<Unit> onAttackUnit;
        public event Action<int> onUpdateArrows;
        public event Action<UnitType> onUpdateArrowsColor;
        public event Action onHideArrows;
        public event Action<List<CardData>> InitializeDeck;
        public event Action<Unit, CardData> onCardActivation;

        #endregion

        #region Trigger methods

        public void CardActivationTrgger(Unit target, CardData data)
        {
            onCardActivation?.Invoke(target, data);
        }

        public void AttackUnitTrigger(Unit target)
        {
            onAttackUnit?.Invoke(target);
        }

        public void UpdateArrowsTrigger(int focusOnCard)
        {
            onUpdateArrows?.Invoke(focusOnCard);
        }

        public void UpdateArrowsColorTrigger(UnitType type)
        {
            onUpdateArrowsColor?.Invoke(type);
        }


        public void HideArrowsTrigger()
        {
            onHideArrows?.Invoke();
        }


        public void InitializeDeckTrigger()
        {
            InitializeDeck?.Invoke(deckData);
        }
        #endregion
    }
}