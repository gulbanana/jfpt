using System;
using Xamarin.Forms;

namespace JFPT
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            try
            {
                using (var connection = Data.Connect())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "select [Hello] from [Test] limit 1";
                        var data = command.ExecuteScalar();
                        MainLabel.Text = (string)data;
                    }
                }
            }
            catch (Exception)
            {
                MainLabel.Text = "Hello, world!";
            }
        }
    }
}
