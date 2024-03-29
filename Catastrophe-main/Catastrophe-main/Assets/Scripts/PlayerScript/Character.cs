using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp = 1000;
    public int currentHP = 1000;

    public int armor = 0;

    public float hpRegenerationRate = 1f;
    public float hpRegenerationTimer;

    public float damageBonus;
    [SerializeField] StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private bool isDead;
    [SerializeField] DataContainer dataContainer;

    PauseManager pauseManager;
    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    private void Start()
    {
        ApplyPersistantUpgrades();
        hpBar.SetState(currentHP, maxHp);
    }

    private void ApplyPersistantUpgrades()
    {
        int hpUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.HP);

        maxHp += maxHp/10 * hpUpgradeLevel;
        // currentHP += maxHp;

        int damageUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.Damage);
        damageBonus = 1f + 0.1f * damageUpgradeLevel;
    }

    private void Update()
    {
        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;

        if (hpRegenerationTimer >= 1f)
        {
            Heal(1);
            hpRegenerationTimer -= 1f;
        }
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        ApplyArmor(ref damage);

        currentHP -= damage;
        if (currentHP <= 0)
        {
            GetComponent<CharacterGameOver>().GameOver();
            isDead = true;
            pauseManager.PauseGame();

        }
        hpBar.SetState(currentHP, maxHp);
    }

    private void ApplyArmor(ref int damage)
    {
        if (armor > 0)
        {
            damage -= armor;
            if (damage < 0)
            {
                damage = 0;
            }
        }
    }

    public void Heal(int amount)
    {
        if (currentHP <= 0) { return; }

        currentHP += amount;
        if (currentHP > maxHp)
        { currentHP = maxHp; }
        hpBar.SetState(currentHP, maxHp);
    }
}
