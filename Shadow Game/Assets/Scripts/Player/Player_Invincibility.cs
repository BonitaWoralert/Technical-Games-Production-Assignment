using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Invincibility : MonoBehaviour
{
    [SerializeField] private float invincibilityTime;
    [SerializeField] private float invincibilityEffectTime;
    [SerializeField] public float maxInvincibilityTime;
    [SerializeField] private bool isInvincible;
    [SerializeField] private AudioSource damageSound;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private Color invincibilityColorAlpha;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
        defaultColor = spriteRenderer.color;
        invincibilityColorAlpha = new Color(255, 255, 255, 0.5f);
        isInvincible = false;
        invincibilityTime = maxInvincibilityTime;
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
            damageSound.Stop();
        }
        else
        {
            //Player is Invincible.
            isInvincible = true;
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
