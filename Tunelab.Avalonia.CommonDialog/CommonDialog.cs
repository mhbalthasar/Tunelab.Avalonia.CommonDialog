using Avalonia.Controls;
using Avalonia.Threading;
using ReactiveUI;
using System.Diagnostics;
using TuneLab_Extensions.GUI;
using TuneLab_Extensions.GUI.Components;
using TuneLab_Extensions.GUI.Controllers;
using TuneLab_Extensions.Utils;

namespace TuneLab.CommonDialog
{
    public class CommonDialog
    {
        public class ButtonAction
        {
            public enum ButtonType
            {
                Primary,
                Normal
            }

            public string Text { get; set; } = "";
            public ButtonType Type { get; set; } = ButtonType.Primary;
            public Action? Action { get; set; } = () => { };
        }
        public static void MessageBox(string title,string message, ButtonAction[] buttons)
        {
            var dialog = new Dialog();
            dialog.SetTitle(title);
            dialog.SetMessage(message);
            foreach (var ba in buttons)
            {
                dialog.AddButton(ba.Text, (Dialog.ButtonType)ba.Type).Clicked += ba.Action;
            }
            if (dialog.ButtonsPanel.Children.Count == 0) dialog.VisibleButtons(false);
            dialog.Show();
        }
        public static void MarkdownBox(string title, string message, string markdownMessage, ButtonAction[] buttons)
        {
            MarkdownDialog dialog = new MarkdownDialog();
            dialog.SetTitle(title);
            if (message != "") dialog.SetMessage(message);
            if (markdownMessage != "") dialog.SetMDMessage(markdownMessage);
            foreach (var ba in buttons)
            {
                dialog.AddButton(ba.Text, (MarkdownDialog.ButtonType)ba.Type).Clicked += ba.Action;
            }
            dialog.Show();
        }
        public static void PromptText(string title, string message, string defaultValue, Action<string> callback)
        {
            var dialog = new FreeDialog();
            dialog.SetTitle(title);
            dialog.SetMessage(message);
            dialog.ResizeMessageWindow(500, 18, new Avalonia.Thickness(0, 12, 0, 0));
            TextInput input = new TextInput();
            input.Margin = new Avalonia.Thickness(12);
            input.Height = 40;
            input.Width = 400;
            input.Text=defaultValue;
            dialog.FreeStackPanel.Children.Add(input);
            dialog.AddButton("OK", FreeDialog.ButtonType.Primary).Clicked += ()=>{
                callback(input.Value);
            };
            dialog.AddButton("Cancel", FreeDialog.ButtonType.Primary);
            dialog.Show();
        }
        public static void Progress(string title, string message, ButtonAction[] buttons, Func<double> tiggerProcess, Func<string>? tiggerMessage = null)
        {
            var dialog = new FreeDialog();
            dialog.SetTitle(title);
            dialog.SetMessage(message);
            dialog.ResizeMessageWindow(500, 18, new Avalonia.Thickness(0, 12, 0, 0));
            ProgressBar pg = new ProgressBar();
            pg.Margin = new Avalonia.Thickness(12);
            pg.Height = 40;
            pg.Width = 400;
            pg.ShowProgressText = true;
            pg.Maximum = 100;
            pg.Minimum = 0;
            pg.Value = 0;
            dialog.FreeStackPanel.Children.Add(pg);
            var task=Task.Run(() => {
                double val = 0;
                while (val < 100)
                {
                    val = tiggerProcess();
                    if (val >= 100)
                    {
                        Dispatcher.UIThread.Post(() =>
                        {
                            dialog.Close();
                        });
                    }
                    else
                    {
                        string? newMsg = null;
                        if (tiggerMessage != null)
                        {
                            newMsg = tiggerMessage();
                        }
                        Dispatcher.UIThread.Post(() =>
                        {
                            pg.Value = val;
                            if (newMsg != null) dialog.SetMessage(newMsg);
                        });
                    }
                    Task.Delay(100).Wait();
                }
            });
            foreach (var ba in buttons)
            {
                dialog.AddButton(ba.Text, (FreeDialog.ButtonType)ba.Type).Clicked += ba.Action;
            }
            if (dialog.ButtonsPanel.Children.Count == 0) dialog.VisibleButtons(false);
            dialog.Show();
        }
        public static void PromptPath(string title, string message, Action<string> callback)
        {
            var dialog = new FreeDialog();
            dialog.SetTitle(title);
            dialog.SetMessage(message);
            dialog.ResizeMessageWindow(500, 18, new Avalonia.Thickness(0, 12, 0, 0));
            PathInput input = new PathInput();
            input.Margin = new Avalonia.Thickness(12);
            input.Height = 40;
            input.Width = 400;
            dialog.FreeStackPanel.Children.Add(input);
            dialog.AddButton("OK", FreeDialog.ButtonType.Primary).Clicked += () => {
                callback(input.Value);
            };
            dialog.AddButton("Cancel", FreeDialog.ButtonType.Primary);
            dialog.Show();
        }
        public static void PromptList(string title, string message, object[] items, Action<int[]> callback,bool isSingleSelect=true)
        {
            var dialog = new FreeDialog();
            dialog.SetTitle(title);
            dialog.SetMessage(message);
            dialog.ResizeMessageWindow(500, 18, new Avalonia.Thickness(0, 12, 0, 0));
            dialog.ResizeFreeWindow(500, 230, new Avalonia.Thickness(0));
            dialog.SetHeight(360);
            ListBox list = new ListBox();
            list.Margin = new Avalonia.Thickness(12);
            list.Background = Style.BACK.ToBrush();
            list.SelectionMode = (isSingleSelect?SelectionMode.Single:SelectionMode.Multiple) | SelectionMode.Toggle;
            list.Height = 200;
            list.Width = 400;
            for (var i=0;i<items.Length;i++) list.Items.Add(new ListBoxItem() { 
                Content = items[i],
                FontSize = 12,
                Tag = i
            });
            dialog.FreeStackPanel.Children.Add(list);
            dialog.AddButton("OK", FreeDialog.ButtonType.Primary).Clicked += () => {
                if (isSingleSelect) { callback([list.SelectedIndex]); }
                else
                {
                    List<int> v = new List<int>();
                    if (list.SelectedItems != null) foreach (var iv in list.SelectedItems)
                        {
                            v.Add((int)(((ListBoxItem)iv).Tag));
                        }
                    callback(v.ToArray());
                }
            };
            dialog.AddButton("Cancel", FreeDialog.ButtonType.Primary);
            dialog.Show();
        }
    }
}
