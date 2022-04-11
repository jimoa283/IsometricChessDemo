using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KasperDev.DialogueEditor
{
    public class DialogueButtonContainer 
    {
        public UnityAction UnityAction { get; set; }
        public string Text { get; set; }
        public bool ConditionCheck { get; set; }
        public ChoiceStateType ChoiceState { get; set; }
    }
}

