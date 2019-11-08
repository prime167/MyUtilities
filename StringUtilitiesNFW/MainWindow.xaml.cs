using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MyUtilities
{
    public partial class MainWindow : Window
    {
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

            var lines1 = txt1.Text.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            var lines2 = txt2.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToHashSet();

            lines1.UnionWith(lines2);

            var sb = new StringBuilder();
            foreach (var line in lines1)
            {
                sb.Append(line);
                sb.Append("\r\n");
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

            var lines1 = txt1.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            var lines2 = txt2.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToHashSet();

            lines1.IntersectWith(lines2);

            var sb = new StringBuilder();
            foreach (var line in lines1)
            {
                sb.Append(line);
                sb.Append("\r\n");
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

            var lines1 = txt1.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            var lines2 = txt2.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToHashSet();

            lines1.ExceptWith(lines2);

            var sb = new StringBuilder();
            foreach (var line in lines1)
            {
                sb.Append(line);
                sb.Append("\r\n");
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
            var lines = txt3.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            lines.Sort();

            var sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.Append(line);
                sb.Append("\r\n");
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
            var tb = (TextBlock)FindName("tb" + txt.Name[txt.Name.Length - 1]);
            tb.Text = txt.LineCount + " 条记录";
        }
    }
}
