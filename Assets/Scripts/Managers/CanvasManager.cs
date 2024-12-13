using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Button")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backButton;
    public Button returnToMenu;
    public Button returnToGame;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Text")]
    public TMP_Text volSliderText;
    public TMP_Text livesText;

    [Header("Slider")]
    public Slider volSlider;

    // Start is called before the first frame update
    void Start()
    {
        //Button Bindings
        if (quitButton) quitButton.onClick.AddListener(QuitGame);
        if (playButton) playButton.onClick.AddListener(() => SceneManager.LoadScene("Level"));
        if (settingsButton) settingsButton.onClick.AddListener(() => SetMenus(settingsMenu, mainMenu));
        if (backButton) backButton.onClick.AddListener(() => SetMenus(mainMenu, settingsMenu));
        if (returnToMenu) returnToMenu.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        if (returnToGame) returnToGame.onClick.AddListener(() => SetMenus(null, pauseMenu));


        //Volume Slider Bindings
        if (volSlider)
        {
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(volSlider.value);
        }


        //Game Hud Bindings
        if (livesText)
        {
            GameManager.Instance.OnLifeValueChanged += (value) => livesText.text = $"Lives: {value}";
            livesText.text = $"Lives: {GameManager.Instance.lives}";
        }

        //inital menu states
        if (mainMenu)
            mainMenu.SetActive(true);
        if (settingsMenu)
            settingsMenu.SetActive(false);
        if (pauseMenu)
            pauseMenu.SetActive(false);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    void SetMenus(GameObject menuToActivate, GameObject menuToDeactivate)
    {
        if (menuToActivate)
            menuToActivate.SetActive(true);

        if (menuToDeactivate)
            menuToDeactivate.SetActive(false);
    }

    void OnSliderValueChanged(float sliderValue)
    {
        if (volSliderText)
            volSliderText.text = volSlider.value.ToString();
    }

    private void Update()
    {
        if (!pauseMenu) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
    public void OnDestroy()
    {
        Time.timeScale = 1f;
    }

}