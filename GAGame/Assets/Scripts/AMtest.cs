using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AMtest : MonoBehaviour {
    int[] score;
    GameObject[] scoreobject;
    public GameObject element, kao;
    GeneManager.ViewParam vp = GeneManager.viewParam; // UI表示用のグローバルパラメタの参照（短いあだ名をつけてるだけ）
    public AMElement mcamera; // cameraが予約語っぽいのでmaincameraの意

    //遺伝子の配列情報（-1,0,1を適当な色に置き換えておく）
    int[][] colorarray;

    GameObject[] geneObject;

    //親となる遺伝子の（子を作る）順番を記した配列
    int[] fatherarray,motherarray;

    GameObject father,mother,child;


    // Use this for initialization
    IEnumerator Start () {
        // input
        score = new int[50];
        for (int i = 0; i < 50; i++)
        {
            score[i] = Random.Range(0, 50);
        }
        colorarray = new int[50][];
        for (int i = 0; i < 50; i++) {
            colorarray[i] = new int[30];
            for (int j = 0; j < 30; j++)
            {
                colorarray[i][j] = Random.Range(-1, 1+1);
            }
        }
        //親の遺伝子集団の作成(並べるだけ)
        geneObject = new GameObject[50];
        for (int i = 0; i < 25; i++)
        {
            geneObject[i] = new GameObject();
            geneObject[i] = Instantiate(element, new Vector3 (0, 0, i*15), Quaternion.identity) as GameObject;
            StartCoroutine(geneObject[i].GetComponent<AMGroup>().move(new Vector3(0, 0, i*15), 0f));
            geneObject[i].GetComponent<AMGroup>().setColor(colorarray[i]);
            geneObject[i].GetComponent<AMGroup> ().setScore (score[i]);
        }
        for (int i=25; i<50; i++){
            geneObject[i] = new GameObject();
            geneObject[i] = Instantiate(element, new Vector3 (320, 0, (i-25)*15), Quaternion.identity) as GameObject;
            StartCoroutine(geneObject[i].GetComponent<AMGroup>().move(new Vector3(320, 0, (i-25)*15), 0f));
            geneObject[i].GetComponent<AMGroup> ().setScore (score[i]);
            geneObject[i].GetComponent<AMGroup> ().setColor (colorarray [i]);
        }
        fatherarray = new int[50];
        motherarray = new int[50];
        for (int i = 0; i < 50; i++) motherarray[i] = Random.Range(0, 50);
        for (int i = 0; i < 50; i++) fatherarray[i] = Random.Range(0, 50);
        //交叉の開始
        yield return StartCoroutine ("cross");
        // カメラ移動テスト
        yield return StartCoroutine(mcamera.move(mcamera.transform.position + new Vector3(0, 0, -500), 3f/vp.playSpeed));
        yield return new WaitForSeconds(3f/vp.playSpeed); // ちょっと待ってからSCに遷移
        SceneManager.LoadScene("SakeruCheese");
    }
    private IEnumerator cross ()
    {
        //親の移動
        for (int i = 0; i < 50; i++)
        {
            if (fatherarray[i] == motherarray[i]) continue;
            father = geneObject[fatherarray[i]];
            mother = geneObject[motherarray[i]];
            yield return StartCoroutine(father.GetComponent<AMGroup>().move(new Vector3(0, 0, -100), 1f/vp.playSpeed));
            yield return StartCoroutine(mother.GetComponent<AMGroup>().move(new Vector3(0, 0, -70), 1f/vp.playSpeed));
            if (GeneManager.param.crossingMode == 0)
            {
                //交叉ポイント
                int r = Random.Range(1, 29);
                GameObject go1 = father.GetComponent<AMGroup>().getSegment(0, r);
                GameObject go2 = mother.GetComponent<AMGroup>().getSegment(r + 1, 30 - 1);
                GameObject face = Instantiate(kao, new Vector3(0, 0, -85), Quaternion.identity) as GameObject;
                yield return StartCoroutine(go1.GetComponent<AMGenePieces>().move(new Vector3(12, 0, -85), 1f/vp.playSpeed));
                yield return StartCoroutine(go2.GetComponent<AMGenePieces>().move(new Vector3(12 + 7 + r * 10, 0, -85), 1f/vp.playSpeed));
                GameObject[] gogo = { go1, go2 };
                yield return StartCoroutine(fadeOut(face, gogo));
            }
            else if (GeneManager.param.crossingMode == 1)
            {
                // 交叉ポイント
                int r = Random.Range(2, 27), q = Random.Range(r + 1, 29);
                GameObject go1 = father.GetComponent<AMGroup>().getSegment(0, r);
                GameObject go2 = mother.GetComponent<AMGroup>().getSegment(r + 1, q);
                GameObject go3 = father.GetComponent<AMGroup>().getSegment(q + 1, 30 - 1);
                GameObject face = Instantiate(kao, new Vector3(0, 0, -85), Quaternion.identity) as GameObject;
                yield return StartCoroutine(go1.GetComponent<AMGenePieces>().move(new Vector3(12, 0, -85), 1f/vp.playSpeed));
                yield return StartCoroutine(go2.GetComponent<AMGenePieces>().move(new Vector3(12 + 7 + r * 10, 0, -85), 1f/vp.playSpeed));
                yield return StartCoroutine(go3.GetComponent<AMGenePieces>().move(new Vector3(12 + 7 + q * 10, 0, -85), 1f/vp.playSpeed));
                GameObject[] gogo = { go1, go2, go3 };
                yield return StartCoroutine(fadeOut(face, gogo));
            }
            else if (GeneManager.param.crossingMode == 2)
            {
                int sz = 0;
                int[] vs = new int[6];
                vs[0] = -1;
                for (int j = 1; j <= 5; j++)
                {
                    vs[j] = Random.Range(vs[j - 1]+1, 30);
                    sz++;
                    if (vs[j] == 29) break;
                }
                vs[sz] = 29;
                GameObject[] go = new GameObject[sz];
                for (int j = 0; j < sz; j++)
                {
                    if (j % 2 == 0)
                    {
                        go[j] = father.GetComponent<AMGroup>().getSegment(vs[j]+1, vs[j + 1]);
                    }
                    else
                    {
                        go[j] = mother.GetComponent<AMGroup>().getSegment(vs[j]+1, vs[j + 1]);
                    }
                }
                GameObject face = Instantiate(kao, new Vector3(0, 0, -85), Quaternion.identity) as GameObject;
                for (int j = 0; j < sz; j++)
                {
                    yield return StartCoroutine(go[j].GetComponent<AMGenePieces>().move(new Vector3(12+(vs[j]+1)*10, 0, -85), 1f/vp.playSpeed));
                }
                yield return StartCoroutine(fadeOut(face, go));
            }
            if (fatherarray[i] < 25)
            {
                yield return StartCoroutine(father.GetComponent<AMGroup>().move(new Vector3(0, 0, fatherarray[i] * 15), 0f));
            }
            else
            {
                yield return StartCoroutine(father.GetComponent<AMGroup>().move(new Vector3(320, 0, (fatherarray[i] - 25) * 15), 0f));
            }
            if (motherarray[i] < 25)
            {
                yield return StartCoroutine(mother.GetComponent<AMGroup>().move(new Vector3(0, 0, motherarray[i] * 15), 0f));
            }
            else
            {
                yield return StartCoroutine(mother.GetComponent<AMGroup>().move(new Vector3(320, 0, (motherarray[i] - 25) * 15), 0f));
            }
        }
    }
    // 交差専用
    // 同時に複数の GenePieces と頭を自然消滅させる
    private IEnumerator fadeOut(GameObject face, GameObject[] gps)
    {
        float fadeTime = 1f / vp.playSpeed;
        float currentRemainTime = fadeTime;
        float interval = AMCommon.interval;
        int sz = gps.Length;
        float buffer = 0f; // getIntervalに渡すやつ
        while (true)
        {
            currentRemainTime -= interval;
            // Debug.Log(currentRemainTime);
            if (currentRemainTime <= 0f)
            {
                Destroy(face);
                for (int i = 0; i < sz; i++)
                {
                    gps[i].GetComponent<AMGenePieces>().delete();
                    Destroy(gps[i]);
                }
                yield break;
            }
            float alpha = currentRemainTime / fadeTime;
            face.GetComponent<AMElement>().setAlpha(alpha);
            for (int i = 0; i < sz; i++) gps[i].GetComponent<AMGenePieces>().setAlpha(alpha);
            yield return new WaitForSeconds(AMCommon.getInterval(interval, ref buffer));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
