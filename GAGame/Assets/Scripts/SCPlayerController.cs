using UnityEngine;
using System.Collections;

public class SCPlayerController : MonoBehaviour {

    public int myNum;
    public GeneManager.Player attr;
    public SCGameController gc;

    // angleVelにはstaticをつけないとありえないほど回転が遅くなる（多分最適化が効かないせい）
    // cexen環境ではdeltaTimeは約 0.0165 s/frame
    public static float angleVel = 360.0f * 0.0165f; // 角速度[度/frame]

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<SCGameController>();
        Debug.Log(gc.score);
    }

    void Update() {
        // ユーザー入力の検出
        // 右入力で時計回り(+)，左入力で反時計回り(-)，無入力で静止
        // Input.GetAxisはキーボード入力にアナログ入力的な慣性をつける
        // Input.GetAxisRawはなにもせず-1か0か1
        // どちらも，ゲームパッド入力はアナログな値をとる
        // 今回はゲームパッドも含めて全て自前で-1,0,1に揃えるので慣性は不要
        // 参考；http://albatrus.com/main/unity/7209
        float sgn = Input.GetAxisRaw("Horizontal");
        if (sgn > 0) sgn = 1;
        else if (sgn < 0) sgn = -1;
        // else x = 0;

        // 自身をy-軸周りに回転
        // 回転角[度/frame]は1フレームの間隔[s/frame]によらず一定（再現性が重要ゆえ）
        if(myNum == 0) transform.Rotate(0, sgn*angleVel, 0);
    }

    void Die()
    {
        Debug.Log(attr);
        attr.score = gc.score;
        Destroy(gameObject);
    }
}
