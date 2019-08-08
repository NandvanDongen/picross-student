using PiCross;
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
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for Puzzle.xaml
    /// </summary>
    public partial class PlayablePuzzle : Page
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer("C://Users/Nand/Documents/schooljaar_2/semester_2/VGO/Music_VGO/music.wav");
        System.Media.SoundPlayer player2 = new System.Media.SoundPlayer("C://Users/Nand/Documents/schooljaar_2/semester_2/VGO/Music_VGO/intromusic.wav");

        public PlayablePuzzle(Puzzel puzzel)
        {
            player.PlayLooping();
            InitializeComponent();
            DataContext = new GridViewModel(puzzel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            player2.PlayLooping();
            NavigationService.Navigate(new StartScreen());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            player2.PlayLooping();
            NavigationService.Navigate(new SelectionScreen());
        }

        private void End(object sender, RoutedEventArgs e)
        {
            player2.PlayLooping();
            NavigationService.Navigate(new SelectionScreen());
        }
    }
}
