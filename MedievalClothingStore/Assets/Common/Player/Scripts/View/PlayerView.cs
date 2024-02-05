using Common.NPC.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Common.NPC.Animations.Constants.AnimationConstants;

namespace Common.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private NPCSpritesheetAnimator baseAnimator = null;
        #endregion

        #region UNITY_CALLS
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region PUBLIC_METHODS
        public void StartStopAnimation()
        {
            baseAnimator.StartStopAnimation();
        }

        public void SetAnimationState(ANIM_STATES_NPC newState)
        {
            if(newState == ANIM_STATES_NPC.WALK_LEFT)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            baseAnimator.ChangeAnimationState(newState);
        }
        #endregion

        #region PRIVATE_METHODS
        #endregion
    }
}