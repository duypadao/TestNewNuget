using PDFCreator.Lab.Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Reflection;
using System.Reflection.PortableExecutable;
using static QuestPDF.Helpers.Colors;

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
        }

        [Obsolete]
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
                    for (int i = 0; i < PropertyInfo.Length; i++)
                    {
                        var colSpan = TableFormats[i] is null ? 1 : TableFormats[i].ColSpan;
                        columns.RelativeColumn(colSpan);
                    }
                });

                table.Header(header =>
                {
                    if (IncludeIndex)
                    {
                        header.Cell().Element(CellHeaderStyle).Text("STT");
                    }
                    foreach (PropertyInfo property in PropertyInfo)
                    {
                        header.Cell().Element(CellHeaderStyle).Text(property.Name).Align;
                    }
                });
                for (int i = 0; i < Values.Count; i++)
                {
                    var item = Values[i];
                    if (IncludeIndex)
                    {
                        table.Cell().Element(CellStyle).AlignCenter().Text(i + 1);
                    }
                    foreach (PropertyInfo property in PropertyInfo)
                    {
                        table.Cell().Element(CellStyle).Text(property.GetValue(item));
                    }
                }
            });
        }
        private static IContainer CellHeaderStyle(IContainer container)
        {
            return container
                .DefaultTextStyle(x => x.Bold())
                .PaddingVertical(5)
                .BorderBottom(1)
                .BorderColor(Black);
        }

        private static IContainer CellStyle(IContainer container)
        {
            return container
                .DefaultTextStyle(x => x.Medium())
                .PaddingVertical(5)
                .BorderBottom(1)
                .BorderColor(Black);
        }
    }
}
