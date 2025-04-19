using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using PDFCreator.Lab.Extension;

namespace PDFCreator.Lab.Component
{
    public class DataLabelMultiValueComponent : IComponent
    {
        private string Label { get; }
        private string LabelTextColor { get; }
        private string LabelBackgroundColor { get; }
        private List<string> Value { get; }
        private string ValueTextColor { get; }
        private string ValueBackGroundColor { get; }
        private string BorderColor { get; }
        public DataLabelMultiValueComponent(string label,
                                  List<string> value,
                                  string labelTextColor,
                                  string labelBackgroundColor,
                                  string valueTextColor,
                                  string valueBackGroundColor,
                                  string borderColor = "#000000",
                                  bool isCentered = false)
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
                       .Column(column =>
                       {
                           foreach (var item in Value)
                           {
                               column.Item()
                                     .AlignMiddle()
                                     .AlignCenter()
                                     .Text(item)
                                     .FontColor(ValueTextColor)
                                     .Italic();

                           }

                       });
                });
        }
        

    }
}
