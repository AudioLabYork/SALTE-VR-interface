using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour
{

    public Pointer m_Pointer;
    public SpriteRenderer m_circleRenderer;

    public Sprite m_OpenSprite;
    public Sprite m_ClosedSprite;

    private Camera m_camera = null;
    // Start is called before the first frame update


    private void Awake()
    {
        m_Pointer.OnPointerUpdate += UpdateSprite;
        m_camera = Camera.main;
    }

    private void OnDestroy()
    {
        m_Pointer.OnPointerUpdate -= UpdateSprite;
    }

    private void UpdateSprite(Vector3 point, GameObject hitObject)
    {

        transform.position = point;
        if (hitObject)
        {
            m_circleRenderer.sprite = m_ClosedSprite;
        }
        else
            m_circleRenderer.sprite = m_OpenSprite;

    }

    private void Update()
    {
        transform.LookAt(m_camera.gameObject.transform);
    }
}
