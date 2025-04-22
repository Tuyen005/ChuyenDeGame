using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public TMP_InputField nicknameInput;
    public TextMeshProUGUI errorText;

    private NetworkRunner runner;
    private const int GAME_SCENE_BUILD_INDEX = 1;

    void Awake()
    {
        if (nicknameInput == null || errorText == null)
        {
            Debug.LogError("NicknameInput hoặc ErrorText chưa được gán!");
        }
        errorText.text = "";
    }

    public async void OnJoinButtonClicked()
    {
        string nickname = nicknameInput.text.Trim();
        if (string.IsNullOrEmpty(nickname) || nickname.Length > 20)
        {
            errorText.text = "Nickname phải có 1-20 ký tự!";
            return;
        }

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            errorText.text = "Không có kết nối internet!";
            return;
        }

        if (runner == null)
        {
            GameObject runnerObj = new GameObject("NetworkRunner");
            runner = runnerObj.AddComponent<NetworkRunner>();
            runner.ProvideInput = true;
            DontDestroyOnLoad(runnerObj);
        }

        int sceneIndex = GAME_SCENE_BUILD_INDEX;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if (sceneIndex < 0 || sceneIndex >= sceneCount)
        {
            errorText.text = "Scene Game không hợp lệ trong Build Settings!";
            return;
        }

        var startGameArgs = new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = "GameRoom",
            Scene = SceneRef.FromIndex(sceneIndex),
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };

        try
        {
            var result = await runner.StartGame(startGameArgs);
            if (result.Ok)
            {
                PlayerPrefs.SetString("PlayerNickname", nickname);
                SceneManager.LoadScene("Game");
            }
            else
            {
                errorText.text = "Không thể tham gia phòng: " + result.ErrorMessage;
                await runner.Shutdown();
                runner = null;
            }
        }
        catch (System.Exception ex)
        {
            errorText.text = "Lỗi khi tham gia phòng!";
            Debug.LogError($"Lỗi StartGame: {ex.Message}");
            if (runner != null)
            {
                await runner.Shutdown();
                runner = null;
            }
        }
    }

    void OnDestroy()
    {
        if (runner != null)
        {
            runner.Shutdown();
            runner = null;
        }
    }
}