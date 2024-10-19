using System;
public class HealthSystem
{
    // Variables
    public int health;
    public string healthStatus;
    public int shield;
    public int lives;

    // Optional XP system variables
    public int xp;
    public int level;

    public HealthSystem()
    {
        health = 100;
        shield = 100;
        lives = 3;
    }

        public string ShowHUD()
    {
        // Implement HUD display
        return $"HP: {health}  Shield: {shield}  Lives: {lives} \n Status: {healthStatus}";
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            return; // Ignore negative damage
        }

        // Damage shield first
        if (shield > 0)
        {
            int shieldDamage = Math.Min(damage, shield);
            shield -= shieldDamage;
            damage -= shieldDamage; // Remaining damage to health
        }

        // If there's remaining damage, apply to health
        if (damage > 0)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0; // Health cannot go below 0
            }
        }

        if (health <= 0)
        {
            if (lives > 1)
            {
                Revive();
            }
            else
            {
                ResetGame();
            }
        }
        // Update health status after taking damage
        UpdateHealthStatus();
    }

    public void Heal(int hp)
    {
        // Check for negative healing input
        if (hp < 0)
        {
            return; // Ignore negative healing
        }

        // Increase health
        health += hp;

        // Ensure health does not exceed 100
        if (health > 100)
        {
            health = 100; // Cap health at 100
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

        shield += hp;

        if (shield > 100)
        {
            shield = 100; // Cap shield at 100
        }
    }

    public void Revive()
    {
        if (lives > 0)
        {
            health = 100;
            shield = 100;
            lives--;
            UpdateHealthStatus(); // Update status after revive
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
        // Implement XP increase and level-up logic
    }
}