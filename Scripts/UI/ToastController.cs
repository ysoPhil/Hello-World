namespace CardGameUIEffect
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /*
    The ToastController class controlls a UI Panel 
    that tells the player when their hand is full
    */
    public class ToastController : MonoBehaviour
    {
        #region Fields
        [Tooltip("Toast pop up duration")]
        public float duration = 2.0f;

        /// <summary>
        /// The pending time to destroy self.
        /// </summary>
        private bool pendingDestroy = false;
        #endregion

        #region Private Methods

        #region LifeCycle Calls
        //Possibly not necessary
        private void Start()
        {
            pendingDestroy = false;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (this.gameObject.activeSelf == true && pendingDestroy == false)
            {
                pendingDestroy = true;
                Debug.Log("Toaster disabled");
                StartCoroutine(this.SelfDisable());
            }
        }
        #endregion

        #region Coroutines
        /// <summary>
        /// Disables the toast.
        /// </summary>
        /// <returns></returns>
        private IEnumerator SelfDisable()
        {
            Debug.Log("TOASTER:: In IEnum");
            yield return new WaitForSeconds(duration);
            this.gameObject.SetActive(false);
            pendingDestroy = false;
        }
        #endregion

        #endregion
    }
}