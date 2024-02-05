using UnityEngine;

using Common.GameItems.Config;

namespace Common.Purchases.Config
{
    [CreateAssetMenu(fileName = "BuyableItemConfig_", menuName = "Game/Common/Purchases/BuyableItemConfig", order = 0)]
    public class BuyableItemConfig : ScriptableObject
    {
        #region EXPOSED_FIELDS
        [SerializeField] private GameItemConfig itemConfig = null;
        [SerializeField] private int price = 0;
        #endregion

        #region PROPERTIES
        public GameItemConfig ItemConfig { get => itemConfig; }
        public int Price { get => price; }
        #endregion
    }
}