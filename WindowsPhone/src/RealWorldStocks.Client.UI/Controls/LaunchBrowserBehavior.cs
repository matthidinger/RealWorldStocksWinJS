using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using Microsoft.Phone.Tasks;

namespace RealWorldStocks.Client.UI.Controls
{
    //public static class Browser
    //{
    //            /// <summary>
    //    /// A property definition representing attached triggers and messages.
    //    /// </summary>
    //    public static readonly DependencyProperty AttachProperty =
    //        DependencyProperty.RegisterAttached(
    //            "External",
    //            typeof(string),
    //            typeof(Browser),
    //            new PropertyMetadata(OnAttachChanged)
    //            );

    //    /// <summary>
    //    /// Sets the attached triggers and messages.
    //    /// </summary>
    //    /// <param name="d">The element to attach to.</param>
    //    /// <param name="attachText">The parsable attachment text.</param>
    //    public static void SetAttach(DependencyObject d, string attachText)
    //    {
    //        d.SetValue(AttachProperty, attachText);
    //    }

    //    /// <summary>
    //    /// Gets the attached triggers and messages.
    //    /// </summary>
    //    /// <param name="d">The element that was attached to.</param>
    //    /// <returns>The parsable attachment text.</returns>
    //    public static string GetAttach(DependencyObject d)
    //    {
    //        return d.GetValue(AttachProperty) as string;
    //    }

    //    private static void OnAttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        if(e.NewValue == e.OldValue)
    //            return;

    //        var messageTriggers = (System.Windows.Interactivity.TriggerBase[])d.GetValue(MessageTriggersProperty);
    //        var allTriggers = Interaction.GetTriggers(d);

    //        if(messageTriggers != null)
    //            messageTriggers.Apply(x => allTriggers.Remove(x));

    //        var newTriggers = Parser.Parse(d, e.NewValue as string).ToArray();
    //        newTriggers.Apply(allTriggers.Add);

    //        if(newTriggers.Length > 0)
    //            d.SetValue(MessageTriggersProperty, newTriggers);
    //        else d.ClearValue(MessageTriggersProperty);
    //    }
    //}

    //}

    public class LaunchBrowserBehavior : Behavior<ButtonBase>
    {
        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register  ("Url", typeof (string), typeof (LaunchBrowserBehavior), null);

        public string Url
        {
            get { return (string) GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += OnClick;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var task = new WebBrowserTask {Uri = new Uri(Url, UriKind.Absolute)};
            task.Show();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= OnClick;
        }
    }
}