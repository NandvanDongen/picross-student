using DataStructures;
using PiCross;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using Grid = DataStructures.Grid;
using Size = DataStructures.Size;


namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer("C://Users/Nand/Documents/schooljaar_2/semester_2/VGO/Music_VGO/intromusic.wav");
        public MainWindow()
        {
            player.PlayLooping();
            InitializeComponent();
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
