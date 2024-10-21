using System;
using UnityEngine;
using System.Diagnostics;
public class HealthSystem
{
    public int health;
    public string healthStatus;
    public int shield;
    public int lives;
    public int xp;
    public int level;

    public HealthSystem()
    {
        health = 100;
        shield = 100;
        lives = 3;
        xp = 0;
        level = 1;
    }

    public string ShowHUD()
    {
        return $"HP: {health}  Shield: {shield}  Lives: {lives} \nStatus: {healthStatus} EXP: {xp}  Level: {level}";
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0) return; 

        if (shield > 0)
        {
            int shieldDamage = Math.Min(damage, shield);
            shield -= shieldDamage;
            damage -= shieldDamage; // Remaining damage to health
        }

        if (damage > 0)
        {
            health -= damage;
            if (health < 0) health = 0; 
        }

        if (health <= 0)
        {
            Revive(); 
        }

        UpdateHealthStatus();
    }


    public void Heal(int hp)
    {
        // Check for negative healing input
        if (hp < 0)
        {
            return; // Ignore negative healing
        }

        // Ensure health does not exceed 100
        if (health > 100)
        {
            health = 100; // Cap health at 100
        }
        if (health < 100)
        {
            health += hp;
        }

        // Update health status after healing
        UpdateHealthStatus();
    }

    public void RegenerateShield(int hp)
    {
        if (hp < 0)
        {
            return; // Ignore negative regeneration
        }

        if (shield < 100)
        {
            shield += hp;
        }
        if (shield > 100)
        {
            shield = 100;
        }
    }

    public void Revive()
    {
        if (lives > 0)
        {
            health = 100; 
            shield = 100; 
            lives--;      
            UpdateHealthStatus();
        }
        else if (health == 0 && lives <= 0)
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        Console.WriteLine("Startover");
        health = 100;
        shield = 100;
        lives = 3;
        UpdateHealthStatus();
    }

    private void UpdateHealthStatus()
    {
        if (health <= 10)
        {
            healthStatus = "You are badly hurt!";
        }
        else if (health <= 50)
        {
            healthStatus = "Half health";
        }
        else if (health <= 75)
        {
            healthStatus = "You are hurt";
        }
        else if (health <= 90)
        {
            healthStatus = "Damage taken";
        }
        else
        {
            healthStatus = "Full Health";
        }
    }

    // Optional XP system methods
    public void IncreaseXP(int exp)
    {
        xp += exp; 
        while (xp >= 100) 
        {
            if (level < 99) 
            {
                level++; 
                xp -= 100; 
            }
            else
            {
                xp = 100; 
                break; 
            }
        }
    }

    public static void RunAllUnitTests()
    {
        TestForNegativeNumbers();
        TestTakeDamage_ShieldOnly();
        TestTakeDamage_ShieldAndHealth();
        TestTakeDamage_HealthOnly();
        TestTakeDamage_HealthToZero();
        TestTakeDamage_ShieldAndHealthToZero();
        TestTakeDamage_NegativeDamage();

        TestHeal_NormalHealing();
        TestHeal_AtMaxHealth();
        TestHeal_NegativeHealing();

        TestRegenerateShield_Normal();
        TestRegenerateShield_AtMax();
        TestRegenerateShield_Negative();

        TestRevive();
        TestResetGame();

        TestIncreaseXP_Normal();
        TestIncreaseXP_LevelUpTo99();
    }
    public static void TestForNegativeNumbers()
    {
        var healthSystem = new HealthSystem();
        healthSystem.TakeDamage(-10);

        UnityEngine.Debug.Assert(healthSystem.health == 100);
    }

    public static void TestTakeDamage_ShieldOnly()
    {
        var healthSystem = new HealthSystem();
        healthSystem.TakeDamage(30);
        UnityEngine.Debug.Assert(healthSystem.shield == 70);
        UnityEngine.Debug.Assert(healthSystem.health == 100);
    }

    public static void TestTakeDamage_ShieldAndHealth()
    {
        var healthSystem = new HealthSystem();
        healthSystem.TakeDamage(130); // Expecting shield to be 0 and health to be 70
        UnityEngine.Debug.Assert(healthSystem.shield == 0, $"Expected 0 shield, got {healthSystem.shield}");
        UnityEngine.Debug.Assert(healthSystem.health == 70, $"Expected 70 health, got {healthSystem.health}");
    }

    public static void TestTakeDamage_HealthOnly()
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 0; // Set shield to 0
        healthSystem.TakeDamage(50); // Expecting health to be 50
        UnityEngine.Debug.Assert(healthSystem.health == 50, $"Expected 50 health, got {healthSystem.health}");
    }

    public static void TestTakeDamage_HealthToZero()
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 0; // Set shield to 0
        healthSystem.TakeDamage(200); // Expecting health to be 0
        UnityEngine.Debug.Assert(healthSystem.health == 0, $"Expected 0 health, got {healthSystem.health}");
        UnityEngine.Debug.Assert(healthSystem.lives == 2, $"Expected 2 lives, got {healthSystem.lives}");
    }

    public static void TestTakeDamage_ShieldAndHealthToZero()
    {
        var healthSystem = new HealthSystem();
        healthSystem.TakeDamage(250);
        UnityEngine.Debug.Assert(healthSystem.health == 0);
        UnityEngine.Debug.Assert(healthSystem.shield == 0);
    }

    public static void TestTakeDamage_NegativeDamage()
    {
        var healthSystem = new HealthSystem();
        healthSystem.TakeDamage(-10);
        UnityEngine.Debug.Assert(healthSystem.health == 100);
        UnityEngine.Debug.Assert(healthSystem.shield == 100);
    }

    public static void TestHeal_NormalHealing()
    {
        var healthSystem = new HealthSystem();
        healthSystem.TakeDamage(30);
        healthSystem.Heal(20);
        UnityEngine.Debug.Assert(healthSystem.health == 90);
    }

    public static void TestHeal_AtMaxHealth()
    {
        var healthSystem = new HealthSystem();
        healthSystem.Heal(20);
        UnityEngine.Debug.Assert(healthSystem.health == 100);
    }

    public static void TestHeal_NegativeHealing()
    {
        var healthSystem = new HealthSystem();
        healthSystem.Heal(-10);
        UnityEngine.Debug.Assert(healthSystem.health == 100);
    }

    public static void TestRegenerateShield_Normal()
    {
        var healthSystem = new HealthSystem();
        healthSystem.RegenerateShield(20);
        UnityEngine.Debug.Assert(healthSystem.shield == 100); // Should remain at max
    }

    public static void TestRegenerateShield_AtMax()
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 50;
        healthSystem.RegenerateShield(100);
        UnityEngine.Debug.Assert(healthSystem.shield == 100);
    }

    public static void TestRegenerateShield_Negative()
    {
        var healthSystem = new HealthSystem();
        healthSystem.RegenerateShield(-10);
        UnityEngine.Debug.Assert(healthSystem.shield == 100);
    }

    public static void TestRevive()
    {
        var healthSystem = new HealthSystem();
        healthSystem.TakeDamage(300); // Should reduce health to 0 and call Revive
        UnityEngine.Debug.Assert(healthSystem.health == 100);
        UnityEngine.Debug.Assert(healthSystem.shield == 100);
        UnityEngine.Debug.Assert(healthSystem.lives == 2); // Lives should decrease by 1
    }

    public static void TestResetGame()
    {
        var healthSystem = new HealthSystem();
        healthSystem.TakeDamage(300); // Trigger a reset
        healthSystem.ResetGame();
        UnityEngine.Debug.Assert(healthSystem.health == 100);
        UnityEngine.Debug.Assert(healthSystem.shield == 100);
        UnityEngine.Debug.Assert(healthSystem.lives == 3);
    }

    public static void TestIncreaseXP_Normal()
    {
        var healthSystem = new HealthSystem();
        healthSystem.IncreaseXP(50);
        UnityEngine.Debug.Assert(healthSystem.xp == 50);
    }

    public static void TestIncreaseXP_LevelUpTo99()
    {
        var healthSystem = new HealthSystem();
        healthSystem.level = 98;
        healthSystem.IncreaseXP(200);
        UnityEngine.Debug.Assert(healthSystem.level == 99);
        UnityEngine.Debug.Assert(healthSystem.xp == 0);
    }

}