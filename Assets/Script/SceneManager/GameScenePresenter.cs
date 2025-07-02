using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class GameScenePresenter : MonoBehaviour
{
    private GameSceneModel model;
    [SerializeField] private GameClearSceneView gameClearSceneView;
    [SerializeField] private GameOverSceneView gameOverSceneView;

    void Awake()
    {
        model = new GameSceneModel();
    }

   void Start()
    {
        gameClearSceneView.Initialize();
        gameOverSceneView.Initialize();

        Bind();
    }

    private void Bind()
    {
        model.GameSceneStateProp
            .Subscribe(state =>
            {
                switch (state)
                {
                    case GameSceneModel.GameSceneState.GameStart:
                        gameClearSceneView.Hide();
                        gameOverSceneView.Hide();
                        break;
                    case GameSceneModel.GameSceneState.GamePlaying:
                        gameClearSceneView.Hide();
                        gameOverSceneView.Hide();
                        break;
                    case GameSceneModel.GameSceneState.GameOver:
                        gameClearSceneView.Hide();
                        gameOverSceneView.Show();
                        break;
                    case GameSceneModel.GameSceneState.GameClear:
                        gameOverSceneView.Hide();
                        gameClearSceneView.Show();
                        break;
                }
            })
            .AddTo(this);
    }
}