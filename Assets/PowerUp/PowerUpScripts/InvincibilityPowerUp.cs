using UnityEngine;

public class InvincibilityPowerUp : PowerUp
{
    [Header("Invincibility Settings")]
    public float duration = 3f;

    protected override void ApplyEffect(GameObject target)
    {
        Collector collector = target.GetComponent<Collector>();

        if (collector != null)
        {
            Debug.Log("Mengaktifkan Mode Dewa pada: " + target.name);
            collector.ActivateInvincibility(duration);
        }

        else
        {
            Debug.LogError("Gagal! Objek '" + target.name + "' tidak punya script Collector.");
        }
    }
}