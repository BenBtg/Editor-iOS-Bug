using Editor_iOS_Bug.Controls;

namespace Editor_iOS_Bug {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            MainPage = new ContentPage();

            VerticalStackLayout vert = new();
            (MainPage as ContentPage).Content = vert;

            //Border border = new();
            //border.StrokeThickness = 4;
            //border.BackgroundColor = Colors.DarkBlue;
            //border.Stroke = Colors.Red;
            //border.Padding = 10;
            //vert.Children.Add(border);

            ContentView contentView = new();
            
            MyEditor editor = new();
            editor.BackgroundColor = Colors.White;
           // editor.AutoSize = EditorAutoSizeOption.TextChanges;
            editor.MaximumHeightRequest = 200;
            // border.Content = editor;
            contentView.Content = editor;
            contentView.BackgroundColor = Colors.DarkBlue;
            contentView.Padding = new Thickness(10);

            vert.Children.Add(contentView);

            Label testLabel = new Label();
            string bugText = "Editor iOS Bug (.NET 7):\n- Editor does not autoscroll downward as you fill the Editor with text (new lines past max height go down off screen)\n- Editor does not scroll with click and drag once past maxHeight in size.\n- There is a frame lag after resizing Editor and before resizing the parent so the Editor goes 'out of bounds' of its parent momentarily each time it grows vertically.";
            bugText += "\n\nEditor iOS Bug (.NET 8):\n- Still has a frame lag as you type new lines.\n- Scrolling by touch and drag is not constrained, so you can slide it up all the way out of view.\n- The new line auto-scrolling is glitchy - if you keep pressing enter on touch screen keyboard it will keep moving the white text field higher and higher until it goes out of view.\n- Could have option to let pulling up down past bounds and then elastically return to rest position, but should also have option to not allow scrolling by finger past the typical edge.";
            testLabel.Text = bugText;
            vert.Children.Add(testLabel);

        }
    }

}