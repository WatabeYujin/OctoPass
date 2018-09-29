using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PhaseResult : MonoBehaviour {
    
    [SerializeField]
    Sprite[] runkSprite;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    private Common common;
    private int nowPhase;
    // Use this for initialization
    void Start () {
        common = Common.Instance;
        RunkPopUP();
    }
	
	// Update is called once per frame
	void Update () {
        if (common.fasePaerl[nowPhase] == 0)
            spriteRenderer.sprite = runkSprite[0];
        else if (common.fasePaerl[nowPhase] == 1)
            spriteRenderer.sprite = runkSprite[1];
        else if (common.fasePaerl[nowPhase] == 2)
            spriteRenderer.sprite = runkSprite[2];
        else if (common.fasePaerl[nowPhase] >= 3)
            spriteRenderer.sprite = runkSprite[3];
    }

    public void RunkView()
    {
        const float m_moveSpeed = 0.5f;

        Vector3 m_center = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,-7);
        Vector2 m_zoomScale = new Vector2(1, 1);

        transform.DOMove(m_center, m_moveSpeed);
        //transform.DOScale(m_zoomScale, m_moveSpeed);
    }
    
    public void PhaseUp()
    {
        nowPhase++;
        
        
        RunkPopUP();
    }

    public bool RunkLimitCheck()
    {
        int m_runkLimit=10;
        if (nowPhase+1 >= m_runkLimit)
            return true;
        return false;
    }

    public void GameFinish()
    {
        transform.localScale = Vector2.zero;
        Common.Instance.ViewResult();
        Destroy(this);
    }

    void RunkPopUP()
    {
        const float m_popUpSpeed = 0.5f;
        Vector3 m_pos = Camera.main.transform.position + new Vector3(-8.5f, -4,10);
        Vector2 m_scale = new Vector2(0.3f, 0.3f);

        transform.localScale = Vector2.zero;
        transform.position = m_pos;
        transform.DOScale(m_scale, m_popUpSpeed);
    }
}
