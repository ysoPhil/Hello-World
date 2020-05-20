namespace CardGameUIEffect.UI
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// The Card class.
    /// </summary>
    public class Card
    {
        #region Properties
        /// <summary>
        /// Gets or sets the card uuid.
        /// </summary>

        /// <summary>
        /// Gets or sets the card instance.
        /// </summary>
        public GameObject Instance { get; set; }

        /// <summary>
        /// Gets or sets the card scale speed.
        /// </summary>
        public float ScaleSpeed { get; set; }

        /// <summary>
        /// Gets or sets the card target angle.
        /// </summary>
        public float TargetAngle { get; set; }

        /// <summary>
        /// Gets or sets the card target position.
        /// </summary>
        public Vector3 TargetPosition { get; set; }

        /// <summary>
        /// Gets or  sets the card sort order.
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the card offset angle.
        /// </summary>
        public float OffsetAngle { get; set; }

        /// <summary>
        /// Gets or sets the card target scale.
        /// </summary>
        public float TargetScale { get; set; }

        /// <summary>
        /// Gets or sets the card move speed.
        /// </summary>
        public float MoveSpeed { get; set; }

        /// <summary>
        /// Gets or sets the card last mouse on time.
        /// </summary>
        public float LastOnTime { get; set; }

        /// <summary>
        /// Gets or sets the card current angle.
        /// </summary>
        public float CurAngle { get; set; }

        /// <summary>
        /// Gets or sets the card non interact begin time.
        /// </summary>
        public float NonInteractBegin { get; set; }

        /// <summary>
        /// Gets or sets the card total distance.
        /// </summary>
        public float TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the card original position y.
        /// </summary>
        public float OriginHighY { get; set; }

        /// <summary>
        /// Gets or sets the card is playing state.
        /// </summary>
        public bool IsPlaying { get; set; }

        /// <summary>
        /// Gets or sets the card is dropping state.
        /// </summary>
        public bool IsDropping { get; set; }

        /// <summary>
        /// Gets or sets the card drop display time.
        /// </summary>
        public float DropDisplayTime { get; set; }

        /// <summary>
        /// Gets or sets the card target player.
        /// </summary>
        public GameObject TargetPlayer { get; set; }

        /// <summary>
        /// Gets or sets the card info.
        /// </summary>
        public CardData data { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        public Card()
        {
            this.ScaleSpeed = 1.0f;
            this.TargetAngle = 0.0f;
            this.TargetPosition = Vector3.zero;
            this.SortOrder = 0;
            this.OffsetAngle = 0.0f;
            this.TargetScale = 0.7f;
            this.MoveSpeed = 1.0f;
            this.LastOnTime = 0.0f;
            this.CurAngle = 0.0f;
            this.NonInteractBegin = 0.0f;
            this.TotalDistance = 0.0f;
            this.OriginHighY = 0.0f;
            this.IsPlaying = false;
            this.IsDropping = false;
        }
        #endregion
    }
}