using UnityEngine;
using System.Collections;

public class UVScroller : MonoBehaviour {
    public Vector2 m_ScrollSpeed;

    Material m_Material;

	void Start () 
    {
        m_Material = GetComponent<Renderer>().material;
	}
	

	void Update () {
        m_Material.SetTextureOffset("_MainTex", m_ScrollSpeed * Time.time);
	}

    public void FadeOut()
    {
        print("FadeOut");
        // SetValue()を毎フレーム呼び出して、１秒間に１から０までの値の中間値を渡す
        iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 2f, "onupdate", "SetValue"));
        
    }
    void SetValue(float alpha)
    {
        // iTweenで呼ばれたら、受け取った値をImageのアルファ値にセット
         gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, alpha);
    }
}
