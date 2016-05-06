using UnityEngine;
using System.Collections;

// 遺伝子のいくつかの集合をまとめるクラス
// 多分交差関連でしか使わないと思います
// 交差関連での使用方法は AMTestAnimation に書いてあるから見てね
// how to use
// AMGroup のオブジェクトで getSegment(l, r) を呼ぶとそのオブジェクトの [l, r] 区間の遺伝子が Instantiate される
// pos() で先頭遺伝子の位置を返す
// delete() で遺伝子を全部消す
// setParams(遺伝子の数, 場所) で指定した遺伝子の数だけ立方体が並んだオブジェクトが指定した場所に現れる
// move(目標位置, 時間) で目標位置まで指定した時間で移動する
// blink(点滅するオブジェクトの集合を示す配列, 時間) で指定したオブジェクトを指定した時間点滅させる
// setColor(各時間にどう動くかを示す配列) で遺伝子に色付けをする 
// setAlpha(alpha 値) で透明度を設定する
public class AMGenePieces : MonoBehaviour
{
    // まとめて動かす対象のオブジェクト(1種類のみ)
    public GameObject prefab;
    // 何秒周期で動かすか
    public float interval;
    // オブジェクトをまとめておくための配列
    private GameObject[] gameObjects;
    // オブジェクトの個数(3000 個らへん超えるとかなり重くなる)
    public int geneSize;
    // 遺伝子をあらわすキューブの大きさ
    const int cubeSize = 10;

    // instantiate されたときに呼ばれる関数
    // 要するに初期化してくれる
    void Awake()
    {
        interval = AMCommon.interval;
        geneSize = 0;
        gameObjects = new GameObject[geneSize];
        for (int i = 0; i < geneSize; i++)
        {
            // 指定した座標にオブジェクトを作る
            // (ここでは等間隔に並べている)
            gameObjects[i] = Instantiate(prefab, new Vector3(cubeSize * i, 0, 0), Quaternion.identity) as GameObject;
        }
    }
    // 先頭の遺伝子がある場所を返す
    public Vector3 pos()
    {
        return gameObjects[0].transform.position;
    }
    // 要素を全消しする
    public void delete()
    {
        for (int i = 0; i < geneSize; i++) Destroy(gameObjects[i]);
    }
    // パラメーターを調整する
    // gene の長さと 場所を指定する
    public void setParams(int size, Vector3 pos)
    {
        geneSize = size;
        gameObjects = new GameObject[geneSize];
        for (int i = 0; i < geneSize; i++)
        {
            gameObjects[i] = Instantiate(prefab, pos + new Vector3(cubeSize * i, 0, 0), Quaternion.identity) as GameObject;
        }
    }

    // to に向かって今の位置から t 秒で進む
    // 速度調整するときは t を調整すれば良い
    public IEnumerator move(Vector3 to, float t)
    {
        float buffer = 0f; // getIntervalに渡すやつ
        // 動きが終わるまでは while から抜けないこと
        while (true)
        {
            // 各オブジェクトを目標位置に動かす
            for (int i = 0; i < geneSize; i++)
            {
                gameObjects[i].GetComponent<AMElement>().moveWith(to + i * (new Vector3(cubeSize, 0, 0)), t);
            }
            // 目的地にいけてるなら終了フラグを立てる
            if (gameObjects[0].GetComponent<AMElement>().progress == 1)
            {
                for (int i = 0; i < geneSize; i++)
                    gameObjects[i].GetComponent<AMElement>().progress = 0;
                Debug.Log("finish!");
                yield break;
            }
            yield return new WaitForSeconds(AMCommon.getInterval(interval, ref buffer));
        }
    }
    // 配列で指定されたオブジェクトを t 秒間点滅させる
    public IEnumerator blink(int[] v, float t)
    {
        if (v == null)
            yield break;
        float buffer = 0f; // getIntervalに渡すやつ
        while (true)
        {
            for (int i = 0; i < v.Length; i++)
            {
                gameObjects[v[i]].GetComponent<AMElement>().blinkWith(t);
            }
            // 点滅が終了したら終了フラグを立てる
            if (gameObjects[v[0]].GetComponent<AMElement>().progress == 1)
            {
                for (int i = 0; i < v.Length; i++)
                {
                    gameObjects[v[i]].GetComponent<AMElement>().progress = 0;
                }
                Debug.Log("finish!");
                yield break;
            }
            // 10 は適当
            yield return new WaitForSeconds(AMCommon.getInterval(10 * interval, ref buffer));
        }
    }
    // 配列を与えられるので, genes の色を変更する
    public void setColor(int[] v)
    {
        for (int i = 0; i < v.Length; i++)
        {
            switch (v[i])
            {
                case -1:
                    gameObjects[i].GetComponent<Renderer>().material.color = Color.red;
                    break;
                case 0:
                    gameObjects[i].GetComponent<Renderer>().material.color = Color.green;
                    break;
                case 1:
                    gameObjects[i].GetComponent<Renderer>().material.color = Color.blue;
                    break;
            }
        }
    }
    public void setAlpha(float alpha)
    {
        int sz = gameObjects.Length;
        for (int i = 0; i < sz; i++) gameObjects[i].GetComponent<AMElement>().setAlpha(alpha);
    }
    public int Length()
    {
        return gameObjects.Length;
    }
    // Update is called once per frame
    void Update()
    {

    }
    // Use this for initialization
    void Start()
    {

    }
}
