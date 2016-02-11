using UnityEngine;
using System.Collections;

public class SakeruCheesePlayerController : MonoBehaviour {
    // angleVelにはstaticをつけないとありえないほど回転が遅くなる（多分最適化が効かないせい）
    // cexen環境ではdeltaTimeは約 0.0165 s/frame
    public static float angleVel = 360.0f * 0.0165f; // 角速度[度/frame]
    void Update() {
        // 自身をy-軸周りに回転
        // 回転角[度/frame]は1フレームの間隔[s/frame]によらず一定（再現性が重要ゆえ）
        transform.Rotate(0, angleVel, 0);
    }
}
