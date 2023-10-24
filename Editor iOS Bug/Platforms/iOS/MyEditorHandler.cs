#nullable enable
using Microsoft.Maui.Handlers;
using Editor_iOS_Bug.Controls;
using UIKit;
using Microsoft.Maui.Platform;
using CoreGraphics;

namespace Editor_iOS_Bug;

public partial class MyEditorHandler : ViewHandler<MyEditor, UITextView>
{
    NSLayoutConstraint _heightConstraint;
    nfloat _maxHeightRequest = 200.0f;
    bool _textUpdating;

    protected override UITextView CreatePlatformView() => new();

    protected override void ConnectHandler(UITextView platformView)
    {
        base.ConnectHandler(platformView);

        // Perform any control setup here

        platformView.Layer.BorderWidth = 4;
        platformView.Layer.BorderColor = UIColor.Red.CGColor;

        platformView.TranslatesAutoresizingMaskIntoConstraints = false;

        _heightConstraint = NSLayoutConstraint.Create(
            platformView,
            NSLayoutAttribute.Height,
            NSLayoutRelation.LessThanOrEqual,
            1,
            _maxHeightRequest
        );

        platformView.AddConstraint(_heightConstraint);
        platformView.Changed += TextViewChanged;
    }

    protected override void DisconnectHandler(UITextView platformView)
    {
        platformView.Changed -= TextViewChanged;
        platformView.Dispose();

        base.DisconnectHandler(platformView);
    }

    public static void MapMaximumHeight(MyEditorHandler handler, MyEditor editor)
    {
        if (handler._heightConstraint is not NSLayoutConstraint constraint)
            return;

        if (double.IsInfinity(editor.MaximumHeightRequest))
            handler._maxHeightRequest = 100.0f; // Default to a reasonable value
        else
            handler._maxHeightRequest = new nfloat(editor.MaximumHeightRequest);
    }

    public static void MapBackgroundColor(MyEditorHandler handler, MyEditor editor)
        => handler.PlatformView.BackgroundColor = editor.BackgroundColor.ToPlatform();

    public static void MapText(MyEditorHandler handler, MyEditor editor)
        => handler.UpdatePlatformText();

    void TextViewChanged(object sender, EventArgs e)
    {
        SetHeightConstraint();
        UpdateVirtualText();
    }

    void SetHeightConstraint()
    {
        var contentSize = PlatformView.ContentSize;
        var newHeight = contentSize.Height;

        var maxHeight = _maxHeightRequest;

        if (newHeight > maxHeight)
            newHeight = maxHeight;

        _heightConstraint.Constant = newHeight;
    }

    void UpdateVirtualText()
    {
        if (_textUpdating)
            return;

        _textUpdating = true;

        // Update virtual text
        VirtualView.Text = PlatformView.Text;

        _textUpdating = false;
    }

    void UpdatePlatformText()
    {
        if (_textUpdating)
            return;

        _textUpdating = true;

        // Update platform text
        PlatformView.Text = VirtualView.Text;

        _textUpdating = false;
    }


}