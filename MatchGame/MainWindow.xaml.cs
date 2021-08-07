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
        private int emojiCount = 0;

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
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondElapsed++;
            timeTextBlock.Text = (tenthsOfSecondElapsed / 10F).ToString("0.0s");

            if ((matchesFound * 2) == emojiCount)
            {
                timer.Stop();
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
                textBlock.Text = "❓";
                findingMatch = false;
            }
            else
            {
                // match not found
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void SetupGame(object sender, RoutedEventArgs e)
        {
            resetGame();

            Button button = sender as Button;
            button.Content = "Restart Game";
            List<string> emojisClone = new List<string>(emojis);
            emojisClone = emojisClone.Take(emojiCount).ToList();

            Random random = new Random();

            foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
            {
                if ((textblock.Tag as string) != "EmojiBlock")
                {
                    continue;
                }
                
                if (emojisClone.Count > 0)
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

        private void changeEmojiCount(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            emojiCount = (int)slider.Value;
        }

        private void resetGame()
        {
            tenthsOfSecondElapsed = 0;
            matchesFound = 0;

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if ((textBlock.Tag as string) == "EmojiBlock")
                {
                    textBlock.Text = "";
                }
            }
        }
    }
}
