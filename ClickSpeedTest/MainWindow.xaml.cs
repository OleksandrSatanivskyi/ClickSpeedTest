﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ClickSpeedTest.SymbolCollections;
using SymbolCollections.SymbolCollections;

namespace ClickSpeedTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer Timer { get; set; }
        private bool TestIsStarted => btStart.Content.ToString() != "Start";
        private Button ChangedButton { get; set; }
        private Brush ChangedButtonStartColor { get; set; }
        private Brush ShiftButtonStartColor { get; set; }
        private Dictionary<char, string> SpecialSymbols { get; set; }
        private int MistakesCount { get; set; }
        private int EnteredSymbolsCount { get; set; }
        private int Seconds { get; set; }
        private SymbolCollection SymbolCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ShiftButtonStartColor = btShift.Background;
            DirectoryInfo curDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            tbString.Text = File.ReadAllText(curDir.Parent.Parent.Parent.FullName + @"/Texts/Hancock.txt");
            SpecialSymbols = new Dictionary<char, string>();
            AddSpecialSymbolsToCollection();
            MistakesCount = 0;
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            EnteredSymbolsCount = 0;
            ChangeCheckboxesEnabling();
            SymbolCollection = new StartSymbolCollection();
        }

        private void ChangeCheckboxesEnabling() 
        {
            AllowLowerCaseLetters.IsChecked= false;
            AllowUpperCaseLetters.IsChecked = false;
            AllowNumbers.IsChecked = false;
            AllowSpecialSymbols.IsChecked = false;

            AllowLowerCaseLetters.IsEnabled = !AllowLowerCaseLetters.IsEnabled;
            AllowUpperCaseLetters.IsEnabled = !AllowUpperCaseLetters.IsEnabled;
            AllowNumbers.IsEnabled = !AllowNumbers.IsEnabled;
            AllowSpecialSymbols.IsEnabled = !AllowSpecialSymbols.IsEnabled;
        }

        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (TestIsStarted)
            {
                if (e.Text == tbString.Text[0].ToString())
                {
                    tbString.Text = tbString.Text.Remove(0, 1);
                    EnteredSymbolsCount++;
                }
                    
                else
                {
                    MistakesCount++;
                    lbPrecision.Content = (Math.Round(100 - (double)((double)MistakesCount * (double)100 / (double)tbString.Text.Length), 2)).ToString() + '%';
                }
            }  
        }

        private void AddSpecialSymbolsToCollection()
        {
            SpecialSymbols.Add('!', "1");
            SpecialSymbols.Add('@', "2");
            SpecialSymbols.Add('#', "3");
            SpecialSymbols.Add('$', "4");
            SpecialSymbols.Add('%', "5");
            SpecialSymbols.Add('^', "6");
            SpecialSymbols.Add('&', "7");
            SpecialSymbols.Add('*', "8");
            SpecialSymbols.Add('(', "9");
            SpecialSymbols.Add(')', "0");
            SpecialSymbols.Add('_', "-");
            SpecialSymbols.Add('+', "=");
            SpecialSymbols.Add('{', "[");
            SpecialSymbols.Add('}', "]");
            SpecialSymbols.Add(':', ";");
            SpecialSymbols.Add('"', "'");
            SpecialSymbols.Add('|', "\\");
            SpecialSymbols.Add('<', ",");
            SpecialSymbols.Add('>', ".");
            SpecialSymbols.Add('?', "/");
        }

        private void DisplayCorrectButton()
        {
            SetStartColorInClickedButton();
            SetWhiteBackgroungInCorrectButton();
        }

        private void SetStartColorInClickedButton()
        {
            if(ChangedButton != null)
            ChangedButton.Background = ChangedButtonStartColor;

            btShift.Background = ShiftButtonStartColor;
        }

        private void SetWhiteBackgroungInCorrectButton()
        {
            if (CurrentSymbolIsNumber())
            {
                foreach (Button button in ((Grid)Keyboard.Children[0]).Children)
                {
                    if (tbString.Text[0].ToString() == button.Content.ToString())
                    {
                        MakeCorrectButtonWhite(button);
                        return;
                    }
                }
            }
            else if (CurrentSymbolIsLetterInLowerCase() || CurrentSymbolIsSpecialSymbolInLowerCase())
            {
                foreach (Grid grid in Keyboard.Children)
                {
                    foreach (Button button in grid.Children)
                    {
                        if (tbString.Text[0].ToString() == button.Content.ToString())
                        {
                            MakeCorrectButtonWhite(button);
                            return;
                        }
                    }
                }
            }
            else if (CurrentSymbolIsLetterInUpperCase())
            {
                foreach (Grid grid in Keyboard.Children)
                {
                    foreach (Button button in grid.Children)
                    {
                        if (tbString.Text[0].ToString().ToLower() == button.Content.ToString())
                        {
                            MakeCorrectButtonWhite(button);
                            btShift.Background = Brushes.White;
                            return;
                        }
                    }
                }
            }
            else if (tbString.Text[0] == ' ')
            {
                foreach (Button button in ((Grid)Keyboard.Children[4]).Children)
                {
                    if (button.Content.ToString() == "Space")
                    {
                        MakeCorrectButtonWhite(button);
                        return;
                    }
                       
                }
            }
            else
            {
                foreach (var item in SpecialSymbols)
                {
                    if (item.Key == tbString.Text[0])
                    {
                        foreach (Grid grid in Keyboard.Children)
                        {
                            foreach (Button button in grid.Children)
                            {
                                if (item.Value == button.Content.ToString())
                                {
                                    MakeCorrectButtonWhite(button);
                                    btShift.Background = Brushes.White;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool CurrentSymbolIsLetterInUpperCase()
            => (int)tbString.Text[0] >= 65 && (int)tbString.Text[0] <= 90;

        private bool CurrentSymbolIsSpecialSymbolInLowerCase()
            => (tbString.Text[0] == ',' ||
            tbString.Text[0] == '.' ||
            tbString.Text[0] == '.' ||
            tbString.Text[0] == '/' ||
            tbString.Text[0] == ';' ||
            tbString.Text[0] == '\'' ||
            tbString.Text[0] == '[' ||
            tbString.Text[0] == ']' ||
            tbString.Text[0] == '=' ||
            tbString.Text[0] == '-' ||
            tbString.Text[0] == '`');

        private bool CurrentSymbolIsLetterInLowerCase()
           => (int)tbString.Text[0] >= 97 && (int)tbString.Text[0] <= 122;

        private void MakeCorrectButtonWhite(Button button)
        {
            ChangedButtonStartColor = button.Background;
            ChangedButton = button;
            button.Background = Brushes.White;
        }

        private bool CurrentSymbolIsNumber() => (int)tbString.Text[0] >= 48 && (int)tbString.Text[0] <= 57;

 
        private void btStart_Click(object sender, RoutedEventArgs e)
        {
            if (TestIsStarted)
            {
                btStart.Content = "Start";
                btStart.Background = Brushes.Green;
                MistakesCount = 0;
                lbPrecision.Content = 100.ToString() + '%';
                Timer.Stop();
                new ResultWindow().ShowDialog();
            }
            else if(!TestIsStarted)
            {
                btStart.Content = "Finish";
                btStart.Background = Brushes.Red;
                Timer.Start();
                EnteredSymbolsCount = 0;
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
           Seconds++;
           lbSpeed.Content = (EnteredSymbolsCount / Seconds).ToString();
        }

        private void tbString_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbString.Text.Length > 0)
            DisplayCorrectButton();
        }

        private void ChangeModeButton_Click(object sender, RoutedEventArgs e)
        {
            if (lbMode.Content.ToString() == "Text")
            {
                lbMode.Content = "Constructor";
            }
            else if (lbMode.Content.ToString() == "Constructor")
            {
                lbMode.Content = "Text";
                LoadTextTotbString();
            }

            ChangeCheckboxesEnabling();
            tbString.Text = "";
        }

        private void LoadTextTotbString()
        {
            DirectoryInfo curDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            string path = curDir.Parent.Parent.Parent.FullName + @"/Texts";
            var files = Directory.GetFiles(path);

            Random random= new Random();
            
            using(FileStream fs = new FileStream(files[random.Next(0, files.Length)], FileMode.Open))
                using(StreamReader sr = new StreamReader(fs))
                tbString.Text = sr.ReadToEnd();
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            SymbolCollection = new StartSymbolCollection();
            if ((bool)AllowLowerCaseLetters.IsChecked)
                SymbolCollection.SetPrimaryCollection(new LowerCaseEnglishLettersCollection());
            if ((bool)AllowUpperCaseLetters.IsChecked)
                SymbolCollection.SetPrimaryCollection(new UpperCaseEnglishLettersCollection());
            if ((bool)AllowNumbers.IsChecked)
                SymbolCollection.SetPrimaryCollection(new NumbersCollection());
            if ((bool)AllowSpecialSymbols.IsChecked)
                SymbolCollection.SetPrimaryCollection(new AdditionalSymbolsCollection());

            SymbolCollection.CreateSymbols();

            CreateRandomSymbolsStringFortbString();
        }

        private void CreateRandomSymbolsStringFortbString()
        {
            if (SymbolCollection.Symbols.Length <=0)
                return;
            Random random = new Random();
            for (int i = 0; i < 200; i++)
            {
                tbString.Text += SymbolCollection.GetRandomSymbol();
            }
        }
    }
}
