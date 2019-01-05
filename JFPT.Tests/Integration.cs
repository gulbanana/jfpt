using JFPT;
using Xamarin.Forms;
using Xunit;

public class Integration
{
    [Fact]
    public void DB()
    {
        Data.Init(Device.macOS);
        using (var connection = Data.Connect())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select [Hello] from [Test] limit 1";
                var data = command.ExecuteScalar();
                Assert.Equal("Hello, dataworld!", data);
            }
        }
    }

    [Fact]
    public void UI()
    {
        var page = new MainPage();
        Assert.Equal("Hello, world!", page.MainLabel.Text);
    }
}
