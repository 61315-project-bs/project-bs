using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using Unity.EditorCoroutines.Editor;

/// <summary>
/// 구글 시트 다운로드 해주는 버튼 만들기
/// 코드에 링크, 파일저장이름 등 다 넣어놨다.
/// </summary>
public class ToolsSheetDownloader : EditorWindow
{
    const string _address = "1z-SKUeGSrDSloutsEzF-UGzgr9fPBsGuF2mKg00R7Yo";
    const string _sheetPath = "Assets/Resources/CSV";
    const string _format = "csv";

    struct SheetInfo
    {
        public string Name;
        public string Id;
    }

    SheetInfo[] _sheets = new SheetInfo[]
    {
        new SheetInfo { Name = "TestData", Id = "1583897051" },
    };

    private int _pendingDownloads = 0;

    /// <summary>
    /// 툴에서 메뉴 창으로 띄우기
    /// </summary>
    [MenuItem("Tools/Sheet Downloader")]
    public static void ShowWindow()
    {
        GetWindow<ToolsSheetDownloader>("Sheet Downloader");
    }

    /// <summary>
    /// 다운로드 버튼
    /// </summary>
    private void OnGUI()
    {
        if (GUILayout.Button("전부 다운받기", GUILayout.Height(40)))
        {
            DownloadAllCSVFiles();
        }

        foreach (SheetInfo sheet in _sheets)
        {
            GUILayout.BeginHorizontal();
            GUIStyle labelStyle = EditorStyles.boldLabel;
            labelStyle.alignment = TextAnchor.MiddleLeft;

            GUILayout.Label($"{sheet.Name}", labelStyle, GUILayout.Height(30));
            if (GUILayout.Button($"다운로드", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _pendingDownloads = 1;
                EditorCoroutineUtility.StartCoroutineOwnerless(Download(sheet.Id, sheet.Name));
            }
            GUILayout.EndHorizontal();
        }
    }

    /// <summary>
    /// 시트 다 다운받기 - 이것만 사용해도 다 받아진다.
    /// </summary>
    private void DownloadAllCSVFiles()
    {
        _pendingDownloads = _sheets.Length;
        EditorApplication.LockReloadAssemblies(); // 에디터 잠금
        foreach (SheetInfo sheet in _sheets)
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(Download(sheet.Id, sheet.Name));
        }
    }

    /// <summary>
    /// 링크로 오픈된 구글 시트에서 각 링크, 페이지 gid로 찾아서 csv로 다운로드
    /// </summary>
    /// <param name="sheetId"></param>
    /// <param name="saveFileName"></param>
    /// <param name="sheetPath"></param>
    /// <param name="address"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    private IEnumerator Download(string sheetId, string saveFileName, string sheetPath = _sheetPath, string address = _address, string format = _format)
    {
        var url = $"https://docs.google.com/spreadsheets/d/{address}/export?format={format}&gid={sheetId}";
        using (var www = UnityWebRequest.Get(url))
        {
            Debug.Log("Start Downloading...");

            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to download {saveFileName}: {www.error}");
                _pendingDownloads--;
                CheckPendingDownloads(); // 다운로드 상태 확인
                yield break;
            }

            var fileUrl = $"{_sheetPath}/{saveFileName}.{format}";
            File.WriteAllText(fileUrl, www.downloadHandler.text + "\n");

            Debug.Log("Download Complete.");
        }

        _pendingDownloads--;
        CheckPendingDownloads(); // 다운로드 상태 확인
    }

    /// <summary>
    /// 다운로드가 모두 완료되었는지 확인 후 완료 시 에디터 잠금 해제
    /// </summary>
    private void CheckPendingDownloads()
    {
        if (_pendingDownloads <= 0)
        {
            EditorApplication.UnlockReloadAssemblies(); // 에디터 잠금 해제
            EditorUtility.DisplayDialog("다운로드 완료", "All CSV files have been downloaded successfully.", "OK");
        }
    }
}
