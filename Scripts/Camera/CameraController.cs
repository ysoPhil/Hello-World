namespace CardGameUIEffect
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// The CameraController class.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        #region Fields
        [Tooltip("Camera offset in y-axis under different aspects")]
        public List<float> cameraYOffset = new List<float>();

        [Tooltip("Aspects to fit")]
        public List<float> cameraAspectRef = new List<float>();

        /// <summary>
        /// The eps.
        /// </summary>
        private const float EPS = 0.05f;                 // Abs(A-B)<=EPS <=> A=B

        /// <summary>
        /// The camera position in z axis.
        /// </summary>
        private const float CAMERA_POS_Z = -30.0f;       // Camera.position.z
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
            // Calcultes the new orthographicSize under aspect ratio 16:9
            var realAspect = this.GetComponent<Camera>().aspect;
            var newSize = this.GetComponent<Camera>().orthographicSize / this.GetComponent<Camera>().aspect * (16.0f / 9.0f);
            this.GetComponent<Camera>().orthographicSize = newSize;
            // Rises up the camera to hide the card bottom in hand
            for (int i = 0; i < cameraAspectRef.Count; i++)
            {
                if (Mathf.Abs(realAspect - cameraAspectRef[i]) <= EPS)
                {
                    if (cameraYOffset[i] < 0)
                    {
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + cameraYOffset[i], CAMERA_POS_Z);
                    }
                    break;
                }
            }
        }
        #endregion

        #endregion
    }
}