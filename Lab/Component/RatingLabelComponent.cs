using PDFCreator.Lab.Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PDFCreator.Lab.Component
{
    public class RatingLabelComponent : IComponent
    {
        private Rating _rating { get; }
        public RatingLabelComponent(Rating rating)
        {
            _rating = rating;
        }
        public void Compose(IContainer container)
        {
            container
                .Row(row =>
                {
                    row.ConstantItem(90)
                       .Column(col =>
                       {
                           col.Item().BorderBottom(0.5f).Text("Pass:");
                           col.Item().BorderBottom(0.5f).Text("Fail:");
                           col.Item().BorderBottom(0.5f).Text("Data Only:");
                           col.Item().BorderBottom(0.5f).Text("N/A:");
                       });
                    row.RelativeItem()
                       .Column(col =>
                       {
                           col.Item().BorderBottom(0.5f).Text(_rating == Rating.Pass ? "X" : "");
                           col.Item().BorderBottom(0.5f).Text(_rating == Rating.Fail ? "X" : "");
                           col.Item().BorderBottom(0.5f).Text(_rating == Rating.Data ? "X" : "");
                           col.Item().BorderBottom(0.5f).Text(_rating == Rating.NotApplicable ? "X" : "");
                       });
                });
        }
    }
}
