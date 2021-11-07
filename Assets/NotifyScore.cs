using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class NotifyScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_tmp;
    
    private Camera m_playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        m_playerCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(m_playerCamera.transform.position);
        transform.Rotate(0,180,0);
    }

    public void Fill(int points)
    {
        m_tmp.text = $"+{points}pts !!!";
        Random rand = new Random();
        float r = (float)rand.Next(255)/255;
        float g = (float)rand.Next(255)/255;
        float b = (float)rand.Next(255)/255;
        m_tmp.color = new Color(r, g, b);
        Destroy(this.gameObject, 2.0f);
    }
}
