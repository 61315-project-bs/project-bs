public class UIScene : UIBase
{
	protected override void Awake()
	{
		base.Awake();
		UIManager.Instance.SetUIScene(gameObject);
	}
}