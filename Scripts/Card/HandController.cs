namespace CardGameUIEffect
{
    using UnityEngine;
    using System.Collections.Generic;

    /// The HandController Class controls the player's hand/deck/discard
    /// retrives card information for each card
    /// triggers initial draw
    public class HandController : MonoBehaviour
    {
        #region Fields
        //[Tooltip("Total card count")]
        //public int cardNum = 15;

        [SerializeField]
        private List<CardData> cardInfos;
        #endregion

        #region Private Methods

        #region LifeCycle Calls
        

        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// Like the Awake function, Start is called exactly once in the lifetime of the script. However, Awake is called when the script object 
        /// is initialised, regardless of whether or not the script is enabled. Start may not be called on the same frame as Awake if the script
        /// is not enabled at initialisation time. The Awake function is called on all objects in the Scene before any object's Start function is
        /// called. This fact is useful in cases where object A's initialisation code needs to rely on object B's already being initialised; B's 
        /// initialisation should be done in Awake, while A's should be done in Start.
        /// </summary>
        private void Start()
        {
        }
        #endregion

        #endregion
    }
}