﻿using UGUIRuntime;
using UnityEngine;

public class SpriteTest : MonoBehaviour
{
    private void Awake()
    {
        UGUI.Create();
        Http.Create();
        Build();
        // SimpleBuild();
    }

    public void SimpleBuild()
    {
        // set host
        UGUI.SetHost("http://39.105.150.229:8741/psyhack_img/");

        SimpleBuilder.CreateRoot();

        SimpleBuilder.indentLevel = 0;
        SimpleBuilder.CreateBackground();
        SimpleBuilder.CreateText("简单构建UI");
        SimpleBuilder.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });

        SimpleBuilder.indentLevel = 1;
        SimpleBuilder.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });
        SimpleBuilder.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });
        SimpleBuilder.indentLevel = 2;
        SimpleBuilder.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });

        SimpleBuilder.indentLevel = 0;
        SimpleBuilder.CreateSlider("滑动条功能", 0.2f, (val) => { Debug.Log("滑动条: " + val); });

        var options = new string[] { "选项1", "选项2", "选项3", "选项4", "选项5", "选项6" };
        SimpleBuilder.CreateDropdown("选项卡功能", 1, options, (index) => { Debug.Log(options[index]); });
    }

    public void Build()
    {
        // set host
        UGUI.SetHost("http://39.105.150.229:8741/psyhack_img/");

        // add ui root
        var ui = UGUI.CreateUIRoot();
        ui.SetSize(Vector2.one * 400);

        // test image (backgroud)
        var background = ui.AddImage("background")
            .SetSize(500, 500)
            .SetSprite("rect", 10)
            .SetColor(new Color32(85, 95, 115, 255));

        // test button
        var button = ui.AddButton("button")
            //.SetPosition(30, 40).SetSize(300, 100)
            .SetSprite("rectr", 10)
            .SetText("测试按钮")
            .SetT()
            .SetColor(Color.gray)
            .SetListener(() =>
            {
                Debug.Log("Button click");
            });

        // test text
        var text = ui.AddText()
            .SetPosition(250, 120).SetSize(100, 50)
            .SetFont(35).SetText("哈哈哈");

        // test toggle
        var toggle = ui.AddToggle()
            .SetPosition(30, 200).SetSize(44, 44)
            .SetText("测试Toggle")
            .SetListener(isOn =>
            {
                Debug.Log("Toggle: " + isOn);
            });

        toggle.GetBackground().SetSprite("radiobutton");
        toggle.GetCheckmark().SetSize(30, 30).SetSprite("circle").SetColor(Color.black);


        // test checkbox
        var checkbox = ui.AddToggle()
            .SetPosition(30, 250).SetSize(44, 44)
            .SetText("测试Checkbox")
            .SetListener(isOn =>
            {
                Debug.Log("checkbox: " + isOn);
            });

        checkbox.GetBackground().SetSprite("checkbox");
        checkbox.GetCheckmark().SetSize(30, 30).SetSprite("checked").SetColor(Color.black);

        // test switch
        var _switch = ui.AddSwitch()
            .SetPosition(30, 300).SetSize(88, 44)
            .SetKnobPosition(-18, 18)
            .SetListener(isOn =>
            {
                Debug.Log("switch: " + isOn);
            });

        _switch.GetBackground().SetSprite("circle", 20);
        _switch.GetCheckmark().SetSize(72, 34).SetSprite("circle", 20).SetColor(Color.green * 0.6f);
        _switch.GetKnob().SetSize(30, 30).SetSprite("circle").SetColor(Color.gray);


        // test slider
        var slider = ui.AddSlider()
            .SetPosition(30, 350).SetSize(200, 20)
            .SetMargin(15)
            .SetListener((val) =>
            {
                Debug.Log("slider: " + val);
            });

        slider.GetBackground().SetSprite("rectr", 10);
        slider.GetFill().SetSprite("rectr", 10).SetColor(Color.gray);
        slider.GetHandle().SetSprite("circle").SetSize(30, 30);


        // test dropdown
        var options = new string[] { "test 1", "中文测试 2", "dd", "aa", "bb", "zz" };
        var dropdown = ui.AddDropdown()
            .SetPosition(30, 400).SetSize(300, 50)
            .SetOptions(options)
            .SetDropdownHeight(160)
            .SetItemHeight(40)
            .SetTextMargin(12)
            .SetListener((val) =>
            {
                Debug.Log("dropdown: " + options[val]);
            });

        dropdown.GetBackground().SetSprite("rect", 10);
        dropdown.GetLabel().SetFont(36).SetColor(Color.black);
        dropdown.GetDropdownBackground().SetSprite("rect", 10).SetColor(Color.white * 0.9f);
        dropdown.GetItemBackground().SetSprite("rect", 10);
        dropdown.GetItemCheckmark().SetSprite("rect", 10).SetColor(Color.blue);
        dropdown.GetItemLabel().SetFont(30).SetColor(Color.black);
    }
}
