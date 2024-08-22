using System.Windows;
using Controls = System.Windows.Controls;

namespace Muse.UI.Utilities;

public class Btn : Controls.RadioButton
{
    static Btn()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Btn), new FrameworkPropertyMetadata(typeof(Btn)));
    }
}