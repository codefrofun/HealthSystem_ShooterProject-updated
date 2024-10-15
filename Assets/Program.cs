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
        int health = 100;
        shield = 100;
        lives = 3;
        ResetGame();
    }

        public string ShowHUD()
    {
        // Implement HUD display
        //display health
        //display shield
        //display lives
        return $"{health} HP, {shield} Shield, {lives} Lives, Status: {healthStatus}";
    }

    public void TakeDamage(int damage)
    {
        // Write logic to handle damage. Check if it affects the shield first, and then the health.
    }

    public void Heal(int hp)
    {
        // Restore health while ensuring it doesn't exceed the maximum.
    }

    public void RegenerateShield(int hp)
    {
        // Restore shield while ensuring it doesn't exceed the maximum.
    }

    public void Revive()
    {
        // Reset health and shield, and decrease lives by one.
    }

    public void ResetGame()
    {
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





// Implement the Optional XP System (if desired) If you choose to include this, add the xp and level variables, along with methods to increase XP and manage leveling up.