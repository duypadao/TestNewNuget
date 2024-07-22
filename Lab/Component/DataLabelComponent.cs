using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PDFCreator.Lab.Component
{
    public class DataLabelComponent : IComponent
    {
        private string Label { get; }
        private string LabelTextColor { get; }
        private string LabelBackgroundColor { get; }
        private string Value { get; }
        private string ValueTextColor { get; }
        private string ValueBackGroundColor { get; }
        private string BorderColor { get; }
        public DataLabelComponent(string label,
                                  string value,
                                  string labelTextColor,
                                  string labelBackgroundColor,
                                  string valueTextColor,
                                  string valueBackGroundColor,
                                  string borderColor = "#000000")
        {
            Label = label;
            Value = value;
            LabelTextColor = labelTextColor;
            LabelBackgroundColor = labelBackgroundColor;
            ValueTextColor = valueTextColor;
            ValueBackGroundColor = valueBackGroundColor;
            BorderColor = borderColor;
        }
        public void Compose(IContainer container)
        {
            container
                .BorderBottom(0.5f)
                .BorderColor(BorderColor)
                .Row(row =>
                {
                    row.ConstantItem(130)
                       .Background(LabelBackgroundColor)
                       .PaddingHorizontal(10)
                       .AlignMiddle()
                       .Text(Label)
                       .FontColor(LabelTextColor)
                       .Bold();
                    row.RelativeItem()
                       .Background(ValueBackGroundColor)
                       .PaddingHorizontal(10)
                       .AlignMiddle()
                       .Text(Value)
                       .FontColor(ValueTextColor)
                       .Italic();
                });
        }
    }
}
