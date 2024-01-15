using Newtonsoft.Json.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;


    [SerializeField] GameObject _mainMenuPanelGo;
    [SerializeField] GameObject _settingsPanelGo;
    [SerializeField] GameObject _levelsPanelGo;
    [SerializeField] GameObject _mapSelectionPanelGo;
    [SerializeField] GameObject _soundSettingsPanelGo;

    List<GameObject> _panelList = new List<GameObject>();

    GameObject _activePanel;
    public List<GameObject> _panelHistoryList = new List<GameObject>();
    public int _panelIndex = -1;

    bool isPaused;

    public GameObject LastSelected { get; set; }
    public int LastSelectedIndex { get; set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        if (_mainMenuPanelGo != null)
            _panelList.Add(_mainMenuPanelGo);
        if (_settingsPanelGo != null)
            _panelList.Add(_settingsPanelGo);
        if (_levelsPanelGo != null)
            _panelList.Add(_levelsPanelGo);
        if(_mapSelectionPanelGo != null)
            _panelList.Add(_mapSelectionPanelGo);
        if (_soundSettingsPanelGo != null)
            _panelList.Add(_soundSettingsPanelGo);

        _settingsPanelGo.SetActive(false);
        _levelsPanelGo.SetActive(false);
        _mapSelectionPanelGo.SetActive(false);
        _activePanel = _mainMenuPanelGo;
    }



    #region Canvas Activations
    public void OpenLevelSelection()
    {
        _levelsPanelGo.SetActive(true);
        SaveActiveAndPreviousPanel(_levelsPanelGo);
    }
    public void OpenMapSelection()
    {
        _mapSelectionPanelGo.SetActive(true);
        SaveActiveAndPreviousPanel(_mapSelectionPanelGo);
    }
    public void OpenSettings()
    {
        _settingsPanelGo.SetActive(true);
        SaveActiveAndPreviousPanel(_settingsPanelGo);
    }
    public void OpenSoundSettings()
    {
        _soundSettingsPanelGo.SetActive(true);
        SaveActiveAndPreviousPanel(_soundSettingsPanelGo);
    }

    void CloseAllMenus()
    {
        _mainMenuPanelGo.SetActive(false);
        _settingsPanelGo.SetActive(false);
    }
    void SaveActiveAndPreviousPanel(GameObject panel)
    {
        _panelHistoryList.Add(_activePanel);
        _activePanel = panel;
        _panelIndex++;
    }
    public void GoBackToPreviousPanel()
    { 
        _activePanel.SetActive(false);
        var previousPanel = _panelHistoryList[_panelIndex];
        _activePanel = previousPanel;
        _panelIndex--;
        _activePanel.SetActive(true);

        if(_activePanel == _mainMenuPanelGo)
            _panelHistoryList.Clear();
    }
    #endregion


}
