using System;

using UnityEngine;

using Common.GameItems.Config;

using Common.NPC.Clothes;

namespace Common.Player.Controller.Inventory
{
    public class PlayerInventoryHandler : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private NPCClothesModel defaultPlayerClothes = null;
        #endregion

        #region PRIVATE_FIELDS
        private NPCClothesModel currentPlayerClothes = null;
        #endregion

        #region ACTIONS
        private Action<GameItemConfig> onUpdatePlayerClothes = null;
        #endregion

        #region PROPERTIES
        public NPCClothesModel CurrentPlayerClothes { get => currentPlayerClothes; }
        #endregion

        #region UNITY_CALLS
        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region PUBLIC_METHODS
        public void Configure(Action<GameItemConfig> onUpdatePlayerClothes)
        {
            this.onUpdatePlayerClothes = onUpdatePlayerClothes;

            currentPlayerClothes = new NPCClothesModel();

            SetPlayerClothingPart(defaultPlayerClothes.HairConfig);
            SetPlayerClothingPart(defaultPlayerClothes.TorsoConfig);
            SetPlayerClothingPart(defaultPlayerClothes.LegsConfig);
            SetPlayerClothingPart(defaultPlayerClothes.FeetConfig);
        }

        public void SetPlayerClothingPart(GameItemConfig partConfig)
        {
            currentPlayerClothes.SetClothingPart(partConfig);

            onUpdatePlayerClothes.Invoke(partConfig);
        }
        #endregion

        #region PRIVATE_METHODS
        #endregion
    }
}