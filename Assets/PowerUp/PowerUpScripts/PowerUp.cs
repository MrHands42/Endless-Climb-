using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : FallingObstacle
{
    [Header("Power Up Settings")]
    public SFX soundEffect = SFX.PowerUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (AudioManager.AudioManagerInstance != null)
            {
                AudioManager.AudioManagerInstance.Play(soundEffect);
            }

            ApplyEffect(other.gameObject);

            Debug.Log("PowerUp Collected: " + gameObject.name);
            Destroy(gameObject);
        }
    }
    protected virtual void ApplyEffect(GameObject target)
    {
        // Diisi power up masing-masing
    }
}