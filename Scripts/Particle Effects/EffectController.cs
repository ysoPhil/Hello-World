namespace CardGameUIEffect
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// The effect controller class.
    /// </summary>
    public class EffectController : MonoBehaviour
    {
        #region Fields
        [Tooltip("Pending seconds to disapear")]
        public float pendingTime = 0.8f;

        [Tooltip("Scale decrease delta")]
        public float scaleDecDelta = 0.005f;

        [Tooltip("Alpha decrease delta")]
        public float alphaDecDelta = 0.02f;

        /// <summary>
        /// The start time.
        /// </summary>
        private float startTime;
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
            startTime = Time.time;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            // The attack and defense icon hold on for a while
            if (Time.time - startTime >= pendingTime)
            {
                var scale = this.transform.localScale;
                this.transform.localScale = new Vector3(scale.x - scaleDecDelta, scale.y - scaleDecDelta, scale.z - scaleDecDelta);  // Scale decrease
                var color = this.GetComponent<SpriteRenderer>().material.color;
                var new_color = new Color(color.r, color.g, color.b, color.a - alphaDecDelta);  // Alpha decrease
                if (new_color.a <= 0.0f)
                {
                    Destroy(gameObject);  // Self destroy when the alpha decrease to 0
                }
                this.GetComponent<SpriteRenderer>().material.color = new_color;
            }
        }
        #endregion

        #endregion
    }
}
