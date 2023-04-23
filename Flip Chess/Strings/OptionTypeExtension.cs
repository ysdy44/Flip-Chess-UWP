using Windows.UI.Xaml.Markup;

namespace Flip_Chess.Strings
{
    [MarkupExtensionReturnType(ReturnType = typeof(OptionType))]
    public class OptionTypeExtension : MarkupExtension
    {
        public OptionType Type { get; set; }
        protected override object ProvideValue() => this.Type;
    }
}