using UnityEngine;
using System.Collections;

// アニメーションでひとつのオブジェクトを動かすためのクラス
// how to use
// Instantiate してオブジェクトを生成
// move(目標位置, 到着するまでの時間) で目標位置まで移動
// blink(時間) で指定した時間点滅
// setColorWith(色) で色を指定
// setColor(色, 時間) で徐々に色が変わっていくかもしれない
// setAlpha(alpha 値) で透明度を変化させる
public class AMElement : MonoBehaviour
{
    // オブジェクトを動かす周期
    public float interval;
    // 進捗管理するための変数
    public int progress;
    // move が呼ばれるたびに動くベクトル
    private Vector3 dr;
    // setColor が呼ばれるたびに動く Color
    private Color dc;
    // 点滅時間を管理するための変数
    private float blinkElapsed;
    private Renderer renderer;

    void Awake()
    {
        interval = 0.01f;
        progress = 0;
        renderer = GetComponent<Renderer>();
        renderer.material.SetFloat("_Mode", 3);
        renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        renderer.material.SetInt("_ZWrite", 0);
        renderer.material.DisableKeyword("_ALPHATEST_ON");
        renderer.material.DisableKeyword("_ALPHABLEND_ON");
        renderer.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        renderer.material.renderQueue = 3000;
    }

    // to に向かって今の位置から t 秒で進む
    // 速度調整するときは t を調整すれば良い
    // 単体で動かす場合はこちらを使う
    // AMGroup に動くのを任せる場合は move を使う
    public IEnumerator move(Vector3 to, float t)
    {
        if (t == 0)
        {
            transform.position = to;
            yield break;
        }
        // 動きが終わるまでは while から抜けないこと
        while (true)
        {
            // 初めて move が呼ばれたときは, 毎周期進む距離を dr で求めておく
            if (progress == 0)
            {
                dr = (to - transform.position) / t;
                dr *= interval;
                progress = -1;
            }

            Vector3 old = transform.position;
            transform.position = transform.position + dr;
            // 目的地にいけてるなら終了フラグを立てる
            if (Vector3.Dot(to - old, to - transform.position) <= 0)
            {
                progress = 1;
                transform.position = to; // 行き過ぎを修正 (cexen)
                yield break;
            }
            yield return new WaitForSeconds(interval);
        }
    }
    // オブジェクトを t 秒の間点滅させる
    public IEnumerator blink(float t)
    {
        while (true)
        {
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
            blinkElapsed += 10 * interval;
            if (blinkElapsed > t)
            {
                blinkElapsed = 0;
                GetComponent<Renderer>().enabled = true;
                yield break;
            }
            else
                yield return new WaitForSeconds(10 * interval);
        }
    }
    public void blinkWith(float t)
    {
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
        blinkElapsed += 10 * interval;
        if (blinkElapsed > t)
        {
            blinkElapsed = 0;
            GetComponent<Renderer>().enabled = true;
            progress = 1;
        }
    }
    // to に向かって今の位置から t 秒で進む
    // 速度調整するときは t を調整すれば良い
    public void moveWith(Vector3 to, float t)
    {
        if (t == 0)
        {
            progress = 1;
            transform.position = to;
            return;
        }
        // 初めて move が呼ばれたときは, 毎周期進む距離を dr で求めておく
        if (progress == 0)
        {
            dr = (to - transform.position) / t;
            dr *= interval;
            progress = -1;
        }

        Vector3 old = transform.position;
        transform.position = transform.position + dr;
        // 目的地にいけてるなら終了フラグを立てる
        if (Vector3.Dot(to - old, to - transform.position) <= 0)
        {
            progress = 1;
            transform.position = to; // 行き過ぎを修正 (cexen)
        }
    }
    public IEnumerator setColor(Color color, float t)
    {
        if (t == 0)
        {
            setColorWith(color);
            yield break;
        }
        if (progress == 0)
        {
            dc = color - GetComponent<Renderer>().material.color;
            dc *= interval / t;
        }
        GetComponent<Renderer>().material.color += dc;
        // 十分近い色になってたら終了フラグを立てる
        Color diff = GetComponent<Renderer>().material.color - color;
        float d = color.r + color.g + color.b;
        if (d < 0.001)
        {
            progress = 1;
        }
    }
    public void setAlpha(float alpha)
    {
        var color = renderer.material.color;
        color.a = alpha;
        renderer.material.SetColor("_Color", color);
    }
    public void setColorWith(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
