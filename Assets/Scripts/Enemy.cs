using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Enemy")]
public class Enemy : MonoBehaviour
{
    public Transform m_explosionFX;

    public float m_speed = 1;
    public float m_life = 2;
    protected float m_rotSpeed = 30;

    internal Renderer m_renderer;
    internal bool m_isActiv = false;
    public float m_point = 10;

   


    // Start is called before the first frame update
    void Start()
    {
        m_renderer = this.GetComponent<Renderer>();
    }
    void OnBecameVisible()
    {
        m_isActiv = true;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateMove();   
        if(m_isActiv && !this.m_renderer.isVisible)
        {
            Destroy(this.gameObject);
        }
    }
    protected virtual void UpdateMove()
    {
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;

        this.transform.Translate(new Vector3(rx, 0, m_speed * Time.deltaTime));
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerRocket")
        {
            Rocket rocket = other.GetComponent<Rocket>();
            if(rocket != null )
            {
                m_life -= rocket.m_power;
                if(m_life <= 0)
                {
                    GameManager.Instance.AddScore(m_point);
                    Instantiate(m_explosionFX, this.transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        }
        else if(other.tag == "Player")
        {
            m_life = 0;
            Destroy(this.gameObject);
        }
    }
}
