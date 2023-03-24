using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Смерть и Победа")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject CameraDeath;
    [SerializeField] private GameObject PointRespawn;
    [SerializeField] private GameObject Crystal;
    [SerializeField] private GameObject TimerDeath;
    [SerializeField] private Text TimerDeathTxT;
    [SerializeField] private float TimerDeathSec;
    [SerializeField] private GameObject PlaneDeath;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Crystal = GameObject.FindGameObjectWithTag("Crystal");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.activeSelf == false)
        {
            
            TimerDeath.SetActive(true);
            CameraDeath.SetActive(true);
            TimerDeathSec -= Time.deltaTime;
            TimerDeathTxT.text = TimerDeathSec.ToString("F2").Replace(",", ":");
            if(TimerDeathSec <= 0)
            {
                TimerDeath.SetActive(false);
                Player.transform.position = PointRespawn.transform.position;
                CameraDeath.SetActive(false);
                Player.SetActive(true);
                TimerDeathSec = 10;
            }
        }
    }
}
