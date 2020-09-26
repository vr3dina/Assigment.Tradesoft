using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Tradesoft.Analogues.Database.Models;

namespace Tradesoft.Analogues.WPFApp
{
    /// <summary>
    /// Interaction logic for ShowPathsWindow.xaml
    /// </summary>
    public partial class ShowPathsWindow : Window
    {
        private List<List<string>> _paths;
        private List<string> _names;
        public ShowPathsWindow(List<List<Product>> paths)
        {
            InitializeComponent();

            _paths = paths.Select(p => getPaths(p).ToList()).ToList();
            _names = Enumerable.Range(1, _paths.Count).Select(e => "Маршрут " + e.ToString()).ToList();

            dgPathNames.ItemsSource = _names;
            dgPaths.SelectedIndex = 0;
        }

        private IEnumerable<string> getPaths(List<Product> pathList)
        {
            List<string> path = new List<string>(pathList.Count - 1);
            for (int i = 0; i < pathList.Count - 1; i++)
            {
                path.Add(pathList[i].ToString() + " -> " + pathList[i + 1].ToString());
            }
            return path;
        }

        private void dgPathNames_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            dgPaths.ItemsSource = _paths[dgPathNames.SelectedIndex];
        }
    }
}
