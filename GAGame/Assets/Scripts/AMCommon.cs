using UnityEngine;
using System.Collections;

public class AMCommon : MonoBehaviour {
    // 何秒周期で動かすか
    public static float interval = 1f/30f;
    // 理想の待機時間から（前回の）処理にかかった時間を差っ引いたものを求めるやつ（ここに置くべきではない）
    // bufferの初期値は0fにしてちょんまげ
    // bufferは前回処理時刻を保存するので毎回同じやつを渡してちょんまげ
    public static float getInterval(float idealInterval, ref float buffer) // bufferはその文脈固有の作業用変数をくれればいい（別々の文脈でgetDeltaが呼ばれても干渉せぬよう）
    {
        if (buffer == 0f)
        {
            buffer = Time.time;
            return idealInterval;
        }
        float delta = 2 * idealInterval - (Time.time - buffer); // 理想的には interval == Time.time - lastTime のはず
        buffer = Time.time; // 処理にかかった時間そのものは計測できないので，WaitForSecondsで待った時間込みで計る
        // Debug.Log("delta: " + delta.ToString()); // deltaが負の値になったら処理がさっぱり追いついてない
        return Mathf.Max(0f, delta);
    }
}
