namespace Otakulore.Views;

public partial class HomePage
{

    private int _count;

    public HomePage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        _count++;
        CounterLabel.Text = $"Current count: {_count}";
        SemanticScreenReader.Announce(CounterLabel.Text);
    }

}