using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System.IO;
using System.Linq;


public class ElementEditorWindow : EditorWindow
{

    private Button EquipmentButton;
    private Button SkillButton;
    private Button SaveButton;
    private ListView ElementListView;

    private Image PreviewImage;
    private VisualElement root;
    private IntegerField IDField;
    private TextField NameField;
    private ObjectField IconField;
    private TextField DescriptionField;
    private EnumField SkillTypeField;

    [Header("ItemGeneral")]
    private VisualElement ItemGeneralArea;
    private EnumField ItemTypeField;
    private IntegerField BuyPriceField;
    private IntegerField SellPriceField;

    [Header("EquipmentInfo")]
    private VisualElement EquipmentInfoArea;
    private IntegerField EquipmentPowerField;
    private IntegerField EquipmentHitRateField;
    private IntegerField EquipmentDefenseField;
    private IntegerField EquipmentSpeedField;
    private IntegerField EquipmentMagicField;
    private IntegerField EquipmentAvoidField;
    private IntegerField EquipmentMagicDefenseField;
    private IntegerField EquipmentVMoveField;
    private IntegerField EquipmentHMoveField;
    private IntegerField EquipmentLuckyField;
    private IntegerField EquipmentCriticalField;
    private IntegerField EquipmentFireResistanceField;
    private IntegerField EquipmentIceResistanceField;
    private IntegerField EquipmentWindResistanceField;
    private IntegerField EquipmentThunderResistanceField;
    private IntegerField EquipmentLightResistanceField;
    private IntegerField EquipmentDarkResistanceField;

    [Header("WeaponInfo")]
    private VisualElement WeaponInfoArea;
    private ObjectField WeaponActiveSkillField;

    [Header("BaseDataField")]
    private VisualElement BaseDataInfoArea;

    private EnumField ActionTypeField;
    private EnumField TargetBaseTypeField;

    private ElementSO currentElement;
    private List<ElementSO> currentElementList;

    [MenuItem("Window/UI Toolkit/ElementEditorWindow")]
    public static void ShowExample()
    {
        ElementEditorWindow wnd = GetWindow<ElementEditorWindow>();
        wnd.titleContent = new GUIContent("ElementEditorWindow");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        root = rootVisualElement;


        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/TestItemEditor/ElementEditorWindow.uxml");
        visualTree.CloneTree(root);

        EquipmentButton = root.Q<Button>("EquipmentButton");
        SkillButton = root.Q<Button>("SkillButton");
        SaveButton = root.Q<Button>("SaveButton");

        CreatePreviewImage();

        IconField = root.Q<ObjectField>("IconField") as ObjectField;
        IconField.objectType =typeof(Sprite);
        IconField.RegisterValueChangedCallback(value =>
        {
            PreviewImage.image = value != null ? (value.newValue as Sprite).texture : null;
        });

        IDField = root.Q<IntegerField>("IDField");
        NameField = root.Q<TextField>("NameField");
        DescriptionField = root.Q<TextField>("DescriptionField");

        SkillTypeField = root.Q<EnumField>("SkillTypeField");
        SkillTypeField.value = SkillType.Active;
        SkillTypeField.Init(SkillType.Active);

        // ItemGeneral
        ItemGeneralArea = root.Q<VisualElement>("ItemGeneralArea");
        ItemTypeField = root.Q<EnumField>("ItemTypeField");
        ItemTypeField.value = ItemType.Equipment;
        ItemTypeField.Init(ItemType.Equipment);
        BuyPriceField = root.Q<IntegerField>("BuyPriceField");
        SellPriceField = root.Q<IntegerField>("SellPriceField");

        // EquipmentInfo
        EquipmentInfoArea = root.Q<VisualElement>("EquipmentInfoArea");
        EquipmentPowerField = root.Q<IntegerField>("EquipmentPowerField");
        EquipmentHitRateField = root.Q<IntegerField>("EquipmentHitRateField");
        EquipmentDefenseField = root.Q<IntegerField>("EquipmentDefenseField");
        EquipmentSpeedField = root.Q<IntegerField>("EquipmentSpeedField");
        EquipmentMagicField = root.Q<IntegerField>("EquipmentMagicField");
        EquipmentAvoidField = root.Q<IntegerField>("EquipmentAvoidField");
        EquipmentMagicDefenseField = root.Q<IntegerField>("EquipmentMagicDefenseField");
        EquipmentVMoveField = root.Q<IntegerField>("EquipmentVMoveField");
        EquipmentHMoveField = root.Q<IntegerField>("EquipmentHMoveField");
        EquipmentLuckyField = root.Q<IntegerField>("EquipmentLuckyField");
        EquipmentCriticalField = root.Q<IntegerField>("EquipmentCriticalField");
        EquipmentFireResistanceField = root.Q<IntegerField>("EquipmentFireResistanceField");
        EquipmentIceResistanceField = root.Q<IntegerField>("EquipmentIceResistanceField");
        EquipmentWindResistanceField = root.Q<IntegerField>("EquipmentWindResistanceField");
        EquipmentThunderResistanceField = root.Q<IntegerField>("EquipmentThunderResistanceField");
        EquipmentLightResistanceField = root.Q<IntegerField>("EquipmentLightResistanceField");
        EquipmentDarkResistanceField = root.Q<IntegerField>("EquipmentDarkResistanceField");

        //WeaponInfo
        WeaponInfoArea = root.Q<VisualElement>("WeaponInfoArea");
        WeaponActiveSkillField = root.Q<ObjectField>("WeaponActiveSkillField");

        ActionTypeField = root.Q<EnumField>("ActionTypeField") ;
        ActionTypeField.value = ActionType.Skill;
        ActionTypeField.Init(ActionType.Skill);

        TargetBaseTypeField = root.Q<EnumField>("TargetBaseTypeField");
        TargetBaseTypeField.value = TargetBaseType.Cell;
        TargetBaseTypeField.Init(TargetBaseType.Cell);

        ItemGeneralArea.visible = false;
        EquipmentInfoArea.visible = false;
        WeaponInfoArea.visible = false;
    }

    private void CreatePreviewImage()
    {
        VisualElement preview = root.Q<VisualElement>("PreviewArea");
        PreviewImage = new Image();
        preview.Add(PreviewImage);
    }

    private void GetSelectItem<T>(string TypeString)
    {

        ElementListView.Clear();

        var guids = AssetDatabase.FindAssets("t:" + TypeString);
        currentElementList.Clear();
        for(int i=0;i<guids.Length;++i)
        {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            currentElementList.Add(AssetDatabase.LoadAssetAtPath<EquipmentSO>(path));
        }

        ElementListView.makeItem = () => new Label();
        ElementListView.bindItem = (element, i) => (element as Label).text = currentElementList[i].Name;
        ElementListView.itemsSource = currentElementList;
        ElementListView.itemHeight = 16;
        ElementListView.selectionType = SelectionType.Single;

        ElementListView.Refresh();
        ElementListView.selectedIndex = 0;

        ElementListView.onSelectionChange += (enumerable) =>
        {
            ElementSO element = null;
            foreach(var item in enumerable)
            {
                element = item as ElementSO;
            }

            currentElement = element;
            ShowElementInformation(element);
        };
    }

    private VisualElement GetListViewItem(ElementSO element)
    {
        VisualElement visualElement = new VisualElement();



        return null;
    }

    

    private void GetAllSkill()
    {

    }

    private void ShowElementInformation(ElementSO element)
    {
        IDField.value = element.ID;
        NameField.value = element.Name;
        IconField.value = element.Sprite;
        DescriptionField.value = element.Info;

        ItemGeneralArea.visible = false;
        EquipmentInfoArea.visible = false;
        WeaponInfoArea.visible = false;

        switch(element)
        {
            case ItemSO item:
                ShowItemInfo(item);
                break;
            default:
                break;
        }
    }

    private void ShowItemInfo(ItemSO item)
    {
        ItemGeneralArea.visible = true;

        ItemTypeField.value = item.ItemType;
        BuyPriceField.value = item.BuyPrice;
        SellPriceField.value = item.SellPrice;

        switch(item)
        {
          case  EquipmentSO equipment:
               ShowEquipmentInfo(equipment);
               break;
            default:
                break;
        }

    }

    private void ShowEquipmentInfo(EquipmentSO equipment)
    {
        EquipmentInfoArea.visible = true;

        EquipmentPowerField.value = equipment.Power;
        EquipmentHitRateField.value = equipment.HitRate;
        EquipmentDefenseField.value = equipment.Defense;
        EquipmentSpeedField.value = equipment.Speed;
        EquipmentMagicField.value = equipment.Magic;
        EquipmentAvoidField.value = equipment.Avoid;
        EquipmentMagicDefenseField.value = equipment.MagicDefense;
        EquipmentVMoveField.value = equipment.VMove;
        EquipmentHMoveField.value = equipment.HMove;
        EquipmentLuckyField.value = equipment.Lucky;
        EquipmentCriticalField.value = equipment.Critical;
        EquipmentFireResistanceField.value = equipment.FireResistance;
        EquipmentIceResistanceField.value = equipment.IceResistance;
        EquipmentWindResistanceField.value = equipment.WindResistance;
        EquipmentThunderResistanceField.value = equipment.ThunderResistance;
        EquipmentLightResistanceField.value = equipment.LightResistance;
        EquipmentDarkResistanceField.value = equipment.DarkResistance;
    }
}