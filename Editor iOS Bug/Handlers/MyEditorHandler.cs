#if IOS || MACCATALYST
using PlatformView = UIKit.UITextView;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif
using Microsoft.Maui.Handlers;
using Editor_iOS_Bug.Controls;

namespace Editor_iOS_Bug;

public partial class MyEditorHandler
{
#if IOS
    public static IPropertyMapper<MyEditor, MyEditorHandler> PropertyMapper = new PropertyMapper<MyEditor, MyEditorHandler>(ViewHandler.ViewMapper)
    {
    };

    public MyEditorHandler() : base(PropertyMapper)
    {
    }
#endif
}