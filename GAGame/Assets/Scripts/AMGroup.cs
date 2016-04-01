using UnityEngine;
using System.Collections;

// アニメーションでいくつかのオブジェクトをまとめて動かすためのクラス
public class AMGroup : MonoBehaviour {
    // 先頭にある顔オブジェクトの prefab (1種類のみ)
    private GameObject face;
    // face の prefab
    public GameObject facePrefab;
    // genePieces の prefab
    public GameObject genePrefab;
    // オブジェクトをまとめておくための配列
    private GameObject[] genes;
    // genes の prefab
    public GameObject prefab;
    // 何秒周期で動かすか
    public float interval;
    // オブジェクトの個数(3000 個らへん超えるとかなり重くなる)
    public int geneSize;
    // 遺伝子をあらわすキューブの大きさ
    const int cubeSize = 1;

    // instantiate されたときに呼ばれる関数
    // 要するに初期化してくれる
    void Awake() {
        interval = 0.01f;
        geneSize = 1000;
        face = new GameObject();
        face = Instantiate(facePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        genes = new GameObject[geneSize];
        for (int i = 0; i < geneSize; i++)
        {
            // 指定した座標にオブジェクトを作る
            // (ここでは等間隔に並べている)
            genes[i] = Instantiate(prefab, new Vector3(cubeSize*i+(cubeSize+2), 0, 0), Quaternion.identity) as GameObject;
        }
    }

    // to に向かって今の位置から t 秒で進む
    // 速度調整するときは t を調整すれば良い
    public IEnumerator move(Vector3 to, float t)
    {
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
            yield return new WaitForSeconds(interval);
        }
    }
    // 配列で指定されたオブジェクトを t 秒間点滅させる
    public IEnumerator blink(int[] v, float t) {
        if (v == null)
            yield break;
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
            yield return new WaitForSeconds (10 * interval);
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
    // 区間が与えられるので, その部分の gene の集合を AMGenePieces として返す
    // [l, r] 区間にしています
    public GameObject getSegment(int l, int r)
    {
        GameObject genePieces = Instantiate(genePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        genePieces.GetComponent<AMGenePieces>().setParams(r-l+1, genes[l].transform.position);
        return genePieces;
    }

    private float min(float p1, float p2)
    {
        if (p1 < p2) return p1;
        return p2;
    }

    // Update is called once per frame
    void Update () {

    }
    // Use this for initialization
    void Start () {

    }
}
