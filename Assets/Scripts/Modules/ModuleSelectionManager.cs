
using UnityEngine;
using TMPro;

public class ModuleSelectionManager : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject moduleSelectionPanel;
    public GameObject backToModulesButton;
    public TMP_Text selectedModuleText;

    [Header("Module 1 UI")]
    public GameObject module1UI;
    public GameObject module1ChallengePanel;

    [Header("Module 2 UI")]
    public GameObject module2ControlPanel;

    [Header("AR Placement")]
    [SerializeField] private ARLabPlacement arLabPlacement;

    [Header("Module 2 Code Panel")]
    public GameObject module2CodePanel;

    [Header("Module 2 Challenge")]
public GameObject module2ChallengePanel;
public GameObject module2OpenChallengeButton;

     [Header("Module 3 UI")]
public GameObject module3ControlPanel;

[Header("Module 3 Code Panel")]
public GameObject module3CodePanel;

[Header("Module 3 Challenge")]
public GameObject module3ChallengePanel;
public GameObject module3OpenChallengeButton;

[Header("Module 4 UI")]
public GameObject module4ControlPanel;

[Header("Module 4 Code Panel")]
public GameObject module4CodePanel;

[Header("Module 4 Challenge")]
public GameObject module4ChallengePanel;
public GameObject module4OpenChallengeButton;

    private int currentModule = 0;

    private void Start()
    {
        OpenModuleMenu();
    }

    public void SelectModule(int moduleNumber)
    {
        if (moduleNumber < 1 || moduleNumber > 5)
        {
            Debug.LogWarning("Invalid module number.");
            return;
        }

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

        // Update selected module title
        if (selectedModuleText != null)
            selectedModuleText.text = moduleName;

        // Hide module selection menu
        if (moduleSelectionPanel != null)
            moduleSelectionPanel.SetActive(false);

        // Show Back button
        if (backToModulesButton != null)
            backToModulesButton.SetActive(true);

        // Show Module 1 UI only for Module 1
        if (module1UI != null)
            module1UI.SetActive(moduleNumber == 1);

        // Show Module 2 UI only for Module 2
        if (module2ControlPanel != null)
            module2ControlPanel.SetActive(moduleNumber == 2);

        // Close Module 1 challenge
        if (module1ChallengePanel != null)
            module1ChallengePanel.SetActive(false);

        // Enable AR placement for selected module
        if (arLabPlacement != null)
        {
            arLabPlacement.EnablePlacement(moduleNumber);
        }
        else
        {
            Debug.LogWarning(
                "ARLabPlacement reference is missing!"
            );
        }

        if (module2CodePanel != null)
    module2CodePanel.SetActive(moduleNumber == 2);

    if (module2ChallengePanel != null)
    module2ChallengePanel.SetActive(false);

if (module2OpenChallengeButton != null)
    module2OpenChallengeButton.SetActive(moduleNumber == 2);

    if (module3ControlPanel != null)
    module3ControlPanel.SetActive(moduleNumber == 3);

    if (module3CodePanel != null)
    module3CodePanel.SetActive(moduleNumber == 3);

    if (module3ChallengePanel != null)
    module3ChallengePanel.SetActive(false);

if (module3OpenChallengeButton != null)
    module3OpenChallengeButton.SetActive(moduleNumber == 3);

    if (module4ControlPanel != null)
    module4ControlPanel.SetActive(moduleNumber == 4);

    if (module4CodePanel != null)
    module4CodePanel.SetActive(moduleNumber == 4);

    if (module4ChallengePanel != null)
    module4ChallengePanel.SetActive(false);

if (module4OpenChallengeButton != null)
    module4OpenChallengeButton.SetActive(moduleNumber == 4);
    }


    public void OpenModuleMenu()
    {
        currentModule = 0;

        // Show module selection menu
        if (moduleSelectionPanel != null)
            moduleSelectionPanel.SetActive(true);

        // Hide Back button
        if (backToModulesButton != null)
            backToModulesButton.SetActive(false);

        // Hide Module 1 UI
        if (module1UI != null)
            module1UI.SetActive(false);

        // Hide Module 2 UI
        if (module2ControlPanel != null)
            module2ControlPanel.SetActive(false);

        // Close Module 1 challenge
        if (module1ChallengePanel != null)
            module1ChallengePanel.SetActive(false);

        // Remove placed lab and disable placement
        if (arLabPlacement != null)
        {
            arLabPlacement.DisablePlacement();
        }

        if (module2CodePanel != null)
    module2CodePanel.SetActive(false);

    if (module2ChallengePanel != null)
    module2ChallengePanel.SetActive(false);

if (module2OpenChallengeButton != null)
    module2OpenChallengeButton.SetActive(false);

        Debug.Log("Returned to Module Selection Menu.");

        if (module3ControlPanel != null)
    module3ControlPanel.SetActive(false);

    if (module3CodePanel != null)
    module3CodePanel.SetActive(false);

    if (module3ChallengePanel != null)
    module3ChallengePanel.SetActive(false);

if (module3OpenChallengeButton != null)
    module3OpenChallengeButton.SetActive(false);

    if (module4ControlPanel != null)
    module4ControlPanel.SetActive(false);

    if (module4CodePanel != null)
    module4CodePanel.SetActive(false);

    if (module4ChallengePanel != null)
    module4ChallengePanel.SetActive(false);

if (module4OpenChallengeButton != null)
    module4OpenChallengeButton.SetActive(false);
    }
}
