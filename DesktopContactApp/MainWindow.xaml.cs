using DesktopContactApp.Classes;
using SQLite;
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

namespace DesktopContactApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<Contact> contacts;
		public MainWindow()
		{
			
			InitializeComponent();
			contacts = new List<Contact>();
			ReadDatabase();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			NewContactWindow newContactWindow = new NewContactWindow();
			newContactWindow.ShowDialog();
			ReadDatabase();
		}

		void ReadDatabase() 
		{
			
			using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
			{
				conn.CreateTable<Contact>();
				contacts=(conn.Table<Contact>().ToList()).OrderBy(c=>c.Name).ToList();

				//var variable =from c2 in contacts
				//			  orderby c2.Name
				//			  select c2;
			}
			if (contacts != null) 
			{
				contactsListView.ItemsSource = contacts;
			}
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox? searchTextBox = sender as TextBox;

			var filterList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();

			var filterList2=(from c2 in contacts
							where c2.Name.ToLower().Contains(searchTextBox.Text.ToLower())
							orderby c2.Email
							select c2).ToList();

			contactsListView.ItemsSource= filterList2;
        }
    }
}
