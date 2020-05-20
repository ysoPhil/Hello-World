namespace CardGameUIEffect
{
    using UnityEngine;
    using System.Collections;

    /// <summary>
    /// The MonsterController class.
    /// </summary>
    public class MonsterController : MonoBehaviour
    {


        #region Public Methods
        /// <summary>
        /// Callback when under attack.
        /// </summary>
        /// <param name="monster">Monster instance.</param>
        /// 

        public void OnAttack(GameObject monster)
        {
            if (monster.name == this.name)
            {
                Debug.Log(string.Format("Monster {0} On Attack.", monster.name));
            }
        }

        /// <summary>
        /// Callback when add health buff.
        /// </summary>
        /// <param name="monster">Monster instance.</param>
        public void OnHeal(GameObject monster)
        {
            if (monster.name == this.name)
            {
                Debug.Log(string.Format("Monster {0} On Heal.", monster.name));
            }
        }
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



        #endregion

        #region Initialize
        /// <summary>
        /// Registers events.
        /// </summary>

        #endregion

        #endregion
    }
}