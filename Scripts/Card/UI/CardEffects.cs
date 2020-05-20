namespace CardGameUIEffect.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// The CardEffects class.
    /// </summary>
    public class CardEffects : MonoBehaviour
    {
        #region Fields
        [Tooltip("The angle between two neighbor cards in hand, this will be changed with different card numbers")]
        public float rotateAngle = 30.0f;

        [Tooltip("The angle between two neighbor cards will be changed with different card numbers, define card number here")]
        public List<int> cardNumForAngles = new List<int>();

        [Tooltip("The angle between two neighbor cards will be changed with different card numbers, define angle here")]
        public List<float> anglesForCardNum = new List<float>();

        [Tooltip("The center position of the hand, all cards' positions in hand will be calculated depending on this")]
        public Vector3 centerPoint = new Vector3(0.0f, -50.0f, 0.0f);

        [Tooltip("HandCards gameobject in hierarchy")]
        public GameObject handCardObj;

        [Tooltip("When the mouse is on a card in hand, the cards on its left hand will rotate (leftPushAngle - offsetAngleDelta * n) degree to left, n=0 for the nearest one, n=1 for the second nearest one...")]
        public float leftPushAngle = 2.0f;

        [Tooltip("When the mouse is on a card in hand, the cards on its right hand will rotate (rightPushAngle - offsetAngleDelta * n) degree to right, n=0 for the nearest one, n=1 for the second nearest one...")]
        public float rightPushAngle = 2.0f;

        [Tooltip("The Offset Angle Delta only can be used with leftPushAngle and rightPushAngle")]
        public float offsetAngleDelta = 0.1f;

        [Tooltip("The card will rise up to position of (0, cardOffsetY, 0) when the mouse is on")]
        public float cardOffsetY = 1.0f;

        [Tooltip("The card move speed when sent from draw pile to hand")]
        public float sendCardMoveSpeed = 20.0f;

        [Tooltip("The card scale speed when sent from draw pile to hand")]
        public float sendCardScaleSpeed = 10.0f;

        [Tooltip("The card move speed when the mouse is on (Such as rising up)")]
        public float moveSpeed = 1.0f;

        [Tooltip("The card move speed when the mouse is off (Such as falling down to the original position)")]
        public float slowMoveSpeed = 1.0f;

        [Tooltip("The card rotate speed when the mouse is on (Such as rising up)")]
        public float rotateSpeed = 1.0f;

        [Tooltip("The radius of the hand's arc shape")]
        public float centerRadius = 50.0f;

        [Tooltip("Maximum scale of the card")]
        public float cardBigScale = 0.9f;

        [Tooltip("Normal scale of the card")]
        public float cardNormalScale = 0.8f;

        [Tooltip("The normal scale change speed of the card (Such as rising up)")]
        public float scaleSpeed = 1.0f;

        [Tooltip("The slow scale change speed of the card (Such as falling down)")]
        public float slowScaleSpeed = 1.0f;

        [Tooltip("The card cannot be checked if the mouse moves on it twice with a short interval")]
        public float mouseOnInterval = 1.0f;

        [Tooltip("The card cannot be clicked if it is in Non Interact Delay state")]
        public float nonInteractDelay = 1.0f;

        [Tooltip("The card move speed in the first stage of dropping effect")]
        public float cardDropSlowSpeed = 1.0f;

        [Tooltip("The card move speed in the second stage of dropping effect")]
        public float cardDropFastSpeed = 1.0f;

        [Tooltip("The card move speed in shuffle effect")]
        public float cardShuffleFastSpeed = 1.0f;

        [Tooltip("The transform of discard pile")]
        public Transform dropCardPile;

        [Tooltip("The transform of draw pile")]
        public Transform getCardPile;

        [Tooltip("The card move speed when it is playing")]
        public float cardPlaySpeed = 1.0f;

        [Tooltip("Number of cards sent to hand automatically")]
        public int StartingHandSize = 5;

        [Tooltip("The minimum card scale use by drop and shuffle effects")]
        public float miniCardScale = 0.12f;

        [Tooltip("The skill icon offset along y-axis on the head of character")]
        public float skillIconOffsetY = 2.0f;

        [Tooltip("The playing card will rise up to the position along y-axis")]
        public float cardPlayRiseDstY = 1.0f;

        [Tooltip("The draw card button")]
        public Button drawBtn;

        [Tooltip("The discard card button")]
        public Button discardBtn;

        [Tooltip("The draw pile notification")]
        public Text drawPileText;

        [Tooltip("The discard pile notification")]
        public Text discardPileText;

        [Tooltip("The toast")]
        public Text toast;

        [Tooltip("The card prefab")]
        public GameObject cardPrefab;

        [Tooltip("The attack effect prefab")]
        public GameObject boomPrefab;

        [Tooltip("The health buff prefab")]
        public GameObject HealthBuffPrefab;

        [Tooltip("This curve defines the motion path of card during dropping card after playing card")]
        public AnimationCurve drop_card_curve;

        [Tooltip("This curve defines the motion path of cards during discarding animation")]
        public AnimationCurve clear_card_curve;

        [Tooltip("Curves in the list define the motion paths of cards during shuffling animation")]
        public List<AnimationCurve> shuffle_card_curve;

        /// <summary>
        /// The card last frame mouse is on.
        /// </summary>
        private int lastFrameMouseOn = -1;

        /// <summary>
        /// The card clicked by mouse.
        /// </summary>
        private int mouseClickCard = -1;

        /// <summary>
        /// The card mouse focus on.
        /// </summary>
        private int focusOnCard = -1;

        /// <summary>
        /// The half size of a card.
        /// </summary>
        private float cardHalfSize = 0.0f;

        /// <summary>
        /// Is shuffleing card.
        /// </summary>
        private bool shufflingCard = false;

        /// <summary>
        /// The playing cards list.
        /// </summary>
        private List<Card> playingCard = new List<Card>();

        /// <summary>
        /// The shuffle card effects list.
        /// </summary>
        private List<GameObject> shuffleCardsEffects = new List<GameObject>();

        /// <summary>
        /// The shuffle card delay time list.
        /// </summary>
        private List<float> shuffleCardDelay = new List<float>();

        /// <summary>
        /// The shuffle begin time.
        /// </summary>
        private float shuffleBegin;

        /// <summary>
        /// The card focus on character.
        /// </summary>
        private GameObject focusOnPlayer = null;

        /// <summary>
        /// The last add hand card time.
        /// </summary>
        private float lastAddHandCardTime;

        /// <summary>
        /// The card total number.
        /// </summary>
        private int cardTotalNum;

        /// <summary>
        /// The hands list.
        /// </summary>
        private List<Card> handCards = new List<Card>();

        /// <summary>
        /// The draw pile cards.
        /// </summary>
        private Queue<Card> drawPileCards = new Queue<Card>();

        /// <summary>
        /// The discard pile cards.
        /// </summary>
        private Queue<Card> discardPileCards = new Queue<Card>();

        private bool playerTurn = true;


        /// <summary>
        /// The hands limit.
        /// </summary>
        private const int HAND_CARD_LIMIT = 10;
        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes the draw pile cards callback.
        /// </summary>
        public void onInitializeDeck(List<CardData> cards)
        {
            this.cardTotalNum = cards.Count;
            while(cards.Count != 0)
            {
                int rand = UnityEngine.Random.Range(0, cards.Count);
                AddDeckCard(cards[rand]);
                cards.RemoveAt(rand);
            }
            this.ShuffleCardAnimation();
        }



        #endregion

        #region Private Methods

        #region LifeCycle Calls


        private void Start()
        {
            EventManager.current.InitializeDeck += onInitializeDeck;
            EventManager.current.InitializeDeckTrigger(); 
            
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            // Shuffle animation has the highest priority to display
            if (this.shufflingCard == false)
            {
                if (this.mouseClickCard == -1)//
                {
                    this.CalCardsTransform();
                }

                this.UpdateOnBoardCard();
                if (playerTurn)
                {

                    this.UpdateMouseClickCard();
                    this.UpdateMouseClickUnit();
                    this.UpdateMouseOnUnit();
                }
                // Updates arrows
                //EventManager.TriggerEvent(Events.UpdateArrows, this.focusOnCard);
                EventManager.current.UpdateArrowsTrigger(this.focusOnCard);
            }
        }


        /// <summary>
        /// Frame-rate independent MonoBehaviour. FixedUpdate message for physics calculations.
        /// MonoBehaviour.FixedUpdate is called every fixed frame-rate frame. Compute Physics system calculations after FixedUpdate. 0.02 seconds(
        /// 50 calls per second) is the default time between calls. Use Time.fixedDeltaTime to access this value. Alter it by setting it to your 
        /// preferred value within a script, or, navigate to Edit > Settings > Time > Fixed Timestep and set it there. Use FixedUpdate when using 
        /// Rigidbody. Set a force to a Rigidbody and it applies each fixed frame. FixedUpdate occurs at a measured time step that typically does 
        /// not coincide with MonoBehaviour.Update.
        /// </summary>
        private void FixedUpdate()
        {
            if (!this.shufflingCard)
            {
                this.UpdateCardRotate();
                this.UpdateCardPosition();
                this.UpdateCardScale();
                this.UpdateCardFollowMouse();
            }
            else
            {
                this.UpdateCardShuffling();
            }

            this.UpdateCardEffect();
            this.UpdateCardPlaying();
        }
        #endregion


        #region Basic card operation
        /// <summary>
        /// Adds card into discard pile.
        /// </summary>
        /// <param name="card">The card instance.</param>
        private void AddDiscardPileCard(Card card)
        {
            this.discardPileCards.Enqueue(card);
            this.discardPileText.text = this.discardPileCards.Count.ToString();
        }

        /// <summary>
        /// Adds card into draw pile card.
        /// </summary>
        /// <param name="cardInfo">Card info.</param>
        private void AddDeckCard(CardData data)
        {
            Card card = new Card();
            card.data = data;
            cardPrefab.GetComponent<CardDisplay>().setDisplay(data);
            card.Instance = (GameObject)Instantiate(this.cardPrefab);
            card.Instance.SetActive(false);
            
            this.drawPileCards.Enqueue(card);
            this.drawPileText.text = this.drawPileCards.Count.ToString();
        }

        /// <summary>
        /// Adds a card into hands.
        /// </summary>
        private void AddHandCard()
        {
            if (this.shufflingCard == true) return;

            if (this.drawPileCards.Count == 0)
            {
                var text = (Text)toast.GetComponent<Text>();
                if (toast.gameObject.activeSelf == false) toast.gameObject.SetActive(true);
                text.text = "No cards left in draw pile!";
                return;
            }

            if (this.handCards.Count >= HAND_CARD_LIMIT)
            {
                var text = (Text)toast.GetComponent<Text>();
                if (toast.gameObject.activeSelf == false) toast.gameObject.SetActive(true);
                text.text = "The limit of cards in hand is 10!";
                return;
            }

            // Prepare card states for sending to hand
            Card card = this.GetCardFromDrawPile();
            card.MoveSpeed = this.sendCardMoveSpeed;
            card.TargetScale = this.cardNormalScale;
            card.ScaleSpeed = this.sendCardScaleSpeed;
            card.NonInteractBegin = Time.time;
            var getCardPileOffsetX = 1.0f;
            card.Instance.transform.position = new Vector3(this.getCardPile.position.x + getCardPileOffsetX, this.getCardPile.position.y, 0);  // The start position for sending card
            card.Instance.transform.localScale = new Vector3(0.2f, 0.2f, 0);      // The start size of the card will be sent to hand
            card.Instance.transform.SetParent(this.handCardObj.transform, false);
            card.Instance.name = "Card:" + (this.handCards.Count).ToString();
            card.Instance.GetComponent<Canvas>().sortingOrder = this.handCards.Count;
            this.handCards.Add(card);
            this.UpdateHandsIntervalAngle();
            this.CalCardsTransform(true);
            cardHalfSize = card.Instance.GetComponent<Canvas>().transform.localScale.y * card.Instance.transform.localScale.y / 2.0f;
            this.lastAddHandCardTime = Time.time;
        }

        /// <summary>
        /// Gets a card from draw pile.
        /// </summary>
        /// <returns>The card instance.</returns>
        private Card GetCardFromDrawPile()
        {
            var card = this.drawPileCards.Dequeue();
            card.Instance.SetActive(true);
            this.drawPileText.text = this.drawPileCards.Count.ToString();
            return card;
        }

        /// <summary>
        /// Clears all hand cards.
        /// </summary>
        private void ClearHandCard()
        {
            if (this.shufflingCard == true) return;
            if (Time.time - this.lastAddHandCardTime <= 0.5f) return;
            this.handCards.ForEach(card =>
            {
                card.IsDropping = true;
                card.Instance.GetComponent<TrailRenderer>().enabled = true;
                card.Instance.transform.localScale = new Vector3(this.miniCardScale, this.miniCardScale, this.miniCardScale);
                card.Instance.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                card.Instance.transform.Rotate(new Vector3(0.0f, 0.0f, Random.Range(0.0f, 30.0f)));                // Every card in hand has random direction for discard effect
                card.TotalDistance = Mathf.Abs(card.Instance.transform.position.x - dropCardPile.position.x);
                card.OriginHighY = card.Instance.transform.position.y;
                this.playingCard.Add(card);
            });

            while (this.handCards.Count > 0)
            {
                this.DropHandCard(0);
            }

            this.focusOnCard = -1;
            this.mouseClickCard = -1;

            // Hides the arrows;
            EventManager.current.HideArrowsTrigger();
        }

        /// <summary>
        /// Drops hand card.
        /// </summary>
        /// <param name="idx">The drop card index.</param>
        private void DropHandCard(int idx)
        {
            if (this.lastFrameMouseOn != -1)
            {
                this.MouseOffCard(this.lastFrameMouseOn);
                this.OffsetSideCards(this.lastFrameMouseOn, 0.0f, 0.0f);
                this.lastFrameMouseOn = -1;
            }
            this.focusOnCard = -1;
            this.mouseClickCard = -1;
            this.handCards[idx].Instance.GetComponent<BoxCollider2D>().enabled = false;  // Can not be touched anymore
            this.handCards.RemoveAt(idx);
            this.ReArrangeCard();
            this.UpdateHandsIntervalAngle();
            this.CalCardsTransform(true);
        }

        // Update the card directions by different card numbers
        void UpdateHandsIntervalAngle()
        {
            for (int i = 0; i < this.cardNumForAngles.Count; ++i)
            {
                if (this.handCards.Count <= this.cardNumForAngles[i] && (i == 0 || this.handCards.Count > this.cardNumForAngles[i - 1]))
                {
                    this.rotateAngle = this.anglesForCardNum[i];
                }
            }
        }

        // The card's name should be modifed by the number of cards in hand
        private void ReArrangeCard()
        {
            for (int i = 0; i < this.handCards.Count; ++i)
            {
                this.handCards[i].Instance.name = "Card:" + i.ToString();
                this.handCards[i].Instance.GetComponent<Canvas>().sortingOrder = i;
            }
        }
        #endregion

        #region Set card state
        /// <summary>
        /// Changes the card state which gets the mouse focus.
        /// </summary>
        /// <param name="idx"></param>
        private void MouseOnCard(int idx)
        {
            Card card = handCards[idx];
            GameObject cardgo = card.Instance;
            card.SortOrder = cardgo.GetComponent<Canvas>().sortingOrder;
            cardgo.GetComponent<Canvas>().sortingOrder = 100;   // Move to the topest layer when a card is checking by the player
            card.TargetScale = cardBigScale;
            card.MoveSpeed = moveSpeed;
            card.ScaleSpeed = scaleSpeed;
        }

        /// <summary>
        /// Changes the card transform which loses the mouse focus.
        /// </summary>
        /// <param name="idx"></param>
        private void MouseOffCard(int idx)
        {
            if (idx == -1) return;
            Card card = handCards[idx];
            GameObject cardgo = card.Instance;
            cardgo.GetComponent<Canvas>().sortingOrder = card.SortOrder;
            card.TargetScale = cardNormalScale;
            card.MoveSpeed = slowMoveSpeed;
            card.ScaleSpeed = slowScaleSpeed;
        }
        #endregion

        #region Updates Card Animation
        /// <summary>
        /// Updates the card playing animation.
        /// </summary>
        private void UpdateCardPlaying()
        {
            foreach (var card in playingCard)
            {
                if (card == null || card.IsPlaying == true || card.IsDropping == true)
                {
                    continue;
                }

                var dstPos = new Vector3(0, cardPlayRiseDstY, 0);  // The playing card will rising up
                if ((card.Instance.transform.position - dstPos).magnitude <= Time.fixedDeltaTime * cardPlaySpeed)
                {
                    if (Time.time - card.DropDisplayTime < 0.3f)
                    {
                        return;
                    }
                    // Prepare card state to drop
                    card.Instance.transform.position = dstPos;
                    card.IsPlaying = true;
                    
                    card.TotalDistance = dropCardPile.position.x - dstPos.x;
                    card.Instance.GetComponent<TrailRenderer>().enabled = true;
                    card.Instance.transform.localScale = new Vector3(miniCardScale, miniCardScale, miniCardScale);
                    card.Instance.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    card.Instance.transform.Rotate(new Vector3(0.0f, 0.0f, -120.0f));
                    card.TotalDistance = Mathf.Abs(card.Instance.transform.position.x - dropCardPile.position.x);
                    card.OriginHighY = card.Instance.transform.position.y;
                    // Display skill effect
                    Unit targetUnit = card.TargetPlayer.GetComponent<Unit>();
                    bool flag = targetUnit.getUnitType().Equals(card.data.GetCardValidTarget());
                    Debug.Log(flag);
                    if (flag)
                    {
                        
                        //TODO Integrate with battlesystem
                        EventManager.current.CardActivationTrgger(targetUnit,card.data);
                        var icon = (GameObject)Instantiate(card.TargetPlayer.GetComponent<Unit>().getUnitType().Equals(UnitType.MONSTER) ? this.boomPrefab : this.HealthBuffPrefab);
                        var target_player_pos = card.TargetPlayer.transform.position;
                        icon.transform.position = new Vector3(target_player_pos.x, target_player_pos.y + skillIconOffsetY, -10.0f);
                        Destroy(icon, 2.0f);
                    }
                    return;
                }
                card.Instance.transform.position = Vector3.MoveTowards(card.Instance.transform.position, dstPos, Time.fixedDeltaTime * cardPlaySpeed);
                card.DropDisplayTime = Time.time;
            }
        }

        /// <summary>
        /// Updates the card shuffle animation.
        /// </summary>
        private void UpdateCardShuffling()
        {
            for (int i = 0; i < shuffleCardsEffects.Count; ++i)
            {
                // Calculate card position on motion path by shuffle card curves
                var currentTime = Time.time;
                if (currentTime - shuffleBegin < shuffleCardDelay[i]) continue;
                var card = shuffleCardsEffects[i];
                var delta_x = Time.deltaTime * cardShuffleFastSpeed;
                var x_distance = card.transform.position.x - delta_x - getCardPile.position.x;
                var totalDistance = dropCardPile.position.x - getCardPile.position.x;
                var factor = (totalDistance - x_distance) / totalDistance;
                card.transform.position = new Vector3(card.transform.position.x - delta_x, dropCardPile.position.y + shuffle_card_curve[i].Evaluate(factor), 0);
                var cardShuffleEps = 0.5f;
                if (Mathf.Abs(getCardPile.position.x - card.transform.position.x) <= cardShuffleEps)
                {
                    card.SetActive(false);
                }
            }

            if (!shuffleCardsEffects.Exists(e => e.activeSelf == true))
            {
                shuffleCardsEffects.ForEach(e => Destroy(e));
                shuffleCardsEffects.Clear();
                shufflingCard = false;
                StartCoroutine(SendHandCards());      // Send card to hand automatically after shuffling animation
            }
        }

        /// <summary>
        /// Sends hand cards.
        /// </summary>
        /// <returns></returns>
        private IEnumerator SendHandCards()
        {
            ShufflePileCard();
            for (int i = 0; i < StartingHandSize; ++i)
            {
                yield return new WaitForSeconds(0.2f);
                AddHandCard();
            }
        }

        /// <summary>
        /// Shuffles the cards in pile.
        /// </summary>
        private void ShufflePileCard()
        {
            // All cards will be sent from discard pile to draw pile
            while (discardPileCards.Count > 0)
            {
                var card = discardPileCards.Dequeue();
                AddDeckCard(card.data);
            }

            drawPileText.text = drawPileCards.Count.ToString();
            discardPileText.text = discardPileCards.Count.ToString();
        }

        /// <summary>
        /// Updates the card effect animation.
        /// </summary>
        private void UpdateCardEffect()
        {
            var clear_flag = true;
            for (int i = 0; i < playingCard.Count; ++i)
            {
                var card = playingCard[i];
                if (card == null) continue;
                clear_flag = false;
                if (card.IsPlaying == false && card.IsDropping == false) continue;
                var delta_x = Time.fixedDeltaTime * cardDropFastSpeed;
                if (card.IsDropping == true)
                {
                    // Calculates the card motion path by clear card curve 
                    var x_distance = dropCardPile.position.x - (card.Instance.transform.position.x + delta_x);
                    var factor = (card.TotalDistance - x_distance) / card.TotalDistance;
                    card.Instance.transform.position = new Vector3(card.Instance.transform.position.x + delta_x, card.OriginHighY + (clear_card_curve.Evaluate(factor) + 0.5f) * Mathf.Abs(card.OriginHighY - dropCardPile.position.y) / 0.5f, 0);
                }
                else
                {
                    // Calculates the card motion path by drop card curve
                    var x_distance = dropCardPile.position.x - (card.Instance.transform.position.x + delta_x);
                    var factor = (card.TotalDistance - x_distance) / card.TotalDistance;
                    card.Instance.transform.position = new Vector3(card.Instance.transform.position.x + delta_x, card.OriginHighY + (drop_card_curve.Evaluate(factor) - 1.0f) * Mathf.Abs(card.OriginHighY - dropCardPile.position.y), 0);
                }

                var cardDropEps = 0.5f;
                if (Mathf.Abs(dropCardPile.position.x - card.Instance.transform.position.x) <= cardDropEps)
                {
                    // Card reached the discard pile will be destroyed
                    card.Instance.SetActive(false);
                    Destroy(card.Instance);
                    AddDiscardPileCard(card);
                    playingCard[i] = null;
                    var all_destroyed = true;
                    for (int j = 0; j < playingCard.Count; ++j)
                    {
                        if (playingCard[j] != null) all_destroyed = false;
                    }
                    if (all_destroyed)
                    {
                        // Play shuffle animation when the number of cards in the discard pile equals to the total card number
                        if (discardPileCards.Count == cardTotalNum)
                        {
                            ShuffleCardAnimation();
                        }
                    }
                }
            }

            if (clear_flag)
            {
                playingCard.Clear();
            }
        }

        /// <summary>
        /// Plays the shuffle cards animation.
        /// </summary>
        private void ShuffleCardAnimation()
        {
            var shuffleCardCurveCount = shuffle_card_curve.Count;
            var halfShuffleCardCurveCount = shuffleCardCurveCount >> 1;

            for (int i = 0; i < shuffleCardCurveCount; ++i)
            {
                // Prepare card state to shuffle
                var curve = shuffle_card_curve[i];
                var card = (GameObject)Instantiate(cardPrefab);
                card.transform.localScale = new Vector3(miniCardScale, miniCardScale, 0);
                card.transform.position = dropCardPile.position;                           // Start from discard pile 
                card.transform.Rotate(new Vector3(0, 0, Random.Range(30.0f, 90.0f)));      // Random directions
                card.GetComponent<TrailRenderer>().enabled = true;                         // Enable the trail renderer
                card.GetComponent<Canvas>().sortingOrder = 0;
                shuffleCardsEffects.Add(card);

                // The first half of the curve list's motion paths are shorter than the second half ones
                // So the first half of the cards' delay time should be longer than the second half ones
                if (i < halfShuffleCardCurveCount)
                {
                    shuffleCardDelay.Add(Random.Range(0.1f, 0.2f));
                }
                else
                {
                    shuffleCardDelay.Add(Random.Range(0.0f, 0.1f));
                }
            }

            shuffleBegin = Time.time;
            shufflingCard = true;
        }
        #endregion

        #region Updates Card Transform
        /// <summary>
        /// Updates the card rotate.
        /// </summary>
        private void UpdateCardRotate()
        {
            foreach (var card in handCards)
            {
                Transform transform = card.Instance.transform;
                if (Mathf.Abs(card.CurAngle - card.TargetAngle) <= Time.fixedDeltaTime * rotateSpeed)
                {
                    card.CurAngle = card.TargetAngle;
                    transform.rotation = Quaternion.Euler(0, 0, card.TargetAngle);
                }
                else if (card.CurAngle > card.TargetAngle)
                {
                    card.CurAngle -= Time.fixedDeltaTime * rotateSpeed;
                    transform.Rotate(0, 0, -Time.fixedDeltaTime * rotateSpeed);
                }
                else
                {
                    card.CurAngle += Time.fixedDeltaTime * rotateSpeed;
                    transform.Rotate(0, 0, Time.fixedDeltaTime * rotateSpeed);
                }
            }
        }

        /// <summary>
        /// Updates the card position.
        /// </summary>
        private void UpdateCardPosition()
        {
            foreach (var card in handCards)
            {
                Transform transform = card.Instance.transform;
                transform.position = new Vector3(transform.position.x, transform.position.y, card.TargetPosition.z);
                if ((transform.position - card.TargetPosition).magnitude <= Time.fixedDeltaTime * card.MoveSpeed)
                {
                    transform.position = card.TargetPosition;
                    card.MoveSpeed = slowMoveSpeed;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, card.TargetPosition, Time.fixedDeltaTime * card.MoveSpeed);
                }
            }
        }

        /// <summary>
        /// Updates the card scale.
        /// </summary>
        private void UpdateCardScale()
        {
            foreach (var card in handCards)
            {
                Transform transform = card.Instance.transform;
                if (transform.localScale.x >= card.TargetScale && transform.localScale.x - card.TargetScale <= Time.fixedDeltaTime * card.ScaleSpeed)
                {
                    transform.localScale = new Vector3(card.TargetScale, card.TargetScale, 0.0f);
                    card.ScaleSpeed = slowScaleSpeed;
                }
                else if (transform.localScale.x <= card.TargetScale && card.TargetScale - transform.localScale.x <= Time.fixedDeltaTime * card.ScaleSpeed)
                {
                    transform.localScale = new Vector3(card.TargetScale, card.TargetScale, 0.0f);
                    card.ScaleSpeed = slowScaleSpeed;
                }
                else
                {
                    float scale = 0.0f;
                    if (transform.localScale.x <= card.TargetScale)
                    {
                        scale = Mathf.Min(card.TargetScale, transform.localScale.x + Time.fixedDeltaTime * card.ScaleSpeed);
                    }
                    else if (transform.localScale.x >= card.TargetScale)
                    {
                        scale = Mathf.Max(card.TargetScale, transform.localScale.x - Time.fixedDeltaTime * card.ScaleSpeed);
                    }
                    transform.localScale = new Vector3(scale, scale, 0.0f);
                }
            }
        }

        /// <summary>
        /// Updates the card following mouse.
        /// </summary>
        private void UpdateCardFollowMouse()
        {
            if (mouseClickCard != -1 && focusOnCard == -1)
            {
                var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                handCards[mouseClickCard].Instance.transform.position = new Vector3(mouse_pos.x, mouse_pos.y, -10.0f);
            }
        }
        #endregion

        #region Calculates Card Transform
        /// <summary>
        /// Calculates the direction of the card in hand without any effects.
        /// </summary>
        /// <param name="idx">The card index.</param>
        /// <returns>The angle.</returns>
        private float OriginalAngle(int idx)
        {
            float leftAngle = (handCards.Count - 1) * rotateAngle / 2;
            return leftAngle - idx * rotateAngle;
        }

        /// <summary>
        /// Calculates the card fall down trail.
        /// </summary>
        /// <param name="idx">The card index.</param>
        /// <returns>The card position.</returns>
        private Vector3 FallDownPosition(int idx)
        {
            float angle = OriginalAngle(idx) + this.handCards[idx].OffsetAngle;
            return new Vector3(centerPoint.x - centerRadius * Mathf.Sin(Utils.ConvertAngleToArc(angle)), centerPoint.y + centerRadius * Mathf.Cos(Utils.ConvertAngleToArc(angle)), 0.0f);
        }

        /// <summary>
        /// Calculates the card rise up trail.
        /// </summary>
        /// <param name="idx">The card index.</param>
        /// <returns>The card position.</returns>
        private Vector3 PushUpPosition(int idx)
        {
            Vector3 fall_down_position = FallDownPosition(idx);
            return new Vector3(fall_down_position.x, cardOffsetY, -10.0f);
        }

        /// <summary>
        /// Calculates the card transform by combined states.
        /// </summary>
        /// <param name="force_update">Is need force update.</param>
        void CalCardsTransform(bool force_update = false)
        {
            int idx = this.MouseOnCard();

            if (idx >= -1 || force_update == true)
            {
                Card card = null;
                for (int i = 0; i < handCards.Count; i++)
                {
                    if (i == idx) continue;
                    card = handCards[i];
                    card.TargetAngle = this.OriginalAngle(i);
                    card.TargetPosition = FallDownPosition(i);
                    card.Instance.transform.position = new Vector3(card.Instance.transform.position.x, card.Instance.transform.position.y, 0.0f);
                }
                if (idx >= 0)
                {
                    card = handCards[idx];
                    card.TargetPosition = PushUpPosition(idx);
                    card.Instance.transform.position = new Vector3(card.Instance.transform.position.x, card.Instance.transform.position.y, -10.0f);
                    card.TargetAngle = 0.0f;
                    card.CurAngle = 0.0f;
                    card.Instance.transform.rotation = Quaternion.Euler(0, 0, card.TargetAngle);
                }
            }
        }

        /// <summary>
        /// This function is used to calculate cards'transform by their mutual interaction in hand from the checking card to neighbor cards one by one
        /// When the mouse moves on a card, it will rise up and push neighbor cards away.
        /// </summary>
        /// <param name="idx">The checking card index.</param>
        /// <param name="front_angle">Left push angle.</param>
        /// <param name="end_angle">Right push angle.</param>
        void OffsetSideCards(int idx, float front_angle, float end_angle)
        {
            int front = idx - 1;
            int end = idx + 1;
            Card card = handCards[idx];
            card.OffsetAngle = 0.0f;
            while (front >= 0)
            {
                card = handCards[front];
                card.OffsetAngle = front_angle;
                front_angle = Mathf.Max(0.0f, front_angle - offsetAngleDelta);  // The push strength decreases from center to left
                front--;
            }
            while (end < handCards.Count)
            {
                card = handCards[end];
                card.OffsetAngle = -end_angle;
                end_angle = Mathf.Max(0.0f, end_angle - offsetAngleDelta);   // The push strength decreases from center to right
                end++;
            }
        }
        #endregion

        #region Updates Mouse Interaction
        /// <summary>
        /// Updates the card onboard.
        /// </summary>
        private void UpdateOnBoardCard()
        {
            if (mouseClickCard == -1) return;
            var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mouse_pos.y >= -3.0f + cardHalfSize / 2)
            {
                var card = handCards[mouseClickCard];
                focusOnCard = mouseClickCard;
                card.NonInteractBegin = Time.time;
                card.TargetAngle = 0.0f;
                card.Instance.transform.position = new Vector3(card.Instance.transform.position.x, card.Instance.transform.position.y, -10.0f);
                card.TargetPosition = new Vector3(0, -3.0f, -10.0f);
                card.MoveSpeed = (card.Instance.transform.position - FallDownPosition(mouseClickCard)).magnitude * 2 / nonInteractDelay;
            }
            else if (focusOnCard != -1)
            {
                if (lastFrameMouseOn != -1)
                {
                    this.MouseOffCard(lastFrameMouseOn);
                    this.OffsetSideCards(lastFrameMouseOn, 0.0f, 0.0f);
                    lastFrameMouseOn = -1;
                }

                var card = handCards[mouseClickCard];
                card.NonInteractBegin = Time.time;
                card.Instance.transform.position = new Vector3(card.Instance.transform.position.x, card.Instance.transform.position.y, 0);
                card.MoveSpeed = (card.Instance.transform.position - FallDownPosition(mouseClickCard)).magnitude * 2 / nonInteractDelay;
                card.ScaleSpeed = slowScaleSpeed;
                this.CalCardsTransform(true);
                mouseClickCard = -1;
                focusOnCard = -1;
            }
        }

        /// <summary>
        /// This function is called to check if the mouse has clicked on a card
        /// </summary>
        private void UpdateMouseClickCard()
        {
            if (Input.GetMouseButtonDown(0) && mouseClickCard == -1)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.name.StartsWith("Card:"))
                {
                    mouseClickCard = int.Parse(hit.collider.gameObject.name.Split(':')[1]);
                    var card = handCards[mouseClickCard];
                    // Cards in non interact state can no be clicked, used to improve the performance
                    if (lastFrameMouseOn != mouseClickCard || Time.time - card.NonInteractBegin < nonInteractDelay)
                    {
                        mouseClickCard = -1;
                    }
                }
            }
            if (mouseClickCard != -1 && Input.GetMouseButtonDown(1))
            {
                if (lastFrameMouseOn != -1)
                {
                    MouseOffCard(lastFrameMouseOn);
                    OffsetSideCards(lastFrameMouseOn, 0.0f, 0.0f);
                    lastFrameMouseOn = -1;
                }

                var card = handCards[mouseClickCard];
                card.NonInteractBegin = Time.time;
                card.Instance.transform.position = new Vector3(card.Instance.transform.position.x, card.Instance.transform.position.y, 0);
                card.MoveSpeed = (card.Instance.transform.position - FallDownPosition(mouseClickCard)).magnitude * 2 / nonInteractDelay;
                card.ScaleSpeed = slowScaleSpeed;
                CalCardsTransform(true);
                mouseClickCard = -1;
                focusOnCard = -1;
            }
        }

        /// <summary>
        /// Checks if the mouse on the monster.
        /// </summary>
        private void UpdateMouseOnUnit()
        {
            focusOnPlayer = null;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && focusOnCard != -1)
            {
                GameObject go = hit.collider.gameObject;
                Unit unit = go.GetComponent<Unit>();
                if (unit)
                {
                    focusOnPlayer = go.gameObject;

                    EventManager.current.UpdateArrowsColorTrigger(unit.getUnitType());
                    return;
                }
                else EventManager.current.UpdateArrowsColorTrigger(UnitType.NONE);
            }
        }

        /// <summary>
        /// Checks if mouse clicked monster.
        /// </summary>
        private void UpdateMouseClickUnit()
        {
            if (focusOnCard != -1 && focusOnPlayer != null && Input.GetMouseButtonUp(0))
            {
                //TODO: IMPROVE THIS
                if(!focusOnPlayer.GetComponent<Unit>().getUnitType().Equals(handCards[focusOnCard].data.GetCardValidTarget()))
                {return;}
                handCards[focusOnCard].TargetPlayer = focusOnPlayer;
                
                playingCard.Add(handCards[focusOnCard]);

                // Drop the card from hand
                DropHandCard(focusOnCard);
                focusOnCard = -1;
                mouseClickCard = -1;

                // Hide the arrow
                EventManager.current.HideArrowsTrigger();
            }
        }

        /// <summary>
        /// Gets the card index in hand which the mouse is on and change other cards' state.
        /// state >= 0 the mouse is on a card
        /// state == -1 the mouse is not on a card in this frame but last frame the mouse is on a card
        /// state == -2 the mouse is not on a card in this frame and last frame
        /// </summary>
        /// <returns>Mouse on card state.</returns>
        private int MouseOnCard()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.name.StartsWith("Card:"))
            {
                GameObject cardd = hit.collider.gameObject;
                int idx = int.Parse(hit.collider.gameObject.name.Split(':')[1]);
                if (lastFrameMouseOn != idx)
                {
                    MouseOffCard(lastFrameMouseOn);
                    float currentTime = Time.time;
                    if (currentTime - handCards[idx].LastOnTime > mouseOnInterval && currentTime - handCards[idx].NonInteractBegin >= nonInteractDelay)
                    {
                        MouseOnCard(idx);
                        OffsetSideCards(idx, leftPushAngle, rightPushAngle);
                        lastFrameMouseOn = idx;
                        handCards[idx].LastOnTime = currentTime;
                        return idx;
                    }
                    if (lastFrameMouseOn >= 0)
                    {
                        OffsetSideCards(lastFrameMouseOn, 0.0f, 0.0f);
                    }
                    lastFrameMouseOn = -1;
                    return -1;
                }
            }
            else if (lastFrameMouseOn != -1)
            {
                MouseOffCard(lastFrameMouseOn);
                OffsetSideCards(lastFrameMouseOn, 0.0f, 0.0f);
                lastFrameMouseOn = -1;
                return -1;
            }
            return -2;
        }
        #endregion

        #endregion
    }
}