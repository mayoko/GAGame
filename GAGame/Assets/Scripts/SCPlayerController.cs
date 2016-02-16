using UnityEngine;
using System.Collections;

public class SCPlayerController : MonoBehaviour {

    public int myNum;
    public string mode;
    public GeneManager.Player attr;
    public SCGameController gc; // PrefabにはSceneオブジェクトはアサインできない

    // angleVelにはstaticをつけないとありえないほど回転が遅くなる（多分最適化が効かないせい）
    // cexen環境ではdeltaTimeは約 0.0165 s/frame
    public static float angleVel = 360.0f * 0.0165f; // 角速度[度/frame]

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<SCGameController>();
    }

    void Update() {
        int cf = gc.getCurrentFrame();

        // Player操作
        {
            sbyte sgn; // 1: 時計回り，-1: 反時計回り，0: 静止
            switch (mode)
            {
                case "manual":
                    // ユーザー入力の検出
                    // 右入力で時計回り(+)，左入力で反時計回り(-)，無入力で静止
                    // Input.GetAxisはキーボード入力にアナログ入力的な慣性をつける
                    // Input.GetAxisRawはなにもせず-1か0か1
                    // どちらも，ゲームパッド入力はアナログな値をとる
                    // 今回はゲームパッドも含めて全て自前で-1,0,1に揃えるので慣性は不要
                    // 参考；http://albatrus.com/main/unity/7209
                    float input = Input.GetAxisRaw("Horizontal");
                    if (input > 0) sgn = 1;
                    else if (input < 0) sgn = -1;
                    else sgn = 0;

                    // 操作の記録
                    attr.gene[cf] = sgn;
                    break;
                case "auto":
                    if (cf < attr.gene.Length) sgn = attr.gene[cf];
                    else
                    {
                        sgn = 0;
                        Debug.LogWarning("geneの長さが足りてないよ: cf=" + cf.ToString());
                    }
                    break;
                default:
                    sgn = 0;
                    Debug.LogError("Unknown type of player mode: " + mode);
                    break;
            }

            // 自身をy-軸周りに回転
            // 回転角[度/frame]は1フレームの間隔[s/frame]によらず一定（再現性が重要ゆえ）
            transform.Rotate(0, sgn * angleVel, 0);
        }

        // 世界の終わりの刻が来たら自殺（いちおう最後のユーザー入力も記録してから）
        if (cf == gc.finalFrame) Die();
    }

    public void Die()
    {
        attr.score = gc.getScore();
        Debug.Log(myNum.ToString() + "番のPlayerがf." + gc.getCurrentFrame().ToString() + "で死んでScoreは" + attr.score.ToString() + "でした．");

        // Destroy(child)だと子のtransformコンポーネントが死ぬだけなので注意
        foreach (Transform child in transform) Destroy(child.gameObject);
        Destroy(gameObject);
    }
}
