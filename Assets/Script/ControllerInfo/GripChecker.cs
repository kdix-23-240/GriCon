public class GripChecker
{
    private float bend = 0f; // 曲がり具合を保持する変数
    private bool isGrip = false; // グリップ状態を保持する変数
    private float bendWall = 4f; // グリップと認識する曲がり具合の閾値
    /// <summary>
    /// グリップ状態をチェックするメソッド
    /// </summary>
    /// <returns>グリップ状態がtrueならばtrue、そうでなければfalse</returns>
    public bool CheckGrip()
    {
        return isGrip;
    }
}