using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class GameScenePresenter : MonoBehaviour
{
    private GameSceneModel model;
    [SerializeField] private Scene sceneManager; // シーン管理用のコンポーネントを参照

    void Awake()
    {
        model = new GameSceneModel();
    }

   void Start()
    {

        Bind();
    }

    private void Bind()
    {

    }
}