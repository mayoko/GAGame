using UnityEngine;
using System.Collections;

public class AMtest : MonoBehaviour {
    int[] score;
    GameObject[] scoreobject;
    public GameObject element, kao;
    GeneManager.ViewParam vp = GeneManager.viewParam;

    //遺伝子の配列情報（-1,0,1を適当な色に置き換えておく）
    int[][] colorarray;

    GameObject[] geneObject;

    //親となる遺伝子の（子を作る）順番を記した配列
    int[] fatherarray,motherarray;

    GameObject father,mother,child;

    //交叉ポイント
    int r;

    // Use this for initialization
    void Start () {
        // input
        score = new int[50];
        colorarray = new int[50][];
        for (int i = 0; i < 50; i++) {
            colorarray[i] = new int[30];
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
        for (int i = 0; i < 50; i++) motherarray[i] = 1;
        for (int i = 0; i < 50; i++) fatherarray[i] = 10;
        //交叉の開始
        StartCoroutine ("cross");
    }
    private IEnumerator cross ()
    {
        //親の移動
        r = 15;
        for (int i = 0; i < 50; i++) {
            if (fatherarray[i] == motherarray[i]) continue;
            father = geneObject[fatherarray[i]];
            mother = geneObject[motherarray[i]];
            yield return StartCoroutine (father.GetComponent<AMGroup> ().move (new Vector3 (0, 0, -100), 1f));
            yield return StartCoroutine (mother.GetComponent<AMGroup> ().move (new Vector3 (0, 0, -70), 1f));
            GameObject go1 = father.GetComponent<AMGroup> ().getSegment (0, r);
            GameObject go2 = mother.GetComponent<AMGroup> ().getSegment (r+1, 30 - 1);
            GameObject face = Instantiate(kao, new Vector3(0, 0, -85), Quaternion.identity) as GameObject;
            yield return StartCoroutine(go1.GetComponent<AMGenePieces>().move(new Vector3(12, 0, -85), 1f));
            yield return StartCoroutine(go2.GetComponent<AMGenePieces>().move(new Vector3(12 + 7 + r * 10, 0, -85), 1f));
            GameObject[] gogo = { go1, go2 };
            yield return StartCoroutine(fadeOut(face, gogo));
            go1.GetComponent<AMGenePieces>().delete();
            go2.GetComponent<AMGenePieces>().delete();
            if (fatherarray[i] < 25)
            {
                yield return StartCoroutine(father.GetComponent<AMGroup>().move(new Vector3(0, 0, fatherarray[i] * 15), 0f));
            }
            else
            {
                yield return StartCoroutine(father.GetComponent<AMGroup>().move(new Vector3(320, 0, (fatherarray[i]-25) * 15), 0f));
            }
            if (motherarray[i] < 25)
            {
                yield return StartCoroutine(mother.GetComponent<AMGroup>().move(new Vector3(0, 0, motherarray[i] * 15), 0f));
            }
            else
            {
                yield return StartCoroutine(mother.GetComponent<AMGroup>().move(new Vector3(320, 0, (motherarray[i]-25) * 15), 0f));
            }
        }
    }
    // 交差専用
    // 同時に複数の GenePieces と頭を自然消滅させる
    private IEnumerator fadeOut(GameObject face, GameObject[] gps)
    {
        float fadeTime = 1f / vp.playSpeed;
        float currentRemainTime = fadeTime;
        float interval = 0.01f;
        int sz = gps.Length;
        while (true)
        {
            currentRemainTime -= interval;
            Debug.Log(currentRemainTime);
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
            yield return new WaitForSeconds(interval);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
