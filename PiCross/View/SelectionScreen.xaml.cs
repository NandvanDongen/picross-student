using PiCross;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for SelectionScreen.xaml
    /// </summary>
    public partial class SelectionScreen : Page
    {
        public SelectionScreen()
        {
            InitializeComponent();
            DataContext = new PuzzlesViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartScreen());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PlayablePuzzle((sender as Button).Tag as Puzzel));
        }
    }
}
