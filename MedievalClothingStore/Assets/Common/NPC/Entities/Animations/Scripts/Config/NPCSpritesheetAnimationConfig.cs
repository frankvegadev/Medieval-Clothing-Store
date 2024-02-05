using UnityEngine;

namespace Common.NPC.Animations.Config
{
    [CreateAssetMenu(fileName = "NPCAnimationConfig_", menuName = "Game/Common/NPCAnimationConfig", order = 1)]
    public class NPCSpritesheetAnimationConfig : ScriptableObject
    {
        #region EXPOSED_FIELDS
        [Header("Stand Animations")]
        [SerializeField] private NPCSpritesheetAnimationFrameConfig standRight = null;
        [SerializeField] private NPCSpritesheetAnimationFrameConfig standUp = null;
        [SerializeField] private NPCSpritesheetAnimationFrameConfig standDown = null;

        [Header("Walk Animations")]
        [SerializeField] private NPCSpritesheetAnimationFrameConfig[] walkRight = null;
        [SerializeField] private NPCSpritesheetAnimationFrameConfig[] walkUp = null;
        [SerializeField] private NPCSpritesheetAnimationFrameConfig[] walkDown = null;
        #endregion

        #region PROPERTIES
        public NPCSpritesheetAnimationFrameConfig StandRight { get => standRight; }
        public NPCSpritesheetAnimationFrameConfig StandUp { get => standUp; }
        public NPCSpritesheetAnimationFrameConfig StandDown { get => standDown; }
        public NPCSpritesheetAnimationFrameConfig[] WalkRight { get => walkRight; }
        public NPCSpritesheetAnimationFrameConfig[] WalkUp { get => walkUp; }
        public NPCSpritesheetAnimationFrameConfig[] WalkDown { get => walkDown; }
        #endregion
    }
}