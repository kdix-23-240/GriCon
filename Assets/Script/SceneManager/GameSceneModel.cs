using UniRx;

public class GameSceneModel
{
    public enum GameSceneState
    {
        GameStart,     // ゲーム開始
        GamePlaying,   // ゲームプレイ中
        GameOver,      // ゲームオーバー
        GameClear      // ゲームクリア
    }

    private ReactiveProperty<GameSceneState> _gameSceneState;
    public ReactiveProperty<GameSceneState> GameSceneStateProp
    {
        get { return _gameSceneState; }
        set { _gameSceneState = value; }
    }

    public GameSceneModel()
    {
        _gameSceneState = new ReactiveProperty<GameSceneState>(GameSceneState.GameStart);
    }
}