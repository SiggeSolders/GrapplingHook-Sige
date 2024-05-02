using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StamminaControler : MonoBehaviour
{
    [HideInInspector] public PlayerMovement playerMovement;
    [Header("Stammina")]
    public float playerStammina = 100.0f;
    [SerializeField] public float maxStammina = 100.0f;
    [SerializeField] private float jumpCost = 20;
    [HideInInspector] public bool hasRegen = true;
    [HideInInspector] public bool isSprinting = false;


    [Header("Stammina regen")]
    [Range(0, 50)] private float StamminaRegen = 10f;
    [Range(0, 50)] private float StamminaDrain = 12f;


    [Header("Stammina UI")]
    [SerializeField] private Image stamminaUI = null;
    [SerializeField] private CanvasGroup slidercanvasGroup;

    private PlayerMovement PlayerController;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSprinting)
        {
            if (playerStammina <= maxStammina - 0.01)
            {

                playerStammina += StamminaRegen * Time.deltaTime;
                UptdateStammina(1);
                if (playerStammina > 10)
                {
                    playerMovement.runCooldown = false;
                }
            }

            if (playerStammina >= maxStammina)
            {
                playerMovement.setRunsSpeed(playerMovement.walkspeed);
                slidercanvasGroup.alpha = 0;
                hasRegen = true;
            }
        }
    }

    public void StamminaJump()
    {
        if (playerStammina >= jumpCost)
        {
            playerStammina -= jumpCost;
            playerMovement.PlayerJump();
            UptdateStammina(1);
        }
    }

    public void IsSprinting()
    {
        if (true)
        {
            isSprinting = true;
            playerStammina -= StamminaDrain * Time.deltaTime;
            UptdateStammina(1);
            if (playerStammina <= 0)
            {
                hasRegen = false;
                playerMovement.setRunsSpeed(playerMovement.walkspeed);
                playerMovement.state = PlayerMovement.MovementState.walking;
                playerMovement.runCooldown = true;
                slidercanvasGroup.alpha = 0;
            }
        }
    }
    void UptdateStammina(int Value)
    {
        if (Value == 0)
        {
            slidercanvasGroup.alpha = 0;
        }
        else
        {
            slidercanvasGroup.alpha = 1;
        }
    }
}

