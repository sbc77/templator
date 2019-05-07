namespace TemplatorEngine.Core.Model
{
    public class ElementStyle
    {
        public ElementStyle(StyleAttribute attribute, object value)
        {
            this.Attribute = attribute;
            this.Value = value;
        }
        public StyleAttribute Attribute { get; }
        
        public object Value { get; }
    }

    public enum StyleAttribute
    {
        Align,
        FontSize,
        FontFamily,
        
    }
}