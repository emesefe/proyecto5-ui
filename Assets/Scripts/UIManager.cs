using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image timeImage;

    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private GameObject gameOverPanel;
    
    [SerializeField] private GameObject mainMenuPanel;
    
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Button backButton;

    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;

    [SerializeField] private Slider volumeSlider;

    private GameManager gameManagerScript;

    private void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>();
        
        //volumeSlider.onValueChanged.AddListener(gameManagerScript.SetBackgroundMusicVolume);
        
        easyButton.onClick.AddListener(() => { gameManagerScript.StartGame(1); });
        mediumButton.onClick.AddListener(() => { gameManagerScript.StartGame(2); });
        hardButton.onClick.AddListener(() => { gameManagerScript.StartGame(3); });
        timeImage.fillAmount = 1;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateTimeText(int time)
    {
        timeText.text = $"Time: {time}";
    }

    public void UpdateTimeImage(int time)
    {
        float fillAmount = time / (float)GameManager.TIME_MAX;
        timeImage.fillAmount = fillAmount;
    }

    public void UpdateTimeUI(int time)
    {
        UpdateTimeImage(time);
        UpdateTimeText(time);
    }

    public void ShowGameOverPanel(int score)
    {
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = $"Score: {score}";
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
    
    public void ShowMainMenuPanel()
    {
        mainMenuPanel.SetActive(true);
    }

    public void HideMainMenuPanel()
    {
        mainMenuPanel.SetActive(false);
    }
    
    public void ShowOptionsPanel()
    {
        optionsPanel.SetActive(true);
    }

    public void HideOptionsPanel()
    {
        optionsPanel.SetActive(false);
    }

    public void SetSliderValue(float value)
    {
        volumeSlider.value = value;
    }
}
