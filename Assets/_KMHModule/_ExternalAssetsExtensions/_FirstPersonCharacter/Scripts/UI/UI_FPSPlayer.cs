using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_FPSPlayer : MonoBehaviour
{
    protected bool _isEquipable;
    public bool IsEquipable
    {
        get
        {
            return _isEquipable;
        }
        set
        {
            _isEquipable = value;

            equipablePanel.SetActive(value);
        }
    }

    [SerializeField]
    protected GameObject equipablePanel;
    [SerializeField]
    protected TMP_Text currentAmmoText; 

    protected void Awake()
    {
        equipablePanel.SetActive(false);
    }

    public void SetAmmoText(int exceptMagCount, int currentMagCount)
    {
        currentAmmoText.text = exceptMagCount + " / " + currentMagCount;
    }
}
