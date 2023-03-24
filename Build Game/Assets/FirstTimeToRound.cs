using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstTimeToRound : MonoBehaviour
{
    [SerializeField] private Text TimerTXT;
    [SerializeField]TxTRegim Regim;
    [SerializeField]private float Timer = 0;
    

    [SerializeField] AI_GUN_Long GunAI_Script;
    [SerializeField] GameObject RedCubes;
    [SerializeField] BuildSystem.ObjectRemover ObjectRemoverSCR;
    [SerializeField] BuildSystem.ObjectSelector ObjectSelectorSCR;
    [SerializeField] BuildSystem.ObjectPlacer ObjectPlacerSCR;
    [SerializeField] InteractionScript InteractionSCR;

    private void Update()
    {
        if((int)Regim == 0)
        {
            TimerTXT.enabled = false;
            if (Input.GetKey(KeyCode.G))
            {
                InteractionSCR.enabled = false;
                ObjectSelectorSCR.enabled = false;
                ObjectPlacerSCR.enabled = false;
                ObjectRemoverSCR.enabled = false;
                GunAI_Script.enabled = true;
                RedCubes.SetActive(false);
            }
        }
        if ((int)Regim == 1)
        {
            Timer -= Time.deltaTime / 60;
            TimerTXT.text = Timer.ToString("F2").Replace(",", ":");
            if (Timer <= 0)
            {
                InteractionSCR.enabled = false;
                ObjectSelectorSCR.enabled = false;
                ObjectPlacerSCR.enabled = false;
                ObjectRemoverSCR.enabled = false;
                GunAI_Script.enabled = true;
                RedCubes.SetActive(false);
                TimerTXT.enabled = false;
            }
        }
    }
}

enum TxTRegim
{
    FreeButtonDown,
    TimerDown
}
