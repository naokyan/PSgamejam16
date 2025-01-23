using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    [Header("HP")]
    public int maxHp;
    [SerializeField] private int currentHp;

    [HideInInspector] public int level;
    private int _xpToLvlUp;
    private int _currentXp;
    
    [Header("Damage")]
    public int damage;

    [Header("HUD Elements")]
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider xpBar;
    [SerializeField] private TextMeshProUGUI ammoCountText;
    [SerializeField] private Image weaponImage;
    private void Start()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        currentHp = maxHp;
        hpBar.maxValue = maxHp;
        hpBar.value = currentHp;
        
        level = 1;
        _currentXp = 0;
        xpBar.maxValue = _xpToLvlUp;
        xpBar.value = _currentXp;
    }

    public void TakeDamage(int amount)
    {
        //Remove x amount of hp
        currentHp -= amount;
        
        //Update hp bar
        hpBar.value = currentHp;
        
        //Check if the player health is below or equal to 0
        if (currentHp <= 0)
        {
            //Die
        }
    }

    public void Heal(int amount)
    {
        //Add x amount of hp
        currentHp += amount;
        
        //Check if the player health is above to max health
        if (currentHp > maxHp)
            currentHp = maxHp;
        
        //Update hp bar
        hpBar.value = currentHp;
    }

    public void GainXp(int amount)
    {
        //Add x amount of xp
        _currentXp += amount;
        
        //Update xp bar
        xpBar.value = _currentXp;
        
        //Check if current amount of xp exceeds the amount of xp to level up
        if (_currentXp >= _xpToLvlUp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _currentXp -= _xpToLvlUp;
        level++;
        _xpToLvlUp += 5 * level;
        xpBar.maxValue = _xpToLvlUp;
        xpBar.value = _currentXp;
        
        //Upgrade Player
        
        maxHp += 5;
        currentHp += 5;
        hpBar.maxValue = maxHp;
        hpBar.value = currentHp;
    }
    public void SetWeaponImage(Sprite icon) { weaponImage.sprite = icon; }
}
