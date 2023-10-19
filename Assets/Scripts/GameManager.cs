using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[AddComponentMenu("MyGame/GameManager")]
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public Transform m_canvas_main;
    public Transform m_canvas_gameover;

    public TextMeshProUGUI m_text_score;
    public TextMeshProUGUI m_text_best;
    public TextMeshProUGUI m_text_life;

    protected float m_score = 0;
    public static float m_hiscore = 0;
    protected Player m_player;

    public AudioClip m_musicClip;
    protected AudioSource m_Audio;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        m_Audio = this.gameObject.AddComponent<AudioSource>();
        m_Audio.clip = m_musicClip;
        m_Audio.loop = true;
        m_Audio.Play();

        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        m_text_score = m_canvas_main.transform.Find("Text_score").GetComponent<TextMeshProUGUI>();
        m_text_best = m_canvas_main.transform.Find("Text_best").GetComponent<TextMeshProUGUI>();
        m_text_life = m_canvas_main.transform.Find("Text_life").GetComponent<TextMeshProUGUI>();

        m_text_score.text = string.Format("score {0}", (int)m_score);
        m_text_best.text = string.Format("best {0}", (int)m_hiscore);
        m_text_life.text = string.Format("life {0}", (int)m_player.m_life);

        var restart_button = m_canvas_gameover.transform.Find("Button").GetComponent<Button>();
        restart_button.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        m_canvas_gameover.gameObject.SetActive(false);
        
    }
    
    public void AddScore(float point)
    {
        m_score += point;
        if(m_hiscore < m_score)
        {
            m_hiscore = m_score;
        }
        m_text_score.text = string.Format("score {0}", (int)m_score);
        m_text_best.text = string.Format("best {0}", (int)m_hiscore);
    }
    public void ChangeLife(float life)
    {
        m_text_life.text = string.Format("life {0}", (int)life);
        if(life <= 0)
        {
            m_canvas_gameover.gameObject.SetActive(true);
        }
    }
   
   
}
