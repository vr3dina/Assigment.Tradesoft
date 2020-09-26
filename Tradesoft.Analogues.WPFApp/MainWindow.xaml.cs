using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tradesoft.Analogues.Database.Models;
using Tradesoft.Analogues.Database.Repos;

namespace Tradesoft.Analogues.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<Analogy> analogies;
        private readonly AnalogyRepository analogyRepository;
        public MainWindow()
        {
            InitializeComponent();

            analogyRepository = new AnalogyRepository();

            //dgAnalogues.Columns[0].Visibility = Visibility.Hidden;
            //dgAnalogues.Columns[1].Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = analogyRepository.GetAll().ToList();
            analogies = new BindingList<Analogy>(list);
            analogies.AddingNew += (sender1, e1) =>
            {
                e1.NewObject = new Analogy() { Product1 = new Product(), Product2 = new Product() };
            };
            dgAnalogues.ItemsSource = analogies;
        }

        private void dgAnalogues_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var analogy = analogyRepository.CreateOrUpdate(e.Row.Item as Analogy);

            if (analogy == null)
            {
                analogies.Remove(analogies[dgAnalogues.SelectedIndex]);
                MessageBox.Show("Ошибка при содании элемента", "Error");
                return;
            }

            analogies[dgAnalogues.SelectedIndex] = analogy;
        }

        private void findPathButton_Click(object sender, RoutedEventArgs e)
        {
            FindPathWindow findPathWindow = new FindPathWindow();
            findPathWindow.Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Analogy item = dgAnalogues.SelectedItem as Analogy;
            analogyRepository.Delete(item.Product1.Id, item.Product2.Id);
            analogies.RemoveAt(dgAnalogues.SelectedIndex);
        }
    }
}
