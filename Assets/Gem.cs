using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IItem
{
    public void Collect()
    {
        GameManager.Instance.GameOver();
    }
}