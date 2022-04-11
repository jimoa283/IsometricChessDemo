using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KasperDev.DialogueEditor
{
    public class UpdateLanguageType             //如果语言种类和数量改变了，一定要使用这个类
    {
        public void UpdateLanguage()
        {
/*            List<DialogueContainerSO> dialogueContainers = Helper.FindAllDialogueContainerSO();

            foreach (DialogueContainerSO DialogueContainer in dialogueContainers)
            {
                foreach (DialogueNodeData nodeData in DialogueContainer.DialogueNodeDatas)
                {
                    nodeData.Textlanguages = UpdateLanguageGenerics(nodeData.Textlanguages);
                    nodeData.AudioClips = UpdateLanguageGenerics(nodeData.AudioClips);

                    foreach (DialogueNodePort nodePort in nodeData.DialogueNodePorts)
                    {
                        nodePort.TextLanguages = UpdateLanguageGenerics(nodePort.TextLanguages);
                    }
                }
            }*/
        }

/*        private List<LanguageGeneric<T>> UpdateLanguageGenerics<T>(List<LanguageGeneric<T>> languageGenerics)
        {
            List<LanguageGeneric<T>> tmp = new List<LanguageGeneric<T>>();

            foreach (LanguageType languageType in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
            {
                tmp.Add(new LanguageGeneric<T>
                {
                    languageType = languageType,
                });
            }

            foreach (LanguageGeneric<T> languageGeneric in languageGenerics)
            {
                if (tmp.Find(language => language.languageType == languageGeneric.languageType) != null)
                {
                    tmp.Find(language => language.languageType == languageGeneric.languageType).LanguageGenericType = languageGeneric.LanguageGenericType;
                }
            }

            return tmp;
        }*/
    }
}
