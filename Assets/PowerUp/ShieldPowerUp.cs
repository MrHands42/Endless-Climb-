using UnityEngine;

public class ShieldPowerUp : PowerUp
{
    // Shield tidak butuh durasi waktu, karena permanen sampai pecah
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