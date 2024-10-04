public class Presenter<T> where T : UIBase
{
  protected T _view;
  public Presenter(T view)
  {
    _view = view;
  }
}