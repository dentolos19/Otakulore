using Microsoft.UI.Xaml;

namespace Otakulore.Controls;

public sealed partial class ResultIndicator
{

    private static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(ResultIndicator), PropertyMetadata.Create("No Result"));
    private static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof(State), typeof(ResultIndicatorState), typeof(ResultIndicator), PropertyMetadata.Create(ResultIndicatorState.None));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ResultIndicatorState State
    {
        get => (ResultIndicatorState)GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    public ResultIndicator()
    {
        InitializeComponent();
    }

}