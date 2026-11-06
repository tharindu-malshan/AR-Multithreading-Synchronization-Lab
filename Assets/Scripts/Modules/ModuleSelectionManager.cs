
using UnityEngine;
using TMPro;

public class ModuleSelectionManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject moduleSelectionPanel;
    public TMP_Text selectedModuleText;

    public GameObject backToModulesButton;

    private int currentModule = 0;

    public void SelectModule(int moduleNumber)
    {
        currentModule = moduleNumber;

        string moduleName = moduleNumber switch
        {
            1 => "Thread Creation & Lifecycle",
            2 => "Concurrent Execution & Shared Memory",
            3 => "Race Condition Simulator",
            4 => "Mutex & Critical Section",
            5 => "Semaphore & Resource Management",
            _ => "Unknown Module"
        };

        Debug.Log("Selected Module: " + moduleName);

        if (selectedModuleText != null)
            selectedModuleText.text = moduleName;

        if (moduleSelectionPanel != null)
            moduleSelectionPanel.SetActive(false);

            if (backToModulesButton != null)
            backToModulesButton.SetActive(true);
    }

   public void OpenModuleMenu()
{
    if (moduleSelectionPanel != null)
        moduleSelectionPanel.SetActive(true);

    if (backToModulesButton != null)
        backToModulesButton.SetActive(false);
}

void Start()
{
    if (backToModulesButton != null)
        backToModulesButton.SetActive(false);
}
}
