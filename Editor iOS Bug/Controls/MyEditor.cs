using Microsoft.Maui.Controls;

namespace Editor_iOS_Bug.Controls
{
    public class MyEditor : View
    {
        public static readonly BindableProperty TextProperty =
           BindableProperty.Create(nameof(Text), typeof(string), typeof(MyEditor), string.Empty);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
