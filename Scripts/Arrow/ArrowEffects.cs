namespace CardGameUIEffect
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections.Generic;

    /// <summary>
    /// The ArrowEffects class.
    /// </summary>
    public class ArrowEffects : MonoBehaviour
    {
        #region Fields
        [Tooltip("Number of arrow parts, the head is the last one")]
        public int arrowsNum = 11;

        [Tooltip("The arrow head sprite with gray color")]
        public Sprite arrowHeadGraySprite;

        [Tooltip("The arrow body sprite with gray color")]
        public Sprite arrowBodyGraySprite;

        [Tooltip("The arrow head sprite with green color")]
        public Sprite arrowHeadGreenSprite;

        [Tooltip("The arrow body sprite with green color")]
        public Sprite arrowBodyGreenSprite;

        [Tooltip("The arrow head sprite with red color")]
        public Sprite arrowHeadRedSprite;

        [Tooltip("The arrow body sprite with red color")]
        public Sprite arrowBodyRedSprite;

        [Tooltip("The arrow head prefab")]
        public GameObject arrowHeadPrefab;

        [Tooltip("The arrow body prefab")]
        public GameObject arrowBodyPrefab;

        [Tooltip("The draw card button")]
        public Button drawBtn;

        [Tooltip("The discard card button")]
        public Button discardBtn;

        /// <summary>
        /// The arrow instances.
        /// </summary>
        private List<GameObject> arrows = new List<GameObject>();
        #endregion

        #region Private Methods

        #region Lifecycle Methods
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Awake is used to initialize any variables or game state before the game starts.Awake is called only once during the 
        /// lifetime of the script instance. Awake is called after all objects are initialized so you can safely speak to other 
        /// objects or query them using for example GameObject. Awake is called in a random order between objects. Because of 
        /// this, you should use Awake to set up references between scripts, and use Start to pass any information back and forth.
        /// Awake is always called before any Start functions. This allows you to order initialization of scripts. Awake can not 
        /// act as a coroutine. Use Awake instead of the constructor for initialization, as the serialized state of the component 
        /// is undefined at construction time. Awake is called once, just like the constructor.
        /// </summary>


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

            InitArrowInstance();
            EventManager.current.onHideArrows += this.OnHideArrows;
            EventManager.current.onUpdateArrows += this.OnUpdateArrows;
            EventManager.current.onUpdateArrowsColor += this.OnChangeArrowsColor;
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Regists events.
        /// </summary>


        /// <summary>
        /// Initializes the arrow instances.
        /// </summary>
        private void InitArrowInstance()
        {
            // The last one is arrow's head.
            for (int i = 0; i < arrowsNum; ++i)
            {
                GameObject arrow = (GameObject)Instantiate(arrowBodyPrefab);
                if (i == arrowsNum - 1)
                {
                    arrow = (GameObject)Instantiate(arrowHeadPrefab);
                }
                arrow.transform.position = Vector3.zero;
                arrows.Add(arrow);
            }
        }
        #endregion

        #endregion

        #region Public Methods
        /// <summary>
        /// Change arrows' color callback.
        /// </summary>
        /// <param name="type"></param>
        public void OnChangeArrowsColor(UnitType type)
        {
            Sprite arrowBody = null;
            Sprite arrowHead = null;

            switch (type)
            {
                case UnitType.NONE:
                    arrowBody = arrowBodyGraySprite;
                    arrowHead = arrowHeadGraySprite;
                    break;

                case UnitType.CHARACTER:
                    arrowBody = arrowBodyGreenSprite;
                    arrowHead = arrowHeadGreenSprite;
                    break;

                case UnitType.MONSTER:
                    arrowBody = arrowBodyRedSprite;
                    arrowHead = arrowHeadRedSprite;
                    break;
                default:
                    throw new ArgumentException("Invalid UnitType");
            }


            for (int i = 0; i < arrowsNum; ++i)
            {
                var arrow = arrows[i];
                arrow.GetComponent<SpriteRenderer>().sprite = arrowBody;
                if (i == arrowsNum - 1)
                {
                    arrow.GetComponent<SpriteRenderer>().sprite = arrowHead;
                }
            }
        }

        /// <summary>
        /// Hides arrows callback.
        /// </summary>
        public void OnHideArrows()
        {
            for (int i = 0; i < arrows.Count; ++i)
            {
                if (arrows[i].activeSelf == true)
                {
                    arrows[i].SetActive(false);
                }
            }
        }

        /// <summary>
        /// Updates arrows callback.
        /// The joints' positions of the arrow are calculated by Bezier curve, it includes a start point, an end point and two control points
        /// The formula of the curve can be expressed as follows:
        /// position.x = startPointPosition.x * (1-t)^3 + 3 * controlPointAPosition.x * t * (1-t)^2 + 3 * controlPointBPosition.x * t^2 * (1-t) + endPointPosition.x * t^3;
        /// position.y = startPointPosition.y * (1-t)^3 + 3 * controlPointAPosition.y * t * (1-t)^2 + 3 * controlPointBPosition.y * t^2 * (1-t) + endPointPosition.y * t^3;
        /// The joint direction of the arrow can be calculated by the vector from this joint to last joint
        /// <param name="focusOnCard">The focus on card state.</param>
        /// </summary>
        public void OnUpdateArrows(int focusOnCard)
        {
            if (focusOnCard != -1)
            {
                var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float mouse_x = mouse_pos.x;
                float mouse_y = mouse_pos.y;

                // The control points' positions are changed with the mouse position
                // The parameters below have been modified to best performance, no need to change!!!
                float center_x = 0.0f;
                float center_y = -4.0f;
                float t = 0.0f;
                float controlA_x_factor = 0.3f;
                float controlA_y_factor = 0.8f;
                float controlB_x_factor = 0.1f;
                float controlB_y_factor = 1.4f;
                float controlA_x = center_x - (mouse_x - center_x) * controlA_x_factor;
                float controlA_y = center_y + (mouse_y - center_y) * controlA_y_factor;
                float controlB_x = center_x + (mouse_x - center_x) * controlB_x_factor;
                float controlB_y = center_y + (mouse_y - center_y) * controlB_y_factor;

                float last_arrow_z = -20.0f;
                float arrow_z = -15.0f;

                for (int i = 0; i < arrows.Count; i++)
                {
                    if (arrows[i].activeSelf == false)
                    {
                        arrows[i].SetActive(true);
                    }

                    t = (i + 1) * 1.0f / arrows.Count;
                    var transform = arrows[i].transform;

                    // Bezier curve calculates the joints' positions
                    var arrow_x = center_x * Mathf.Pow(1 - t, 3) + 3 * controlA_x * t * Mathf.Pow(1 - t, 2) + 3 * controlB_x * Mathf.Pow(t, 2) * (1 - t) + mouse_x * Mathf.Pow(t, 3);
                    var arrow_y = center_y * Mathf.Pow(1 - t, 3) + 3 * controlA_y * t * Mathf.Pow(1 - t, 2) + 3 * controlB_y * Mathf.Pow(t, 2) * (1 - t) + mouse_y * Mathf.Pow(t, 3);

                    if (i == arrows.Count - 1)
                    {
                        arrows[i].transform.position = new Vector3(arrow_x, arrow_y, last_arrow_z);
                    }
                    else
                    {
                        arrows[i].transform.position = new Vector3(arrow_x, arrow_y, arrow_z);
                    }

                    // The direction of a joint can be calculated by the vector from this joint to last joint
                    arrows[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    if (i > 0)
                    {
                        var len_x = arrows[i].transform.position.x - arrows[i - 1].transform.position.x;
                        var len_y = arrows[i].transform.position.y - arrows[i - 1].transform.position.y;
                        arrows[i].transform.Rotate(0, 0, Utils.GetAngleByVector(len_x, len_y));
                    }
                    else
                    {
                        var len_x = arrows[i + 1].transform.position.x - arrows[i].transform.position.x;
                        var len_y = arrows[i + 1].transform.position.y - arrows[i].transform.position.y;
                        arrows[i].transform.Rotate(0, 0, Utils.GetAngleByVector(len_x, len_y));
                    }

                    float scaleFactor = 0.03f;
                    arrows[i].transform.localScale = new Vector3(1.0f - scaleFactor * (arrows.Count - 1 - i), 1.0f - scaleFactor * (arrows.Count - 1 - i), 0);
                }

                drawBtn.enabled = false;
                discardBtn.enabled = false;
            }
            else
            {
                arrows.ForEach(arrow =>
                {
                    if (arrow.activeSelf)
                    {
                        arrow.SetActive(false);
                    }
                });

                drawBtn.enabled = true;
                discardBtn.enabled = true;
            }
        }
        #endregion
    }
}