using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace StringUtilities
{
    public partial class MainWindow : Window
    {
        private const string RuleFile = @"D:\code\blocklist\rules.txt";
        private const string NewLine = "\r\n";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnMerge_OnClick(object sender, RoutedEventArgs e)
        {
            BtnMerge.IsEnabled = false;
            BtnCommon.IsEnabled = false;

            txt3.Clear();
            //var lines1 = GetLines(txt1);
            //var lines2 = GetLines(txt2);

            var lines1 = txt1.Text.Split(new[] {NewLine}, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).ToHashSet();
            var lines2 = txt2.Text.Split(new[] {NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).ToHashSet();

            lines1.UnionWith(lines2);

            var sb = new StringBuilder();
            foreach (var line in lines1)
            {
                sb.Append(line);
                sb.Append(NewLine);
            }

            if (sb.Length >= 2)
            {
                sb.Length--;
                sb.Length--;
            }

            txt3.Text = sb.ToString();
            BtnMerge.IsEnabled = true;
            BtnCommon.IsEnabled = true;
        }

        private void BtnCommon_OnClick(object sender, RoutedEventArgs e)
        {
            BtnMerge.IsEnabled = false;
            BtnCommon.IsEnabled = false;

            txt3.Clear();
            //var lines1 = GetLines(txt1);
            //var lines2 = GetLines(txt2);

            var lines1 = txt1.Text.Split(new[] { NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).ToHashSet();
            var lines2 = txt2.Text.Split(new[] { NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).ToHashSet();

            lines1.IntersectWith(lines2);

            var sb = new StringBuilder();
            foreach (var line in lines1)
            {
                sb.Append(line);
                sb.Append(NewLine);
            }

            if (sb.Length >= 2)
            {
                sb.Length--;
                sb.Length--;
            }

            txt3.Text = sb.ToString();
            BtnMerge.IsEnabled = true;
            BtnCommon.IsEnabled = true;
        }

        private void BtnExcept_OnClick(object sender, RoutedEventArgs e)
        {
            BtnMerge.IsEnabled = false;
            BtnCommon.IsEnabled = false;

            txt3.Clear();
            //var lines1 = GetLines(txt1);
            //var lines2 = GetLines(txt2);

            var lines1 = txt1.Text.Split(new[] { NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).ToHashSet();
            var lines2 = txt2.Text.Split(new[] { NewLine}, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).ToHashSet();

            lines1.ExceptWith(lines2);

            var sb = new StringBuilder();
            foreach (var line in lines1)
            {
                sb.Append(line);
                sb.Append(NewLine);
            }

            if (sb.Length >= 2)
            {
                sb.Length--;
                sb.Length--;
            }

            txt3.Text = sb.ToString();
            BtnMerge.IsEnabled = true;
            BtnCommon.IsEnabled = true;
        }

        private void BtnExchange_OnClick(object sender, RoutedEventArgs e)
        {
            var temp = txt1.Text;
            txt1.Text = txt2.Text;
            txt2.Text = temp;
        }

        private void BtnSort_OnClick(object sender, RoutedEventArgs e)
        {
            var lines = txt3.Text.Split(new[] { NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).ToList();
            lines.Sort();

            var sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.Append(line);
                sb.Append(NewLine);
            }

            if (sb.Length >= 2)
            {
                sb.Length--;
                sb.Length--;
            }

            txt3.Text = sb.ToString();
        }

        private void Txt_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = e.OriginalSource as TextBox;
            var tb = (TextBlock)FindName("tb" + txt.Name[^1]);
            tb.Text = txt.LineCount + " 条记录";
        }

        private void BtnToLeft_OnClick(object sender, RoutedEventArgs e)
        {
            txt1.Text = txt3.Text;
            txt2.Clear();
            txt3.Clear();
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var all = await File.ReadAllTextAsync(RuleFile);
            txt1.Text = all;
            lbl.Content = RuleFile;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(RuleFile,txt1.Text);
        }

        private void BtnOpenDir_OnClick(object sender, RoutedEventArgs e)
        {
            var dir = Path.GetDirectoryName(RuleFile);
            Process.Start("explorer.exe", dir);
        }
    }
}
