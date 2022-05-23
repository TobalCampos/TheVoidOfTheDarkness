using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;
    private bool respawn;

    [SerializeField]
    private GameObject deadCanvas;

    [SerializeField]
    private GameObject canvasEXP;

    private CinemachineVirtualCamera CVC;
    private PlayerStats stats;
    public PuntoGuardado PuntoGuardado;

    public void Start()
    {
        CVC = GameObject.Find("PlayerCamara").GetComponent<CinemachineVirtualCamera>();
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    

    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        PuntoGuardado.ZonaRespawn();
        respawnTimeStart = Time.time;
        respawn = true;
        deadCanvas.SetActive(true);
        canvasEXP.SetActive(false);
    }

    public void CheckRespawn()
    {
        if(Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            CVC.m_Follow = playerTemp.transform;
            respawn = false;
            deadCanvas.SetActive(false);  
            canvasEXP.SetActive(true);

        }
    }
}
