﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuneLab.Base.Event;
using TuneLab.Base.Properties;

namespace TuneLab_Extensions.GUI.Components;

internal class ToggleContent
{
    public required IItem Item { get; set; }
    public ColorSet UncheckedColorSet;
    public ColorSet CheckedColorSet;
}

internal class Toggle : Button, IDataValueController<bool>
{
    public event Func<bool>? AllowSwitch;
    public IActionEvent Switched => mValueChanged;
    public bool IsChecked { get; private set; }

    public IActionEvent ValueWillChange => mValueWillChange;
    public IActionEvent ValueChanged => mValueChanged;
    public IActionEvent ValueCommited => mValueCommited;
    public bool Value => IsChecked;

    public Toggle()
    {
        Pressed += () =>
        {
            if (AllowSwitch != null && !AllowSwitch())
                return;

            mValueWillChange.Invoke();
            IsChecked = !IsChecked;
            mValueChanged.Invoke();
            mValueCommited.Invoke();
            foreach (var kvp in mContentMap)
            {
                kvp.Value.ColorSet = IsChecked ? kvp.Key.CheckedColorSet : kvp.Key.UncheckedColorSet;
            }
        };
    }

    public Toggle AddContent(ToggleContent content)
    {
        var buttonContent = new ButtonContent() { Item = content.Item, ColorSet = IsChecked ? content.CheckedColorSet : content.UncheckedColorSet };
        mContentMap.Add(content, buttonContent);
        AddContent(buttonContent);
        return this;
    }

    public void Display(bool value)
    {
        IsChecked = value;
        foreach (var kvp in mContentMap)
        {
            kvp.Value.ColorSet = IsChecked ? kvp.Key.CheckedColorSet : kvp.Key.UncheckedColorSet;
        }
        CorrectColor();
    }

    readonly ActionEvent mValueWillChange = new();
    readonly ActionEvent mValueChanged = new();
    readonly ActionEvent mValueCommited = new();
    Dictionary<ToggleContent, ButtonContent> mContentMap = new();
}
