using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApereceBoss : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private GameObject boss;  

    public BoxCollider2D m_Collider;

    private PlayerStats Ps;

    void Start()
    {
        m_Collider = GetComponent<BoxCollider2D>();
        Ps = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (GameObject.Find("Boss(Clone)") != null)
        {
            Destroy(GameObject.Find("Boss(Clone)"));
        }

        Instantiate(boss, respawnPoint);
        m_Collider.enabled = !m_Collider.enabled;
        Ps.BossActivado = true;

    }

    public void RestartBoss()
    {
        m_Collider.enabled = !m_Collider.enabled;
    }
}
