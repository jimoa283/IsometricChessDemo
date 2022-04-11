using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace KasperDev.DialogueEditor
{
    public class DialogueEditorWindow : EditorWindow
    {
        private DialogueContainerSO currentDialogueContainer;                                   // Current open dialogue in dialogue editor window.
        private DialogueGraphView graphView;                                                    // Reference to GrapView Class.
        private DialogueSaveAndLoad saveAndLoad;                                                // Reference to SaveAndLoad Class.

        private LanguageType selectedLanguage = LanguageType.English;                           // Current selected language in the dialogue editor window
        private ToolbarMenu languageDropdownMenu;                                               // Languages toolbar menu in the top of dialogue editor window.
        private Label nameOfDialogueContainer;                                                  // Name of the current open dialogue container.
        private string graphViewStyleSheet = "USS/EditorWindow/EditorWindowStyleSheet";        // Name of the graph view style sheet.

        /// <summary>
        /// Current selected language in the dialogue editor window.
        /// </summary>
        public LanguageType SelectedLanguage { get => selectedLanguage; set => selectedLanguage = value; }

        [OnOpenAsset(1)]
        public static bool ShowWindow(int instanceId, int line)
        {
            UnityEngine.Object item = EditorUtility.InstanceIDToObject(instanceId);   //Find Unity Object with this instanceId and load it in.

            if (item is DialogueContainerSO)              //Check if item is a DialogueContainerSO object
            {
                DialogueEditorWindow window = (DialogueEditorWindow)GetWindow(typeof(DialogueEditorWindow));     //Make a unity editor window of type DialogueEditorWindow. 
                window.titleContent = new GUIContent("Dialogue Editor");                                        //Name of editor window.
                window.currentDialogueContainer = item as DialogueContainerSO;                                  //The DialogueContainerSO we will load in to editor window.
                window.minSize = new Vector2(500, 500);                                                         //Starter size of the editor window.
                window.Load();                                                                                  //Load in DialogueContainerSO data in to the editor window.
            }

            return false;   //we did not handle the open
        }

        private void OnEnable()
        {
            ConstructGraphView();
            GenerateToolbar();
            Load();
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }


        /// <summary>
        /// Construct graph view 
        /// </summary>
        private void ConstructGraphView()
        {
            //Make the DialogueGraphView and stretch it to the same size as the Parent.
            //Add it to the DialogueEditorWindow.
            graphView = new DialogueGraphView(this);
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);

            saveAndLoad = new DialogueSaveAndLoad(graphView);
        }

        /// <summary>
        /// Generate the toolbar you will see in the top left of the dialogue editor window.
        /// </summary>
        private void GenerateToolbar()
        {
            //Find and load the styleSheet for graph view.
            StyleSheet styleSheet = Resources.Load<StyleSheet>(graphViewStyleSheet);
            //Add the styleSheet for graph view.
            rootVisualElement.styleSheets.Add(styleSheet);

            Toolbar toolbar = new Toolbar();


            //Save button.
            {
                Button saveBtn = new Button()
                {
                    text = "Save"
                };
                saveBtn.clicked += () =>
                  {
                      Save();
                  };
                toolbar.Add(saveBtn);
            }

            //Load button.
            {
                Button loadBtn = new Button()
                {
                    text = "Load"
                };
                loadBtn.clicked += () =>
                  {
                      Load();
                  };
                toolbar.Add(loadBtn);
            }

            //Dropdown menu for languages
            {
                languageDropdownMenu = new ToolbarMenu();

                //Here we go through each language and make a button with that language.
                //When you click on the language in the dropdown menu we tell it to run Language(language) method.
                foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
                {
                    languageDropdownMenu.menu.AppendAction(language.ToString(), new Action<DropdownMenuAction>(x => Language(language)));
                }
                toolbar.Add(languageDropdownMenu);
            }

            //Name of current DialogueContainer you have open.
            {
                nameOfDialogueContainer = new Label("");
                toolbar.Add(nameOfDialogueContainer);
                nameOfDialogueContainer.AddToClassList("nameofDialogueContainer");
            }

            rootVisualElement.Add(toolbar);
        }


        /// <summary>
        /// Will load in current selected dialogue container.
        /// </summary>
        private void Load()
        {
            //Debug.Log("Load");
            if (currentDialogueContainer != null)
            {
                Language(LanguageType.English);
                nameOfDialogueContainer.text = "Name: " + currentDialogueContainer.name;
                saveAndLoad.Load(currentDialogueContainer);
            }
        }


        /// <summary>
        /// Will save the current changes to dialogue container.
        /// </summary>
        private void Save()
        {
            if (currentDialogueContainer != null)
            {
                saveAndLoad.Save(currentDialogueContainer);
            }

        }

        /// <summary>
        /// Will change the language in the dialogue editor window
        /// </summary>
        /// <param name="language">Language that you want to change to</param>
        private void Language(LanguageType language)
        {
            languageDropdownMenu.text = "Language: " + language.ToString();
            selectedLanguage = language;
            graphView.ReloadLanguage();
        }
    }
}
