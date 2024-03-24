using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screenset : MonoBehaviour
{
    [SerializeField] Canvas testCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        int i_width = Screen.width;
        int i_height = Screen.height;
        // Canvas Scaler ������Ʈ ��������
        CanvasScaler canvasScaler = testCanvas.GetComponent<CanvasScaler>();
        // Reference Resolution �� ����
        canvasScaler.referenceResolution = new Vector2(i_width, i_height);

        //Camera.main.orthographicSize = testCanvas.GetComponent<CanvasScaler>().referenceResolution.y / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
