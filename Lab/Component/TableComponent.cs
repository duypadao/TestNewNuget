using PDFCreator.Lab.Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Reflection;
using System.Reflection.PortableExecutable;

namespace PDFCreator.Lab.Component
{
    public class TableComponent<T> : IComponent
    {
        private readonly List<T> Values;
        private readonly PropertyInfo[] PropertyInfo;
        private readonly bool IncludeIndex;
        private readonly TableFormat DefaultTableFormat = new TableFormat()
        {
            ColSpan = 1,
            Align = Align.Left
        };
        private readonly List<TableFormat> TableFormats;
        private readonly Dictionary<Align, Action<string, IContainer>> HeaderAlign;
        private readonly Dictionary<Align, Action<string, IContainer>> ContentAlign;

        public TableComponent(List<T> values, List<TableFormat>? tableFormats = null, bool includeIndex = true)
        {
            Values = values;
            PropertyInfo = typeof(T).GetProperties();
            IncludeIndex = includeIndex;
            if (tableFormats == null)
            {
                tableFormats = new List<TableFormat>();
            }
            if (tableFormats.Count < values.Count)
            {
                int less = values.Count - tableFormats.Count;
                for (int i = 0; i < less; i++)
                {
                    tableFormats.Add(DefaultTableFormat);
                }
            }
            TableFormats = tableFormats;
            HeaderAlign = new Dictionary<Align, Action<string, IContainer>>
            {
                { Align.Left, (text, cell) => cell.Element(CellHeaderStyle).AlignLeft().Text(text) },
                { Align.Right, (text, cell) => cell.Element(CellHeaderStyle).AlignRight().Text(text) },
                { Align.Center, (text, cell) => cell.Element(CellHeaderStyle).AlignCenter().Text(text) }
            };

            ContentAlign = new Dictionary<Align, Action<string, IContainer>>
            {
                { Align.Left, (value, cell) => cell.Element(CellStyle).AlignLeft().Text(value) },
                { Align.Right, (value, cell) => cell.Element(CellStyle).AlignRight().Text(value) },
                { Align.Center, (value, cell) => cell.Element(CellStyle).AlignCenter().Text(value) }
            };
        }

        public void Compose(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    if (IncludeIndex)
                    {
                        columns.ConstantColumn(40);
                    }
                    for (int j = 0; j < PropertyInfo.Length; j++)
                    {
                        columns.RelativeColumn(TableFormats[j].ColSpan);
                    }
                });
                table.Header(header =>
                {
                    if (IncludeIndex)
                    {
                        header.Cell().Element(CellHeaderStyle).Text("STT").AlignCenter();
                    }
                    for (int j = 0; j < PropertyInfo.Length; j++)
                    {
                        var align = TableFormats[j].Align;
                        var text = TableFormats[j].Caption ?? PropertyInfo[j].Name;
                        HeaderAlign[align](text, header.Cell());
                    }
                });

                for (int i = 0; i < Values.Count; i++)
                {
                    if (IncludeIndex)
                    {
                        table.Cell().Element(CellStyle).AlignCenter().Text($"{i + 1}");
                    }
                    for (int j = 0; j < PropertyInfo.Length; j++)
                    {
                        var align = TableFormats[j].Align;
                        var value = PropertyInfo[j].GetValue(Values[i])?.ToString() ?? string.Empty;
                        
                        ContentAlign[align](value, table.Cell());
                    }
                }
            });
        }
        private static IContainer CellHeaderStyle(IContainer container)
        {
            return container
                .DefaultTextStyle(x => x.Bold())
                .PaddingVertical(5)
                .BorderBottom(0.5f)
                .BorderColor(Colors.Black);
        }

        private static IContainer CellStyle(IContainer container)
        {
            return container
                .DefaultTextStyle(x => x.Medium())
                .PaddingVertical(5)
                .BorderBottom(0.5f)
                .BorderColor(Colors.Black);
        }
    }
}
