using UnityEngine;

public class TimeSlowPowerUp : PowerUp
{
    [Header("Time Slow")]
    [Tooltip("Kecepatan saat slow motion")]
    public float slowFactor = 0.5f;

    [Tooltip("Durasi kembali ke normal")]
    public float duration = 5f;

    protected override void ApplyEffect(GameObject target)
    {
        if (TimeManager.instance != null)
        {
            TimeManager.instance.ActivateSlowMotion(slowFactor, duration);
        }
    }
}