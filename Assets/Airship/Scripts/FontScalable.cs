using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FontScalable : MonoBehaviour
{
    public float fontScale = 0.5f;
    private TextMesh textMesh;
    
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }

    void Update()
    {
        Vector3 defaultScale = new Vector3(1, 1, 1) * fontScale;
        int fontSize = textMesh.fontSize;
        fontSize = fontSize == 0 ? 12 : fontSize;

        float scale = 0.1f * 128 / fontSize;
        transform.localScale = defaultScale * scale;
    }
}