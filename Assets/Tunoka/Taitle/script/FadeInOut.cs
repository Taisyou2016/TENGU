using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {


    [SerializeField]
    private string m_scenechange;
    [SerializeField]
    private GameObject m_Fade;
    bool m_tr =true;
    void Start()
    {
        FadeOut();
    }
    void Update()
    {
       
    }
    public void FadeIn()
    {
        print("FadeIn");
        m_tr = true;
        // SetValue()を毎フレーム呼び出して、１秒間に０から１までの値の中間値を渡す
        iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "time", 1f, "onupdate", "SetValue"));
    }
    public void FadeOut()
    {
        print("FadeOut");
        // SetValue()を毎フレーム呼び出して、１秒間に１から０までの値の中間値を渡す
        iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 1f, "onupdate", "SetValue"));
        m_tr = false;
    }
    void SetValue(float alpha)
    {
        // iTweenで呼ばれたら、受け取った値をImageのアルファ値にセット
        m_Fade.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, alpha);
        if (alpha >= 1 && m_tr) SceneChange(m_scenechange);
    }
    void SceneChange(string name)
    {
        print(name+ "にシーンチェンジ");
    }
}
