using UnityEngine;
using System.Collections;

// アニメーションでいくつかのオブジェクトをまとめて動かすためのクラス
// how to use
// getFace() は顔(球オブジェクト)を返す
// move(目標位置, 時間) で目標位置まで指定した時間で移動する
// blink(点滅するオブジェクトの集合を示す配列, 時間) で指定したオブジェクトを指定した時間点滅させる
// setColor(各時間にどう動くかを示す配列) で遺伝子に色付けをする 
// getScore(スコア) でスコアが高いやつほど顔の色を濃くする
// getSegment(左端, 右端) で [l, r] 区間にある遺伝子配列を返す
public class AMGroup : MonoBehaviour {
    // 先頭にある顔オブジェクトの prefab (1種類のみ)
    private GameObject face;
    // face の prefab
    public GameObject facePrefab;
    // genePieces の prefab
    public GameObject genePrefab;
    // オブジェクトをまとめておくための配列
    private GameObject[] genes;
    // カラー配列
    private int[] colorArray;
    // genes の prefab
    public GameObject prefab;
    // 何秒周期で動かすか
    public float interval;
    // オブジェクトの個数(3000 個らへん超えるとかなり重くなる)
    public int geneSize;
    // 遺伝子をあらわすキューブの大きさ
    const int cubeSize = 10;

    // instantiate されたときに呼ばれる関数
    // 要するに初期化してくれる
    void Awake() {
        interval = AMCommon.interval;
        geneSize = 30;
        face = new GameObject();
        face = Instantiate(facePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        genes = new GameObject[geneSize];
        colorArray = new int[geneSize];
        for (int i = 0; i < geneSize; i++)
        {
            // 指定した座標にオブジェクトを作る
            // (ここでは等間隔に並べている)
            genes[i] = Instantiate(prefab, new Vector3(cubeSize*i+(cubeSize+2), 0, 0), Quaternion.identity) as GameObject;
        }
    }
    // 頭のゲームオブジェクトを返す
    public GameObject getFace()
    {
        return face;
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
            face.GetComponent<AMElement>().moveWith(to, t);
            for (int i = 0; i < geneSize; i++)
            {
                genes[i].GetComponent<AMElement>().moveWith(to + new Vector3(cubeSize*i+(cubeSize+2), 0, 0), t);
            }
            // 目的地にいけてるなら終了フラグを立てる
            if (genes [0].GetComponent<AMElement> ().progress == 1) {
                face.GetComponent<AMElement>().progress = 0;
                for (int i = 0; i < geneSize; i++)
                    genes [i].GetComponent<AMElement> ().progress = 0;
                Debug.Log ("finish!");
                yield break;
            }
            yield return new WaitForSeconds(AMCommon.getInterval(interval, ref buffer));
        }
    }
    // 配列で指定されたオブジェクトを t 秒間点滅させる
    public IEnumerator blink(int[] v, float t) {
        if (v == null)
            yield break;
        float buffer = 0f; // getIntervalに渡すやつ
        while (true) {
            for (int i = 0; i < v.Length; i++) {
                genes [v [i]].GetComponent<AMElement> ().blinkWith (t);
            }
            // 点滅が終了したら終了フラグを立てる
            if (genes [v [0]].GetComponent<AMElement> ().progress == 1) {
                for (int i = 0; i < v.Length; i++) {
                    genes [v [i]].GetComponent<AMElement> ().progress = 0;
                }
                Debug.Log ("finish!");
                yield break;
            }
            yield return new WaitForSeconds (AMCommon.getInterval(10 * interval, ref buffer));
        }
    }
    // 配列を与えられるので, genes の色を変更する
    public void setColor(int[] v)
    {
        for (int i = 0; i < v.Length; i++)
        {
            colorArray[i] = v[i];
            switch (v[i])
            {
                case -1:
                    genes[i].GetComponent<Renderer>().material.color = Color.red;
                    break;
                case 0:
                    genes[i].GetComponent<Renderer>().material.color = Color.green;
                    break;
                case 1:
                    genes[i].GetComponent<Renderer>().material.color = Color.blue;
                    break;
            }
        }
    }
    // スコアが与えられるので, それっぽい色に変更する
    public void setScore(float score)
    {
        face.GetComponent<Renderer>().material.color = new Color(min(score / 50, 1f), 0f, 0f, 1f);
    }
    public void moveWith(Vector3 to, float t)
    {
        // 各オブジェクトを目標位置に動かす
        face.GetComponent<AMElement>().moveWith(to, t);
        for (int i = 0; i < geneSize; i++)
        {
            genes[i].GetComponent<AMElement>().moveWith(to + new Vector3(cubeSize * i + (cubeSize + 2), 0, 0), t);
        }
        // 目的地にいけてるなら終了フラグを立てる
        if (genes[0].GetComponent<AMElement>().progress == 1)
        {
            face.GetComponent<AMElement>().progress = 0;
            for (int i = 0; i < geneSize; i++)
                genes[i].GetComponent<AMElement>().progress = 0;
            Debug.Log("finish!");
        }
    }
    // 要素を全消しする
    public void delete()
    {
        Destroy(face);
        for (int i = 0; i < geneSize; i++) Destroy(genes[i]);
    }
    // 区間が与えられるので, その部分の gene の集合を AMGenePieces として返す
    // [l, r] 区間にしています
    public GameObject getSegment(int l, int r)
    {
        GameObject genePieces = Instantiate(genePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        genePieces.GetComponent<AMGenePieces>().setParams(r - l + 1, genes[l].transform.position);
        int[] vs = new int[r - l + 1];
        for (int i = 0; i <= r - l; i++)
        {
            vs[i] = colorArray[i + l];
        }
        genePieces.GetComponent<AMGenePieces>().setColor(vs);
        return genePieces;
    }

    private float min(float p1, float p2)
    {
        if (p1 < p2) return p1;
        return p2;
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
