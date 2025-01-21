using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private int _level;
    private float _maxHp;
    private float _xpToLvlUp;
    
    public float currentHp;
    public float currentXp;

    public bool canMorph;

    //public Weapon weapon;
    public Transform firepoint;
    void Awake()
    {
        _maxHp = 50;
        currentHp = _maxHp;
        
        _xpToLvlUp = 50;
        canMorph = false;
    }

    private void OnEnable()
    {
        InputManager.onMorph += Morph;
        InputManager.onShoot += Shoot;
    }

    private void OnDisable()
    {
        InputManager.onMorph -= Morph;
        InputManager.onShoot -= Shoot;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
        //     Enemy enemy = other.GetComponent<Enemy>();
        //     if (enemy.isDead)
        //     {
        //          canMorph = true;
        //     }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
        //     Enemy enemy = other.GetComponent<Enemy>();
        //     if (enemy.isDead)
        //     {
        //          canMorph = false;
        //     }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            //Die
        }
    }

    public void GainXp(float xpValue)
    {
        currentXp += xpValue;
        if (currentXp >= _xpToLvlUp)
        {
            currentXp -= _xpToLvlUp;
            //Level up
            //Increase xp to level up
        }
    }

    public void Morph()
    {
        if (canMorph)
        {
            canMorph = false;
            //Change Form
            //Remove enemy
        }
    }
    
    private void Shoot()
    {
        // if (weapon)
        // {
        //     
        // }
    }

}
