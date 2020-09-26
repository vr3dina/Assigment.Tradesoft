using System.Windows;
using Tradesoft.Analogues.Core;
using Tradesoft.Analogues.Database.Repos;

namespace Tradesoft.Analogues.WPFApp
{
    /// <summary>
    /// Interaction logic for FindPath.xaml
    /// </summary>
    public partial class FindPathWindow : Window
    {
        private readonly AnalogyGraph _analogyGraph;
        private readonly ProductRepository productRepository;

        public FindPathWindow()
        {
            InitializeComponent();
            _analogyGraph = new AnalogyGraph();
            productRepository = new ProductRepository();
        }

        private void findPathButton_Click(object sender, RoutedEventArgs e)
        {
            var p1 = productRepository.Get(tbFromVendorCode.Text, tbFromManufacturer.Text);
            var p2 = productRepository.Get(tbToVendorCode.Text, tbToManufacturer.Text);
            if (int.TryParse(tbDepth.Text, out int depth) && p1 != null && p2 != null)
            {
                var paths = _analogyGraph.GetPath(p1, p2, depth);
                if (paths.Count == 0)
                {
                    MessageBox.Show($"Не найдено за {depth} шагов");
                    return;
                }
                ShowPathsWindow window = new ShowPathsWindow(paths);
                window.Show();
            }
            else
            {
                string error = p1 == null ? "Продукт 1 не найден\n" : "";
                error += p2 == null ? "Продукт 2 не найден\n" : "";
                error += p1 != null && p2 != null ? "Неверная глубина рекурсии" : "";
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
