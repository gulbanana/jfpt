using JFPT;
using Xunit;

public class Integration
{
    [Fact]
    public void Test()
    {
        var page = new MainPage();
        Assert.Equal("Welcome to Xamarin.Forms!", page.MainLabel.Text);
    }
}
