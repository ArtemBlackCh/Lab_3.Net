using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using PlantLib;

namespace PlantApp;

public partial class MainWindow : Window
{
    private readonly ObservableCollection<Plant> plants = new();

    public MainWindow()
    {
        InitializeComponent();
        PlantList.ItemsSource = plants;
    }

    private void KindBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ExtraLabel == null)
        {
            return;
        }
        ExtraLabel.Text = KindBox.SelectedIndex == 0 ? "Вік" : "Колір";
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        string name = NameBox.Text.Trim();
        string type = TypeBox.Text.Trim();
        string place = PlaceBox.Text.Trim();
        string extra = ExtraBox.Text.Trim();

        if (name == "" || type == "" || place == "" || extra == "")
        {
            MessageBox.Show("Заповніть усі поля");
            return;
        }

        if (KindBox.SelectedIndex == 0)
        {
            if (!int.TryParse(extra, out int age) || age < 0)
            {
                MessageBox.Show("Вік має бути цілим числом не менше 0");
                return;
            }
            plants.Add(new Tree(name, type, place, age));
        }
        else
        {
            plants.Add(new Flower(name, type, place, extra));
        }

        AverageText.Text = $"Середній вік дерев: {PlantCalculator.GetAverageTreeAge(plants):F1}";

        NameBox.Clear();
        TypeBox.Clear();
        PlaceBox.Clear();
        ExtraBox.Clear();
    }

    private void Water_Click(object sender, RoutedEventArgs e)
    {
        if (PlantList.SelectedItem is Plant plant)
        {
            MessageBox.Show(plant.Water());
        }
        else
        {
            MessageBox.Show("Оберіть рослину зі списку");
        }
    }

    private void PlantIt_Click(object sender, RoutedEventArgs e)
    {
        if (PlantList.SelectedItem is Plant plant)
        {
            MessageBox.Show(plant.PlantIt());
        }
        else
        {
            MessageBox.Show("Оберіть рослину зі списку");
        }
    }
}
