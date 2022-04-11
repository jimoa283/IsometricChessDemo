using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;
using System.Linq;

namespace KasperDev.DialogueEditor
{
    public class DialogueNode : BaseNode
    {
        private DialogueData dialogueData = new DialogueData();
        public DialogueData DialogueData { get => dialogueData; set => dialogueData = value; }

        public DialogueNode() { }

        public DialogueNode(Vector2 position, DialogueEditorWindow editorWindow, DialogueGraphView graphView)
        {
            this.editorWindow = editorWindow;
            this.graphView = graphView;

            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/DialogueNodeStyleSheet");
            styleSheets.Add(styleSheet);

            title = "Dialogue";
            SetPosition(new Rect(position, defaultNodeSize));
            nodeGuid = Guid.NewGuid().ToString();

            //Add standard ports
            AddInputPort("Input", Port.Capacity.Multi);
            AddOutputPort("Continue");

            TopContainer();

            CharacterName();
            ImagePic();
            TextLine();

        }

        private void TopContainer()
        {
            AddPortButton();
            //AddDropdownMenu();
        }

        private void AddPortButton()
        {
            Button btn = new Button()
            {
                text = "Add Choice",
            };
            btn.AddToClassList("TopBtn");

            btn.clicked += () =>
              {
                  AddChoicePort(this);
              };

            titleButtonContainer.Add(btn);
        }

        private void AddDropdownMenu()
        {
            ToolbarMenu Menu = new ToolbarMenu();
            Menu.text = "Add Content";

            Menu.menu.AppendAction("Text", new Action<DropdownMenuAction>(x => TextLine()));
            Menu.menu.AppendAction("Image", new Action<DropdownMenuAction>(x => ImagePic()));
            Menu.menu.AppendAction("Name", new Action<DropdownMenuAction>(x => CharacterName()));

            titleButtonContainer.Add(Menu);
        }

        // Port -----------------------------------------------------------------------------------------------

        public Port AddChoicePort(BaseNode baseNode, DialogueData_Port dialogueData_Port=null)
        {
            Port port = GetPortInstance(Direction.Output);
            DialogueData_Port newDialogue_Port = new DialogueData_Port();

            //Check if we load it in with values
            if(dialogueData_Port!=null)
            {
                newDialogue_Port.InputGuid = dialogueData_Port.InputGuid;
                newDialogue_Port.OutputGuid = dialogueData_Port.OutputGuid;
                newDialogue_Port.PortGuid = dialogueData_Port.PortGuid;
            }
            else
            {
                newDialogue_Port.PortGuid = Guid.NewGuid().ToString();
            }

            //Delete button
            {
                Button deleteButton = new Button(() => DeletePort(baseNode, port))
                {
                    text = "X",
                };
                port.contentContainer.Add(deleteButton);
            }

            port.portName = newDialogue_Port.PortGuid;                         //We use portName as port ID
            Label portNameLabel = port.contentContainer.Q<Label>("type");      //Get Label in port that is used to contain the port name.
            portNameLabel.AddToClassList("PortName");                          //Here we add a uss class to it so we can hide it in the editor window.

            //Set color of the port.
            port.portColor = Color.yellow;

            DialogueData.DialogueData_Ports.Add(newDialogue_Port);

            baseNode.outputContainer.Add(port);

            //Refresh
            baseNode.RefreshPorts();
            baseNode.RefreshExpandedState();

            return port;
        }

        private void DeletePort(BaseNode node,Port port)
        {
            DialogueData_Port tmp = DialogueData.DialogueData_Ports.Find(findPort => findPort.PortGuid == port.portName);
            DialogueData.DialogueData_Ports.Remove(tmp);

            IEnumerable<Edge> portEdge = graphView.edges.ToList().Where(edge => edge.output == port);

            if(portEdge.Any())
            {
                Edge edge = portEdge.First();
                edge.input.Disconnect(edge);
                edge.output.Disconnect(edge);
                graphView.RemoveElement(edge);
            }

            node.outputContainer.Remove(port);

            //Refresh
            node.RefreshPorts();
            node.RefreshExpandedState();
        }

        //Menu dropdown ------------------------------------------------------------------------------------------------------------

        public void TextLine(DialogueData_Text data_Text=null)
        {
            //DialogueData_Text newDialogueBaseContainer_Text = new DialogueData_Text();
            //DialogueData.DialogueData_Text = newDialogueBaseContainer_Text;
            //DialogueData.dialogueData_BaseContainers.Add(newDialogueBaseContainer_Text);

            

            //Load in data if it got any
            if(data_Text!=null)
            {
                //Guid ID
                dialogueData.DialogueData_Text.GuidID = data_Text.GuidID;

                //Text
                foreach(LanguageGeneric<string> data_text in data_Text.Text)
                {
                    foreach(LanguageGeneric<string> text in dialogueData.DialogueData_Text.Text)
                    {
                        if(text.languageType==data_text.languageType)
                        {
                            text.LanguageGenericType = data_text.LanguageGenericType;
                        }
                    }
                }

                //Audio
                foreach(LanguageGeneric<AudioClip> data_audioClip in data_Text.AudioClips)
                {
                    foreach(LanguageGeneric<AudioClip> audioClip in dialogueData.DialogueData_Text.AudioClips)
                    {
                        if(audioClip.languageType==data_audioClip.languageType)
                        {
                            audioClip.LanguageGenericType = data_audioClip.LanguageGenericType;
                        }
                    }
                }

                ReloadLanguage();
                
                return;
            }
            else
            {
                //Make New Guid ID
                dialogueData.DialogueData_Text.GuidID.Value = Guid.NewGuid().ToString();
            }

            //Add Container Box
            Box boxContainer = new Box();
            boxContainer.AddToClassList("DialogueBox");

            //Add Fields
            AddLabelAndButton(dialogueData.DialogueData_Text, boxContainer, "Text", "TextColor");
            AddTextField(dialogueData.DialogueData_Text, boxContainer);
            AddAudioClips(dialogueData.DialogueData_Text, boxContainer);

            //Reaload the current selected language
            ReloadLanguage();

            mainContainer.Add(boxContainer);
        }

        public void ImagePic(DialogueData_Images data_Images=null)
        {
            //DialogueData_Images dialogueData_Images = new DialogueData_Images();
            if(data_Images!=null)
            {
                DialogueData.DialogueData_Image.Sprite_Left.Value = data_Images.Sprite_Left.Value;
                DialogueData.DialogueData_Image.Sprite_Right.Value = data_Images.Sprite_Right.Value;
                DialogueData.DialogueData_Image.LeftField.SetValueWithoutNotify(data_Images.Sprite_Left.Value);
                DialogueData.DialogueData_Image.RightField.SetValueWithoutNotify(data_Images.Sprite_Right.Value);
                DialogueData.DialogueData_Image.LeftImage.image = data_Images.Sprite_Left.Value.texture;
                DialogueData.DialogueData_Image.RightImage.image = data_Images.Sprite_Right.Value.texture;
                return;
            }
            //DialogueData.dialogueData_BaseContainers.Add(dialogueData_Images);
            //DialogueData.DialogueData_Image = dialogueData_Images;

            Box boxContainer = new Box();
            boxContainer.AddToClassList("DialogueBox");

            AddLabelAndButton(DialogueData.DialogueData_Image, boxContainer, "Image", "ImageColor");
            AddImages(DialogueData.DialogueData_Image, boxContainer);

            mainContainer.Add(boxContainer);
        }

        public void CharacterName(DialogueData_Name data_Name=null)
        {
            //DialogueData_Name dialogue_Name = new DialogueData_Name();
            if(data_Name!=null)
            {
                DialogueData.DialogueData_Name.CharacterName.Value = data_Name.CharacterName.Value;
                DialogueData.DialogueData_Name.TextField.SetValueWithoutNotify(data_Name.CharacterName.Value);
                //dialogueData.DialogueData_Name.TextField.value = data_Name.CharacterName.Value;
                return;
            }
            //DialogueData.dialogueData_BaseContainers.Add(dialogue_Name);
            //DialogueData.DialogueData_Name = dialogue_Name;

            Box boxContainer = new Box();
            boxContainer.AddToClassList("CharacterNameBox");

            AddLabelAndButton(DialogueData.DialogueData_Name, boxContainer,"Name", "NameColor");
            AddTextField_CharacterName(DialogueData.DialogueData_Name, boxContainer);

            mainContainer.Add(boxContainer);
        }

        // Fields --------------------------------------------------------------------------------------------------------

        private void AddLabelAndButton(DialogueData_BaseContainer container,Box boxContainer,string labelName,string uniqueUSS="")
        {
            Box topBoxContainer = new Box();
            topBoxContainer.AddToClassList("TopBox");

            //Label Name
            Label label_texts = GetNewLabel(labelName, "LabelText", uniqueUSS);

            //Remove button.
            /*Button btn = GetNewButton("X", "TextBtn");
            btn.clicked += () =>
              {
                  DeleteBox(boxContainer);
                  DialogueData.dialogueData_BaseContainers.Remove(container);
              };*/

            topBoxContainer.Add(label_texts);
            //topBoxContainer.Add(btn);

            boxContainer.Add(topBoxContainer);
        }

        private void AddTextField_CharacterName(DialogueData_Name container,Box boxContainer)
        {
            TextField textField = GetNewTextField(container.CharacterName, "Name", "CharacterName");

            container.TextField = textField;

            boxContainer.Add(textField);
        }

        private void AddTextField(DialogueData_Text container,Box boxContainer)
        {
            TextField textField = GetNewTextField_TextLanguage(container.Text, "Text areans", "TextBox");

            container.TextField = textField;

            boxContainer.Add(textField);
        }

        private void AddAudioClips(DialogueData_Text container,Box boxContainer)
        {
            ObjectField objectField = GetNewObjectField_AudioClipsLanguage(container.AudioClips, "AudioClip");

            container.ObjectField = objectField;

            boxContainer.Add(objectField);
        }

        private void AddImages(DialogueData_Images container,Box boxContainer)
        {
            Box ImagePreviewBox = new Box();
            Box ImagesBox = new Box();

            ImagePreviewBox.AddToClassList("BoxRow");
            ImagesBox.AddToClassList("BoxRow");

            //Set up Image Preview.
             container.LeftImage = GetNewImage("ImagePreview", "ImagePreviewLeft");
             container.RightImage= GetNewImage("ImagePreview", "ImagePreviewRight");

            ImagePreviewBox.Add(container.LeftImage);
            ImagePreviewBox.Add(container.RightImage);

            //Set up Sprite.
            container.LeftField = GetNewObjectField_Sprite(container.Sprite_Left, container.LeftImage, "SpriteLeft");
            container.RightField = GetNewObjectField_Sprite(container.Sprite_Right, container.RightImage, "SpriteRight");

            ImagesBox.Add(container.LeftField);
            ImagesBox.Add(container.RightField);

            //Add to box container.
            boxContainer.Add(ImagePreviewBox);
            boxContainer.Add(ImagesBox);
        }

        // --------------------------------------------------------------------------------------------------------------

        public override void ReloadLanguage()
        {
            base.ReloadLanguage();
        }

        public override void LoadValueInToField()
        {
            
        }
    }

    public enum DialogueFaceImageType
    {
        Left,
        Right
    }
}
