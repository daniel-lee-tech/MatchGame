using System;
using System.Collections.Generic;
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

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetupGame();

         
        }

        private void SetupGame()
        {
            List<string> animalEmoji = new List<String>()
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

            Random random = new Random();

            foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
            {
                int randomIndex = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[randomIndex];
                textblock.Text = nextEmoji;
                animalEmoji.RemoveAt(randomIndex);
            }
        }
    }
}
