using UnityEngine;

using TMPro;

namespace Common.Player.Currencies.View
{
    public class PlayerCurrenciesView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private TMP_Text coinsText = null;
        #endregion

        #region UNITY_CALLS
        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region PUBLIC_METHODS
        public void SetCoinsText(string newText)
        {
            coinsText.text = newText;
        }
        #endregion
    }
}