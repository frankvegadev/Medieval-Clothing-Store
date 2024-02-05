using UnityEngine;

using Common.GameItems.Config;

namespace Common.GameItems.Instance
{
    public class GameItemInstanceModel
    {
        #region EXPOSED_FIELDS
        [SerializeField] private string instanceID = string.Empty;
        [SerializeField] private GameItemConfig itemConfigAttached = null;
        #endregion

        #region PROTECTED_FIELDS
        protected static int instances = 0;
        #endregion

        #region PROPERTIES
        public string InstanceID { get => instanceID; }
        public GameItemConfig ItemConfigAttached { get => itemConfigAttached; }
        #endregion

        #region PUBLIC_METHODS
        public GameItemInstanceModel(GameItemConfig itemConfig)
        {
            this.itemConfigAttached = itemConfig;

            instanceID = itemConfigAttached.ItemID + instances;
            instances++;
        }
        #endregion
    }
}