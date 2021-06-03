using System.Windows.Controls;
using Otakulore.Core;

namespace Otakulore
{

    public partial class App
    {

        internal static UserData Settings { get; } = UserData.LoadData();
        
        public static void NavigateSinglePage(Page view)
        {
            if (Current.MainWindow is MainWindow window)
                window.View.Navigate(view);
        }

    }

}