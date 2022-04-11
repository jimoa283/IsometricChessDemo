﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;

namespace KasperDev.DialogueEditor
{
    public class BranchNode : BaseNode
    {
        private BranchData branchData = new BranchData();
        public BranchData BranchData { get => branchData; set => branchData = value; }

        public BranchNode(){}

        public BranchNode(Vector2 position,DialogueEditorWindow editorWindow,DialogueGraphView graphView)
        {
            base.editorWindow = editorWindow;
            base.graphView = graphView;

            StyleSheet styleSheet = Resources.Load<StyleSheet>("USS/Nodes/BranchNodeStyleSheet");
            styleSheets.Add(styleSheet);

            title = "Branch";                                     //Set Name;
            SetPosition(new Rect(position, defaultNodeSize));     //Set Position;
            nodeGuid = Guid.NewGuid().ToString();                 //Set ID;

            AddInputPort("Input", Port.Capacity.Multi);
            AddOutputPort("True", Port.Capacity.Single);
            AddOutputPort("False", Port.Capacity.Single);

            TopButton();
        }

        

        private void TopButton()
        {
            ToolbarMenu menu = new ToolbarMenu();
            menu.text = "Add Condition";

            menu.menu.AppendAction("String Event Condition", new Action<DropdownMenuAction>(x => AddCondition()));

            titleButtonContainer.Add(menu);
        }

        public void AddCondition(EventData_StringCondition stringEvent=null)
        {
            AddStringConditionEventBuild(branchData.EventData_StringConditions, stringEvent);
        }
    }
}


