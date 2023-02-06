using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Invincibility : MonoBehaviour
{
    //Make sure that playerNormalCollider2D is manually referenced with normal/not-shadow-form box collider 2D.
    [SerializeField] private float invincibilityTime;
    [SerializeField] private float invincibilityEffectTime;
    [SerializeField] public float maxInvincibilityTime;
    [SerializeField] private bool isInvincible;
    [SerializeField] private AudioSource damageSound;
    private SpriteRenderer spriteRenderer;
    private GameObject playerGameObject;
    private GameObject playerInvincibilityGameObject;
    private BoxCollider2D playerInvincibleCollider2D;
    [SerializeField] private BoxCollider2D playerNormalCollider2D;
    private Color defaultColor;
    private Color invincibilityColorAlpha;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerGameObject = GameObject.Find("Player");
        playerInvincibilityGameObject = playerGameObject.transform.Find("InvincibleCollisionBox").gameObject;
        playerInvincibleCollider2D = playerInvincibilityGameObject.GetComponent<BoxCollider2D>();
        defaultColor = spriteRenderer.color;
        invincibilityColorAlpha = new Color(255, 255, 255, 0.5f);
        isInvincible = false;
        invincibilityTime = maxInvincibilityTime;
        playerInvincibleCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInvincible == true)
        {
            InvicibilityTimeUpdate();
            InvincibilityEffect();
        }
        else
        {
            spriteRenderer.color = defaultColor;
        }
    }

    private void InvicibilityTimeUpdate()
    {
        invincibilityTime -= Time.deltaTime;
        if(invincibilityTime <= 0)
        {
            //Invincibility is over.
            invincibilityTime = maxInvincibilityTime;
            isInvincible = false;
            playerNormalCollider2D.enabled = true;
            playerInvincibleCollider2D.enabled = false;
            damageSound.Stop();

        }
        else
        {
            //Player is Invincible.
            isInvincible = true;
            playerNormalCollider2D.enabled = false;
            playerInvincibleCollider2D.enabled = true;
        }
    }

    public void SetInvincibility(bool newState)
    {
        isInvincible = newState;
        damageSound.Play();
    }

    public bool GetInvincibility()
    {
        return isInvincible;
    }
    
    public void InvincibilityEffect()
    {
        invincibilityEffectTime += Time.deltaTime;

        //Player blinking effect.
        if(invincibilityEffectTime >= 1f)
        {
            invincibilityEffectTime = 0;
        }
        else if (invincibilityEffectTime >= 0.5f)
        {
            spriteRenderer.color = invincibilityColorAlpha;
        }
        else
        {
            spriteRenderer.color = defaultColor;  
        }
    }
}
