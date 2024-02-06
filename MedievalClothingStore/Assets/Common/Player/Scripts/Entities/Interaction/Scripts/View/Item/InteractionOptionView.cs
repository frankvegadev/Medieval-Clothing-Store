using System;

using UnityEngine;
using UnityEngine.UI;

namespace Common.Player.Interaction.View.Item
{
    public class InteractionOptionView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Button optionBtn = null;
        #endregion

        #region PUBLIC_METHODS
        public void Configure(Action btnAction)
        {
            optionBtn.onClick.RemoveAllListeners();
            optionBtn.onClick.AddListener(() => { btnAction.Invoke(); });
        }
        #endregion
    }
}