using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer timer = new DispatcherTimer();
        private int tenthsOfSecondElapsed = 0;
        private int matchesFound = 0;

        List<string> emojis = new List<String>()
            {
                "😊","😊",
                "😂","😂",
                "🤣","🤣",
                "❤","❤",
                "😍","😍",
                "😒","😒",
                "👌","👌",
                "😘","😘"
            };

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetupGame(emojis);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondElapsed++;
            timeTextBlock.Text = (tenthsOfSecondElapsed / 10F).ToString("0.0s");

            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play Again?";
            }
        }

        private void SetupGame(List<String> emojis)
        {
            List<string> emojisClone = new List<string>(emojis);

            Random random = new Random();

            foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textblock.Name == "timeTextBlock")
                {
                    continue;
                }
                else
                {
                    // generate randomly ordered emoji textblocks
                    int randomIndex = random.Next(emojisClone.Count);
                    string nextEmoji = emojisClone[randomIndex];
                    textblock.Text = nextEmoji;
                    emojisClone.RemoveAt(randomIndex);
                    textblock.Visibility = Visibility.Visible;
                }

                timer.Start();
            }
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            // found match!
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                // match not found
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                tenthsOfSecondElapsed = 0;
                matchesFound = 0;
                SetupGame(emojis);
            }
        }
    }
}
