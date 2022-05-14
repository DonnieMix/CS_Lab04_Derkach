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
using System.Windows.Shapes;
using CS_Lab04_Derkach.ViewModels;
using CS_Lab04_Derkach.Models;

namespace CS_Lab04_Derkach.Views
{
    /// <summary>
    /// Interaction logic for UsersView.xaml
    /// </summary>
    public partial class UsersView : Window
    {
        private UsersViewModel _viewModel;
        public UsersView()
        {
            InitializeComponent();
            _viewModel = new UsersViewModel(this);
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.BAdd_Click(sender, e);
        }

        private void BRefresh_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.BRefresh_Click(sender, e);
        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.BEdit_Click(sender, e);
        }

        private void BRemove_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.BRemove_Click(sender, e);
        }

        private void DGUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.DGUsers_SelectionChanged(sender, e);
        }
    }
}
