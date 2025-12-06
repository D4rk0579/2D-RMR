using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public static int damage;
    public static int DamageCost;
    public static int money;
    public Text damageCost;
    public Text moneyText;
    public Text damageValueText;

    private void Start()
    {
        if (damage < 10)
        {
            damage = 10;
        }
        if (DamageCost <= 0)
        {
            DamageCost = 1; // assign defaults, mainly just bc I'm paranoid it'll break under certain conditions
        }
        Debug.Log("money: " + money);
        damageCost.text = "Cost: " + DamageCost;
        moneyText.text = "Money: " + money;
        damageValueText.text = "Damage: " + damage; // set the cost/stats texts
    }

    public void UpgradeDamage()
    {
        if (money >= DamageCost)
        {
            damage += 10;
            money -= DamageCost;
            DamageCost *= 2;
            Debug.Log("Damage upgraded! New damage: " + damage);
            KeepData.damageValue = damage;
            damageCost.text = "Cost: " + DamageCost;
            moneyText.text = "Money: " + money;
            damageValueText.text = "Damage: " + damage; // if you got enough money, upgrade damage
        }
        else
        {
            Debug.Log("Not enough money for damage. Current money: " + money + "Cost:" + DamageCost); // poor
        }
    }
}


