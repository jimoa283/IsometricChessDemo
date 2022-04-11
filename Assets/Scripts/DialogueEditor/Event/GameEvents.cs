using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KasperDev.DialogueEditor
{

    public class GameEvents : MonoBehaviour
    {
        private event Action<int> randomColorModel;

        public static GameEvents Instance { get; private set; }

        public Action<int> RandomColorModel { get => randomColorModel; set => randomColorModel = value; }

        private void Awake()
        {
            if(Instance==null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void CallRandomColorModel(int num)
        {
            randomColorModel?.Invoke(num);
        }

        public virtual void DialogueModifierEvents(string stringEvent, StringEventModifierType stringEventModifierType, float value = 0)
        {

        }

        public virtual bool DialogueConditionEvents(string stringEvent, StringEventConditionType stringEventConditionType, float value = 0)
        {
            return false;
        }
    }
}
