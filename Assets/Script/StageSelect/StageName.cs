public class StageName
{
    private static StageName instance;
    public static StageName GetInstance()
    {
        if (instance == null)
        {
            instance = new StageName();
        }
        return instance;
    }
    private StageName() { }
    public string StageNameText { get; set; } = "Stage1"; // ‰Šú’l‚ÍStage1
}