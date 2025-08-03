using System;

public class HpPickup : IPickupable
{
    private int _healthAmount;

    private const string MESSAGE = "HP already full";

    public event Action<string> OnMessageShown;

    public HpPickup(int amount)
    {
        _healthAmount = amount;
    }

    public void ApplyEffect(Player player)
    {
        if (CanPickUp(player))
        {
            player.RestoreHealth(_healthAmount);
            AudioManager.Instance.PlaySound(SoundType.HealPickup);
        }
        else
        {
            OnMessageShown?.Invoke(MESSAGE);
        }
    }

    public bool CanPickUp(Player player)
    {
        return player.CurrentHealth() < 3;
    }
}
