using UnityEngine;
using System.Collections;

// アニメーションのサンプル
public class AMTestAnimation : MonoBehaviour
{
    public GameObject group, element;
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
    }
    // Update is called once per frame
    void Update()
    {

    }
}
