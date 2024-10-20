/// <summary>
/// CSV : 모든 데이터 로드해서 딕셔너리로 만들어주기
/// Json : 로컬에 데이터 저장하고 불러오기
/// </summary>
public class DataManager : Singleton<DataManager>
{
  public DefaultData DefaultData { get; private set; }
  public SaveData SaveData { get; private set; }
  protected override void Awake()
  {
    _isDontDestroyOnLoad = true;
    base.Awake();

    DefaultData = new DefaultData();
    DefaultData.Init();

    SaveData = new SaveData();
    SaveData.Init();
  }
}