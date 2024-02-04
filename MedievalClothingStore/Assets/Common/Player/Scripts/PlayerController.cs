using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Common.Player.Movement;

public class PlayerController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [Header("Comp Assigment")]
    [SerializeField] private PlayerMovement playerMovement = null;
    #endregion

    #region UNITY_CALLS
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        playerMovement.HandleAxisInput();
    }

    private void FixedUpdate()
    {
        playerMovement.HandleAxisMovement();
    }
    #endregion
}
