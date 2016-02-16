//C#
public class TestHorizontalSlider : MonoBehaviour {
	public float hSliderValue = 0.0F;
	void OnGUI () {
		// スライダー（水平）を表示する
		hSliderValue = GUI.HorizontalSlider(new Rect(20, 20, 100, 30), hSliderValue, 0.0F, 10.0F);
		// ラベルにhSliderValueの値を表示する
		GUI.Label(new Rect(20, 50, 100, 20), "value: " + hSliderValue);
	}
}