using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
  int _popupSortingOrder = -10;
  int _subSceneSortingOrder = -20;
  Stack<UISubScene> _subSceneStack = new Stack<UISubScene>();
  Stack<UIPopup> _popupStack = new Stack<UIPopup>();

  protected override void Awake()
  {
    _isDontDestroyOnLoad = true;
    base.Awake();
  }

  public GameObject UIRoot
  {
    get
    {
      GameObject uiroot = GameObject.Find("UIRoot");
      if (uiroot == null)
      {
        uiroot = new GameObject { name = "UIRoot" };
      }
      return uiroot;
    }
  }

  public UIScene UIScene { get; private set; }

  public void SetUIScene(GameObject go, bool isNotOverlay = false)
  {
    Canvas canvas = go.GetOrAddComponent<Canvas>();
    // if (!isNotOverlay) canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    canvas.overrideSorting = true;
    canvas.sortingOrder = -25;

    UIScene = go.GetOrAddComponent<UIScene>();
    go.transform.SetParent(UIRoot.transform);
  }

  #region UISubScene

  /// <summary>
  /// Instantiates a UISubScene and pushes it onto the stack managed by UIScene.
  /// </summary>
  public void SetUISubScene(GameObject go, bool sort = true)
  {
    Canvas canvas = go.GetOrAddComponent<Canvas>();
    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    canvas.overrideSorting = true;

    // 서브 씬을 활성화하기 전에 이전 씬을 모두 비활성화
    if (_subSceneStack.Count > 0)
    {
      _subSceneStack.Peek().gameObject.SetActive(false);
    }

    if (sort)
    {
      canvas.sortingOrder = _subSceneSortingOrder;
      _subSceneSortingOrder++;
    }
    else
    {
      canvas.sortingOrder = 0;
    }
  }

  public T InstantiateUISubScene<T>(string name = null, Transform parent = null) where T : UISubScene
  {
    if (string.IsNullOrEmpty(name))
    {
      name = typeof(T).Name;
    }

    GameObject go = ResourceManager.Instance.Instantiate($"UI/UISubScene/{name}");
    T subScene = go.GetOrAddComponent<T>();

    if (_subSceneStack.Count == 0)
    {
      UIScene.gameObject.SetActive(false);
    }

    _subSceneStack.Push(subScene);

    if (parent != null)
    {
      go.transform.SetParent(parent);
    }
    else
    {
      go.transform.SetParent(UIRoot.transform);
    }

    go.transform.localScale = Vector3.one;

    return subScene;
  }

  public void DestroyUISubScene(UISubScene subScene)
  {
    if (_subSceneStack.Count == 0 || _subSceneStack.Peek() != subScene)
    {
      Debug.LogError("Close SubScene Failed!");
      return;
    }

    // 현재 서브 씬 파괴
    _subSceneStack.Pop();
    if (subScene != null && subScene.gameObject != null)
    {
      Destroy(subScene.gameObject);
      _subSceneSortingOrder--;
    }

    // 스택에 남아 있는 이전 씬이 있다면 다시 활성화
    if (_subSceneStack.Count > 0)
    {
      _subSceneStack.Peek().gameObject.SetActive(true);
    }
    else
    {
      if (UIScene != null)
      {
        UIScene.gameObject.SetActive(true);
      }
    }
  }

  public void DestroyAllUISubScenes()
  {
    while (_subSceneStack.Count > 0)
    {
      UISubScene subScene = _subSceneStack.Pop();
      if (subScene != null && subScene.gameObject != null)
      {
        Destroy(subScene.gameObject);
        _subSceneSortingOrder--;
      }
    }

    // 서브 씬이 모두 파괴된 후, 메인 씬이 있다면 활성화
    if (UIScene != null)
    {
      UIScene.gameObject.SetActive(true);
    }
  }

  public T FindUISubScene<T>() where T : UISubScene
  {
    return _subSceneStack.Where(x => x.GetType() == typeof(T)).FirstOrDefault() as T;
  }

  #endregion

  #region UIPopup
  public void SetUIPopup(GameObject go, bool sort = true)
  {
    Canvas canvas = go.GetOrAddComponent<Canvas>();
    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    canvas.overrideSorting = true;

    if (sort)
    {
      canvas.sortingOrder = _popupSortingOrder;
      _popupSortingOrder++;
    }
    else
    {
      canvas.sortingOrder = 0;
    }
  }
  public T InstantiateUIPopup<T>(string name = null, Transform parent = null) where T : UIPopup
  {
    if (string.IsNullOrEmpty(name))
    {
      name = typeof(T).Name;
    }

    GameObject go = ResourceManager.Instance.Instantiate($"UI/UIPopup/{name}");
    T popup = go.GetOrAddComponent<T>();
    _popupStack.Push(popup);

    if (parent != null)
    {
      go.transform.SetParent(parent);
    }
    else
    {
      go.transform.SetParent(UIRoot.transform);
    }

    go.transform.localScale = Vector3.one;

    return popup;
  }

  public void DestroyUIPopup(UIPopup popup)
  {
    if (_popupStack.Count == 0 || _popupStack.Peek() != popup)
    {
      Debug.LogError("Close Popup Failed!");
      return;
    }

    _popupStack.Pop();
    if (popup != null && popup.gameObject != null)
    {
      Destroy(popup.gameObject);
      _popupSortingOrder--;
    }
  }

  public void DestroyAllUIPopups()
  {
    while (_popupStack.Count > 0)
    {
      UIPopup popup = _popupStack.Pop();
      if (popup != null && popup.gameObject != null)
      {
        Destroy(popup.gameObject);
        _popupSortingOrder--;
      }
    }
  }
  public T FindUIPopup<T>() where T : UIPopup
  {
    return _popupStack.Where(x => x.GetType() == typeof(T)).FirstOrDefault() as T;
  }

  #endregion
}
