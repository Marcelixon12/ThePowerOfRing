using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public CharacterMovement cm;

    // Start is called before the first frame update
    public void SetActive()
    {
        cm.store.SetActive(true);
    }
    public void NoSetActive()
    {
        cm.store.SetActive(false);
    }
    public void BuyStoneBow()
    {
        if (cm.gold >= cm.stoneBowPrice)
        {
            cm.gold -= cm.stoneBowPrice;
            cm.woodBow.SetActive(false);
            cm.stoneBow.SetActive(true);
            cm.ironBow.SetActive(false);
            cm.goldBow.SetActive(false);
            cm.diamondBow.SetActive(false);
            CharacterMovement.currentDamage = cm.stoneDamage;
        }
    }
    public void BuyIronBow()
    {
        if (cm.gold >= cm.ironBowPrice)
        {
            cm.gold -= cm.ironBowPrice;
            cm.woodBow.SetActive(false);
            cm.stoneBow.SetActive(false);
            cm.ironBow.SetActive(true);
            cm.goldBow.SetActive(false);
            cm.diamondBow.SetActive(false);
            CharacterMovement.currentDamage = cm.ironDamage;
        }
    }
    public void BuyGoldBow()
    {
        if (cm.gold >= cm.goldBowPrice)
        {
            cm.gold -= cm.goldBowPrice;
            cm.woodBow.SetActive(false);
            cm.stoneBow.SetActive(false);
            cm.ironBow.SetActive(false);
            cm.goldBow.SetActive(true);
            cm.diamondBow.SetActive(false);
            CharacterMovement.currentDamage = cm.goldDamage;
        }
    }
    public void BuyDiamondBow()
    {
        if (cm.gold >= cm.diamondBowPrice)
        {
            cm.gold -= cm.diamondBowPrice;
            cm.woodBow.SetActive(false);
            cm.stoneBow.SetActive(false);
            cm.ironBow.SetActive(false);
            cm.goldBow.SetActive(false);
            cm.diamondBow.SetActive(true);
            CharacterMovement.currentDamage = cm.diamondDamage;
        }
    }
}
