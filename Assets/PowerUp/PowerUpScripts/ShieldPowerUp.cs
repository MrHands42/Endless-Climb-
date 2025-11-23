using UnityEngine;

public class ShieldPowerUp : PowerUp
{
    protected override void ApplyEffect(GameObject target)
    {
        Collector collector = target.GetComponent<Collector>();

        if (collector != null)
        {
            Debug.Log("Mengaktifkan Shield pada: " + target.name);
            collector.ActivateShield();
        }
    }
}