using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// アニメーションのサンプル
// fadeOut は複数の genePiece を同時に透明にしていって最後に消す関数
public class AMTestAnimation : MonoBehaviour
{
    public GameObject group, element, kao;
    // Use this for initialization
    void Start()
    {
        // まず Instantiate 関数でオブジェクトを作る
        element = Instantiate(element, new Vector3(-2, 0, 0), Quaternion.identity) as GameObject;
        group = Instantiate(group, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        // Sample にアニメーションの動きを次々に書いていく
        StartCoroutine("Sample");
    }
    private IEnumerator Sample()
    {
        // 単体オブジェクトを点滅させる
        yield return StartCoroutine(element.GetComponent<AMElement>().blink(1f));
        // 2 個のオブジェクトを同時に点滅させる
        group.GetComponent<AMGroup>().setScore(30f);
        yield return StartCoroutine(group.GetComponent<AMGroup>().blink(new int[] { 0, 1 }, 1f));
        group.GetComponent<AMGroup>().setColor(new int[] { -1, 0, 1 });
        // 単体オブジェクトを動かす
        yield return StartCoroutine(element.GetComponent<AMElement>().move(new Vector3(1, 1, 1), 1f));
        group.GetComponent<AMGroup>().setScore(0f);
        // オブジェクトをまとめて二段階に分けて動かす
        // 一段階目
        yield return StartCoroutine(group.GetComponent<AMGroup>().move(new Vector3(1, 1, 1), 1f));
        // 二段階目
        yield return StartCoroutine(group.GetComponent<AMGroup>().move(new Vector3(1, 3, 1), 0f));
        // group から一部切り取る
        GameObject go = group.GetComponent<AMGroup>().getSegment(0, 3);
        yield return StartCoroutine(go.GetComponent<AMGenePieces>().move(new Vector3(-1, -1, -1), 1f));
        GameObject face = Instantiate(kao, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        GameObject[] gogo = { go };
        yield return StartCoroutine(fadeOut(face, gogo));
    }
    // 交差専用
    // 同時に複数の GenePieces と頭を自然消滅させる
    private IEnumerator fadeOut(GameObject face, GameObject[] gps)
    {
        float fadeTime = 1f;
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
    public void Skip()
    {
        SceneManager.LoadScene("SakeruCheese");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Skip();
        }
    }
}
