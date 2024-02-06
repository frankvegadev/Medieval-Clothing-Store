using UnityEngine;

namespace Common.NPC.Animations.Config
{
    [System.Serializable]
    public class NPCSpritesheetAnimationFrameConfig
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Sprite sprite = null;
        [SerializeField] private bool flipX = false;
        [SerializeField] private bool flipY = false;
        [SerializeField] private AudioClip audioClipToPlay = null;
        #endregion

        #region PROPERTIES
        public Sprite Sprite { get => sprite; }
        public bool FlipX { get => flipX; }
        public bool FlipY { get => flipY; }
        public AudioClip AudioClipToPlay { get => audioClipToPlay; }
        #endregion
    }
}