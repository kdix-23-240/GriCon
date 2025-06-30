using UniRx;

public class ControllerModel
{
    // シングルトンのインスタンスを保持する静的なプロパティ
    private static ControllerModel _instance;
    public static ControllerModel GetInstance
    {
        get
        {
            // インスタンスがまだ存在しない場合、新しく作成する
            if (_instance == null)
            {
                _instance = new ControllerModel();
            }
            return _instance;
        }
    }

    // 各回転軸とベンドのReactiveProperty
    // private readonly フィールドは不要。直接publicプロパティを使う
    public ReactiveProperty<float> RotateX { get; private set; }
    public ReactiveProperty<float> RotateY { get; private set; }
    public ReactiveProperty<float> RotateZ { get; private set; }
    public ReactiveProperty<float> Bend { get; private set; }

    // コンストラクタをprivateにすることで、外部からのインスタンス化を防ぐ
    private ControllerModel()
    {
        RotateX = new ReactiveProperty<float>(0f);
        RotateY = new ReactiveProperty<float>(0f);
        RotateZ = new ReactiveProperty<float>(0f);
        Bend = new ReactiveProperty<float>(0f);
    }
}